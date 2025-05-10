using CDHelper.Coordinators;
using CDHelper.Models.PacketData;
using CDHelper.Structs;
using CDHelper.Utils;
using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

namespace CDHelper.Services
{
    public class JukeboxService
    {
        private readonly GEarthExtension _extension;
        private readonly NotificationService _notificationService;

        public JukeboxService(GEarthExtension extension, NotificationService notificationService)
        {
            _extension = extension;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Receives and parses Trax song information from the server.
        /// Recebe e processa as informacoes das musicas Trax do servidor.
        /// </summary>
        public async Task<List<TraxSongData>?> GetTraxSongInfoAsync()
        {
            try
            {
                // Waits for the packet with song information
                // Espera o pacote com as informações da música
                var packetArgs = await _extension.ReceiveAsync(In.TraxSongInfo, 2000);

                if (packetArgs is not null)
                {
                    // Adds the CDs to the room coordinator
                    // Adiciona os CDs no coordenador do quarto
                    var jukeInfo = packetArgs.Read<TraxSongData[]>();

                    return jukeInfo?.ToList() ?? [];
                }
            }
            catch (Exception)
            {

            }

            return null;

        }

        /// <summary>
        /// Retrieves the list of CDs from the jukebox  
        /// Obtém a lista de CDs do jukebox  
        /// </summary>
        public async Task<List<CdData>?> GetJukeboxCds()
        {
            // Requests the jukebox playlist
            // Solicita a lista de músicas do jukebox
            _extension.Send(Out.GetJukeboxPlayList);

            List<TraxSongData>? jukeInfo = await GetTraxSongInfoAsync();

            if (jukeInfo is not null)
            {
                var data = jukeInfo.Select(x => new CdData(x));
                RoomCdCoordinator.AddCdsToRoom(data);
            }

            // Retrieves the CDs from the current room
            // Obtém os CDs do quarto atual
            var cds = RoomCdCoordinator.GetCurrentRoomCds();

            return cds?.ToList();
        }

        /// <summary>
        /// Retrieves and processes marketplace cds offer data
        /// Recupera e processa os dados de ofertas de cds do marketplace
        /// </summary>
        public async Task GetMarketCDDataAsync()
        {
            _extension.Send(Out.GetMarketplaceOffers, -1, -1, DefaultMarketCD.GetName(), 1);

            // Capture the marketplace offers packet
            // Captura o pacote de ofertas do marketplace
            var packetArgs = await _extension.ReceiveAsync(In.MarketPlaceOffers);

            // Parse the offer data from the packet
            // Converte os dados da oferta do pacote
            OfferData[] offers = packetArgs.Read<OfferData[]>();

            OfferData? offer = offers.FirstOrDefault(x => x.FurniId == FurniIds.CD);

            if (offer == null)
            {
                _notificationService.SendNoCdsFoundNotification();
                return;
            }

            // Extract author and title
            // Extrai o nome do autor e do cd
            string[] nameParts = offer.Name!.Split('\n');

            string author = nameParts.First().Trim();
            string title = nameParts.Last().Trim();

            CdData cdData = new(title, author);

            _notificationService.SendMarketNotification(cdData);
        }


        /// <summary>
        /// Retrieves and processes the CDs data from the room's jukebox
        /// Recupera e processa os dados de CDs do jukebox do quarto
        /// </summary>
        public async Task GetJukeboxCdsAsync()
        {
            var cds = await GetJukeboxCds();

            _notificationService.SendCdsFoundNotification(cds, NotificationBadges.Jukebox);
        }

    }
}

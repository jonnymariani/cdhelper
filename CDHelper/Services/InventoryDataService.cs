using CDHelper.Models.PacketData;
using CDHelper.Structs;
using CDHelper.Utils;
using Xabbo.Core.Game;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

namespace CDHelper.Services
{
    public class InventoryDataService
    {
        private readonly GEarthExtension _extension;
        private readonly NotificationService _notificationService;
        private readonly InventoryManager _inventoryManager;
        private readonly JukeboxService _jukeboxService;
        private readonly FurniHelper _furniHelper;

        public InventoryDataService(GEarthExtension extension, NotificationService notificationService, InventoryManager inventoryManager, JukeboxService jukeboxService, FurniHelper furniHelper)
        {
            _extension = extension;
            _notificationService = notificationService;
            _inventoryManager = inventoryManager;
            _jukeboxService = jukeboxService;
            _furniHelper = furniHelper;
        }

        /// <summary>
        /// Retrieves the list of CDs from the inventory  
        /// Recupera a lista de CDs do inventário  
        /// </summary>
        public async Task<List<CdData>?> GetInventoryCds()
        {
            try
            {
                // Loads the inventory   
                // Carrega o inventário
                IInventory inventory = await _inventoryManager.LoadInventoryAsync();

                // Filters the furni to get only the CDs (kind 2322)  
                // Filtra os furnis para pegar apenas os CDs (tipo 2322) 
                var cdFurni = inventory?
                .Where(x => x.Kind == 2322)
                .ToArray();

                // List to store song IDs  
                // Lista para armazenar os IDs das musicas  
                List<int> songIds = [];

                if (cdFurni != null)
                {
                    foreach (var furni in cdFurni)
                    {
                        // Get the extra value from the furni  
                        // Pega o valor extra do mobi  
                        int? extraValue = _furniHelper.GetExtraValue(furni);

                        if (extraValue != null)
                            songIds.Add(extraValue.Value);
                    }
                }

                // Initialize the list to store CD data  
                // Inicializa a lista para armazenar os dados dos CDs  
                List<CdData> cds = [];

                // Send requests in chunks of 20 song IDs  
                // Envia solicitações em pedaços de 20 IDs de músicas  
                var chunkSize = 20;
                for (int i = 0; i < songIds.Count; i += chunkSize)
                {
                    // Get the current chunk of song IDs  
                    // Obtém o pedaço atual dos IDs das músicas  
                    var chunk = songIds.Skip(i).Take(chunkSize).ToArray();

                    // Sends a request to get song information for the current chunk  
                    // Envia uma solicitação para obter informações sobre as músicas para o pedaço atual  
                    _extension.Send(Out.GetSongInfo, chunk.Length, chunk);

                    // Wait for the song info response  
                    // Aguarda pela resposta da informação da música  
                    List<TraxSongData>? jukeInfo = await _jukeboxService.GetTraxSongInfoAsync();

                    // Process the song data  
                    // Processa os dados das músicas  
                    if (jukeInfo != null)
                    {
                        var data = jukeInfo
                            .Select(x => new CdData(x.Title ?? string.Empty, x.Author ?? string.Empty))
                            .ToList();

                        // Add data to final list  
                        // Adiciona os dados a lista final  
                        cds.AddRange(data);
                    }
                }

                return cds;

            }
            catch (TimeoutException e)
            {
                _notificationService.SendToastNotification("Unable to load inventory data.", NotificationBadges.Error);

                return null;
            }


        }
    }
}

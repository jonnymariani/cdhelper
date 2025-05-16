using CDHelper.Models.PacketData;
using CDHelper.Structs;
using System.Text.Json;
using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

namespace CDHelper.Services
{
    public class ExportService
    {
        private readonly NotificationService _notificationService;
        private readonly RoomDataService _roomDataService;
        private readonly JukeboxService _jukeboxService;
        private readonly InventoryDataService _inventoryService;
        private readonly GEarthExtension _extension;

        public ExportService(NotificationService notificationService, RoomDataService roomDataService, JukeboxService jukeboxService, InventoryDataService inventoryService, GEarthExtension extension)
        {
            _notificationService = notificationService;
            _roomDataService = roomDataService;
            _jukeboxService = jukeboxService;
            _inventoryService = inventoryService;
            _extension = extension;
        }

        ///// <summary>
        ///// Exports the CDs from the current room  
        ///// Exporta os CDs do quarto atual
        ///// </summary>
        //public void ExportRoom()
        //{
        //    _notificationService.SendExportingNotification();

        //    // Retrieves the list of CDs in the current room  
        //    // Obtem a lista de CDs do quarto atual
        //    var cds = _roomDataService.GetRoomCds();

        //    // Exports the retrieved CDs  
        //    // Exporta os CDs obtidos
        //    Export(cds, ExportSuffix.Room);
        //}


        /// <summary>
        /// Exports the CDs from the current room  
        /// Exporta os CDs do quarto atual
        /// </summary>
        public async Task ExportRoom(bool ehBom)
        {
            _notificationService.SendExportingNotification();

            // Retrieves the list of CDs in the current room  
            // Obtem a lista de CDs do quarto atual
            var cds = _roomDataService.GetRoomCds();

            if (cds == null)
                return;

            _extension.Send(Out.GetSongInfo, cds.Select(x => x.SongId).ToArray());


            // Waits for the packet with song information
            // Espera o pacote com as informações da música
            var packetArgs = await _extension.ReceiveAsync(In.TraxSongInfo, 2000);
            List<CdData> res = new List<CdData>();

            if (packetArgs is not null)
            {
                // Adds the CDs to the room coordinator
                // Adiciona os CDs no coordenador do quarto
                var jukeInfo = packetArgs.Read<TraxSongData[]>();

                foreach (var item in jukeInfo)
                {
                    CdData cdData = new(item);
                    res.Add(cdData);
                }

            }

            // Exports the retrieved CDs  
            // Exporta os CDs obtidos
            Export(res, ExportSuffix.Room, ehBom);
        }

        /// <summary>
        /// Exports the CDs from the inventory  
        /// Exporta os CDs do inventario  
        /// </summary>
        public async Task ExportInventory()
        {
            _notificationService.SendExportingNotification();

            // Retrieves the list of CDs in the inventory  
            // Obtem a lista de CDs no inventario  
            var cds = await _inventoryService.GetInventoryCds();

            // Exports the retrieved CDs  
            // Exporta os CDs obtidos  
            Export(cds, ExportSuffix.Inventory);
        }

        /// <summary>
        /// Exports the CDs from the jukebox  
        /// Exporta os CDs do jukebox  
        /// </summary>
        public async Task ExportJukebox()
        {
            _notificationService.SendExportingNotification();

            // Retrieves the list of CDs in the jukebox  
            // Obtem a lista de CDs no jukebox  
            var cds = await _jukeboxService.GetJukeboxCds();

            // Exports the retrieved CDs  
            // Exporta os CDs obtidos  
            Export(cds, ExportSuffix.Jukebox);
        }

        /// <summary>
        /// Exports the CDs from the jukebox, inventory and room  
        /// Exporta os CDs do jukebox, inventario e quarto
        /// </summary>
        public async Task ExportAll()
        {
            _notificationService.SendExportingNotification();

            List<CdData> cds = [];

            // Retrieves the list of CDs in the jukebox  
            // Obtem a lista de CDs no jukebox  
            var jukeCds = await _jukeboxService.GetJukeboxCds();

            if (jukeCds is not null)
                cds.AddRange(jukeCds);

            // Retrieves the list of CDs in the inventory  
            // Obtem a lista de CDs no inventario  
            var inventoryCds = await _inventoryService.GetInventoryCds();

            if (inventoryCds is not null)
                cds.AddRange(inventoryCds);

            // Retrieves the list of CDs in the current room  
            // Obtem a lista de CDs do quarto atual
            var roomCds = _roomDataService.GetRoomCds();

            if (roomCds is not null)
                cds.AddRange(roomCds);

            if (cds.Count == 0)
            {
                _notificationService.SendNoCdsFoundNotification();
                return;
            }

            // Exports the retrieved CDs  
            // Exporta os CDs obtidos  
            Export(cds);
        }

        /// <summary>
        /// Exports a list of CDs to a text file, grouped by title and author, 
        /// with a count of duplicates, and opens the file explorer to the generated file.
        /// Exporta uma lista de CDs para um arquivo de texto, agrupados por título e autor, 
        /// com a contagem de duplicatas, e abre o explorador de arquivos no arquivo gerado.
        /// </summary>
        public void Export(List<CdData>? cds, string? suffix = "", bool ehBom = false)
        {
            if (cds == null || cds.Count == 0)
            {
                // Envia notificação se não encontrar nenhum CD
                // Sends a notification if no CDs are found
                _notificationService.SendNoCdsFoundNotification();
                return;
            }

            // Gets the executable directory
            // Obtém o diretório do executável
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory + "export";

            if (!Path.Exists(exeDirectory))
            {
                Directory.CreateDirectory(exeDirectory);
            }

            string? lastFile = Directory
            .GetFiles(exeDirectory, "Cds_Data_*.txt")
            .OrderByDescending(f => File.GetCreationTime(f))
            .FirstOrDefault();

            // Lista de todas as entradas atuais + anteriores (se houver)
            List<string> allEntries = new();

            // Carrega dados antigos do último arquivo
            if (lastFile != null)
            {
                var previousEntries = File.ReadAllLines(lastFile);
                allEntries.AddRange(previousEntries);
            }

            // Formats the date and time to create a unique file name
            // Formata a data e hora para criar um nome de arquivo único

            string fileName = $"Cds_Data_{DateTime.Now:dd_MM_yyyy_HH_mm_ss}.txt";
            string filePath = Path.Combine(exeDirectory, fileName);

            // Groups CDs by Title + Author and counts occurrences
            // Agrupa os CDs por Título + Autor e conta as ocorrências
            var grouped = cds
                .GroupBy(cd => new { cd.Title, cd.Author, cd.SongData, cd.SongId })
                .Select(g =>
                {
                    var obj = new
                    {
                        title = g.Key.Title,
                        author = g.Key.Author,
                        songid = g.Key.SongId,
                        data = g.Key.SongData,
                        isGood = ehBom,
                    };

                    return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });              

                  
                })
                .ToList();

            allEntries.AddRange(grouped);


            // Writes the data to the text file
            // Escreve os dados no arquivo de texto
            File.AppendAllLines(filePath, allEntries);

            //// Opens the file explorer and selects the exported file
            //// Abre o explorador de arquivos e seleciona o arquivo exportado
            //System.Diagnostics.Process.Start("explorer", $"/select,\"{filePath}\"");

            _notificationService.SendExportSuccessNotification(cds.Count);

        }
    }
}

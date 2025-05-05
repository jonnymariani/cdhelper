using CDHelper.Models.PacketData;
using CDHelper.Structs;

namespace CDHelper.Services
{
    public class ExportService
    {
        private readonly NotificationService _notificationService;
        private readonly RoomDataService _roomDataService;
        private readonly JukeboxService _jukeboxService;
        private readonly InventoryDataService _inventoryService;

        public ExportService(NotificationService notificationService, RoomDataService roomDataService, JukeboxService jukeboxService, InventoryDataService inventoryService)
        {
            _notificationService = notificationService;
            _roomDataService = roomDataService;
            _jukeboxService = jukeboxService;
            _inventoryService = inventoryService;
        }

        /// <summary>
        /// Exports the CDs from the current room  
        /// Exporta os CDs do quarto atual
        /// </summary>
        public void ExportRoom()
        {
            _notificationService.SendExportingNotification();

            // Retrieves the list of CDs in the current room  
            // Obtem a lista de CDs do quarto atual
            var cds = _roomDataService.GetRoomCds();

            // Exports the retrieved CDs  
            // Exporta os CDs obtidos
            Export(cds, ExportSuffix.Room);
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
        public void Export(List<CdData>? cds, string? suffix = "")
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
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Formats the date and time to create a unique file name
            // Formata a data e hora para criar um nome de arquivo único
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string fileName = $"CDHelperExport_{timestamp}{(string.IsNullOrEmpty(suffix) ? "" : "_" + suffix)}.txt";
            string filePath = Path.Combine(exeDirectory + "/export", fileName);

            // Groups CDs by Title + Author and counts occurrences
            // Agrupa os CDs por Título + Autor e conta as ocorrências
            var grouped = cds
                .GroupBy(cd => new { cd.Title, cd.Author })
                .Select(g =>
                {
                    string entry = $"{g.Key.Title} - {g.Key.Author}";
                    if (g.Count() > 1)
                        entry += $" x{g.Count()}";
                    return entry;
                })
                .ToList();

            // Writes the data to the text file
            // Escreve os dados no arquivo de texto
            File.WriteAllLines(filePath, grouped);

            // Opens the file explorer and selects the exported file
            // Abre o explorador de arquivos e seleciona o arquivo exportado
            System.Diagnostics.Process.Start("explorer", $"/select,\"{filePath}\"");

            _notificationService.SendExportSuccessNotification(cds.Count);

        }
    }
}

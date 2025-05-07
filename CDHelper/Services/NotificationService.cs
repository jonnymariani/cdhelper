using System.Text;
using CDHelper.Models.PacketData;
using CDHelper.Resources;
using CDHelper.Structs;
using CDHelper.Utils;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

namespace CDHelper.Services
{
    /// <summary>
    /// Handles sending notifications
    /// Gerencia o envio de notificacoes
    /// </summary>
    public class NotificationService(GEarthExtension extension)
    {
        private readonly GEarthExtension _extension = extension;

        public void SendMarketNotification(CdData cdData)
        {
            // Create the notification message
            // Cria a mensagem de notificação
            string notification = LanguageHelper.Get(Messages.OfferBy, cdData.Title, cdData.Author);
            
            SendToastNotification(notification, NotificationBadges.Market);
        }

        public void SendJukeboxCdsNotification(IEnumerable<CdData>? cds)
        {
            if (cds?.Any() == true) // Checks if there are any CDs
            {
                // Builds the message with the list of found CDs
                // Monta a mensagem com a lista de CDs encontrados
                var messageBuilder = new StringBuilder("\n");

                foreach (var cd in cds)
                {
                    // Add each CD to the message with proper formatting
                    // Adiciona cada CD à mensagem com a formatação adequada
                    string authorDisplay = string.IsNullOrEmpty(cd.Author) ? "" : $" - {cd.Author}";
                    messageBuilder.AppendLine($"\t<b>{cd.Title}{authorDisplay}</b>");
                }

                messageBuilder.Append("\n  ");

                string message = messageBuilder.ToString();

                // Sends the notification with the found CDs
                // Envia a notificação com os CDs encontrados
                SendModalNotification(LanguageHelper.Get(Messages.CDsFound, cds.Count()), message, NotificationBadges.Jukebox);
            }
            else
            {
                // Sends a notification if no CDs are found
                // Envia uma notificação caso nenhum CD seja encontrado
                SendNoCdsFoundNotification();
            }
        }

        public void SendHelpNotification()
        {
            string badge = NotificationBadges.Alert;
            string title = LanguageHelper.Get(Messages.Info);
            string message = "\n";
            
            message += $"<small>{LanguageHelper.Get(Messages.ExtensionDescription)}.</small>\n\n";

            message += $"<b>{LanguageHelper.Get(Messages.CommandList)}:</b>\n\n";

            message += $"<b>{Commands.GetJukeboxCds}</b> - {LanguageHelper.Get(Messages.RetrievesListOfCDs)}.\n\n";
            message += $"<b>{Commands.GetCdInfoFromMarket}</b> - {LanguageHelper.Get(Messages.RetrievesNameMarketplace)}.\n\n";
            message += $"<b>{Commands.Help}</b> - {LanguageHelper.Get(Messages.OpensThisScreen)}\n\n";

            message += $"<b>{Commands.Export}</b> - {LanguageHelper.Get(Messages.Export)}\n\n";
            message += $"\t<b>{Commands.Export} {ExportSuffix.Inventory}</b> - {LanguageHelper.Get(Messages.ExportInv)}\n";
            message += $"\t<b>{Commands.Export} {ExportSuffix.Room}</b> - {LanguageHelper.Get(Messages.ExportRoom)}\n";
            message += $"\t<b>{Commands.Export} {ExportSuffix.Jukebox}</b> - {LanguageHelper.Get(Messages.ExportJuke)}\n";
            message += $"\t<b>{Commands.Export}</b> / <b>{Commands.Export} {ExportSuffix.Jukebox}</b> - {LanguageHelper.Get(Messages.ExportAll)}\n";
                        
            message += $"<b>{LanguageHelper.Get(Messages.ExampleUsage)}:</b>\n\n";
            message += $"{LanguageHelper.Get(Messages.ToUseCommands)}:\n\n";
            message += $"<i><b>{Commands.PreFix} {LanguageHelper.Get(Messages.Command)}</b></i>\n";

            _extension.Send(In.NotificationDialog, "", 3,
                "title", title,
                "message", message,
                "image", badge
            );
        }


        public void SendNoCdsFoundNotification()
        {
            // Create the notification message
            // Cria a mensagem de notificação
            string notification = $"{LanguageHelper.Get(Messages.NoCDsFound)}!";
            string badge = NotificationBadges.Alert;

            SendToastNotification(notification, badge);
        }

        public void SendExportSuccessNotification(int count)
        {           
            string notification = LanguageHelper.Get(Messages.SuccessfullyExportedCDs, count);
            string badge = NotificationBadges.FileExportSuccess;

            SendToastNotification(notification, badge);
        }

        public void SendExportingNotification()
        {           
            string notification = $"{LanguageHelper.Get(Messages.ExportingToFile)}...";
            string badge = NotificationBadges.FileExportInProgress;

            SendToastNotification(notification, badge);
        }

        /// <summary>
        /// Sends a custom notification with a text and badge
        /// Envia uma notificacao com texto e emblema
        /// </summary>
        /// <param name="text">The message text to display in the notification | O texto da mensagem a ser exibida na notificacao.</param>
        /// <param name="badge">The badge identifier | O identificador do emblema</param>
        public void SendToastNotification(string text, string badge)
        {
            _extension.Send(In.NotificationDialog, "", 3,
                "display", "BUBBLE",
                "message", text,
                "image", badge
            );
        }

        public void SendModalNotification(string title, string text, string badge)
        {
            _extension.Send(In.NotificationDialog, "", 3,
                   "title", title,
                   "message", text,
                   "image", badge
               );
        }
               
    }
}

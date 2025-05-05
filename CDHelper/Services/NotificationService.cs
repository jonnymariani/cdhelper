using System.Text;
using CDHelper.Models.PacketData;
using CDHelper.Structs;
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
            string notification = $"{cdData.Title}\n\nOffer by {cdData.Author}";

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
                SendModalNotification($"{cds.Count()} CD's Found!", message, NotificationBadges.Jukebox);
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
            string title = "Info";
            string message = "\n";

            //message += "<b>CD Helper</b>\n";
            message += "<small>CD Helper is an extension to help you manage and discover CDs in Habbo more easily.</small>\n\n";

            message += "<b>Command List:</b>\n\n";

            message += $"<b>{Commands.GetJukeboxCds}</b> - Retrieves the list of CD's from the room's jukebox.\n\n";
            message += $"<b>{Commands.GetCdInfoFromMarket}</b> - Retrieves the name of the current CD from the marketplace.\n\n";
            message += $"<b>{Commands.Help}</b> - Uh... well... opens this very screen. What did you expect?\n\n";

            // Add section with generic usage example
            message += "<b>Example Usage:</b>\n\n";
            message += "To use the commands, type them in the chat as follows:\n\n";
            message += $"<i><b>{Commands.PreFix} command</b></i>\n";

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
            string notification = "No CD's found in the jukebox!";
            string badge = NotificationBadges.Alert;

            SendToastNotification(notification, badge);
        }

        public void SendExportSuccessNotification(int count)
        {           
            string notification = $"Successfully exported {count} CDs";
            string badge = NotificationBadges.FileExportSuccess;

            SendToastNotification(notification, badge);
        }

        public void SendExportingNotification()
        {           
            string notification = "Exporting to file...";
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

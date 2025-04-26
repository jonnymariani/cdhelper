using Xabbo.GEarth;
using Xabbo.Messages.Flash;

namespace CDHelper
{
    /// <summary>
    /// Handles sending notifications
    /// Gerencia o envio de notificacoes
    /// </summary>
    public class NotificationHandler(GEarthExtension extension)
    {
        private readonly GEarthExtension _extension = extension;

        /// <summary>
        /// Sends a custom notification with a text and badge
        /// Envia uma notificacao com texto e emblema
        /// </summary>
        /// <param name="text">The message text to display in the notification | O texto da mensagem a ser exibida na notificacao.</param>
        /// <param name="badge">The badge identifier | O identificador do emblema</param>
        public void SendNotification(string text, string badge)
        {
            _extension.Send(In.NotificationDialog, "", 3, "display", "BUBBLE", "message", text, "image", badge);
        }
    }
}

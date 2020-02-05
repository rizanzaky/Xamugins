using System.Threading.Tasks;

namespace Xamugin.Core.XamFcmPlugin
{
    public abstract class XamFcmActionDelegate
    {
        /// <summary>
        ///     Called when a notification is received while the App is in foreground.
        /// </summary>
        /// <param name="data">A <c>NotificationInfoModal</c> object with notification information</param>
        /// <returns>Return True if Basic Alert should be displayed, False if no alerts should be displayed</returns>
        public virtual Task<bool> OnNotificationReceivedAsync(NotificationInfoModal data)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        ///     Called when a user interacts with a notification alert.
        /// </summary>
        public virtual void OnNotificationAction()
        {
        }

        /// <summary>
        ///     Called if the FCM Token is renewed AND when <c>XamFcmPlugin.Initialize()</c> is invoked after the App is launched.
        /// </summary>
        /// <param name="token">The FCM Token that recognizes the devise to send FCM notifications</param>
        public virtual void OnFcmTokenUpdated(string token)
        {
        }
    }
}
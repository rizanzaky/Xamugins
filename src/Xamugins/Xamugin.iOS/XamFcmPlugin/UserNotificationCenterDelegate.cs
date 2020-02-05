using System;
using UserNotifications;
using Visit.Mobile.XamFcmPlugin;

namespace Visit.Mobile.iOS.XamFcmPlugin
{
    public class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        public override async void WillPresentNotification(
            UNUserNotificationCenter center,
            UNNotification notification,
            Action<UNNotificationPresentationOptions> completionHandler)
        {
            var notificationInfo = notification.Request.Content.UserInfo;
            var notificationInfoModal = new NotificationInfoModal();
            var isContinue = await Mobile.XamFcmPlugin.XamFcmPlugin.Delegate?.OnNotificationReceivedAsync(notificationInfoModal);
            completionHandler(isContinue ? UNNotificationPresentationOptions.Alert : UNNotificationPresentationOptions.None);
        }

        public override void DidReceiveNotificationResponse(
            UNUserNotificationCenter center,
            UNNotificationResponse response,
            Action completionHandler)
        {
            Mobile.XamFcmPlugin.XamFcmPlugin.Delegate?.OnNotificationAction();
        }
    }
}
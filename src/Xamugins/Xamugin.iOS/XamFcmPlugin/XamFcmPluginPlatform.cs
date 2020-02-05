using System;
using Firebase.CloudMessaging;
using Firebase.InstanceID;
using Foundation;
using UIKit;
using UserNotifications;
using Visit.Mobile.iOS.XamFcmPlugin;
using Visit.Mobile.XamFcmPlugin;
using Xamarin.Forms;

[assembly: Dependency(typeof(XamFcmPluginPlatform))]
namespace Visit.Mobile.iOS.XamFcmPlugin
{
    [Register(nameof(XamFcmPluginPlatform))]
    public class XamFcmPluginPlatform : IXamFcmPluginPlatform
    {
        private static NSObject m_tokenObserver;

        public void Configure()
        {
            Firebase.Core.App.Configure();
            UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();

            RequestNotificationAuthorization();
        }

        public void Initialize()
        {
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
            Mobile.XamFcmPlugin.XamFcmPlugin.FcmToken = Messaging.SharedInstance.FcmToken;
            m_tokenObserver = Messaging.Notifications.ObserveRegistrationTokenRefreshed(OnFcmTokenRefreshed);
        }

        public void Terminate()
        {
            m_tokenObserver?.Dispose();
            var deleteHandler = new InstanceIdDeleteHandler(error => { Console.WriteLine(error); });
            InstanceId.SharedInstance.DeleteId(deleteHandler);
            UIApplication.SharedApplication.UnregisterForRemoteNotifications();

            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            UNUserNotificationCenter.Current.RemoveAllPendingNotificationRequests();
            UNUserNotificationCenter.Current.RemoveAllDeliveredNotifications();
        }

        private static void RequestNotificationAuthorization()
        {
            const UNAuthorizationOptions AuthOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
            UNUserNotificationCenter.Current.RequestAuthorization(AuthOptions, (success, error) => { });
        }

        private static void OnFcmTokenRefreshed(object sender, NSNotificationEventArgs e)
        {
            Mobile.XamFcmPlugin.XamFcmPlugin.FcmToken = Messaging.SharedInstance.FcmToken;
        }
    }
}
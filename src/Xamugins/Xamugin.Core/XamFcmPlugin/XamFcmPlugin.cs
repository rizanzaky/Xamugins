using Xamarin.Forms;

namespace Xamugin.Core.XamFcmPlugin
{
    public static class XamFcmPlugin
    {
        private static IXamFcmPluginPlatform s_fcmPluginPlatform;

        private static string s_fcmToken;
        private static bool s_isInitCalled;

        /// <summary>
        ///     The FCM Token that recognizes the devise to send FCM notifications. This value can change.
        /// </summary>
        public static string FcmToken
        {
            get => s_fcmToken;
            set
            {
                if (!s_isInitCalled)
                {
                    s_fcmToken = null;
                    return;
                }

                if (string.Equals(s_fcmToken, value))
                {
                    return;
                }

                s_fcmToken = value;
                Delegate?.OnFcmTokenUpdated(value);
            }
        }

        /// <summary>
        ///     Delegate used to listen to notification events. Set an object extended from
        ///     <c>XamFcmPlugin.XamFcmActionDelegate</c>
        /// </summary>
        public static XamFcmActionDelegate Delegate { get; set; }

        private static IXamFcmPluginPlatform FcmPluginPlatform
        {
            get
            {
                s_fcmPluginPlatform = s_fcmPluginPlatform ?? DependencyService.Get<IXamFcmPluginPlatform>();
                return s_fcmPluginPlatform;
            }
        }

        /// <summary>
        ///     Does the basic setup to configure the device to receive FCM notifications. Should be invoked on App launch.
        /// </summary>
        public static void Configure()
        {
            FcmPluginPlatform.Configure();
        }

        /// <summary>
        ///     Opens the FCM channel to start receiving FCM notifications. If invoked ones, FCM notifications will keep receiving
        ///     until <c>XamFcmPlugin.Terminate()</c> is invoked.
        /// </summary>
        public static void Initialize()
        {
            s_isInitCalled = true;
            FcmPluginPlatform.Initialize();
        }

        /// <summary>
        ///     Closes the FCM channel to stop receiving FCM notifications.
        /// </summary>
        public static void Terminate()
        {
            s_isInitCalled = false;
            FcmPluginPlatform.Terminate();
        }
    }
}
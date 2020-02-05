namespace Visit.Mobile.XamFcmPlugin
{
    public interface IXamFcmPluginPlatform
    {
        void Configure();
        void Initialize();
        void Terminate();
    }
}
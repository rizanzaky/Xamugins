namespace Xamugin.Core.XamFcmPlugin
{
    public interface IXamFcmPluginPlatform
    {
        void Configure();
        void Initialize();
        void Terminate();
    }
}
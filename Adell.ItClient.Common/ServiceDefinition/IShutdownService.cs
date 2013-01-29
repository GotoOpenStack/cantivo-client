namespace Adell.ItClient.Common.ServiceDefinition
{
    public interface IShutdownService
    {
        void Shutdown();
        void Reboot();
    }
}

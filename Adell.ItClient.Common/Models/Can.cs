namespace Adell.ItClient.Common.Models
{
    public class Can
    {
        public Can()
        {
            
        }
        public Can(string hostname, string name)
        {
            HostName = hostname;
            Name = name;

        }
        public string HostName { get; set; }
        public string Name { get; set; }
    }
}

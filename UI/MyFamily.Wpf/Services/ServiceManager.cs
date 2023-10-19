namespace MyFamily.Wpf.Services
{
    internal static class ServiceManager
    {
        private readonly static InternetConnectionService _connectionService = new InternetConnectionService();

        public static InternetConnectionService InternetConnectionService => _connectionService;


        
    }
}

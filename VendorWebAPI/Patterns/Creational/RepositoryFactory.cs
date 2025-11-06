using VendorWebAPI.Interfaces;

namespace VendorWebAPI.Patterns.Creational
{
    public class RepositoryFactory
    {
        public static IVendorService CreateVendorService(IServiceCollection provider)
        {
            var repository = provider.BuildServiceProvider().GetRequiredService<IVendorService>();
            return repository;
        }
    }
}

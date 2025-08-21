using AdminControl.Core.Entities;

namespace AdminControl.Core.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        Task<Vendor?> GetVendorByIdAsync(Guid id);
        Task<Vendor?> AddVendorAsync(Vendor vendor);
        Task<Vendor?> UpdateVendorAsync(Vendor vendor);
        Task<bool> DeleteVendorAsync(Guid id);
        Task<IEnumerable<Vendor>> SearchVendorsAsync(string searchTerm);
    }
}

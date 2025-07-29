using VendorWebAPI.DTOs;
using VendorWebAPI.Model;

namespace VendorWebAPI.Interfaces
{
    public interface IVendorService
    {
        Task<bool> RegisterVendorAsync(Vendor vendor);
        Task<Model.Vendor?> GetVendorByIdAsync(Guid vendorId);
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        Task<bool> UpdateVendorAsync(Vendor vendor);
        Task<bool> DeleteVendorAsync(Guid vendorId);
        Task<bool> IsCompanyNameTakenAsync(string companyName);
        Task<bool> IsContactEmailTakenAsync(string contactEmail);   
    }
}

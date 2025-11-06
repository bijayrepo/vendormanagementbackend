using VendorWebAPI.Interfaces;
using VendorWebAPI.Model;

namespace VendorWebAPI.Patterns.Structural
{
    public class CachingDecorator :IVendorService
    {
        private readonly IVendorService _vendorService;
        private IEnumerable<Vendor>? _cache;
        public CachingDecorator(IVendorService vendorService)
        { 
            _vendorService = vendorService;
        }
        public async Task<bool> RegisterVendorAsync(Vendor vendor)
        {
            var result = await _vendorService.RegisterVendorAsync(vendor);
            _cache = null; // Invalidate cache
            return result;
        }
        public async Task<Vendor?> GetVendorByIdAsync(Guid vendorId)
        {
            return await _vendorService.GetVendorByIdAsync(vendorId);
        }
        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            if (_cache == null)
            {
                _cache = await _vendorService.GetAllVendorsAsync();
            }
            return _cache;
        }
        public async Task<bool> UpdateVendorAsync(Vendor vendor)
        {
            var result = await _vendorService.UpdateVendorAsync(vendor);
            _cache = null; // Invalidate cache
            return result;
        }
        public async Task<bool> DeleteVendorAsync(Guid vendorId)
        {
            var result = await _vendorService.DeleteVendorAsync(vendorId);
            _cache = null; // Invalidate cache
            return result;
        }
        public async Task<bool> IsCompanyNameTakenAsync(string companyName)
        {
            return await _vendorService.IsCompanyNameTakenAsync(companyName);
        }
        public async Task<bool> IsContactEmailTakenAsync(string contactEmail)
        {
            return await _vendorService.IsContactEmailTakenAsync(contactEmail);
        }
        public void ClearCache()
        {
            _cache = null;
        }
    }
}

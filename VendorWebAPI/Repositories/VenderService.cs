using Microsoft.EntityFrameworkCore;
using VendorWebAPI.Data;
using VendorWebAPI.Interfaces;
using VendorWebAPI.Model;

namespace VendorWebAPI.Repositories
{
    public class VenderServiceRepository:IVendorService
    {
        private readonly AppDbContext _context;
        public VenderServiceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterVendorAsync(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Vendor?> GetVendorByIdAsync(Guid vendorId)
        {
            await _context.Vendors.ToListAsync();
            return null;
        }
        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return null;
        }
        public async Task<bool> UpdateVendorAsync(Vendor vendor)
        {
            return true;
        }
        public async Task<bool> DeleteVendorAsync(Guid vendorId)
        {
            return true;
        }
        public async Task<bool> IsCompanyNameTakenAsync(string companyName)
        {
            return false;
        }
        public async Task<bool> IsContactEmailTakenAsync(string contactEmail)
        {
            return false;
        }
    }
}

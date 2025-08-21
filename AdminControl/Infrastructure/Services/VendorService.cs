using AdminControl.Core.Entities;
using AdminControl.Core.Interfaces;
using AdminControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AdminControl.Infrastructure.Services
{
    public class VendorService: IVendorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IVendorService _IVendorService;
        public VendorService(ApplicationDbContext context,IVendorService vendorService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _IVendorService = vendorService ?? throw new ArgumentNullException(nameof(vendorService));
        }
        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return await _context.Vendors.ToListAsync();
        }
        public async Task<Vendor?> GetVendorByIdAsync(Guid VendorId)
        {
            return await _context.Vendors.FindAsync(VendorId);
        }
        public async Task<Vendor?> AddVendorAsync(Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException(nameof(vendor));
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return vendor;
        }
        public async Task<Vendor?> UpdateVendorAsync(Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException(nameof(vendor));
            var existingVendor = await _context.Vendors.FindAsync(vendor.VendorID);
            if (existingVendor == null)
                return null;
            existingVendor.CompanyName = vendor.CompanyName;
            existingVendor.ContactEmail = vendor.ContactEmail;
            existingVendor.ContactPhone = vendor.ContactPhone;
            _context.Vendors.Update(existingVendor);
            await _context.SaveChangesAsync();
            return existingVendor;
        }
        public async Task<bool> DeleteVendorAsync(Guid id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
                return false;
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Vendor>> SearchVendorsAsync(string searchTerm)
        {
            return await _context.Vendors
                .Where(v => v.Name.Contains(searchTerm) || v.Description.Contains(searchTerm))
                .ToListAsync();
        }
    }
}

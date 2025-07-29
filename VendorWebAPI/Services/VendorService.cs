using Microsoft.EntityFrameworkCore;
using VendorWebAPI.Data;
using VendorWebAPI.DTOs;
using VendorWebAPI.Interfaces;
using VendorWebAPI.Model;

namespace VendorWebAPI.Services
{
    public class VendorService: IVendorService
    {
       private readonly AppDbContext _context;
        private readonly IUserService _userService;
        public VendorService(AppDbContext context,IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<bool> RegisterVendorAsync(Vendor vendor)
        {
            try
            {

                var user = await _userService.RegisterUserAsync(
                    new RegisterUserDto
                    {
                        UserID= Guid.NewGuid(),
                        FullName = vendor.ContactName,
                        Username = vendor.CompanyName,
                        Email = vendor.ContactEmail,
                        Password = $"VDR-{DateTime.UtcNow:yyyyMMddHHmmss}" // Assuming you have a password field in Vendor
                    });
                new VendorRegistrationDto
                {
                    VendorID = vendor.VendorID,
                    CompanyName = vendor.CompanyName,
                    ContactName = vendor.ContactName,
                    ContactEmail = vendor.ContactEmail,
                    ContactPhone = vendor.ContactPhone,
                    Address = vendor.Address,
                    City = vendor.City,
                    State = vendor.State,
                    Country = vendor.Country,
                    GSTNumber = vendor.GSTNumber,
                    PANNumber = vendor.PANNumber,
                    CreatedAt = DateTime.UtcNow
                };
                if (vendor == null) return false;
                await _context.Vendors.AddAsync(vendor);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering vendor: {ex.Message}");
                Console.WriteLine($"Error registering vendor: {ex.StackTrace}");
                // Log the exception (ex) here if needed
                return false;
            }
        }
        public async Task<Model.Vendor?> GetVendorByIdAsync(Guid vendorId)
        {
            return await _context.Vendors.FindAsync(vendorId);
        }
        public async Task<IEnumerable<Model.Vendor>> GetAllVendorsAsync()
        {
            return await _context.Vendors.ToListAsync();
        }
        public async Task<bool> UpdateVendorAsync(Model.Vendor vendor)
        {
            if (vendor == null) return false;
            _context.Vendors.Update(vendor);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteVendorAsync(Guid vendorId)
        {
            var vendor = await GetVendorByIdAsync(vendorId);
            if (vendor == null) return false;
            _context.Vendors.Remove(vendor);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> IsCompanyNameTakenAsync(string companyName)
        {
            return await _context.Vendors.AnyAsync(v => v.CompanyName == companyName);
        }
        public async Task<bool> IsContactEmailTakenAsync(string contactEmail)
        {
            return await _context.Vendors.AnyAsync(v => v.ContactEmail == contactEmail);
        }

        public Task<bool> RegisterVendorAsync(VendorRegistrationDto vendor)
        {
            throw new NotImplementedException();
        }
    }
}

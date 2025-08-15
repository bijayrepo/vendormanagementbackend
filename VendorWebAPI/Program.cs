using Microsoft.AspNetCore.Authentication.Negotiate;
using VendorWebAPI.Data;
using VendorWebAPI.Interfaces;
using VendorWebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace VendorWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAngular",
            //        policy =>
            //        {
            //            policy.WithOrigins("http://localhost:4200")
            //                  .AllowAnyHeader()
            //                  .AllowAnyMethod();
            //        });
            //});
            var allowedOrigins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowConfiguredOrigins", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();
            // Register the UserService as a scoped service
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IVendorService, VendorService>();
            //builder.Services.AddScoped<IUserService, UserService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            //builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            //    .AddNegotiate();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddDbContext<AppDbContext>(opt =>opt.UseInMemoryDatabase("UserDb"));

          
            //builder.Services.AddAuthorization(options =>
            //{
            //    // By default, all incoming requests will be authorized according to the default policy.
            //    options.FallbackPolicy = options.DefaultPolicy;
            //});
            //builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

            var app = builder.Build();
            app.UseCors("AllowConfiguredOrigins");

            //Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();
            // Log all endpoints on startup
            var endpointDataSource = app.Services.GetRequiredService<Microsoft.AspNetCore.Routing.EndpointDataSource>();
            foreach (var endpoint in endpointDataSource.Endpoints)
            {
                Console.WriteLine($"[ENDPOINT] {endpoint.DisplayName}");
            }


            app.Run();
        }
    }
}

using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using VendorWebAPI.Data;
using VendorWebAPI.Interfaces;
using VendorWebAPI.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            builder.Services.AddMemoryCache();
            // Register the UserService as a scoped service
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IVendorService, VendorService>();
            builder.Services.AddScoped<IAuthService, AuthService>(provider =>
            {
                var context = provider.GetRequiredService<AppDbContext>();
                //var ServiceContext = provider.GetRequiredService<ServicesDbContext>();
                var jwtSecret = builder.Configuration.GetValue<string>("JwtSettings:SecretKey") ?? "Demo";
                return new AuthService(context, jwtSecret);
            });
            builder.Services.AddTransient<IUserAuthService, AuthService>(provider =>
            {
                var context = provider.GetRequiredService<AppDbContext>();
                var jwtSecret = builder.Configuration.GetValue<string>("JwtSettings:SecretKey") ?? "Demo";
                return new AuthService(context, jwtSecret);
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();

            //dotnet tool install --global dotnet-ef
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("VendorDBConnection")));
            //dotnet ef migrations add InitialCreate --Context AppDBContext --OutputDir Migrations\VendorDBConnection
            builder.Services.AddDbContext<ServicesDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ServicesDBConnection")));
            // dotnet ef migrations add InitialCreate --context ServicesDbContext --output-dir Migrations\ServicesDBConnection
            //dotnet ef database update --context ServicesDbContext
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
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            //app.UseAuthorization();
            //



            // Log each incoming request URL
            app.Use(async (context, next) =>
            {
                var request = context.Request;
                var url = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
                Console.WriteLine($"[REQUEST] {request.Method} {url}");
                await next();
            });



            ///


            //app.UseLoggingMiddleware();
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

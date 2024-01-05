using System.Data.SqlClient;
using Web.Entities;
using Web.Repositories;

namespace Web;

public class Program
{
    public static async Task Main(string[] args)
    {


        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddScoped<SqlConnector>();
        builder.Services.AddScoped<ProducerRepository>();
        builder.Services.AddScoped<FurnitureRepository>();
        builder.Services.AddScoped<SellingPlatformRepository>();
        builder.Services.AddScoped<ShippingRepository>();



        // Add services to the container.
        builder.Services.AddRazorPages();
            
        var app = builder.Build();


        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var shippingRepository = serviceProvider.GetRequiredService<ShippingRepository>();

            // Execute GetAll function
            var shippings = await shippingRepository.GetAll();

            // Print contents of the returned list
            foreach (var s in shippings)
            {
                Console.WriteLine($"{s.ToString()}\n");
            }
        }




        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }



}


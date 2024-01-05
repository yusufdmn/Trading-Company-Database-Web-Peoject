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



        // Add services to the container.
        builder.Services.AddRazorPages();
            
        var app = builder.Build();


        //using (var scope = app.Services.CreateScope())
        //{
        //    var serviceProvider = scope.ServiceProvider;
        //    var furnitureRepository = serviceProvider.GetRequiredService<FurnitureRepository>();

        //    // Execute GetAll function
        //    var furnitureList = await furnitureRepository.GetAll();

        //    // Print contents of the returned list
        //    foreach (var furniture in furnitureList)
        //    {
        //        Console.WriteLine($"SKU: {furniture.SKU}, Name: {furniture.Name}, Type: {furniture.FurnitureType}, Material: {furniture.TreeMaterial}, Price: {furniture.BasePrice}");
        //    }
        //}




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


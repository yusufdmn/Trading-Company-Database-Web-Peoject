using System.Data.SqlClient;
using Web.Entities;
using Web.Handlers;
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

        builder.Services.AddScoped<CustomerRepository>();
        builder.Services.AddScoped<SellingPlatformRepository>();
        builder.Services.AddScoped<ShippingRepository>();
        builder.Services.AddScoped<OrderRepository>();
        builder.Services.AddScoped<OrderlineRepository>();
        

        builder.Services.AddScoped<OrderCreationHandler>();


        // Add services to the container.
        builder.Services.AddRazorPages();
            
        var app = builder.Build();



        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;

            var handler = serviceProvider.GetRequiredService<OrderCreationHandler>();

            // Execute GetAll function

            int customerId = 10;
            string platformUrl = "https://www.tinellawood.com/";
            
            List<Orderline> orderlines = new List<Orderline>();
            Orderline orderline = new Orderline();
            orderline.SKU = 56;
            orderline.Price = 100;
            Orderline orderline2 = new Orderline();
            orderline2.SKU = 59;
            orderline2.Price = 200;


            orderlines.Add(orderline);
            orderlines.Add(orderline2);
            await handler.Handle(orderlines,customerId,platformUrl);

            
        }


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

        //using (var scope = app.Services.CreateScope())
        //{
        //    var serviceProvider = scope.ServiceProvider;
        //    var orderlineRepository = serviceProvider.GetRequiredService<OrderlineRepository>();

        //    // Execute GetAll function
        //    await orderlineRepository.Insert(
        //        25, 1, 30,1
        //    );

        //    // Print contents of the returned list

        //}

        //using (var scope = app.Services.CreateScope())
        //{
        //    var serviceProvider = scope.ServiceProvider;
        //    var orderRepository = serviceProvider.GetRequiredService<OrderRepository>();

        //    // Execute GetAll function
        //    var orderID = await orderRepository.Insert(
        //        "asdas.htlm", 1, "https://www.facebook.com/Tinellawoods/"
        //    );

        //    // Print contents of the returned list
        //    Console.WriteLine($"OrderID:{orderID}");
        //}


        //using (var scope = app.Services.CreateScope())
        //{
        //    var serviceProvider = scope.ServiceProvider;
        //    var furnitureRepository = serviceProvider.GetRequiredService<FurnitureRepository>();

        //    // Execute GetAll function
        //    var sku = await furnitureRepository.Insert(
        //        "yeniname",180,"tahta",'F',"Mavi"
        //        );

        //    // Print contents of the returned list
        //        Console.WriteLine($"SKU:{sku}");
        //}
        //using (var scope = app.Services.CreateScope())
        //{
        //    var serviceProvider = scope.ServiceProvider;
        //    var customerRepository = serviceProvider.GetRequiredService<CustomerRepository>();

        //    // Execute GetAll function
        //    var customerList = await customerRepository.GetAll();

        //    // Print contents of the returned list


        //    foreach (var each in customerList)
        //    {
        //        Console.WriteLine(each.ID +each.Name+each.Country+each.State+each.PhoneNumber+each.Email);
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


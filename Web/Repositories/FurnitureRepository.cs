using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Security.Cryptography;
using Web.Entities;

namespace Web.Repositories;

public class FurnitureRepository
{
    private readonly SqlConnector _connector;

    public FurnitureRepository(SqlConnector connector)
    {
        _connector = connector;
    }

    public async Task<List<Furniture>> GetAll(string queryString = "Select * from Furniture")
    {
        List<Furniture> furnitures= new List<Furniture>();
        SqlConnection conn = _connector.SqlConnection;
        using (SqlCommand command = new SqlCommand(queryString, conn))
        {
            try
            {

                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Furniture furniture = new Furniture();
                    furniture.SKU = int.Parse(reader["SKU"].ToString());
                    furniture.FurnitureType = reader["FurnitureType"].ToString().ElementAt(0);
                    furniture.TreeMaterial = reader["TreeMaterial"].ToString();
                    furniture.Name = reader["Name"].ToString();

                    furniture.BasePrice = reader["BasePrice"] is Decimal price ? price : 0;
                    
                    

                    
                    
                    furnitures.Add(furniture);
                }

                await reader.CloseAsync();
                await conn.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        

        return furnitures;
    }

    public async Task<IList<Furniture>> GetNotOrderedFurnitures(string queryString = "Select * from ViewFurnitureStock")
    {
        List<Furniture> furnitures = new List<Furniture>();
        SqlConnection conn = _connector.SqlConnection;
        using (SqlCommand command = new SqlCommand(queryString, conn))
        {
            try
            {

                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Furniture furniture = new Furniture();
                    furniture.SKU = int.Parse(reader["FurnitureSKU"].ToString());
                    furniture.FurnitureType = reader["FurnitureType"].ToString().ElementAt(0);
                    furniture.TreeMaterial = reader["TreeMaterial"].ToString();
                    furniture.Name = reader["Name"].ToString();

                    furniture.BasePrice = reader["BasePrice"] is Decimal price ? price : 0;





                    furnitures.Add(furniture);
                }

                await reader.CloseAsync();
                await conn.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        return furnitures;
    }

    public async Task<int?> Insert(
        string name,
        decimal? basePrice,
        string? treeMaterial,
        char furnitureType,
        string color
        )
    {
        int sku = -1;
        try
        {
            using (SqlConnection conn = _connector.SqlConnection)
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                
               
                string str = "('"+name+"',"+basePrice+",'"+treeMaterial+"','"+furnitureType+"')";
                
                
                command.CommandText = @"
                INSERT INTO Furniture (Name, BasePrice, TreeMaterial, FurnitureType) OUTPUT INSERTED.SKU
                VALUES " + str;
                
                sku = (int) await command.ExecuteScalarAsync() ;

                str = "("+sku+",'"+color+"')";
                command.CommandText = @"
                INSERT INTO Furniture_Color (SKU,Color)
                VALUES " + str;
                await command.ExecuteNonQueryAsync();

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Consider logging or handling the exception appropriately.
        }

        return sku;
    }

    public async Task<int?> Delete(int sku)
    {
        try
        {
            using (SqlConnection conn = _connector.SqlConnection)
            {
                await conn.OpenAsync();
                SqlCommand command = conn.CreateCommand();

                // Log the SKU being deleted
                Console.WriteLine("Deleting furniture with SKU: " + sku);

                // Delete from Furniture_Color
                command.CommandText = @"
                DELETE FROM Furniture_Color WHERE SKU = " + sku;
                await command.ExecuteNonQueryAsync();

                // Delete from Furniture
                command.CommandText = @"
                DELETE FROM Furniture WHERE SKU = " + sku;
                int rowsAffected = await command.ExecuteNonQueryAsync();

                // Log the number of rows affected
                Console.WriteLine("Rows affected: " + rowsAffected);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Consider logging or handling the exception appropriately.
        }

        return sku;
    }


    public async Task<int?> Update(
        int sku,
        string name,
        decimal? basePrice,
        string? treeMaterial,
        char furnitureType,
        string color
    )
    {
        try
        {
            using (SqlConnection conn = _connector.SqlConnection)
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                // Use parameterized queries to prevent SQL injection
                command.CommandText = @"
                UPDATE Furniture 
                SET BasePrice = @BasePrice, TreeMaterial = @TreeMaterial, Name = @Name 
                WHERE SKU = @SKU";

                // Add parameters
                command.Parameters.AddWithValue("@BasePrice", basePrice ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TreeMaterial", treeMaterial ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@SKU", sku);

                // Execute the query
                await command.ExecuteNonQueryAsync();

                // Update Furniture_Color table
                command.CommandText = @"
                UPDATE Furniture_Color 
                SET Color = @Color 
                WHERE SKU = @SKU";

                // Add parameter for Color
                command.Parameters.AddWithValue("@Color", color);

                // Execute the query
                await command.ExecuteNonQueryAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Consider logging or handling the exception appropriately.
        }

        return sku;
    }



}
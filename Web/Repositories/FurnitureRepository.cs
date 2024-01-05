using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
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


}

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
                    furniture.BasePrice= decimal.Parse(reader["BasePrice"].ToString());
                    
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
    



}

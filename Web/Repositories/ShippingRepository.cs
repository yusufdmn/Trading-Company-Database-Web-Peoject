using System.Data.SqlClient;
using Web.Entities;

namespace Web.Repositories;

public class ShippingRepository
{
    private readonly SqlConnector _sqlConnector;

    public ShippingRepository(SqlConnector sqlConnector)
    {
        _sqlConnector = sqlConnector;
    }


    public async Task<List<Shipping>> GetAll(string queryString = "SELECT * FROM Shipping s JOIN ShippingDetails sd ON sd.ShippingID = s.ShippingID")
    {
        List<Shipping> shippings = new List<Shipping>();
        SqlConnection conn = _sqlConnector.SqlConnection;
        using (SqlCommand command = new SqlCommand(queryString, conn))
        {
            try
            {

                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Shipping shipping = new()
                    {
                        ShippingID = (int)reader["ShippingID"],
                        Type = reader["Type"].ToString(),
                        Cost = (decimal)reader["Cost"],
                        Details = new ShippingDetails()
                        {
                            ShippingID = (int)reader["ShippingID"],
                            City = reader["City"].ToString(),
                            Country = reader["Country"].ToString(),
                            Company = reader["Company"].ToString(),
                            DeliveryDate = reader["DeliveryDate"] is DateTime deliveryDate ? deliveryDate : (DateTime?)null,
                            SDetailID = (int)reader["SDetaiIID"],
                            ShippingDate = (DateTime)reader["ShippingDate"],
                            State = reader["DeliveryDate"] as string ?? null,
                            ZipCode = reader["ZipCode"].ToString()
                        }
                    };
                    shippings.Add(shipping);
                }

                await reader.CloseAsync();
                await conn.CloseAsync();
            }
                catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return shippings;
    }

    
}
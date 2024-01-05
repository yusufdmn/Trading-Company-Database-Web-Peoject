using System.Data.SqlClient;

namespace Web.Repositories;

public class OrderlineRepository
{
    private readonly SqlConnector _sqlConnector;

    public OrderlineRepository(SqlConnector sqlConnector)
    {
        _sqlConnector = sqlConnector;
    }



    public async Task Insert(
        decimal price,
        int sku,
        int orderID,
        int? shippingID = null)
        {

        int orderlineID = -1;
        try
        {
            using (SqlConnection conn = _sqlConnector.SqlConnection)
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();



                string str = "('" + price + "'," + sku + ",'" + orderID + "','" + shippingID+ "')";


                command.CommandText = @"
                INSERT INTO Orderline (Price, SKU, OrderID, ShippingID) 
                VALUES "
                                      + str;

                orderlineID = (int) await command.ExecuteScalarAsync();
                

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Consider logging or handling the exception appropriately.
            
        }
    }
}
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
        int? orderID,
        int? shippingID = null)
        {

        int orderlineID = -1;
        try
        {
            using (SqlConnection conn = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=TinellaWoodDb; Trusted_Connection=True;"))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                if(shippingID != null)
                {
                    string str = "('" + price + "'," + sku + ",'" + orderID + "','" + shippingID + "')";


                    command.CommandText = @"
                INSERT INTO Orderline (Price, SKU, OrderID, ShippingID) 
                VALUES "
                                          + str;
                }
                else
                {
                    string str = "('" + price + "'," + sku + "," + orderID  +")";


                    command.CommandText = @"
                INSERT INTO Orderline (Price, SKU, OrderID) 
                VALUES "
                                          + str;
                }


                

               await command.ExecuteScalarAsync();
                

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Consider logging or handling the exception appropriately.
        }

    }
}

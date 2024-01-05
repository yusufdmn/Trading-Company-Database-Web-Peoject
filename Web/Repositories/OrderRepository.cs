using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Xml.Linq;

namespace Web.Repositories;

public class OrderRepository
{
    private readonly SqlConnector _sqlConnector;

    public OrderRepository(SqlConnector sqlConnector)
    {
        _sqlConnector = sqlConnector;
    }


    public async Task<int?> Insert(
        string? billURL,
        int customerID,
        string platformURL)
    {
        int orderID = -1;
        try
        {
            using (SqlConnection conn = _sqlConnector.SqlConnection)
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();



                string str = "('" + billURL + "'," + 0 + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + customerID + "','"+ platformURL + "')";


                command.CommandText = @"
                INSERT INTO Orders (BillURL, TotalPrice, OrderDate, CustomerID, PlatformURL) OUTPUT INSERTED.OrderID
                VALUES "
                + str;

                orderID = (int)await command.ExecuteScalarAsync();
                return orderID;

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Consider logging or handling the exception appropriately.
            return orderID;
        }
    }
}
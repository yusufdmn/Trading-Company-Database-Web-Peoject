using System.Data.SqlClient;
using Web.Entities;

namespace Web.Repositories;

public class CustomerRepository
{

    private readonly SqlConnector _connector;

    public CustomerRepository(SqlConnector connector)
    {
        _connector = connector;
    }


    public async Task<List<Customer>> GetAll(string queryString = "SELECT * FROM Customer")
    {

        List<Customer> customers = new List<Customer>();
        SqlConnection conn = _connector.SqlConnection;
        using (SqlCommand command = new SqlCommand(queryString, conn))
        {
            try
            {

                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Customer customer = new Customer();
                    customer.ID = int.Parse(reader["ID"].ToString());
                    customer.Name = reader["Name"].ToString();
                    customer.Country = reader["Country"].ToString();
                    customer.State= reader["State"].ToString();
                    customer.PhoneNumber= reader["PhoneNumber"].ToString();
                    customer.Email= reader["Email"].ToString();

                    customers.Add(customer);
                }

                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return customers;

    }



}

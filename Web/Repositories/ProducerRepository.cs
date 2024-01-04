using System.Collections;
using System.Data.SqlClient;
using Web.Entities;

namespace Web.Repositories;

public class ProducerRepository
{
    private readonly SqlConnector _connector;

    public ProducerRepository(SqlConnector connector)
    {
        _connector = connector;
    }

    public async Task<List<Producer>> GetAll(string queryString = "SELECT * FROM Producer")
    {

        List<Producer> producers = new List<Producer>();
        SqlConnection conn = _connector.SqlConnection;
        using (SqlCommand command = new SqlCommand(queryString, conn))
        {
            try
            {
                
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Producer producer = new Producer();
                    producer.ID = int.Parse(reader["ID"].ToString());
                    producer.Name = reader["Name"].ToString();
                    producer.Email = reader["Email"].ToString();
                    producer.PhoneNumber = reader["PhoneNumber"].ToString();
                    producer.Location = reader["Location"].ToString();
                    producers.Add(producer);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return producers;

    }




}

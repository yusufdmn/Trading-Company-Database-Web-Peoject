using Microsoft.AspNetCore.Http;
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
                
                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Producer producer = new Producer();
                    producer.ID = int.Parse(reader["ID"].ToString());
                    producer.Name = reader["Name"].ToString();
                    producer.Email = reader["Email"].ToString();
                    producer.PhoneNumber = reader["PhoneNumber"].ToString();
                    producer.Location = reader["Location"].ToString();
                    producers.Add(producer);
                }

                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return producers;

    }

    public async Task<int?> Insert(string? name, string? location, string? phoneNumber, string? email)
    {
        int outp = 0;
        try
        {
            using (SqlConnection conn = _connector.SqlConnection)
            {
                await conn.OpenAsync();
                SqlCommand command = conn.CreateCommand();

                string str = "('"+name+"','"+location+"','"+phoneNumber+"','"+email+"')";
                command.CommandText = @"
                INSERT INTO Producer (Name, Location, PhoneNumber, Email)
                VALUES "+str;

                outp = await command.ExecuteNonQueryAsync();
                await conn.OpenAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Consider logging or handling the exception appropriately.
        }

        return outp;
    }



}

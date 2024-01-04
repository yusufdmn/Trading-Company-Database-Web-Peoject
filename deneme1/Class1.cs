using System;
using System.Data;
using System.Data.SqlClient;

namespace deneme1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Your connection string to the MSSQL database
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TinellaWoodDb; Trusted_Connection=True;";
            //Server=(localdb)\\MSSQLLocalDB;Database=OnlineBookstoreDb; Trusted_Connection=True;

            // SQL query string
            string queryString = "SELECT * FROM Producer";

            // Create a connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a command to execute the query
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the query and get the results
                        SqlDataReader reader = command.ExecuteReader();

                        // Process the results (for example, print them)
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["ColumnName1"].ToString() + " " + reader["ColumnName2"].ToString());
                        }

                        // Close the reader
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}

using System.Data.SqlClient;
using Web.Entities;

namespace Web.Repositories
{
    public class SellingPlatformRepository
    {
        private readonly SqlConnector _connector;

        public SellingPlatformRepository(SqlConnector connector)
        {
            _connector = connector;
        }


        public async Task<List<SellingPlatform>> GetAll(string queryString = "SELECT * FROM SellingPlatform")
        {
            List<SellingPlatform> sellingPlatforms = new List<SellingPlatform>();
            SqlConnection conn = _connector.SqlConnection;
            using (SqlCommand command = new SqlCommand(queryString, conn))
            {
                try
                {

                    await conn.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        SellingPlatform sellingPlatform = new()
                        {
                            CommissionFee = reader["CommissionFee"].ToString(),
                            PlatformName = reader["PlatformName"].ToString(),
                            URL = reader["URL"].ToString()
                        };
                        sellingPlatforms.Add(sellingPlatform);
                    }

                    await reader.CloseAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return sellingPlatforms;
        }
    }
}

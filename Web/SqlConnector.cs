
using System.Data.SqlClient;

namespace Web;

public class SqlConnector
{

    private String ConnectionString { get; } = "Server=(localdb)\\MSSQLLocalDB;Database=TinellaWoodDb; Trusted_Connection=True;";

    public SqlConnection SqlConnection { get; set; }

    public SqlConnector()
    {
        SqlConnection = new SqlConnection(connectionString: ConnectionString);
    }



}

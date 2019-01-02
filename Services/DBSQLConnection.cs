using System.Data.SqlClient;

namespace newvat.Services
{
    public class DBSQLConnection
    {
        public SqlConnection GetConnection()
        {
            string ConnectionString = "data source=DEV6;initial catalog=PCCL_DB;user id=sa;password=S123456_;Integrated Security=False;connect Timeout=600;";
            SqlConnection conn = new SqlConnection(ConnectionString);
            return conn;
        }
    }
}
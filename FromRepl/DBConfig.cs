using System.Data.SqlClient;
using System.Configuration;

namespace RepDLL
{
    public class _DBConfig
    {
        private string DBName;
        public _DBConfig(string DBName)
        {
            this.DBName = DBName;
        }
        public _DBConfig()
        {
            this.DBName = "DefaultConnection";
        }
        public SqlConnection GetDBConnection()
        {
            //<add name="DBName" connectionString="Data Source=sivirt06;Initial Catalog=serche;Persist Security Info=True;User ID=src;Password=1234"/>
            var connectionString = ConfigurationManager.ConnectionStrings[DBName].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}

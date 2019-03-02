using MySql.Data.MySqlClient;

namespace pharmaAPI
{
    class dbutils
    {
        public static MySqlConnection OpenDB()
        {
            try
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection();
                    string myConnectionString;

                    myConnectionString = "server=13.238.149.159;uid=pharmacare;" +
                        "pwd=pharmacare;database=pharmacare";

                    conn.ConnectionString = myConnectionString;
                    conn.Open();
                    return conn;


                }
                catch (System.Exception)
                {

                    MySqlConnection conn = new MySqlConnection();
                    string myConnectionString;

                    myConnectionString = "SERVER=172.17.124.118;DATABASE=pharmacare;UID=elan;" +
                        "PASSWORD=elan759;";

                    conn.ConnectionString = myConnectionString;
                    conn.Open();
                    return conn;

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
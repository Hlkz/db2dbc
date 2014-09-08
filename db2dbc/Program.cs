using System;

using MySql.Data.MySqlClient;

namespace DBtoDBC
{
    class Program
    {
        static void Main(string[] args)
        {
            DB2DBC.GlobalLocalization = 2;
            string connectionString, server, database, uid, password;
            server = "localhost"; database = "iwpw"; uid = "root"; password = "";
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database
                + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            DB2DBC.ExtractAll(connection);

            connection.Close();
            //Console.ReadKey();
        }
    }
}

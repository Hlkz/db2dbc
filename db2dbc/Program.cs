using System;

using MySql.Data.MySqlClient;

namespace DBtoDBC
{
    class Program
    {
        static void Main(string[] args)
        {
            string instruc = "all";
            DB2DBC.GlobalLocalization = 2; // Default localization

            if (args != null) {
                if (args.Length > 0) instruc = args[0]; // Arg 0 is Instruction
                if (args.Length > 1) DB2DBC.GlobalLocalization = Convert.ToUInt32(args[1]); } // Arg 1 is Localization

            string connectionString, server, database, uid, password;
            server = "localhost"; database = "iwpw"; uid = "root"; password = "";
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database
                + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            switch (instruc)
            {
                case "spells":
                    Console.WriteLine("Extracting spells dbc.");
                    DB2DBC.ExtractSpells(connection); break;
                case "all":
                    Console.WriteLine("Extracting all dbc.");
                    DB2DBC.ExtractAll(connection); break;
                default:
                    Console.WriteLine("Bad arg[0], extracting all dbc.");
                    DB2DBC.ExtractAll(connection); break;
            }
            Console.WriteLine("Done.");

            connection.Close();
            //Console.ReadKey();
        }
    }
}

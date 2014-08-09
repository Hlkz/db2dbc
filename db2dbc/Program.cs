using System;

using MySql.Data.MySqlClient;

namespace DBtoDBC
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString, server, database, uid, password;
            server = "localhost"; database = "iwpw"; uid = "root"; password = "";
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database
                + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            MySqlConnection connection = new MySqlConnection(connectionString);

            Spelldbc spelldbc = new Spelldbc();
            spelldbc.LoadDB(connection);
            spelldbc.SaveDBC("Spell.dbc");

            Talentdbc talentdbc = new Talentdbc();
            talentdbc.LoadDB(connection);
            talentdbc.SaveDBC("Talent.dbc");
        }
    }

    public struct DBCHeader
    {
        public UInt32 magic;
        public UInt32 record_count;
        public UInt32 field_count;
        public UInt32 record_size;
        public Int32 string_block_size;
    };
}

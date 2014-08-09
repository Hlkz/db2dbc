using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.InteropServices;

using MySql.Data.MySqlClient;

namespace DBtoDBC
{
    public class Talentdbc
    {
        public DBCHeader header;
        public TalentBody body;

        public bool LoadDB(MySqlConnection connection)
        {
            try
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM talentdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT TalentID, TalentTab, Row, Col, Rank1, Rank2, Rank3, Rank4, Rank5, DependsOn, DependsOnRank, "
                    +"needAddInSpellBook, requiredSpellID, allowForPetHigh, allowForPetLow FROM talentdbc ORDER BY TalentID";


                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                // Prepare body
                body.records = new TalentRecordMap[rowCount];

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 23;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(TalentRecord));
                // header.string_block_size;

                UInt32 i = 0;
                //if (!reader.HasRows) return false;
                while (reader.Read())
                {
                    body.records[i].record.TalentID             = reader.GetInt32(0);
                    body.records[i].record.TalentTab            = reader.GetInt32(1);
                    body.records[i].record.Row                  = reader.GetInt32(2);
                    body.records[i].record.Col                  = reader.GetInt32(3);
                    body.records[i].record.Rank1                = reader.GetInt32(4);
                    body.records[i].record.Rank2                = reader.GetInt32(5);
                    body.records[i].record.Rank3                = reader.GetInt32(6);
                    body.records[i].record.Rank4                = reader.GetInt32(7);
                    body.records[i].record.Rank5                = reader.GetInt32(8);
                    body.records[i].record.Rank6                = 0;
                    body.records[i].record.Rank7                = 0;
                    body.records[i].record.Rank8                = 0;
                    body.records[i].record.Rank9                = 0;
                    body.records[i].record.DependsOn            = reader.GetInt32(9);
                    body.records[i].record.DependsOnRank        = reader.GetInt32(10);
                    body.records[i].record.unk0                 = 0;
                    body.records[i].record.unk1                 = 0;
                    body.records[i].record.unk2                 = 0;
                    body.records[i].record.unk3                 = 0;
                    body.records[i].record.needAddInSpellBook   = reader.GetInt32(11);
                    body.records[i].record.requiredSpellID      = reader.GetInt32(12);
                    body.records[i].record.allowForPetFlagsHigh = reader.GetInt32(13);
                    body.records[i].record.allowForPetFlagsLow  = reader.GetInt32(14);
                    i++;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public bool SaveDBC(string fileName)
        {
            try
            {
                Dictionary<int, UInt32> offsetStorage = new Dictionary<int, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                // Generate some string offsets...
                UInt32 stringBlockOffset = 1; // first character is always \0
                header.string_block_size = (int)stringBlockOffset;

                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);

                // Write header
                int count = Marshal.SizeOf(typeof(DBCHeader));
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();

                // Write records
                for (UInt32 i = 0; i < header.record_count; ++i)
                {
                    // Write main body
                    count = Marshal.SizeOf(typeof(TalentRecord));
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free();
                }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                // Write string block
                writer.Write(Encoding.UTF8.GetBytes("\0"));
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }
    }
}

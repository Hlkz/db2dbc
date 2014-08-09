using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.InteropServices;

using MySql.Data.MySqlClient;

namespace DBtoDBC
{
    public class Achievementdbc
    {
        public DBCHeader header;
        public AchievementBody body;

        public bool LoadDB(MySqlConnection connection)
        {
            try
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM achievementdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT ID, Faction, Map, Previous, Name, Name_loc2, Description, Description_loc2, "
                    +"Category, Points, OrderInGroup, Flags, SpellIcon, Reward, Reward_loc2, Demands, ReferencedAchievement "
                    +" FROM achievementdbc ORDER BY ID";


                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                // Prepare body
                body.records = new AchievementRecordMap[rowCount];

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 62;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(AchievementRecord));
                // header.string_block_size;

                UInt32 i = 0;
                //if (!reader.HasRows) return false;
                while (reader.Read())
                {
                    body.records[i].record.ID           = reader.GetInt32(0);
                    body.records[i].record.Faction      = reader.GetInt32(1);
                    body.records[i].record.Map          = reader.GetInt32(2);
                    body.records[i].record.Previous     = reader.GetInt32(3);
                    body.records[i].record.Category     = reader.GetInt32(8);
                    body.records[i].record.Points       = reader.GetInt32(9);
                    body.records[i].record.OrderInGroup = reader.GetInt32(10);
                    body.records[i].record.Flags        = reader.GetInt32(11);
                    body.records[i].record.SpellIcon    = reader.GetInt32(12);
                    body.records[i].record.Demands      = reader.GetInt32(15);
                    body.records[i].record.ReferencedAchievement = reader.GetInt32(16);

                    body.records[i].achName = new string[17];
                    body.records[i].achDesc = new string[17];
                    body.records[i].achRewd = new string[17];
                    body.records[i].record.Name         = new UInt32[17];
                    body.records[i].record.Description  = new UInt32[17];
                    body.records[i].record.Reward       = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].achName[loc] = "";
                        body.records[i].achDesc[loc] = "";
                        body.records[i].achRewd[loc] = ""; }
                    body.records[i].achName[2] = reader.GetString(5);
                    body.records[i].achDesc[2] = reader.GetString(7);
                    body.records[i].achRewd[2] = reader.GetString(14);
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

                for (UInt32 i = 0; i < header.record_count; ++i)
                {
                    for (UInt32 j = 0; j < 17; ++j)
                    {
                        // Name
                        if (body.records[i].achName[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else
                        {
                            int key = body.records[i].achName[j].GetHashCode();
                            if (offsetStorage.ContainsKey(key))
                                body.records[i].record.Name[j] = offsetStorage[key];
                            else
                            {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].achName[j]) + 1;
                                offsetStorage.Add(key, body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].achName[j]);
                            }
                        }
                        // Desc
                        if (body.records[i].achDesc[j].Length == 0)
                            body.records[i].record.Description[j] = 0;
                        else
                        {
                            int key = body.records[i].achDesc[j].GetHashCode();
                            if (offsetStorage.ContainsKey(key))
                                body.records[i].record.Description[j] = offsetStorage[key];
                            else
                            {
                                body.records[i].record.Description[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].achDesc[j]) + 1;
                                offsetStorage.Add(key, body.records[i].record.Description[j]);
                                reverseStorage.Add(body.records[i].record.Description[j], body.records[i].achDesc[j]);
                            }
                        }
                        // Reward
                        if (body.records[i].achRewd[j].Length == 0)
                            body.records[i].record.Reward[j] = 0;
                        else
                        {
                            int key = body.records[i].achRewd[j].GetHashCode();
                            if (offsetStorage.ContainsKey(key))
                                body.records[i].record.Reward[j] = offsetStorage[key];
                            else
                            {
                                body.records[i].record.Reward[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].achRewd[j]) + 1;
                                offsetStorage.Add(key, body.records[i].record.Reward[j]);
                                reverseStorage.Add(body.records[i].record.Reward[j], body.records[i].achRewd[j]);
                            }
                        }
                    }
                }

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
                    count = Marshal.SizeOf(typeof(AchievementRecord));
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

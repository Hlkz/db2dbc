using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace DBtoDBC {
    
    public class achievement_categorydbc {
        public DBCHeader header;
        public achievement_categoryBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM achievement_category", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, ParentId, Name, Name_loc2, SortOrder FROM achievement_category ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new achievement_categoryMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 20;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(achievement_categoryRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.ParentId = reader.GetInt32("ParentId");
                    body.records[i].record.SortOrder = reader.GetInt32("SortOrder");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(achievement_categoryRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // achievement_category

    public class animationdatadbc {
        public DBCHeader header;
        public animationdataBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM animationdata", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Weaponflags, Bodyflags, Flags, Fallback, BehaviourId, BehaviorTier FROM animationdata ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new animationdataMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(animationdataRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.Weaponflags = reader.GetInt32("Weaponflags");
                    body.records[i].record.Bodyflags = reader.GetInt32("Bodyflags");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.Fallback = reader.GetInt32("Fallback");
                    body.records[i].record.BehaviourId = reader.GetInt32("BehaviourId");
                    body.records[i].record.BehaviorTier = reader.GetInt32("BehaviorTier");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0

                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }

 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(animationdataRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // animationdata

    public class attackanimkitsdbc {
        public DBCHeader header;
        public attackanimkitsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM attackanimkits", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Animation, Type, Flags, Unknown FROM attackanimkits ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new attackanimkitsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(attackanimkitsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Animation = reader.GetInt32("Animation");
                    body.records[i].record.Type = reader.GetInt32("Type");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.Unknown = reader.GetInt32("Unknown");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(attackanimkitsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // attackanimkits

    public class attackanimtypesdbc {
        public DBCHeader header;
        public attackanimtypesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM attackanimtypes", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name FROM attackanimtypes ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new attackanimtypesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(attackanimtypesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(attackanimtypesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // attackanimtypes

    public class camerashakesdbc {
        public DBCHeader header;
        public camerashakesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM camerashakes", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, ShakeType, Direction, Amplitude, Frequency, Duration, Phase, Coefficient FROM camerashakes ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new camerashakesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(camerashakesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.ShakeType = reader.GetInt32("ShakeType");
                    body.records[i].record.Direction = reader.GetInt32("Direction");
                    body.records[i].record.Amplitude = reader.GetFloat("Amplitude");
                    body.records[i].record.Frequency = reader.GetFloat("Frequency");
                    body.records[i].record.Duration = reader.GetFloat("Duration");
                    body.records[i].record.Phase = reader.GetFloat("Phase");
                    body.records[i].record.Coefficient = reader.GetFloat("Coefficient");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(camerashakesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // camerashakes

    public class cfg_categoriesdbc {
        public DBCHeader header;
        public cfg_categoriesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM cfg_categories", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, LocaleMask, CharsetMask, Flags, Name, Name_loc2 FROM cfg_categories ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new cfg_categoriesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 21;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(cfg_categoriesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.LocaleMask = reader.GetInt32("LocaleMask");
                    body.records[i].record.CharsetMask = reader.GetInt32("CharsetMask");
                    body.records[i].record.Flags = reader.GetInt32("Flags");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(cfg_categoriesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // cfg_categories

    public class cfg_configsdbc {
        public DBCHeader header;
        public cfg_configsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM cfg_configs", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, RealmType, PlayerKillingAllowed, Roleplaying FROM cfg_configs ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new cfg_configsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(cfg_configsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.RealmType = reader.GetInt32("RealmType");
                    body.records[i].record.PlayerKillingAllowed = reader.GetInt32("PlayerKillingAllowed");
                    body.records[i].record.Roleplaying = reader.GetInt32("Roleplaying");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(cfg_configsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // cfg_configs

    public class characterfacialhairstylesdbc {
        public DBCHeader header;
        public characterfacialhairstylesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM characterfacialhairstyles", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Race, Gender, SpecificId, GeosetId1, GeosetId2, GeosetId3, GeosetId4, GeosetId5 FROM characterfacialhairstyles ORDER BY Race ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new characterfacialhairstylesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(characterfacialhairstylesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Race = reader.GetInt32("Race");
                    body.records[i].record.Gender = reader.GetInt32("Gender");
                    body.records[i].record.SpecificId = reader.GetInt32("SpecificId");
                    body.records[i].record.GeosetId1 = reader.GetInt32("GeosetId1");
                    body.records[i].record.GeosetId2 = reader.GetInt32("GeosetId2");
                    body.records[i].record.GeosetId3 = reader.GetInt32("GeosetId3");
                    body.records[i].record.GeosetId4 = reader.GetInt32("GeosetId4");
                    body.records[i].record.GeosetId5 = reader.GetInt32("GeosetId5");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(characterfacialhairstylesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // characterfacialhairstyles

    public class charbaseinfodbc {
        public DBCHeader header;
        public charbaseinfoBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM charbaseinfo", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Race, Class FROM charbaseinfo ORDER BY Race ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new charbaseinfoMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(charbaseinfoRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Race = reader.GetByte("Race");
                    body.records[i].record.Class = reader.GetByte("Class");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(charbaseinfoRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // charbaseinfo

    public class charhairgeosetsdbc {
        public DBCHeader header;
        public charhairgeosetsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM charhairgeosets", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Race, Gender, HairType, Geoset, Bald FROM charhairgeosets ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new charhairgeosetsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 6;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(charhairgeosetsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Race = reader.GetInt32("Race");
                    body.records[i].record.Gender = reader.GetInt32("Gender");
                    body.records[i].record.HairType = reader.GetInt32("HairType");
                    body.records[i].record.Geoset = reader.GetInt32("Geoset");
                    body.records[i].record.Bald = reader.GetInt32("Bald");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(charhairgeosetsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // charhairgeosets

    public class charhairtexturesdbc {
        public DBCHeader header;
        public charhairtexturesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM charhairtextures", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Race, Gender, Unk1, Unk2, Unk3, Unk4, Unk5 FROM charhairtextures ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new charhairtexturesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(charhairtexturesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Race = reader.GetInt32("Race");
                    body.records[i].record.Gender = reader.GetInt32("Gender");
                    body.records[i].record.Unk1 = reader.GetInt32("Unk1");
                    body.records[i].record.Unk2 = reader.GetInt32("Unk2");
                    body.records[i].record.Unk3 = reader.GetInt32("Unk3");
                    body.records[i].record.Unk4 = reader.GetInt32("Unk4");
                    body.records[i].record.Unk5 = reader.GetInt32("Unk5");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(charhairtexturesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // charhairtextures

    public class chatprofanitydbc {
        public DBCHeader header;
        public chatprofanityBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM chatprofanity", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, DirtyWord, LanguageId FROM chatprofanity ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new chatprofanityMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(chatprofanityRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].DirtyWord = reader.GetString("DirtyWord");
                    body.records[i].record.LanguageId = reader.GetInt32("LanguageId");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // DirtyWord
                    if (body.records[i].DirtyWord.Length == 0)
                        body.records[i].record.DirtyWord = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].DirtyWord)) body.records[i].record.DirtyWord = offsetStorage[body.records[i].DirtyWord];
                        else {
                            body.records[i].record.DirtyWord = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].DirtyWord) + 1;
                            offsetStorage.Add(body.records[i].DirtyWord, body.records[i].record.DirtyWord);
                            reverseStorage.Add(body.records[i].record.DirtyWord, body.records[i].DirtyWord); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(chatprofanityRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // chatprofanity

    public class cinematiccameradbc {
        public DBCHeader header;
        public cinematiccameraBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM cinematiccamera", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Filepath, Voiceover, X, Y, Z, Rotation FROM cinematiccamera ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new cinematiccameraMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 7;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(cinematiccameraRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Filepath = reader.GetString("Filepath");
                    body.records[i].record.Voiceover = reader.GetInt32("Voiceover");
                    body.records[i].record.X = reader.GetFloat("X");
                    body.records[i].record.Y = reader.GetFloat("Y");
                    body.records[i].record.Z = reader.GetFloat("Z");
                    body.records[i].record.Rotation = reader.GetFloat("Rotation");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Filepath
                    if (body.records[i].Filepath.Length == 0)
                        body.records[i].record.Filepath = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Filepath)) body.records[i].record.Filepath = offsetStorage[body.records[i].Filepath];
                        else {
                            body.records[i].record.Filepath = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Filepath) + 1;
                            offsetStorage.Add(body.records[i].Filepath, body.records[i].record.Filepath);
                            reverseStorage.Add(body.records[i].record.Filepath, body.records[i].Filepath); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(cinematiccameraRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // cinematiccamera

    public class creaturemovementinfodbc {
        public DBCHeader header;
        public creaturemovementinfoBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM creaturemovementinfo", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SmoothFacingChaseRate FROM creaturemovementinfo ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new creaturemovementinfoMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(creaturemovementinfoRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SmoothFacingChaseRate = reader.GetFloat("SmoothFacingChaseRate");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(creaturemovementinfoRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // creaturemovementinfo

    public class creaturesounddatadbc {
        public DBCHeader header;
        public creaturesounddataBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM creaturesounddata", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT ID, Exertion, ExertionCritical, Injury, InjuryCritical, InjuryCrushingBlow, Death, Stun, Stand, Footstep, Aggro, WingFlap, WingGlide, Alert, Fidget1, Fidget2, Fidget3, Fidget4, Fidget5, CustomAttack1, CustomAttack2, CustomAttack3, CustomAttack4, NPCSoundId, LoopSoundId, CreatureImpactType, JumpStart, JumpEnd, PetAttack, PetOrder, PetDismiss, FidgetDelaySecondsMin, FidgetDelaySecondsMax, Birth, SpellCastDirected, Submerge, Submerged, CreatureSoundDataIdPet FROM creaturesounddata ORDER BY ID ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new creaturesounddataMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 38;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(creaturesounddataRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.ID = reader.GetInt32("ID");
                    body.records[i].record.Exertion = reader.GetInt32("Exertion");
                    body.records[i].record.ExertionCritical = reader.GetInt32("ExertionCritical");
                    body.records[i].record.Injury = reader.GetInt32("Injury");
                    body.records[i].record.InjuryCritical = reader.GetInt32("InjuryCritical");
                    body.records[i].record.InjuryCrushingBlow = reader.GetInt32("InjuryCrushingBlow");
                    body.records[i].record.Death = reader.GetInt32("Death");
                    body.records[i].record.Stun = reader.GetInt32("Stun");
                    body.records[i].record.Stand = reader.GetInt32("Stand");
                    body.records[i].record.Footstep = reader.GetInt32("Footstep");
                    body.records[i].record.Aggro = reader.GetInt32("Aggro");
                    body.records[i].record.WingFlap = reader.GetInt32("WingFlap");
                    body.records[i].record.WingGlide = reader.GetInt32("WingGlide");
                    body.records[i].record.Alert = reader.GetInt32("Alert");
                    body.records[i].record.Fidget1 = reader.GetInt32("Fidget1");
                    body.records[i].record.Fidget2 = reader.GetInt32("Fidget2");
                    body.records[i].record.Fidget3 = reader.GetInt32("Fidget3");
                    body.records[i].record.Fidget4 = reader.GetInt32("Fidget4");
                    body.records[i].record.Fidget5 = reader.GetInt32("Fidget5");
                    body.records[i].record.CustomAttack1 = reader.GetInt32("CustomAttack1");
                    body.records[i].record.CustomAttack2 = reader.GetInt32("CustomAttack2");
                    body.records[i].record.CustomAttack3 = reader.GetInt32("CustomAttack3");
                    body.records[i].record.CustomAttack4 = reader.GetInt32("CustomAttack4");
                    body.records[i].record.NPCSoundId = reader.GetInt32("NPCSoundId");
                    body.records[i].record.LoopSoundId = reader.GetInt32("LoopSoundId");
                    body.records[i].record.CreatureImpactType = reader.GetInt32("CreatureImpactType");
                    body.records[i].record.JumpStart = reader.GetInt32("JumpStart");
                    body.records[i].record.JumpEnd = reader.GetInt32("JumpEnd");
                    body.records[i].record.PetAttack = reader.GetInt32("PetAttack");
                    body.records[i].record.PetOrder = reader.GetInt32("PetOrder");
                    body.records[i].record.PetDismiss = reader.GetInt32("PetDismiss");
                    body.records[i].record.FidgetDelaySecondsMin = reader.GetFloat("FidgetDelaySecondsMin");
                    body.records[i].record.FidgetDelaySecondsMax = reader.GetFloat("FidgetDelaySecondsMax");
                    body.records[i].record.Birth = reader.GetInt32("Birth");
                    body.records[i].record.SpellCastDirected = reader.GetInt32("SpellCastDirected");
                    body.records[i].record.Submerge = reader.GetInt32("Submerge");
                    body.records[i].record.Submerged = reader.GetInt32("Submerged");
                    body.records[i].record.CreatureSoundDataIdPet = reader.GetInt32("CreatureSoundDataIdPet");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(creaturesounddataRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // creaturesounddata

    public class currencycategorydbc {
        public DBCHeader header;
        public currencycategoryBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM currencycategory", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT ID, Flags, Name, Name_loc2 FROM currencycategory ORDER BY ID ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new currencycategoryMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 19;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(currencycategoryRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.ID = reader.GetInt32("ID");
                    body.records[i].record.Flags = reader.GetInt32("Flags");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(currencycategoryRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // currencycategory

    public class dancemovesdbc {
        public DBCHeader header;
        public dancemovesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM dancemoves", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Type, Value, Fallback, Racemask, Internal, Name, Name_loc2, LockId FROM dancemoves ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new dancemovesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 24;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(dancemovesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Type = reader.GetInt32("Type");
                    body.records[i].record.Value = reader.GetInt32("Value");
                    body.records[i].record.Fallback = reader.GetInt32("Fallback");
                    body.records[i].record.Racemask = reader.GetInt32("Racemask");
                    body.records[i].Internal = reader.GetString("Internal");
                    body.records[i].record.LockId = reader.GetInt32("LockId");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
                    // Internal
                    if (body.records[i].Internal.Length == 0)
                        body.records[i].record.Internal = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Internal)) body.records[i].record.Internal = offsetStorage[body.records[i].Internal];
                        else {
                            body.records[i].record.Internal = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Internal) + 1;
                            offsetStorage.Add(body.records[i].Internal, body.records[i].record.Internal);
                            reverseStorage.Add(body.records[i].record.Internal, body.records[i].Internal); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(dancemovesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // dancemoves

    public class deaththudlookupsdbc {
        public DBCHeader header;
        public deaththudlookupsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM deaththudlookups", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Size, TerrainTypeSound, SoundId, SoundIdWater FROM deaththudlookups ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new deaththudlookupsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(deaththudlookupsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Size = reader.GetInt32("Size");
                    body.records[i].record.TerrainTypeSound = reader.GetInt32("TerrainTypeSound");
                    body.records[i].record.SoundId = reader.GetInt32("SoundId");
                    body.records[i].record.SoundIdWater = reader.GetInt32("SoundIdWater");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(deaththudlookupsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // deaththudlookups

    public class declinedworddbc {
        public DBCHeader header;
        public declinedwordBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM declinedword", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Word FROM declinedword ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new declinedwordMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(declinedwordRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Word = reader.GetString("Word");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Word
                    if (body.records[i].Word.Length == 0)
                        body.records[i].record.Word = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Word)) body.records[i].record.Word = offsetStorage[body.records[i].Word];
                        else {
                            body.records[i].record.Word = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Word) + 1;
                            offsetStorage.Add(body.records[i].Word, body.records[i].record.Word);
                            reverseStorage.Add(body.records[i].record.Word, body.records[i].Word); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(declinedwordRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // declinedword

    public class declinedwordcasesdbc {
        public DBCHeader header;
        public declinedwordcasesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM declinedwordcases", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Word, `Case`, DeclinedWord FROM declinedwordcases ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new declinedwordcasesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(declinedwordcasesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Word = reader.GetInt32("Word");
                    body.records[i].record.Case = reader.GetInt32("Case");
                    body.records[i].DeclinedWord = reader.GetString("DeclinedWord");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // DeclinedWord
                    if (body.records[i].DeclinedWord.Length == 0)
                        body.records[i].record.DeclinedWord = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].DeclinedWord)) body.records[i].record.DeclinedWord = offsetStorage[body.records[i].DeclinedWord];
                        else {
                            body.records[i].record.DeclinedWord = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].DeclinedWord) + 1;
                            offsetStorage.Add(body.records[i].DeclinedWord, body.records[i].record.DeclinedWord);
                            reverseStorage.Add(body.records[i].record.DeclinedWord, body.records[i].DeclinedWord); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(declinedwordcasesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // declinedwordcases

    public class dungeonmapdbc {
        public DBCHeader header;
        public dungeonmapBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM dungeonmap", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Map, Layer, X, Y, Z, O, Area FROM dungeonmap ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new dungeonmapMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(dungeonmapRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Map = reader.GetInt32("Map");
                    body.records[i].record.Layer = reader.GetInt32("Layer");
                    body.records[i].record.X = reader.GetFloat("X");
                    body.records[i].record.Y = reader.GetFloat("Y");
                    body.records[i].record.Z = reader.GetFloat("Z");
                    body.records[i].record.O = reader.GetFloat("O");
                    body.records[i].record.Area = reader.GetInt32("Area");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(dungeonmapRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // dungeonmap

    public class dungeonmapchunkdbc {
        public DBCHeader header;
        public dungeonmapchunkBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM dungeonmapchunk", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Map, WMOId, DungeonMap, MinZ FROM dungeonmapchunk ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new dungeonmapchunkMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(dungeonmapchunkRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Map = reader.GetInt32("Map");
                    body.records[i].record.WMOId = reader.GetInt32("WMOId");
                    body.records[i].record.DungeonMap = reader.GetInt32("DungeonMap");
                    body.records[i].record.MinZ = reader.GetFloat("MinZ");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(dungeonmapchunkRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // dungeonmapchunk

    public class emotestextdatadbc {
        public DBCHeader header;
        public emotestextdataBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM emotestextdata", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Text, Text_loc2 FROM emotestextdata ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new emotestextdataMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(emotestextdataRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Text = new string[17];
                    body.records[i].record.Text = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Text[loc] = "";
                    body.records[i].Text[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Text_loc2" : "Text");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Text
                        if (body.records[i].Text[j].Length == 0)
                            body.records[i].record.Text[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Text[j])) body.records[i].record.Text[j] = offsetStorage[body.records[i].Text[j]];
                            else {
                                body.records[i].record.Text[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Text[j]) + 1;
                                offsetStorage.Add(body.records[i].Text[j], body.records[i].record.Text[j]);
                                reverseStorage.Add(body.records[i].record.Text[j], body.records[i].Text[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(emotestextdataRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // emotestextdata

    public class emotestextsounddbc {
        public DBCHeader header;
        public emotestextsoundBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM emotestextsound", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, EmoteText, Race, Gender, Sound FROM emotestextsound ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new emotestextsoundMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(emotestextsoundRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.EmoteText = reader.GetInt32("EmoteText");
                    body.records[i].record.Race = reader.GetInt32("Race");
                    body.records[i].record.Gender = reader.GetInt32("Gender");
                    body.records[i].record.Sound = reader.GetInt32("Sound");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(emotestextsoundRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // emotestextsound

    public class environmentaldamagedbc {
        public DBCHeader header;
        public environmentaldamageBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM environmentaldamage", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, EnumId, SpellVisual FROM environmentaldamage ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new environmentaldamageMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(environmentaldamageRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.EnumId = reader.GetInt32("EnumId");
                    body.records[i].record.SpellVisual = reader.GetInt32("SpellVisual");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(environmentaldamageRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // environmentaldamage

    public class exhaustiondbc {
        public DBCHeader header;
        public exhaustionBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM exhaustion", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Xp, Factor, OutdoorHours, InnHours, Name, Name_loc2, Threshold FROM exhaustion ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new exhaustionMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 23;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(exhaustionRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Xp = reader.GetInt32("Xp");
                    body.records[i].record.Factor = reader.GetFloat("Factor");
                    body.records[i].record.OutdoorHours = reader.GetFloat("OutdoorHours");
                    body.records[i].record.InnHours = reader.GetFloat("InnHours");
                    body.records[i].record.Threshold = reader.GetInt32("Threshold");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(exhaustionRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // exhaustion

    public class factiongroupdbc {
        public DBCHeader header;
        public factiongroupBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM factiongroup", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Flags, InternalName, Name, Name_loc2 FROM factiongroup ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new factiongroupMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 20;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(factiongroupRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].InternalName = reader.GetString("InternalName");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
                    // InternalName
                    if (body.records[i].InternalName.Length == 0)
                        body.records[i].record.InternalName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].InternalName)) body.records[i].record.InternalName = offsetStorage[body.records[i].InternalName];
                        else {
                            body.records[i].record.InternalName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].InternalName) + 1;
                            offsetStorage.Add(body.records[i].InternalName, body.records[i].record.InternalName);
                            reverseStorage.Add(body.records[i].record.InternalName, body.records[i].InternalName); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(factiongroupRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // factiongroup

    public class filedatadbc {
        public DBCHeader header;
        public filedataBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM filedata", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, FileName, FilePath FROM filedata ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new filedataMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(filedataRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].FileName = reader.GetString("FileName");
                    body.records[i].FilePath = reader.GetString("FilePath");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // FileName
                    if (body.records[i].FileName.Length == 0)
                        body.records[i].record.FileName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName)) body.records[i].record.FileName = offsetStorage[body.records[i].FileName];
                        else {
                            body.records[i].record.FileName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName) + 1;
                            offsetStorage.Add(body.records[i].FileName, body.records[i].record.FileName);
                            reverseStorage.Add(body.records[i].record.FileName, body.records[i].FileName); } }
                    // FilePath
                    if (body.records[i].FilePath.Length == 0)
                        body.records[i].record.FilePath = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FilePath)) body.records[i].record.FilePath = offsetStorage[body.records[i].FilePath];
                        else {
                            body.records[i].record.FilePath = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FilePath) + 1;
                            offsetStorage.Add(body.records[i].FilePath, body.records[i].record.FilePath);
                            reverseStorage.Add(body.records[i].record.FilePath, body.records[i].FilePath); } }
                    // FileName
                    if (body.records[i].FileName.Length == 0)
                        body.records[i].record.FileName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName)) body.records[i].record.FileName = offsetStorage[body.records[i].FileName];
                        else {
                            body.records[i].record.FileName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName) + 1;
                            offsetStorage.Add(body.records[i].FileName, body.records[i].record.FileName);
                            reverseStorage.Add(body.records[i].record.FileName, body.records[i].FileName); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(filedataRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // filedata

    public class footprinttexturesdbc {
        public DBCHeader header;
        public footprinttexturesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM footprinttextures", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, FileName FROM footprinttextures ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new footprinttexturesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(footprinttexturesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].FileName = reader.GetString("FileName");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Texture1
                    if (body.records[i].FileName.Length == 0)
                        body.records[i].record.FileName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName)) body.records[i].record.FileName = offsetStorage[body.records[i].FileName];
                        else {
                            body.records[i].record.FileName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName) + 1;
                            offsetStorage.Add(body.records[i].FileName, body.records[i].record.FileName);
                            reverseStorage.Add(body.records[i].record.FileName, body.records[i].FileName); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(footprinttexturesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // footprinttextures

    public class footstepterrainlookupdbc {
        public DBCHeader header;
        public footstepterrainlookupBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM footstepterrainlookup", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, GroundEffectDoodad, TerrainType, SoundDry, SoundWet FROM footstepterrainlookup ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new footstepterrainlookupMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(footstepterrainlookupRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.GroundEffectDoodad = reader.GetInt32("GroundEffectDoodad");
                    body.records[i].record.TerrainType = reader.GetInt32("TerrainType");
                    body.records[i].record.SoundDry = reader.GetInt32("SoundDry");
                    body.records[i].record.SoundWet = reader.GetInt32("SoundWet");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(footstepterrainlookupRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // footstepterrainlookup

    public class gameobjectartkitdbc {
        public DBCHeader header;
        public gameobjectartkitBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gameobjectartkit", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Texture1, Texture2, Texture3, Model1, Model2, Model3, Model4 FROM gameobjectartkit ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gameobjectartkitMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gameobjectartkitRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Texture1 = reader.GetString("Texture1");
                    body.records[i].Texture2 = reader.GetString("Texture2");
                    body.records[i].Texture3 = reader.GetString("Texture3");
                    body.records[i].Model1 = reader.GetString("Model1");
                    body.records[i].Model2 = reader.GetString("Model2");
                    body.records[i].Model3 = reader.GetString("Model3");
                    body.records[i].Model4 = reader.GetString("Model4");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Texture1
                    if (body.records[i].Texture1.Length == 0)
                        body.records[i].record.Texture1 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Texture1)) body.records[i].record.Texture1 = offsetStorage[body.records[i].Texture1];
                        else {
                            body.records[i].record.Texture1 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Texture1) + 1;
                            offsetStorage.Add(body.records[i].Texture1, body.records[i].record.Texture1);
                            reverseStorage.Add(body.records[i].record.Texture1, body.records[i].Texture1); } }
                    // Texture2
                    if (body.records[i].Texture2.Length == 0)
                        body.records[i].record.Texture2 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Texture2)) body.records[i].record.Texture2 = offsetStorage[body.records[i].Texture2];
                        else {
                            body.records[i].record.Texture2 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Texture2) + 1;
                            offsetStorage.Add(body.records[i].Texture2, body.records[i].record.Texture2);
                            reverseStorage.Add(body.records[i].record.Texture2, body.records[i].Texture2); } }
                    // Texture3
                    if (body.records[i].Texture3.Length == 0)
                        body.records[i].record.Texture3 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Texture3)) body.records[i].record.Texture3 = offsetStorage[body.records[i].Texture3];
                        else {
                            body.records[i].record.Texture3 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Texture3) + 1;
                            offsetStorage.Add(body.records[i].Texture3, body.records[i].record.Texture3);
                            reverseStorage.Add(body.records[i].record.Texture3, body.records[i].Texture3); } }
                    // Model1
                    if (body.records[i].Model1.Length == 0)
                        body.records[i].record.Model1 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Model1)) body.records[i].record.Model1 = offsetStorage[body.records[i].Model1];
                        else {
                            body.records[i].record.Model1 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Model1) + 1;
                            offsetStorage.Add(body.records[i].Model1, body.records[i].record.Model1);
                            reverseStorage.Add(body.records[i].record.Model1, body.records[i].Model1); } }
                    // Model2
                    if (body.records[i].Model2.Length == 0)
                        body.records[i].record.Model2 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Model2)) body.records[i].record.Model2 = offsetStorage[body.records[i].Model2];
                        else {
                            body.records[i].record.Model2 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Model2) + 1;
                            offsetStorage.Add(body.records[i].Model2, body.records[i].record.Model2);
                            reverseStorage.Add(body.records[i].record.Model2, body.records[i].Model2); } }
                    // Model3
                    if (body.records[i].Model3.Length == 0)
                        body.records[i].record.Model3 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Model3)) body.records[i].record.Model3 = offsetStorage[body.records[i].Model3];
                        else {
                            body.records[i].record.Model3 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Model3) + 1;
                            offsetStorage.Add(body.records[i].Model3, body.records[i].record.Model3);
                            reverseStorage.Add(body.records[i].record.Model3, body.records[i].Model3); } }
                    // Model4
                    if (body.records[i].Model4.Length == 0)
                        body.records[i].record.Model4 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Model4)) body.records[i].record.Model4 = offsetStorage[body.records[i].Model4];
                        else {
                            body.records[i].record.Model4 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Model4) + 1;
                            offsetStorage.Add(body.records[i].Model4, body.records[i].record.Model4);
                            reverseStorage.Add(body.records[i].record.Model4, body.records[i].Model4); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gameobjectartkitRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gameobjectartkit

    public class gametablesdbc {
        public DBCHeader header;
        public gametablesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gametables", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Name, NumRows, NumColumns FROM gametables ORDER BY Name ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gametablesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gametablesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.NumRows = reader.GetInt32("NumRows");
                    body.records[i].record.NumColumns = reader.GetInt32("NumColumns");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gametablesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gametables

    public class gametipsdbc {
        public DBCHeader header;
        public gametipsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gametips", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Tip, Tip_loc2 FROM gametips ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gametipsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gametipsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Tip = new string[17];
                    body.records[i].record.Tip = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Tip[loc] = "";
                    body.records[i].Tip[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Tip_loc2" : "Tip");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Tip
                        if (body.records[i].Tip[j].Length == 0)
                            body.records[i].record.Tip[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Tip[j])) body.records[i].record.Tip[j] = offsetStorage[body.records[i].Tip[j]];
                            else {
                                body.records[i].record.Tip[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Tip[j]) + 1;
                                offsetStorage.Add(body.records[i].Tip[j], body.records[i].record.Tip[j]);
                                reverseStorage.Add(body.records[i].record.Tip[j], body.records[i].Tip[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gametipsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gametips

    public class gmsurveyanswersdbc {
        public DBCHeader header;
        public gmsurveyanswersBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gmsurveyanswers", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SortIndex, GMSurveyQuestionId, Answer, Answer_loc2 FROM gmsurveyanswers ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gmsurveyanswersMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 20;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gmsurveyanswersRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SortIndex = reader.GetInt32("SortIndex");
                    body.records[i].record.GMSurveyQuestionId = reader.GetInt32("GMSurveyQuestionId");

                    body.records[i].Answer = new string[17];
                    body.records[i].record.Answer = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Answer[loc] = "";
                    body.records[i].Answer[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Answer_loc2" : "Answer");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Answer
                        if (body.records[i].Answer[j].Length == 0)
                            body.records[i].record.Answer[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Answer[j])) body.records[i].record.Answer[j] = offsetStorage[body.records[i].Answer[j]];
                            else {
                                body.records[i].record.Answer[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Answer[j]) + 1;
                                offsetStorage.Add(body.records[i].Answer[j], body.records[i].record.Answer[j]);
                                reverseStorage.Add(body.records[i].record.Answer[j], body.records[i].Answer[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gmsurveyanswersRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gmsurveyanswers

    public class gmsurveycurrentsurveydbc {
        public DBCHeader header;
        public gmsurveycurrentsurveyBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gmsurveycurrentsurvey", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT LangId, GMSurveyId FROM gmsurveycurrentsurvey ORDER BY LangId ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gmsurveycurrentsurveyMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gmsurveycurrentsurveyRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.LangId = reader.GetInt32("LangId");
                    body.records[i].record.GMSurveyId = reader.GetInt32("GMSurveyId");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gmsurveycurrentsurveyRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gmsurveycurrentsurvey

    public class gmsurveyquestionsdbc {
        public DBCHeader header;
        public gmsurveyquestionsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gmsurveyquestions", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Question, Question_loc2 FROM gmsurveyquestions ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gmsurveyquestionsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gmsurveyquestionsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Question = new string[17];
                    body.records[i].record.Question = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Question[loc] = "";
                    body.records[i].Question[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Question_loc2" : "Question");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Question
                        if (body.records[i].Question[j].Length == 0)
                            body.records[i].record.Question[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Question[j])) body.records[i].record.Question[j] = offsetStorage[body.records[i].Question[j]];
                            else {
                                body.records[i].record.Question[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Question[j]) + 1;
                                offsetStorage.Add(body.records[i].Question[j], body.records[i].record.Question[j]);
                                reverseStorage.Add(body.records[i].record.Question[j], body.records[i].Question[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gmsurveyquestionsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gmsurveyquestions

    public class gmsurveysurveysdbc {
        public DBCHeader header;
        public gmsurveysurveysBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gmsurveysurveys", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, RefId1, RefId2, RefId3, RefId4, RefId5, RefId6, RefId7, RefId8, RefId9, RefId10 FROM gmsurveysurveys ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gmsurveysurveysMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 11;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gmsurveysurveysRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.RefId1 = reader.GetInt32("RefId1");
                    body.records[i].record.RefId2 = reader.GetInt32("RefId2");
                    body.records[i].record.RefId3 = reader.GetInt32("RefId3");
                    body.records[i].record.RefId4 = reader.GetInt32("RefId4");
                    body.records[i].record.RefId5 = reader.GetInt32("RefId5");
                    body.records[i].record.RefId6 = reader.GetInt32("RefId6");
                    body.records[i].record.RefId7 = reader.GetInt32("RefId7");
                    body.records[i].record.RefId8 = reader.GetInt32("RefId8");
                    body.records[i].record.RefId9 = reader.GetInt32("RefId9");
                    body.records[i].record.RefId10 = reader.GetInt32("RefId10");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gmsurveysurveysRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gmsurveysurveys

    public class gmticketcategorydbc {
        public DBCHeader header;
        public gmticketcategoryBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gmticketcategory", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2 FROM gmticketcategory ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gmticketcategoryMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gmticketcategoryRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gmticketcategoryRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gmticketcategory

    public class groundeffectdoodaddbc {
        public DBCHeader header;
        public groundeffectdoodadBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM groundeffectdoodad", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Model, Flags FROM groundeffectdoodad ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new groundeffectdoodadMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(groundeffectdoodadRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Model = reader.GetString("Model");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Model
                    if (body.records[i].Model.Length == 0)
                        body.records[i].record.Model = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Model)) body.records[i].record.Model = offsetStorage[body.records[i].Model];
                        else {
                            body.records[i].record.Model = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Model) + 1;
                            offsetStorage.Add(body.records[i].Model, body.records[i].record.Model);
                            reverseStorage.Add(body.records[i].record.Model, body.records[i].Model); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(groundeffectdoodadRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // groundeffectdoodad

    public class groundeffecttexturedbc {
        public DBCHeader header;
        public groundeffecttextureBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM groundeffecttexture", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Doodad1, Doodad2, Doodad3, Doodad4, Weight1, Weight2, Weight3, Weight4, Amount, TerrainType FROM groundeffecttexture ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new groundeffecttextureMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 11;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(groundeffecttextureRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Doodad1 = reader.GetInt32("Doodad1");
                    body.records[i].record.Doodad2 = reader.GetInt32("Doodad2");
                    body.records[i].record.Doodad3 = reader.GetInt32("Doodad3");
                    body.records[i].record.Doodad4 = reader.GetInt32("Doodad4");
                    body.records[i].record.Weight1 = reader.GetInt32("Weight1");
                    body.records[i].record.Weight2 = reader.GetInt32("Weight2");
                    body.records[i].record.Weight3 = reader.GetInt32("Weight3");
                    body.records[i].record.Weight4 = reader.GetInt32("Weight4");
                    body.records[i].record.Amount = reader.GetInt32("Amount");
                    body.records[i].record.TerrainType = reader.GetInt32("TerrainType");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(groundeffecttextureRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // groundeffecttexture
    
    public class gtbarbershopcostbasedbc {
        public DBCHeader header;
        public gtbarbershopcostbaseBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtbarbershopcostbase", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT `Value` FROM gtbarbershopcostbase ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtbarbershopcostbaseMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtbarbershopcostbaseRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtbarbershopcostbaseRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtbarbershopcostbase

    public class gtchancetomeleecritdbc {
        public DBCHeader header;
        public gtchancetomeleecritBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtchancetomeleecrit", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT `Value` FROM gtchancetomeleecrit ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtchancetomeleecritMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtchancetomeleecritRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtchancetomeleecritRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtchancetomeleecrit

    public class gtchancetomeleecritbasedbc {
        public DBCHeader header;
        public gtchancetomeleecritbaseBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtchancetomeleecritbase", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT `Value` FROM gtchancetomeleecritbase ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtchancetomeleecritbaseMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtchancetomeleecritbaseRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtchancetomeleecritbaseRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtchancetomeleecritbase

    public class gtchancetospellcritdbc {
        public DBCHeader header;
        public gtchancetospellcritBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtchancetospellcrit", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT `Value` FROM gtchancetospellcrit ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtchancetospellcritMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtchancetospellcritRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtchancetospellcritRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtchancetospellcrit

    public class gtchancetospellcritbasedbc {
        public DBCHeader header;
        public gtchancetospellcritbaseBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtchancetospellcritbase", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT `Value` FROM gtchancetospellcritbase ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtchancetospellcritbaseMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtchancetospellcritbaseRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtchancetospellcritbaseRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtchancetospellcritbase

    public class gtcombatratingsdbc {
        public DBCHeader header;
        public gtcombatratingsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtcombatratings", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT `Value` FROM gtcombatratings ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtcombatratingsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtcombatratingsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtcombatratingsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtcombatratings

    public class gtnpcmanacostscalerdbc {
        public DBCHeader header;
        public gtnpcmanacostscalerBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtnpcmanacostscaler", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT `Value` FROM gtnpcmanacostscaler ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtnpcmanacostscalerMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtnpcmanacostscalerRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtnpcmanacostscalerRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtnpcmanacostscaler

    public class gtoctclasscombatratingscalardbc {
        public DBCHeader header;
        public gtoctclasscombatratingscalarBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtoctclasscombatratingscalar", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, `Value` FROM gtoctclasscombatratingscalar ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtoctclasscombatratingscalarMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtoctclasscombatratingscalarRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtoctclasscombatratingscalarRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtoctclasscombatratingscalar

    public class gtoctregenhpdbc {
        public DBCHeader header;
        public gtoctregenhpBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtoctregenhp", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT `Value` FROM gtoctregenhp ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtoctregenhpMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtoctregenhpRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtoctregenhpRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtoctregenhp

    public class gtoctregenmpdbc {
        public DBCHeader header;
        public gtoctregenmpBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtoctregenmp", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT `Value` FROM gtoctregenmp ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtoctregenmpMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtoctregenmpRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtoctregenmpRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtoctregenmp

    public class gtregenhppersptdbc {
        public DBCHeader header;
        public gtregenhppersptBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtregenhpperspt", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT `Value` FROM gtregenhpperspt ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtregenhppersptMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtregenhppersptRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtregenhppersptRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtregenhpperspt

    public class gtregenmppersptdbc {
        public DBCHeader header;
        public gtregenmppersptBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gtregenmpperspt", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT `Value` FROM gtregenmpperspt ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new gtregenmppersptMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gtregenmppersptRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Value = reader.GetFloat("Value");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(gtregenmppersptRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // gtregenmpperspt

    public class helmetgeosetvisdatadbc {
        public DBCHeader header;
        public helmetgeosetvisdataBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM helmetgeosetvisdata", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, HairFlags, Facial1Flags, Facial2Flags, Facial3Flags, EarsFlags, Unk1, Unk2 FROM helmetgeosetvisdata ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new helmetgeosetvisdataMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(helmetgeosetvisdataRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.HairFlags = reader.GetInt32("HairFlags");
                    body.records[i].record.Facial1Flags = reader.GetInt32("Facial1Flags");
                    body.records[i].record.Facial2Flags = reader.GetInt32("Facial2Flags");
                    body.records[i].record.Facial3Flags = reader.GetInt32("Facial3Flags");
                    body.records[i].record.EarsFlags = reader.GetInt32("EarsFlags");
                    body.records[i].record.Unk1 = reader.GetInt32("Unk1");
                    body.records[i].record.Unk2 = reader.GetInt32("Unk2");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(helmetgeosetvisdataRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // helmetgeosetvisdata

    public class holidaydescriptionsdbc {
        public DBCHeader header;
        public holidaydescriptionsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM holidaydescriptions", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Description, Description_loc2 FROM holidaydescriptions ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new holidaydescriptionsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(holidaydescriptionsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Description = new string[17];
                    body.records[i].record.Description = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Description[loc] = "";
                    body.records[i].Description[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Description_loc2" : "Description");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Description
                        if (body.records[i].Description[j].Length == 0)
                            body.records[i].record.Description[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Description[j])) body.records[i].record.Description[j] = offsetStorage[body.records[i].Description[j]];
                            else {
                                body.records[i].record.Description[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Description[j]) + 1;
                                offsetStorage.Add(body.records[i].Description[j], body.records[i].record.Description[j]);
                                reverseStorage.Add(body.records[i].record.Description[j], body.records[i].Description[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(holidaydescriptionsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // holidaydescriptions

    public class holidaynamesdbc {
        public DBCHeader header;
        public holidaynamesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM holidaynames", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2 FROM holidaynames ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new holidaynamesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(holidaynamesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(holidaynamesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // holidaynames

    public class itemclassdbc {
        public DBCHeader header;
        public itemclassBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemclass", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SubClass, IsWeapon, Name, Name_loc2 FROM itemclass ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new itemclassMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 20;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemclassRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SubClass = reader.GetInt32("SubClass");
                    body.records[i].record.IsWeapon = reader.GetInt32("IsWeapon");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(itemclassRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // itemclass

    public class itemcondextcostsdbc {
        public DBCHeader header;
        public itemcondextcostsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemcondextcosts", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Unk, ExtendedCost, Unk2 FROM itemcondextcosts ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new itemcondextcostsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemcondextcostsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Unk = reader.GetInt32("Unk");
                    body.records[i].record.ExtendedCost = reader.GetInt32("ExtendedCost");
                    body.records[i].record.Unk2 = reader.GetInt32("Unk2");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(itemcondextcostsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // itemcondextcosts

    public class itemgroupsoundsdbc {
        public DBCHeader header;
        public itemgroupsoundsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemgroupsounds", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Pickup, Putdown, Unk1, Unk2 FROM itemgroupsounds ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new itemgroupsoundsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemgroupsoundsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Pickup = reader.GetInt32("Pickup");
                    body.records[i].record.Putdown = reader.GetInt32("Putdown");
                    body.records[i].record.Unk1 = reader.GetInt32("Unk1");
                    body.records[i].record.Unk2 = reader.GetInt32("Unk2");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(itemgroupsoundsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // itemgroupsounds

    public class itempetfooddbc {
        public DBCHeader header;
        public itempetfoodBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itempetfood", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2 FROM itempetfood ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new itempetfoodMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itempetfoodRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(itempetfoodRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // itempetfood

    public class itempurchasegroupdbc {
        public DBCHeader header;
        public itempurchasegroupBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itempurchasegroup", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Description, Description_loc2 FROM itempurchasegroup ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new itempurchasegroupMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 26;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itempurchasegroupRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Item1 = reader.GetInt32("Item1");
                    body.records[i].record.Item2 = reader.GetInt32("Item2");
                    body.records[i].record.Item3 = reader.GetInt32("Item3");
                    body.records[i].record.Item4 = reader.GetInt32("Item4");
                    body.records[i].record.Item5 = reader.GetInt32("Item5");
                    body.records[i].record.Item6 = reader.GetInt32("Item6");
                    body.records[i].record.Item7 = reader.GetInt32("Item7");
                    body.records[i].record.Item8 = reader.GetInt32("Item8");

                    body.records[i].Description = new string[17];
                    body.records[i].record.Description = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Description[loc] = "";
                    body.records[i].Description[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Description_loc2" : "Description");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Description
                        if (body.records[i].Description[j].Length == 0)
                            body.records[i].record.Description[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Description[j])) body.records[i].record.Description[j] = offsetStorage[body.records[i].Description[j]];
                            else {
                                body.records[i].record.Description[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Description[j]) + 1;
                                offsetStorage.Add(body.records[i].Description[j], body.records[i].record.Description[j]);
                                reverseStorage.Add(body.records[i].record.Description[j], body.records[i].Description[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(itempurchasegroupRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // itempurchasegroup

    public class itemsubclassdbc {
        public DBCHeader header;
        public itemsubclassBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemsubclass", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT ItemClass, SubClass, PreRequisiteProficiency, PostRequisiteProficiency, Flags, DisplayFlags, WeaponParrySeq, WeaponReadySeq, WeaponAttackSeq, WeaponSwingSize, DisplayName, DisplayName_loc2, VerboseName, VerboseName_loc2 FROM itemsubclass ORDER BY ItemClass ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new itemsubclassMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 44;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemsubclassRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.ItemClass = reader.GetInt32("ItemClass");
                    body.records[i].record.SubClass = reader.GetInt32("SubClass");
                    body.records[i].record.PreRequisiteProficiency = reader.GetInt32("PreRequisiteProficiency");
                    body.records[i].record.PostRequisiteProficiency = reader.GetInt32("PostRequisiteProficiency");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.DisplayFlags = reader.GetInt32("DisplayFlags");
                    body.records[i].record.WeaponParrySeq = reader.GetInt32("WeaponParrySeq");
                    body.records[i].record.WeaponReadySeq = reader.GetInt32("WeaponReadySeq");
                    body.records[i].record.WeaponAttackSeq = reader.GetInt32("WeaponAttackSeq");
                    body.records[i].record.WeaponSwingSize = reader.GetInt32("WeaponSwingSize");

                    body.records[i].DisplayName = new string[17];
                    body.records[i].VerboseName = new string[17];
                    body.records[i].record.DisplayName = new UInt32[17];
                    body.records[i].record.VerboseName = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].DisplayName [loc] = "";
                        body.records[i].VerboseName[loc] = ""; }
                    body.records[i].DisplayName [DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "DisplayName_loc2" : "DisplayName");
                    body.records[i].VerboseName[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "VerboseName_loc2" : "VerboseName");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // DisplayName
                        if (body.records[i].DisplayName[j].Length == 0)
                            body.records[i].record.DisplayName[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].DisplayName[j])) body.records[i].record.DisplayName[j] = offsetStorage[body.records[i].DisplayName[j]];
                            else {
                                body.records[i].record.DisplayName[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].DisplayName[j]) + 1;
                                offsetStorage.Add(body.records[i].DisplayName[j], body.records[i].record.DisplayName[j]);
                                reverseStorage.Add(body.records[i].record.DisplayName[j], body.records[i].DisplayName[j]); } }
                        // VerboseName
                        if (body.records[i].VerboseName[j].Length == 0)
                            body.records[i].record.VerboseName[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].VerboseName[j])) body.records[i].record.VerboseName[j] = offsetStorage[body.records[i].VerboseName[j]];
                            else {
                                body.records[i].record.VerboseName[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].VerboseName[j]) + 1;
                                offsetStorage.Add(body.records[i].VerboseName[j], body.records[i].record.VerboseName[j]);
                                reverseStorage.Add(body.records[i].record.VerboseName[j], body.records[i].VerboseName[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(itemsubclassRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // itemsubclass

    public class itemsubclassmaskdbc {
        public DBCHeader header;
        public itemsubclassmaskBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemsubclassmask", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT ItemClass, SubClassMask, Name, Name_loc2 FROM itemsubclassmask ORDER BY ItemClass ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new itemsubclassmaskMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 19;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemsubclassmaskRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.ItemClass = reader.GetInt32("ItemClass");
                    body.records[i].record.SubClassMask = reader.GetInt32("SubClassMask");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(itemsubclassmaskRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // itemsubclassmask

    public class itemvisualeffectsdbc {
        public DBCHeader header;
        public itemvisualeffectsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemvisualeffects", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Model FROM itemvisualeffects ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new itemvisualeffectsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemvisualeffectsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Model = reader.GetString("Model");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Model
                    if (body.records[i].Model.Length == 0)
                        body.records[i].record.Model = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Model)) body.records[i].record.Model = offsetStorage[body.records[i].Model];
                        else {
                            body.records[i].record.Model = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Model) + 1;
                            offsetStorage.Add(body.records[i].Model, body.records[i].record.Model);
                            reverseStorage.Add(body.records[i].record.Model, body.records[i].Model); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(itemvisualeffectsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // itemvisualeffects

    public class itemvisualsdbc {
        public DBCHeader header;
        public itemvisualsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemvisuals", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, VisualEffect1, VisualEffect2, VisualEffect3, VisualEffect4, VisualEffect5 FROM itemvisuals ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new itemvisualsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 6;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemvisualsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.VisualEffect1 = reader.GetInt32("VisualEffect1");
                    body.records[i].record.VisualEffect2 = reader.GetInt32("VisualEffect2");
                    body.records[i].record.VisualEffect3 = reader.GetInt32("VisualEffect3");
                    body.records[i].record.VisualEffect4 = reader.GetInt32("VisualEffect4");
                    body.records[i].record.VisualEffect5 = reader.GetInt32("VisualEffect5");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(itemvisualsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // itemvisuals

    public class languagesdbc {
        public DBCHeader header;
        public languagesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM languages", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2 FROM languages ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new languagesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(languagesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(languagesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // languages

    public class languagewordsdbc {
        public DBCHeader header;
        public languagewordsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM languagewords", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Language, Word FROM languagewords ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new languagewordsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(languagewordsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Language = reader.GetInt32("Language");
                    body.records[i].Word = reader.GetString("Word");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Word
                    if (body.records[i].Word.Length == 0)
                        body.records[i].record.Word = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Word)) body.records[i].record.Word = offsetStorage[body.records[i].Word];
                        else {
                            body.records[i].record.Word = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Word) + 1;
                            offsetStorage.Add(body.records[i].Word, body.records[i].record.Word);
                            reverseStorage.Add(body.records[i].record.Word, body.records[i].Word); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(languagewordsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // languagewords

    public class lfgdungeonexpansiondbc {
        public DBCHeader header;
        public lfgdungeonexpansionBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM lfgdungeonexpansion", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Dungeon, Expansion, RandomId, HardModeLevelMin, HardModeLevelMax, TargetLevelMin, TargetLevelMax FROM lfgdungeonexpansion ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new lfgdungeonexpansionMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(lfgdungeonexpansionRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Dungeon = reader.GetInt32("Dungeon");
                    body.records[i].record.Expansion = reader.GetInt32("Expansion");
                    body.records[i].record.RandomId = reader.GetInt32("RandomId");
                    body.records[i].record.HardModeLevelMin = reader.GetInt32("HardModeLevelMin");
                    body.records[i].record.HardModeLevelMax = reader.GetInt32("HardModeLevelMax");
                    body.records[i].record.TargetLevelMin = reader.GetInt32("TargetLevelMin");
                    body.records[i].record.TargetLevelMax = reader.GetInt32("TargetLevelMax");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(lfgdungeonexpansionRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // lfgdungeonexpansion

    public class lfgdungeongroupdbc {
        public DBCHeader header;
        public lfgdungeongroupBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM lfgdungeongroup", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, OrderIndex, ParentGroup, TypeId FROM lfgdungeongroup ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new lfgdungeongroupMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 21;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(lfgdungeongroupRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.OrderIndex = reader.GetInt32("OrderIndex");
                    body.records[i].record.ParentGroup = reader.GetInt32("ParentGroup");
                    body.records[i].record.TypeId = reader.GetInt32("TypeId");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(lfgdungeongroupRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // lfgdungeongroup

    public class lightfloatbanddbc {
        public DBCHeader header;
        public lightfloatbandBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM lightfloatband", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, UsedValues, TimeValue1, TimeValue2, TimeValue3, TimeValue4, TimeValue5, TimeValue6, TimeValue7, TimeValue8, TimeValue9, TimeValue10, TimeValue11, TimeValue12, TimeValue13, TimeValue14, TimeValue15, TimeValue16, FloatValue1, FloatValue2, FloatValue3, FloatValue4, FloatValue5, FloatValue6, FloatValue7, FloatValue8, FloatValue9, FloatValue10, FloatValue11, FloatValue12, FloatValue13, FloatValue14, FloatValue15, FloatValue16 FROM lightfloatband ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new lightfloatbandMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 34;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(lightfloatbandRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.UsedValues = reader.GetInt32("UsedValues");
                    body.records[i].record.TimeValue1 = reader.GetInt32("TimeValue1");
                    body.records[i].record.TimeValue2 = reader.GetInt32("TimeValue2");
                    body.records[i].record.TimeValue3 = reader.GetInt32("TimeValue3");
                    body.records[i].record.TimeValue4 = reader.GetInt32("TimeValue4");
                    body.records[i].record.TimeValue5 = reader.GetInt32("TimeValue5");
                    body.records[i].record.TimeValue6 = reader.GetInt32("TimeValue6");
                    body.records[i].record.TimeValue7 = reader.GetInt32("TimeValue7");
                    body.records[i].record.TimeValue8 = reader.GetInt32("TimeValue8");
                    body.records[i].record.TimeValue9 = reader.GetInt32("TimeValue9");
                    body.records[i].record.TimeValue10 = reader.GetInt32("TimeValue10");
                    body.records[i].record.TimeValue11 = reader.GetInt32("TimeValue11");
                    body.records[i].record.TimeValue12 = reader.GetInt32("TimeValue12");
                    body.records[i].record.TimeValue13 = reader.GetInt32("TimeValue13");
                    body.records[i].record.TimeValue14 = reader.GetInt32("TimeValue14");
                    body.records[i].record.TimeValue15 = reader.GetInt32("TimeValue15");
                    body.records[i].record.TimeValue16 = reader.GetInt32("TimeValue16");
                    body.records[i].record.FloatValue1 = reader.GetFloat("FloatValue1");
                    body.records[i].record.FloatValue2 = reader.GetFloat("FloatValue2");
                    body.records[i].record.FloatValue3 = reader.GetFloat("FloatValue3");
                    body.records[i].record.FloatValue4 = reader.GetFloat("FloatValue4");
                    body.records[i].record.FloatValue5 = reader.GetFloat("FloatValue5");
                    body.records[i].record.FloatValue6 = reader.GetFloat("FloatValue6");
                    body.records[i].record.FloatValue7 = reader.GetFloat("FloatValue7");
                    body.records[i].record.FloatValue8 = reader.GetFloat("FloatValue8");
                    body.records[i].record.FloatValue9 = reader.GetFloat("FloatValue9");
                    body.records[i].record.FloatValue10 = reader.GetFloat("FloatValue10");
                    body.records[i].record.FloatValue11 = reader.GetFloat("FloatValue11");
                    body.records[i].record.FloatValue12 = reader.GetFloat("FloatValue12");
                    body.records[i].record.FloatValue13 = reader.GetFloat("FloatValue13");
                    body.records[i].record.FloatValue14 = reader.GetFloat("FloatValue14");
                    body.records[i].record.FloatValue15 = reader.GetFloat("FloatValue15");
                    body.records[i].record.FloatValue16 = reader.GetFloat("FloatValue16");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(lightfloatbandRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // lightfloatband

    public class lightintbanddbc {
        public DBCHeader header;
        public lightintbandBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM lightintband", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, UsedValues, TimeValue1, TimeValue2, TimeValue3, TimeValue4, TimeValue5, TimeValue6, TimeValue7, TimeValue8, TimeValue9, TimeValue10, TimeValue11, TimeValue12, TimeValue13, TimeValue14, TimeValue15, TimeValue16, Color1_R, Color1_G, Color1_B, Color1_A, Color2_R, Color2_G, Color2_B, Color2_A, Color3_R, Color3_G, Color3_B, Color3_A, Color4_R, Color4_G, Color4_B, Color4_A, Color5_R, Color5_G, Color5_B, Color5_A, Color6_R, Color6_G, Color6_B, Color6_A, Color7_R, Color7_G, Color7_B, Color7_A, Color8_R, Color8_G, Color8_B, Color8_A, Color9_R, Color9_G, Color9_B, Color9_A, Color10_R, Color10_G, Color10_B, Color10_A, Color11_R, Color11_G, Color11_B, Color11_A, Color12_R, Color12_G, Color12_B, Color12_A, Color13_R, Color13_G, Color13_B, Color13_A, Color14_R, Color14_G, Color14_B, Color14_A, Color15_R, Color15_G, Color15_B, Color15_A, Color16_R, Color16_G, Color16_B, Color16_A FROM lightintband ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new lightintbandMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 34;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(lightintbandRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.UsedValues = reader.GetInt32("UsedValues");
                    body.records[i].record.TimeValue1 = reader.GetInt32("TimeValue1");
                    body.records[i].record.TimeValue2 = reader.GetInt32("TimeValue2");
                    body.records[i].record.TimeValue3 = reader.GetInt32("TimeValue3");
                    body.records[i].record.TimeValue4 = reader.GetInt32("TimeValue4");
                    body.records[i].record.TimeValue5 = reader.GetInt32("TimeValue5");
                    body.records[i].record.TimeValue6 = reader.GetInt32("TimeValue6");
                    body.records[i].record.TimeValue7 = reader.GetInt32("TimeValue7");
                    body.records[i].record.TimeValue8 = reader.GetInt32("TimeValue8");
                    body.records[i].record.TimeValue9 = reader.GetInt32("TimeValue9");
                    body.records[i].record.TimeValue10 = reader.GetInt32("TimeValue10");
                    body.records[i].record.TimeValue11 = reader.GetInt32("TimeValue11");
                    body.records[i].record.TimeValue12 = reader.GetInt32("TimeValue12");
                    body.records[i].record.TimeValue13 = reader.GetInt32("TimeValue13");
                    body.records[i].record.TimeValue14 = reader.GetInt32("TimeValue14");
                    body.records[i].record.TimeValue15 = reader.GetInt32("TimeValue15");
                    body.records[i].record.TimeValue16 = reader.GetInt32("TimeValue16");
                    byte[] Color1 = { reader.GetByte("Color1_R"), reader.GetByte("Color1_G"), reader.GetByte("Color1_B"), reader.GetByte("Color1_A") };
                    body.records[i].record.Color1 = BitConverter.ToInt32(Color1, 0);
                    byte[] Color2 = { reader.GetByte("Color2_R"), reader.GetByte("Color2_G"), reader.GetByte("Color2_B"), reader.GetByte("Color2_A") };
                    body.records[i].record.Color2 = BitConverter.ToInt32(Color2, 0);
                    byte[] Color3 = { reader.GetByte("Color3_R"), reader.GetByte("Color3_G"), reader.GetByte("Color3_B"), reader.GetByte("Color3_A") };
                    body.records[i].record.Color3 = BitConverter.ToInt32(Color3, 0);
                    byte[] Color4 = { reader.GetByte("Color4_R"), reader.GetByte("Color4_G"), reader.GetByte("Color4_B"), reader.GetByte("Color4_A") };
                    body.records[i].record.Color4 = BitConverter.ToInt32(Color4, 0);
                    byte[] Color5 = { reader.GetByte("Color5_R"), reader.GetByte("Color5_G"), reader.GetByte("Color5_B"), reader.GetByte("Color5_A") };
                    body.records[i].record.Color5 = BitConverter.ToInt32(Color5, 0);
                    byte[] Color6 = { reader.GetByte("Color6_R"), reader.GetByte("Color6_G"), reader.GetByte("Color6_B"), reader.GetByte("Color6_A") };
                    body.records[i].record.Color6 = BitConverter.ToInt32(Color6, 0);
                    byte[] Color7 = { reader.GetByte("Color7_R"), reader.GetByte("Color7_G"), reader.GetByte("Color7_B"), reader.GetByte("Color7_A") };
                    body.records[i].record.Color7 = BitConverter.ToInt32(Color7, 0);
                    byte[] Color8 = { reader.GetByte("Color8_R"), reader.GetByte("Color8_G"), reader.GetByte("Color8_B"), reader.GetByte("Color8_A") };
                    body.records[i].record.Color8 = BitConverter.ToInt32(Color8, 0);
                    byte[] Color9 = { reader.GetByte("Color9_R"), reader.GetByte("Color9_G"), reader.GetByte("Color9_B"), reader.GetByte("Color9_A") };
                    body.records[i].record.Color9 = BitConverter.ToInt32(Color9, 0);
                    byte[] Color10 = { reader.GetByte("Color10_R"), reader.GetByte("Color10_G"), reader.GetByte("Color10_B"), reader.GetByte("Color10_A") };
                    body.records[i].record.Color10 = BitConverter.ToInt32(Color10, 0);
                    byte[] Color11 = { reader.GetByte("Color11_R"), reader.GetByte("Color11_G"), reader.GetByte("Color11_B"), reader.GetByte("Color11_A") };
                    body.records[i].record.Color11 = BitConverter.ToInt32(Color11, 0);
                    byte[] Color12 = { reader.GetByte("Color12_R"), reader.GetByte("Color12_G"), reader.GetByte("Color12_B"), reader.GetByte("Color12_A") };
                    body.records[i].record.Color12 = BitConverter.ToInt32(Color12, 0);
                    byte[] Color13 = { reader.GetByte("Color13_R"), reader.GetByte("Color13_G"), reader.GetByte("Color13_B"), reader.GetByte("Color13_A") };
                    body.records[i].record.Color13 = BitConverter.ToInt32(Color13, 0);
                    byte[] Color14 = { reader.GetByte("Color14_R"), reader.GetByte("Color14_G"), reader.GetByte("Color14_B"), reader.GetByte("Color14_A") };
                    body.records[i].record.Color14 = BitConverter.ToInt32(Color14, 0);
                    byte[] Color15 = { reader.GetByte("Color15_R"), reader.GetByte("Color15_G"), reader.GetByte("Color15_B"), reader.GetByte("Color15_A") };
                    body.records[i].record.Color15 = BitConverter.ToInt32(Color15, 0);
                    byte[] Color16 = { reader.GetByte("Color16_R"), reader.GetByte("Color16_G"), reader.GetByte("Color16_B"), reader.GetByte("Color16_A") };
                    body.records[i].record.Color16 = BitConverter.ToInt32(Color16, 0);
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(lightintbandRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // lightintband

    public class lightparamsdbc {
        public DBCHeader header;
        public lightparamsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM lightparams", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, HighLightSky, SkyBox, CloudType, Glow, WaterShallow, WaterDeep, OceanShallow, OceanDeep FROM lightparams ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new lightparamsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 9;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(lightparamsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.HighLightSky = reader.GetInt32("HighLightSky");
                    body.records[i].record.SkyBox = reader.GetInt32("SkyBox");
                    body.records[i].record.CloudType = reader.GetInt32("CloudType");
                    body.records[i].record.Glow = reader.GetFloat("Glow");
                    body.records[i].record.WaterShallow = reader.GetFloat("WaterShallow");
                    body.records[i].record.WaterDeep = reader.GetFloat("WaterDeep");
                    body.records[i].record.OceanShallow = reader.GetFloat("OceanShallow");
                    body.records[i].record.OceanDeep = reader.GetFloat("OceanDeep");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(lightparamsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // lightparams

    public class lightskyboxdbc {
        public DBCHeader header;
        public lightskyboxBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM lightskybox", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Model, Flags FROM lightskybox ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new lightskyboxMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(lightskyboxRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Model = reader.GetString("Model");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Model
                    if (body.records[i].Model.Length == 0)
                        body.records[i].record.Model = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Model)) body.records[i].record.Model = offsetStorage[body.records[i].Model];
                        else {
                            body.records[i].record.Model = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Model) + 1;
                            offsetStorage.Add(body.records[i].Model, body.records[i].record.Model);
                            reverseStorage.Add(body.records[i].record.Model, body.records[i].Model); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(lightskyboxRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // lightskybox

    public class liquidmaterialdbc {
        public DBCHeader header;
        public liquidmaterialBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM liquidmaterial", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, LiquidVertexFormat, IsTransparent FROM liquidmaterial ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new liquidmaterialMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(liquidmaterialRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.LiquidVertexFormat = reader.GetInt32("LiquidVertexFormat");
                    body.records[i].record.IsTransparent = reader.GetInt32("IsTransparent");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(liquidmaterialRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // liquidmaterial

    public class loadingscreensdbc {
        public DBCHeader header;
        public loadingscreensBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM loadingscreens", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Texture, HasWideScreen FROM loadingscreens ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new loadingscreensMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(loadingscreensRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].Texture = reader.GetString("Texture");
                    body.records[i].record.HasWideScreen = reader.GetInt32("HasWideScreen");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } }
                    // Texture
                    if (body.records[i].Texture.Length == 0)
                        body.records[i].record.Texture = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Texture)) body.records[i].record.Texture = offsetStorage[body.records[i].Texture];
                        else {
                            body.records[i].record.Texture = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Texture) + 1;
                            offsetStorage.Add(body.records[i].Texture, body.records[i].record.Texture);
                            reverseStorage.Add(body.records[i].record.Texture, body.records[i].Texture); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(loadingscreensRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // loadingscreens

    public class loadingscreentaxisplinesdbc {
        public DBCHeader header;
        public loadingscreentaxisplinesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM loadingscreentaxisplines", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, TaxiPath, X1, X2, X3, X4, X5, X6, X7, X8, Y1, Y2, Y3, Y4, Y5, Y6, Y7, Y8, LegIndex FROM loadingscreentaxisplines ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new loadingscreentaxisplinesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 19;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(loadingscreentaxisplinesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.TaxiPath = reader.GetInt32("TaxiPath");
                    body.records[i].record.X1 = reader.GetFloat("X1");
                    body.records[i].record.X2 = reader.GetFloat("X2");
                    body.records[i].record.X3 = reader.GetFloat("X3");
                    body.records[i].record.X4 = reader.GetFloat("X4");
                    body.records[i].record.X5 = reader.GetFloat("X5");
                    body.records[i].record.X6 = reader.GetFloat("X6");
                    body.records[i].record.X7 = reader.GetFloat("X7");
                    body.records[i].record.X8 = reader.GetFloat("X8");
                    body.records[i].record.Y1 = reader.GetFloat("Y1");
                    body.records[i].record.Y2 = reader.GetFloat("Y2");
                    body.records[i].record.Y3 = reader.GetFloat("Y3");
                    body.records[i].record.Y4 = reader.GetFloat("Y4");
                    body.records[i].record.Y5 = reader.GetFloat("Y5");
                    body.records[i].record.Y6 = reader.GetFloat("Y6");
                    body.records[i].record.Y7 = reader.GetFloat("Y7");
                    body.records[i].record.Y8 = reader.GetFloat("Y8");
                    body.records[i].record.LegIndex = reader.GetInt32("LegIndex");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(loadingscreentaxisplinesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // loadingscreentaxisplines
    public class locktypedbc {
        public DBCHeader header;
        public locktypeBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM locktype", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, ItemStateName, ItemStateName_loc2, ProcessName, ProcessName_loc2, InternalName FROM locktype ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new locktypeMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 53;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(locktypeRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].InternalName = reader.GetString("InternalName");

                    body.records[i].Name = new string[17];
                    body.records[i].ItemStateName = new string[17];
                    body.records[i].ProcessName = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.ItemStateName = new UInt32[17];
                    body.records[i].record.ProcessName = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name [loc] = "";
                        body.records[i].ItemStateName [loc] = "";
                        body.records[i].ProcessName [loc] = ""; }
                    body.records[i].Name [DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");
                    body.records[i].ItemStateName [DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "ItemStateName_loc2" : "ItemStateName");
                    body.records[i].ProcessName [DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "ProcessName_loc2" : "ProcessName");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } }
                        // ItemStateName
                        if (body.records[i].ItemStateName[j].Length == 0)
                            body.records[i].record.ItemStateName[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].ItemStateName[j])) body.records[i].record.ItemStateName[j] = offsetStorage[body.records[i].ItemStateName[j]];
                            else {
                                body.records[i].record.ItemStateName[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].ItemStateName[j]) + 1;
                                offsetStorage.Add(body.records[i].ItemStateName[j], body.records[i].record.ItemStateName[j]);
                                reverseStorage.Add(body.records[i].record.ItemStateName[j], body.records[i].ItemStateName[j]); } }
                        // ProcessName
                        if (body.records[i].ProcessName[j].Length == 0)
                            body.records[i].record.ProcessName[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].ProcessName[j])) body.records[i].record.ProcessName[j] = offsetStorage[body.records[i].ProcessName[j]];
                            else {
                                body.records[i].record.ProcessName[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].ProcessName[j]) + 1;
                                offsetStorage.Add(body.records[i].ProcessName[j], body.records[i].record.ProcessName[j]);
                                reverseStorage.Add(body.records[i].record.ProcessName[j], body.records[i].ProcessName[j]); } } }
                    // InternalName
                    if (body.records[i].InternalName.Length == 0)
                        body.records[i].record.InternalName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].InternalName)) body.records[i].record.InternalName = offsetStorage[body.records[i].InternalName];
                        else {
                            body.records[i].record.InternalName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].InternalName) + 1;
                            offsetStorage.Add(body.records[i].InternalName, body.records[i].record.InternalName);
                            reverseStorage.Add(body.records[i].record.InternalName, body.records[i].InternalName); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(locktypeRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // locktype

    public class materialdbc {
        public DBCHeader header;
        public materialBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM material", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Flags, FoleySound, SheathSound, UnSeathSound FROM material ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new materialMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(materialRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.FoleySound = reader.GetInt32("FoleySound");
                    body.records[i].record.SheathSound = reader.GetInt32("SheathSound");
                    body.records[i].record.UnSeathSound = reader.GetInt32("UnSeathSound");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(materialRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // material

    public class moviefiledatadbc {
        public DBCHeader header;
        public moviefiledataBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM moviefiledata", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Resolution FROM moviefiledata ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new moviefiledataMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(moviefiledataRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Resolution = reader.GetInt32("Resolution");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(moviefiledataRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // moviefiledata

    public class movievariationdbc {
        public DBCHeader header;
        public movievariationBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM movievariation", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Movie, FileData FROM movievariation ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new movievariationMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(movievariationRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Movie = reader.GetInt32("Movie");
                    body.records[i].record.FileData = reader.GetInt32("FileData");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(movievariationRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // movievariation

    public class namegendbc {
        public DBCHeader header;
        public namegenBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM namegen", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Race, Gender FROM namegen ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new namegenMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(namegenRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.Race = reader.GetInt32("Race");
                    body.records[i].record.Gender = reader.GetInt32("Gender");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(namegenRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // namegen

    public class namesprofanitydbc {
        public DBCHeader header;
        public namesprofanityBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM namesprofanity", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Pattern, Language FROM namesprofanity ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new namesprofanityMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(namesprofanityRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Pattern = reader.GetString("Pattern");
                    body.records[i].record.Language = reader.GetInt32("Language");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Pattern
                    if (body.records[i].Pattern.Length == 0)
                        body.records[i].record.Pattern = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Pattern)) body.records[i].record.Pattern = offsetStorage[body.records[i].Pattern];
                        else {
                            body.records[i].record.Pattern = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Pattern) + 1;
                            offsetStorage.Add(body.records[i].Pattern, body.records[i].record.Pattern);
                            reverseStorage.Add(body.records[i].record.Pattern, body.records[i].Pattern); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(namesprofanityRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // namesprofanity

    public class namesreserveddbc {
        public DBCHeader header;
        public namesreservedBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM namesreserved", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Pattern, Language FROM namesreserved ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new namesreservedMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(namesreservedRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Pattern = reader.GetString("Pattern");
                    body.records[i].record.Language = reader.GetInt32("Language");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Pattern
                    if (body.records[i].Pattern.Length == 0)
                        body.records[i].record.Pattern = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Pattern)) body.records[i].record.Pattern = offsetStorage[body.records[i].Pattern];
                        else {
                            body.records[i].record.Pattern = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Pattern) + 1;
                            offsetStorage.Add(body.records[i].Pattern, body.records[i].record.Pattern);
                            reverseStorage.Add(body.records[i].record.Pattern, body.records[i].Pattern); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(namesreservedRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // namesreserved

    public class npcsoundsdbc {
        public DBCHeader header;
        public npcsoundsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM npcsounds", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Sound1, Sound2, Sound3, Sound4 FROM npcsounds ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new npcsoundsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(npcsoundsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Sound1 = reader.GetInt32("Sound1");
                    body.records[i].record.Sound2 = reader.GetInt32("Sound2");
                    body.records[i].record.Sound3 = reader.GetInt32("Sound3");
                    body.records[i].record.Sound4 = reader.GetInt32("Sound4");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(npcsoundsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // npcsounds

    public class objecteffectdbc {
        public DBCHeader header;
        public objecteffectBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM objecteffect", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, `Group`, TriggerType, EventType, EffectRecType, EffectRecId, Attachment, OffsetX, OffsetY, OffsetZ, ModifierId FROM objecteffect ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new objecteffectMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 12;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(objecteffectRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.Group = reader.GetInt32("Group");
                    body.records[i].record.TriggerType = reader.GetInt32("TriggerType");
                    body.records[i].record.EventType = reader.GetInt32("EventType");
                    body.records[i].record.EffectRecType = reader.GetInt32("EffectRecType");
                    body.records[i].record.EffectRecId = reader.GetInt32("EffectRecId");
                    body.records[i].record.Attachment = reader.GetInt32("Attachment");
                    body.records[i].record.OffsetX = reader.GetFloat("OffsetX");
                    body.records[i].record.OffsetY = reader.GetFloat("OffsetY");
                    body.records[i].record.OffsetZ = reader.GetFloat("OffsetZ");
                    body.records[i].record.ModifierId = reader.GetInt32("ModifierId");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(objecteffectRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // objecteffect

    public class objecteffectgroupdbc {
        public DBCHeader header;
        public objecteffectgroupBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM objecteffectgroup", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name FROM objecteffectgroup ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new objecteffectgroupMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(objecteffectgroupRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(objecteffectgroupRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // objecteffectgroup

    public class objecteffectmodifierdbc {
        public DBCHeader header;
        public objecteffectmodifierBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM objecteffectmodifier", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, InputType, MapType, OutputType, Param1, Param2, Param3, Param4 FROM objecteffectmodifier ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new objecteffectmodifierMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(objecteffectmodifierRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.InputType = reader.GetInt32("InputType");
                    body.records[i].record.MapType = reader.GetInt32("MapType");
                    body.records[i].record.OutputType = reader.GetInt32("OutputType");
                    body.records[i].record.Param1 = reader.GetFloat("Param1");
                    body.records[i].record.Param2 = reader.GetFloat("Param2");
                    body.records[i].record.Param3 = reader.GetFloat("Param3");
                    body.records[i].record.Param4 = reader.GetFloat("Param4");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(objecteffectmodifierRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // objecteffectmodifier

    public class objecteffectpackagedbc {
        public DBCHeader header;
        public objecteffectpackageBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM objecteffectpackage", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name FROM objecteffectpackage ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new objecteffectpackageMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(objecteffectpackageRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(objecteffectpackageRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // objecteffectpackage

    public class objecteffectpackageelemdbc {
        public DBCHeader header;
        public objecteffectpackageelemBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM objecteffectpackageelem", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, PackageId, GroupId, StateType FROM objecteffectpackageelem ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new objecteffectpackageelemMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(objecteffectpackageelemRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.PackageId = reader.GetInt32("PackageId");
                    body.records[i].record.GroupId = reader.GetInt32("GroupId");
                    body.records[i].record.StateType = reader.GetInt32("StateType");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(objecteffectpackageelemRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // objecteffectpackageelem

    public class packagedbc {
        public DBCHeader header;
        public packageBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM package", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Icon, Price, Description, Description_loc2 FROM package ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new packageMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 20;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(packageRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Icon = reader.GetString("Icon");
                    body.records[i].record.Price = reader.GetInt32("Price");

                    body.records[i].Description = new string[17];
                    body.records[i].record.Description = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Description[loc] = "";
                    body.records[i].Description[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Description_loc2" : "Description");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Description
                        if (body.records[i].Description[j].Length == 0)
                            body.records[i].record.Description[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Description[j])) body.records[i].record.Description[j] = offsetStorage[body.records[i].Description[j]];
                            else {
                                body.records[i].record.Description[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Description[j]) + 1;
                                offsetStorage.Add(body.records[i].Description[j], body.records[i].record.Description[j]);
                                reverseStorage.Add(body.records[i].record.Description[j], body.records[i].Description[j]); } } }
                    // Icon
                    if (body.records[i].Icon.Length == 0)
                        body.records[i].record.Icon = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Icon)) body.records[i].record.Icon = offsetStorage[body.records[i].Icon];
                        else {
                            body.records[i].record.Icon = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Icon) + 1;
                            offsetStorage.Add(body.records[i].Icon, body.records[i].record.Icon);
                            reverseStorage.Add(body.records[i].record.Icon, body.records[i].Icon); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(packageRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // package

    public class pagetextmaterialdbc {
        public DBCHeader header;
        public pagetextmaterialBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM pagetextmaterial", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name FROM pagetextmaterial ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new pagetextmaterialMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(pagetextmaterialRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(pagetextmaterialRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // pagetextmaterial

    public class paperdollitemframedbc {
        public DBCHeader header;
        public paperdollitemframeBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM paperdollitemframe", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Slot, Icon, SlotId FROM paperdollitemframe ORDER BY Slot ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new paperdollitemframeMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(paperdollitemframeRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].Slot = reader.GetString("Slot");
                    body.records[i].Icon = reader.GetString("Icon");
                    body.records[i].record.SlotId = reader.GetInt32("SlotId");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Slot
                    if (body.records[i].Slot.Length == 0)
                        body.records[i].record.Slot = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Slot)) body.records[i].record.Slot = offsetStorage[body.records[i].Slot];
                        else {
                            body.records[i].record.Slot = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Slot) + 1;
                            offsetStorage.Add(body.records[i].Slot, body.records[i].record.Slot);
                            reverseStorage.Add(body.records[i].record.Slot, body.records[i].Slot); } }
                    // Icon
                    if (body.records[i].Icon.Length == 0)
                        body.records[i].record.Icon = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Icon)) body.records[i].record.Icon = offsetStorage[body.records[i].Icon];
                        else {
                            body.records[i].record.Icon = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Icon) + 1;
                            offsetStorage.Add(body.records[i].Icon, body.records[i].record.Icon);
                            reverseStorage.Add(body.records[i].record.Icon, body.records[i].Icon); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(paperdollitemframeRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // paperdollitemframe

    public class particlecolordbc {
        public DBCHeader header;
        public particlecolorBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM particlecolor", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, StartColorRed1, StartColorGreen1, StartColorBlue1, StartColorUnused1, StartColorRed2, StartColorGreen2, StartColorBlue2, StartColorUnused2, StartColorRed3, StartColorGreen3, StartColorBlue3, StartColorUnused3, MidColorRed1, MidColorGreen1, MidColorBlue1, MidColorUnused1, MidColorRed2, MidColorGreen2, MidColorBlue2, MidColorUnused2, MidColorRed3, MidColorGreen3, MidColorBlue3, MidColorUnused3, EndColorRed1, EndColorGreen1, EndColorBlue1, EndColorUnused1, EndColorRed2, EndColorGreen2, EndColorBlue2, EndColorUnused2, EndColorRed3, EndColorGreen3, EndColorBlue3, EndColorUnused3 FROM particlecolor ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new particlecolorMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 10;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(particlecolorRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.StartColorRed1 = reader.GetByte("StartColorRed1");
                    body.records[i].record.StartColorGreen1 = reader.GetByte("StartColorGreen1");
                    body.records[i].record.StartColorBlue1 = reader.GetByte("StartColorBlue1");
                    body.records[i].record.StartColorUnused1 = reader.GetByte("StartColorUnused1");
                    body.records[i].record.StartColorRed2 = reader.GetByte("StartColorRed2");
                    body.records[i].record.StartColorGreen2 = reader.GetByte("StartColorGreen2");
                    body.records[i].record.StartColorBlue2 = reader.GetByte("StartColorBlue2");
                    body.records[i].record.StartColorUnused2 = reader.GetByte("StartColorUnused2");
                    body.records[i].record.StartColorRed3 = reader.GetByte("StartColorRed3");
                    body.records[i].record.StartColorGreen3 = reader.GetByte("StartColorGreen3");
                    body.records[i].record.StartColorBlue3 = reader.GetByte("StartColorBlue3");
                    body.records[i].record.StartColorUnused3 = reader.GetByte("StartColorUnused3");
                    body.records[i].record.MidColorRed1 = reader.GetByte("MidColorRed1");
                    body.records[i].record.MidColorGreen1 = reader.GetByte("MidColorGreen1");
                    body.records[i].record.MidColorBlue1 = reader.GetByte("MidColorBlue1");
                    body.records[i].record.MidColorUnused1 = reader.GetByte("MidColorUnused1");
                    body.records[i].record.MidColorRed2 = reader.GetByte("MidColorRed2");
                    body.records[i].record.MidColorGreen2 = reader.GetByte("MidColorGreen2");
                    body.records[i].record.MidColorBlue2 = reader.GetByte("MidColorBlue2");
                    body.records[i].record.MidColorUnused2 = reader.GetByte("MidColorUnused2");
                    body.records[i].record.MidColorRed3 = reader.GetByte("MidColorRed3");
                    body.records[i].record.MidColorGreen3 = reader.GetByte("MidColorGreen3");
                    body.records[i].record.MidColorBlue3 = reader.GetByte("MidColorBlue3");
                    body.records[i].record.MidColorUnused3 = reader.GetByte("MidColorUnused3");
                    body.records[i].record.EndColorRed1 = reader.GetByte("EndColorRed1");
                    body.records[i].record.EndColorGreen1 = reader.GetByte("EndColorGreen1");
                    body.records[i].record.EndColorBlue1 = reader.GetByte("EndColorBlue1");
                    body.records[i].record.EndColorUnused1 = reader.GetByte("EndColorUnused1");
                    body.records[i].record.EndColorRed2 = reader.GetByte("EndColorRed2");
                    body.records[i].record.EndColorGreen2 = reader.GetByte("EndColorGreen2");
                    body.records[i].record.EndColorBlue2 = reader.GetByte("EndColorBlue2");
                    body.records[i].record.EndColorUnused2 = reader.GetByte("EndColorUnused2");
                    body.records[i].record.EndColorRed3 = reader.GetByte("EndColorRed3");
                    body.records[i].record.EndColorGreen3 = reader.GetByte("EndColorGreen3");
                    body.records[i].record.EndColorBlue3 = reader.GetByte("EndColorBlue3");
                    body.records[i].record.EndColorUnused3 = reader.GetByte("EndColorUnused3");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(particlecolorRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // particlecolor

    public class petitiontypedbc {
        public DBCHeader header;
        public petitiontypeBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM petitiontype", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Unk FROM petitiontype ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new petitiontypeMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(petitiontypeRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.Unk = reader.GetInt32("Unk");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(petitiontypeRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // petitiontype

    public class petpersonalitydbc {
        public DBCHeader header;
        public petpersonalityBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM petpersonality", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, UnhappyLevel, ContentLevel, HappyLevel, UnhappyDamageModifier, ContentDamageModifier, HappyDamageModifier FROM petpersonality ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new petpersonalityMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 24;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(petpersonalityRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.UnhappyLevel = reader.GetInt32("UnhappyLevel");
                    body.records[i].record.ContentLevel = reader.GetInt32("ContentLevel");
                    body.records[i].record.HappyLevel = reader.GetInt32("HappyLevel");
                    body.records[i].record.UnhappyDamageModifier = reader.GetFloat("UnhappyDamageModifier");
                    body.records[i].record.ContentDamageModifier = reader.GetFloat("ContentDamageModifier");
                    body.records[i].record.HappyDamageModifier = reader.GetFloat("HappyDamageModifier");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(petpersonalityRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // petpersonality

    public class questinfodbc {
        public DBCHeader header;
        public questinfoBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM questinfo", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2 FROM questinfo ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new questinfoMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(questinfoRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(questinfoRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // questinfo

    public class resistancesdbc {
        public DBCHeader header;
        public resistancesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM resistances", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Flags, FizzleSound, Name, Name_loc2 FROM resistances ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new resistancesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 20;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(resistancesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.FizzleSound = reader.GetInt32("FizzleSound");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(resistancesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // resistances

    public class screeneffectdbc {
        public DBCHeader header;
        public screeneffectBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM screeneffect", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Type, Color_R, Color_G, Color_B, Color_A, Edge, BlackWhite, Unk, LightParams, SoundAmbience, ZoneMusic FROM screeneffect ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new screeneffectMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 10;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(screeneffectRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.Type = reader.GetInt32("Type");
                    byte[] color = { reader.GetByte("Color_R"), reader.GetByte("Color_G"), reader.GetByte("Color_B"), reader.GetByte("Color_A") };
                    body.records[i].record.Color = BitConverter.ToInt32(color, 0);
                    body.records[i].record.Edge = reader.GetInt32("Edge");
                    body.records[i].record.BlackWhite = reader.GetInt32("BlackWhite");
                    body.records[i].record.Unk = reader.GetInt32("Unk");
                    body.records[i].record.LightParams = reader.GetInt32("LightParams");
                    body.records[i].record.SoundAmbience = reader.GetInt32("SoundAmbience");
                    body.records[i].record.ZoneMusic = reader.GetInt32("ZoneMusic");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(screeneffectRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // screeneffect

    public class servermessagesdbc {
        public DBCHeader header;
        public servermessagesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM servermessages", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Message, Message_loc2 FROM servermessages ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new servermessagesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(servermessagesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Message = new string[17];
                    body.records[i].record.Message = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Message[loc] = "";
                    body.records[i].Message[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Message_loc2" : "Message");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Message
                        if (body.records[i].Message[j].Length == 0)
                            body.records[i].record.Message[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Message[j])) body.records[i].record.Message[j] = offsetStorage[body.records[i].Message[j]];
                            else {
                                body.records[i].record.Message[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Message[j]) + 1;
                                offsetStorage.Add(body.records[i].Message[j], body.records[i].record.Message[j]);
                                reverseStorage.Add(body.records[i].record.Message[j], body.records[i].Message[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(servermessagesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // servermessages

    public class sheathesoundlookupsdbc {
        public DBCHeader header;
        public sheathesoundlookupsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM sheathesoundlookups", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, UnkItemClass, UnkItemSubClass, UnkType, UnkIsWeapon, SheathSound, UnSheathSound FROM sheathesoundlookups ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new sheathesoundlookupsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 7;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(sheathesoundlookupsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.UnkItemClass = reader.GetInt32("UnkItemClass");
                    body.records[i].record.UnkItemSubClass = reader.GetInt32("UnkItemSubClass");
                    body.records[i].record.UnkType = reader.GetInt32("UnkType");
                    body.records[i].record.UnkIsWeapon = reader.GetInt32("UnkIsWeapon");
                    body.records[i].record.SheathSound = reader.GetInt32("SheathSound");
                    body.records[i].record.UnSheathSound = reader.GetInt32("UnSheathSound");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(sheathesoundlookupsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // sheathesoundlookups

    public class skillcostsdatadbc {
        public DBCHeader header;
        public skillcostsdataBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM skillcostsdata", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SkillCostsId, Cost1, Cost2, Cost3 FROM skillcostsdata ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new skillcostsdataMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(skillcostsdataRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SkillCostsId = reader.GetInt32("SkillCostsId");
                    body.records[i].record.Cost1 = reader.GetInt32("Cost1");
                    body.records[i].record.Cost2 = reader.GetInt32("Cost2");
                    body.records[i].record.Cost3 = reader.GetInt32("Cost3");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(skillcostsdataRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // skillcostsdata

    public class skilllinecategorydbc {
        public DBCHeader header;
        public skilllinecategoryBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM skilllinecategory", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, DisplayOrder FROM skilllinecategory ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new skilllinecategoryMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 19;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(skilllinecategoryRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.DisplayOrder = reader.GetInt32("DisplayOrder");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(skilllinecategoryRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // skilllinecategory

    public class soundambiencedbc {
        public DBCHeader header;
        public soundambienceBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM soundambience", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SoundDay, SoundNight FROM soundambience ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new soundambienceMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(soundambienceRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SoundDay = reader.GetInt32("SoundDay");
                    body.records[i].record.SoundNight = reader.GetInt32("SoundNight");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(soundambienceRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // soundambience

    public class soundemittersdbc {
        public DBCHeader header;
        public soundemittersBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM soundemitters", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, PositionX, PositionY, PositionZ, DirectionX, DirectionY, DirectionZ, Sound, Map, Name FROM soundemitters ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new soundemittersMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 10;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(soundemittersRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.PositionX = reader.GetFloat("PositionX");
                    body.records[i].record.PositionY = reader.GetFloat("PositionY");
                    body.records[i].record.PositionZ = reader.GetFloat("PositionZ");
                    body.records[i].record.DirectionX = reader.GetFloat("DirectionX");
                    body.records[i].record.DirectionY = reader.GetFloat("DirectionY");
                    body.records[i].record.DirectionZ = reader.GetFloat("DirectionZ");
                    body.records[i].record.Sound = reader.GetInt32("Sound");
                    body.records[i].record.Map = reader.GetInt32("Map");
                    body.records[i].Name = reader.GetString("Name");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(soundemittersRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // soundemitters

    public class soundentriesadvanceddbc {
        public DBCHeader header;
        public soundentriesadvancedBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM soundentriesadvanced", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Sound, InnerRadius, TimeA, TimeB, TimeC, TimeD, RandomOffsetRange, `Usage`, IntervalMin, IntervalMax, VolumeSliderCategory, DuckToSFX, DuckToMusic, DuckToAmbience, InnerRadiusOfInfluence, OuterRadiusOfInfluence, TimeToDuck, TimeToUnduck, InsideAngle, OutsideAngle, OutsideVolume, OuterRadius2D, Name FROM soundentriesadvanced ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new soundentriesadvancedMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 24;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(soundentriesadvancedRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Sound = reader.GetInt32("Sound");
                    body.records[i].record.InnerRadius = reader.GetFloat("InnerRadius");
                    body.records[i].record.TimeA = reader.GetInt32("TimeA");
                    body.records[i].record.TimeB = reader.GetInt32("TimeB");
                    body.records[i].record.TimeC = reader.GetInt32("TimeC");
                    body.records[i].record.TimeD = reader.GetInt32("TimeD");
                    body.records[i].record.RandomOffsetRange = reader.GetInt32("RandomOffsetRange");
                    body.records[i].record.Usage = reader.GetInt32("Usage");
                    body.records[i].record.IntervalMin = reader.GetInt32("IntervalMin");
                    body.records[i].record.IntervalMax = reader.GetInt32("IntervalMax");
                    body.records[i].record.VolumeSliderCategory = reader.GetInt32("VolumeSliderCategory");
                    body.records[i].record.DuckToSFX = reader.GetFloat("DuckToSFX");
                    body.records[i].record.DuckToMusic = reader.GetFloat("DuckToMusic");
                    body.records[i].record.DuckToAmbience = reader.GetFloat("DuckToAmbience");
                    body.records[i].record.InnerRadiusOfInfluence = reader.GetFloat("InnerRadiusOfInfluence");
                    body.records[i].record.OuterRadiusOfInfluence = reader.GetFloat("OuterRadiusOfInfluence");
                    body.records[i].record.TimeToDuck = reader.GetInt32("TimeToDuck");
                    body.records[i].record.TimeToUnduck = reader.GetInt32("TimeToUnduck");
                    body.records[i].record.InsideAngle = reader.GetFloat("InsideAngle");
                    body.records[i].record.OutsideAngle = reader.GetFloat("OutsideAngle");
                    body.records[i].record.OutsideVolume = reader.GetFloat("OutsideVolume");
                    body.records[i].record.OuterRadius2D = reader.GetFloat("OuterRadius2D");
                    body.records[i].Name = reader.GetString("Name");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(soundentriesadvancedRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // soundentriesadvanced

    public class soundfilterdbc {
        public DBCHeader header;
        public soundfilterBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM soundfilter", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name FROM soundfilter ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new soundfilterMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(soundfilterRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(soundfilterRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // soundfilter

    public class soundfilterelemdbc {
        public DBCHeader header;
        public soundfilterelemBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM soundfilterelem", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SoundFilter, OrderIndex, FilterType, True, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6, Parameter7, Parameter8 FROM soundfilterelem ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new soundfilterelemMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 13;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(soundfilterelemRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SoundFilter = reader.GetInt32("SoundFilter");
                    body.records[i].record.OrderIndex = reader.GetInt32("OrderIndex");
                    body.records[i].record.FilterType = reader.GetInt32("FilterType");
                    body.records[i].record.True = reader.GetInt32("True");
                    body.records[i].record.Parameter1 = reader.GetFloat("Parameter1");
                    body.records[i].record.Parameter2 = reader.GetFloat("Parameter2");
                    body.records[i].record.Parameter3 = reader.GetFloat("Parameter3");
                    body.records[i].record.Parameter4 = reader.GetFloat("Parameter4");
                    body.records[i].record.Parameter5 = reader.GetFloat("Parameter5");
                    body.records[i].record.Parameter6 = reader.GetFloat("Parameter6");
                    body.records[i].record.Parameter7 = reader.GetFloat("Parameter7");
                    body.records[i].record.Parameter8 = reader.GetFloat("Parameter8");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(soundfilterelemRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // soundfilterelem

    public class soundproviderpreferencesdbc {
        public DBCHeader header;
        public soundproviderpreferencesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM soundproviderpreferences", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Description, Flags, EAXEnvSelection, EAXDecayTime, EAX2EnvSize, EAX2EnvDiffusion, EAX2Room, EAX2RoomHF, EAX2DecayHFRatio, EAX2Reflections, EAX2ReflectionsDelay, EAX2Reverb, EAX2ReverbDelay, EAX2RoomRolloff, EAX2AirAbsorption, EAX3RoomLF, EAX3DecayLFRatio, EAX3EchoTime, EAX3EchoDepth, EAX3ModulationTime, EAX3ModulationDepth, EAX3HFReference, EAX3LFReference FROM soundproviderpreferences ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new soundproviderpreferencesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 24;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(soundproviderpreferencesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Description = reader.GetString("Description");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.EAXEnvSelection = reader.GetInt32("EAXEnvSelection");
                    body.records[i].record.EAXDecayTime = reader.GetFloat("EAXDecayTime");
                    body.records[i].record.EAX2EnvSize = reader.GetFloat("EAX2EnvSize");
                    body.records[i].record.EAX2EnvDiffusion = reader.GetFloat("EAX2EnvDiffusion");
                    body.records[i].record.EAX2Room = reader.GetInt32("EAX2Room");
                    body.records[i].record.EAX2RoomHF = reader.GetInt32("EAX2RoomHF");
                    body.records[i].record.EAX2DecayHFRatio = reader.GetFloat("EAX2DecayHFRatio");
                    body.records[i].record.EAX2Reflections = reader.GetInt32("EAX2Reflections");
                    body.records[i].record.EAX2ReflectionsDelay = reader.GetFloat("EAX2ReflectionsDelay");
                    body.records[i].record.EAX2Reverb = reader.GetInt32("EAX2Reverb");
                    body.records[i].record.EAX2ReverbDelay = reader.GetFloat("EAX2ReverbDelay");
                    body.records[i].record.EAX2RoomRolloff = reader.GetFloat("EAX2RoomRolloff");
                    body.records[i].record.EAX2AirAbsorption = reader.GetInt32("EAX2AirAbsorption");
                    body.records[i].record.EAX3RoomLF = reader.GetInt32("EAX3RoomLF");
                    body.records[i].record.EAX3DecayLFRatio = reader.GetFloat("EAX3DecayLFRatio");
                    body.records[i].record.EAX3EchoTime = reader.GetFloat("EAX3EchoTime");
                    body.records[i].record.EAX3EchoDepth = reader.GetFloat("EAX3EchoDepth");
                    body.records[i].record.EAX3ModulationTime = reader.GetFloat("EAX3ModulationTime");
                    body.records[i].record.EAX3ModulationDepth = reader.GetFloat("EAX3ModulationDepth");
                    body.records[i].record.EAX3HFReference = reader.GetFloat("EAX3HFReference");
                    body.records[i].record.EAX3LFReference = reader.GetFloat("EAX3LFReference");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Description
                    if (body.records[i].Description.Length == 0)
                        body.records[i].record.Description = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Description)) body.records[i].record.Description = offsetStorage[body.records[i].Description];
                        else {
                            body.records[i].record.Description = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Description) + 1;
                            offsetStorage.Add(body.records[i].Description, body.records[i].record.Description);
                            reverseStorage.Add(body.records[i].record.Description, body.records[i].Description); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(soundproviderpreferencesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // soundproviderpreferences

    public class soundsamplepreferencesdbc {
        public DBCHeader header;
        public soundsamplepreferencesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM soundsamplepreferences", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Unk1, Unk2, Unk3, Unk4, Unk5, Unk6, Unk7, Unk8, Unk9, Unk10, Unk11, Unk12, Unk13, Unk14, Unk15, Unk16 FROM soundsamplepreferences ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new soundsamplepreferencesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 17;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(soundsamplepreferencesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Unk1 = reader.GetInt32("Unk1");
                    body.records[i].record.Unk2 = reader.GetInt32("Unk2");
                    body.records[i].record.Unk3 = reader.GetInt32("Unk3");
                    body.records[i].record.Unk4 = reader.GetInt32("Unk4");
                    body.records[i].record.Unk5 = reader.GetInt32("Unk5");
                    body.records[i].record.Unk6 = reader.GetFloat("Unk6");
                    body.records[i].record.Unk7 = reader.GetInt32("Unk7");
                    body.records[i].record.Unk8 = reader.GetFloat("Unk8");
                    body.records[i].record.Unk9 = reader.GetFloat("Unk9");
                    body.records[i].record.Unk10 = reader.GetInt32("Unk10");
                    body.records[i].record.Unk11 = reader.GetFloat("Unk11");
                    body.records[i].record.Unk12 = reader.GetInt32("Unk12");
                    body.records[i].record.Unk13 = reader.GetFloat("Unk13");
                    body.records[i].record.Unk14 = reader.GetFloat("Unk14");
                    body.records[i].record.Unk15 = reader.GetFloat("Unk15");
                    body.records[i].record.Unk16 = reader.GetInt32("Unk16");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(soundsamplepreferencesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // soundsamplepreferences

    public class soundwatertypedbc {
        public DBCHeader header;
        public soundwatertypeBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM soundwatertype", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, LiquidType, FluidSpeed, Sound FROM soundwatertype ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new soundwatertypeMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(soundwatertypeRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.LiquidType = reader.GetInt32("LiquidType");
                    body.records[i].record.FluidSpeed = reader.GetInt32("FluidSpeed");
                    body.records[i].record.Sound = reader.GetInt32("Sound");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(soundwatertypeRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // soundwatertype

    public class spammessagesdbc {
        public DBCHeader header;
        public spammessagesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spammessages", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, RegEx FROM spammessages ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spammessagesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spammessagesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].RegEx = reader.GetString("RegEx");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // RegEx
                    if (body.records[i].RegEx.Length == 0)
                        body.records[i].record.RegEx = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].RegEx)) body.records[i].record.RegEx = offsetStorage[body.records[i].RegEx];
                        else {
                            body.records[i].record.RegEx = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].RegEx) + 1;
                            offsetStorage.Add(body.records[i].RegEx, body.records[i].record.RegEx);
                            reverseStorage.Add(body.records[i].record.RegEx, body.records[i].RegEx); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spammessagesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spammessages

    public class spellchaineffectsdbc {
        public DBCHeader header;
        public spellchaineffectsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellchaineffects", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, AvgSegLen, Width, NoiseScale, TexCoordScale, SegDuration, SegDelay, Texture, Flags, JointCount, JointOffsetRadius, JointsPerMinorJoint, MinorJointsPerMajorJoint, MinorJointScale, MajorJointScale, JointMoveSpeed, JointSmoothness, MinDurationBetweenJointJumps, MaxDurationBetweenJointJumps, WaveHeight, WaveFreq, WaveSpeed, MinWaveAngle, MaxWaveAngle, MinWaveSpin, MaxWaveSpin, ArcHeight, MinArcAngle, MaxArcAngle, MinArcSpin, MaxArcSpin, DelayBetweenEffects, MinFlickerOnDuration, MaxFlickerOnDuration, MinFlickerOffDuration, MaxFlickerOffDuration, PulseSpeed, PulseOnLength, PulseFadeLength, Alpha, Red, Green, Blue, BlendMode, Combo, RenderLayer, TextureLength, WavePhase FROM spellchaineffects ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spellchaineffectsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 48;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellchaineffectsRecord));
 
                UInt32 i = 0;
                while (reader.Read())
                { //if (!reader.HasRows) return false;
                    body.records[i].record.ID = reader.GetInt32("ID");
                    body.records[i].record.AvgSegLen = reader.GetFloat("AvgSegLen");
                    body.records[i].record.Width = reader.GetFloat("Width");
                    body.records[i].record.NoiseScale = reader.GetFloat("NoiseScale");
                    body.records[i].record.TexCoordScale = reader.GetFloat("TexCoordScale");
                    body.records[i].record.SegDuration = reader.GetInt32("SegDuration");
                    body.records[i].record.SegDelay = reader.GetInt32("SegDelay");
                    body.records[i].Texture = reader.GetString("Texture");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.JointCount = reader.GetInt32("JointCount");
                    body.records[i].record.JointOffsetRadius = reader.GetFloat("JointOffsetRadius");
                    body.records[i].record.JointsPerMinorJoint = reader.GetInt32("JointsPerMinorJoint");
                    body.records[i].record.MinorJointsPerMajorJoint = reader.GetInt32("MinorJointsPerMajorJoint");
                    body.records[i].record.MinorJointScale = reader.GetFloat("MinorJointScale");
                    body.records[i].record.MajorJointScale = reader.GetFloat("MajorJointScale");
                    body.records[i].record.JointMoveSpeed = reader.GetFloat("JointMoveSpeed");
                    body.records[i].record.JointSmoothness = reader.GetFloat("JointSmoothness");
                    body.records[i].record.MinDurationBetweenJointJumps = reader.GetFloat("MinDurationBetweenJointJumps");
                    body.records[i].record.MaxDurationBetweenJointJumps = reader.GetFloat("MaxDurationBetweenJointJumps");
                    body.records[i].record.WaveHeight = reader.GetFloat("WaveHeight");
                    body.records[i].record.WaveFreq = reader.GetFloat("WaveFreq");
                    body.records[i].record.WaveSpeed = reader.GetFloat("WaveSpeed");
                    body.records[i].record.MinWaveAngle = reader.GetFloat("MinWaveAngle");
                    body.records[i].record.MaxWaveAngle = reader.GetFloat("MaxWaveAngle");
                    body.records[i].record.MinWaveSpin = reader.GetFloat("MinWaveSpin");
                    body.records[i].record.MaxWaveSpin = reader.GetFloat("MaxWaveSpin");
                    body.records[i].record.ArcHeight = reader.GetFloat("ArcHeight");
                    body.records[i].record.MinArcAngle = reader.GetFloat("MinArcAngle");
                    body.records[i].record.MaxArcAngle = reader.GetFloat("MaxArcAngle");
                    body.records[i].record.MinArcSpin = reader.GetFloat("MinArcSpin");
                    body.records[i].record.MaxArcSpin = reader.GetFloat("MaxArcSpin");
                    body.records[i].record.DelayBetweenEffects = reader.GetFloat("DelayBetweenEffects");
                    body.records[i].record.MinFlickerOnDuration = reader.GetFloat("MinFlickerOnDuration");
                    body.records[i].record.MaxFlickerOnDuration = reader.GetFloat("MaxFlickerOnDuration");
                    body.records[i].record.MinFlickerOffDuration = reader.GetFloat("MinFlickerOffDuration");
                    body.records[i].record.MaxFlickerOffDuration = reader.GetFloat("MaxFlickerOffDuration");
                    body.records[i].record.PulseSpeed = reader.GetFloat("PulseSpeed");
                    body.records[i].record.PulseOnLength = reader.GetFloat("PulseOnLength");
                    body.records[i].record.PulseFadeLength = reader.GetFloat("PulseFadeLength");
                    body.records[i].record.Alpha = reader.GetByte("Alpha");
                    body.records[i].record.Red = reader.GetByte("Red");
                    body.records[i].record.Green = reader.GetByte("Green");
                    body.records[i].record.Blue = reader.GetByte("Blue");
                    body.records[i].record.BlendMode = reader.GetByte("BlendMode");
                    body.records[i].record.Combo = reader.GetInt32("Combo");
                    body.records[i].record.RenderLayer = reader.GetInt32("RenderLayer");
                    body.records[i].record.TextureLength = reader.GetFloat("TextureLength");
                    body.records[i].record.WavePhase = reader.GetFloat("WavePhase");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Texture
                    if (body.records[i].Texture.Length == 0)
                        body.records[i].record.Texture = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Texture)) body.records[i].record.Texture = offsetStorage[body.records[i].Texture];
                        else {
                            body.records[i].record.Texture = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Texture) + 1;
                            offsetStorage.Add(body.records[i].Texture, body.records[i].record.Texture);
                            reverseStorage.Add(body.records[i].record.Texture, body.records[i].Texture); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spellchaineffectsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spellchaineffects

    public class spelldescriptionvariablesdbc {
        public DBCHeader header;
        public spelldescriptionvariablesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spelldescriptionvariables", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Text FROM spelldescriptionvariables ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spelldescriptionvariablesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spelldescriptionvariablesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Text = reader.GetString("Text");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Text
                    if (body.records[i].Text.Length == 0)
                        body.records[i].record.Text = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Text)) body.records[i].record.Text = offsetStorage[body.records[i].Text];
                        else {
                            body.records[i].record.Text = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Text) + 1;
                            offsetStorage.Add(body.records[i].Text, body.records[i].record.Text);
                            reverseStorage.Add(body.records[i].record.Text, body.records[i].Text); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spelldescriptionvariablesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spelldescriptionvariables

    public class spelldispeltypedbc {
        public DBCHeader header;
        public spelldispeltypeBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spelldispeltype", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, Mask, ImmunityPossible, InternalName FROM spelldispeltype ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spelldispeltypeMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 21;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spelldispeltypeRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Mask = reader.GetInt32("Mask");
                    body.records[i].record.ImmunityPossible = reader.GetInt32("ImmunityPossible");
                    body.records[i].InternalName = reader.GetString("InternalName");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
                    // InternalName
                    if (body.records[i].InternalName.Length == 0)
                        body.records[i].record.InternalName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].InternalName)) body.records[i].record.InternalName = offsetStorage[body.records[i].InternalName];
                        else {
                            body.records[i].record.InternalName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].InternalName) + 1;
                            offsetStorage.Add(body.records[i].InternalName, body.records[i].record.InternalName);
                            reverseStorage.Add(body.records[i].record.InternalName, body.records[i].InternalName); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spelldispeltypeRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spelldispeltype

    public class spelleffectcamerashakesdbc {
        public DBCHeader header;
        public spelleffectcamerashakesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spelleffectcamerashakes", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, CameraShakes1, CameraShakes2, CameraShakes3 FROM spelleffectcamerashakes ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spelleffectcamerashakesMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spelleffectcamerashakesRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.CameraShakes1 = reader.GetInt32("CameraShakes1");
                    body.records[i].record.CameraShakes2 = reader.GetInt32("CameraShakes2");
                    body.records[i].record.CameraShakes3 = reader.GetInt32("CameraShakes3");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spelleffectcamerashakesRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spelleffectcamerashakes

    public class spellicondbc {
        public DBCHeader header;
        public spelliconBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellicon", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name FROM spellicon ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spelliconMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spelliconRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spelliconRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spellicon

    public class spellmechanicdbc {
        public DBCHeader header;
        public spellmechanicBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellmechanic", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2 FROM spellmechanic ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spellmechanicMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellmechanicRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Name = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Name[loc] = "";
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Name
                        if (body.records[i].Name[j].Length == 0)
                            body.records[i].record.Name[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name[j])) body.records[i].record.Name[j] = offsetStorage[body.records[i].Name[j]];
                            else {
                                body.records[i].record.Name[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name[j]) + 1;
                                offsetStorage.Add(body.records[i].Name[j], body.records[i].record.Name[j]);
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spellmechanicRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spellmechanic

    public class spellmissiledbc {
        public DBCHeader header;
        public spellmissileBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellmissile", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Flags, DefaultPitchMin, DefaultPitchMax, DefaultSpeedMin, DefaultSpeedMax, RandomizeFacingMin, RandomizeFacingMax, RandomizePitchMin, RandomizePitchMax, RandomizeSpeedMin, RandomizeSpeedMax, Gravity, MaxDuration, CollisionRadius FROM spellmissile ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spellmissileMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 15;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellmissileRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.DefaultPitchMin = reader.GetFloat("DefaultPitchMin");
                    body.records[i].record.DefaultPitchMax = reader.GetFloat("DefaultPitchMax");
                    body.records[i].record.DefaultSpeedMin = reader.GetFloat("DefaultSpeedMin");
                    body.records[i].record.DefaultSpeedMax = reader.GetFloat("DefaultSpeedMax");
                    body.records[i].record.RandomizeFacingMin = reader.GetFloat("RandomizeFacingMin");
                    body.records[i].record.RandomizeFacingMax = reader.GetFloat("RandomizeFacingMax");
                    body.records[i].record.RandomizePitchMin = reader.GetFloat("RandomizePitchMin");
                    body.records[i].record.RandomizePitchMax = reader.GetFloat("RandomizePitchMax");
                    body.records[i].record.RandomizeSpeedMin = reader.GetFloat("RandomizeSpeedMin");
                    body.records[i].record.RandomizeSpeedMax = reader.GetFloat("RandomizeSpeedMax");
                    body.records[i].record.Gravity = reader.GetFloat("Gravity");
                    body.records[i].record.MaxDuration = reader.GetFloat("MaxDuration");
                    body.records[i].record.CollisionRadius = reader.GetFloat("CollisionRadius");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spellmissileRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spellmissile

    public class spellmissilemotiondbc {
        public DBCHeader header;
        public spellmissilemotionBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellmissilemotion", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, ScriptBody, Flags, MissileCount FROM spellmissilemotion ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spellmissilemotionMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellmissilemotionRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].ScriptBody = reader.GetString("ScriptBody");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.MissileCount = reader.GetInt32("MissileCount");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } }
                    // ScriptBody
                    if (body.records[i].ScriptBody.Length == 0)
                        body.records[i].record.ScriptBody = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].ScriptBody)) body.records[i].record.ScriptBody = offsetStorage[body.records[i].ScriptBody];
                        else {
                            body.records[i].record.ScriptBody = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].ScriptBody) + 1;
                            offsetStorage.Add(body.records[i].ScriptBody, body.records[i].record.ScriptBody);
                            reverseStorage.Add(body.records[i].record.ScriptBody, body.records[i].ScriptBody); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spellmissilemotionRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spellmissilemotion

    public class spellvisualdbc {
        public DBCHeader header;
        public spellvisualBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellvisual", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, PrecastKit, CastKit, ImpactKit, StateKit, StateDoneKit, ChannelKit, HasMissile, MissileModel, MissilePathType, MissileDestinationAttachment, MissileSound, AnimEventSound, Flags, CasterImpactKit, TargetImpactKit, MissileAttachment, MissileFollowGroundHeight, MissileFollowGroundDropSpeed, MissileFollowGroundApproach, MissileFollowGroundFlags, MissileMotion, MissileTargetingKit, InstantAreaKit, ImpactAreaKit, PersistentAreaKit, MissileCastOffset1, MissileCastOffset2, MissileCastOffset3, MissileImpactOffset1, MissileImpactOffset2, MissileImpactOffset3 FROM spellvisual ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spellvisualMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 32;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellvisualRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.PrecastKit = reader.GetInt32("PrecastKit");
                    body.records[i].record.CastKit = reader.GetInt32("CastKit");
                    body.records[i].record.ImpactKit = reader.GetInt32("ImpactKit");
                    body.records[i].record.StateKit = reader.GetInt32("StateKit");
                    body.records[i].record.StateDoneKit = reader.GetInt32("StateDoneKit");
                    body.records[i].record.ChannelKit = reader.GetInt32("ChannelKit");
                    body.records[i].record.HasMissile = reader.GetInt32("HasMissile");
                    body.records[i].record.MissileModel = reader.GetInt32("MissileModel");
                    body.records[i].record.MissilePathType = reader.GetInt32("MissilePathType");
                    body.records[i].record.MissileDestinationAttachment = reader.GetInt32("MissileDestinationAttachment");
                    body.records[i].record.MissileSound = reader.GetInt32("MissileSound");
                    body.records[i].record.AnimEventSound = reader.GetInt32("AnimEventSound");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.CasterImpactKit = reader.GetInt32("CasterImpactKit");
                    body.records[i].record.TargetImpactKit = reader.GetInt32("TargetImpactKit");
                    body.records[i].record.MissileAttachment = reader.GetInt32("MissileAttachment");
                    body.records[i].record.MissileFollowGroundHeight = reader.GetInt32("MissileFollowGroundHeight");
                    body.records[i].record.MissileFollowGroundDropSpeed = reader.GetInt32("MissileFollowGroundDropSpeed");
                    body.records[i].record.MissileFollowGroundApproach = reader.GetInt32("MissileFollowGroundApproach");
                    body.records[i].record.MissileFollowGroundFlags = reader.GetInt32("MissileFollowGroundFlags");
                    body.records[i].record.MissileMotion = reader.GetInt32("MissileMotion");
                    body.records[i].record.MissileTargetingKit = reader.GetInt32("MissileTargetingKit");
                    body.records[i].record.InstantAreaKit = reader.GetInt32("InstantAreaKit");
                    body.records[i].record.ImpactAreaKit = reader.GetInt32("ImpactAreaKit");
                    body.records[i].record.PersistentAreaKit = reader.GetInt32("PersistentAreaKit");
                    body.records[i].record.MissileCastOffset1 = reader.GetFloat("MissileCastOffset1");
                    body.records[i].record.MissileCastOffset2 = reader.GetFloat("MissileCastOffset2");
                    body.records[i].record.MissileCastOffset3 = reader.GetFloat("MissileCastOffset3");
                    body.records[i].record.MissileImpactOffset1 = reader.GetFloat("MissileImpactOffset1");
                    body.records[i].record.MissileImpactOffset2 = reader.GetFloat("MissileImpactOffset2");
                    body.records[i].record.MissileImpactOffset3 = reader.GetFloat("MissileImpactOffset3");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spellvisualRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spellvisual

    public class spellvisualeffectnamedbc {
        public DBCHeader header;
        public spellvisualeffectnameBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellvisualeffectname", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Model, AreaEffectSize, Scale, MinAllowedScale, MaxAllowedScale FROM spellvisualeffectname ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spellvisualeffectnameMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 7;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellvisualeffectnameRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].Model = reader.GetString("Model");
                    body.records[i].record.AreaEffectSize = reader.GetFloat("AreaEffectSize");
                    body.records[i].record.Scale = reader.GetFloat("Scale");
                    body.records[i].record.MinAllowedScale = reader.GetFloat("MinAllowedScale");
                    body.records[i].record.MaxAllowedScale = reader.GetFloat("MaxAllowedScale");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } }
                    // Model
                    if (body.records[i].Model.Length == 0)
                        body.records[i].record.Model = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Model)) body.records[i].record.Model = offsetStorage[body.records[i].Model];
                        else {
                            body.records[i].record.Model = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Model) + 1;
                            offsetStorage.Add(body.records[i].Model, body.records[i].record.Model);
                            reverseStorage.Add(body.records[i].record.Model, body.records[i].Model); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spellvisualeffectnameRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spellvisualeffectname

    public class spellvisualkitdbc {
        public DBCHeader header;
        public spellvisualkitBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellvisualkit", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, StartAnimId, AnimId, AnimKitId, HeadEffect, ChestEffect, BaseEffect, LeftHandEffect, RightHandEffect, BreathEffect, LeftWeaponEffect, RightWeaponEffect, SpecialEffect, WorldEffect, Sound, Shake, CharProc, Unk1, Unk2, Unk3, Unk4, Unk51, Unk52, Unk53, Unk54, Unk61, Unk62, Unk63, Unk64, Unk71, Unk72, Unk73, Unk74, Unk8, Unk9, Unk10, Unk11, Unk12, Unk13, Unk14, Unk15, Unk16, Unk17, Unk18, Unk19, Unk20, Flags FROM spellvisualkit ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spellvisualkitMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 38;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellvisualkitRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.StartAnimId = reader.GetInt32("StartAnimId");
                    body.records[i].record.AnimId = reader.GetInt32("AnimId");
                    body.records[i].record.AnimKitId = reader.GetInt32("AnimKitId");
                    body.records[i].record.HeadEffect = reader.GetInt32("HeadEffect");
                    body.records[i].record.ChestEffect = reader.GetInt32("ChestEffect");
                    body.records[i].record.BaseEffect = reader.GetInt32("BaseEffect");
                    body.records[i].record.LeftHandEffect = reader.GetInt32("LeftHandEffect");
                    body.records[i].record.RightHandEffect = reader.GetInt32("RightHandEffect");
                    body.records[i].record.BreathEffect = reader.GetInt32("BreathEffect");
                    body.records[i].record.LeftWeaponEffect = reader.GetInt32("LeftWeaponEffect");
                    body.records[i].record.RightWeaponEffect = reader.GetInt32("RightWeaponEffect");
                    body.records[i].record.SpecialEffect = reader.GetInt32("SpecialEffect");
                    body.records[i].record.WorldEffect = reader.GetInt32("WorldEffect");
                    body.records[i].record.Sound = reader.GetInt32("Sound");
                    body.records[i].record.Shake = reader.GetInt32("Shake");
                    body.records[i].record.CharProc = reader.GetInt32("CharProc");
                    body.records[i].record.Unk1 = reader.GetInt32("Unk1");
                    body.records[i].record.Unk2 = reader.GetInt32("Unk2");
                    body.records[i].record.Unk3 = reader.GetInt32("Unk3");
                    body.records[i].record.Unk4 = reader.GetInt32("Unk4");
                    byte[] Unk5 = { reader.GetByte("Unk51"), reader.GetByte("Unk52"), reader.GetByte("Unk53"), reader.GetByte("Unk54") };
                    body.records[i].record.Unk5 = BitConverter.ToInt32(Unk5, 0);
                    byte[] Unk6 = { reader.GetByte("Unk61"), reader.GetByte("Unk62"), reader.GetByte("Unk63"), reader.GetByte("Unk64") };
                    body.records[i].record.Unk6 = BitConverter.ToInt32(Unk6, 0);
                    byte[] Unk7 = { reader.GetByte("Unk71"), reader.GetByte("Unk72"), reader.GetByte("Unk73"), reader.GetByte("Unk74") };
                    body.records[i].record.Unk7 = BitConverter.ToInt32(Unk7, 0);
                    body.records[i].record.Unk8 = reader.GetFloat("Unk8");
                    body.records[i].record.Unk9 = reader.GetFloat("Unk9");
                    body.records[i].record.Unk10 = reader.GetFloat("Unk10");
                    body.records[i].record.Unk11 = reader.GetFloat("Unk11");
                    body.records[i].record.Unk12 = reader.GetFloat("Unk12");
                    body.records[i].record.Unk13 = reader.GetFloat("Unk13");
                    body.records[i].record.Unk14 = reader.GetFloat("Unk14");
                    body.records[i].record.Unk15 = reader.GetFloat("Unk15");
                    body.records[i].record.Unk16 = reader.GetFloat("Unk16");
                    body.records[i].record.Unk17 = reader.GetFloat("Unk17");
                    body.records[i].record.Unk18 = reader.GetFloat("Unk18");
                    body.records[i].record.Unk19 = reader.GetFloat("Unk19");
                    body.records[i].record.Unk20 = reader.GetFloat("Unk20");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spellvisualkitRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spellvisualkit

    public class spellvisualkitareamodeldbc {
        public DBCHeader header;
        public spellvisualkitareamodelBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellvisualkitareamodel", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, EnumId FROM spellvisualkitareamodel ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spellvisualkitareamodelMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellvisualkitareamodelRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.EnumId = reader.GetInt32("EnumId");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spellvisualkitareamodelRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spellvisualkitareamodel

    public class spellvisualkitmodelattachdbc {
        public DBCHeader header;
        public spellvisualkitmodelattachBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellvisualkitmodelattach", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, ParentSpellVisualKit, VisualEffectName, AttachmentId, OffsetX, OffsetY, OffsetZ, Yaw, Pitch, Roll FROM spellvisualkitmodelattach ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spellvisualkitmodelattachMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 10;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellvisualkitmodelattachRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.ParentSpellVisualKit = reader.GetInt32("ParentSpellVisualKit");
                    body.records[i].record.VisualEffectName = reader.GetInt32("VisualEffectName");
                    body.records[i].record.AttachmentId = reader.GetInt32("AttachmentId");
                    body.records[i].record.OffsetX = reader.GetFloat("OffsetX");
                    body.records[i].record.OffsetY = reader.GetFloat("OffsetY");
                    body.records[i].record.OffsetZ = reader.GetFloat("OffsetZ");
                    body.records[i].record.Yaw = reader.GetFloat("Yaw");
                    body.records[i].record.Pitch = reader.GetFloat("Pitch");
                    body.records[i].record.Roll = reader.GetFloat("Roll");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spellvisualkitmodelattachRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spellvisualkitmodelattach

    public class spellvisualprecasttransitionsdbc {
        public DBCHeader header;
        public spellvisualprecasttransitionsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellvisualprecasttransitions", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, `Load`, Hold FROM spellvisualprecasttransitions ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new spellvisualprecasttransitionsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellvisualprecasttransitionsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Load = reader.GetString("Load");
                    body.records[i].Hold = reader.GetString("Hold");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Load
                    if (body.records[i].Load.Length == 0)
                        body.records[i].record.Load = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Load)) body.records[i].record.Load = offsetStorage[body.records[i].Load];
                        else {
                            body.records[i].record.Load = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Load) + 1;
                            offsetStorage.Add(body.records[i].Load, body.records[i].record.Load);
                            reverseStorage.Add(body.records[i].record.Load, body.records[i].Load); } }
                    // Hold
                    if (body.records[i].Hold.Length == 0)
                        body.records[i].record.Hold = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Hold)) body.records[i].record.Hold = offsetStorage[body.records[i].Hold];
                        else {
                            body.records[i].record.Hold = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Hold) + 1;
                            offsetStorage.Add(body.records[i].Hold, body.records[i].record.Hold);
                            reverseStorage.Add(body.records[i].record.Hold, body.records[i].Hold); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(spellvisualprecasttransitionsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // spellvisualprecasttransitions

    public class startup_stringsdbc {
        public DBCHeader header;
        public startup_stringsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM startup_strings", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, InternalName, Text, Text_loc2 FROM startup_strings ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new startup_stringsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 19;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(startup_stringsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].InternalName = reader.GetString("InternalName");

                    body.records[i].Text = new string[17];
                    body.records[i].record.Text = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Text[loc] = "";
                    body.records[i].Text[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Text_loc2" : "Text");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Text
                        if (body.records[i].Text[j].Length == 0)
                            body.records[i].record.Text[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Text[j])) body.records[i].record.Text[j] = offsetStorage[body.records[i].Text[j]];
                            else {
                                body.records[i].record.Text[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Text[j]) + 1;
                                offsetStorage.Add(body.records[i].Text[j], body.records[i].record.Text[j]);
                                reverseStorage.Add(body.records[i].record.Text[j], body.records[i].Text[j]); } } }
                    // InternalName
                    if (body.records[i].InternalName.Length == 0)
                        body.records[i].record.InternalName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].InternalName)) body.records[i].record.InternalName = offsetStorage[body.records[i].InternalName];
                        else {
                            body.records[i].record.InternalName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].InternalName) + 1;
                            offsetStorage.Add(body.records[i].InternalName, body.records[i].record.InternalName);
                            reverseStorage.Add(body.records[i].record.InternalName, body.records[i].InternalName); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(startup_stringsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // startup_strings

    public class stationerydbc {
        public DBCHeader header;
        public stationeryBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM stationery", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Item, Texture, Flags FROM stationery ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new stationeryMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(stationeryRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Item = reader.GetInt32("Item");
                    body.records[i].Texture = reader.GetString("Texture");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Texture
                    if (body.records[i].Texture.Length == 0)
                        body.records[i].record.Texture = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Texture)) body.records[i].record.Texture = offsetStorage[body.records[i].Texture];
                        else {
                            body.records[i].record.Texture = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Texture) + 1;
                            offsetStorage.Add(body.records[i].Texture, body.records[i].record.Texture);
                            reverseStorage.Add(body.records[i].record.Texture, body.records[i].Texture); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(stationeryRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // stationery

    public class stringlookupsdbc {
        public DBCHeader header;
        public stringlookupsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM stringlookups", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Model FROM stringlookups ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new stringlookupsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(stringlookupsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Model = reader.GetString("Model");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Model
                    if (body.records[i].Model.Length == 0)
                        body.records[i].record.Model = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Model)) body.records[i].record.Model = offsetStorage[body.records[i].Model];
                        else {
                            body.records[i].record.Model = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Model) + 1;
                            offsetStorage.Add(body.records[i].Model, body.records[i].record.Model);
                            reverseStorage.Add(body.records[i].record.Model, body.records[i].Model); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(stringlookupsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // stringlookups

    public class terraintypedbc {
        public DBCHeader header;
        public terraintypeBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM terraintype", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Description, FootstepSprayRun, FootstepSprayWalk, Sound, Flags FROM terraintype ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new terraintypeMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 6;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(terraintypeRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Description = reader.GetString("Description");
                    body.records[i].record.FootstepSprayRun = reader.GetInt32("FootstepSprayRun");
                    body.records[i].record.FootstepSprayWalk = reader.GetInt32("FootstepSprayWalk");
                    body.records[i].record.Sound = reader.GetInt32("Sound");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0

                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // InternalName
                    if (body.records[i].Description.Length == 0)
                        body.records[i].record.Description = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Description)) body.records[i].record.Description = offsetStorage[body.records[i].Description];
                        else {
                            body.records[i].record.Description = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Description) + 1;
                            offsetStorage.Add(body.records[i].Description, body.records[i].record.Description);
                            reverseStorage.Add(body.records[i].record.Description, body.records[i].Description); } } }
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(terraintypeRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // terraintype
    
    public class terraintypesoundsdbc {
        public DBCHeader header;
        public terraintypesoundsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM terraintypesounds", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id FROM terraintypesounds ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new terraintypesoundsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 1;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(terraintypesoundsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(terraintypesoundsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // terraintypesounds

    public class transportphysicsdbc {
        public DBCHeader header;
        public transportphysicsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM transportphysics", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, WaveAmp, WaveTimeScale, RollAmp, RollTimeScale, PitchAmp, PitchTimeScale, MaxBank, MaxBankTurnSpeed, SpeedDampThresh, SpeedDamp FROM transportphysics ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new transportphysicsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 11;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(transportphysicsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.WaveAmp = reader.GetFloat("WaveAmp");
                    body.records[i].record.WaveTimeScale = reader.GetFloat("WaveTimeScale");
                    body.records[i].record.RollAmp = reader.GetFloat("RollAmp");
                    body.records[i].record.RollTimeScale = reader.GetFloat("RollTimeScale");
                    body.records[i].record.PitchAmp = reader.GetFloat("PitchAmp");
                    body.records[i].record.PitchTimeScale = reader.GetFloat("PitchTimeScale");
                    body.records[i].record.MaxBank = reader.GetFloat("MaxBank");
                    body.records[i].record.MaxBankTurnSpeed = reader.GetFloat("MaxBankTurnSpeed");
                    body.records[i].record.SpeedDampThresh = reader.GetFloat("SpeedDampThresh");
                    body.records[i].record.SpeedDamp = reader.GetFloat("SpeedDamp");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(transportphysicsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // transportphysics

    public class uisoundlookupsdbc {
        public DBCHeader header;
        public uisoundlookupsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM uisoundlookups", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Sound, InternalName FROM uisoundlookups ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new uisoundlookupsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(uisoundlookupsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Sound = reader.GetInt32("Sound");
                    body.records[i].InternalName = reader.GetString("InternalName");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // InternalName
                    if (body.records[i].InternalName.Length == 0)
                        body.records[i].record.InternalName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].InternalName)) body.records[i].record.InternalName = offsetStorage[body.records[i].InternalName];
                        else {
                            body.records[i].record.InternalName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].InternalName) + 1;
                            offsetStorage.Add(body.records[i].InternalName, body.records[i].record.InternalName);
                            reverseStorage.Add(body.records[i].record.InternalName, body.records[i].InternalName); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(uisoundlookupsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // uisoundlookups

    public class unitblooddbc {
        public DBCHeader header;
        public unitbloodBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM unitblood", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, CombatBloodSpurtFront1, CombatBloodSpurtFront2, CombatBloodSpurtBack1, CombatBloodSpurtBack2, GroundBlood1, GroundBlood2, GroundBlood3, GroundBlood4, GroundBlood5 FROM unitblood ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new unitbloodMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 10;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(unitbloodRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.CombatBloodSpurtFront1 = reader.GetInt32("CombatBloodSpurtFront1");
                    body.records[i].record.CombatBloodSpurtFront2 = reader.GetInt32("CombatBloodSpurtFront2");
                    body.records[i].record.CombatBloodSpurtBack1 = reader.GetInt32("CombatBloodSpurtBack1");
                    body.records[i].record.CombatBloodSpurtBack2 = reader.GetInt32("CombatBloodSpurtBack2");
                    body.records[i].GroundBlood1 = reader.GetString("GroundBlood1");
                    body.records[i].GroundBlood2 = reader.GetString("GroundBlood2");
                    body.records[i].GroundBlood3 = reader.GetString("GroundBlood3");
                    body.records[i].GroundBlood4 = reader.GetString("GroundBlood4");
                    body.records[i].GroundBlood5 = reader.GetString("GroundBlood5");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // GroundBlood1
                    if (body.records[i].GroundBlood1.Length == 0)
                        body.records[i].record.GroundBlood1 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].GroundBlood1)) body.records[i].record.GroundBlood1 = offsetStorage[body.records[i].GroundBlood1];
                        else {
                            body.records[i].record.GroundBlood1 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].GroundBlood1) + 1;
                            offsetStorage.Add(body.records[i].GroundBlood1, body.records[i].record.GroundBlood1);
                            reverseStorage.Add(body.records[i].record.GroundBlood1, body.records[i].GroundBlood1); } }
                    // GroundBlood2
                    if (body.records[i].GroundBlood2.Length == 0)
                        body.records[i].record.GroundBlood2 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].GroundBlood2)) body.records[i].record.GroundBlood2 = offsetStorage[body.records[i].GroundBlood2];
                        else {
                            body.records[i].record.GroundBlood2 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].GroundBlood2) + 1;
                            offsetStorage.Add(body.records[i].GroundBlood2, body.records[i].record.GroundBlood2);
                            reverseStorage.Add(body.records[i].record.GroundBlood2, body.records[i].GroundBlood2); } }
                    // GroundBlood3
                    if (body.records[i].GroundBlood3.Length == 0)
                        body.records[i].record.GroundBlood3 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].GroundBlood3)) body.records[i].record.GroundBlood3 = offsetStorage[body.records[i].GroundBlood3];
                        else {
                            body.records[i].record.GroundBlood3 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].GroundBlood3) + 1;
                            offsetStorage.Add(body.records[i].GroundBlood3, body.records[i].record.GroundBlood3);
                            reverseStorage.Add(body.records[i].record.GroundBlood3, body.records[i].GroundBlood3); } }
                    // GroundBlood4
                    if (body.records[i].GroundBlood4.Length == 0)
                        body.records[i].record.GroundBlood4 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].GroundBlood4)) body.records[i].record.GroundBlood4 = offsetStorage[body.records[i].GroundBlood4];
                        else {
                            body.records[i].record.GroundBlood4 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].GroundBlood4) + 1;
                            offsetStorage.Add(body.records[i].GroundBlood4, body.records[i].record.GroundBlood4);
                            reverseStorage.Add(body.records[i].record.GroundBlood4, body.records[i].GroundBlood4); } }
                    // GroundBlood5
                    if (body.records[i].GroundBlood5.Length == 0)
                        body.records[i].record.GroundBlood5 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].GroundBlood5)) body.records[i].record.GroundBlood5 = offsetStorage[body.records[i].GroundBlood5];
                        else {
                            body.records[i].record.GroundBlood5 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].GroundBlood5) + 1;
                            offsetStorage.Add(body.records[i].GroundBlood5, body.records[i].record.GroundBlood5);
                            reverseStorage.Add(body.records[i].record.GroundBlood5, body.records[i].GroundBlood5); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(unitbloodRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // unitblood

    public class unitbloodlevelsdbc {
        public DBCHeader header;
        public unitbloodlevelsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM unitbloodlevels", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, ViolenceLevel1, ViolenceLevel2, ViolenceLevel3 FROM unitbloodlevels ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new unitbloodlevelsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(unitbloodlevelsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.ViolenceLevel1 = reader.GetInt32("ViolenceLevel1");
                    body.records[i].record.ViolenceLevel2 = reader.GetInt32("ViolenceLevel2");
                    body.records[i].record.ViolenceLevel3 = reader.GetInt32("ViolenceLevel3");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(unitbloodlevelsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // unitbloodlevels

    public class vehicleuiindicatordbc {
        public DBCHeader header;
        public vehicleuiindicatorBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM vehicleuiindicator", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Texture FROM vehicleuiindicator ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new vehicleuiindicatorMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(vehicleuiindicatorRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Texture = reader.GetString("Texture");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Texture
                    if (body.records[i].Texture.Length == 0)
                        body.records[i].record.Texture = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Texture)) body.records[i].record.Texture = offsetStorage[body.records[i].Texture];
                        else {
                            body.records[i].record.Texture = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Texture) + 1;
                            offsetStorage.Add(body.records[i].Texture, body.records[i].record.Texture);
                            reverseStorage.Add(body.records[i].record.Texture, body.records[i].Texture); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(vehicleuiindicatorRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // vehicleuiindicator

    public class vehicleuiindseatdbc {
        public DBCHeader header;
        public vehicleuiindseatBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM vehicleuiindseat", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, VehicleUIIndicator, VirtualSeatIndex, X, Y FROM vehicleuiindseat ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new vehicleuiindseatMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(vehicleuiindseatRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.VehicleUIIndicator = reader.GetInt32("VehicleUIIndicator");
                    body.records[i].record.VirtualSeatIndex = reader.GetInt32("VirtualSeatIndex");
                    body.records[i].record.X = reader.GetFloat("X");
                    body.records[i].record.Y = reader.GetFloat("Y");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(vehicleuiindseatRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // vehicleuiindseat

    public class videohardwaredbc {
        public DBCHeader header;
        public videohardwareBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM videohardware", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Vendor, Device, FarclipIdx, TerrainLODDistIdx, TerrainShadowLOD, DetailDoodadDensityIdx, DetailDoodadAlpha, AnimatingDoodadIdx, Trilinear, NumLights, Specularity, WaterLODIdx, ParticleDensityIdx, UnitDrawDistIdx, SmallCullDistIdx, ResolutionIdx, BaseMipLevel, OglOverrides, D3DOverrides, FixLag, Multisample, AtlasDisable FROM videohardware ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new videohardwareMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 23;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(videohardwareRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Vendor = reader.GetInt32("Vendor");
                    body.records[i].record.Device = reader.GetInt32("Device");
                    body.records[i].record.FarclipIdx = reader.GetInt32("FarclipIdx");
                    body.records[i].record.TerrainLODDistIdx = reader.GetInt32("TerrainLODDistIdx");
                    body.records[i].record.TerrainShadowLOD = reader.GetInt32("TerrainShadowLOD");
                    body.records[i].record.DetailDoodadDensityIdx = reader.GetInt32("DetailDoodadDensityIdx");
                    body.records[i].record.DetailDoodadAlpha = reader.GetInt32("DetailDoodadAlpha");
                    body.records[i].record.AnimatingDoodadIdx = reader.GetInt32("AnimatingDoodadIdx");
                    body.records[i].record.Trilinear = reader.GetInt32("Trilinear");
                    body.records[i].record.NumLights = reader.GetInt32("NumLights");
                    body.records[i].record.Specularity = reader.GetInt32("Specularity");
                    body.records[i].record.WaterLODIdx = reader.GetInt32("WaterLODIdx");
                    body.records[i].record.ParticleDensityIdx = reader.GetInt32("ParticleDensityIdx");
                    body.records[i].record.UnitDrawDistIdx = reader.GetInt32("UnitDrawDistIdx");
                    body.records[i].record.SmallCullDistIdx = reader.GetInt32("SmallCullDistIdx");
                    body.records[i].record.ResolutionIdx = reader.GetInt32("ResolutionIdx");
                    body.records[i].record.BaseMipLevel = reader.GetInt32("BaseMipLevel");
                    body.records[i].record.OglOverrides = reader.GetInt32("OglOverrides");
                    body.records[i].record.D3DOverrides = reader.GetInt32("D3DOverrides");
                    body.records[i].record.FixLag = reader.GetInt32("FixLag");
                    body.records[i].record.Multisample = reader.GetInt32("Multisample");
                    body.records[i].record.AtlasDisable = reader.GetInt32("AtlasDisable");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(videohardwareRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                writer.Write(824980480); // Fck the logic.
                writer.Write(824980795);
                writer.Write(824980480);
                writer.Write(824980795);
                writer.Write(824981051);
                writer.Write(858533888);

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // videohardware

    public class vocaluisoundsdbc {
        public DBCHeader header;
        public vocaluisoundsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM vocaluisounds", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, VocalUIEnum, Race, NormalSound1, NormalSound2, PissedSound1, PissedSound2 FROM vocaluisounds ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new vocaluisoundsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 7;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(vocaluisoundsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.VocalUIEnum = reader.GetInt32("VocalUIEnum");
                    body.records[i].record.Race = reader.GetInt32("Race");
                    body.records[i].record.NormalSound1 = reader.GetInt32("NormalSound1");
                    body.records[i].record.NormalSound2 = reader.GetInt32("NormalSound2");
                    body.records[i].record.PissedSound1 = reader.GetInt32("PissedSound1");
                    body.records[i].record.PissedSound2 = reader.GetInt32("PissedSound2");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(vocaluisoundsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // vocaluisounds

    public class weaponimpactsoundsdbc {
        public DBCHeader header;
        public weaponimpactsoundsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM weaponimpactsounds", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, WeaponSubClass, ParrySoundType, ImpactSound1, ImpactSound2, ImpactSound3, ImpactSound4, ImpactSound5, ImpactSound6, ImpactSound7, ImpactSound8, ImpactSound9, ImpactSound10, CritImpactSound1, CritImpactSound2, CritImpactSound3, CritImpactSound4, CritImpactSound5, CritImpactSound6, CritImpactSound7, CritImpactSound8, CritImpactSound9, CritImpactSound10 FROM weaponimpactsounds ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new weaponimpactsoundsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 23;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(weaponimpactsoundsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.WeaponSubClass = reader.GetInt32("WeaponSubClass");
                    body.records[i].record.ParrySoundType = reader.GetInt32("ParrySoundType");
                    body.records[i].record.ImpactSound1 = reader.GetInt32("ImpactSound1");
                    body.records[i].record.ImpactSound2 = reader.GetInt32("ImpactSound2");
                    body.records[i].record.ImpactSound3 = reader.GetInt32("ImpactSound3");
                    body.records[i].record.ImpactSound4 = reader.GetInt32("ImpactSound4");
                    body.records[i].record.ImpactSound5 = reader.GetInt32("ImpactSound5");
                    body.records[i].record.ImpactSound6 = reader.GetInt32("ImpactSound6");
                    body.records[i].record.ImpactSound7 = reader.GetInt32("ImpactSound7");
                    body.records[i].record.ImpactSound8 = reader.GetInt32("ImpactSound8");
                    body.records[i].record.ImpactSound9 = reader.GetInt32("ImpactSound9");
                    body.records[i].record.ImpactSound10 = reader.GetInt32("ImpactSound10");
                    body.records[i].record.CritImpactSound1 = reader.GetInt32("CritImpactSound1");
                    body.records[i].record.CritImpactSound2 = reader.GetInt32("CritImpactSound2");
                    body.records[i].record.CritImpactSound3 = reader.GetInt32("CritImpactSound3");
                    body.records[i].record.CritImpactSound4 = reader.GetInt32("CritImpactSound4");
                    body.records[i].record.CritImpactSound5 = reader.GetInt32("CritImpactSound5");
                    body.records[i].record.CritImpactSound6 = reader.GetInt32("CritImpactSound6");
                    body.records[i].record.CritImpactSound7 = reader.GetInt32("CritImpactSound7");
                    body.records[i].record.CritImpactSound8 = reader.GetInt32("CritImpactSound8");
                    body.records[i].record.CritImpactSound9 = reader.GetInt32("CritImpactSound9");
                    body.records[i].record.CritImpactSound10 = reader.GetInt32("CritImpactSound10");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(weaponimpactsoundsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // weaponimpactsounds

    public class weaponswingsounds2dbc {
        public DBCHeader header;
        public weaponswingsounds2Body body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM weaponswingsounds2", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Weight, Critical, Sound FROM weaponswingsounds2 ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new weaponswingsounds2Map[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(weaponswingsounds2Record));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Weight = reader.GetInt32("Weight");
                    body.records[i].record.Critical = reader.GetInt32("Critical");
                    body.records[i].record.Sound = reader.GetInt32("Sound");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(weaponswingsounds2Record)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // weaponswingsounds2

    public class weatherdbc {
        public DBCHeader header;
        public weatherBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM weather", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, AmbienceId, EffectType, EffectColor1, EffectColor2, EffectColor3, EffectColor4, Texture FROM weather ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new weatherMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(weatherRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.AmbienceId = reader.GetInt32("AmbienceId");
                    body.records[i].record.EffectType = reader.GetInt32("EffectType");
                    body.records[i].record.EffectColor1 = reader.GetFloat("EffectColor1");
                    body.records[i].record.EffectColor2 = reader.GetFloat("EffectColor2");
                    body.records[i].record.EffectColor3 = reader.GetFloat("EffectColor3");
                    body.records[i].record.EffectColor4 = reader.GetFloat("EffectColor4");
                    body.records[i].Texture = reader.GetString("Texture");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Texture
                    if (body.records[i].Texture.Length == 0)
                        body.records[i].record.Texture = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Texture)) body.records[i].record.Texture = offsetStorage[body.records[i].Texture];
                        else {
                            body.records[i].record.Texture = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Texture) + 1;
                            offsetStorage.Add(body.records[i].Texture, body.records[i].record.Texture);
                            reverseStorage.Add(body.records[i].record.Texture, body.records[i].Texture); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(weatherRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // weather
    
    public class worldchunksoundsdbc {
        public DBCHeader header;
        public worldchunksoundsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM worldchunksounds", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Map, ChunkX, ChunkY, SubChunkX, SubChunkY, ZoneIntroMusic, ZoneMusic, SoundAmbience, SoundProviderPreference FROM worldchunksounds ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new worldchunksoundsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 9;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(worldchunksoundsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Map = reader.GetInt32("Map");
                    body.records[i].record.ChunkX = reader.GetInt32("ChunkX");
                    body.records[i].record.ChunkY = reader.GetInt32("ChunkY");
                    body.records[i].record.SubChunkX = reader.GetInt32("SubChunkX");
                    body.records[i].record.SubChunkY = reader.GetInt32("SubChunkY");
                    body.records[i].record.ZoneIntroMusic = reader.GetInt32("ZoneIntroMusic");
                    body.records[i].record.ZoneMusic = reader.GetInt32("ZoneMusic");
                    body.records[i].record.SoundAmbience = reader.GetInt32("SoundAmbience");
                    body.records[i].record.SoundProviderPreference = reader.GetInt32("SoundProviderPreference");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(worldchunksoundsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // worldchunksounds

    public class worldmapcontinentdbc {
        public DBCHeader header;
        public worldmapcontinentBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM worldmapcontinent", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Map, LeftBoundary, RightBoundary, TopBoundary, BottomBoundary, ContinentOffsetX, ContinentOffsetY, Scale, TaxiMinX, TaxiMinY, TaxiMaxX, TaxiMaxY, WorldMap FROM worldmapcontinent ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new worldmapcontinentMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 14;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(worldmapcontinentRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Map = reader.GetInt32("Map");
                    body.records[i].record.LeftBoundary = reader.GetInt32("LeftBoundary");
                    body.records[i].record.RightBoundary = reader.GetInt32("RightBoundary");
                    body.records[i].record.TopBoundary = reader.GetInt32("TopBoundary");
                    body.records[i].record.BottomBoundary = reader.GetInt32("BottomBoundary");
                    body.records[i].record.ContinentOffsetX = reader.GetFloat("ContinentOffsetX");
                    body.records[i].record.ContinentOffsetY = reader.GetFloat("ContinentOffsetY");
                    body.records[i].record.Scale = reader.GetFloat("Scale");
                    body.records[i].record.TaxiMinX = reader.GetFloat("TaxiMinX");
                    body.records[i].record.TaxiMinY = reader.GetFloat("TaxiMinY");
                    body.records[i].record.TaxiMaxX = reader.GetFloat("TaxiMaxX");
                    body.records[i].record.TaxiMaxY = reader.GetFloat("TaxiMaxY");
                    body.records[i].record.WorldMap = reader.GetInt32("WorldMap");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(worldmapcontinentRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // worldmapcontinent

    public class worldmaptransformsdbc {
        public DBCHeader header;
        public worldmaptransformsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM worldmaptransforms", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Map, RegionMinX, RegionMinY, RegionMaxX, RegionMaxY, DestinationMap, RegionOffsetX, RegionOffsetY, DestinationArea FROM worldmaptransforms ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new worldmaptransformsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 10;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(worldmaptransformsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Map = reader.GetInt32("Map");
                    body.records[i].record.RegionMinX = reader.GetFloat("RegionMinX");
                    body.records[i].record.RegionMinY = reader.GetFloat("RegionMinY");
                    body.records[i].record.RegionMaxX = reader.GetFloat("RegionMaxX");
                    body.records[i].record.RegionMaxY = reader.GetFloat("RegionMaxY");
                    body.records[i].record.DestinationMap = reader.GetInt32("DestinationMap");
                    body.records[i].record.RegionOffsetX = reader.GetFloat("RegionOffsetX");
                    body.records[i].record.RegionOffsetY = reader.GetFloat("RegionOffsetY");
                    body.records[i].record.DestinationArea = reader.GetInt32("DestinationArea");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(worldmaptransformsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // worldmaptransforms

    public class worldstateuidbc {
        public DBCHeader header;
        public worldstateuiBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM worldstateui", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Map, Zone, PhaseShift, Icon, Text, Text_loc2, Description, Description_loc2, StateVariable, Type, DynamicIcon, DynamicTooltip, DynamicTooltip_loc2, ExtendedUI, ExtendedUIStateVariable1, ExtendedUIStateVariable2, ExtendedUIStateVariable3 FROM worldstateui ORDER BY Description ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new worldstateuiMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 63;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(worldstateuiRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Map = reader.GetInt32("Map");
                    body.records[i].record.Zone = reader.GetInt32("Zone");
                    body.records[i].record.PhaseShift = reader.GetInt32("PhaseShift");
                    body.records[i].Icon = reader.GetString("Icon");
                    body.records[i].record.StateVariable = reader.GetInt32("StateVariable");
                    body.records[i].record.Type = reader.GetInt32("Type");
                    body.records[i].DynamicIcon = reader.GetString("DynamicIcon");
                    body.records[i].record.ExtendedUI = reader.GetInt32("ExtendedUI");
                    body.records[i].record.ExtendedUIStateVariable1 = reader.GetInt32("ExtendedUIStateVariable1");
                    body.records[i].record.ExtendedUIStateVariable2 = reader.GetInt32("ExtendedUIStateVariable2");
                    body.records[i].record.ExtendedUIStateVariable3 = reader.GetInt32("ExtendedUIStateVariable3");

                    body.records[i].Text = new string[17];
                    body.records[i].Description = new string[17];
                    body.records[i].DynamicTooltip = new string[17];
                    body.records[i].record.Text = new UInt32[17];
                    body.records[i].record.Description = new UInt32[17];
                    body.records[i].record.DynamicTooltip = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Text [loc] = "";
                        body.records[i].Description [loc] = "";
                        body.records[i].DynamicTooltip [loc] = ""; }
                    body.records[i].Text [DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Text_loc2" : "Text");
                    body.records[i].Description [DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Description_loc2" : "Description");
                    body.records[i].DynamicTooltip [DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "DynamicTooltip_loc2" : "DynamicTooltip");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0

                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Text
                        if (body.records[i].Text[j].Length == 0)
                            body.records[i].record.Text[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Text[j])) body.records[i].record.Text[j] = offsetStorage[body.records[i].Text[j]];
                            else {
                                body.records[i].record.Text[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Text[j]) + 1;
                                offsetStorage.Add(body.records[i].Text[j], body.records[i].record.Text[j]);
                                reverseStorage.Add(body.records[i].record.Text[j], body.records[i].Text[j]); } }
                        // Description
                        if (body.records[i].Description[j].Length == 0)
                            body.records[i].record.Description[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Description[j])) body.records[i].record.Description[j] = offsetStorage[body.records[i].Description[j]];
                            else {
                                body.records[i].record.Description[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Description[j]) + 1;
                                offsetStorage.Add(body.records[i].Description[j], body.records[i].record.Description[j]);
                                reverseStorage.Add(body.records[i].record.Description[j], body.records[i].Description[j]); } }
                        // DynamicToolTip
                        if (body.records[i].DynamicTooltip[j].Length == 0)
                            body.records[i].record.DynamicTooltip[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].DynamicTooltip[j])) body.records[i].record.DynamicTooltip[j] = offsetStorage[body.records[i].DynamicTooltip[j]];
                            else {
                                body.records[i].record.DynamicTooltip[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].DynamicTooltip[j]) + 1;
                                offsetStorage.Add(body.records[i].DynamicTooltip[j], body.records[i].record.DynamicTooltip[j]);
                                reverseStorage.Add(body.records[i].record.DynamicTooltip[j], body.records[i].DynamicTooltip[j]); } } }
                    // Icon
                    if (body.records[i].Icon.Length == 0)
                        body.records[i].record.Icon = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Icon)) body.records[i].record.Icon = offsetStorage[body.records[i].Icon];
                        else {
                            body.records[i].record.Icon = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Icon) + 1;
                            offsetStorage.Add(body.records[i].Icon, body.records[i].record.Icon);
                            reverseStorage.Add(body.records[i].record.Icon, body.records[i].Icon); } }
                    // DynamicIcon
                    if (body.records[i].DynamicIcon.Length == 0)
                        body.records[i].record.DynamicIcon = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].DynamicIcon)) body.records[i].record.DynamicIcon = offsetStorage[body.records[i].DynamicIcon];
                        else {
                            body.records[i].record.DynamicIcon = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].DynamicIcon) + 1;
                            offsetStorage.Add(body.records[i].DynamicIcon, body.records[i].record.DynamicIcon);
                            reverseStorage.Add(body.records[i].record.DynamicIcon, body.records[i].DynamicIcon); } } }

                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(worldstateuiRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // worldstateui

    public class worldstatezonesoundsdbc {
        public DBCHeader header;
        public worldstatezonesoundsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM worldstatezonesounds", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Value, Area, WMOArea, IntroMusic, Music, SoundAmbience, Preferences FROM worldstatezonesounds ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new worldstatezonesoundsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(worldstatezonesoundsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Value = reader.GetInt32("Value");
                    body.records[i].record.Area = reader.GetInt32("Area");
                    body.records[i].record.WMOArea = reader.GetInt32("WMOArea");
                    body.records[i].record.IntroMusic = reader.GetInt32("IntroMusic");
                    body.records[i].record.Music = reader.GetInt32("Music");
                    body.records[i].record.SoundAmbience = reader.GetInt32("SoundAmbience");
                    body.records[i].record.Preferences = reader.GetInt32("Preferences");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j);
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(worldstatezonesoundsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // worldstatezonesounds

    public class wowerror_stringsdbc {
        public DBCHeader header;
        public wowerror_stringsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM wowerror_strings", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Text, Text_loc2 FROM wowerror_strings ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new wowerror_stringsMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 19;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(wowerror_stringsRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");

                    body.records[i].Text = new string[17];
                    body.records[i].record.Text = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc)
                        body.records[i].Text[loc] = "";
                    body.records[i].Text[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Text_loc2" : "Text");

                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    for (UInt32 j = 0; j < 17; ++j) {
                        // Text
                        if (body.records[i].Text[j].Length == 0)
                            body.records[i].record.Text[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Text[j])) body.records[i].record.Text[j] = offsetStorage[body.records[i].Text[j]];
                            else {
                                body.records[i].record.Text[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Text[j]) + 1;
                                offsetStorage.Add(body.records[i].Text[j], body.records[i].record.Text[j]);
                                reverseStorage.Add(body.records[i].record.Text[j], body.records[i].Text[j]); } } }
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } }
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(wowerror_stringsRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // wowerror_strings

    public class zoneintromusictabledbc {
        public DBCHeader header;
        public zoneintromusictableBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM zoneintromusictable", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Sound, Priority, MinDelayMinutes FROM zoneintromusictable ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new zoneintromusictableMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(zoneintromusictableRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.Sound = reader.GetInt32("Sound");
                    body.records[i].record.Priority = reader.GetInt32("Priority");
                    body.records[i].record.MinDelayMinutes = reader.GetInt32("MinDelayMinutes");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(zoneintromusictableRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // zoneintromusictable

    public class zonemusicdbc {
        public DBCHeader header;
        public zonemusicBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM zonemusic", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, SilenceIntervalMinDay, SilenceIntervalMinNight, SilenceIntervalMaxDay, SilenceIntervalMaxNight, DayMusic, NightMusic FROM zonemusic ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new zonemusicMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(zonemusicRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.SilenceIntervalMinDay = reader.GetInt32("SilenceIntervalMinDay");
                    body.records[i].record.SilenceIntervalMinNight = reader.GetInt32("SilenceIntervalMinNight");
                    body.records[i].record.SilenceIntervalMaxDay = reader.GetInt32("SilenceIntervalMaxDay");
                    body.records[i].record.SilenceIntervalMaxNight = reader.GetInt32("SilenceIntervalMaxNight");
                    body.records[i].record.DayMusic = reader.GetInt32("DayMusic");
                    body.records[i].record.NightMusic = reader.GetInt32("NightMusic");
                    i++; }
                reader.Close(); }
             catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }
 
             return true; }
 
         public bool SaveDBC(string fileName) {
             try {
                Dictionary<string, UInt32> offsetStorage = new Dictionary<string, UInt32>();
                Dictionary<UInt32, string> reverseStorage = new Dictionary<UInt32, string>();
                UInt32 stringBlockOffset = 1; // first character is always \0
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Generate some string offsets...
                    // Name
                    if (body.records[i].Name.Length == 0)
                        body.records[i].record.Name = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Name)) body.records[i].record.Name = offsetStorage[body.records[i].Name];
                        else {
                            body.records[i].record.Name = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name) + 1;
                            offsetStorage.Add(body.records[i].Name, body.records[i].record.Name);
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name); } } }
 
                header.string_block_size = (int)stringBlockOffset;
 
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fs);
                int count = Marshal.SizeOf(typeof(DBCHeader)); // Write header
                byte[] buffer = new byte[count];
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(header, gcHandle.AddrOfPinnedObject(), true);
                writer.Write(buffer, 0, count);
                gcHandle.Free();
 
                for (UInt32 i = 0; i < header.record_count; ++i) { // Write records
                    count = Marshal.SizeOf(typeof(zonemusicRecord)); // Write main body
                    buffer = new byte[count];
                    gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    Marshal.StructureToPtr(body.records[i].record, gcHandle.AddrOfPinnedObject(), true);
                    writer.Write(buffer, 0, count);
                    gcHandle.Free(); }

                UInt32[] offsets_stored = offsetStorage.Values.ToArray<UInt32>();
                writer.Write(Encoding.UTF8.GetBytes("\0")); // Write string block
                for (int i = 0; i < offsets_stored.Length; ++i)
                    writer.Write(Encoding.UTF8.GetBytes(reverseStorage[offsets_stored[i]] + "\0"));

                writer.Close();
                fs.Close(); }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false; }

            return true; } } // zonemusic
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace DBtoDBC {

    public class achievement_criteriadbc {
        public DBCHeader header;
        public achievement_criteriaBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM achievement_criteriadbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Achievement, Type, AssetId, Quantity, StartEvent, StartAsset, FailEvent, FailAsset, Description, Description_loc2, Flags, TimerAssetId, TimerStartEvent, TimerTime, UIOrder FROM achievement_criteriadbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new achievement_criteriaMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 31;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(achievement_criteriaRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Achievement = reader.GetInt32("Achievement");
                    body.records[i].record.Type = reader.GetInt32("Type");
                    body.records[i].record.AssetId = reader.GetInt32("AssetId");
                    body.records[i].record.Quantity = reader.GetInt32("Quantity");
                    body.records[i].record.StartEvent = reader.GetInt32("StartEvent");
                    body.records[i].record.StartAsset = reader.GetInt32("StartAsset");
                    body.records[i].record.FailEvent = reader.GetInt32("FailEvent");
                    body.records[i].record.FailAsset = reader.GetInt32("FailAsset");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.TimerAssetId = reader.GetInt32("TimerAssetId");
                    body.records[i].record.TimerStartEvent = reader.GetInt32("TimerStartEvent");
                    body.records[i].record.TimerTime = reader.GetInt32("TimerTime");
                    body.records[i].record.UIOrder = reader.GetInt32("UIOrder");
                    
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
                        // Desc
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
                    count = Marshal.SizeOf(typeof(achievement_criteriaRecord)); // Write main body
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

            return true; } } // achievement_criteria

    public class achievementdbc {
        public DBCHeader header;
        public achievementBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM achievementdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Faction, MapId, Previous, Name, Name_loc2, Description, Description_loc2, Category, Points, OrderInGroup, Flags, SpellIcon, Reward, Reward_loc2, Rewardflags, Demands, ReferencedAchievement FROM achievementdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new achievementMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 62;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(achievementRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Faction = reader.GetInt32("Faction");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.Previous = reader.GetInt32("Previous");
                    body.records[i].record.Category = reader.GetInt32("Category");
                    body.records[i].record.Points = reader.GetInt32("Points");
                    body.records[i].record.OrderInGroup = reader.GetInt32("OrderInGroup");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.SpellIcon = reader.GetInt32("SpellIcon");
                    body.records[i].record.Demands = reader.GetInt32("Demands");
                    body.records[i].record.ReferencedAchievement = reader.GetInt32("ReferencedAchievement");

                    body.records[i].Name = new string[17];
                    body.records[i].Description = new string[17];
                    body.records[i].Reward = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.Description = new UInt32[17];
                    body.records[i].record.Reward = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name[loc] = "";
                        body.records[i].Description[loc] = "";
                        body.records[i].Reward[loc] = ""; }
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");
                    body.records[i].Description[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Description_loc2" : "Description");
                    body.records[i].Reward[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Reward_loc2" : "Reward");

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
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } }
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
                        // Reward
                        if (body.records[i].Reward[j].Length == 0)
                            body.records[i].record.Reward[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Reward[j])) body.records[i].record.Reward[j] = offsetStorage[body.records[i].Reward[j]];
                            else {
                                body.records[i].record.Reward[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Reward[j]) + 1;
                                offsetStorage.Add(body.records[i].Reward[j], body.records[i].record.Reward[j]);
                                reverseStorage.Add(body.records[i].record.Reward[j], body.records[i].Reward[j]); } } }

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
                    count = Marshal.SizeOf(typeof(achievementRecord)); // Write main body
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

            return true; } } // achievement

    public class areagroupdbc {
        public DBCHeader header;
        public areagroupBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM areagroupdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, AreaId1, AreaId2, AreaId3, AreaId4, AreaId5, AreaId6, NextGroup FROM areagroupdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new areagroupMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(areagroupRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.AreaId1 = reader.GetInt32("AreaId1");
                    body.records[i].record.AreaId2 = reader.GetInt32("AreaId2");
                    body.records[i].record.AreaId3 = reader.GetInt32("AreaId3");
                    body.records[i].record.AreaId4 = reader.GetInt32("AreaId4");
                    body.records[i].record.AreaId5 = reader.GetInt32("AreaId5");
                    body.records[i].record.AreaId6 = reader.GetInt32("AreaId6");
                    body.records[i].record.NextGroup = reader.GetInt32("NextGroup");
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
                    count = Marshal.SizeOf(typeof(areagroupRecord)); // Write main body
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

            return true; } } // areagroup

    public class areapoidbc {
        public DBCHeader header;
        public areapoiBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM areapoidbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Importance, NormalIcon, NormalIcon50, NormalIcon0, HordeIcon, HordeIcon50, HordeIcon0, AllianceIcon, AllianceIcon50, AllianceIcon0, FactionId, X, Y, Z, MapId, Flags, Area, Name, Name_loc2, Description, Description_loc2, WorldState, WorldMapLink FROM areapoidbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new areapoiMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 54;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(areapoiRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Importance = reader.GetInt32("Importance");
                    body.records[i].record.NormalIcon = reader.GetInt32("NormalIcon");
                    body.records[i].record.NormalIcon50 = reader.GetInt32("NormalIcon50");
                    body.records[i].record.NormalIcon0 = reader.GetInt32("NormalIcon0");
                    body.records[i].record.HordeIcon = reader.GetInt32("HordeIcon");
                    body.records[i].record.HordeIcon50 = reader.GetInt32("HordeIcon50");
                    body.records[i].record.HordeIcon0 = reader.GetInt32("HordeIcon0");
                    body.records[i].record.AllianceIcon = reader.GetInt32("AllianceIcon");
                    body.records[i].record.AllianceIcon50 = reader.GetInt32("AllianceIcon50");
                    body.records[i].record.AllianceIcon0 = reader.GetInt32("AllianceIcon0");
                    body.records[i].record.FactionId = reader.GetInt32("FactionId");
                    body.records[i].record.X = reader.GetFloat("X");
                    body.records[i].record.Y = reader.GetFloat("Y");
                    body.records[i].record.Z = reader.GetFloat("Z");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.Area = reader.GetInt32("Area");
                    body.records[i].record.WorldState = reader.GetInt32("WorldState");
                    body.records[i].record.WorldMapLink = reader.GetInt32("WorldMapLink");

                    body.records[i].Name = new string[17];
                    body.records[i].Description = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.Description = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name[loc] = "";
                        body.records[i].Description[loc] = ""; }
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");
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
                    count = Marshal.SizeOf(typeof(areapoiRecord)); // Write main body
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

            return true; } } // areapoi

    public class areatabledbc {
        public DBCHeader header;
        public areatableBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM areatabledbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, MapId, ParentArea, ExploreFlag, Flags, SoundPreferences, SoundPreferencesUnderwater, SoundAmbience, ZoneMusic, ZoneIntroMusic, ExplorationLevel, Name, Name_loc2, FactionGroup, LiquidTypeWater, LiquidTypeOcean, LiquidTypeMagma, LiquidTypeSlime, MinElevation, AmbientMultiplier, Light FROM areatabledbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new areatableMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 36;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(areatableRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.ParentArea = reader.GetInt32("ParentArea");
                    body.records[i].record.ExploreFlag = reader.GetInt32("ExploreFlag");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.SoundPreferences = reader.GetInt32("SoundPreferences");
                    body.records[i].record.SoundPreferencesUnderwater = reader.GetInt32("SoundPreferencesUnderwater");
                    body.records[i].record.SoundAmbience = reader.GetInt32("SoundAmbience");
                    body.records[i].record.ZoneMusic = reader.GetInt32("ZoneMusic");
                    body.records[i].record.ZoneIntroMusic = reader.GetInt32("ZoneIntroMusic");
                    body.records[i].record.ExplorationLevel = reader.GetInt32("ExplorationLevel");
                    body.records[i].record.FactionGroup = reader.GetInt32("FactionGroup");
                    body.records[i].record.LiquidTypeWater = reader.GetInt32("LiquidTypeWater");
                    body.records[i].record.LiquidTypeOcean = reader.GetInt32("LiquidTypeOcean");
                    body.records[i].record.LiquidTypeMagma = reader.GetInt32("LiquidTypeMagma");
                    body.records[i].record.LiquidTypeSlime = reader.GetInt32("LiquidTypeSlime");
                    body.records[i].record.MinElevation = reader.GetFloat("MinElevation");
                    body.records[i].record.AmbientMultiplier = reader.GetFloat("AmbientMultiplier");
                    body.records[i].record.Light = reader.GetInt32("Light");

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
                    count = Marshal.SizeOf(typeof(areatableRecord)); // Write main body
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

            return true; } } // areatable

    public class areatriggerdbc {
        public DBCHeader header;
        public areatriggerBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM areatriggerdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, MapId, X, Y, Z, Radius, BoxX, BoxY, BoxZ, BoxOrientation FROM areatriggerdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new areatriggerMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 10;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(areatriggerRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.X = reader.GetFloat("X");
                    body.records[i].record.Y = reader.GetFloat("Y");
                    body.records[i].record.Z = reader.GetFloat("Z");
                    body.records[i].record.Radius = reader.GetFloat("Radius");
                    body.records[i].record.BoxX = reader.GetFloat("BoxX");
                    body.records[i].record.BoxY = reader.GetFloat("BoxY");
                    body.records[i].record.BoxZ = reader.GetFloat("BoxZ");
                    body.records[i].record.BoxOrientation = reader.GetFloat("BoxOrientation");
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
                    count = Marshal.SizeOf(typeof(areatriggerRecord)); // Write main body
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

            return true; } } // areatrigger

    public class auctionhousedbc {
        public DBCHeader header;
        public auctionhouseBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM auctionhousedbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT HouseId, Faction, DepositPercent, CutPercent, Name, Name_loc2 FROM auctionhousedbc ORDER BY HouseId ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new auctionhouseMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 21;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(auctionhouseRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.HouseId = reader.GetInt32("HouseId");
                    body.records[i].record.Faction = reader.GetInt32("Faction");
                    body.records[i].record.DepositPercent = reader.GetInt32("DepositPercent");
                    body.records[i].record.CutPercent = reader.GetInt32("CutPercent");

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
                    count = Marshal.SizeOf(typeof(auctionhouseRecord)); // Write main body
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

            return true; } } // auctionhouse

    public class bankbagslotpricesdbc {
        public DBCHeader header;
        public bankbagslotpricesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM bankbagslotpricesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Price FROM bankbagslotpricesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new bankbagslotpricesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(bankbagslotpricesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Price = reader.GetInt32("Price");
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
                    count = Marshal.SizeOf(typeof(bankbagslotpricesRecord)); // Write main body
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

            return true; } } // bankbagslotprices

    public class bannedaddonsdbc {
        public DBCHeader header;
        public bannedaddonsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM bannedaddonsdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, NameMD51, NameMD52, NameMD53, NameMD54, VersionMD51, VersionMD52, VersionMD53, VersionMD54, Timestamp, State FROM bannedaddonsdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new bannedaddonsMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 11;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(bannedaddonsRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.NameMD51 = reader.GetInt32("NameMD51");
                    body.records[i].record.NameMD52 = reader.GetInt32("NameMD52");
                    body.records[i].record.NameMD53 = reader.GetInt32("NameMD53");
                    body.records[i].record.NameMD54 = reader.GetInt32("NameMD54");
                    body.records[i].record.VersionMD51 = reader.GetInt32("VersionMD51");
                    body.records[i].record.VersionMD52 = reader.GetInt32("VersionMD52");
                    body.records[i].record.VersionMD53 = reader.GetInt32("VersionMD53");
                    body.records[i].record.VersionMD54 = reader.GetInt32("VersionMD54");
                    body.records[i].record.Timestamp = reader.GetInt32("Timestamp");
                    body.records[i].record.State = reader.GetInt32("State");
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
                    count = Marshal.SizeOf(typeof(bannedaddonsRecord)); // Write main body
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

            return true; } } // bannedaddons

    public class barbershopstyledbc {
        public DBCHeader header;
        public barbershopstyleBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM barbershopstyledbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Type, Name, Name_loc2, CostMultiplier, Race, Gender, HairId FROM barbershopstyledbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new barbershopstyleMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 40;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(barbershopstyleRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Type = reader.GetInt32("Type");
                    body.records[i].record.CostMultiplier = reader.GetFloat("CostMultiplier");
                    body.records[i].record.Race = reader.GetInt32("Race");
                    body.records[i].record.Gender = reader.GetInt32("Gender");
                    body.records[i].record.HairId = reader.GetInt32("HairId");

                    body.records[i].Name = new string[17];
                    body.records[i].AdditionalName = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.AdditionalName = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name[loc] = "";
                        body.records[i].AdditionalName[loc] = ""; }
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
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } }
                        body.records[i].record.AdditionalName[j] = 0; }

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
                    count = Marshal.SizeOf(typeof(barbershopstyleRecord)); // Write main body
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

            return true; } } // barbershopstyle

    public class battlemasterlistdbc {
        public DBCHeader header;
        public battlemasterlistBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM battlemasterlistdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Instance1, Instance2, Instance3, Instance4, Instance5, Instance6, Instance7, Instance8, InstanceType, JoinAsGroup, Name, Name_loc2, MaxGroupSize, HolidayWorldStateId, MinLevel, MaxLevel FROM battlemasterlistdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new battlemasterlistMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 32;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(battlemasterlistRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Instance1 = reader.GetInt32("Instance1");
                    body.records[i].record.Instance2 = reader.GetInt32("Instance2");
                    body.records[i].record.Instance3 = reader.GetInt32("Instance3");
                    body.records[i].record.Instance4 = reader.GetInt32("Instance4");
                    body.records[i].record.Instance5 = reader.GetInt32("Instance5");
                    body.records[i].record.Instance6 = reader.GetInt32("Instance6");
                    body.records[i].record.Instance7 = reader.GetInt32("Instance7");
                    body.records[i].record.Instance8 = reader.GetInt32("Instance8");
                    body.records[i].record.InstanceType = reader.GetInt32("InstanceType");
                    body.records[i].record.JoinAsGroup = reader.GetInt32("JoinAsGroup");
                    body.records[i].record.MaxGroupSize = reader.GetInt32("MaxGroupSize");
                    body.records[i].record.HolidayWorldStateId = reader.GetInt32("HolidayWorldStateId");
                    body.records[i].record.MinLevel = reader.GetInt32("MinLevel");
                    body.records[i].record.MaxLevel = reader.GetInt32("MaxLevel");

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
                    count = Marshal.SizeOf(typeof(battlemasterlistRecord)); // Write main body
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

            return true; } } // battlemasterlist

    public class charstartoutfitdbc {
        public DBCHeader header;
        public charstartoutfitBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM charstartoutfitdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Race, Class, Gender, Unused, ItemId1, ItemId2, ItemId3, ItemId4, ItemId5, ItemId6, ItemId7, ItemId8, ItemId9, ItemId10, ItemId11, ItemId12, ItemId13, ItemId14, ItemId15, ItemId16, ItemId17, ItemId18, ItemId19, ItemId20, ItemId21, ItemId22, ItemId23, ItemId24, ItemDisplayId1, ItemDisplayId2, ItemDisplayId3, ItemDisplayId4, ItemDisplayId5, ItemDisplayId6, ItemDisplayId7, ItemDisplayId8, ItemDisplayId9, ItemDisplayId10, ItemDisplayId11, ItemDisplayId12, ItemDisplayId13, ItemDisplayId14, ItemDisplayId15, ItemDisplayId16, ItemDisplayId17, ItemDisplayId18, ItemDisplayId19, ItemDisplayId20, ItemDisplayId21, ItemDisplayId22, ItemDisplayId23, ItemDisplayId24, ItemInventorySlot1, ItemInventorySlot2, ItemInventorySlot3, ItemInventorySlot4, ItemInventorySlot5, ItemInventorySlot6, ItemInventorySlot7, ItemInventorySlot8, ItemInventorySlot9, ItemInventorySlot10, ItemInventorySlot11, ItemInventorySlot12, ItemInventorySlot13, ItemInventorySlot14, ItemInventorySlot15, ItemInventorySlot16, ItemInventorySlot17, ItemInventorySlot18, ItemInventorySlot19, ItemInventorySlot20, ItemInventorySlot21, ItemInventorySlot22, ItemInventorySlot23, ItemInventorySlot24 FROM charstartoutfitdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new charstartoutfitMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 77;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(charstartoutfitRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Race = reader.GetByte("Race");
                    body.records[i].record.Class = reader.GetByte("Class");
                    body.records[i].record.Gender = reader.GetByte("Gender");
                    body.records[i].record.Unused = reader.GetByte("Unused");
                    body.records[i].record.ItemId1 = reader.GetInt32("ItemId1");
                    body.records[i].record.ItemId2 = reader.GetInt32("ItemId2");
                    body.records[i].record.ItemId3 = reader.GetInt32("ItemId3");
                    body.records[i].record.ItemId4 = reader.GetInt32("ItemId4");
                    body.records[i].record.ItemId5 = reader.GetInt32("ItemId5");
                    body.records[i].record.ItemId6 = reader.GetInt32("ItemId6");
                    body.records[i].record.ItemId7 = reader.GetInt32("ItemId7");
                    body.records[i].record.ItemId8 = reader.GetInt32("ItemId8");
                    body.records[i].record.ItemId9 = reader.GetInt32("ItemId9");
                    body.records[i].record.ItemId10 = reader.GetInt32("ItemId10");
                    body.records[i].record.ItemId11 = reader.GetInt32("ItemId11");
                    body.records[i].record.ItemId12 = reader.GetInt32("ItemId12");
                    body.records[i].record.ItemId13 = reader.GetInt32("ItemId13");
                    body.records[i].record.ItemId14 = reader.GetInt32("ItemId14");
                    body.records[i].record.ItemId15 = reader.GetInt32("ItemId15");
                    body.records[i].record.ItemId16 = reader.GetInt32("ItemId16");
                    body.records[i].record.ItemId17 = reader.GetInt32("ItemId17");
                    body.records[i].record.ItemId18 = reader.GetInt32("ItemId18");
                    body.records[i].record.ItemId19 = reader.GetInt32("ItemId19");
                    body.records[i].record.ItemId20 = reader.GetInt32("ItemId20");
                    body.records[i].record.ItemId21 = reader.GetInt32("ItemId21");
                    body.records[i].record.ItemId22 = reader.GetInt32("ItemId22");
                    body.records[i].record.ItemId23 = reader.GetInt32("ItemId23");
                    body.records[i].record.ItemId24 = reader.GetInt32("ItemId24");
                    body.records[i].record.ItemDisplayId1 = reader.GetInt32("ItemDisplayId1");
                    body.records[i].record.ItemDisplayId2 = reader.GetInt32("ItemDisplayId2");
                    body.records[i].record.ItemDisplayId3 = reader.GetInt32("ItemDisplayId3");
                    body.records[i].record.ItemDisplayId4 = reader.GetInt32("ItemDisplayId4");
                    body.records[i].record.ItemDisplayId5 = reader.GetInt32("ItemDisplayId5");
                    body.records[i].record.ItemDisplayId6 = reader.GetInt32("ItemDisplayId6");
                    body.records[i].record.ItemDisplayId7 = reader.GetInt32("ItemDisplayId7");
                    body.records[i].record.ItemDisplayId8 = reader.GetInt32("ItemDisplayId8");
                    body.records[i].record.ItemDisplayId9 = reader.GetInt32("ItemDisplayId9");
                    body.records[i].record.ItemDisplayId10 = reader.GetInt32("ItemDisplayId10");
                    body.records[i].record.ItemDisplayId11 = reader.GetInt32("ItemDisplayId11");
                    body.records[i].record.ItemDisplayId12 = reader.GetInt32("ItemDisplayId12");
                    body.records[i].record.ItemDisplayId13 = reader.GetInt32("ItemDisplayId13");
                    body.records[i].record.ItemDisplayId14 = reader.GetInt32("ItemDisplayId14");
                    body.records[i].record.ItemDisplayId15 = reader.GetInt32("ItemDisplayId15");
                    body.records[i].record.ItemDisplayId16 = reader.GetInt32("ItemDisplayId16");
                    body.records[i].record.ItemDisplayId17 = reader.GetInt32("ItemDisplayId17");
                    body.records[i].record.ItemDisplayId18 = reader.GetInt32("ItemDisplayId18");
                    body.records[i].record.ItemDisplayId19 = reader.GetInt32("ItemDisplayId19");
                    body.records[i].record.ItemDisplayId20 = reader.GetInt32("ItemDisplayId20");
                    body.records[i].record.ItemDisplayId21 = reader.GetInt32("ItemDisplayId21");
                    body.records[i].record.ItemDisplayId22 = reader.GetInt32("ItemDisplayId22");
                    body.records[i].record.ItemDisplayId23 = reader.GetInt32("ItemDisplayId23");
                    body.records[i].record.ItemDisplayId24 = reader.GetInt32("ItemDisplayId24");
                    body.records[i].record.ItemInventorySlot1 = reader.GetInt32("ItemInventorySlot1");
                    body.records[i].record.ItemInventorySlot2 = reader.GetInt32("ItemInventorySlot2");
                    body.records[i].record.ItemInventorySlot3 = reader.GetInt32("ItemInventorySlot3");
                    body.records[i].record.ItemInventorySlot4 = reader.GetInt32("ItemInventorySlot4");
                    body.records[i].record.ItemInventorySlot5 = reader.GetInt32("ItemInventorySlot5");
                    body.records[i].record.ItemInventorySlot6 = reader.GetInt32("ItemInventorySlot6");
                    body.records[i].record.ItemInventorySlot7 = reader.GetInt32("ItemInventorySlot7");
                    body.records[i].record.ItemInventorySlot8 = reader.GetInt32("ItemInventorySlot8");
                    body.records[i].record.ItemInventorySlot9 = reader.GetInt32("ItemInventorySlot9");
                    body.records[i].record.ItemInventorySlot10 = reader.GetInt32("ItemInventorySlot10");
                    body.records[i].record.ItemInventorySlot11 = reader.GetInt32("ItemInventorySlot11");
                    body.records[i].record.ItemInventorySlot12 = reader.GetInt32("ItemInventorySlot12");
                    body.records[i].record.ItemInventorySlot13 = reader.GetInt32("ItemInventorySlot13");
                    body.records[i].record.ItemInventorySlot14 = reader.GetInt32("ItemInventorySlot14");
                    body.records[i].record.ItemInventorySlot15 = reader.GetInt32("ItemInventorySlot15");
                    body.records[i].record.ItemInventorySlot16 = reader.GetInt32("ItemInventorySlot16");
                    body.records[i].record.ItemInventorySlot17 = reader.GetInt32("ItemInventorySlot17");
                    body.records[i].record.ItemInventorySlot18 = reader.GetInt32("ItemInventorySlot18");
                    body.records[i].record.ItemInventorySlot19 = reader.GetInt32("ItemInventorySlot19");
                    body.records[i].record.ItemInventorySlot20 = reader.GetInt32("ItemInventorySlot20");
                    body.records[i].record.ItemInventorySlot21 = reader.GetInt32("ItemInventorySlot21");
                    body.records[i].record.ItemInventorySlot22 = reader.GetInt32("ItemInventorySlot22");
                    body.records[i].record.ItemInventorySlot23 = reader.GetInt32("ItemInventorySlot23");
                    body.records[i].record.ItemInventorySlot24 = reader.GetInt32("ItemInventorySlot24");
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
                    count = Marshal.SizeOf(typeof(charstartoutfitRecord)); // Write main body
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

            return true; } } // charstartoutfit

    public class chartitlesdbc {
        public DBCHeader header;
        public chartitlesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM chartitlesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, UnkRef, Male, Male_loc2, Female, Female_loc2, InGameOrder FROM chartitlesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new chartitlesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 37;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(chartitlesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.UnkRef = reader.GetInt32("UnkRef");
                    body.records[i].record.InGameOrder = reader.GetInt32("InGameOrder");

                    body.records[i].Male = new string[17];
                    body.records[i].Female = new string[17];
                    body.records[i].record.Male = new UInt32[17];
                    body.records[i].record.Female = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Male[loc] = "";
                        body.records[i].Female[loc] = ""; }
                    body.records[i].Male[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Male_loc2" : "Male");
                    body.records[i].Female[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Female_loc2" : "Female");

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
                        // Male
                        if (body.records[i].Male[j].Length == 0)
                            body.records[i].record.Male[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Male[j])) body.records[i].record.Male[j] = offsetStorage[body.records[i].Male[j]];
                            else {
                                body.records[i].record.Male[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Male[j]) + 1;
                                offsetStorage.Add(body.records[i].Male[j], body.records[i].record.Male[j]);
                                reverseStorage.Add(body.records[i].record.Male[j], body.records[i].Male[j]); } }
                        // Female
                        if (body.records[i].Female[j].Length == 0)
                            body.records[i].record.Female[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Female[j])) body.records[i].record.Female[j] = offsetStorage[body.records[i].Female[j]];
                            else {
                                body.records[i].record.Female[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Female[j]) + 1;
                                offsetStorage.Add(body.records[i].Female[j], body.records[i].record.Female[j]);
                                reverseStorage.Add(body.records[i].record.Female[j], body.records[i].Female[j]); } } }

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
                    count = Marshal.SizeOf(typeof(chartitlesRecord)); // Write main body
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

            return true; } } // chartitles

    public class chatchannelsdbc {
        public DBCHeader header;
        public chatchannelsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM chatchannelsdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT ChannelId, Flags, FactionGroup, Pattern, Pattern_loc2, Name, Name_loc2 FROM chatchannelsdbc ORDER BY ChannelId ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new chatchannelsMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 37;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(chatchannelsRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.ChannelId = reader.GetInt32("ChannelId");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.FactionGroup = reader.GetInt32("FactionGroup");

                    body.records[i].Pattern = new string[17];
                    body.records[i].Name = new string[17];
                    body.records[i].record.Pattern = new UInt32[17];
                    body.records[i].record.Name = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Pattern[loc] = "";
                        body.records[i].Name[loc] = ""; }
                    body.records[i].Pattern[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Pattern_loc2" : "Pattern");
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
                        // Pattern
                        if (body.records[i].Pattern[j].Length == 0)
                            body.records[i].record.Pattern[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Pattern[j])) body.records[i].record.Pattern[j] = offsetStorage[body.records[i].Pattern[j]];
                            else {
                                body.records[i].record.Pattern[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Pattern[j]) + 1;
                                offsetStorage.Add(body.records[i].Pattern[j], body.records[i].record.Pattern[j]);
                                reverseStorage.Add(body.records[i].record.Pattern[j], body.records[i].Pattern[j]); } }
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
                    count = Marshal.SizeOf(typeof(chatchannelsRecord)); // Write main body
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

            return true; } } // chatchannels

    public class chrclassesdbc {
        public DBCHeader header;
        public chrclassesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM chrclassesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Unused, PowerType, DispayPower, Name, Name_loc2, Female, Female_loc2, Male, Male_loc2, FileName, SpellFamily, Flags, CinematicSequence, Expansion FROM chrclassesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new chrclassesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 60;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(chrclassesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Unused = reader.GetInt32("Unused");
                    body.records[i].record.PowerType = reader.GetInt32("PowerType");
                    body.records[i].record.DispayPower = reader.GetInt32("DispayPower");
                    body.records[i].FileName = reader.GetString("FileName");
                    body.records[i].record.SpellFamily = reader.GetInt32("SpellFamily");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.CinematicSequence = reader.GetInt32("CinematicSequence");
                    body.records[i].record.Expansion = reader.GetInt32("Expansion");

                    body.records[i].Name = new string[17];
                    body.records[i].Female = new string[17];
                    body.records[i].Male = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.Female = new UInt32[17];
                    body.records[i].record.Male = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name[loc] = "";
                        body.records[i].Female[loc] = "";
                        body.records[i].Male[loc] = ""; }
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");
                    body.records[i].Female[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Female_loc2" : "Female");
                    body.records[i].Male[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Male_loc2" : "Male");

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

                for (UInt32 i = 0; i < header.record_count; ++i) // Generate some string offsets...
                {
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
                        // Female
                        if (body.records[i].Female[j].Length == 0)
                            body.records[i].record.Female[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Female[j])) body.records[i].record.Female[j] = offsetStorage[body.records[i].Female[j]];
                            else {
                                body.records[i].record.Female[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Female[j]) + 1;
                                offsetStorage.Add(body.records[i].Female[j], body.records[i].record.Female[j]);
                                reverseStorage.Add(body.records[i].record.Female[j], body.records[i].Female[j]); } }
                        // Male
                        if (body.records[i].Male[j].Length == 0)
                            body.records[i].record.Male[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Male[j])) body.records[i].record.Male[j] = offsetStorage[body.records[i].Male[j]];
                            else {
                                body.records[i].record.Male[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Male[j]) + 1;
                                offsetStorage.Add(body.records[i].Male[j], body.records[i].record.Male[j]);
                                reverseStorage.Add(body.records[i].record.Male[j], body.records[i].Male[j]); } } }
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
                    count = Marshal.SizeOf(typeof(chrclassesRecord)); // Write main body
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

            return true; } } // chrclasses

    public class chrracesdbc {
        public DBCHeader header;
        public chrracesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM chrracesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Flags, FactionId, Exploration, ModelMale, ModelFemale, ClientPrefix, BaseLanguage, CreatureType, ResSicknessSpellId, SplashSoundId, InternalName, CinematicSequence, TeamId, Name, Name_loc2, NameFemale, NameFemale_loc2, NameMale, NameMale_loc2, FacialHairCustomization1, FacialHairCustomization2, HairCustomization, Expansion FROM chrracesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new chrracesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 69;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(chrracesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.FactionId = reader.GetInt32("FactionId");
                    body.records[i].record.Exploration = reader.GetInt32("Exploration");
                    body.records[i].record.ModelMale = reader.GetInt32("ModelMale");
                    body.records[i].record.ModelFemale = reader.GetInt32("ModelFemale");
                    body.records[i].ClientPrefix = reader.GetString("ClientPrefix");
                    body.records[i].record.BaseLanguage = reader.GetInt32("BaseLanguage");
                    body.records[i].record.CreatureType = reader.GetInt32("CreatureType");
                    body.records[i].record.ResSicknessSpellId = reader.GetInt32("ResSicknessSpellId");
                    body.records[i].record.SplashSoundId = reader.GetInt32("SplashSoundId");
                    body.records[i].InternalName = reader.GetString("InternalName");
                    body.records[i].record.CinematicSequence = reader.GetInt32("CinematicSequence");
                    body.records[i].record.TeamId = reader.GetInt32("TeamId");
                    body.records[i].FacialHairCustomization1 = reader.GetString("FacialHairCustomization1");
                    body.records[i].FacialHairCustomization2 = reader.GetString("FacialHairCustomization2");
                    body.records[i].HairCustomization = reader.GetString("HairCustomization");
                    body.records[i].record.Expansion = reader.GetInt32("Expansion");

                    body.records[i].Name = new string[17];
                    body.records[i].NameFemale = new string[17];
                    body.records[i].NameMale = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.NameFemale = new UInt32[17];
                    body.records[i].record.NameMale = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name[loc] = "";
                        body.records[i].NameFemale[loc] = "";
                        body.records[i].NameMale[loc] = ""; }
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");
                    body.records[i].NameFemale[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "NameFemale_loc2" : "NameFemale");
                    body.records[i].NameMale[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "NameMale_loc2" : "NameMale");

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
                        // NameFemale
                        if (body.records[i].NameFemale[j].Length == 0)
                            body.records[i].record.NameFemale[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].NameFemale[j])) body.records[i].record.NameFemale[j] = offsetStorage[body.records[i].NameFemale[j]];
                            else {
                                body.records[i].record.NameFemale[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].NameFemale[j]) + 1;
                                offsetStorage.Add(body.records[i].NameFemale[j], body.records[i].record.NameFemale[j]);
                                reverseStorage.Add(body.records[i].record.NameFemale[j], body.records[i].NameFemale[j]); } }
                        // NameMale
                        if (body.records[i].NameMale[j].Length == 0)
                            body.records[i].record.NameMale[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].NameMale[j])) body.records[i].record.NameMale[j] = offsetStorage[body.records[i].NameMale[j]];
                            else {
                                body.records[i].record.NameMale[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].NameMale[j]) + 1;
                                offsetStorage.Add(body.records[i].NameMale[j], body.records[i].record.NameMale[j]);
                                reverseStorage.Add(body.records[i].record.NameMale[j], body.records[i].NameMale[j]); } } }
                    // ClientPrefix
                    if (body.records[i].ClientPrefix.Length == 0)
                        body.records[i].record.ClientPrefix = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].ClientPrefix)) body.records[i].record.ClientPrefix = offsetStorage[body.records[i].ClientPrefix];
                        else {
                            body.records[i].record.ClientPrefix = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].ClientPrefix) + 1;
                            offsetStorage.Add(body.records[i].ClientPrefix, body.records[i].record.ClientPrefix);
                            reverseStorage.Add(body.records[i].record.ClientPrefix, body.records[i].ClientPrefix); } }
                    // InternalName
                    if (body.records[i].InternalName.Length == 0)
                        body.records[i].record.InternalName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].InternalName)) body.records[i].record.InternalName = offsetStorage[body.records[i].InternalName];
                        else {
                            body.records[i].record.InternalName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].InternalName) + 1;
                            offsetStorage.Add(body.records[i].InternalName, body.records[i].record.InternalName);
                            reverseStorage.Add(body.records[i].record.InternalName, body.records[i].InternalName); } }
                    // FacialHairCustomization1
                    if (body.records[i].FacialHairCustomization1.Length == 0)
                        body.records[i].record.FacialHairCustomization1 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FacialHairCustomization1)) body.records[i].record.FacialHairCustomization1 = offsetStorage[body.records[i].FacialHairCustomization1];
                        else {
                            body.records[i].record.FacialHairCustomization1 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FacialHairCustomization1) + 1;
                            offsetStorage.Add(body.records[i].FacialHairCustomization1, body.records[i].record.FacialHairCustomization1);
                            reverseStorage.Add(body.records[i].record.FacialHairCustomization1, body.records[i].FacialHairCustomization1); } }
                    // FacialHairCustomization2
                    if (body.records[i].FacialHairCustomization2.Length == 0)
                        body.records[i].record.FacialHairCustomization2 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FacialHairCustomization2)) body.records[i].record.FacialHairCustomization2 = offsetStorage[body.records[i].FacialHairCustomization2];
                        else {
                            body.records[i].record.FacialHairCustomization2 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FacialHairCustomization2) + 1;
                            offsetStorage.Add(body.records[i].FacialHairCustomization2, body.records[i].record.FacialHairCustomization2);
                            reverseStorage.Add(body.records[i].record.FacialHairCustomization2, body.records[i].FacialHairCustomization2); } }
                    // HairCustomization
                    if (body.records[i].HairCustomization.Length == 0)
                        body.records[i].record.HairCustomization = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].HairCustomization)) body.records[i].record.HairCustomization = offsetStorage[body.records[i].HairCustomization];
                        else {
                            body.records[i].record.HairCustomization = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].HairCustomization) + 1;
                            offsetStorage.Add(body.records[i].HairCustomization, body.records[i].record.HairCustomization);
                            reverseStorage.Add(body.records[i].record.HairCustomization, body.records[i].HairCustomization); } } }

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
                    count = Marshal.SizeOf(typeof(chrracesRecord)); // Write main body
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

            return true; } } // chrraces

    public class cinematicsequencesdbc {
        public DBCHeader header;
        public cinematicsequencesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM cinematicsequencesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SoundId, CinematicCamera, Camera1, Camera2, Camera3, Camera4, Camera5, Camera6, Camera7 FROM cinematicsequencesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new cinematicsequencesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 10;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(cinematicsequencesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SoundId = reader.GetInt32("SoundId");
                    body.records[i].record.CinematicCamera = reader.GetInt32("CinematicCamera");
                    body.records[i].record.Camera1 = reader.GetInt32("Camera1");
                    body.records[i].record.Camera2 = reader.GetInt32("Camera2");
                    body.records[i].record.Camera3 = reader.GetInt32("Camera3");
                    body.records[i].record.Camera4 = reader.GetInt32("Camera4");
                    body.records[i].record.Camera5 = reader.GetInt32("Camera5");
                    body.records[i].record.Camera6 = reader.GetInt32("Camera6");
                    body.records[i].record.Camera7 = reader.GetInt32("Camera7");
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
                    count = Marshal.SizeOf(typeof(cinematicsequencesRecord)); // Write main body
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

            return true; } } // cinematicsequences

    public class creaturedisplayinfodbc {
        public DBCHeader header;
        public creaturedisplayinfoBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM creaturedisplayinfodbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, ModelId, Sound, ExtraId, Scale, Opacity, Skin1, Skin2, Skin3, PortraitTextureName, BloodLevel, Blood, NPCSounds, Particles, CreatureGeoosetData, ObjectEffectPackageId FROM creaturedisplayinfodbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new creaturedisplayinfoMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 16;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(creaturedisplayinfoRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.ModelId = reader.GetInt32("ModelId");
                    body.records[i].record.Sound = reader.GetInt32("Sound");
                    body.records[i].record.ExtraId = reader.GetInt32("ExtraId");
                    body.records[i].record.Scale = reader.GetFloat("Scale");
                    body.records[i].record.Opacity = reader.GetInt32("Opacity");
                    body.records[i].Skin1 = reader.GetString("Skin1");
                    body.records[i].Skin2 = reader.GetString("Skin2");
                    body.records[i].Skin3 = reader.GetString("Skin3");
                    body.records[i].PortraitTextureName = reader.GetString("PortraitTextureName");
                    body.records[i].record.BloodLevel = reader.GetInt32("BloodLevel");
                    body.records[i].record.Blood = reader.GetInt32("Blood");
                    body.records[i].record.NPCSounds = reader.GetInt32("NPCSounds");
                    body.records[i].record.Particles = reader.GetInt32("Particles");
                    body.records[i].record.CreatureGeoosetData = reader.GetInt32("CreatureGeoosetData");
                    body.records[i].record.ObjectEffectPackageId = reader.GetInt32("ObjectEffectPackageId");
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
                    // Skin1
                    if (body.records[i].Skin1.Length == 0)
                        body.records[i].record.Skin1 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Skin1)) body.records[i].record.Skin1 = offsetStorage[body.records[i].Skin1];
                        else {
                            body.records[i].record.Skin1 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Skin1) + 1;
                            offsetStorage.Add(body.records[i].Skin1, body.records[i].record.Skin1);
                            reverseStorage.Add(body.records[i].record.Skin1, body.records[i].Skin1); } }
                    // Skin2
                    if (body.records[i].Skin2.Length == 0)
                        body.records[i].record.Skin2 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Skin2)) body.records[i].record.Skin2 = offsetStorage[body.records[i].Skin2];
                        else {
                            body.records[i].record.Skin2 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Skin2) + 1;
                            offsetStorage.Add(body.records[i].Skin2, body.records[i].record.Skin2);
                            reverseStorage.Add(body.records[i].record.Skin2, body.records[i].Skin2); } }
                    // Skin3
                    if (body.records[i].Skin3.Length == 0)
                        body.records[i].record.Skin3 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Skin3)) body.records[i].record.Skin3 = offsetStorage[body.records[i].Skin3];
                        else {
                            body.records[i].record.Skin3 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Skin3) + 1;
                            offsetStorage.Add(body.records[i].Skin3, body.records[i].record.Skin3);
                            reverseStorage.Add(body.records[i].record.Skin3, body.records[i].Skin3); } }
                    // PortraitTextureName
                    if (body.records[i].PortraitTextureName.Length == 0)
                        body.records[i].record.PortraitTextureName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].PortraitTextureName)) body.records[i].record.PortraitTextureName = offsetStorage[body.records[i].PortraitTextureName];
                        else {
                            body.records[i].record.PortraitTextureName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].PortraitTextureName) + 1;
                            offsetStorage.Add(body.records[i].PortraitTextureName, body.records[i].record.PortraitTextureName);
                            reverseStorage.Add(body.records[i].record.PortraitTextureName, body.records[i].PortraitTextureName); } } }

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
                    count = Marshal.SizeOf(typeof(creaturedisplayinfoRecord)); // Write main body
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

            return true; } } // creaturedisplayinfo

    public class creaturedisplayinfoextradbc {
        public DBCHeader header;
        public creaturedisplayinfoextraBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM creaturedisplayinfoextradbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Race, Gender, SkinColor, FaceType, HairType, HairStyle, FacialHair, HelmDisplayId, ShoulderDisplayId, ShirtDisplayId, ChestDisplayId, BeltDisplayId, LegsDisplayId, BootsDisplayId, WristDisplayId, GlovesDisplayId, TabardDisplayId, CloakDisplayId, CanEquip, Texture FROM creaturedisplayinfoextradbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new creaturedisplayinfoextraMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 21;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(creaturedisplayinfoextraRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Race = reader.GetInt32("Race");
                    body.records[i].record.Gender = reader.GetInt32("Gender");
                    body.records[i].record.SkinColor = reader.GetInt32("SkinColor");
                    body.records[i].record.FaceType = reader.GetInt32("FaceType");
                    body.records[i].record.HairType = reader.GetInt32("HairType");
                    body.records[i].record.HairStyle = reader.GetInt32("HairStyle");
                    body.records[i].record.FacialHair = reader.GetInt32("FacialHair");
                    body.records[i].record.HelmDisplayId = reader.GetInt32("HelmDisplayId");
                    body.records[i].record.ShoulderDisplayId = reader.GetInt32("ShoulderDisplayId");
                    body.records[i].record.ShirtDisplayId = reader.GetInt32("ShirtDisplayId");
                    body.records[i].record.ChestDisplayId = reader.GetInt32("ChestDisplayId");
                    body.records[i].record.BeltDisplayId = reader.GetInt32("BeltDisplayId");
                    body.records[i].record.LegsDisplayId = reader.GetInt32("LegsDisplayId");
                    body.records[i].record.BootsDisplayId = reader.GetInt32("BootsDisplayId");
                    body.records[i].record.WristDisplayId = reader.GetInt32("WristDisplayId");
                    body.records[i].record.GlovesDisplayId = reader.GetInt32("GlovesDisplayId");
                    body.records[i].record.TabardDisplayId = reader.GetInt32("TabardDisplayId");
                    body.records[i].record.CloakDisplayId = reader.GetInt32("CloakDisplayId");
                    body.records[i].record.CanEquip = reader.GetInt32("CanEquip");
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
                        if (offsetStorage.ContainsKey(body.records[i].Texture))
                            body.records[i].record.Texture = offsetStorage[body.records[i].Texture];
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
                    count = Marshal.SizeOf(typeof(creaturedisplayinfoextraRecord)); // Write main body
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

            return true; } } // creaturedisplayinfoextra

    public class creaturefamilydbc {
        public DBCHeader header;
        public creaturefamilyBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM creaturefamilydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, MinScale, MinScaleLevel, MaxScale, MaxScaleLevel, SkillLine_F6, SkillLine_F7, PetFoodMask, PetTalentType, CategoryEnumID, Name, Name_loc2, IconFile FROM creaturefamilydbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new creaturefamilyMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 28;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(creaturefamilyRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MinScale = reader.GetFloat("MinScale");
                    body.records[i].record.MinScaleLevel = reader.GetInt32("MinScaleLevel");
                    body.records[i].record.MaxScale = reader.GetFloat("MaxScale");
                    body.records[i].record.MaxScaleLevel = reader.GetInt32("MaxScaleLevel");
                    body.records[i].record.SkillLine_F6 = reader.GetInt32("SkillLine_F6");
                    body.records[i].record.SkillLine_F7 = reader.GetInt32("SkillLine_F7");
                    body.records[i].record.PetFoodMask = reader.GetInt32("PetFoodMask");
                    body.records[i].record.PetTalentType = reader.GetInt32("PetTalentType");
                    body.records[i].record.CategoryEnumID = reader.GetInt32("CategoryEnumID");
                    body.records[i].IconFile = reader.GetString("IconFile");

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
                    // IconFile
                    if (body.records[i].IconFile.Length == 0)
                        body.records[i].record.IconFile = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].IconFile)) body.records[i].record.IconFile = offsetStorage[body.records[i].IconFile];
                        else {
                            body.records[i].record.IconFile = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].IconFile) + 1;
                            offsetStorage.Add(body.records[i].IconFile, body.records[i].record.IconFile);
                            reverseStorage.Add(body.records[i].record.IconFile, body.records[i].IconFile); } } }

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
                    count = Marshal.SizeOf(typeof(creaturefamilyRecord)); // Write main body
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

            return true; } } // creaturefamily

    public class creaturemodeldatadbc {
        public DBCHeader header;
        public creaturemodeldataBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM creaturemodeldatadbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Flags,Model, SizeClass, Scale, BloodLevel, FootprintTexture, FootprintTextureLength, FootprintTextureWidth, FootprintParticleScale, FoleyMaterialId, FootstepShakeSize, DeathThudShakeSize, Sound, CollisionWidth, CollisionHeight, MountHeight, GeoBoxMin1, GeoBoxMin2, GeoBoxMin3, GeoBoxMax1, GeoBoxMax2, GeoBoxMax3, WorldEffectScale, AttachedEffectScale, MissileCollisionRadius, MissileCollisionPush, MissileCollisionRaise FROM creaturemodeldatadbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new creaturemodeldataMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 28;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(creaturemodeldataRecord));

                UInt32 i = 0;
                while (reader.Read())
                { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].Model = reader.GetString("Model");
                    body.records[i].record.SizeClass = reader.GetInt32("SizeClass");
                    body.records[i].record.Scale = reader.GetFloat("Scale");
                    body.records[i].record.BloodLevel = reader.GetInt32("BloodLevel");
                    body.records[i].record.FootprintTexture = reader.GetInt32("FootprintTexture");
                    body.records[i].record.FootprintTextureLength = reader.GetFloat("FootprintTextureLength");
                    body.records[i].record.FootprintTextureWidth = reader.GetFloat("FootprintTextureWidth");
                    body.records[i].record.FootprintParticleScale = reader.GetFloat("FootprintParticleScale");
                    body.records[i].record.FoleyMaterialId = reader.GetInt32("FoleyMaterialId");
                    body.records[i].record.FootstepShakeSize = reader.GetInt32("FootstepShakeSize");
                    body.records[i].record.DeathThudShakeSize = reader.GetInt32("DeathThudShakeSize");
                    body.records[i].record.Sound = reader.GetInt32("Sound");
                    body.records[i].record.CollisionWidth = reader.GetFloat("CollisionWidth");
                    body.records[i].record.CollisionHeight = reader.GetFloat("CollisionHeight");
                    body.records[i].record.MountHeight = reader.GetFloat("MountHeight");
                    body.records[i].record.GeoBoxMin1 = reader.GetFloat("GeoBoxMin1");
                    body.records[i].record.GeoBoxMin2 = reader.GetFloat("GeoBoxMin2");
                    body.records[i].record.GeoBoxMin3 = reader.GetFloat("GeoBoxMin3");
                    body.records[i].record.GeoBoxMax1 = reader.GetFloat("GeoBoxMax1");
                    body.records[i].record.GeoBoxMax2 = reader.GetFloat("GeoBoxMax2");
                    body.records[i].record.GeoBoxMax3 = reader.GetFloat("GeoBoxMax3");
                    body.records[i].record.WorldEffectScale = reader.GetFloat("WorldEffectScale");
                    body.records[i].record.AttachedEffectScale = reader.GetFloat("AttachedEffectScale");
                    body.records[i].record.MissileCollisionRadius = reader.GetFloat("MissileCollisionRadius");
                    body.records[i].record.MissileCollisionPush = reader.GetInt32("MissileCollisionPush");
                    body.records[i].record.MissileCollisionRaise = reader.GetInt32("MissileCollisionRaise");
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
                    // ModelPath
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
                    count = Marshal.SizeOf(typeof(creaturemodeldataRecord)); // Write main body
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

            return true; } } // creaturemodeldata

    public class creaturespelldatadbc {
        public DBCHeader header;
        public creaturespelldataBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM creaturespelldatadbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SpellId1, SpellId2, SpellId3, SpellId4, Availability1, Availability2, Availability3, Availability4 FROM creaturespelldatadbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new creaturespelldataMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 9;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(creaturespelldataRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SpellId1 = reader.GetInt32("SpellId1");
                    body.records[i].record.SpellId2 = reader.GetInt32("SpellId2");
                    body.records[i].record.SpellId3 = reader.GetInt32("SpellId3");
                    body.records[i].record.SpellId4 = reader.GetInt32("SpellId4");
                    body.records[i].record.Availability1 = reader.GetInt32("Availability1");
                    body.records[i].record.Availability2 = reader.GetInt32("Availability2");
                    body.records[i].record.Availability3 = reader.GetInt32("Availability3");
                    body.records[i].record.Availability4 = reader.GetInt32("Availability4");
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
                    count = Marshal.SizeOf(typeof(creaturespelldataRecord)); // Write main body
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

            return true; } } // creaturespelldata

    public class creaturetypedbc {
        public DBCHeader header;
        public creaturetypeBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM creaturetypedbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, NoExperience FROM creaturetypedbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new creaturetypeMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 19;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(creaturetypeRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.NoExperience = reader.GetInt32("NoExperience");

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
                    count = Marshal.SizeOf(typeof(creaturetypeRecord)); // Write main body
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

            return true; } } // creaturetype

    public class currencytypesdbc {
        public DBCHeader header;
        public currencytypesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM currencytypesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, ItemId, Category, BitIndex FROM currencytypesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new currencytypesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(currencytypesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.ItemId = reader.GetInt32("ItemId");
                    body.records[i].record.Category = reader.GetInt32("Category");
                    body.records[i].record.BitIndex = reader.GetInt32("BitIndex");
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
                    count = Marshal.SizeOf(typeof(currencytypesRecord)); // Write main body
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

            return true; } } // currencytypes

    public class destructiblemodeldatadbc {
        public DBCHeader header;
        public destructiblemodeldataBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM destructiblemodeldatadbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, DamagedUnk1, DamagedUnk2, DamagedDisplayId, DamagedUnk3, DestroyedUnk1, DestroyedUnk2, DestroyedDisplayId, DestroyedUnk3, RebuildingUnk1, RebuildingUnk2, RebuildingDisplayId, RebuildingUnk3, SmokeUnk1, SmokeUnk2, SmokeDisplayId, SmokeUnk3, Unk4, Unk5 FROM destructiblemodeldatadbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new destructiblemodeldataMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 19;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(destructiblemodeldataRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.DamagedUnk1 = reader.GetInt32("DamagedUnk1");
                    body.records[i].record.DamagedUnk2 = reader.GetInt32("DamagedUnk2");
                    body.records[i].record.DamagedDisplayId = reader.GetInt32("DamagedDisplayId");
                    body.records[i].record.DamagedUnk3 = reader.GetInt32("DamagedUnk3");
                    body.records[i].record.DestroyedUnk1 = reader.GetInt32("DestroyedUnk1");
                    body.records[i].record.DestroyedUnk2 = reader.GetInt32("DestroyedUnk2");
                    body.records[i].record.DestroyedDisplayId = reader.GetInt32("DestroyedDisplayId");
                    body.records[i].record.DestroyedUnk3 = reader.GetInt32("DestroyedUnk3");
                    body.records[i].record.RebuildingUnk1 = reader.GetInt32("RebuildingUnk1");
                    body.records[i].record.RebuildingUnk2 = reader.GetInt32("RebuildingUnk2");
                    body.records[i].record.RebuildingDisplayId = reader.GetInt32("RebuildingDisplayId");
                    body.records[i].record.RebuildingUnk3 = reader.GetInt32("RebuildingUnk3");
                    body.records[i].record.SmokeUnk1 = reader.GetInt32("SmokeUnk1");
                    body.records[i].record.SmokeUnk2 = reader.GetInt32("SmokeUnk2");
                    body.records[i].record.SmokeDisplayId = reader.GetInt32("SmokeDisplayId");
                    body.records[i].record.SmokeUnk3 = reader.GetInt32("SmokeUnk3");
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
                    count = Marshal.SizeOf(typeof(destructiblemodeldataRecord)); // Write main body
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

            return true; } } // destructiblemodeldata

    public class dungeonencounterdbc {
        public DBCHeader header;
        public dungeonencounterBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM dungeonencounterdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, MapId, Difficulty, OrderIndex, EncounterIndex, EncounterName, EncounterName_loc2, SpellIconId FROM dungeonencounterdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new dungeonencounterMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 23;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(dungeonencounterRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.Difficulty = reader.GetInt32("Difficulty");
                    body.records[i].record.OrderIndex = reader.GetInt32("OrderIndex");
                    body.records[i].record.EncounterIndex = reader.GetInt32("EncounterIndex");
                    body.records[i].record.SpellIconId = reader.GetInt32("SpellIconId");

                    body.records[i].EncounterName = new string[17];
                    body.records[i].record.EncounterName = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) 
                        body.records[i].EncounterName[loc] = ""; 
                    body.records[i].EncounterName[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "EncounterName_loc2" : "EncounterName");

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
                        // EncounterName
                        if (body.records[i].EncounterName[j].Length == 0)
                            body.records[i].record.EncounterName[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].EncounterName[j])) body.records[i].record.EncounterName[j] = offsetStorage[body.records[i].EncounterName[j]];
                            else {
                                body.records[i].record.EncounterName[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].EncounterName[j]) + 1;
                                offsetStorage.Add(body.records[i].EncounterName[j], body.records[i].record.EncounterName[j]);
                                reverseStorage.Add(body.records[i].record.EncounterName[j], body.records[i].EncounterName[j]); } } }

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
                    count = Marshal.SizeOf(typeof(dungeonencounterRecord)); // Write main body
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

            return true; } } // dungeonencounter

    public class durabilitycostsdbc {
        public DBCHeader header;
        public durabilitycostsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM durabilitycostsdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT ItemLevel, Multiplier1, Multiplier2, Multiplier3, Multiplier4, Multiplier5, Multiplier6, Multiplier7, Multiplier8, Multiplier9, Multiplier10, Multiplier11, Multiplier12, Multiplier13, Multiplier14, Multiplier15, Multiplier16, Multiplier17, Multiplier18, Multiplier19, Multiplier20, Multiplier21, Multiplier22, Multiplier23, Multiplier24, Multiplier25, Multiplier26, Multiplier27, Multiplier28, Multiplier29 FROM durabilitycostsdbc ORDER BY ItemLevel ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new durabilitycostsMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 30;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(durabilitycostsRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.ItemLevel = reader.GetInt32("ItemLevel");
                    body.records[i].record.Multiplier1 = reader.GetInt32("Multiplier1");
                    body.records[i].record.Multiplier2 = reader.GetInt32("Multiplier2");
                    body.records[i].record.Multiplier3 = reader.GetInt32("Multiplier3");
                    body.records[i].record.Multiplier4 = reader.GetInt32("Multiplier4");
                    body.records[i].record.Multiplier5 = reader.GetInt32("Multiplier5");
                    body.records[i].record.Multiplier6 = reader.GetInt32("Multiplier6");
                    body.records[i].record.Multiplier7 = reader.GetInt32("Multiplier7");
                    body.records[i].record.Multiplier8 = reader.GetInt32("Multiplier8");
                    body.records[i].record.Multiplier9 = reader.GetInt32("Multiplier9");
                    body.records[i].record.Multiplier10 = reader.GetInt32("Multiplier10");
                    body.records[i].record.Multiplier11 = reader.GetInt32("Multiplier11");
                    body.records[i].record.Multiplier12 = reader.GetInt32("Multiplier12");
                    body.records[i].record.Multiplier13 = reader.GetInt32("Multiplier13");
                    body.records[i].record.Multiplier14 = reader.GetInt32("Multiplier14");
                    body.records[i].record.Multiplier15 = reader.GetInt32("Multiplier15");
                    body.records[i].record.Multiplier16 = reader.GetInt32("Multiplier16");
                    body.records[i].record.Multiplier17 = reader.GetInt32("Multiplier17");
                    body.records[i].record.Multiplier18 = reader.GetInt32("Multiplier18");
                    body.records[i].record.Multiplier19 = reader.GetInt32("Multiplier19");
                    body.records[i].record.Multiplier20 = reader.GetInt32("Multiplier20");
                    body.records[i].record.Multiplier21 = reader.GetInt32("Multiplier21");
                    body.records[i].record.Multiplier22 = reader.GetInt32("Multiplier22");
                    body.records[i].record.Multiplier23 = reader.GetInt32("Multiplier23");
                    body.records[i].record.Multiplier24 = reader.GetInt32("Multiplier24");
                    body.records[i].record.Multiplier25 = reader.GetInt32("Multiplier25");
                    body.records[i].record.Multiplier26 = reader.GetInt32("Multiplier26");
                    body.records[i].record.Multiplier27 = reader.GetInt32("Multiplier27");
                    body.records[i].record.Multiplier28 = reader.GetInt32("Multiplier28");
                    body.records[i].record.Multiplier29 = reader.GetInt32("Multiplier29");
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
                    count = Marshal.SizeOf(typeof(durabilitycostsRecord)); // Write main body
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

            return true; } } // durabilitycosts

    public class durabilityqualitydbc {
        public DBCHeader header;
        public durabilityqualityBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM durabilityqualitydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, QualityMod FROM durabilityqualitydbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new durabilityqualityMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(durabilityqualityRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.QualityMod = reader.GetFloat("QualityMod");
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
                    count = Marshal.SizeOf(typeof(durabilityqualityRecord)); // Write main body
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

            return true; } } // durabilityquality

    public class emotesdbc {
        public DBCHeader header;
        public emotesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM emotesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, AnimationId, Flags, EmoteType, UnitStandState, SoundId FROM emotesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new emotesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 7;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(emotesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.AnimationId = reader.GetInt32("AnimationId");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.EmoteType = reader.GetInt32("EmoteType");
                    body.records[i].record.UnitStandState = reader.GetInt32("UnitStandState");
                    body.records[i].record.SoundId = reader.GetInt32("SoundId");
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
                    count = Marshal.SizeOf(typeof(emotesRecord)); // Write main body
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

            return true; } } // emotes

    public class emotestextdbc {
        public DBCHeader header;
        public emotestextBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM emotestextdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, EmoteId, TextData1, TextData2, TextData3, TextData4, TextData5, TextData6, TextData7, TextData8, TextData9, TextData10, TextData11, TextData12, TextData13, TextData14, TextData15, TextData16 FROM emotestextdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new emotestextMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 19;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(emotestextRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.EmoteId = reader.GetUInt32("EmoteId");
                    body.records[i].record.TextData1 = reader.GetInt32("TextData1");
                    body.records[i].record.TextData2 = reader.GetInt32("TextData2");
                    body.records[i].record.TextData3 = reader.GetInt32("TextData3");
                    body.records[i].record.TextData4 = reader.GetInt32("TextData4");
                    body.records[i].record.TextData5 = reader.GetInt32("TextData5");
                    body.records[i].record.TextData6 = reader.GetInt32("TextData6");
                    body.records[i].record.TextData7 = reader.GetInt32("TextData7");
                    body.records[i].record.TextData8 = reader.GetInt32("TextData8");
                    body.records[i].record.TextData9 = reader.GetInt32("TextData9");
                    body.records[i].record.TextData10 = reader.GetInt32("TextData10");
                    body.records[i].record.TextData11 = reader.GetInt32("TextData11");
                    body.records[i].record.TextData12 = reader.GetInt32("TextData12");
                    body.records[i].record.TextData13 = reader.GetInt32("TextData13");
                    body.records[i].record.TextData14 = reader.GetInt32("TextData14");
                    body.records[i].record.TextData15 = reader.GetInt32("TextData15");
                    body.records[i].record.TextData16 = reader.GetInt32("TextData16");
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
                            reverseStorage.Add(body.records[i].record.Name, body.records[i].Name);
                        }
                    }
                }

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
                    count = Marshal.SizeOf(typeof(emotestextRecord)); // Write main body
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

            return true; } } // emotestext

    public class factiondbc {
        public DBCHeader header;
        public factionBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM factiondbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, ReputationListId, BaseRepRaceMask1, BaseRepRaceMask2, BaseRepRaceMask3, BaseRepRaceMask4, BaseRepClassMask1, BaseRepClassMask2, BaseRepClassMask3, BaseRepClassMask4, BaseRepValue1, BaseRepValue2, BaseRepValue3, BaseRepValue4, ReputationFlags1, ReputationFlags2, ReputationFlags3, ReputationFlags4, Team, SpilloverRateIn, SpilloverRateOut, SpilloverMaxRankIn, SpilloverRankUnk, Name, Name_loc2, Description, Description_loc2 FROM factiondbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new factionMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 57;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(factionRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.ReputationListId = reader.GetInt32("ReputationListId");
                    body.records[i].record.BaseRepRaceMask1 = reader.GetInt32("BaseRepRaceMask1");
                    body.records[i].record.BaseRepRaceMask2 = reader.GetInt32("BaseRepRaceMask2");
                    body.records[i].record.BaseRepRaceMask3 = reader.GetInt32("BaseRepRaceMask3");
                    body.records[i].record.BaseRepRaceMask4 = reader.GetInt32("BaseRepRaceMask4");
                    body.records[i].record.BaseRepClassMask1 = reader.GetInt32("BaseRepClassMask1");
                    body.records[i].record.BaseRepClassMask2 = reader.GetInt32("BaseRepClassMask2");
                    body.records[i].record.BaseRepClassMask3 = reader.GetInt32("BaseRepClassMask3");
                    body.records[i].record.BaseRepClassMask4 = reader.GetInt32("BaseRepClassMask4");
                    body.records[i].record.BaseRepValue1 = reader.GetInt32("BaseRepValue1");
                    body.records[i].record.BaseRepValue2 = reader.GetInt32("BaseRepValue2");
                    body.records[i].record.BaseRepValue3 = reader.GetInt32("BaseRepValue3");
                    body.records[i].record.BaseRepValue4 = reader.GetInt32("BaseRepValue4");
                    body.records[i].record.ReputationFlags1 = reader.GetInt32("ReputationFlags1");
                    body.records[i].record.ReputationFlags2 = reader.GetInt32("ReputationFlags2");
                    body.records[i].record.ReputationFlags3 = reader.GetInt32("ReputationFlags3");
                    body.records[i].record.ReputationFlags4 = reader.GetInt32("ReputationFlags4");
                    body.records[i].record.Team = reader.GetInt32("Team");
                    body.records[i].record.SpilloverRateIn = reader.GetFloat("SpilloverRateIn");
                    body.records[i].record.SpilloverRateOut = reader.GetFloat("SpilloverRateOut");
                    body.records[i].record.SpilloverMaxRankIn = reader.GetInt32("SpilloverMaxRankIn");
                    body.records[i].record.SpilloverRankUnk = reader.GetInt32("SpilloverRankUnk");

                    body.records[i].Name = new string[17];
                    body.records[i].Description = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.Description = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name[loc] = "";
                        body.records[i].Description[loc] = ""; }
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");
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
                    count = Marshal.SizeOf(typeof(factionRecord)); // Write main body
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

            return true; } } // faction

    public class factiontemplatedbc {
        public DBCHeader header;
        public factiontemplateBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM factiontemplatedbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Faction, FactionFlags, OurMask, FriendlyMask, HostileMask, EnemyFaction1, EnemyFaction2, EnemyFaction3, EnemyFaction4, FriendFaction1, FriendFaction2, FriendFaction3, FriendFaction4 FROM factiontemplatedbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new factiontemplateMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 14;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(factiontemplateRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Faction = reader.GetInt32("Faction");
                    body.records[i].record.FactionFlags = reader.GetInt32("FactionFlags");
                    body.records[i].record.OurMask = reader.GetInt32("OurMask");
                    body.records[i].record.FriendlyMask = reader.GetInt32("FriendlyMask");
                    body.records[i].record.HostileMask = reader.GetInt32("HostileMask");
                    body.records[i].record.EnemyFaction1 = reader.GetInt32("EnemyFaction1");
                    body.records[i].record.EnemyFaction2 = reader.GetInt32("EnemyFaction2");
                    body.records[i].record.EnemyFaction3 = reader.GetInt32("EnemyFaction3");
                    body.records[i].record.EnemyFaction4 = reader.GetInt32("EnemyFaction4");
                    body.records[i].record.FriendFaction1 = reader.GetInt32("FriendFaction1");
                    body.records[i].record.FriendFaction2 = reader.GetInt32("FriendFaction2");
                    body.records[i].record.FriendFaction3 = reader.GetInt32("FriendFaction3");
                    body.records[i].record.FriendFaction4 = reader.GetInt32("FriendFaction4");
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
                    count = Marshal.SizeOf(typeof(factiontemplateRecord)); // Write main body
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

            return true; } } // factiontemplate

    public class gameobjectdisplayinfodbc {
        public DBCHeader header;
        public gameobjectdisplayinfoBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gameobjectdisplayinfodbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, FileName, Unk1, Unk2, Unk3, Unk4, Unk5, Unk6, Unk7, Unk8, Unk9, Unk10, MinX, MinY, MinZ, MaxX, MaxY, MaxZ, Transport FROM gameobjectdisplayinfodbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new gameobjectdisplayinfoMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 19;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gameobjectdisplayinfoRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].FileName = reader.GetString("FileName");
                    body.records[i].record.Unk1 = reader.GetInt32("Unk1");
                    body.records[i].record.Unk2 = reader.GetInt32("Unk2");
                    body.records[i].record.Unk3 = reader.GetInt32("Unk3");
                    body.records[i].record.Unk4 = reader.GetInt32("Unk4");
                    body.records[i].record.Unk5 = reader.GetInt32("Unk5");
                    body.records[i].record.Unk6 = reader.GetInt32("Unk6");
                    body.records[i].record.Unk7 = reader.GetInt32("Unk7");
                    body.records[i].record.Unk8 = reader.GetInt32("Unk8");
                    body.records[i].record.Unk9 = reader.GetInt32("Unk9");
                    body.records[i].record.Unk10 = reader.GetInt32("Unk10");
                    body.records[i].record.MinX = reader.GetFloat("MinX");
                    body.records[i].record.MinY = reader.GetFloat("MinY");
                    body.records[i].record.MinZ = reader.GetFloat("MinZ");
                    body.records[i].record.MaxX = reader.GetFloat("MaxX");
                    body.records[i].record.MaxY = reader.GetFloat("MaxY");
                    body.records[i].record.MaxZ = reader.GetFloat("MaxZ");
                    body.records[i].record.Transport = reader.GetInt32("Transport");
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
                    count = Marshal.SizeOf(typeof(gameobjectdisplayinfoRecord)); // Write main body
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

            return true; } } // gameobjectdisplayinfo

    public class gempropertiesdbc {
        public DBCHeader header;
        public gempropertiesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM gempropertiesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SpellItemEnchantment, MaxCountInventory, MaxCountItem, Color FROM gempropertiesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new gempropertiesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(gempropertiesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SpellItemEnchantment = reader.GetInt32("SpellItemEnchantment");
                    body.records[i].record.MaxCountInventory = reader.GetInt32("MaxCountInventory");
                    body.records[i].record.MaxCountItem = reader.GetInt32("MaxCountItem");
                    body.records[i].record.Color = reader.GetInt32("Color");
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
                    count = Marshal.SizeOf(typeof(gempropertiesRecord)); // Write main body
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

            return true; } } // gemproperties

    public class glyphpropertiesdbc {
        public DBCHeader header;
        public glyphpropertiesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM glyphpropertiesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SpellId, TypeFlags, Unk1 FROM glyphpropertiesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new glyphpropertiesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(glyphpropertiesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SpellId = reader.GetInt32("SpellId");
                    body.records[i].record.TypeFlags = reader.GetInt32("TypeFlags");
                    body.records[i].record.Unk1 = reader.GetInt32("Unk1");
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
                    count = Marshal.SizeOf(typeof(glyphpropertiesRecord)); // Write main body
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

            return true; } } // glyphproperties

    public class glyphslotdbc {
        public DBCHeader header;
        public glyphslotBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM glyphslotdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, TypeFlags, `Order` FROM glyphslotdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new glyphslotMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(glyphslotRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.TypeFlags = reader.GetInt32("TypeFlags");
                    body.records[i].record.Order = reader.GetInt32("Order");
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
                    count = Marshal.SizeOf(typeof(glyphslotRecord)); // Write main body
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

            return true; } } // glyphslot

    public class holidaysdbc {
        public DBCHeader header;
        public holidaysBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM holidaysdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Duration1, Duration2, Duration3, Duration4, Duration5, Duration6, Duration7, Duration8, Duration9, Duration10, Date1, Date2, Date3, Date4, Date5, Date6, Date7, Date8, Date9, Date10, Date11, Date12, Date13, Date14, Date15, Date16, Date17, Date18, Date19, Date20, Date21, Date22, Date23, Date24, Date25, Date26, Region, Looping, CalendarFlags1, CalendarFlags2, CalendarFlags3, CalendarFlags4, CalendarFlags5, CalendarFlags6, CalendarFlags7, CalendarFlags8, CalendarFlags9, CalendarFlags10, HolidayNameId, HolidayDescriptionId, TextureFilename, Priority, CalendarFilterType, Flags FROM holidaysdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new holidaysMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 55;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(holidaysRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Duration1 = reader.GetInt32("Duration1");
                    body.records[i].record.Duration2 = reader.GetInt32("Duration2");
                    body.records[i].record.Duration3 = reader.GetInt32("Duration3");
                    body.records[i].record.Duration4 = reader.GetInt32("Duration4");
                    body.records[i].record.Duration5 = reader.GetInt32("Duration5");
                    body.records[i].record.Duration6 = reader.GetInt32("Duration6");
                    body.records[i].record.Duration7 = reader.GetInt32("Duration7");
                    body.records[i].record.Duration8 = reader.GetInt32("Duration8");
                    body.records[i].record.Duration9 = reader.GetInt32("Duration9");
                    body.records[i].record.Duration10 = reader.GetInt32("Duration10");
                    body.records[i].record.Date1 = reader.GetFloat("Date1");
                    body.records[i].record.Date2 = reader.GetFloat("Date2");
                    body.records[i].record.Date3 = reader.GetFloat("Date3");
                    body.records[i].record.Date4 = reader.GetFloat("Date4");
                    body.records[i].record.Date5 = reader.GetFloat("Date5");
                    body.records[i].record.Date6 = reader.GetFloat("Date6");
                    body.records[i].record.Date7 = reader.GetFloat("Date7");
                    body.records[i].record.Date8 = reader.GetFloat("Date8");
                    body.records[i].record.Date9 = reader.GetFloat("Date9");
                    body.records[i].record.Date10 = reader.GetFloat("Date10");
                    body.records[i].record.Date11 = reader.GetFloat("Date11");
                    body.records[i].record.Date12 = reader.GetFloat("Date12");
                    body.records[i].record.Date13 = reader.GetFloat("Date13");
                    body.records[i].record.Date14 = reader.GetInt32("Date14");
                    body.records[i].record.Date15 = reader.GetInt32("Date15");
                    body.records[i].record.Date16 = reader.GetInt32("Date16");
                    body.records[i].record.Date17 = reader.GetInt32("Date17");
                    body.records[i].record.Date18 = reader.GetInt32("Date18");
                    body.records[i].record.Date19 = reader.GetInt32("Date19");
                    body.records[i].record.Date20 = reader.GetInt32("Date20");
                    body.records[i].record.Date21 = reader.GetInt32("Date21");
                    body.records[i].record.Date22 = reader.GetInt32("Date22");
                    body.records[i].record.Date23 = reader.GetInt32("Date23");
                    body.records[i].record.Date24 = reader.GetInt32("Date24");
                    body.records[i].record.Date25 = reader.GetInt32("Date25");
                    body.records[i].record.Date26 = reader.GetInt32("Date26");
                    body.records[i].record.Region = reader.GetInt32("Region");
                    body.records[i].record.Looping = reader.GetInt32("Looping");
                    body.records[i].record.CalendarFlags1 = reader.GetInt32("CalendarFlags1");
                    body.records[i].record.CalendarFlags2 = reader.GetInt32("CalendarFlags2");
                    body.records[i].record.CalendarFlags3 = reader.GetInt32("CalendarFlags3");
                    body.records[i].record.CalendarFlags4 = reader.GetInt32("CalendarFlags4");
                    body.records[i].record.CalendarFlags5 = reader.GetInt32("CalendarFlags5");
                    body.records[i].record.CalendarFlags6 = reader.GetInt32("CalendarFlags6");
                    body.records[i].record.CalendarFlags7 = reader.GetInt32("CalendarFlags7");
                    body.records[i].record.CalendarFlags8 = reader.GetInt32("CalendarFlags8");
                    body.records[i].record.CalendarFlags9 = reader.GetInt32("CalendarFlags9");
                    body.records[i].record.CalendarFlags10 = reader.GetInt32("CalendarFlags10");
                    body.records[i].record.HolidayNameId = reader.GetInt32("HolidayNameId");
                    body.records[i].record.HolidayDescriptionId = reader.GetInt32("HolidayDescriptionId");
                    body.records[i].TextureFilename = reader.GetString("TextureFilename");
                    body.records[i].record.Priority = reader.GetInt32("Priority");
                    body.records[i].record.CalendarFilterType = reader.GetInt32("CalendarFilterType");
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
                    // TextureFilename
                    if (body.records[i].TextureFilename.Length == 0)
                        body.records[i].record.TextureFilename = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].TextureFilename)) body.records[i].record.TextureFilename = offsetStorage[body.records[i].TextureFilename];
                        else {
                            body.records[i].record.TextureFilename = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].TextureFilename) + 1;
                            offsetStorage.Add(body.records[i].TextureFilename, body.records[i].record.TextureFilename);
                            reverseStorage.Add(body.records[i].record.TextureFilename, body.records[i].TextureFilename); } } }

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
                    count = Marshal.SizeOf(typeof(holidaysRecord)); // Write main body
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

            return true; } } // holidays
    
    public class itemdbc {
        public DBCHeader header;
        public itemBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM item_template", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Entry, Class, SubClass, SoundOverrideSubClass, Material, DisplayId, InventoryType, Sheath FROM item_template ORDER BY Entry ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new itemMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Entry");
                    body.records[i].record.Class = reader.GetInt32("Class");
                    body.records[i].record.SubClass = reader.GetInt32("SubClass");
                    body.records[i].record.SoundOverride = reader.GetInt32("SoundOverrideSubClass");
                    body.records[i].record.Material = reader.GetInt32("Material");
                    body.records[i].record.DisplayInfo = reader.GetInt32("DisplayId");
                    body.records[i].record.InventorySlot = reader.GetInt32("InventoryType");
                    body.records[i].record.Sheath = reader.GetInt32("Sheath");
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
                    for (UInt32 j = 0; j < 17; ++j) ;

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
                    count = Marshal.SizeOf(typeof(itemRecord)); // Write main body
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

            return true; } } // item

    public class itembagfamilydbc {
        public DBCHeader header;
        public itembagfamilyBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itembagfamilydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2 FROM itembagfamilydbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new itembagfamilyMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itembagfamilyRecord));

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
                    count = Marshal.SizeOf(typeof(itembagfamilyRecord)); // Write main body
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

            return true; } } // itembagfamily

    public class itemextendedcostdbc {
        public DBCHeader header;
        public itemextendedcostBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemextendedcostdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, ReqHonorPoints, ReqArenaPoints, ReqArenaSlot, ReqItem1, ReqItem2, ReqItem3, ReqItem4, ReqItem5, ReqItemCount1, ReqItemCount2, ReqItemCount3, ReqItemCount4, ReqItemCount5, ReqPersonalArenaRating, PurchaseGroup FROM itemextendedcostdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new itemextendedcostMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 16;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemextendedcostRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.ReqHonorPoints = reader.GetInt32("ReqHonorPoints");
                    body.records[i].record.ReqArenaPoints = reader.GetInt32("ReqArenaPoints");
                    body.records[i].record.ReqArenaSlot = reader.GetInt32("ReqArenaSlot");
                    body.records[i].record.ReqItem1 = reader.GetInt32("ReqItem1");
                    body.records[i].record.ReqItem2 = reader.GetInt32("ReqItem2");
                    body.records[i].record.ReqItem3 = reader.GetInt32("ReqItem3");
                    body.records[i].record.ReqItem4 = reader.GetInt32("ReqItem4");
                    body.records[i].record.ReqItem5 = reader.GetInt32("ReqItem5");
                    body.records[i].record.ReqItemCount1 = reader.GetInt32("ReqItemCount1");
                    body.records[i].record.ReqItemCount2 = reader.GetInt32("ReqItemCount2");
                    body.records[i].record.ReqItemCount3 = reader.GetInt32("ReqItemCount3");
                    body.records[i].record.ReqItemCount4 = reader.GetInt32("ReqItemCount4");
                    body.records[i].record.ReqItemCount5 = reader.GetInt32("ReqItemCount5");
                    body.records[i].record.ReqPersonalArenaRating = reader.GetInt32("ReqPersonalArenaRating");
                    body.records[i].record.PurchaseGroup = reader.GetInt32("PurchaseGroup");
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
                    count = Marshal.SizeOf(typeof(itemextendedcostRecord)); // Write main body
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

            return true; } } // itemextendedcost

    public class itemlimitcategorydbc {
        public DBCHeader header;
        public itemlimitcategoryBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemlimitcategorydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, MaxCount, Mode FROM itemlimitcategorydbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new itemlimitcategoryMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 20;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemlimitcategoryRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MaxCount = reader.GetInt32("MaxCount");
                    body.records[i].record.Mode = reader.GetInt32("Mode");

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
                    count = Marshal.SizeOf(typeof(itemlimitcategoryRecord)); // Write main body
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

            return true; } } // itemlimitcategory

    public class itemrandompropertiesdbc {
        public DBCHeader header;
        public itemrandompropertiesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemrandompropertiesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, InternalName, EnchantId1, EnchantId2, EnchantId3, EnchantId4, EnchantId5, Name, Name_loc2 FROM itemrandompropertiesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new itemrandompropertiesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 24;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemrandompropertiesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].InternalName = reader.GetString("InternalName");
                    body.records[i].record.EnchantId1 = reader.GetInt32("EnchantId1");
                    body.records[i].record.EnchantId2 = reader.GetInt32("EnchantId2");
                    body.records[i].record.EnchantId3 = reader.GetInt32("EnchantId3");
                    body.records[i].record.EnchantId4 = reader.GetInt32("EnchantId4");
                    body.records[i].record.EnchantId5 = reader.GetInt32("EnchantId5");

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
                    for (UInt32 j = 0; j < 17; ++j) { // Name
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
                    count = Marshal.SizeOf(typeof(itemrandompropertiesRecord)); // Write main body
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

            return true; } } // itemrandomproperties

    public class itemrandomsuffixdbc {
        public DBCHeader header;
        public itemrandomsuffixBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemrandomsuffixdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, InternalName, EnchantId1, EnchantId2, EnchantId3, EnchantId4, EnchantId5, Prefix1, Prefix2, Prefix3, Prefix4, Prefix5 FROM itemrandomsuffixdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new itemrandomsuffixMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 29;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemrandomsuffixRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].InternalName = reader.GetString("InternalName");
                    body.records[i].record.EnchantId1 = reader.GetInt32("EnchantId1");
                    body.records[i].record.EnchantId2 = reader.GetInt32("EnchantId2");
                    body.records[i].record.EnchantId3 = reader.GetInt32("EnchantId3");
                    body.records[i].record.EnchantId4 = reader.GetInt32("EnchantId4");
                    body.records[i].record.EnchantId5 = reader.GetInt32("EnchantId5");
                    body.records[i].record.Prefix1 = reader.GetInt32("Prefix1");
                    body.records[i].record.Prefix2 = reader.GetInt32("Prefix2");
                    body.records[i].record.Prefix3 = reader.GetInt32("Prefix3");
                    body.records[i].record.Prefix4 = reader.GetInt32("Prefix4");
                    body.records[i].record.Prefix5 = reader.GetInt32("Prefix5");

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
                    count = Marshal.SizeOf(typeof(itemrandomsuffixRecord)); // Write main body
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

            return true; } } // itemrandomsuffix

    public class itemsetdbc {
        public DBCHeader header;
        public itemsetBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM itemsetdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, ItemId1, ItemId2, ItemId3, ItemId4, ItemId5, ItemId6, ItemId7, ItemId8, ItemId9, ItemId10, Unknown1, Unknown2, Unknown3, Unknown4, Unknown5, Unknown6, Unknown7, Spells1, Spells2, Spells3, Spells4, Spells5, Spells6, Spells7, Spells8, ItemsToTriggerSpell1, ItemsToTriggerSpell2, ItemsToTriggerSpell3, ItemsToTriggerSpell4, ItemsToTriggerSpell5, ItemsToTriggerSpell6, ItemsToTriggerSpell7, ItemsToTriggerSpell8, RequiredSkillId, RequiredSkillValue FROM itemsetdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new itemsetMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 53;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(itemsetRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.ItemId1 = reader.GetInt32("ItemId1");
                    body.records[i].record.ItemId2 = reader.GetInt32("ItemId2");
                    body.records[i].record.ItemId3 = reader.GetInt32("ItemId3");
                    body.records[i].record.ItemId4 = reader.GetInt32("ItemId4");
                    body.records[i].record.ItemId5 = reader.GetInt32("ItemId5");
                    body.records[i].record.ItemId6 = reader.GetInt32("ItemId6");
                    body.records[i].record.ItemId7 = reader.GetInt32("ItemId7");
                    body.records[i].record.ItemId8 = reader.GetInt32("ItemId8");
                    body.records[i].record.ItemId9 = reader.GetInt32("ItemId9");
                    body.records[i].record.ItemId10 = reader.GetInt32("ItemId10");
                    body.records[i].record.Unknown1 = reader.GetInt32("Unknown1");
                    body.records[i].record.Unknown2 = reader.GetInt32("Unknown2");
                    body.records[i].record.Unknown3 = reader.GetInt32("Unknown3");
                    body.records[i].record.Unknown4 = reader.GetInt32("Unknown4");
                    body.records[i].record.Unknown5 = reader.GetInt32("Unknown5");
                    body.records[i].record.Unknown6 = reader.GetInt32("Unknown6");
                    body.records[i].record.Unknown7 = reader.GetInt32("Unknown7");
                    body.records[i].record.Spells1 = reader.GetInt32("Spells1");
                    body.records[i].record.Spells2 = reader.GetInt32("Spells2");
                    body.records[i].record.Spells3 = reader.GetInt32("Spells3");
                    body.records[i].record.Spells4 = reader.GetInt32("Spells4");
                    body.records[i].record.Spells5 = reader.GetInt32("Spells5");
                    body.records[i].record.Spells6 = reader.GetInt32("Spells6");
                    body.records[i].record.Spells7 = reader.GetInt32("Spells7");
                    body.records[i].record.Spells8 = reader.GetInt32("Spells8");
                    body.records[i].record.ItemsToTriggerSpell1 = reader.GetInt32("ItemsToTriggerSpell1");
                    body.records[i].record.ItemsToTriggerSpell2 = reader.GetInt32("ItemsToTriggerSpell2");
                    body.records[i].record.ItemsToTriggerSpell3 = reader.GetInt32("ItemsToTriggerSpell3");
                    body.records[i].record.ItemsToTriggerSpell4 = reader.GetInt32("ItemsToTriggerSpell4");
                    body.records[i].record.ItemsToTriggerSpell5 = reader.GetInt32("ItemsToTriggerSpell5");
                    body.records[i].record.ItemsToTriggerSpell6 = reader.GetInt32("ItemsToTriggerSpell6");
                    body.records[i].record.ItemsToTriggerSpell7 = reader.GetInt32("ItemsToTriggerSpell7");
                    body.records[i].record.ItemsToTriggerSpell8 = reader.GetInt32("ItemsToTriggerSpell8");
                    body.records[i].record.RequiredSkillId = reader.GetInt32("RequiredSkillId");
                    body.records[i].record.RequiredSkillValue = reader.GetInt32("RequiredSkillValue");

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
                    count = Marshal.SizeOf(typeof(itemsetRecord)); // Write main body
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

            return true; } } // itemset

    public class lfgdungeonsdbc {
        public DBCHeader header;
        public lfgdungeonsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM lfgdungeonsdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, MinLevel, MaxLevel, RecLevel, RecMinLevel, RecMaxLevel, MapId, Difficulty, Flags, Type, Unk, IconName, Expansion, Unk2, GroupType, Description, Description_loc2 FROM lfgdungeonsdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new lfgdungeonsMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 49;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(lfgdungeonsRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MinLevel = reader.GetInt32("MinLevel");
                    body.records[i].record.MaxLevel = reader.GetInt32("MaxLevel");
                    body.records[i].record.RecLevel = reader.GetInt32("RecLevel");
                    body.records[i].record.RecMinLevel = reader.GetInt32("RecMinLevel");
                    body.records[i].record.RecMaxLevel = reader.GetInt32("RecMaxLevel");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.Difficulty = reader.GetInt32("Difficulty");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.Type = reader.GetInt32("Type");
                    body.records[i].record.Unk = reader.GetInt32("Unk");
                    body.records[i].IconName = reader.GetString("IconName");
                    body.records[i].record.Expansion = reader.GetInt32("Expansion");
                    body.records[i].record.Unk2 = reader.GetInt32("Unk2");
                    body.records[i].record.GroupType = reader.GetInt32("GroupType");

                    body.records[i].Name = new string[17];
                    body.records[i].Description = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.Description = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name[loc] = "";
                        body.records[i].Description[loc] = ""; }
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");
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
                // IconName
                    if (body.records[i].IconName.Length == 0)
                        body.records[i].record.IconName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].IconName)) body.records[i].record.IconName = offsetStorage[body.records[i].IconName];
                        else {
                            body.records[i].record.IconName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].IconName) + 1;
                            offsetStorage.Add(body.records[i].IconName, body.records[i].record.IconName);
                            reverseStorage.Add(body.records[i].record.IconName, body.records[i].IconName); } } }

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
                    count = Marshal.SizeOf(typeof(lfgdungeonsRecord)); // Write main body
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

            return true; } } // lfgdungeons

    public class lightdbc {
        public DBCHeader header;
        public lightBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM lightdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, MapId, X, Y, Z, FalloffStart, FalloffEnd, SkyAndFog, WaterSettings, SunsetParams, OtherParams, DeathParams, Unk1, Unk2, Unk3 FROM lightdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new lightMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 15;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(lightRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.X = reader.GetFloat("X");
                    body.records[i].record.Y = reader.GetFloat("Y");
                    body.records[i].record.Z = reader.GetFloat("Z");
                    body.records[i].record.FalloffStart = reader.GetFloat("FalloffStart");
                    body.records[i].record.FalloffEnd = reader.GetFloat("FalloffEnd");
                    body.records[i].record.SkyAndFog = reader.GetInt32("SkyAndFog");
                    body.records[i].record.WaterSettings = reader.GetInt32("WaterSettings");
                    body.records[i].record.SunsetParams = reader.GetInt32("SunsetParams");
                    body.records[i].record.OtherParams = reader.GetInt32("OtherParams");
                    body.records[i].record.DeathParams = reader.GetInt32("DeathParams");
                    body.records[i].record.Unk1 = reader.GetInt32("Unk1");
                    body.records[i].record.Unk2 = reader.GetInt32("Unk2");
                    body.records[i].record.Unk3 = reader.GetInt32("Unk3");
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
                    count = Marshal.SizeOf(typeof(lightRecord)); // Write main body
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

            return true; } } // light

    public class liquidtypedbc {
        public DBCHeader header;
        public liquidtypeBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM liquidtypedbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Flags, Type, SoundId, SpellId, MaxDarkenDepth, FogDarkenIntensity, AmbDarkenIntensity, DirDarkenIntensity, LightID, ParticleScale, ParticleMovement, ParticleTexSlots, LiquidMaterialID, Texture1, Texture2, Texture3, Texture4, Texture5, Texture6, Color1, Color2, UnkA1, UnkA2, UnkA3, UnkA4, UnkA5, UnkA6, UnkA7, UnkA8, UnkA9, UnkA10, UnkA11, UnkA12, UnkA13, UnkA14, UnkA15, UnkA16, UnkA17, UnkA18, UnkB1, UnkB2, UnkB3, UnkB4 FROM liquidtypedbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new liquidtypeMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 45;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(liquidtypeRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.Type = reader.GetInt32("Type");
                    body.records[i].record.SoundId = reader.GetInt32("SoundId");
                    body.records[i].record.SpellId = reader.GetInt32("SpellId");
                    body.records[i].record.MaxDarkenDepth = reader.GetFloat("MaxDarkenDepth");
                    body.records[i].record.FogDarkenIntensity = reader.GetFloat("FogDarkenIntensity");
                    body.records[i].record.AmbDarkenIntensity = reader.GetFloat("AmbDarkenIntensity");
                    body.records[i].record.DirDarkenIntensity = reader.GetFloat("DirDarkenIntensity");
                    body.records[i].record.LightID = reader.GetInt32("LightID");
                    body.records[i].record.ParticleScale = reader.GetFloat("ParticleScale");
                    body.records[i].record.ParticleMovement = reader.GetInt32("ParticleMovement");
                    body.records[i].record.ParticleTexSlots = reader.GetInt32("ParticleTexSlots");
                    body.records[i].record.LiquidMaterialID = reader.GetInt32("LiquidMaterialID");
                    body.records[i].Texture1 = reader.GetString("Texture1");
                    body.records[i].Texture2 = reader.GetString("Texture2");
                    body.records[i].Texture3 = reader.GetString("Texture3");
                    body.records[i].Texture4 = reader.GetString("Texture4");
                    body.records[i].Texture5 = reader.GetString("Texture5");
                    body.records[i].Texture6 = reader.GetString("Texture6");
                    body.records[i].record.Color1 = reader.GetInt32("Color1");
                    body.records[i].record.Color2 = reader.GetInt32("Color2");
                    body.records[i].record.UnkA1 = reader.GetFloat("UnkA1");
                    body.records[i].record.UnkA2 = reader.GetFloat("UnkA2");
                    body.records[i].record.UnkA3 = reader.GetFloat("UnkA3");
                    body.records[i].record.UnkA4 = reader.GetFloat("UnkA4");
                    body.records[i].record.UnkA5 = reader.GetFloat("UnkA5");
                    body.records[i].record.UnkA6 = reader.GetFloat("UnkA6");
                    body.records[i].record.UnkA7 = reader.GetFloat("UnkA7");
                    body.records[i].record.UnkA8 = reader.GetFloat("UnkA8");
                    body.records[i].record.UnkA9 = reader.GetFloat("UnkA9");
                    body.records[i].record.UnkA10 = reader.GetFloat("UnkA10");
                    body.records[i].record.UnkA11 = reader.GetFloat("UnkA11");
                    body.records[i].record.UnkA12 = reader.GetFloat("UnkA12");
                    body.records[i].record.UnkA13 = reader.GetFloat("UnkA13");
                    body.records[i].record.UnkA14 = reader.GetFloat("UnkA14");
                    body.records[i].record.UnkA15 = reader.GetFloat("UnkA15");
                    body.records[i].record.UnkA16 = reader.GetFloat("UnkA16");
                    body.records[i].record.UnkA17 = reader.GetFloat("UnkA17");
                    body.records[i].record.UnkA18 = reader.GetFloat("UnkA18");
                    body.records[i].record.UnkB1 = reader.GetInt32("UnkB1");
                    body.records[i].record.UnkB2 = reader.GetInt32("UnkB2");
                    body.records[i].record.UnkB3 = reader.GetInt32("UnkB3");
                    body.records[i].record.UnkB4 = reader.GetInt32("UnkB4");
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
                    // Texture4
                    if (body.records[i].Texture4.Length == 0)
                        body.records[i].record.Texture4 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Texture4)) body.records[i].record.Texture4 = offsetStorage[body.records[i].Texture4];
                        else {
                            body.records[i].record.Texture4 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Texture4) + 1;
                            offsetStorage.Add(body.records[i].Texture4, body.records[i].record.Texture4);
                            reverseStorage.Add(body.records[i].record.Texture4, body.records[i].Texture4); } }
                    // Texture5
                    if (body.records[i].Texture5.Length == 0)
                        body.records[i].record.Texture5 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Texture5)) body.records[i].record.Texture5 = offsetStorage[body.records[i].Texture5];
                        else {
                            body.records[i].record.Texture5 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Texture5) + 1;
                            offsetStorage.Add(body.records[i].Texture5, body.records[i].record.Texture5);
                            reverseStorage.Add(body.records[i].record.Texture5, body.records[i].Texture5); } }
                    // Texture6
                    if (body.records[i].Texture6.Length == 0)
                        body.records[i].record.Texture6 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Texture6)) body.records[i].record.Texture6 = offsetStorage[body.records[i].Texture6];
                        else {
                            body.records[i].record.Texture6 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Texture6) + 1;
                            offsetStorage.Add(body.records[i].Texture6, body.records[i].record.Texture6);
                            reverseStorage.Add(body.records[i].record.Texture6, body.records[i].Texture6); } } }

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
                    count = Marshal.SizeOf(typeof(liquidtypeRecord)); // Write main body
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

            return true; } } // liquidtype

    public class lockdbc {
        public DBCHeader header;
        public lockBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM lockdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Type1, Type2, Type3, Type4, Type5, Type6, Type7, Type8, Index1, Index2, Index3, Index4, Index5, Index6, Index7, Index8, Skill1, Skill2, Skill3, Skill4, Skill5, Skill6, Skill7, Skill8, Action1, Action2, Action3, Action4, Action5, Action6, Action7, Action8 FROM lockdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new lockMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 33;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(lockRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Type1 = reader.GetInt32("Type1");
                    body.records[i].record.Type2 = reader.GetInt32("Type2");
                    body.records[i].record.Type3 = reader.GetInt32("Type3");
                    body.records[i].record.Type4 = reader.GetInt32("Type4");
                    body.records[i].record.Type5 = reader.GetInt32("Type5");
                    body.records[i].record.Type6 = reader.GetInt32("Type6");
                    body.records[i].record.Type7 = reader.GetInt32("Type7");
                    body.records[i].record.Type8 = reader.GetInt32("Type8");
                    body.records[i].record.Index1 = reader.GetInt32("Index1");
                    body.records[i].record.Index2 = reader.GetInt32("Index2");
                    body.records[i].record.Index3 = reader.GetInt32("Index3");
                    body.records[i].record.Index4 = reader.GetInt32("Index4");
                    body.records[i].record.Index5 = reader.GetInt32("Index5");
                    body.records[i].record.Index6 = reader.GetInt32("Index6");
                    body.records[i].record.Index7 = reader.GetInt32("Index7");
                    body.records[i].record.Index8 = reader.GetInt32("Index8");
                    body.records[i].record.Skill1 = reader.GetInt32("Skill1");
                    body.records[i].record.Skill2 = reader.GetInt32("Skill2");
                    body.records[i].record.Skill3 = reader.GetInt32("Skill3");
                    body.records[i].record.Skill4 = reader.GetInt32("Skill4");
                    body.records[i].record.Skill5 = reader.GetInt32("Skill5");
                    body.records[i].record.Skill6 = reader.GetInt32("Skill6");
                    body.records[i].record.Skill7 = reader.GetInt32("Skill7");
                    body.records[i].record.Skill8 = reader.GetInt32("Skill8");
                    body.records[i].record.Action1 = reader.GetInt32("Action1");
                    body.records[i].record.Action2 = reader.GetInt32("Action2");
                    body.records[i].record.Action3 = reader.GetInt32("Action3");
                    body.records[i].record.Action4 = reader.GetInt32("Action4");
                    body.records[i].record.Action5 = reader.GetInt32("Action5");
                    body.records[i].record.Action6 = reader.GetInt32("Action6");
                    body.records[i].record.Action7 = reader.GetInt32("Action7");
                    body.records[i].record.Action8 = reader.GetInt32("Action8");
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
                    count = Marshal.SizeOf(typeof(lockRecord)); // Write main body
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

            return true; } } // lock

    public class mailtemplatedbc {
        public DBCHeader header;
        public mailtemplateBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM mailtemplatedbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Subject, Subject_loc2, Content, Content_loc2 FROM mailtemplatedbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new mailtemplateMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 35;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(mailtemplateRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");

                    body.records[i].Subject = new string[17];
                    body.records[i].Content = new string[17];
                    body.records[i].record.Subject = new UInt32[17];
                    body.records[i].record.Content = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Subject[loc] = "";
                        body.records[i].Content[loc] = ""; }
                    body.records[i].Subject[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Subject_loc2" : "Subject");
                    body.records[i].Content[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Content_loc2" : "Content");

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
                        // Subject
                        if (body.records[i].Subject[j].Length == 0)
                            body.records[i].record.Subject[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Subject[j])) body.records[i].record.Subject[j] = offsetStorage[body.records[i].Subject[j]];
                            else {
                                body.records[i].record.Subject[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Subject[j]) + 1;
                                offsetStorage.Add(body.records[i].Subject[j], body.records[i].record.Subject[j]);
                                reverseStorage.Add(body.records[i].record.Subject[j], body.records[i].Subject[j]); } }
                        // Content
                        if (body.records[i].Content[j].Length == 0)
                            body.records[i].record.Content[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Content[j])) body.records[i].record.Content[j] = offsetStorage[body.records[i].Content[j]];
                            else {
                                body.records[i].record.Content[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Content[j]) + 1;
                                offsetStorage.Add(body.records[i].Content[j], body.records[i].record.Content[j]);
                                reverseStorage.Add(body.records[i].record.Content[j], body.records[i].Content[j]); } } }

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
                    count = Marshal.SizeOf(typeof(mailtemplateRecord)); // Write main body
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

            return true; } } // mailtemplate

    public class mapdbc {
        public DBCHeader header;
        public mapBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM mapdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, InternalName, MapType, Flags, IsBattleground, Name, Name_loc2, LinkedZone, HordeIntro, HordeIntro_loc2, AllianceIntro, AllianceIntro_loc2, MultiMapId, BattlefieldMapIconScale, EntranceMap, EntranceX, EntranceY, TimeOfDayOverride, Addon, UnkTime, MaxPlayers FROM mapdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new mapMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 66;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(mapRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].InternalName = reader.GetString("InternalName");
                    body.records[i].record.MapType = reader.GetInt32("MapType");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.IsBattleground = reader.GetInt32("IsBattleground");
                    body.records[i].record.LinkedZone = reader.GetInt32("LinkedZone");
                    body.records[i].record.MultiMapId = reader.GetInt32("MultiMapId");
                    body.records[i].record.BattlefieldMapIconScale = reader.GetFloat("BattlefieldMapIconScale");
                    body.records[i].record.EntranceMap = reader.GetInt32("EntranceMap");
                    body.records[i].record.EntranceX = reader.GetFloat("EntranceX");
                    body.records[i].record.EntranceY = reader.GetFloat("EntranceY");
                    body.records[i].record.TimeOfDayOverride = reader.GetInt32("TimeOfDayOverride");
                    body.records[i].record.Addon = reader.GetInt32("Addon");
                    body.records[i].record.UnkTime = reader.GetInt32("UnkTime");
                    body.records[i].record.MaxPlayers = reader.GetInt32("MaxPlayers");

                    body.records[i].Name = new string[17];
                    body.records[i].HordeIntro = new string[17];
                    body.records[i].AllianceIntro = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.HordeIntro = new UInt32[17];
                    body.records[i].record.AllianceIntro = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name[loc] = "";
                        body.records[i].HordeIntro[loc] = "";
                        body.records[i].AllianceIntro[loc] = ""; }
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");
                    body.records[i].HordeIntro[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "HordeIntro_loc2" : "HordeIntro");
                    body.records[i].AllianceIntro[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "AllianceIntro_loc2" : "AllianceIntro");

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
                        // HordeIntro
                        if (body.records[i].HordeIntro[j].Length == 0)
                            body.records[i].record.HordeIntro[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].HordeIntro[j])) body.records[i].record.HordeIntro[j] = offsetStorage[body.records[i].HordeIntro[j]];
                            else {
                                body.records[i].record.HordeIntro[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].HordeIntro[j]) + 1;
                                offsetStorage.Add(body.records[i].HordeIntro[j], body.records[i].record.HordeIntro[j]);
                                reverseStorage.Add(body.records[i].record.HordeIntro[j], body.records[i].HordeIntro[j]); } }
                        // AllianceIntro
                        if (body.records[i].AllianceIntro[j].Length == 0)
                            body.records[i].record.AllianceIntro[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].AllianceIntro[j])) body.records[i].record.AllianceIntro[j] = offsetStorage[body.records[i].AllianceIntro[j]];
                            else {
                                body.records[i].record.AllianceIntro[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].AllianceIntro[j]) + 1;
                                offsetStorage.Add(body.records[i].AllianceIntro[j], body.records[i].record.AllianceIntro[j]);
                                reverseStorage.Add(body.records[i].record.AllianceIntro[j], body.records[i].AllianceIntro[j]); } } }
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
                    count = Marshal.SizeOf(typeof(mapRecord)); // Write main body
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

            return true; } } // map

    public class mapdifficultydbc {
        public DBCHeader header;
        public mapdifficultyBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM mapdifficultydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, MapId, Difficulty, AreaTriggerText, AreaTriggerText_loc2, ResetTime, MaxPlayers, DifficultyString FROM mapdifficultydbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new mapdifficultyMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 23;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(mapdifficultyRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.Difficulty = reader.GetInt32("Difficulty");
                    body.records[i].record.ResetTime = reader.GetInt32("ResetTime");
                    body.records[i].record.MaxPlayers = reader.GetInt32("MaxPlayers");
                    body.records[i].DifficultyString = reader.GetString("DifficultyString");

                    body.records[i].AreaTriggerText = new string[17];
                    body.records[i].record.AreaTriggerText = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) 
                        body.records[i].AreaTriggerText[loc] = ""; 
                    body.records[i].AreaTriggerText[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "AreaTriggerText_loc2" : "AreaTriggerText");

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
                        // AreaTriggerText
                        if (body.records[i].AreaTriggerText[j].Length == 0)
                            body.records[i].record.AreaTriggerText[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].AreaTriggerText[j])) body.records[i].record.AreaTriggerText[j] = offsetStorage[body.records[i].AreaTriggerText[j]];
                            else {
                                body.records[i].record.AreaTriggerText[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].AreaTriggerText[j]) + 1;
                                offsetStorage.Add(body.records[i].AreaTriggerText[j], body.records[i].record.AreaTriggerText[j]);
                                reverseStorage.Add(body.records[i].record.AreaTriggerText[j], body.records[i].AreaTriggerText[j]); } } }
                    // DifficultyString
                    if (body.records[i].DifficultyString.Length == 0)
                        body.records[i].record.DifficultyString = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].DifficultyString)) body.records[i].record.DifficultyString = offsetStorage[body.records[i].DifficultyString];
                        else {
                            body.records[i].record.DifficultyString = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].DifficultyString) + 1;
                            offsetStorage.Add(body.records[i].DifficultyString, body.records[i].record.DifficultyString);
                            reverseStorage.Add(body.records[i].record.DifficultyString, body.records[i].DifficultyString); } } }

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
                    count = Marshal.SizeOf(typeof(mapdifficultyRecord)); // Write main body
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

            return true; } } // mapdifficulty

    public class moviedbc {
        public DBCHeader header;
        public movieBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM moviedbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Filename, Unk FROM moviedbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new movieMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 3;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(movieRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].Filename = reader.GetString("Filename");
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
                    // Filename
                    if (body.records[i].Filename.Length == 0)
                        body.records[i].record.Filename = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Filename)) body.records[i].record.Filename = offsetStorage[body.records[i].Filename];
                        else {
                            body.records[i].record.Filename = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Filename) + 1;
                            offsetStorage.Add(body.records[i].Filename, body.records[i].record.Filename);
                            reverseStorage.Add(body.records[i].record.Filename, body.records[i].Filename); } } }

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
                    count = Marshal.SizeOf(typeof(movieRecord)); // Write main body
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

            return true; } } // movie

    public class overridespelldatadbc {
        public DBCHeader header;
        public overridespelldataBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM overridespelldatadbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SpellId1, SpellId2, SpellId3, SpellId4, SpellId5, SpellId6, SpellId7, SpellId8, SpellId9, SpellId10, Unk FROM overridespelldatadbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new overridespelldataMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 12;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(overridespelldataRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SpellId1 = reader.GetInt32("SpellId1");
                    body.records[i].record.SpellId2 = reader.GetInt32("SpellId2");
                    body.records[i].record.SpellId3 = reader.GetInt32("SpellId3");
                    body.records[i].record.SpellId4 = reader.GetInt32("SpellId4");
                    body.records[i].record.SpellId5 = reader.GetInt32("SpellId5");
                    body.records[i].record.SpellId6 = reader.GetInt32("SpellId6");
                    body.records[i].record.SpellId7 = reader.GetInt32("SpellId7");
                    body.records[i].record.SpellId8 = reader.GetInt32("SpellId8");
                    body.records[i].record.SpellId9 = reader.GetInt32("SpellId9");
                    body.records[i].record.SpellId10 = reader.GetInt32("SpellId10");
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
                    count = Marshal.SizeOf(typeof(overridespelldataRecord)); // Write main body
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

            return true; } } // overridespelldata

    public class powerdisplaydbc {
        public DBCHeader header;
        public powerdisplayBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM powerdisplaydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, PowerType, Name, R, G, B FROM powerdisplaydbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new powerdisplayMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 6;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(powerdisplayRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.PowerType = reader.GetInt32("PowerType");
                    body.records[i].Name = reader.GetString("Name");
                    body.records[i].record.R = reader.GetByte("R");
                    body.records[i].record.G = reader.GetByte("G");
                    body.records[i].record.B = reader.GetByte("B");
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
                    count = Marshal.SizeOf(typeof(powerdisplayRecord)); // Write main body
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
                Console.ReadLine();
                return false; }

            return true; } } // powerdisplay

    public class pvpdifficultydbc {
        public DBCHeader header;
        public pvpdifficultyBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM pvpdifficultydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, MapId, BracketId, MinLevel, MaxLevel, Difficulty FROM pvpdifficultydbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new pvpdifficultyMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 6;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(pvpdifficultyRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.BracketId = reader.GetInt32("BracketId");
                    body.records[i].record.MinLevel = reader.GetInt32("MinLevel");
                    body.records[i].record.MaxLevel = reader.GetInt32("MaxLevel");
                    body.records[i].record.Difficulty = reader.GetInt32("Difficulty");
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
                    count = Marshal.SizeOf(typeof(pvpdifficultyRecord)); // Write main body
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

            return true; } } // pvpdifficulty

    public class questfactionrewarddbc {
        public DBCHeader header;
        public questfactionrewardBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM questfactionrewarddbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, QuestRewFactionValue1, QuestRewFactionValue2, QuestRewFactionValue3, QuestRewFactionValue4, QuestRewFactionValue5, QuestRewFactionValue6, QuestRewFactionValue7, QuestRewFactionValue8, QuestRewFactionValue9, QuestRewFactionValue10 FROM questfactionrewarddbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new questfactionrewardMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 11;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(questfactionrewardRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.QuestRewFactionValue1 = reader.GetInt32("QuestRewFactionValue1");
                    body.records[i].record.QuestRewFactionValue2 = reader.GetInt32("QuestRewFactionValue2");
                    body.records[i].record.QuestRewFactionValue3 = reader.GetInt32("QuestRewFactionValue3");
                    body.records[i].record.QuestRewFactionValue4 = reader.GetInt32("QuestRewFactionValue4");
                    body.records[i].record.QuestRewFactionValue5 = reader.GetInt32("QuestRewFactionValue5");
                    body.records[i].record.QuestRewFactionValue6 = reader.GetInt32("QuestRewFactionValue6");
                    body.records[i].record.QuestRewFactionValue7 = reader.GetInt32("QuestRewFactionValue7");
                    body.records[i].record.QuestRewFactionValue8 = reader.GetInt32("QuestRewFactionValue8");
                    body.records[i].record.QuestRewFactionValue9 = reader.GetInt32("QuestRewFactionValue9");
                    body.records[i].record.QuestRewFactionValue10 = reader.GetInt32("QuestRewFactionValue10");
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
                    count = Marshal.SizeOf(typeof(questfactionrewardRecord)); // Write main body
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

            return true; } } // questfactionrew

    public class questsortdbc {
        public DBCHeader header;
        public questsortBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM questsortdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2 FROM questsortdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new questsortMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(questsortRecord));

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
                    count = Marshal.SizeOf(typeof(questsortRecord)); // Write main body
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

            return true; } } // questsort

    public class questxpdbc {
        public DBCHeader header;
        public questxpBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM questxpdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Exp1, Exp2, Exp3, Exp4, Exp5, Exp6, Exp7, Exp8, Exp9, Exp10 FROM questxpdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new questxpMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 11;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(questxpRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Exp1 = reader.GetInt32("Exp1");
                    body.records[i].record.Exp2 = reader.GetInt32("Exp2");
                    body.records[i].record.Exp3 = reader.GetInt32("Exp3");
                    body.records[i].record.Exp4 = reader.GetInt32("Exp4");
                    body.records[i].record.Exp5 = reader.GetInt32("Exp5");
                    body.records[i].record.Exp6 = reader.GetInt32("Exp6");
                    body.records[i].record.Exp7 = reader.GetInt32("Exp7");
                    body.records[i].record.Exp8 = reader.GetInt32("Exp8");
                    body.records[i].record.Exp9 = reader.GetInt32("Exp9");
                    body.records[i].record.Exp10 = reader.GetInt32("Exp10");
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
                    count = Marshal.SizeOf(typeof(questxpRecord)); // Write main body
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

            return true; } } // questxp

    public class randproppointsdbc {
        public DBCHeader header;
        public randproppointsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM randproppointsdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT ItemLevel, EpicPropertiesPoints1, EpicPropertiesPoints2, EpicPropertiesPoints3, EpicPropertiesPoints4, EpicPropertiesPoints5, RarePropertiesPoints1, RarePropertiesPoints2, RarePropertiesPoints3, RarePropertiesPoints4, RarePropertiesPoints5, UncommonPropertiesPoints1, UncommonPropertiesPoints2, UncommonPropertiesPoints3, UncommonPropertiesPoints4, UncommonPropertiesPoints5 FROM randproppointsdbc ORDER BY ItemLevel ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new randproppointsMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 16;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(randproppointsRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.ItemLevel = reader.GetInt32("ItemLevel");
                    body.records[i].record.EpicPropertiesPoints1 = reader.GetInt32("EpicPropertiesPoints1");
                    body.records[i].record.EpicPropertiesPoints2 = reader.GetInt32("EpicPropertiesPoints2");
                    body.records[i].record.EpicPropertiesPoints3 = reader.GetInt32("EpicPropertiesPoints3");
                    body.records[i].record.EpicPropertiesPoints4 = reader.GetInt32("EpicPropertiesPoints4");
                    body.records[i].record.EpicPropertiesPoints5 = reader.GetInt32("EpicPropertiesPoints5");
                    body.records[i].record.RarePropertiesPoints1 = reader.GetInt32("RarePropertiesPoints1");
                    body.records[i].record.RarePropertiesPoints2 = reader.GetInt32("RarePropertiesPoints2");
                    body.records[i].record.RarePropertiesPoints3 = reader.GetInt32("RarePropertiesPoints3");
                    body.records[i].record.RarePropertiesPoints4 = reader.GetInt32("RarePropertiesPoints4");
                    body.records[i].record.RarePropertiesPoints5 = reader.GetInt32("RarePropertiesPoints5");
                    body.records[i].record.UncommonPropertiesPoints1 = reader.GetInt32("UncommonPropertiesPoints1");
                    body.records[i].record.UncommonPropertiesPoints2 = reader.GetInt32("UncommonPropertiesPoints2");
                    body.records[i].record.UncommonPropertiesPoints3 = reader.GetInt32("UncommonPropertiesPoints3");
                    body.records[i].record.UncommonPropertiesPoints4 = reader.GetInt32("UncommonPropertiesPoints4");
                    body.records[i].record.UncommonPropertiesPoints5 = reader.GetInt32("UncommonPropertiesPoints5");
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
                    count = Marshal.SizeOf(typeof(randproppointsRecord)); // Write main body
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

            return true; } } // randproppoints

    public class scalingstatdistributiondbc {
        public DBCHeader header;
        public scalingstatdistributionBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM scalingstatdistributiondbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, StatMod1, StatMod2, StatMod3, StatMod4, StatMod5, StatMod6, StatMod7, StatMod8, StatMod9, StatMod10, Modifier1, Modifier2, Modifier3, Modifier4, Modifier5, Modifier6, Modifier7, Modifier8, Modifier9, Modifier10, MaxLevel FROM scalingstatdistributiondbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new scalingstatdistributionMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 22;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(scalingstatdistributionRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.StatMod1 = reader.GetInt32("StatMod1");
                    body.records[i].record.StatMod2 = reader.GetInt32("StatMod2");
                    body.records[i].record.StatMod3 = reader.GetInt32("StatMod3");
                    body.records[i].record.StatMod4 = reader.GetInt32("StatMod4");
                    body.records[i].record.StatMod5 = reader.GetInt32("StatMod5");
                    body.records[i].record.StatMod6 = reader.GetInt32("StatMod6");
                    body.records[i].record.StatMod7 = reader.GetInt32("StatMod7");
                    body.records[i].record.StatMod8 = reader.GetInt32("StatMod8");
                    body.records[i].record.StatMod9 = reader.GetInt32("StatMod9");
                    body.records[i].record.StatMod10 = reader.GetInt32("StatMod10");
                    body.records[i].record.Modifier1 = reader.GetInt32("Modifier1");
                    body.records[i].record.Modifier2 = reader.GetInt32("Modifier2");
                    body.records[i].record.Modifier3 = reader.GetInt32("Modifier3");
                    body.records[i].record.Modifier4 = reader.GetInt32("Modifier4");
                    body.records[i].record.Modifier5 = reader.GetInt32("Modifier5");
                    body.records[i].record.Modifier6 = reader.GetInt32("Modifier6");
                    body.records[i].record.Modifier7 = reader.GetInt32("Modifier7");
                    body.records[i].record.Modifier8 = reader.GetInt32("Modifier8");
                    body.records[i].record.Modifier9 = reader.GetInt32("Modifier9");
                    body.records[i].record.Modifier10 = reader.GetInt32("Modifier10");
                    body.records[i].record.MaxLevel = reader.GetInt32("MaxLevel");
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
                    count = Marshal.SizeOf(typeof(scalingstatdistributionRecord)); // Write main body
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

            return true; } } // scalingstatdistribution

    public class scalingstatvaluesdbc {
        public DBCHeader header;
        public scalingstatvaluesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM scalingstatvaluesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Level, SsdMultiplierA1, SsdMultiplierA2, SsdMultiplierA3, SsdMultiplierA4, ArmorModA1, ArmorModA2, ArmorModA3, ArmorModA4, DpsMod1, DpsMod2, DpsMod3, DpsMod4, DpsMod5, DpsMod6, SpellPower, SsdMultiplierB, SsdMultiplierC, ArmorModB1, ArmorModB2, ArmorModB3, ArmorModB4, ArmorModB5 FROM scalingstatvaluesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new scalingstatvaluesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 24;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(scalingstatvaluesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Level = reader.GetInt32("Level");
                    body.records[i].record.SsdMultiplierA1 = reader.GetInt32("SsdMultiplierA1");
                    body.records[i].record.SsdMultiplierA2 = reader.GetInt32("SsdMultiplierA2");
                    body.records[i].record.SsdMultiplierA3 = reader.GetInt32("SsdMultiplierA3");
                    body.records[i].record.SsdMultiplierA4 = reader.GetInt32("SsdMultiplierA4");
                    body.records[i].record.ArmorModA1 = reader.GetInt32("ArmorModA1");
                    body.records[i].record.ArmorModA2 = reader.GetInt32("ArmorModA2");
                    body.records[i].record.ArmorModA3 = reader.GetInt32("ArmorModA3");
                    body.records[i].record.ArmorModA4 = reader.GetInt32("ArmorModA4");
                    body.records[i].record.DpsMod1 = reader.GetInt32("DpsMod1");
                    body.records[i].record.DpsMod2 = reader.GetInt32("DpsMod2");
                    body.records[i].record.DpsMod3 = reader.GetInt32("DpsMod3");
                    body.records[i].record.DpsMod4 = reader.GetInt32("DpsMod4");
                    body.records[i].record.DpsMod5 = reader.GetInt32("DpsMod5");
                    body.records[i].record.DpsMod6 = reader.GetInt32("DpsMod6");
                    body.records[i].record.SpellPower = reader.GetInt32("SpellPower");
                    body.records[i].record.SsdMultiplierB = reader.GetInt32("SsdMultiplierB");
                    body.records[i].record.SsdMultiplierC = reader.GetInt32("SsdMultiplierC");
                    body.records[i].record.ArmorModB1 = reader.GetInt32("ArmorModB1");
                    body.records[i].record.ArmorModB2 = reader.GetInt32("ArmorModB2");
                    body.records[i].record.ArmorModB3 = reader.GetInt32("ArmorModB3");
                    body.records[i].record.ArmorModB4 = reader.GetInt32("ArmorModB4");
                    body.records[i].record.ArmorModB5 = reader.GetInt32("ArmorModB5");
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
                    count = Marshal.SizeOf(typeof(scalingstatvaluesRecord)); // Write main body
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

            return true; } } // scalingstatvalues

    public class skilllineabilitydbc {
        public DBCHeader header;
        public skilllineabilityBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM skilllineabilitydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SkillId, SpellId, Racemask, Classemask, RacemaskNot, ClassemaskNot, ReqSkillValue, ForwardSpellId, LearnOnGetSkill, `MaxValue`, MinValue, CharacterPoints1, CharacterPoints2 FROM skilllineabilitydbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new skilllineabilityMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 14;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(skilllineabilityRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SkillId = reader.GetInt32("SkillId");
                    body.records[i].record.SpellId = reader.GetInt32("SpellId");
                    body.records[i].record.Racemask = reader.GetInt32("Racemask");
                    body.records[i].record.Classemask = reader.GetInt32("Classemask");
                    body.records[i].record.RacemaskNot = reader.GetInt32("RacemaskNot");
                    body.records[i].record.ClassemaskNot = reader.GetInt32("ClassemaskNot");
                    body.records[i].record.ReqSkillValue = reader.GetInt32("ReqSkillValue");
                    body.records[i].record.ForwardSpellId = reader.GetInt32("ForwardSpellId");
                    body.records[i].record.LearnOnGetSkill = reader.GetInt32("LearnOnGetSkill");
                    body.records[i].record.MaxValue = reader.GetInt32("MaxValue");
                    body.records[i].record.MinValue = reader.GetInt32("MinValue");
                    body.records[i].record.CharacterPoints1 = reader.GetInt32("CharacterPoints1");
                    body.records[i].record.CharacterPoints2 = reader.GetInt32("CharacterPoints2");
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
                    count = Marshal.SizeOf(typeof(skilllineabilityRecord)); // Write main body
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

            return true; } } // skilllineability

    public class skilllinedbc {
        public DBCHeader header;
        public skilllineBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM skilllinedbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, CategoryId, SkillCostId, Name, Name_loc2, Description, Description_loc2, SpellIcon, AlternateVerb, AlternateVerb_loc2, CanLink FROM skilllinedbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new skilllineMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 56;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(skilllineRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.CategoryId = reader.GetInt32("CategoryId");
                    body.records[i].record.SkillCostId = reader.GetInt32("SkillCostId");
                    body.records[i].record.SpellIcon = reader.GetInt32("SpellIcon");
                    body.records[i].record.CanLink = reader.GetInt32("CanLink");

                    body.records[i].Name = new string[17];
                    body.records[i].Description = new string[17];
                    body.records[i].AlternateVerb = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.Description = new UInt32[17];
                    body.records[i].record.AlternateVerb = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name[loc] = "";
                        body.records[i].Description[loc] = "";
                        body.records[i].AlternateVerb[loc] = ""; }
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");
                    body.records[i].Description[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Description_loc2" : "Description");
                    body.records[i].AlternateVerb[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "AlternateVerb_loc2" : "AlternateVerb");

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
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } }
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
                        // AlternateVerb
                        if (body.records[i].AlternateVerb[j].Length == 0)
                            body.records[i].record.AlternateVerb[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].AlternateVerb[j])) body.records[i].record.AlternateVerb[j] = offsetStorage[body.records[i].AlternateVerb[j]];
                            else {
                                body.records[i].record.AlternateVerb[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].AlternateVerb[j]) + 1;
                                offsetStorage.Add(body.records[i].AlternateVerb[j], body.records[i].record.AlternateVerb[j]);
                                reverseStorage.Add(body.records[i].record.AlternateVerb[j], body.records[i].AlternateVerb[j]); } } }

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
                    count = Marshal.SizeOf(typeof(skilllineRecord)); // Write main body
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

            return true; } } // skillline
    
    public class skillraceclassinfodbc {
        public DBCHeader header;
        public skillraceclassinfoBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM skillraceclassinfodbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SkillLine, Race, Class, Flags, MinLevel, SkillTier, SkillCostId FROM skillraceclassinfodbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new skillraceclassinfoMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 8;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(skillraceclassinfoRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SkillLine = reader.GetInt32("SkillLine");
                    body.records[i].record.Race = reader.GetInt32("Race");
                    body.records[i].record.Class = reader.GetInt32("Class");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.MinLevel = reader.GetInt32("MinLevel");
                    body.records[i].record.SkillTier = reader.GetInt32("SkillTier");
                    body.records[i].record.SkillCostId = reader.GetInt32("SkillCostId");
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
                    count = Marshal.SizeOf(typeof(skillraceclassinfoRecord)); // Write main body
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

            return true; } } // skillraceclassinfo

    public class skilltiersdbc {
        public DBCHeader header;
        public skilltiersBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM skilltiersdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, SkillValue1, SkillValue2, SkillValue3, SkillValue4, SkillValue5, SkillValue6, SkillValue7, SkillValue8, SkillValue9, SkillValue10, SkillValue11, SkillValue12, SkillValue13, SkillValue14, SkillValue15, SkillValue16, MaxValue1, MaxValue2, MaxValue3, MaxValue4, MaxValue5, MaxValue6, MaxValue7, MaxValue8, MaxValue9, MaxValue10, MaxValue11, MaxValue12, MaxValue13, MaxValue14, MaxValue15, MaxValue16 FROM skilltiersdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new skilltiersMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 33;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(skilltiersRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SkillValue1 = reader.GetInt32("SkillValue1");
                    body.records[i].record.SkillValue2 = reader.GetInt32("SkillValue2");
                    body.records[i].record.SkillValue3 = reader.GetInt32("SkillValue3");
                    body.records[i].record.SkillValue4 = reader.GetInt32("SkillValue4");
                    body.records[i].record.SkillValue5 = reader.GetInt32("SkillValue5");
                    body.records[i].record.SkillValue6 = reader.GetInt32("SkillValue6");
                    body.records[i].record.SkillValue7 = reader.GetInt32("SkillValue7");
                    body.records[i].record.SkillValue8 = reader.GetInt32("SkillValue8");
                    body.records[i].record.SkillValue9 = reader.GetInt32("SkillValue9");
                    body.records[i].record.SkillValue10 = reader.GetInt32("SkillValue10");
                    body.records[i].record.SkillValue11 = reader.GetInt32("SkillValue11");
                    body.records[i].record.SkillValue12 = reader.GetInt32("SkillValue12");
                    body.records[i].record.SkillValue13 = reader.GetInt32("SkillValue13");
                    body.records[i].record.SkillValue14 = reader.GetInt32("SkillValue14");
                    body.records[i].record.SkillValue15 = reader.GetInt32("SkillValue15");
                    body.records[i].record.SkillValue16 = reader.GetInt32("SkillValue16");
                    body.records[i].record.MaxValue1 = reader.GetInt32("MaxValue1");
                    body.records[i].record.MaxValue2 = reader.GetInt32("MaxValue2");
                    body.records[i].record.MaxValue3 = reader.GetInt32("MaxValue3");
                    body.records[i].record.MaxValue4 = reader.GetInt32("MaxValue4");
                    body.records[i].record.MaxValue5 = reader.GetInt32("MaxValue5");
                    body.records[i].record.MaxValue6 = reader.GetInt32("MaxValue6");
                    body.records[i].record.MaxValue7 = reader.GetInt32("MaxValue7");
                    body.records[i].record.MaxValue8 = reader.GetInt32("MaxValue8");
                    body.records[i].record.MaxValue9 = reader.GetInt32("MaxValue9");
                    body.records[i].record.MaxValue10 = reader.GetInt32("MaxValue10");
                    body.records[i].record.MaxValue11 = reader.GetInt32("MaxValue11");
                    body.records[i].record.MaxValue12 = reader.GetInt32("MaxValue12");
                    body.records[i].record.MaxValue13 = reader.GetInt32("MaxValue13");
                    body.records[i].record.MaxValue14 = reader.GetInt32("MaxValue14");
                    body.records[i].record.MaxValue15 = reader.GetInt32("MaxValue15");
                    body.records[i].record.MaxValue16 = reader.GetInt32("MaxValue16");
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
                    count = Marshal.SizeOf(typeof(skilltiersRecord)); // Write main body
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

            return true; } } // skilltiers

    public class soundentriesdbc {
        public DBCHeader header;
        public soundentriesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM soundentriesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Type, InternalName, FileName1, FileName2, FileName3, FileName4, FileName5, FileName6, FileName7, FileName8, FileName9, FileName10, Freq1, Freq2, Freq3, Freq4, Freq5, Freq6, Freq7, Freq8, Freq9, Freq10, Path, Volume, Flags, MinDistance, DistanceCutOff, EAXDef, SoundEntriesAdvancedId FROM soundentriesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new soundentriesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 30;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(soundentriesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Type = reader.GetInt32("Type");
                    body.records[i].InternalName = reader.GetString("InternalName");
                    body.records[i].FileName1 = reader.GetString("FileName1");
                    body.records[i].FileName2 = reader.GetString("FileName2");
                    body.records[i].FileName3 = reader.GetString("FileName3");
                    body.records[i].FileName4 = reader.GetString("FileName4");
                    body.records[i].FileName5 = reader.GetString("FileName5");
                    body.records[i].FileName6 = reader.GetString("FileName6");
                    body.records[i].FileName7 = reader.GetString("FileName7");
                    body.records[i].FileName8 = reader.GetString("FileName8");
                    body.records[i].FileName9 = reader.GetString("FileName9");
                    body.records[i].FileName10 = reader.GetString("FileName10");
                    body.records[i].record.Freq1 = reader.GetInt32("Freq1");
                    body.records[i].record.Freq2 = reader.GetInt32("Freq2");
                    body.records[i].record.Freq3 = reader.GetInt32("Freq3");
                    body.records[i].record.Freq4 = reader.GetInt32("Freq4");
                    body.records[i].record.Freq5 = reader.GetInt32("Freq5");
                    body.records[i].record.Freq6 = reader.GetInt32("Freq6");
                    body.records[i].record.Freq7 = reader.GetInt32("Freq7");
                    body.records[i].record.Freq8 = reader.GetInt32("Freq8");
                    body.records[i].record.Freq9 = reader.GetInt32("Freq9");
                    body.records[i].record.Freq10 = reader.GetInt32("Freq10");
                    body.records[i].Path = reader.GetString("Path");
                    body.records[i].record.Volume = reader.GetFloat("Volume");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.MinDistance = reader.GetFloat("MinDistance");
                    body.records[i].record.DistanceCutOff = reader.GetFloat("DistanceCutOff");
                    body.records[i].record.EAXDef = reader.GetInt32("EAXDef");
                    body.records[i].record.SoundEntriesAdvancedId = reader.GetInt32("SoundEntriesAdvancedId");
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
                            reverseStorage.Add(body.records[i].record.InternalName, body.records[i].InternalName); } }
                    // FileName1
                    if (body.records[i].FileName1.Length == 0)
                        body.records[i].record.FileName1 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName1)) body.records[i].record.FileName1 = offsetStorage[body.records[i].FileName1];
                        else {
                            body.records[i].record.FileName1 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName1) + 1;
                            offsetStorage.Add(body.records[i].FileName1, body.records[i].record.FileName1);
                            reverseStorage.Add(body.records[i].record.FileName1, body.records[i].FileName1); } }
                    // FileName2
                    if (body.records[i].FileName2.Length == 0)
                        body.records[i].record.FileName2 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName2)) body.records[i].record.FileName2 = offsetStorage[body.records[i].FileName2];
                        else {
                            body.records[i].record.FileName2 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName2) + 1;
                            offsetStorage.Add(body.records[i].FileName2, body.records[i].record.FileName2);
                            reverseStorage.Add(body.records[i].record.FileName2, body.records[i].FileName2); } }
                    // FileName3
                    if (body.records[i].FileName3.Length == 0)
                        body.records[i].record.FileName3 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName3)) body.records[i].record.FileName3 = offsetStorage[body.records[i].FileName3];
                        else {
                            body.records[i].record.FileName3 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName3) + 1;
                            offsetStorage.Add(body.records[i].FileName3, body.records[i].record.FileName3);
                            reverseStorage.Add(body.records[i].record.FileName3, body.records[i].FileName3); } }
                    // FileName4
                    if (body.records[i].FileName4.Length == 0)
                        body.records[i].record.FileName4 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName4)) body.records[i].record.FileName4 = offsetStorage[body.records[i].FileName4];
                        else {
                            body.records[i].record.FileName4 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName4) + 1;
                            offsetStorage.Add(body.records[i].FileName4, body.records[i].record.FileName4);
                            reverseStorage.Add(body.records[i].record.FileName4, body.records[i].FileName4); } }
                    // FileName5
                    if (body.records[i].FileName5.Length == 0)
                        body.records[i].record.FileName5 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName5)) body.records[i].record.FileName5 = offsetStorage[body.records[i].FileName5];
                        else {
                            body.records[i].record.FileName5 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName5) + 1;
                            offsetStorage.Add(body.records[i].FileName5, body.records[i].record.FileName5);
                            reverseStorage.Add(body.records[i].record.FileName5, body.records[i].FileName5); } }
                    // FileName6
                    if (body.records[i].FileName6.Length == 0)
                        body.records[i].record.FileName6 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName6)) body.records[i].record.FileName6 = offsetStorage[body.records[i].FileName6];
                        else {
                            body.records[i].record.FileName6 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName6) + 1;
                            offsetStorage.Add(body.records[i].FileName6, body.records[i].record.FileName6);
                            reverseStorage.Add(body.records[i].record.FileName6, body.records[i].FileName6); } }
                    // FileName7
                    if (body.records[i].FileName7.Length == 0)
                        body.records[i].record.FileName7 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName7)) body.records[i].record.FileName7 = offsetStorage[body.records[i].FileName7];
                        else {
                            body.records[i].record.FileName7 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName7) + 1;
                            offsetStorage.Add(body.records[i].FileName7, body.records[i].record.FileName7);
                            reverseStorage.Add(body.records[i].record.FileName7, body.records[i].FileName7); } }
                    // FileName8
                    if (body.records[i].FileName8.Length == 0)
                        body.records[i].record.FileName8 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName8)) body.records[i].record.FileName8 = offsetStorage[body.records[i].FileName8];
                        else {
                            body.records[i].record.FileName8 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName8) + 1;
                            offsetStorage.Add(body.records[i].FileName8, body.records[i].record.FileName8);
                            reverseStorage.Add(body.records[i].record.FileName8, body.records[i].FileName8); } }
                    // FileName9
                    if (body.records[i].FileName9.Length == 0)
                        body.records[i].record.FileName9 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName9)) body.records[i].record.FileName9 = offsetStorage[body.records[i].FileName9];
                        else {
                            body.records[i].record.FileName9 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName9) + 1;
                            offsetStorage.Add(body.records[i].FileName9, body.records[i].record.FileName9);
                            reverseStorage.Add(body.records[i].record.FileName9, body.records[i].FileName9); } }
                    // FileName10
                    if (body.records[i].FileName10.Length == 0)
                        body.records[i].record.FileName10 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].FileName10)) body.records[i].record.FileName10 = offsetStorage[body.records[i].FileName10];
                        else {
                            body.records[i].record.FileName10 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].FileName10) + 1;
                            offsetStorage.Add(body.records[i].FileName10, body.records[i].record.FileName10);
                            reverseStorage.Add(body.records[i].record.FileName10, body.records[i].FileName10); } }
                    // Path
                    if (body.records[i].Path.Length == 0)
                        body.records[i].record.Path = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].Path)) body.records[i].record.Path = offsetStorage[body.records[i].Path];
                        else {
                            body.records[i].record.Path = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Path) + 1;
                            offsetStorage.Add(body.records[i].Path, body.records[i].record.Path);
                            reverseStorage.Add(body.records[i].record.Path, body.records[i].Path); } } }

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
                    count = Marshal.SizeOf(typeof(soundentriesRecord)); // Write main body
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

            return true; } } // soundentries

    public class spellcasttimesdbc {
        public DBCHeader header;
        public spellcasttimesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellcasttimesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, CastTime, CastTimePetLevel, MinCastTime FROM spellcasttimesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spellcasttimesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellcasttimesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.CastTime = reader.GetInt32("CastTime");
                    body.records[i].record.CastTimePetLevel = reader.GetInt32("CastTimePetLevel");
                    body.records[i].record.MinCastTime = reader.GetInt32("MinCastTime");
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
                    count = Marshal.SizeOf(typeof(spellcasttimesRecord)); // Write main body
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

            return true; } } // spellcasttimes

    public class spellcategorydbc {
        public DBCHeader header;
        public spellcategoryBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellcategorydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Flags FROM spellcategorydbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spellcategoryMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellcategoryRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
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
                    count = Marshal.SizeOf(typeof(spellcategoryRecord)); // Write main body
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

            return true; } } // spellcategory



    public class spelldbc {
        public DBCHeader header;
        public spellBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spelldbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Entry, Type, Category, Dispel, Mechanic, Attributes, AttributesEx, AttributesEx2, AttributesEx3, AttributesEx4, AttributesEx5, AttributesEx6, AttributesEx7, Stances, unk320_2, StancesNot, unk320_3, Targets, TargetCreatureType, RequiresSpellFocus, FacingCasterFlags, CasterAuraState, TargetAuraState, CasterAuraStateNot, TargetAuraStateNot, casterAuraSpell, targetAuraSpell, excludeCasterAuraSpell, excludeTargetAuraSpell, CastingTimeIndex, RecoveryTime, CategoryRecoveryTime, InterruptFlags, AuraInterruptFlags, ChannelInterruptFlags, procFlags, procChance, procCharges, maxLevel, baseLevel, spellLevel, DurationIndex, powerType, manaCost, manaCostPerlevel, manaPerSecond, manaPerSecondPerLevel, rangeIndex, speed, modalNextSpell, StackAmount, Totem1, Totem2, Reagent1, Reagent2, Reagent3, Reagent4, Reagent5, Reagent6, Reagent7, Reagent8, ReagentCount1, ReagentCount2, ReagentCount3, ReagentCount4, ReagentCount5, ReagentCount6, ReagentCount7, ReagentCount8, EquippedItemClass, EquippedItemSubClassMask, EquippedItemInventoryTypeMask, Effect1, Effect2, Effect3, EffectDieSides1, EffectDieSides2, EffectDieSides3, EffectRealPointsPerLevel1, EffectRealPointsPerLevel2, EffectRealPointsPerLevel3, EffectBasePoints1, EffectBasePoints2, EffectBasePoints3, EffectMechanic1, EffectMechanic2, EffectMechanic3, EffectImplicitTargetA1, EffectImplicitTargetA2, EffectImplicitTargetA3, EffectImplicitTargetB1, EffectImplicitTargetB2, EffectImplicitTargetB3, EffectRadiusIndex1, EffectRadiusIndex2, EffectRadiusIndex3, EffectApplyAuraName1, EffectApplyAuraName2, EffectApplyAuraName3, EffectAmplitude1, EffectAmplitude2, EffectAmplitude3, EffectValueMultiplier1, EffectValueMultiplier2, EffectValueMultiplier3, EffectChainTarget1, EffectChainTarget2, EffectChainTarget3, EffectItemType1, EffectItemType2, EffectItemType3, EffectMiscValue1, EffectMiscValue2, EffectMiscValue3, EffectMiscValueB1, EffectMiscValueB2, EffectMiscValueB3, EffectTriggerSpell1, EffectTriggerSpell2, EffectTriggerSpell3, EffectPointsPerComboPoint1, EffectPointsPerComboPoint2, EffectPointsPerComboPoint3, EffectSpellClassMaskA1, EffectSpellClassMaskA2, EffectSpellClassMaskA3, EffectSpellClassMaskB1, EffectSpellClassMaskB2, EffectSpellClassMaskB3, EffectSpellClassMaskC1, EffectSpellClassMaskC2, EffectSpellClassMaskC3, SpellVisual1, SpellVisual2, SpellIconID, activeIconID, spellPriority, Name, Name_loc2, Rank, Rank_loc2, Description, Description_loc2, ToolTip, ToolTip_loc2, ManaCostPercentage, StartRecoveryCategory, StartRecoveryTime, MaxTargetLevel, SpellFamilyName, SpellFamilyFlagsLow, SpellFamilyFlagsHigh, SpellFamilyFlags2, MaxAffectedTargets, DmgClass, PreventionType, StanceBarOrder, EffectDamageMultiplier1, EffectDamageMultiplier2, EffectDamageMultiplier3, MinFactionId, MinReputation, RequiredAuraVision, TotemCategory1, TotemCategory2, AreaGroupId, SchoolMask, runeCostID, spellMissileID, PowerDisplayId, EffectBonusMultiplier1, EffectBonusMultiplier2, EffectBonusMultiplier3, spellDescriptionVariableID, SpellDifficultyId FROM spelldbc ORDER BY Entry ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spellMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 234;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Entry = reader.GetUInt32("Entry");
                    body.records[i].record.Category = reader.GetUInt32("Category");
                    body.records[i].record.Dispel = reader.GetUInt32("Dispel");
                    body.records[i].record.Mechanic = reader.GetUInt32("Mechanic");
                    body.records[i].record.Attributes = reader.GetUInt32("Attributes");
                    body.records[i].record.AttributesEx = reader.GetUInt32("AttributesEx");
                    body.records[i].record.AttributesEx2 = reader.GetUInt32("AttributesEx2");
                    body.records[i].record.AttributesEx3 = reader.GetUInt32("AttributesEx3");
                    body.records[i].record.AttributesEx4 = reader.GetUInt32("AttributesEx4");
                    body.records[i].record.AttributesEx5 = reader.GetUInt32("AttributesEx5");
                    body.records[i].record.AttributesEx6 = reader.GetUInt32("AttributesEx6");
                    body.records[i].record.AttributesEx7 = reader.GetUInt32("AttributesEx7");
                    body.records[i].record.Stances = reader.GetUInt32("Stances");
                    body.records[i].record.unk320_2 = reader.GetUInt32("unk320_2");
                    body.records[i].record.StancesNot = reader.GetUInt32("StancesNot");
                    body.records[i].record.unk320_3 = reader.GetUInt32("unk320_3");
                    body.records[i].record.Targets = reader.GetUInt32("Targets");
                    body.records[i].record.TargetCreatureType = reader.GetUInt32("TargetCreatureType");
                    body.records[i].record.RequiresSpellFocus = reader.GetUInt32("RequiresSpellFocus");
                    body.records[i].record.FacingCasterFlags = reader.GetUInt32("FacingCasterFlags");
                    body.records[i].record.CasterAuraState = reader.GetUInt32("CasterAuraState");
                    body.records[i].record.TargetAuraState = reader.GetUInt32("TargetAuraState");
                    body.records[i].record.CasterAuraStateNot = reader.GetUInt32("CasterAuraStateNot");
                    body.records[i].record.TargetAuraStateNot = reader.GetUInt32("TargetAuraStateNot");
                    body.records[i].record.casterAuraSpell = reader.GetUInt32("casterAuraSpell");
                    body.records[i].record.targetAuraSpell = reader.GetUInt32("targetAuraSpell");
                    body.records[i].record.excludeCasterAuraSpell = reader.GetUInt32("excludeCasterAuraSpell");
                    body.records[i].record.excludeTargetAuraSpell = reader.GetUInt32("excludeTargetAuraSpell");
                    body.records[i].record.CastingTimeIndex = reader.GetUInt32("CastingTimeIndex");
                    body.records[i].record.RecoveryTime = reader.GetUInt32("RecoveryTime");
                    body.records[i].record.CategoryRecoveryTime = reader.GetUInt32("CategoryRecoveryTime");
                    body.records[i].record.InterruptFlags = reader.GetUInt32("InterruptFlags");
                    body.records[i].record.AuraInterruptFlags = reader.GetUInt32("AuraInterruptFlags");
                    body.records[i].record.ChannelInterruptFlags = reader.GetUInt32("ChannelInterruptFlags");
                    body.records[i].record.procFlags = reader.GetUInt32("procFlags");
                    body.records[i].record.procChance = reader.GetUInt32("procChance");
                    body.records[i].record.procCharges = reader.GetUInt32("procCharges");
                    body.records[i].record.maxLevel = reader.GetUInt32("maxLevel");
                    body.records[i].record.baseLevel = reader.GetUInt32("baseLevel");
                    body.records[i].record.spellLevel = reader.GetUInt32("spellLevel");
                    body.records[i].record.DurationIndex = reader.GetUInt32("DurationIndex");
                    body.records[i].record.powerType = reader.GetUInt32("powerType");
                    body.records[i].record.manaCost = reader.GetUInt32("manaCost");
                    body.records[i].record.manaCostPerlevel = reader.GetUInt32("manaCostPerlevel");
                    body.records[i].record.manaPerSecond = reader.GetUInt32("manaPerSecond");
                    body.records[i].record.manaPerSecondPerLevel = reader.GetUInt32("manaPerSecondPerLevel");
                    body.records[i].record.rangeIndex = reader.GetUInt32("rangeIndex");
                    body.records[i].record.speed = reader.GetFloat("speed");
                    body.records[i].record.modalNextSpell = reader.GetInt32("modalNextSpell");
                    body.records[i].record.StackAmount = reader.GetInt32("StackAmount");
                    body.records[i].record.Totem1 = reader.GetInt32("Totem1");
                    body.records[i].record.Totem2 = reader.GetInt32("Totem2");
                    body.records[i].record.Reagent1 = reader.GetInt32("Reagent1");
                    body.records[i].record.Reagent2 = reader.GetInt32("Reagent2");
                    body.records[i].record.Reagent3 = reader.GetInt32("Reagent3");
                    body.records[i].record.Reagent4 = reader.GetInt32("Reagent4");
                    body.records[i].record.Reagent5 = reader.GetInt32("Reagent5");
                    body.records[i].record.Reagent6 = reader.GetInt32("Reagent6");
                    body.records[i].record.Reagent7 = reader.GetInt32("Reagent7");
                    body.records[i].record.Reagent8 = reader.GetInt32("Reagent8");
                    body.records[i].record.ReagentCount1 = reader.GetInt32("ReagentCount1");
                    body.records[i].record.ReagentCount2 = reader.GetInt32("ReagentCount2");
                    body.records[i].record.ReagentCount3 = reader.GetInt32("ReagentCount3");
                    body.records[i].record.ReagentCount4 = reader.GetInt32("ReagentCount4");
                    body.records[i].record.ReagentCount5 = reader.GetInt32("ReagentCount5");
                    body.records[i].record.ReagentCount6 = reader.GetInt32("ReagentCount6");
                    body.records[i].record.ReagentCount7 = reader.GetInt32("ReagentCount7");
                    body.records[i].record.ReagentCount8 = reader.GetInt32("ReagentCount8");
                    body.records[i].record.EquippedItemClass = reader.GetInt32("EquippedItemClass");
                    body.records[i].record.EquippedItemSubClassMask = reader.GetInt32("EquippedItemSubClassMask");
                    body.records[i].record.EquippedItemInventoryTypeMask = reader.GetInt32("EquippedItemInventoryTypeMask");
                    body.records[i].record.Effect1 = reader.GetInt32("Effect1");
                    body.records[i].record.Effect2 = reader.GetInt32("Effect2");
                    body.records[i].record.Effect3 = reader.GetInt32("Effect3");
                    body.records[i].record.EffectDieSides1 = reader.GetInt32("EffectDieSides1");
                    body.records[i].record.EffectDieSides2 = reader.GetInt32("EffectDieSides2");
                    body.records[i].record.EffectDieSides3 = reader.GetInt32("EffectDieSides3");
                    body.records[i].record.EffectRealPointsPerLevel1 = reader.GetFloat("EffectRealPointsPerLevel1");
                    body.records[i].record.EffectRealPointsPerLevel2 = reader.GetFloat("EffectRealPointsPerLevel2");
                    body.records[i].record.EffectRealPointsPerLevel3 = reader.GetFloat("EffectRealPointsPerLevel3");
                    body.records[i].record.EffectBasePoints1 = reader.GetInt32("EffectBasePoints1");
                    body.records[i].record.EffectBasePoints2 = reader.GetInt32("EffectBasePoints2");
                    body.records[i].record.EffectBasePoints3 = reader.GetInt32("EffectBasePoints3");
                    body.records[i].record.EffectMechanic1 = reader.GetInt32("EffectMechanic1");
                    body.records[i].record.EffectMechanic2 = reader.GetInt32("EffectMechanic2");
                    body.records[i].record.EffectMechanic3 = reader.GetInt32("EffectMechanic3");
                    body.records[i].record.EffectImplicitTargetA1 = reader.GetInt32("EffectImplicitTargetA1");
                    body.records[i].record.EffectImplicitTargetA2 = reader.GetInt32("EffectImplicitTargetA2");
                    body.records[i].record.EffectImplicitTargetA3 = reader.GetInt32("EffectImplicitTargetA3");
                    body.records[i].record.EffectImplicitTargetB1 = reader.GetInt32("EffectImplicitTargetB1");
                    body.records[i].record.EffectImplicitTargetB2 = reader.GetInt32("EffectImplicitTargetB2");
                    body.records[i].record.EffectImplicitTargetB3 = reader.GetInt32("EffectImplicitTargetB3");
                    body.records[i].record.EffectRadiusIndex1 = reader.GetInt32("EffectRadiusIndex1");
                    body.records[i].record.EffectRadiusIndex2 = reader.GetInt32("EffectRadiusIndex2");
                    body.records[i].record.EffectRadiusIndex3 = reader.GetInt32("EffectRadiusIndex3");
                    body.records[i].record.EffectApplyAuraName1 = reader.GetInt32("EffectApplyAuraName1");
                    body.records[i].record.EffectApplyAuraName2 = reader.GetInt32("EffectApplyAuraName2");
                    body.records[i].record.EffectApplyAuraName3 = reader.GetInt32("EffectApplyAuraName3");
                    body.records[i].record.EffectAmplitude1 = reader.GetInt32("EffectAmplitude1");
                    body.records[i].record.EffectAmplitude2 = reader.GetInt32("EffectAmplitude2");
                    body.records[i].record.EffectAmplitude3 = reader.GetInt32("EffectAmplitude3");
                    body.records[i].record.EffectValueMultiplier1 = reader.GetFloat("EffectValueMultiplier1");
                    body.records[i].record.EffectValueMultiplier2 = reader.GetFloat("EffectValueMultiplier2");
                    body.records[i].record.EffectValueMultiplier3 = reader.GetFloat("EffectValueMultiplier3");
                    body.records[i].record.EffectChainTarget1 = reader.GetInt32("EffectChainTarget1");
                    body.records[i].record.EffectChainTarget2 = reader.GetInt32("EffectChainTarget2");
                    body.records[i].record.EffectChainTarget3 = reader.GetInt32("EffectChainTarget3");
                    body.records[i].record.EffectItemType1 = reader.GetInt32("EffectItemType1");
                    body.records[i].record.EffectItemType2 = reader.GetInt32("EffectItemType2");
                    body.records[i].record.EffectItemType3 = reader.GetInt32("EffectItemType3");
                    body.records[i].record.EffectMiscValue1 = reader.GetInt32("EffectMiscValue1");
                    body.records[i].record.EffectMiscValue2 = reader.GetInt32("EffectMiscValue2");
                    body.records[i].record.EffectMiscValue3 = reader.GetInt32("EffectMiscValue3");
                    body.records[i].record.EffectMiscValueB1 = reader.GetInt32("EffectMiscValueB1");
                    body.records[i].record.EffectMiscValueB2 = reader.GetInt32("EffectMiscValueB2");
                    body.records[i].record.EffectMiscValueB3 = reader.GetInt32("EffectMiscValueB3");
                    body.records[i].record.EffectTriggerSpell1 = reader.GetInt32("EffectTriggerSpell1");
                    body.records[i].record.EffectTriggerSpell2 = reader.GetInt32("EffectTriggerSpell2");
                    body.records[i].record.EffectTriggerSpell3 = reader.GetInt32("EffectTriggerSpell3");
                    body.records[i].record.EffectPointsPerComboPoint1 = reader.GetFloat("EffectPointsPerComboPoint1");
                    body.records[i].record.EffectPointsPerComboPoint2 = reader.GetFloat("EffectPointsPerComboPoint2");
                    body.records[i].record.EffectPointsPerComboPoint3 = reader.GetFloat("EffectPointsPerComboPoint3");
                    body.records[i].record.EffectSpellClassMaskA1 = reader.GetUInt32("EffectSpellClassMaskA1");
                    body.records[i].record.EffectSpellClassMaskA2 = reader.GetUInt32("EffectSpellClassMaskA2");
                    body.records[i].record.EffectSpellClassMaskA3 = reader.GetUInt32("EffectSpellClassMaskA3");
                    body.records[i].record.EffectSpellClassMaskB1 = reader.GetUInt32("EffectSpellClassMaskB1");
                    body.records[i].record.EffectSpellClassMaskB2 = reader.GetUInt32("EffectSpellClassMaskB2");
                    body.records[i].record.EffectSpellClassMaskB3 = reader.GetUInt32("EffectSpellClassMaskB3");
                    body.records[i].record.EffectSpellClassMaskC1 = reader.GetUInt32("EffectSpellClassMaskC1");
                    body.records[i].record.EffectSpellClassMaskC2 = reader.GetUInt32("EffectSpellClassMaskC2");
                    body.records[i].record.EffectSpellClassMaskC3 = reader.GetUInt32("EffectSpellClassMaskC3");
                    body.records[i].record.SpellVisual1 = reader.GetInt32("SpellVisual1");
                    body.records[i].record.SpellVisual2 = reader.GetInt32("SpellVisual2");
                    body.records[i].record.SpellIconID = reader.GetInt32("SpellIconID");
                    body.records[i].record.activeIconID = reader.GetInt32("activeIconID");
                    body.records[i].record.spellPriority = reader.GetInt32("spellPriority");
                    body.records[i].record.ManaCostPercentage = reader.GetUInt32("ManaCostPercentage");
                    body.records[i].record.StartRecoveryCategory = reader.GetUInt32("StartRecoveryCategory");
                    body.records[i].record.StartRecoveryTime = reader.GetUInt32("StartRecoveryTime");
                    body.records[i].record.MaxTargetLevel = reader.GetUInt32("MaxTargetLevel");
                    body.records[i].record.SpellFamilyName = reader.GetUInt32("SpellFamilyName");
                    body.records[i].record.SpellFamilyFlagsLow = reader.GetUInt32("SpellFamilyFlagsLow");
                    body.records[i].record.SpellFamilyFlagsHigh = reader.GetUInt32("SpellFamilyFlagsHigh");
                    body.records[i].record.SpellFamilyFlags2 = reader.GetUInt32("SpellFamilyFlags2");
                    body.records[i].record.MaxAffectedTargets = reader.GetUInt32("MaxAffectedTargets");
                    body.records[i].record.DmgClass = reader.GetUInt32("DmgClass");
                    body.records[i].record.PreventionType = reader.GetUInt32("PreventionType");
                    body.records[i].record.StanceBarOrder = reader.GetUInt32("StanceBarOrder");
                    body.records[i].record.EffectDamageMultiplier1 = reader.GetFloat("EffectDamageMultiplier1");
                    body.records[i].record.EffectDamageMultiplier2 = reader.GetFloat("EffectDamageMultiplier2");
                    body.records[i].record.EffectDamageMultiplier3 = reader.GetFloat("EffectDamageMultiplier3");
                    body.records[i].record.MinFactionId = reader.GetUInt32("MinFactionId");
                    body.records[i].record.MinReputation = reader.GetUInt32("MinReputation");
                    body.records[i].record.RequiredAuraVision = reader.GetUInt32("RequiredAuraVision");
                    body.records[i].record.TotemCategory1 = reader.GetUInt32("TotemCategory1");
                    body.records[i].record.TotemCategory2 = reader.GetUInt32("TotemCategory2");
                    body.records[i].record.AreaGroupId = reader.GetInt32("AreaGroupId");
                    body.records[i].record.SchoolMask = reader.GetUInt32("SchoolMask");
                    body.records[i].record.runeCostID = reader.GetUInt32("runeCostID");
                    body.records[i].record.spellMissileID = reader.GetUInt32("spellMissileID");
                    body.records[i].record.PowerDisplayId = reader.GetUInt32("PowerDisplayId");
                    body.records[i].record.EffectBonusMultiplier1 = reader.GetFloat("EffectBonusMultiplier1");
                    body.records[i].record.EffectBonusMultiplier2 = reader.GetFloat("EffectBonusMultiplier2");
                    body.records[i].record.EffectBonusMultiplier3 = reader.GetFloat("EffectBonusMultiplier3");
                    body.records[i].record.spellDescriptionVariableID = reader.GetUInt32("spellDescriptionVariableID");
                    body.records[i].record.SpellDifficultyId = reader.GetUInt32("SpellDifficultyId");

                    body.records[i].Name = new string[17];
                    body.records[i].Rank = new string[17];
                    body.records[i].Description = new string[17];
                    body.records[i].Tooltip = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.Rank = new UInt32[17];
                    body.records[i].record.Description = new UInt32[17];
                    body.records[i].record.Tooltip = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name[loc] = "";
                        body.records[i].Rank[loc] = "";
                        body.records[i].Description[loc] = "";
                        body.records[i].Tooltip[loc] = ""; }
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");
                    body.records[i].Rank[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Rank_loc2" : "Rank");
                    body.records[i].Description[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Description_loc2" : "Description");
                    body.records[i].Tooltip[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Tooltip_loc2" : "Tooltip");

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
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } }
                        // Rank
                        if (body.records[i].Rank[j].Length == 0)
                            body.records[i].record.Rank[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Rank[j])) body.records[i].record.Rank[j] = offsetStorage[body.records[i].Rank[j]];
                            else {
                                body.records[i].record.Rank[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Rank[j]) + 1;
                                offsetStorage.Add(body.records[i].Rank[j], body.records[i].record.Rank[j]);
                                reverseStorage.Add(body.records[i].record.Rank[j], body.records[i].Rank[j]); } }
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
                        // Tooltip
                        if (body.records[i].Tooltip[j].Length == 0)
                            body.records[i].record.Tooltip[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Tooltip[j])) body.records[i].record.Tooltip[j] = offsetStorage[body.records[i].Tooltip[j]];
                            else {
                                body.records[i].record.Tooltip[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Tooltip[j]) + 1;
                                offsetStorage.Add(body.records[i].Tooltip[j], body.records[i].record.Tooltip[j]);
                                reverseStorage.Add(body.records[i].record.Tooltip[j], body.records[i].Tooltip[j]); } } }

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
                    count = Marshal.SizeOf(typeof(spellRecord)); // Write main body
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

            return true; } } // spell

    public class spelldifficultydbc {
        public DBCHeader header;
        public spelldifficultyBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spelldifficultydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT id, spellid0, spellid1, spellid2, spellid3 FROM spelldifficultydbc ORDER BY id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spelldifficultyMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spelldifficultyRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.id = reader.GetInt32("id");
                    body.records[i].record.spellid0 = reader.GetInt32("spellid0");
                    body.records[i].record.spellid1 = reader.GetInt32("spellid1");
                    body.records[i].record.spellid2 = reader.GetInt32("spellid2");
                    body.records[i].record.spellid3 = reader.GetInt32("spellid3");
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
                    count = Marshal.SizeOf(typeof(spelldifficultyRecord)); // Write main body
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

            return true; } } // spelldifficulty

    public class spelldurationdbc {
        public DBCHeader header;
        public spelldurationBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spelldurationdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Duration1, Duration2, Duration3 FROM spelldurationdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spelldurationMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spelldurationRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Duration1 = reader.GetInt32("Duration1");
                    body.records[i].record.Duration2 = reader.GetInt32("Duration2");
                    body.records[i].record.Duration3 = reader.GetInt32("Duration3");
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
                    count = Marshal.SizeOf(typeof(spelldurationRecord)); // Write main body
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

            return true; } } // spellduration

    public class spellfocusobjectdbc {
        public DBCHeader header;
        public spellfocusobjectBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellfocusobjectdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2 FROM spellfocusobjectdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spellfocusobjectMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 18;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellfocusobjectRecord));

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
                    count = Marshal.SizeOf(typeof(spellfocusobjectRecord)); // Write main body
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

            return true; } } // spellfocusobject

    public class spellitemenchantmentconditiondbc {
        public DBCHeader header;
        public spellitemenchantmentconditionBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellitemenchantmentconditiondbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Color1, Color2, Color3, Color4, Color5, LTOperand1, LTOperand2, LTOperand3, LTOperand4, LTOperand5, Comparator1, Comparator2, Comparator3, Comparator4, Comparator5, CompareColor1, CompareColor2, CompareColor3, CompareColor4, CompareColor5, Value1, Value2, Value3, Value4, Value5, Logic1, Logic2, Logic3, Logic4, Logic5 FROM spellitemenchantmentconditiondbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spellitemenchantmentconditionMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 31;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellitemenchantmentconditionRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Color1 = reader.GetByte("Color1");
                    body.records[i].record.Color2 = reader.GetByte("Color2");
                    body.records[i].record.Color3 = reader.GetByte("Color3");
                    body.records[i].record.Color4 = reader.GetByte("Color4");
                    body.records[i].record.Color5 = reader.GetByte("Color5");
                    body.records[i].record.LTOperand1 = reader.GetInt32("LTOperand1");
                    body.records[i].record.LTOperand2 = reader.GetInt32("LTOperand2");
                    body.records[i].record.LTOperand3 = reader.GetInt32("LTOperand3");
                    body.records[i].record.LTOperand4 = reader.GetInt32("LTOperand4");
                    body.records[i].record.LTOperand5 = reader.GetInt32("LTOperand5");
                    body.records[i].record.Comparator1 = reader.GetByte("Comparator1");
                    body.records[i].record.Comparator2 = reader.GetByte("Comparator2");
                    body.records[i].record.Comparator3 = reader.GetByte("Comparator3");
                    body.records[i].record.Comparator4 = reader.GetByte("Comparator4");
                    body.records[i].record.Comparator5 = reader.GetByte("Comparator5");
                    body.records[i].record.CompareColor1 = reader.GetByte("CompareColor1");
                    body.records[i].record.CompareColor2 = reader.GetByte("CompareColor2");
                    body.records[i].record.CompareColor3 = reader.GetByte("CompareColor3");
                    body.records[i].record.CompareColor4 = reader.GetByte("CompareColor4");
                    body.records[i].record.CompareColor5 = reader.GetByte("CompareColor5");
                    body.records[i].record.Value1 = reader.GetInt32("Value1");
                    body.records[i].record.Value2 = reader.GetInt32("Value2");
                    body.records[i].record.Value3 = reader.GetInt32("Value3");
                    body.records[i].record.Value4 = reader.GetInt32("Value4");
                    body.records[i].record.Value5 = reader.GetInt32("Value5");
                    body.records[i].record.Logic1 = reader.GetByte("Logic1");
                    body.records[i].record.Logic2 = reader.GetByte("Logic2");
                    body.records[i].record.Logic3 = reader.GetByte("Logic3");
                    body.records[i].record.Logic4 = reader.GetByte("Logic4");
                    body.records[i].record.Logic5 = reader.GetByte("Logic5");
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
                    count = Marshal.SizeOf(typeof(spellitemenchantmentconditionRecord)); // Write main body
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

            return true; } } // spellitemenchantmentcondition

    public class spellitemenchantmentdbc {
        public DBCHeader header;
        public spellitemenchantmentBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellitemenchantmentdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Charges, Type1, Type2, Type3, Amount1, Amount2, Amount3, AmountB1, AmountB2, AmountB3, SpellId1, SpellId2, SpellId3, Description, Description_loc2, AuraId, Slot, GemId, EnchantmentCondition, RequiredSkill, RequiredSkillValue, RequiredLevel FROM spellitemenchantmentdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spellitemenchantmentMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 38;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellitemenchantmentRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Charges = reader.GetInt32("Charges");
                    body.records[i].record.Type1 = reader.GetInt32("Type1");
                    body.records[i].record.Type2 = reader.GetInt32("Type2");
                    body.records[i].record.Type3 = reader.GetInt32("Type3");
                    body.records[i].record.Amount1 = reader.GetInt32("Amount1");
                    body.records[i].record.Amount2 = reader.GetInt32("Amount2");
                    body.records[i].record.Amount3 = reader.GetInt32("Amount3");
                    body.records[i].record.AmountB1 = reader.GetInt32("AmountB1");
                    body.records[i].record.AmountB2 = reader.GetInt32("AmountB2");
                    body.records[i].record.AmountB3 = reader.GetInt32("AmountB3");
                    body.records[i].record.SpellId1 = reader.GetInt32("SpellId1");
                    body.records[i].record.SpellId2 = reader.GetInt32("SpellId2");
                    body.records[i].record.SpellId3 = reader.GetInt32("SpellId3");
                    body.records[i].record.AuraId = reader.GetInt32("AuraId");
                    body.records[i].record.Slot = reader.GetInt32("Slot");
                    body.records[i].record.GemId = reader.GetInt32("GemId");
                    body.records[i].record.EnchantmentCondition = reader.GetInt32("EnchantmentCondition");
                    body.records[i].record.RequiredSkill = reader.GetInt32("RequiredSkill");
                    body.records[i].record.RequiredSkillValue = reader.GetInt32("RequiredSkillValue");
                    body.records[i].record.RequiredLevel = reader.GetInt32("RequiredLevel");

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
                    count = Marshal.SizeOf(typeof(spellitemenchantmentRecord)); // Write main body
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

            return true; } } // spellitemenchantment

    public class spellradiusdbc {
        public DBCHeader header;
        public spellradiusBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellradiusdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, RadiusMin, RadiusPerLevel, RadiusMax FROM spellradiusdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spellradiusMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellradiusRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.RadiusMin = reader.GetFloat("RadiusMin");
                    body.records[i].record.RadiusPerLevel = reader.GetFloat("RadiusPerLevel");
                    body.records[i].record.RadiusMax = reader.GetFloat("RadiusMax");
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
                    count = Marshal.SizeOf(typeof(spellradiusRecord)); // Write main body
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

            return true; } } // spellradius

    public class spellrangedbc {
        public DBCHeader header;
        public spellrangeBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellrangedbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, MinRangeHostile, MinRangeFriend, MaxRangeHostile, MaxRangeFriend, Type, Name, Name_loc2, Name2, Name2_loc2 FROM spellrangedbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spellrangeMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 40;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellrangeRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MinRangeHostile = reader.GetInt32("MinRangeHostile");
                    body.records[i].record.MinRangeFriend = reader.GetInt32("MinRangeFriend");
                    body.records[i].record.MaxRangeHostile = reader.GetFloat("MaxRangeHostile");
                    body.records[i].record.MaxRangeFriend = reader.GetFloat("MaxRangeFriend");
                    body.records[i].record.Type = reader.GetInt32("Type");

                    body.records[i].Name = new string[17];
                    body.records[i].Name2 = new string[17];
                    body.records[i].record.Name = new UInt32[17];
                    body.records[i].record.Name2 = new UInt32[17];
                    for (int loc = 0; loc < 17; ++loc) {
                        body.records[i].Name[loc] = "";
                        body.records[i].Name2[loc] = ""; }
                    body.records[i].Name[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name_loc2" : "Name");
                    body.records[i].Name2[DB2DBC.GlobalLocalization] = reader.GetString(DB2DBC.GlobalLocalization == 2 ? "Name2_loc2" : "Name2");

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
                                reverseStorage.Add(body.records[i].record.Name[j], body.records[i].Name[j]); } }
                        // Name2
                        if (body.records[i].Name2[j].Length == 0)
                            body.records[i].record.Name2[j] = 0;
                        else {
                            if (offsetStorage.ContainsKey(body.records[i].Name2[j])) body.records[i].record.Name2[j] = offsetStorage[body.records[i].Name2[j]];
                            else {
                                body.records[i].record.Name2[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].Name2[j]) + 1;
                                offsetStorage.Add(body.records[i].Name2[j], body.records[i].record.Name2[j]);
                                reverseStorage.Add(body.records[i].record.Name2[j], body.records[i].Name2[j]); } } }

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
                    count = Marshal.SizeOf(typeof(spellrangeRecord)); // Write main body
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

            return true; } } // spellrange

    public class spellrunecostdbc {
        public DBCHeader header;
        public spellrunecostBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellrunecostdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, RuneCostBlood, RuneCostFrost, RuneCostUnholy, RunePowerGain FROM spellrunecostdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spellrunecostMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 5;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellrunecostRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.RuneCostBlood = reader.GetInt32("RuneCostBlood");
                    body.records[i].record.RuneCostFrost = reader.GetInt32("RuneCostFrost");
                    body.records[i].record.RuneCostUnholy = reader.GetInt32("RuneCostUnholy");
                    body.records[i].record.RunePowerGain = reader.GetInt32("RunePowerGain");
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
                    count = Marshal.SizeOf(typeof(spellrunecostRecord)); // Write main body
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

            return true; } } // spellrunecost

    public class spellshapeshiftformdbc {
        public DBCHeader header;
        public spellshapeshiftformBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spellshapeshiftformdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, ButtonPosition, Name, Name_loc2, Flags1, CreatureType, Unk1, AttackSpeed, ModelIdAlliance, ModelIdHorde, Unk2, Unk3, StanceSpell1, StanceSpell2, StanceSpell3, StanceSpell4, StanceSpell5, StanceSpell6, StanceSpell7, StanceSpell8 FROM spellshapeshiftformdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new spellshapeshiftformMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 35;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(spellshapeshiftformRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.ButtonPosition = reader.GetInt32("ButtonPosition");
                    body.records[i].record.Flags1 = reader.GetInt32("Flags1");
                    body.records[i].record.CreatureType = reader.GetInt32("CreatureType");
                    body.records[i].record.Unk1 = reader.GetInt32("Unk1");
                    body.records[i].record.AttackSpeed = reader.GetInt32("AttackSpeed");
                    body.records[i].record.ModelIdAlliance = reader.GetInt32("ModelIdAlliance");
                    body.records[i].record.ModelIdHorde = reader.GetInt32("ModelIdHorde");
                    body.records[i].record.Unk2 = reader.GetInt32("Unk2");
                    body.records[i].record.Unk3 = reader.GetInt32("Unk3");
                    body.records[i].record.StanceSpell1 = reader.GetInt32("StanceSpell1");
                    body.records[i].record.StanceSpell2 = reader.GetInt32("StanceSpell2");
                    body.records[i].record.StanceSpell3 = reader.GetInt32("StanceSpell3");
                    body.records[i].record.StanceSpell4 = reader.GetInt32("StanceSpell4");
                    body.records[i].record.StanceSpell5 = reader.GetInt32("StanceSpell5");
                    body.records[i].record.StanceSpell6 = reader.GetInt32("StanceSpell6");
                    body.records[i].record.StanceSpell7 = reader.GetInt32("StanceSpell7");
                    body.records[i].record.StanceSpell8 = reader.GetInt32("StanceSpell8");

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
                    count = Marshal.SizeOf(typeof(spellshapeshiftformRecord)); // Write main body
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

            return true; } } // spellshapeshiftform

    public class stableslotpricesdbc {
        public DBCHeader header;
        public stableslotpricesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM stableslotpricesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Slot, Price FROM stableslotpricesdbc ORDER BY Slot ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new stableslotpricesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(stableslotpricesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Slot = reader.GetInt32("Slot");
                    body.records[i].record.Price = reader.GetInt32("Price");
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
                    count = Marshal.SizeOf(typeof(stableslotpricesRecord)); // Write main body
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

            return true; } } // stableslotprices

    public class summonpropertiesdbc {
        public DBCHeader header;
        public summonpropertiesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM summonpropertiesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Category, Faction, Type, Slot, Flags FROM summonpropertiesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new summonpropertiesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 6;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(summonpropertiesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Category = reader.GetInt32("Category");
                    body.records[i].record.Faction = reader.GetInt32("Faction");
                    body.records[i].record.Type = reader.GetInt32("Type");
                    body.records[i].record.Slot = reader.GetInt32("Slot");
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
                    count = Marshal.SizeOf(typeof(summonpropertiesRecord)); // Write main body
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

            return true; } } // summonproperties

    public class talentdbc {
        public DBCHeader header;
        public talentBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM talentdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, TalentTab, Row, Col, Rank1, Rank2, Rank3, Rank4, Rank5, DependsOn, DependsOnRank, needAddInSpellBook, unk0, allowForPetHigh, allowForPetLow FROM talentdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new talentMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 23;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(talentRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.TalentTab = reader.GetInt32("TalentTab");
                    body.records[i].record.Row = reader.GetInt32("Row");
                    body.records[i].record.Col = reader.GetInt32("Col");
                    body.records[i].record.Rank1 = reader.GetInt32("Rank1");
                    body.records[i].record.Rank2 = reader.GetInt32("Rank2");
                    body.records[i].record.Rank3 = reader.GetInt32("Rank3");
                    body.records[i].record.Rank4 = reader.GetInt32("Rank4");
                    body.records[i].record.Rank5 = reader.GetInt32("Rank5");
                    body.records[i].record.Rank6 = 0;
                    body.records[i].record.Rank7 = 0;
                    body.records[i].record.Rank8 = 0;
                    body.records[i].record.Rank9 = 0;
                    body.records[i].record.DependsOn1 = reader.GetInt32("DependsOn");
                    body.records[i].record.DependsOn2 = 0;
                    body.records[i].record.DependsOn3 = 0;
                    body.records[i].record.DependsOnRank1 = reader.GetInt32("DependsOnRank");
                    body.records[i].record.DependsOnRank2 = 0;
                    body.records[i].record.DependsOnRank3 = 0;
                    body.records[i].record.needAddInSpellBook = reader.GetInt32("needAddInSpellBook");
                    body.records[i].record.unk0 = reader.GetInt32("unk0");
                    body.records[i].record.allowForPetHigh = reader.GetInt32("allowForPetHigh");
                    body.records[i].record.allowForPetLow = reader.GetInt32("allowForPetLow");
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
                    count = Marshal.SizeOf(typeof(talentRecord)); // Write main body
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

            return true; } } // talent

    public class talenttabdbc {
        public DBCHeader header;
        public talenttabBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM talenttabdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, SpellIcon, Name14, ClassMask, PetTalentMask, TabPage, InternalName FROM talenttabdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new talenttabMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 24;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(talenttabRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.SpellIcon = reader.GetInt32("SpellIcon");
                    body.records[i].record.Name14 = reader.GetInt32("Name14");
                    body.records[i].record.ClassMask = reader.GetInt32("ClassMask");
                    body.records[i].record.PetTalentMask = reader.GetInt32("PetTalentMask");
                    body.records[i].record.TabPage = reader.GetInt32("TabPage");
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
                    count = Marshal.SizeOf(typeof(talenttabRecord)); // Write main body
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

            return true; } } // talenttab

    public class taxinodesdbc {
        public DBCHeader header;
        public taxinodesBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM taxinodesdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, MapId, X, Y, Z, Name, Name_loc2, MountCreatureId1, MountCreatureId2 FROM taxinodesdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new taxinodesMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 24;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(taxinodesRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.X = reader.GetFloat("X");
                    body.records[i].record.Y = reader.GetFloat("Y");
                    body.records[i].record.Z = reader.GetFloat("Z");
                    body.records[i].record.MountCreatureId1 = reader.GetInt32("MountCreatureId1");
                    body.records[i].record.MountCreatureId2 = reader.GetInt32("MountCreatureId2");

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
                    count = Marshal.SizeOf(typeof(taxinodesRecord)); // Write main body
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

            return true; } } // taxinodes

    public class taxipathdbc {
        public DBCHeader header;
        public taxipathBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM taxipathdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, `From`, `To`, Price FROM taxipathdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new taxipathMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 4;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(taxipathRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.From = reader.GetInt32("From");
                    body.records[i].record.To = reader.GetInt32("To");
                    body.records[i].record.Price = reader.GetInt32("Price");
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
                    count = Marshal.SizeOf(typeof(taxipathRecord)); // Write main body
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

            return true; } } // taxipath

    public class taxipathnodedbc {
        public DBCHeader header;
        public taxipathnodeBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM taxipathnodedbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, PathId, `Index`, MapId, X, Y, Z, ActionFlag, Delay, ArrivalEventId, DepartureEventId FROM taxipathnodedbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new taxipathnodeMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 11;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(taxipathnodeRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.PathId = reader.GetInt32("PathId");
                    body.records[i].record.Index = reader.GetInt32("Index");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.X = reader.GetFloat("X");
                    body.records[i].record.Y = reader.GetFloat("Y");
                    body.records[i].record.Z = reader.GetFloat("Z");
                    body.records[i].record.ActionFlag = reader.GetInt32("ActionFlag");
                    body.records[i].record.Delay = reader.GetInt32("Delay");
                    body.records[i].record.ArrivalEventId = reader.GetInt32("ArrivalEventId");
                    body.records[i].record.DepartureEventId = reader.GetInt32("DepartureEventId");
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
                    count = Marshal.SizeOf(typeof(taxipathnodeRecord)); // Write main body
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

            return true; } } // taxipathnode

    public class teamcontributionpointsdbc {
        public DBCHeader header;
        public teamcontributionpointsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM teamcontributionpointsdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Value FROM teamcontributionpointsdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new teamcontributionpointsMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 2;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(teamcontributionpointsRecord));

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
                    count = Marshal.SizeOf(typeof(teamcontributionpointsRecord)); // Write main body
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

            return true; } } // teamcontributionpoints

    public class totemcategorydbc {
        public DBCHeader header;
        public totemcategoryBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM totemcategorydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Name, Name_loc2, CategoryType, CategoryMask FROM totemcategorydbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new totemcategoryMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 20;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(totemcategoryRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.CategoryType = reader.GetInt32("CategoryType");
                    body.records[i].record.CategoryMask = reader.GetInt32("CategoryMask");

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
                    count = Marshal.SizeOf(typeof(totemcategoryRecord)); // Write main body
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

            return true; } } // totemcategory

    public class transportanimationdbc {
        public DBCHeader header;
        public transportanimationBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM transportanimationdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, TransportEntry, TimeSeg, X, Y, Z, MovementId FROM transportanimationdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new transportanimationMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 7;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(transportanimationRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.TransportEntry = reader.GetInt32("TransportEntry");
                    body.records[i].record.TimeSeg = reader.GetInt32("TimeSeg");
                    body.records[i].record.X = reader.GetFloat("X");
                    body.records[i].record.Y = reader.GetFloat("Y");
                    body.records[i].record.Z = reader.GetFloat("Z");
                    body.records[i].record.MovementId = reader.GetInt32("MovementId");
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
                    count = Marshal.SizeOf(typeof(transportanimationRecord)); // Write main body
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

            return true; } } // transportanimation

    public class transportrotationdbc {
        public DBCHeader header;
        public transportrotationBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM transportrotationdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, TransportEntry, TimeSeg, X, Y, Z, W FROM transportrotationdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new transportrotationMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 7;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(transportrotationRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.TransportEntry = reader.GetInt32("TransportEntry");
                    body.records[i].record.TimeSeg = reader.GetInt32("TimeSeg");
                    body.records[i].record.X = reader.GetFloat("X");
                    body.records[i].record.Y = reader.GetFloat("Y");
                    body.records[i].record.Z = reader.GetFloat("Z");
                    body.records[i].record.W = reader.GetFloat("W");
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
                    count = Marshal.SizeOf(typeof(transportrotationRecord)); // Write main body
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

            return true; } } // transportrotation

    public class vehicledbc {
        public DBCHeader header;
        public vehicleBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM vehicledbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Flags, TurnSpeed, PitchSpeed, PitchMin, PitchMax, SeatId1, SeatId2, SeatId3, SeatId4, SeatId5, SeatId6, SeatId7, SeatId8, MouseLookOffsetPitch, CameraFadeDistScalarMin, CameraFadeDistScalarMax, CameraPitchOffset, FacingLimitRight, FacingLimitLeft, MSSLTrgtTurnLingering, MSSLTrgtPitchLingering, MSSLTrgtMouseLingering, MSSLTrgtEndOpacity, MSSLTrgtArcSpeed, MSSLTrgtArcRepeat, MSSLTrgtArcWidth, MSSLTrgtImpactRadius1, MSSLTrgtImpactRadius2, MSSLTrgtArcTexture, MSSLTrgtImpactTexture, MSSLTrgtImpactModel1, MSSLTrgtImpactModel2, CameraYawOffset, UiLocomotionType, MSSLTrgtImpactTexRadius, UiSeatIndicatorType, PowerDisplayId1, PowerDisplayId2, PowerDisplayId3 FROM vehicledbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new vehicleMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 40;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(vehicleRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.TurnSpeed = reader.GetFloat("TurnSpeed");
                    body.records[i].record.PitchSpeed = reader.GetFloat("PitchSpeed");
                    body.records[i].record.PitchMin = reader.GetFloat("PitchMin");
                    body.records[i].record.PitchMax = reader.GetFloat("PitchMax");
                    body.records[i].record.SeatId1 = reader.GetInt32("SeatId1");
                    body.records[i].record.SeatId2 = reader.GetInt32("SeatId2");
                    body.records[i].record.SeatId3 = reader.GetInt32("SeatId3");
                    body.records[i].record.SeatId4 = reader.GetInt32("SeatId4");
                    body.records[i].record.SeatId5 = reader.GetInt32("SeatId5");
                    body.records[i].record.SeatId6 = reader.GetInt32("SeatId6");
                    body.records[i].record.SeatId7 = reader.GetInt32("SeatId7");
                    body.records[i].record.SeatId8 = reader.GetInt32("SeatId8");
                    body.records[i].record.MouseLookOffsetPitch = reader.GetFloat("MouseLookOffsetPitch");
                    body.records[i].record.CameraFadeDistScalarMin = reader.GetFloat("CameraFadeDistScalarMin");
                    body.records[i].record.CameraFadeDistScalarMax = reader.GetFloat("CameraFadeDistScalarMax");
                    body.records[i].record.CameraPitchOffset = reader.GetFloat("CameraPitchOffset");
                    body.records[i].record.FacingLimitRight = reader.GetFloat("FacingLimitRight");
                    body.records[i].record.FacingLimitLeft = reader.GetFloat("FacingLimitLeft");
                    body.records[i].record.MSSLTrgtTurnLingering = reader.GetFloat("MSSLTrgtTurnLingering");
                    body.records[i].record.MSSLTrgtPitchLingering = reader.GetFloat("MSSLTrgtPitchLingering");
                    body.records[i].record.MSSLTrgtMouseLingering = reader.GetFloat("MSSLTrgtMouseLingering");
                    body.records[i].record.MSSLTrgtEndOpacity = reader.GetFloat("MSSLTrgtEndOpacity");
                    body.records[i].record.MSSLTrgtArcSpeed = reader.GetInt32("MSSLTrgtArcSpeed");
                    body.records[i].record.MSSLTrgtArcRepeat = reader.GetFloat("MSSLTrgtArcRepeat");
                    body.records[i].record.MSSLTrgtArcWidth = reader.GetFloat("MSSLTrgtArcWidth");
                    body.records[i].record.MSSLTrgtImpactRadius1 = reader.GetFloat("MSSLTrgtImpactRadius1");
                    body.records[i].record.MSSLTrgtImpactRadius2 = reader.GetFloat("MSSLTrgtImpactRadius2");
                    body.records[i].MSSLTrgtArcTexture = reader.GetString("MSSLTrgtArcTexture");
                    body.records[i].MSSLTrgtImpactTexture = reader.GetString("MSSLTrgtImpactTexture");
                    body.records[i].MSSLTrgtImpactModel1 = reader.GetString("MSSLTrgtImpactModel1");
                    body.records[i].MSSLTrgtImpactModel2 = reader.GetString("MSSLTrgtImpactModel2");
                    body.records[i].record.CameraYawOffset = reader.GetFloat("CameraYawOffset");
                    body.records[i].record.UiLocomotionType = reader.GetInt32("UiLocomotionType");
                    body.records[i].record.MSSLTrgtImpactTexRadius = reader.GetFloat("MSSLTrgtImpactTexRadius");
                    body.records[i].record.UiSeatIndicatorType = reader.GetInt32("UiSeatIndicatorType");
                    body.records[i].record.PowerDisplayId1 = reader.GetInt32("PowerDisplayId1");
                    body.records[i].record.PowerDisplayId2 = reader.GetInt32("PowerDisplayId2");
                    body.records[i].record.PowerDisplayId3 = reader.GetInt32("PowerDisplayId3");
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
                    // MSSLTrgtArcTexture
                    if (body.records[i].MSSLTrgtArcTexture.Length == 0)
                        body.records[i].record.MSSLTrgtArcTexture = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].MSSLTrgtArcTexture)) body.records[i].record.MSSLTrgtArcTexture = offsetStorage[body.records[i].MSSLTrgtArcTexture];
                        else {
                            body.records[i].record.MSSLTrgtArcTexture = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].MSSLTrgtArcTexture) + 1;
                            offsetStorage.Add(body.records[i].MSSLTrgtArcTexture, body.records[i].record.MSSLTrgtArcTexture);
                            reverseStorage.Add(body.records[i].record.MSSLTrgtArcTexture, body.records[i].MSSLTrgtArcTexture); } }
                    // MSSLTrgtImpactTexture
                    if (body.records[i].MSSLTrgtImpactTexture.Length == 0)
                        body.records[i].record.MSSLTrgtImpactTexture = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].MSSLTrgtImpactTexture)) body.records[i].record.MSSLTrgtImpactTexture = offsetStorage[body.records[i].MSSLTrgtImpactTexture];
                        else {
                            body.records[i].record.MSSLTrgtImpactTexture = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].MSSLTrgtImpactTexture) + 1;
                            offsetStorage.Add(body.records[i].MSSLTrgtImpactTexture, body.records[i].record.MSSLTrgtImpactTexture);
                            reverseStorage.Add(body.records[i].record.MSSLTrgtImpactTexture, body.records[i].MSSLTrgtImpactTexture); } }
                    // MSSLTrgtImpactModel1
                    if (body.records[i].MSSLTrgtImpactModel1.Length == 0)
                        body.records[i].record.MSSLTrgtImpactModel1 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].MSSLTrgtImpactModel1)) body.records[i].record.MSSLTrgtImpactModel1 = offsetStorage[body.records[i].MSSLTrgtImpactModel1];
                        else {
                            body.records[i].record.MSSLTrgtImpactModel1 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].MSSLTrgtImpactModel1) + 1;
                            offsetStorage.Add(body.records[i].MSSLTrgtImpactModel1, body.records[i].record.MSSLTrgtImpactModel1);
                            reverseStorage.Add(body.records[i].record.MSSLTrgtImpactModel1, body.records[i].MSSLTrgtImpactModel1); } }
                    // MSSLTrgtImpactModel2
                    if (body.records[i].MSSLTrgtImpactModel2.Length == 0)
                        body.records[i].record.MSSLTrgtImpactModel2 = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].MSSLTrgtImpactModel2)) body.records[i].record.MSSLTrgtImpactModel2 = offsetStorage[body.records[i].MSSLTrgtImpactModel2];
                        else {
                            body.records[i].record.MSSLTrgtImpactModel2 = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].MSSLTrgtImpactModel2) + 1;
                            offsetStorage.Add(body.records[i].MSSLTrgtImpactModel2, body.records[i].record.MSSLTrgtImpactModel2);
                            reverseStorage.Add(body.records[i].record.MSSLTrgtImpactModel2, body.records[i].MSSLTrgtImpactModel2); } } }

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
                    count = Marshal.SizeOf(typeof(vehicleRecord)); // Write main body
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

            return true; } } // vehicle

    public class vehicleseatdbc {
        public DBCHeader header;
        public vehicleseatBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM vehicleseatdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Flags, AttachmentOffsetId, AttachmentOffsetX, AttachmentOffsetY, AttachmentOffsetZ, PreDelay, EnterSpeed, EnterGravity, EnterMinDuration, EnterMaxDuration, EnterMinArcHeight, EnterMaxArcHeight, EnterAnimStart, EnterAnimLoop, RideAnimStart, RideAnimLoop, RideUpperAnimStart, RideupperAnimLoop, ExitPreDelay, ExitSpeed, ExitGravity, ExitMinDuration, ExitMaxDuration, ExitMinArcHeight, ExitMaxArcHeight, ExitAnimStart, ExitAnimLoop, ExitAnimEnd, PassengerYaw, PassengerPitch, PassengerRoll, PassengerAttachmentId, VehicleEnterAnim, VehicleExitAnim, VehicleRideAnimLoop, VehicleRideAnimBone, VehicleExitAnimBone, VehicleRideAnimLoopBone, VehicleEnterAnimDelay, VehicleExitAnimDelay, VehicleAbilityDisplay, EnterUISoundId, ExitUISoundId, UiSkin, FlagsB, CameraEnteringDelay, CameraEnteringDuration, CameraExitingDelay, CameraExitingDuration, CameraOffset, CameraPosChaseRate, CameraFacingChaseRate, CameraEnteringZoom, CameraSeatZoomMin, CameraSeatZoomMax, EnterAnimKitId, RideAnimKitId FROM vehicleseatdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new vehicleseatMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 58;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(vehicleseatRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Flags = reader.GetFloat("Flags");
                    body.records[i].record.AttachmentOffsetId = reader.GetInt32("AttachmentOffsetId");
                    body.records[i].record.AttachmentOffsetX = reader.GetFloat("AttachmentOffsetX");
                    body.records[i].record.AttachmentOffsetY = reader.GetFloat("AttachmentOffsetY");
                    body.records[i].record.AttachmentOffsetZ = reader.GetFloat("AttachmentOffsetZ");
                    body.records[i].record.PreDelay = reader.GetFloat("PreDelay");
                    body.records[i].record.EnterSpeed = reader.GetFloat("EnterSpeed");
                    body.records[i].record.EnterGravity = reader.GetFloat("EnterGravity");
                    body.records[i].record.EnterMinDuration = reader.GetFloat("EnterMinDuration");
                    body.records[i].record.EnterMaxDuration = reader.GetFloat("EnterMaxDuration");
                    body.records[i].record.EnterMinArcHeight = reader.GetFloat("EnterMinArcHeight");
                    body.records[i].record.EnterMaxArcHeight = reader.GetFloat("EnterMaxArcHeight");
                    body.records[i].record.EnterAnimStart = reader.GetInt32("EnterAnimStart");
                    body.records[i].record.EnterAnimLoop = reader.GetInt32("EnterAnimLoop");
                    body.records[i].record.RideAnimStart = reader.GetInt32("RideAnimStart");
                    body.records[i].record.RideAnimLoop = reader.GetInt32("RideAnimLoop");
                    body.records[i].record.RideUpperAnimStart = reader.GetInt32("RideUpperAnimStart");
                    body.records[i].record.RideupperAnimLoop = reader.GetInt32("RideupperAnimLoop");
                    body.records[i].record.ExitPreDelay = reader.GetFloat("ExitPreDelay");
                    body.records[i].record.ExitSpeed = reader.GetInt32("ExitSpeed");
                    body.records[i].record.ExitGravity = reader.GetFloat("ExitGravity");
                    body.records[i].record.ExitMinDuration = reader.GetFloat("ExitMinDuration");
                    body.records[i].record.ExitMaxDuration = reader.GetFloat("ExitMaxDuration");
                    body.records[i].record.ExitMinArcHeight = reader.GetFloat("ExitMinArcHeight");
                    body.records[i].record.ExitMaxArcHeight = reader.GetInt32("ExitMaxArcHeight");
                    body.records[i].record.ExitAnimStart = reader.GetInt32("ExitAnimStart");
                    body.records[i].record.ExitAnimLoop = reader.GetInt32("ExitAnimLoop");
                    body.records[i].record.ExitAnimEnd = reader.GetInt32("ExitAnimEnd");
                    body.records[i].record.PassengerYaw = reader.GetFloat("PassengerYaw");
                    body.records[i].record.PassengerPitch = reader.GetFloat("PassengerPitch");
                    body.records[i].record.PassengerRoll = reader.GetFloat("PassengerRoll");
                    body.records[i].record.PassengerAttachmentId = reader.GetInt32("PassengerAttachmentId");
                    body.records[i].record.VehicleEnterAnim = reader.GetInt32("VehicleEnterAnim");
                    body.records[i].record.VehicleExitAnim = reader.GetInt32("VehicleExitAnim");
                    body.records[i].record.VehicleRideAnimLoop = reader.GetInt32("VehicleRideAnimLoop");
                    body.records[i].record.VehicleRideAnimBone = reader.GetInt32("VehicleRideAnimBone");
                    body.records[i].record.VehicleExitAnimBone = reader.GetInt32("VehicleExitAnimBone");
                    body.records[i].record.VehicleRideAnimLoopBone = reader.GetInt32("VehicleRideAnimLoopBone");
                    body.records[i].record.VehicleEnterAnimDelay = reader.GetFloat("VehicleEnterAnimDelay");
                    body.records[i].record.VehicleExitAnimDelay = reader.GetFloat("VehicleExitAnimDelay");
                    body.records[i].record.VehicleAbilityDisplay = reader.GetInt32("VehicleAbilityDisplay");
                    body.records[i].record.EnterUISoundId = reader.GetInt32("EnterUISoundId");
                    body.records[i].record.ExitUISoundId = reader.GetFloat("ExitUISoundId");
                    body.records[i].record.UiSkin = reader.GetInt32("UiSkin");
                    body.records[i].record.FlagsB = reader.GetInt32("FlagsB");
                    body.records[i].record.CameraEnteringDelay = reader.GetFloat("CameraEnteringDelay");
                    body.records[i].record.CameraEnteringDuration = reader.GetFloat("CameraEnteringDuration");
                    body.records[i].record.CameraExitingDelay = reader.GetInt32("CameraExitingDelay");
                    body.records[i].record.CameraExitingDuration = reader.GetFloat("CameraExitingDuration");
                    body.records[i].record.CameraOffset = reader.GetInt32("CameraOffset");
                    body.records[i].record.CameraPosChaseRate = reader.GetInt32("CameraPosChaseRate");
                    body.records[i].record.CameraFacingChaseRate = reader.GetFloat("CameraFacingChaseRate");
                    body.records[i].record.CameraEnteringZoom = reader.GetFloat("CameraEnteringZoom");
                    body.records[i].record.CameraSeatZoomMin = reader.GetInt32("CameraSeatZoomMin");
                    body.records[i].record.CameraSeatZoomMax = reader.GetInt32("CameraSeatZoomMax");
                    body.records[i].record.EnterAnimKitId = reader.GetInt32("EnterAnimKitId");
                    body.records[i].record.RideAnimKitId = reader.GetInt32("RideAnimKitId");
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
                    count = Marshal.SizeOf(typeof(vehicleseatRecord)); // Write main body
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

            return true; } } // vehicleseat

    public class wmoareatabledbc {
        public DBCHeader header;
        public wmoareatableBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM wmoareatabledbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, RootId, AdtId, GroupId, SoundProviderPref, SoundProviderPrefUnderwater, AmbienceId, ZoneMusic, IntroSound, Flags, AreaId, Name, Name_loc2 FROM wmoareatabledbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new wmoareatableMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 28;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(wmoareatableRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.RootId = reader.GetInt32("RootId");
                    body.records[i].record.AdtId = reader.GetInt32("AdtId");
                    body.records[i].record.GroupId = reader.GetInt32("GroupId");
                    body.records[i].record.SoundProviderPref = reader.GetInt32("SoundProviderPref");
                    body.records[i].record.SoundProviderPrefUnderwater = reader.GetInt32("SoundProviderPrefUnderwater");
                    body.records[i].record.AmbienceId = reader.GetInt32("AmbienceId");
                    body.records[i].record.ZoneMusic = reader.GetInt32("ZoneMusic");
                    body.records[i].record.IntroSound = reader.GetInt32("IntroSound");
                    body.records[i].record.Flags = reader.GetInt32("Flags");
                    body.records[i].record.AreaId = reader.GetInt32("AreaId");

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
                    count = Marshal.SizeOf(typeof(wmoareatableRecord)); // Write main body
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

            return true; } } // wmoareatable

    public class worldmapareadbc {
        public DBCHeader header;
        public worldmapareaBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM worldmapareadbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, Map, Area, InternalName, LocLeft, LocRight, LocTop, LocBottom, DisplayMap, DungeonMap, ParentMap FROM worldmapareadbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
 
                body.records = new worldmapareaMap[rowCount]; // Prepare body
 
                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 11;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(worldmapareaRecord));
 
                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false;
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.Map = reader.GetInt32("Map");
                    body.records[i].record.Area = reader.GetInt32("Area");
                    body.records[i].InternalName = reader.GetString("InternalName");
                    body.records[i].record.LocLeft = reader.GetFloat("LocLeft");
                    body.records[i].record.LocRight = reader.GetFloat("LocRight");
                    body.records[i].record.LocTop = reader.GetFloat("LocTop");
                    body.records[i].record.LocBottom = reader.GetFloat("LocBottom");
                    body.records[i].record.DisplayMap = reader.GetInt32("DisplayMap");
                    body.records[i].record.DefaultDungeonFloor = reader.GetInt32("DungeonMap");
                    body.records[i].record.ParentWorldMap = reader.GetInt32("ParentMap");
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
                    // AreaName
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
                    count = Marshal.SizeOf(typeof(worldmapareaRecord)); // Write main body
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

            return true; } } // worldmaparea

    public class worldmapoverlaydbc {
        public DBCHeader header;
        public worldmapoverlayBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM worldmapoverlaydbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, WorldMapAreaId, AreaTableId1, AreaTableId2, AreaTableId3, AreaTableId4, MapPointX, MapPointY, TextureName, TextureWidth, TextureHeight, OffsetX, OffsetY, HitRectTop, HitRectLeft, HitRectBottom, HitRectRight FROM worldmapoverlaydbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new worldmapoverlayMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 17;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(worldmapoverlayRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.WorldMapAreaId = reader.GetInt32("WorldMapAreaId");
                    body.records[i].record.AreaTableId1 = reader.GetInt32("AreaTableId1");
                    body.records[i].record.AreaTableId2 = reader.GetInt32("AreaTableId2");
                    body.records[i].record.AreaTableId3 = reader.GetInt32("AreaTableId3");
                    body.records[i].record.AreaTableId4 = reader.GetInt32("AreaTableId4");
                    body.records[i].record.MapPointX = reader.GetInt32("MapPointX");
                    body.records[i].record.MapPointY = reader.GetInt32("MapPointY");
                    body.records[i].TextureName = reader.GetString("TextureName");
                    body.records[i].record.TextureWidth = reader.GetInt32("TextureWidth");
                    body.records[i].record.TextureHeight = reader.GetInt32("TextureHeight");
                    body.records[i].record.OffsetX = reader.GetInt32("OffsetX");
                    body.records[i].record.OffsetY = reader.GetInt32("OffsetY");
                    body.records[i].record.HitRectTop = reader.GetInt32("HitRectTop");
                    body.records[i].record.HitRectLeft = reader.GetInt32("HitRectLeft");
                    body.records[i].record.HitRectBottom = reader.GetInt32("HitRectBottom");
                    body.records[i].record.HitRectRight = reader.GetInt32("HitRectRight");
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
                    // TextureName
                    if (body.records[i].TextureName.Length == 0)
                        body.records[i].record.TextureName = 0;
                    else {
                        if (offsetStorage.ContainsKey(body.records[i].TextureName)) body.records[i].record.TextureName = offsetStorage[body.records[i].TextureName];
                        else {
                            body.records[i].record.TextureName = stringBlockOffset;
                            stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].TextureName) + 1;
                            offsetStorage.Add(body.records[i].TextureName, body.records[i].record.TextureName);
                            reverseStorage.Add(body.records[i].record.TextureName, body.records[i].TextureName); } } }

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
                    count = Marshal.SizeOf(typeof(worldmapoverlayRecord)); // Write main body
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

            return true; } } // worldmapoverlay

    public class worldsafelocsdbc {
        public DBCHeader header;
        public worldsafelocsBody body;

        public bool LoadDB(MySqlConnection connection) {
            try {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM worldsafelocsdbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT Id, MapId, X, Y, Z, Name, Name_loc2 FROM worldsafelocsdbc ORDER BY Id ASC";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                body.records = new worldsafelocsMap[rowCount]; // Prepare body

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 22;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(worldsafelocsRecord));

                UInt32 i = 0;
                while (reader.Read()) { //if (!reader.HasRows) return false; 
                    body.records[i].record.Id = reader.GetInt32("Id");
                    body.records[i].record.MapId = reader.GetInt32("MapId");
                    body.records[i].record.X = reader.GetFloat("X");
                    body.records[i].record.Y = reader.GetFloat("Y");
                    body.records[i].record.Z = reader.GetFloat("Z");

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
                    count = Marshal.SizeOf(typeof(worldsafelocsRecord)); // Write main body
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

            return true; } } // worldsafelocs
}

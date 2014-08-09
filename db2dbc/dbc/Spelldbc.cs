using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.InteropServices;

using MySql.Data.MySqlClient;

namespace DBtoDBC
{
    public class Spelldbc
    {
        public DBCHeader header;
        public SpellBody body;


        public bool LoadDB(MySqlConnection connection)
        {
            try
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM spelldbc", connection);
                UInt32 rowCount = Convert.ToUInt32(cmd.ExecuteScalar());

                string query = "SELECT "
        /* 0 */    +"Entry, Category, Dispel, Mechanic, Attributes, AttributesEx, AttributesEx2, AttributesEx3, AttributesEx4, AttributesEx5, AttributesEx6, AttributesEx7, "
        /* 12 */   +"Stances, StancesNot, Targets, TargetCreatureType, RequiresSpellFocus, FacingCasterFlags, CasterAuraState, TargetAuraState, CasterAuraStateNot, TargetAuraStateNot, casterAuraSpell, targetAuraSpell, "
        /* 24 */   +"excludeCasterAuraSpell, excludeTargetAuraSpell, CastingTimeIndex, RecoveryTime, CategoryRecoveryTime, InterruptFlags, AuraInterruptFlags, ChannelInterruptFlags, procFlags, procChance, procCharges, "
        /* 35 */   +"maxLevel, baseLevel, spellLevel, DurationIndex, powerType, manaCost, manaCostPerlevel, manaPerSecond, manaPerSecondPerLevel, rangeIndex, speed, StackAmount, Totem1, Totem2, "
        /* 49 */   +"Reagent1, Reagent2, Reagent3, Reagent4, Reagent5, Reagent6, Reagent7, Reagent8, ReagentCount1, ReagentCount2, ReagentCount3, ReagentCount4, ReagentCount5, ReagentCount6, ReagentCount7, ReagentCount8, "
        /* 65 */   +"EquippedItemClass, EquippedItemSubClassMask, EquippedItemInventoryTypeMask, "
        /* 68 */   +"Effect1, Effect2, Effect3, EffectDieSides1, EffectDieSides2, EffectDieSides3, EffectRealPointsPerLevel1, EffectRealPointsPerLevel2, EffectRealPointsPerLevel3, EffectBasePoints1, EffectBasePoints2, EffectBasePoints3, "
        /* 80 */   +"EffectMechanic1, EffectMechanic2, EffectMechanic3, EffectImplicitTargetA1, EffectImplicitTargetA2, EffectImplicitTargetA3, EffectImplicitTargetB1, EffectImplicitTargetB2, EffectImplicitTargetB3, "
        /* 89 */   +"EffectRadiusIndex1, EffectRadiusIndex2, EffectRadiusIndex3, EffectApplyAuraName1, EffectApplyAuraName2, EffectApplyAuraName3, EffectAmplitude1, EffectAmplitude2, EffectAmplitude3, "
        /* 98 */   +"EffectValueMultiplier1, EffectValueMultiplier2, EffectValueMultiplier3, EffectChainTarget1, EffectChainTarget2, EffectChainTarget3, EffectItemType1, EffectItemType2, EffectItemType3, "
        /* 107 */  +"EffectMiscValue1, EffectMiscValue2, EffectMiscValue3, EffectMiscValueB1, EffectMiscValueB2, EffectMiscValueB3, EffectTriggerSpell1, EffectTriggerSpell2, EffectTriggerSpell3, "
        /* 116 */  +"EffectPointsPerComboPoint1, EffectPointsPerComboPoint2, EffectPointsPerComboPoint3, EffectSpellClassMaskA1, EffectSpellClassMaskA2, EffectSpellClassMaskA3, "
        /* 122 */  +"EffectSpellClassMaskB1, EffectSpellClassMaskB2, EffectSpellClassMaskB3, EffectSpellClassMaskC1, EffectSpellClassMaskC2, EffectSpellClassMaskC3, "
        /* 128 */  +"SpellVisual1, SpellVisual2, SpellIconID, activeIconID, spellPriority, ManaCostPercentage, StartRecoveryCategory, StartRecoveryTime, MaxTargetLevel, "
        /* 137 */  +"SpellFamilyName, SpellFamilyFlagsLow, SpellFamilyFlagsHigh, SpellFamilyFlags2, MaxAffectedTargets, DmgClass, PreventionType, StanceBarOrder, "
        /* 145 */  +"EffectDamageMultiplier1, EffectDamageMultiplier2, EffectDamageMultiplier3, MinFactionId, MinReputation, RequiredAuraVision, TotemCategory1, TotemCategory2, AreaGroupId, "
        /* 154 */  +"SchoolMask, runeCostID, spellMissileID, PowerDisplayId, EffectBonusMultiplier1, EffectBonusMultiplier2, EffectBonusMultiplier3, spellDescriptionVariableID, SpellDifficultyId, "
        /* 163 */  +"Name_loc2, Rank_loc2, Description_loc2, Tooltip_loc2, "
        /* 167 */  +"SpellNameFlags, RankFlags, DescriptionFlags, Tooltipflags FROM spelldbc ORDER BY Entry";


                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                // Prepare body
                body.records = new SpellRecordMap[rowCount];

                header.magic = 1128416343;
                header.record_count = rowCount;
                header.field_count = 234;
                header.record_size = (UInt32)Marshal.SizeOf(typeof(SpellRecord));
                // header.string_block_size;

                UInt32 i = 0;
                //if (!reader.HasRows) return false;
                while (reader.Read())
                {
                    body.records[i].record.Id                   = reader.GetUInt32(0);
                    body.records[i].record.Category             = reader.GetUInt32(1);
                    body.records[i].record.Dispel               = reader.GetUInt32(2);
                    body.records[i].record.Mechanic             = reader.GetUInt32(3);
                    body.records[i].record.Attributes           = reader.GetUInt32(4);
                    body.records[i].record.AttributesEx         = reader.GetUInt32(5);
                    body.records[i].record.AttributesEx2        = reader.GetUInt32(6);
                    body.records[i].record.AttributesEx3        = reader.GetUInt32(7);
                    body.records[i].record.AttributesEx4        = reader.GetUInt32(8);
                    body.records[i].record.AttributesEx5        = reader.GetUInt32(9);
                    body.records[i].record.AttributesEx6        = reader.GetUInt32(10);
                    body.records[i].record.AttributesEx7        = reader.GetUInt32(11);
                    body.records[i].record.Stances              = reader.GetUInt32(12);
                    body.records[i].record.unk_320_2            = 0;
                    body.records[i].record.StancesNot           = reader.GetUInt32(13);
                    body.records[i].record.unk_320_3            = 0;
                    body.records[i].record.Targets              = reader.GetUInt32(14);
                    body.records[i].record.TargetCreatureType   = reader.GetUInt32(15);
                    body.records[i].record.RequiresSpellFocus   = reader.GetUInt32(16);
                    body.records[i].record.FacingCasterFlags    = reader.GetUInt32(17);
                    body.records[i].record.CasterAuraState      = reader.GetUInt32(18);
                    body.records[i].record.TargetAuraState      = reader.GetUInt32(19);
                    body.records[i].record.CasterAuraStateNot   = reader.GetUInt32(20);
                    body.records[i].record.TargetAuraStateNot   = reader.GetUInt32(21);
                    body.records[i].record.casterAuraSpell      = reader.GetUInt32(22);
                    body.records[i].record.targetAuraSpell      = reader.GetUInt32(23);
                    body.records[i].record.excludeCasterAuraSpell = reader.GetUInt32(24);
                    body.records[i].record.excludeTargetAuraSpell = reader.GetUInt32(25);
                    body.records[i].record.CastingTimeIndex     = reader.GetUInt32(26);
                    body.records[i].record.RecoveryTime         = reader.GetUInt32(27);
                    body.records[i].record.CategoryRecoveryTime = reader.GetUInt32(28);
                    body.records[i].record.InterruptFlags       = reader.GetUInt32(29);
                    body.records[i].record.AuraInterruptFlags   = reader.GetUInt32(30);
                    body.records[i].record.ChannelInterruptFlags = reader.GetUInt32(31);
                    body.records[i].record.procFlags            = reader.GetUInt32(32);
                    body.records[i].record.procChance           = reader.GetUInt32(33);
                    body.records[i].record.procCharges          = reader.GetUInt32(34);
                    body.records[i].record.maxLevel             = reader.GetUInt32(35);
                    body.records[i].record.baseLevel            = reader.GetUInt32(36);
                    body.records[i].record.spellLevel           = reader.GetUInt32(37);
                    body.records[i].record.DurationIndex        = reader.GetUInt32(38);
                    body.records[i].record.powerType            = reader.GetUInt32(39);
                    body.records[i].record.manaCost             = reader.GetUInt32(40);
                    body.records[i].record.manaCostPerlevel     = reader.GetUInt32(41);
                    body.records[i].record.manaPerSecond        = reader.GetUInt32(42);
                    body.records[i].record.manaPerSecondPerLevel = reader.GetUInt32(43);
                    body.records[i].record.rangeIndex           = reader.GetUInt32(44);
                    body.records[i].record.speed                = reader.GetUInt32(45);
                    body.records[i].record.modalNextSpell       = 0;
                    body.records[i].record.StackAmount          = reader.GetUInt32(46);
                    body.records[i].record.Totem1               = reader.GetUInt32(47);
                    body.records[i].record.Totem2               = reader.GetUInt32(48);
                    body.records[i].record.Reagent1             = reader.GetInt32(49);
                    body.records[i].record.Reagent2             = reader.GetInt32(50);
                    body.records[i].record.Reagent3             = reader.GetInt32(51);
                    body.records[i].record.Reagent4             = reader.GetInt32(52);
                    body.records[i].record.Reagent5             = reader.GetInt32(53);
                    body.records[i].record.Reagent6             = reader.GetInt32(54);
                    body.records[i].record.Reagent7             = reader.GetInt32(55);
                    body.records[i].record.Reagent8             = reader.GetInt32(56);
                    body.records[i].record.ReagentCount1        = reader.GetUInt32(57);
                    body.records[i].record.ReagentCount2        = reader.GetUInt32(58);
                    body.records[i].record.ReagentCount3        = reader.GetUInt32(59);
                    body.records[i].record.ReagentCount4        = reader.GetUInt32(60);
                    body.records[i].record.ReagentCount5        = reader.GetUInt32(61);
                    body.records[i].record.ReagentCount6        = reader.GetUInt32(62);
                    body.records[i].record.ReagentCount7        = reader.GetUInt32(63);
                    body.records[i].record.ReagentCount8        = reader.GetUInt32(64);
                    body.records[i].record.EquippedItemClass    = reader.GetInt32(65);
                    body.records[i].record.EquippedItemSubClassMask = reader.GetInt32(66);
                    body.records[i].record.EquippedItemInventoryTypeMask = reader.GetInt32(67);
                    body.records[i].record.Effect1              = reader.GetUInt32(68);
                    body.records[i].record.Effect2              = reader.GetUInt32(69);
                    body.records[i].record.Effect3              = reader.GetUInt32(70);
                    body.records[i].record.EffectDieSides1      = reader.GetInt32(71);
                    body.records[i].record.EffectDieSides2      = reader.GetInt32(72);
                    body.records[i].record.EffectDieSides3      = reader.GetInt32(73);
                    body.records[i].record.EffectRealPointsPerLevel1 = reader.GetFloat(74);
                    body.records[i].record.EffectRealPointsPerLevel2 = reader.GetFloat(75);
                    body.records[i].record.EffectRealPointsPerLevel3 = reader.GetFloat(76);
                    body.records[i].record.EffectBasePoints1    = reader.GetInt32(77);
                    body.records[i].record.EffectBasePoints2    = reader.GetInt32(78);
                    body.records[i].record.EffectBasePoints3    = reader.GetInt32(79);
                    body.records[i].record.EffectMechanic1      = reader.GetUInt32(80);
                    body.records[i].record.EffectMechanic2      = reader.GetUInt32(81);
                    body.records[i].record.EffectMechanic3      = reader.GetUInt32(82);
                    body.records[i].record.EffectImplicitTargetA1 = reader.GetUInt32(83);
                    body.records[i].record.EffectImplicitTargetA2 = reader.GetUInt32(84);
                    body.records[i].record.EffectImplicitTargetA3 = reader.GetUInt32(85);
                    body.records[i].record.EffectImplicitTargetB1 = reader.GetUInt32(86);
                    body.records[i].record.EffectImplicitTargetB2 = reader.GetUInt32(87);
                    body.records[i].record.EffectImplicitTargetB3 = reader.GetUInt32(88);
                    body.records[i].record.EffectRadiusIndex1   = reader.GetUInt32(89);
                    body.records[i].record.EffectRadiusIndex2   = reader.GetUInt32(90);
                    body.records[i].record.EffectRadiusIndex3   = reader.GetUInt32(91);
                    body.records[i].record.EffectApplyAuraName1 = reader.GetUInt32(92);
                    body.records[i].record.EffectApplyAuraName2 = reader.GetUInt32(93);
                    body.records[i].record.EffectApplyAuraName3 = reader.GetUInt32(94);
                    body.records[i].record.EffectAmplitude1     = reader.GetUInt32(95);
                    body.records[i].record.EffectAmplitude2     = reader.GetUInt32(96);
                    body.records[i].record.EffectAmplitude3     = reader.GetUInt32(97);
                    body.records[i].record.EffectValueMultiplier1 = reader.GetFloat(98);
                    body.records[i].record.EffectValueMultiplier2 = reader.GetFloat(99);
                    body.records[i].record.EffectValueMultiplier3 = reader.GetFloat(100);
                    body.records[i].record.EffectChainTarget1   = reader.GetUInt32(101);
                    body.records[i].record.EffectChainTarget2   = reader.GetUInt32(102);
                    body.records[i].record.EffectChainTarget3   = reader.GetUInt32(103);
                    body.records[i].record.EffectItemType1      = reader.GetUInt32(104);
                    body.records[i].record.EffectItemType2      = reader.GetUInt32(105);
                    body.records[i].record.EffectItemType3      = reader.GetUInt32(106);
                    body.records[i].record.EffectMiscValue1     = reader.GetInt32(107);
                    body.records[i].record.EffectMiscValue2     = reader.GetInt32(108);
                    body.records[i].record.EffectMiscValue3     = reader.GetInt32(109);
                    body.records[i].record.EffectMiscValueB1    = reader.GetInt32(110);
                    body.records[i].record.EffectMiscValueB2    = reader.GetInt32(111);
                    body.records[i].record.EffectMiscValueB3    = reader.GetInt32(112);
                    body.records[i].record.EffectTriggerSpell1  = reader.GetUInt32(113);
                    body.records[i].record.EffectTriggerSpell2  = reader.GetUInt32(114);
                    body.records[i].record.EffectTriggerSpell3  = reader.GetUInt32(115);
                    body.records[i].record.EffectPointsPerComboPoint1 = reader.GetFloat(116);
                    body.records[i].record.EffectPointsPerComboPoint2 = reader.GetFloat(117);
                    body.records[i].record.EffectPointsPerComboPoint3 = reader.GetFloat(118);
                    body.records[i].record.EffectSpellClassMaskA1 = reader.GetUInt32(119);
                    body.records[i].record.EffectSpellClassMaskA2 = reader.GetUInt32(120);
                    body.records[i].record.EffectSpellClassMaskA3 = reader.GetUInt32(121);
                    body.records[i].record.EffectSpellClassMaskB1 = reader.GetUInt32(122);
                    body.records[i].record.EffectSpellClassMaskB2 = reader.GetUInt32(123);
                    body.records[i].record.EffectSpellClassMaskB3 = reader.GetUInt32(124);
                    body.records[i].record.EffectSpellClassMaskC1 = reader.GetUInt32(125);
                    body.records[i].record.EffectSpellClassMaskC2 = reader.GetUInt32(126);
                    body.records[i].record.EffectSpellClassMaskC3 = reader.GetUInt32(127);
                    body.records[i].record.SpellVisual1         = reader.GetUInt32(128);
                    body.records[i].record.SpellVisual2         = reader.GetUInt32(129);
                    body.records[i].record.SpellIconID          = reader.GetUInt32(130);
                    body.records[i].record.activeIconID         = reader.GetUInt32(131);
                    body.records[i].record.spellPriority        = reader.GetUInt32(132);
                    // SpellName] SpellNameFlag Rank RankFlags Description DescriptionFlags ToolTip ToolTipFlags
                    body.records[i].record.ManaCostPercentage = reader.GetUInt32(133);
                    body.records[i].record.StartRecoveryCategory = reader.GetUInt32(134);
                    body.records[i].record.StartRecoveryTime    = reader.GetUInt32(135);
                    body.records[i].record.MaxTargetLevel       = reader.GetUInt32(136);
                    body.records[i].record.SpellFamilyName      = reader.GetUInt32(137);
                    body.records[i].record.SpellFamilyFlagsHigh = reader.GetUInt32(138);
                    body.records[i].record.SpellFamilyFlagsLow  = reader.GetUInt32(140);
                    body.records[i].record.SpellFamilyFlags2    = reader.GetUInt32(140);
                    body.records[i].record.MaxAffectedTargets   = reader.GetUInt32(141);
                    body.records[i].record.DmgClass             = reader.GetUInt32(142);
                    body.records[i].record.PreventionType       = reader.GetUInt32(143);
                    body.records[i].record.StanceBarOrder       = reader.GetUInt32(144);
                    body.records[i].record.EffectDamageMultiplier1 = reader.GetFloat(145);
                    body.records[i].record.EffectDamageMultiplier2 = reader.GetFloat(146);
                    body.records[i].record.EffectDamageMultiplier3 = reader.GetFloat(147);
                    body.records[i].record.MinFactionId         = reader.GetUInt32(148);
                    body.records[i].record.MinReputation        = reader.GetUInt32(149);
                    body.records[i].record.RequiredAuraVision   = reader.GetUInt32(150);
                    body.records[i].record.TotemCategory1       = reader.GetUInt32(151);
                    body.records[i].record.TotemCategory2       = reader.GetUInt32(152);
                    body.records[i].record.AreaGroupId          = reader.GetInt32(153);
                    body.records[i].record.SchoolMask           = reader.GetUInt32(154);
                    body.records[i].record.runeCostID           = reader.GetUInt32(155);
                    body.records[i].record.spellMissileID       = reader.GetUInt32(156);
                    body.records[i].record.PowerDisplayId       = reader.GetUInt32(157);
                    body.records[i].record.EffectBonusMultiplier1 = reader.GetFloat(158);
                    body.records[i].record.EffectBonusMultiplier2 = reader.GetFloat(159);
                    body.records[i].record.EffectBonusMultiplier3 = reader.GetFloat(160);
                    body.records[i].record.spellDescriptionVariableID = reader.GetUInt32(161);
                    body.records[i].record.SpellDifficultyId    = reader.GetUInt32(162);

                    body.records[i].spellName = new string[9];
                    body.records[i].spellRank = new string[9];
                    body.records[i].spellDesc = new string[9];
                    body.records[i].spellTool = new string[9];
                    body.records[i].record.SpellName    = new UInt32[9];
                    body.records[i].record.Rank         = new UInt32[9];
                    body.records[i].record.Description  = new UInt32[9];
                    body.records[i].record.ToolTip      = new UInt32[9];
                    for (int loc = 0; loc < 9; ++loc) {
                        body.records[i].spellName[loc] = "";
                        body.records[i].spellRank[loc] = "";
                        body.records[i].spellDesc[loc] = "";
                        body.records[i].spellTool[loc] = ""; }
                    body.records[i].spellName[2] = reader.GetString(163);
                    body.records[i].spellRank[2] = reader.GetString(164);
                    body.records[i].spellDesc[2] = reader.GetString(165);
                    body.records[i].spellTool[2] = reader.GetString(166);

                    body.records[i].record.SpellNameFlags   = new UInt32[8];
                    body.records[i].record.RankFlags        = new UInt32[8];
                    body.records[i].record.DescriptionFlags = new UInt32[8];
                    body.records[i].record.ToolTipFlags     = new UInt32[8];
                    for (Int32 flag = 0; flag < 7; flag++) {
                        body.records[i].record.SpellNameFlags[flag]     = 0;
                        body.records[i].record.RankFlags[flag]          = 0;
                        body.records[i].record.DescriptionFlags[flag]   = 0;
                        body.records[i].record.ToolTipFlags[flag]       = 0; }
                    body.records[i].record.SpellNameFlags[7]    = reader.GetUInt32(167);
                    body.records[i].record.RankFlags[7]         = reader.GetUInt32(168);
                    body.records[i].record.DescriptionFlags[7]  = reader.GetUInt32(169);
                    body.records[i].record.ToolTipFlags[7]      = reader.GetUInt32(170);

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
                    for (UInt32 j = 0; j < 9; ++j)
                    {
                        // Name
                        if (body.records[i].spellName[j].Length == 0)
                            body.records[i].record.SpellName[j] = 0;
                        else
                        {
                            int key = body.records[i].spellName[j].GetHashCode();
                            if (offsetStorage.ContainsKey(key))
                                body.records[i].record.SpellName[j] = offsetStorage[key];
                            else
                            {
                                body.records[i].record.SpellName[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].spellName[j]) + 1;
                                offsetStorage.Add(key, body.records[i].record.SpellName[j]);
                                reverseStorage.Add(body.records[i].record.SpellName[j], body.records[i].spellName[j]);
                            }
                        }
                        // Rank
                        if (body.records[i].spellRank[j].Length == 0)
                            body.records[i].record.Rank[j] = 0;
                        else
                        {
                            int key = body.records[i].spellRank[j].GetHashCode();
                            if (offsetStorage.ContainsKey(key))
                                body.records[i].record.Rank[j] = offsetStorage[key];
                            else
                            {
                                body.records[i].record.Rank[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].spellRank[j]) + 1;
                                offsetStorage.Add(key, body.records[i].record.Rank[j]);
                                reverseStorage.Add(body.records[i].record.Rank[j], body.records[i].spellRank[j]);
                            }
                        }
                        // Tooltip
                        if (body.records[i].spellTool[j].Length == 0)
                            body.records[i].record.ToolTip[j] = 0;
                        else
                        {
                            int key = body.records[i].spellTool[j].GetHashCode();
                            if (offsetStorage.ContainsKey(key))
                                body.records[i].record.ToolTip[j] = offsetStorage[key];
                            else
                            {
                                body.records[i].record.ToolTip[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].spellTool[j]) + 1;
                                offsetStorage.Add(key, body.records[i].record.ToolTip[j]);
                                reverseStorage.Add(body.records[i].record.ToolTip[j], body.records[i].spellTool[j]);
                            }
                        }
                        // Desc
                        if (body.records[i].spellDesc[j].Length == 0)
                            body.records[i].record.Description[j] = 0;
                        else
                        {
                            int key = body.records[i].spellDesc[j].GetHashCode();
                            if (offsetStorage.ContainsKey(key))
                                body.records[i].record.Description[j] = offsetStorage[key];
                            else
                            {
                                body.records[i].record.Description[j] = stringBlockOffset;
                                stringBlockOffset += (UInt32)Encoding.UTF8.GetByteCount(body.records[i].spellDesc[j]) + 1;
                                offsetStorage.Add(key, body.records[i].record.Description[j]);
                                reverseStorage.Add(body.records[i].record.Description[j], body.records[i].spellDesc[j]);
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
                    count = Marshal.SizeOf(typeof(SpellRecord));
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

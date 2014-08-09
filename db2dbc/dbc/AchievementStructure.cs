using System;

using System.Runtime.InteropServices;

namespace DBtoDBC
{
    public struct AchievementBody
    {
        public AchievementRecordMap[] records;
    };

    public struct AchievementRecordMap
    {
        public AchievementRecord record;
        public string[] achName;
        public string[] achDesc;
        public string[] achRewd;
    };

    public struct AchievementRecord
    {
        public Int32 ID;
        public Int32 Faction;
        public Int32 Map;
        public Int32 Previous;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description;
        public Int32 Category;
        public Int32 Points;
        public Int32 OrderInGroup;
        public Int32 Flags;
        public Int32 SpellIcon;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Reward;
        public Int32 Demands;
        public Int32 ReferencedAchievement;
    };
}

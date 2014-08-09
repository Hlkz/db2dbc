using System;

using System.Runtime.InteropServices;

namespace DBtoDBC
{
    public struct TalentBody
    {
        public TalentRecordMap[] records;
    };

    public struct TalentRecordMap
    {
        public TalentRecord record;
    };

    public struct TalentRecord
    {
        public Int32 TalentID;
        public Int32 TalentTab;
        public Int32 Row;
        public Int32 Col;
        public Int32 Rank1;
        public Int32 Rank2;
        public Int32 Rank3;
        public Int32 Rank4;
        public Int32 Rank5;
        public Int32 Rank6;
        public Int32 Rank7;
        public Int32 Rank8;
        public Int32 Rank9;
        public Int32 DependsOn;
        public Int32 DependsOnRank;
        public Int32 unk0;
        public Int32 unk1;
        public Int32 unk2;
        public Int32 unk3;
        public Int32 needAddInSpellBook;
        public Int32 requiredSpellID;
        public Int32 allowForPetFlagsHigh;
        public Int32 allowForPetFlagsLow;
    };
}

using System;
using System.Runtime.InteropServices;

namespace DBtoDBC
{
    public struct achievement_criteriaRecord {
        public Int32 Id;
        public Int32 Achievement;
        public Int32 Type;
        public Int32 AssetId;
        public Int32 Quantity;
        public Int32 StartEvent;
        public Int32 StartAsset;
        public Int32 FailEvent;
        public Int32 FailAsset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description;
        public Int32 Flags;
        public Int32 TimerAssetId;
        public Int32 TimerStartEvent;
        public Int32 TimerTime;
        public Int32 UIOrder; }

    public struct achievementRecord {
        public Int32 Id;
        public Int32 Faction;
        public Int32 MapId;
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
        public Int32 ReferencedAchievement; }

    public struct areagroupRecord {
        public Int32 Id;
        public Int32 AreaId1;
        public Int32 AreaId2;
        public Int32 AreaId3;
        public Int32 AreaId4;
        public Int32 AreaId5;
        public Int32 AreaId6;
        public Int32 NextGroup; }

    public struct areapoiRecord {
        public Int32 Id;
        public Int32 Importance;
        public Int32 NormalIcon;
        public Int32 NormalIcon50;
        public Int32 NormalIcon0;
        public Int32 HordeIcon;
        public Int32 HordeIcon50;
        public Int32 HordeIcon0;
        public Int32 AllianceIcon;
        public Int32 AllianceIcon50;
        public Int32 AllianceIcon0;
        public Int32 FactionId;
        public float X;
        public float Y;
        public float Z;
        public Int32 MapId;
        public Int32 Flags;
        public Int32 Area;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description;
        public Int32 WorldState;
        public Int32 WorldMapLink; }

    public struct areatableRecord {
        public Int32 Id;
        public Int32 MapId;
        public Int32 ParentArea;
        public Int32 ExploreFlag;
        public Int32 Flags;
        public Int32 SoundPreferences;
        public Int32 SoundPreferencesUnderwater;
        public Int32 SoundAmbience;
        public Int32 ZoneMusic;
        public Int32 ZoneIntroMusic;
        public Int32 ExplorationLevel;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 FactionGroup;
        public Int32 LiquidTypeWater;
        public Int32 LiquidTypeOcean;
        public Int32 LiquidTypeMagma;
        public Int32 LiquidTypeSlime;
        public float MinElevation;
        public float AmbientMultiplier;
        public Int32 Light; }

    public struct areatriggerRecord {
        public Int32 Id;
        public Int32 MapId;
        public float X;
        public float Y;
        public float Z;
        public float Radius;
        public float BoxX;
        public float BoxY;
        public float BoxZ;
        public float BoxOrientation; }

    public struct auctionhouseRecord {
        public Int32 HouseId;
        public Int32 Faction;
        public Int32 DepositPercent;
        public Int32 CutPercent;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct bankbagslotpricesRecord {
        public Int32 Id;
        public Int32 Price; }

    public struct bannedaddonsRecord {
        public Int32 Id;
        public Int32 NameMD51;
        public Int32 NameMD52;
        public Int32 NameMD53;
        public Int32 NameMD54;
        public Int32 VersionMD51;
        public Int32 VersionMD52;
        public Int32 VersionMD53;
        public Int32 VersionMD54;
        public Int32 Timestamp;
        public Int32 State; }

    public struct barbershopstyleRecord {
        public Int32 Id;
        public Int32 Type;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] AdditionalName;
        public float CostMultiplier;
        public Int32 Race;
        public Int32 Gender;
        public Int32 HairId; }

    public struct battlemasterlistRecord {
        public Int32 Id;
        public Int32 Instance1;
        public Int32 Instance2;
        public Int32 Instance3;
        public Int32 Instance4;
        public Int32 Instance5;
        public Int32 Instance6;
        public Int32 Instance7;
        public Int32 Instance8;
        public Int32 InstanceType;
        public Int32 JoinAsGroup;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 MaxGroupSize;
        public Int32 HolidayWorldStateId;
        public Int32 MinLevel;
        public Int32 MaxLevel; }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct charstartoutfitRecord {
        public Int32 Id;
        public Byte Race;
        public Byte Class;
        public Byte Gender;
        public Byte Unused;
        public Int32 ItemId1;
        public Int32 ItemId2;
        public Int32 ItemId3;
        public Int32 ItemId4;
        public Int32 ItemId5;
        public Int32 ItemId6;
        public Int32 ItemId7;
        public Int32 ItemId8;
        public Int32 ItemId9;
        public Int32 ItemId10;
        public Int32 ItemId11;
        public Int32 ItemId12;
        public Int32 ItemId13;
        public Int32 ItemId14;
        public Int32 ItemId15;
        public Int32 ItemId16;
        public Int32 ItemId17;
        public Int32 ItemId18;
        public Int32 ItemId19;
        public Int32 ItemId20;
        public Int32 ItemId21;
        public Int32 ItemId22;
        public Int32 ItemId23;
        public Int32 ItemId24;
        public Int32 ItemDisplayId1;
        public Int32 ItemDisplayId2;
        public Int32 ItemDisplayId3;
        public Int32 ItemDisplayId4;
        public Int32 ItemDisplayId5;
        public Int32 ItemDisplayId6;
        public Int32 ItemDisplayId7;
        public Int32 ItemDisplayId8;
        public Int32 ItemDisplayId9;
        public Int32 ItemDisplayId10;
        public Int32 ItemDisplayId11;
        public Int32 ItemDisplayId12;
        public Int32 ItemDisplayId13;
        public Int32 ItemDisplayId14;
        public Int32 ItemDisplayId15;
        public Int32 ItemDisplayId16;
        public Int32 ItemDisplayId17;
        public Int32 ItemDisplayId18;
        public Int32 ItemDisplayId19;
        public Int32 ItemDisplayId20;
        public Int32 ItemDisplayId21;
        public Int32 ItemDisplayId22;
        public Int32 ItemDisplayId23;
        public Int32 ItemDisplayId24;
        public Int32 ItemInventorySlot1;
        public Int32 ItemInventorySlot2;
        public Int32 ItemInventorySlot3;
        public Int32 ItemInventorySlot4;
        public Int32 ItemInventorySlot5;
        public Int32 ItemInventorySlot6;
        public Int32 ItemInventorySlot7;
        public Int32 ItemInventorySlot8;
        public Int32 ItemInventorySlot9;
        public Int32 ItemInventorySlot10;
        public Int32 ItemInventorySlot11;
        public Int32 ItemInventorySlot12;
        public Int32 ItemInventorySlot13;
        public Int32 ItemInventorySlot14;
        public Int32 ItemInventorySlot15;
        public Int32 ItemInventorySlot16;
        public Int32 ItemInventorySlot17;
        public Int32 ItemInventorySlot18;
        public Int32 ItemInventorySlot19;
        public Int32 ItemInventorySlot20;
        public Int32 ItemInventorySlot21;
        public Int32 ItemInventorySlot22;
        public Int32 ItemInventorySlot23;
        public Int32 ItemInventorySlot24; }

    public struct chartitlesRecord {
        public Int32 Id;
        public Int32 UnkRef;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Male;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Female;
        public Int32 InGameOrder; }

    public struct chatchannelsRecord {
        public Int32 ChannelId;
        public Int32 Flags;
        public Int32 FactionGroup;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Pattern;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct chrclassesRecord {
        public Int32 Id;
        public Int32 Unused;
        public Int32 PowerType;
        public Int32 DispayPower;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Female;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Male;
        public UInt32 FileName; // string
        public Int32 SpellFamily;
        public Int32 Flags;
        public Int32 CinematicSequence;
        public Int32 Expansion; }

    public struct chrracesRecord {
        public Int32 Id;
        public Int32 Flags;
        public Int32 FactionId;
        public Int32 Exploration;
        public Int32 ModelMale;
        public Int32 ModelFemale;
        public UInt32 ClientPrefix; // string
        public Int32 BaseLanguage;
        public Int32 CreatureType;
        public Int32 ResSicknessSpellId;
        public Int32 SplashSoundId;
        public UInt32 InternalName; // string
        public Int32 CinematicSequence;
        public Int32 TeamId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] NameFemale;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] NameMale;
        public UInt32 FacialHairCustomization1; // string
        public UInt32 FacialHairCustomization2; // string
        public UInt32 HairCustomization; // string
        public Int32 Expansion; }

    public struct cinematicsequencesRecord {
        public Int32 Id;
        public Int32 SoundId;
        public Int32 CinematicCamera;
        public Int32 Camera1;
        public Int32 Camera2;
        public Int32 Camera3;
        public Int32 Camera4;
        public Int32 Camera5;
        public Int32 Camera6;
        public Int32 Camera7; }

    public struct creaturedisplayinfoRecord {
        public Int32 Id;
        public Int32 ModelId;
        public Int32 Sound;
        public Int32 ExtraId;
        public float Scale;
        public Int32 Opacity;
        public UInt32 Skin1; // string
        public UInt32 Skin2; // string
        public UInt32 Skin3; // string
        public UInt32 PortraitTextureName; // string
        public Int32 BloodLevel;
        public Int32 Blood;
        public Int32 NPCSounds;
        public Int32 Particles;
        public Int32 CreatureGeoosetData;
        public Int32 ObjectEffectPackageId; }

    public struct creaturedisplayinfoextraRecord {
        public Int32 Id;
        public Int32 Race;
        public Int32 Gender;
        public Int32 SkinColor;
        public Int32 FaceType;
        public Int32 HairType;
        public Int32 HairStyle;
        public Int32 FacialHair;
        public Int32 HelmDisplayId;
        public Int32 ShoulderDisplayId;
        public Int32 ShirtDisplayId;
        public Int32 ChestDisplayId;
        public Int32 BeltDisplayId;
        public Int32 LegsDisplayId;
        public Int32 BootsDisplayId;
        public Int32 WristDisplayId;
        public Int32 GlovesDisplayId;
        public Int32 TabardDisplayId;
        public Int32 CloakDisplayId;
        public Int32 CanEquip;
        public UInt32 Texture; } // string

    public struct creaturefamilyRecord {
        public Int32 Id;
        public float MinScale;
        public Int32 MinScaleLevel;
        public float MaxScale;
        public Int32 MaxScaleLevel;
        public Int32 SkillLine_F6;
        public Int32 SkillLine_F7;
        public Int32 PetFoodMask;
        public Int32 PetTalentType;
        public Int32 CategoryEnumID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public UInt32 IconFile; } // string

    public struct creaturemodeldataRecord {
        public Int32 Id;
        public Int32 Flags;
        public UInt32 Model; // string
        public Int32 SizeClass;
        public float Scale;
        public Int32 BloodLevel;
        public Int32 FootprintTexture;
        public float FootprintTextureLength;
        public float FootprintTextureWidth;
        public float FootprintParticleScale;
        public Int32 FoleyMaterialId;
        public Int32 FootstepShakeSize;
        public Int32 DeathThudShakeSize;
        public Int32 Sound;
        public float CollisionWidth;
        public float CollisionHeight;
        public float MountHeight;
        public float GeoBoxMin1;
        public float GeoBoxMin2;
        public float GeoBoxMin3;
        public float GeoBoxMax1;
        public float GeoBoxMax2;
        public float GeoBoxMax3;
        public float WorldEffectScale;
        public float AttachedEffectScale;
        public float MissileCollisionRadius;
        public Int32 MissileCollisionPush;
        public Int32 MissileCollisionRaise; }

    public struct creaturespelldataRecord {
        public Int32 Id;
        public Int32 SpellId1;
        public Int32 SpellId2;
        public Int32 SpellId3;
        public Int32 SpellId4;
        public Int32 Availability1;
        public Int32 Availability2;
        public Int32 Availability3;
        public Int32 Availability4; }

    public struct creaturetypeRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 NoExperience; }

    public struct currencytypesRecord {
        public Int32 Id;
        public Int32 ItemId;
        public Int32 Category;
        public Int32 BitIndex; }

    public struct destructiblemodeldataRecord {
        public Int32 Id;
        public Int32 DamagedUnk1;
        public Int32 DamagedUnk2;
        public Int32 DamagedDisplayId;
        public Int32 DamagedUnk3;
        public Int32 DestroyedUnk1;
        public Int32 DestroyedUnk2;
        public Int32 DestroyedDisplayId;
        public Int32 DestroyedUnk3;
        public Int32 RebuildingUnk1;
        public Int32 RebuildingUnk2;
        public Int32 RebuildingDisplayId;
        public Int32 RebuildingUnk3;
        public Int32 SmokeUnk1;
        public Int32 SmokeUnk2;
        public Int32 SmokeDisplayId;
        public Int32 SmokeUnk3;
        public Int32 Unk4;
        public Int32 Unk5; }

    public struct dungeonencounterRecord {
        public Int32 Id;
        public Int32 MapId;
        public Int32 Difficulty;
        public Int32 OrderIndex;
        public Int32 EncounterIndex;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] EncounterName;
        public Int32 SpellIconId; }

    public struct durabilitycostsRecord {
        public Int32 ItemLevel;
        public Int32 Multiplier1;
        public Int32 Multiplier2;
        public Int32 Multiplier3;
        public Int32 Multiplier4;
        public Int32 Multiplier5;
        public Int32 Multiplier6;
        public Int32 Multiplier7;
        public Int32 Multiplier8;
        public Int32 Multiplier9;
        public Int32 Multiplier10;
        public Int32 Multiplier11;
        public Int32 Multiplier12;
        public Int32 Multiplier13;
        public Int32 Multiplier14;
        public Int32 Multiplier15;
        public Int32 Multiplier16;
        public Int32 Multiplier17;
        public Int32 Multiplier18;
        public Int32 Multiplier19;
        public Int32 Multiplier20;
        public Int32 Multiplier21;
        public Int32 Multiplier22;
        public Int32 Multiplier23;
        public Int32 Multiplier24;
        public Int32 Multiplier25;
        public Int32 Multiplier26;
        public Int32 Multiplier27;
        public Int32 Multiplier28;
        public Int32 Multiplier29; }

    public struct durabilityqualityRecord {
        public Int32 Id;
        public float QualityMod; }

    public struct emotesRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public Int32 AnimationId;
        public Int32 Flags;
        public Int32 EmoteType;
        public Int32 UnitStandState;
        public Int32 SoundId; }

    public struct emotestextRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public UInt32 EmoteId;
        public Int32 TextData1;
        public Int32 TextData2;
        public Int32 TextData3;
        public Int32 TextData4;
        public Int32 TextData5;
        public Int32 TextData6;
        public Int32 TextData7;
        public Int32 TextData8;
        public Int32 TextData9;
        public Int32 TextData10;
        public Int32 TextData11;
        public Int32 TextData12;
        public Int32 TextData13;
        public Int32 TextData14;
        public Int32 TextData15;
        public Int32 TextData16; }

    public struct factionRecord {
        public Int32 Id;
        public Int32 ReputationListId;
        public Int32 BaseRepRaceMask1;
        public Int32 BaseRepRaceMask2;
        public Int32 BaseRepRaceMask3;
        public Int32 BaseRepRaceMask4;
        public Int32 BaseRepClassMask1;
        public Int32 BaseRepClassMask2;
        public Int32 BaseRepClassMask3;
        public Int32 BaseRepClassMask4;
        public Int32 BaseRepValue1;
        public Int32 BaseRepValue2;
        public Int32 BaseRepValue3;
        public Int32 BaseRepValue4;
        public Int32 ReputationFlags1;
        public Int32 ReputationFlags2;
        public Int32 ReputationFlags3;
        public Int32 ReputationFlags4;
        public Int32 Team;
        public float SpilloverRateIn;
        public float SpilloverRateOut;
        public Int32 SpilloverMaxRankIn;
        public Int32 SpilloverRankUnk;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description; }

    public struct factiontemplateRecord {
        public Int32 Id;
        public Int32 Faction;
        public Int32 FactionFlags;
        public Int32 OurMask;
        public Int32 FriendlyMask;
        public Int32 HostileMask;
        public Int32 EnemyFaction1;
        public Int32 EnemyFaction2;
        public Int32 EnemyFaction3;
        public Int32 EnemyFaction4;
        public Int32 FriendFaction1;
        public Int32 FriendFaction2;
        public Int32 FriendFaction3;
        public Int32 FriendFaction4; }

    public struct gameobjectdisplayinfoRecord {
        public Int32 Id;
        public UInt32 FileName; // string
        public Int32 Unk1;
        public Int32 Unk2;
        public Int32 Unk3;
        public Int32 Unk4;
        public Int32 Unk5;
        public Int32 Unk6;
        public Int32 Unk7;
        public Int32 Unk8;
        public Int32 Unk9;
        public Int32 Unk10;
        public float MinX;
        public float MinY;
        public float MinZ;
        public float MaxX;
        public float MaxY;
        public float MaxZ;
        public Int32 Transport; }

    public struct gempropertiesRecord {
        public Int32 Id;
        public Int32 SpellItemEnchantment;
        public Int32 MaxCountInventory;
        public Int32 MaxCountItem;
        public Int32 Color; }

    public struct glyphpropertiesRecord {
        public Int32 Id;
        public Int32 SpellId;
        public Int32 TypeFlags;
        public Int32 Unk1; }

    public struct glyphslotRecord {
        public Int32 Id;
        public Int32 TypeFlags;
        public Int32 Order; }

    public struct holidaysRecord {
        public Int32 Id;
        public Int32 Duration1;
        public Int32 Duration2;
        public Int32 Duration3;
        public Int32 Duration4;
        public Int32 Duration5;
        public Int32 Duration6;
        public Int32 Duration7;
        public Int32 Duration8;
        public Int32 Duration9;
        public Int32 Duration10;
        public float Date1;
        public float Date2;
        public float Date3;
        public float Date4;
        public float Date5;
        public float Date6;
        public float Date7;
        public float Date8;
        public float Date9;
        public float Date10;
        public float Date11;
        public float Date12;
        public float Date13;
        public Int32 Date14;
        public Int32 Date15;
        public Int32 Date16;
        public Int32 Date17;
        public Int32 Date18;
        public Int32 Date19;
        public Int32 Date20;
        public Int32 Date21;
        public Int32 Date22;
        public Int32 Date23;
        public Int32 Date24;
        public Int32 Date25;
        public Int32 Date26;
        public Int32 Region;
        public Int32 Looping;
        public Int32 CalendarFlags1;
        public Int32 CalendarFlags2;
        public Int32 CalendarFlags3;
        public Int32 CalendarFlags4;
        public Int32 CalendarFlags5;
        public Int32 CalendarFlags6;
        public Int32 CalendarFlags7;
        public Int32 CalendarFlags8;
        public Int32 CalendarFlags9;
        public Int32 CalendarFlags10;
        public Int32 HolidayNameId;
        public Int32 HolidayDescriptionId;
        public UInt32 TextureFilename; // string
        public Int32 Priority;
        public Int32 CalendarFilterType;
        public Int32 Flags; }
    
    public struct itemRecord {
        public Int32 Id;
        public Int32 Class;
        public Int32 SubClass;
        public Int32 SoundOverride;
        public Int32 Material;
        public Int32 DisplayInfo;
        public Int32 InventorySlot;
        public Int32 Sheath; }

    public struct itembagfamilyRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct itemextendedcostRecord {
        public Int32 Id;
        public Int32 ReqHonorPoints;
        public Int32 ReqArenaPoints;
        public Int32 ReqArenaSlot;
        public Int32 ReqItem1;
        public Int32 ReqItem2;
        public Int32 ReqItem3;
        public Int32 ReqItem4;
        public Int32 ReqItem5;
        public Int32 ReqItemCount1;
        public Int32 ReqItemCount2;
        public Int32 ReqItemCount3;
        public Int32 ReqItemCount4;
        public Int32 ReqItemCount5;
        public Int32 ReqPersonalArenaRating;
        public Int32 PurchaseGroup; }

    public struct itemlimitcategoryRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 MaxCount;
        public Int32 Mode; }

    public struct itemrandompropertiesRecord {
        public Int32 Id;
        public UInt32 InternalName; // string
        public Int32 EnchantId1;
        public Int32 EnchantId2;
        public Int32 EnchantId3;
        public Int32 EnchantId4;
        public Int32 EnchantId5;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct itemrandomsuffixRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public UInt32 InternalName; // string
        public Int32 EnchantId1;
        public Int32 EnchantId2;
        public Int32 EnchantId3;
        public Int32 EnchantId4;
        public Int32 EnchantId5;
        public Int32 Prefix1;
        public Int32 Prefix2;
        public Int32 Prefix3;
        public Int32 Prefix4;
        public Int32 Prefix5; }

    public struct itemsetRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 ItemId1;
        public Int32 ItemId2;
        public Int32 ItemId3;
        public Int32 ItemId4;
        public Int32 ItemId5;
        public Int32 ItemId6;
        public Int32 ItemId7;
        public Int32 ItemId8;
        public Int32 ItemId9;
        public Int32 ItemId10;
        public Int32 Unknown1;
        public Int32 Unknown2;
        public Int32 Unknown3;
        public Int32 Unknown4;
        public Int32 Unknown5;
        public Int32 Unknown6;
        public Int32 Unknown7;
        public Int32 Spells1;
        public Int32 Spells2;
        public Int32 Spells3;
        public Int32 Spells4;
        public Int32 Spells5;
        public Int32 Spells6;
        public Int32 Spells7;
        public Int32 Spells8;
        public Int32 ItemsToTriggerSpell1;
        public Int32 ItemsToTriggerSpell2;
        public Int32 ItemsToTriggerSpell3;
        public Int32 ItemsToTriggerSpell4;
        public Int32 ItemsToTriggerSpell5;
        public Int32 ItemsToTriggerSpell6;
        public Int32 ItemsToTriggerSpell7;
        public Int32 ItemsToTriggerSpell8;
        public Int32 RequiredSkillId;
        public Int32 RequiredSkillValue; }

    public struct lfgdungeonsRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 MinLevel;
        public Int32 MaxLevel;
        public Int32 RecLevel;
        public Int32 RecMinLevel;
        public Int32 RecMaxLevel;
        public Int32 MapId;
        public Int32 Difficulty;
        public Int32 Flags;
        public Int32 Type;
        public Int32 Unk;
        public UInt32 IconName; // string
        public Int32 Expansion;
        public Int32 Unk2;
        public Int32 GroupType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description; }

    public struct lightRecord {
        public Int32 Id;
        public Int32 MapId;
        public float X;
        public float Y;
        public float Z;
        public float FalloffStart;
        public float FalloffEnd;
        public Int32 SkyAndFog;
        public Int32 WaterSettings;
        public Int32 SunsetParams;
        public Int32 OtherParams;
        public Int32 DeathParams;
        public Int32 Unk1;
        public Int32 Unk2;
        public Int32 Unk3; }

    public struct liquidtypeRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public Int32 Flags;
        public Int32 Type;
        public Int32 SoundId;
        public Int32 SpellId;
        public float MaxDarkenDepth;
        public float FogDarkenIntensity;
        public float AmbDarkenIntensity;
        public float DirDarkenIntensity;
        public Int32 LightID;
        public float ParticleScale;
        public Int32 ParticleMovement;
        public Int32 ParticleTexSlots;
        public Int32 LiquidMaterialID;
        public UInt32 Texture1; // string
        public UInt32 Texture2; // string
        public UInt32 Texture3; // string
        public UInt32 Texture4; // string
        public UInt32 Texture5; // string
        public UInt32 Texture6; // string
        public Int32 Color1;
        public Int32 Color2;
        public float UnkA1;
        public float UnkA2;
        public float UnkA3;
        public float UnkA4;
        public float UnkA5;
        public float UnkA6;
        public float UnkA7;
        public float UnkA8;
        public float UnkA9;
        public float UnkA10;
        public float UnkA11;
        public float UnkA12;
        public float UnkA13;
        public float UnkA14;
        public float UnkA15;
        public float UnkA16;
        public float UnkA17;
        public float UnkA18;
        public Int32 UnkB1;
        public Int32 UnkB2;
        public Int32 UnkB3;
        public Int32 UnkB4; }

    public struct lockRecord {
        public Int32 Id;
        public Int32 Type1;
        public Int32 Type2;
        public Int32 Type3;
        public Int32 Type4;
        public Int32 Type5;
        public Int32 Type6;
        public Int32 Type7;
        public Int32 Type8;
        public Int32 Index1;
        public Int32 Index2;
        public Int32 Index3;
        public Int32 Index4;
        public Int32 Index5;
        public Int32 Index6;
        public Int32 Index7;
        public Int32 Index8;
        public Int32 Skill1;
        public Int32 Skill2;
        public Int32 Skill3;
        public Int32 Skill4;
        public Int32 Skill5;
        public Int32 Skill6;
        public Int32 Skill7;
        public Int32 Skill8;
        public Int32 Action1;
        public Int32 Action2;
        public Int32 Action3;
        public Int32 Action4;
        public Int32 Action5;
        public Int32 Action6;
        public Int32 Action7;
        public Int32 Action8; }

    public struct mailtemplateRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Subject;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Content; }

    public struct mapRecord {
        public Int32 Id;
        public UInt32 InternalName; // string
        public Int32 MapType;
        public Int32 Flags;
        public Int32 IsBattleground;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 LinkedZone;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] HordeIntro;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] AllianceIntro;
        public Int32 MultiMapId;
        public float BattlefieldMapIconScale;
        public Int32 EntranceMap;
        public float EntranceX;
        public float EntranceY;
        public Int32 TimeOfDayOverride;
        public Int32 Addon;
        public Int32 UnkTime;
        public Int32 MaxPlayers; }

    public struct mapdifficultyRecord {
        public Int32 Id;
        public Int32 MapId;
        public Int32 Difficulty;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] AreaTriggerText;
        public Int32 ResetTime;
        public Int32 MaxPlayers;
        public UInt32 DifficultyString; } // string

    public struct movieRecord {
        public Int32 Id;
        public UInt32 Filename; // string
        public Int32 Unk; }

    public struct overridespelldataRecord {
        public Int32 Id;
        public Int32 SpellId1;
        public Int32 SpellId2;
        public Int32 SpellId3;
        public Int32 SpellId4;
        public Int32 SpellId5;
        public Int32 SpellId6;
        public Int32 SpellId7;
        public Int32 SpellId8;
        public Int32 SpellId9;
        public Int32 SpellId10;
        public Int32 Unk; }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct powerdisplayRecord {
        public Int32 Id;
        public Int32 PowerType;
        public UInt32 Name; // string
        public Byte R;
        public Byte G;
        public Byte B; }

    public struct pvpdifficultyRecord {
        public Int32 Id;
        public Int32 MapId;
        public Int32 BracketId;
        public Int32 MinLevel;
        public Int32 MaxLevel;
        public Int32 Difficulty; }

    public struct questfactionrewardRecord {
        public Int32 Id;
        public Int32 QuestRewFactionValue1;
        public Int32 QuestRewFactionValue2;
        public Int32 QuestRewFactionValue3;
        public Int32 QuestRewFactionValue4;
        public Int32 QuestRewFactionValue5;
        public Int32 QuestRewFactionValue6;
        public Int32 QuestRewFactionValue7;
        public Int32 QuestRewFactionValue8;
        public Int32 QuestRewFactionValue9;
        public Int32 QuestRewFactionValue10; }

    public struct questsortRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct questxpRecord {
        public Int32 Id;
        public Int32 Exp1;
        public Int32 Exp2;
        public Int32 Exp3;
        public Int32 Exp4;
        public Int32 Exp5;
        public Int32 Exp6;
        public Int32 Exp7;
        public Int32 Exp8;
        public Int32 Exp9;
        public Int32 Exp10; }

    public struct randproppointsRecord {
        public Int32 ItemLevel;
        public Int32 EpicPropertiesPoints1;
        public Int32 EpicPropertiesPoints2;
        public Int32 EpicPropertiesPoints3;
        public Int32 EpicPropertiesPoints4;
        public Int32 EpicPropertiesPoints5;
        public Int32 RarePropertiesPoints1;
        public Int32 RarePropertiesPoints2;
        public Int32 RarePropertiesPoints3;
        public Int32 RarePropertiesPoints4;
        public Int32 RarePropertiesPoints5;
        public Int32 UncommonPropertiesPoints1;
        public Int32 UncommonPropertiesPoints2;
        public Int32 UncommonPropertiesPoints3;
        public Int32 UncommonPropertiesPoints4;
        public Int32 UncommonPropertiesPoints5; }

    public struct scalingstatdistributionRecord {
        public Int32 Id;
        public Int32 StatMod1;
        public Int32 StatMod2;
        public Int32 StatMod3;
        public Int32 StatMod4;
        public Int32 StatMod5;
        public Int32 StatMod6;
        public Int32 StatMod7;
        public Int32 StatMod8;
        public Int32 StatMod9;
        public Int32 StatMod10;
        public Int32 Modifier1;
        public Int32 Modifier2;
        public Int32 Modifier3;
        public Int32 Modifier4;
        public Int32 Modifier5;
        public Int32 Modifier6;
        public Int32 Modifier7;
        public Int32 Modifier8;
        public Int32 Modifier9;
        public Int32 Modifier10;
        public Int32 MaxLevel; }

    public struct scalingstatvaluesRecord {
        public Int32 Id;
        public Int32 Level;
        public Int32 SsdMultiplierA1;
        public Int32 SsdMultiplierA2;
        public Int32 SsdMultiplierA3;
        public Int32 SsdMultiplierA4;
        public Int32 ArmorModA1;
        public Int32 ArmorModA2;
        public Int32 ArmorModA3;
        public Int32 ArmorModA4;
        public Int32 DpsMod1;
        public Int32 DpsMod2;
        public Int32 DpsMod3;
        public Int32 DpsMod4;
        public Int32 DpsMod5;
        public Int32 DpsMod6;
        public Int32 SpellPower;
        public Int32 SsdMultiplierB;
        public Int32 SsdMultiplierC;
        public Int32 ArmorModB1;
        public Int32 ArmorModB2;
        public Int32 ArmorModB3;
        public Int32 ArmorModB4;
        public Int32 ArmorModB5; }

    public struct skilllineabilityRecord {
        public Int32 Id;
        public Int32 SkillId;
        public Int32 SpellId;
        public Int32 Racemask;
        public Int32 Classemask;
        public Int32 RacemaskNot;
        public Int32 ClassemaskNot;
        public Int32 ReqSkillValue;
        public Int32 ForwardSpellId;
        public Int32 LearnOnGetSkill;
        public Int32 MaxValue;
        public Int32 MinValue;
        public Int32 CharacterPoints1;
        public Int32 CharacterPoints2; }

    public struct skilllineRecord {
        public Int32 Id;
        public Int32 CategoryId;
        public Int32 SkillCostId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description;
        public Int32 SpellIcon;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] AlternateVerb;
        public Int32 CanLink; }

    public struct soundentriesRecord {
        public Int32 Id;
        public Int32 Type;
        public UInt32 InternalName; // string
        public UInt32 FileName1; // string
        public UInt32 FileName2; // string
        public UInt32 FileName3; // string
        public UInt32 FileName4; // string
        public UInt32 FileName5; // string
        public UInt32 FileName6; // string
        public UInt32 FileName7; // string
        public UInt32 FileName8; // string
        public UInt32 FileName9; // string
        public UInt32 FileName10; // string
        public Int32 Freq1;
        public Int32 Freq2;
        public Int32 Freq3;
        public Int32 Freq4;
        public Int32 Freq5;
        public Int32 Freq6;
        public Int32 Freq7;
        public Int32 Freq8;
        public Int32 Freq9;
        public Int32 Freq10;
        public UInt32 Path; // string
        public float Volume;
        public Int32 Flags;
        public float MinDistance;
        public float DistanceCutOff;
        public Int32 EAXDef;
        public Int32 SoundEntriesAdvancedId; }

    public struct spellcasttimesRecord {
        public Int32 Id;
        public Int32 CastTime;
        public Int32 CastTimePetLevel;
        public Int32 MinCastTime; }

    public struct spellcategoryRecord {
        public Int32 Id;
        public Int32 Flags; }

    public struct spellRecord {
        public UInt32 Entry;
        public UInt32 Category;
        public UInt32 Dispel;
        public UInt32 Mechanic;
        public UInt32 Attributes;
        public UInt32 AttributesEx;
        public UInt32 AttributesEx2;
        public UInt32 AttributesEx3;
        public UInt32 AttributesEx4;
        public UInt32 AttributesEx5;
        public UInt32 AttributesEx6;
        public UInt32 AttributesEx7;
        public UInt32 Stances;
        public UInt32 unk320_2;
        public UInt32 StancesNot;
        public UInt32 unk320_3;
        public UInt32 Targets;
        public UInt32 TargetCreatureType;
        public UInt32 RequiresSpellFocus;
        public UInt32 FacingCasterFlags;
        public UInt32 CasterAuraState;
        public UInt32 TargetAuraState;
        public UInt32 CasterAuraStateNot;
        public UInt32 TargetAuraStateNot;
        public UInt32 casterAuraSpell;
        public UInt32 targetAuraSpell;
        public UInt32 excludeCasterAuraSpell;
        public UInt32 excludeTargetAuraSpell;
        public UInt32 CastingTimeIndex;
        public UInt32 RecoveryTime;
        public UInt32 CategoryRecoveryTime;
        public UInt32 InterruptFlags;
        public UInt32 AuraInterruptFlags;
        public UInt32 ChannelInterruptFlags;
        public UInt32 procFlags;
        public UInt32 procChance;
        public UInt32 procCharges;
        public UInt32 maxLevel;
        public UInt32 baseLevel;
        public UInt32 spellLevel;
        public UInt32 DurationIndex;
        public UInt32 powerType;
        public UInt32 manaCost;
        public UInt32 manaCostPerlevel;
        public UInt32 manaPerSecond;
        public UInt32 manaPerSecondPerLevel;
        public UInt32 rangeIndex;
        public float speed;
        public Int32 modalNextSpell;
        public Int32 StackAmount;
        public Int32 Totem1;
        public Int32 Totem2;
        public Int32 Reagent1;
        public Int32 Reagent2;
        public Int32 Reagent3;
        public Int32 Reagent4;
        public Int32 Reagent5;
        public Int32 Reagent6;
        public Int32 Reagent7;
        public Int32 Reagent8;
        public Int32 ReagentCount1;
        public Int32 ReagentCount2;
        public Int32 ReagentCount3;
        public Int32 ReagentCount4;
        public Int32 ReagentCount5;
        public Int32 ReagentCount6;
        public Int32 ReagentCount7;
        public Int32 ReagentCount8;
        public Int32 EquippedItemClass;
        public Int32 EquippedItemSubClassMask;
        public Int32 EquippedItemInventoryTypeMask;
        public Int32 Effect1;
        public Int32 Effect2;
        public Int32 Effect3;
        public Int32 EffectDieSides1;
        public Int32 EffectDieSides2;
        public Int32 EffectDieSides3;
        public float EffectRealPointsPerLevel1;
        public float EffectRealPointsPerLevel2;
        public float EffectRealPointsPerLevel3;
        public Int32 EffectBasePoints1;
        public Int32 EffectBasePoints2;
        public Int32 EffectBasePoints3;
        public Int32 EffectMechanic1;
        public Int32 EffectMechanic2;
        public Int32 EffectMechanic3;
        public Int32 EffectImplicitTargetA1;
        public Int32 EffectImplicitTargetA2;
        public Int32 EffectImplicitTargetA3;
        public Int32 EffectImplicitTargetB1;
        public Int32 EffectImplicitTargetB2;
        public Int32 EffectImplicitTargetB3;
        public Int32 EffectRadiusIndex1;
        public Int32 EffectRadiusIndex2;
        public Int32 EffectRadiusIndex3;
        public Int32 EffectApplyAuraName1;
        public Int32 EffectApplyAuraName2;
        public Int32 EffectApplyAuraName3;
        public Int32 EffectAmplitude1;
        public Int32 EffectAmplitude2;
        public Int32 EffectAmplitude3;
        public float EffectValueMultiplier1;
        public float EffectValueMultiplier2;
        public float EffectValueMultiplier3;
        public Int32 EffectChainTarget1;
        public Int32 EffectChainTarget2;
        public Int32 EffectChainTarget3;
        public Int32 EffectItemType1;
        public Int32 EffectItemType2;
        public Int32 EffectItemType3;
        public Int32 EffectMiscValue1;
        public Int32 EffectMiscValue2;
        public Int32 EffectMiscValue3;
        public Int32 EffectMiscValueB1;
        public Int32 EffectMiscValueB2;
        public Int32 EffectMiscValueB3;
        public Int32 EffectTriggerSpell1;
        public Int32 EffectTriggerSpell2;
        public Int32 EffectTriggerSpell3;
        public float EffectPointsPerComboPoint1;
        public float EffectPointsPerComboPoint2;
        public float EffectPointsPerComboPoint3;
        public UInt32 EffectSpellClassMaskA1;
        public UInt32 EffectSpellClassMaskA2;
        public UInt32 EffectSpellClassMaskA3;
        public UInt32 EffectSpellClassMaskB1;
        public UInt32 EffectSpellClassMaskB2;
        public UInt32 EffectSpellClassMaskB3;
        public UInt32 EffectSpellClassMaskC1;
        public UInt32 EffectSpellClassMaskC2;
        public UInt32 EffectSpellClassMaskC3;
        public Int32 SpellVisual1;
        public Int32 SpellVisual2;
        public Int32 SpellIconID;
        public Int32 activeIconID;
        public Int32 spellPriority;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Rank;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Tooltip;
        public UInt32 ManaCostPercentage;
        public UInt32 StartRecoveryCategory;
        public UInt32 StartRecoveryTime;
        public UInt32 MaxTargetLevel;
        public UInt32 SpellFamilyName;
        public UInt32 SpellFamilyFlagsLow;
        public UInt32 SpellFamilyFlagsHigh;
        public UInt32 SpellFamilyFlags2;
        public UInt32 MaxAffectedTargets;
        public UInt32 DmgClass;
        public UInt32 PreventionType;
        public UInt32 StanceBarOrder;
        public float EffectDamageMultiplier1;
        public float EffectDamageMultiplier2;
        public float EffectDamageMultiplier3;
        public UInt32 MinFactionId;
        public UInt32 MinReputation;
        public UInt32 RequiredAuraVision;
        public UInt32 TotemCategory1;
        public UInt32 TotemCategory2;
        public Int32 AreaGroupId;
        public UInt32 SchoolMask;
        public UInt32 runeCostID;
        public UInt32 spellMissileID;
        public UInt32 PowerDisplayId;
        public float EffectBonusMultiplier1;
        public float EffectBonusMultiplier2;
        public float EffectBonusMultiplier3;
        public UInt32 spellDescriptionVariableID;
        public UInt32 SpellDifficultyId; }

    public struct spelldifficultyRecord {
        public Int32 id;
        public Int32 spellid0;
        public Int32 spellid1;
        public Int32 spellid2;
        public Int32 spellid3; }

    public struct spelldurationRecord {
        public Int32 Id;
        public Int32 Duration1;
        public Int32 Duration2;
        public Int32 Duration3; }

    public struct spellfocusobjectRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct spellitemenchantmentconditionRecord {
        public Int32 Id;
        public Byte Color1;
        public Byte Color2;
        public Byte Color3;
        public Byte Color4;
        public Byte Color5;
        public Int32 LTOperand1;
        public Int32 LTOperand2;
        public Int32 LTOperand3;
        public Int32 LTOperand4;
        public Int32 LTOperand5;
        public Byte Comparator1;
        public Byte Comparator2;
        public Byte Comparator3;
        public Byte Comparator4;
        public Byte Comparator5;
        public Byte CompareColor1;
        public Byte CompareColor2;
        public Byte CompareColor3;
        public Byte CompareColor4;
        public Byte CompareColor5;
        public Int32 Value1;
        public Int32 Value2;
        public Int32 Value3;
        public Int32 Value4;
        public Int32 Value5;
        public Byte Logic1;
        public Byte Logic2;
        public Byte Logic3;
        public Byte Logic4;
        public Byte Logic5; }

    public struct spellitemenchantmentRecord {
        public Int32 Id;
        public Int32 Charges;
        public Int32 Type1;
        public Int32 Type2;
        public Int32 Type3;
        public Int32 Amount1;
        public Int32 Amount2;
        public Int32 Amount3;
        public Int32 AmountB1;
        public Int32 AmountB2;
        public Int32 AmountB3;
        public Int32 SpellId1;
        public Int32 SpellId2;
        public Int32 SpellId3;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description;
        public Int32 AuraId;
        public Int32 Slot;
        public Int32 GemId;
        public Int32 EnchantmentCondition;
        public Int32 RequiredSkill;
        public Int32 RequiredSkillValue;
        public Int32 RequiredLevel; }

    public struct spellradiusRecord {
        public Int32 Id;
        public float RadiusMin;
        public float RadiusPerLevel;
        public float RadiusMax; }

    public struct spellrangeRecord {
        public Int32 Id;
        public float MinRangeHostile;
        public float MinRangeFriend;
        public float MaxRangeHostile;
        public float MaxRangeFriend;
        public Int32 Type;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name2; }

    public struct spellrunecostRecord {
        public Int32 Id;
        public Int32 RuneCostBlood;
        public Int32 RuneCostFrost;
        public Int32 RuneCostUnholy;
        public Int32 RunePowerGain; }

    public struct spellshapeshiftformRecord {
        public Int32 Id;
        public Int32 ButtonPosition;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 Flags1;
        public Int32 CreatureType;
        public Int32 Unk1;
        public Int32 AttackSpeed;
        public Int32 ModelIdAlliance;
        public Int32 ModelIdHorde;
        public Int32 Unk2;
        public Int32 Unk3;
        public Int32 StanceSpell1;
        public Int32 StanceSpell2;
        public Int32 StanceSpell3;
        public Int32 StanceSpell4;
        public Int32 StanceSpell5;
        public Int32 StanceSpell6;
        public Int32 StanceSpell7;
        public Int32 StanceSpell8; }

    public struct stableslotpricesRecord {
        public Int32 Slot;
        public Int32 Price; }

    public struct summonpropertiesRecord {
        public Int32 Id;
        public Int32 Category;
        public Int32 Faction;
        public Int32 Type;
        public Int32 Slot;
        public Int32 Flags; }

    public struct talentRecord {
        public Int32 Id;
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
        public Int32 DependsOn1;
        public Int32 DependsOn2;
        public Int32 DependsOn3;
        public Int32 DependsOnRank1;
        public Int32 DependsOnRank2;
        public Int32 DependsOnRank3;
        public Int32 needAddInSpellBook;
        public Int32 unk0;
        public Int32 allowForPetHigh;
        public Int32 allowForPetLow; }

    public struct talenttabRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 SpellIcon;
        public Int32 Name14;
        public Int32 ClassMask;
        public Int32 PetTalentMask;
        public Int32 TabPage;
        public UInt32 InternalName; } // string

    public struct taxinodesRecord {
        public Int32 Id;
        public Int32 MapId;
        public float X;
        public float Y;
        public float Z;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 MountCreatureId1;
        public Int32 MountCreatureId2; }

    public struct taxipathRecord {
        public Int32 Id;
        public Int32 From;
        public Int32 To;
        public Int32 Price; }

    public struct taxipathnodeRecord {
        public Int32 Id;
        public Int32 PathId;
        public Int32 Index;
        public Int32 MapId;
        public float X;
        public float Y;
        public float Z;
        public Int32 ActionFlag;
        public Int32 Delay;
        public Int32 ArrivalEventId;
        public Int32 DepartureEventId; }

    public struct teamcontributionpointsRecord {
        public Int32 Id;
        public float Value; }

    public struct totemcategoryRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 CategoryType;
        public Int32 CategoryMask; }

    public struct transportanimationRecord {
        public Int32 Id;
        public Int32 TransportEntry;
        public Int32 TimeSeg;
        public float X;
        public float Y;
        public float Z;
        public Int32 MovementId; }

    public struct transportrotationRecord {
        public Int32 Id;
        public Int32 TransportEntry;
        public Int32 TimeSeg;
        public float X;
        public float Y;
        public float Z;
        public float W; }

    public struct vehicleRecord {
        public Int32 Id;
        public Int32 Flags;
        public float TurnSpeed;
        public float PitchSpeed;
        public float PitchMin;
        public float PitchMax;
        public Int32 SeatId1;
        public Int32 SeatId2;
        public Int32 SeatId3;
        public Int32 SeatId4;
        public Int32 SeatId5;
        public Int32 SeatId6;
        public Int32 SeatId7;
        public Int32 SeatId8;
        public float MouseLookOffsetPitch;
        public float CameraFadeDistScalarMin;
        public float CameraFadeDistScalarMax;
        public float CameraPitchOffset;
        public float FacingLimitRight;
        public float FacingLimitLeft;
        public float MSSLTrgtTurnLingering;
        public float MSSLTrgtPitchLingering;
        public float MSSLTrgtMouseLingering;
        public float MSSLTrgtEndOpacity;
        public Int32 MSSLTrgtArcSpeed;
        public float MSSLTrgtArcRepeat;
        public float MSSLTrgtArcWidth;
        public float MSSLTrgtImpactRadius1;
        public float MSSLTrgtImpactRadius2;
        public UInt32 MSSLTrgtArcTexture; // string
        public UInt32 MSSLTrgtImpactTexture; // string
        public UInt32 MSSLTrgtImpactModel1; // string
        public UInt32 MSSLTrgtImpactModel2; // string
        public float CameraYawOffset;
        public Int32 UiLocomotionType;
        public float MSSLTrgtImpactTexRadius;
        public Int32 UiSeatIndicatorType;
        public Int32 PowerDisplayId1;
        public Int32 PowerDisplayId2;
        public Int32 PowerDisplayId3; }

    public struct vehicleseatRecord {
        public Int32 Id;
        public float Flags;
        public Int32 AttachmentOffsetId;
        public float AttachmentOffsetX;
        public float AttachmentOffsetY;
        public float AttachmentOffsetZ;
        public float PreDelay;
        public float EnterSpeed;
        public float EnterGravity;
        public float EnterMinDuration;
        public float EnterMaxDuration;
        public float EnterMinArcHeight;
        public float EnterMaxArcHeight;
        public Int32 EnterAnimStart;
        public Int32 EnterAnimLoop;
        public Int32 RideAnimStart;
        public Int32 RideAnimLoop;
        public Int32 RideUpperAnimStart;
        public Int32 RideupperAnimLoop;
        public float ExitPreDelay;
        public Int32 ExitSpeed;
        public float ExitGravity;
        public float ExitMinDuration;
        public float ExitMaxDuration;
        public float ExitMinArcHeight;
        public Int32 ExitMaxArcHeight;
        public Int32 ExitAnimStart;
        public Int32 ExitAnimLoop;
        public Int32 ExitAnimEnd;
        public float PassengerYaw;
        public float PassengerPitch;
        public float PassengerRoll;
        public Int32 PassengerAttachmentId;
        public Int32 VehicleEnterAnim;
        public Int32 VehicleExitAnim;
        public Int32 VehicleRideAnimLoop;
        public Int32 VehicleRideAnimBone;
        public Int32 VehicleExitAnimBone;
        public Int32 VehicleRideAnimLoopBone;
        public float VehicleEnterAnimDelay;
        public float VehicleExitAnimDelay;
        public Int32 VehicleAbilityDisplay;
        public Int32 EnterUISoundId;
        public float ExitUISoundId;
        public Int32 UiSkin;
        public Int32 FlagsB;
        public float CameraEnteringDelay;
        public float CameraEnteringDuration;
        public Int32 CameraExitingDelay;
        public float CameraExitingDuration;
        public Int32 CameraOffset;
        public Int32 CameraPosChaseRate;
        public float CameraFacingChaseRate;
        public float CameraEnteringZoom;
        public Int32 CameraSeatZoomMin;
        public Int32 CameraSeatZoomMax;
        public Int32 EnterAnimKitId;
        public Int32 RideAnimKitId; }

    public struct wmoareatableRecord {
        public Int32 Id;
        public Int32 RootId;
        public Int32 AdtId;
        public Int32 GroupId;
        public Int32 SoundProviderPref;
        public Int32 SoundProviderPrefUnderwater;
        public Int32 AmbienceId;
        public Int32 ZoneMusic;
        public Int32 IntroSound;
        public Int32 Flags;
        public Int32 AreaId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct worldmapoverlayRecord {
        public Int32 Id;
        public Int32 WorldMapAreaId;
        public Int32 AreaTableId1;
        public Int32 AreaTableId2;
        public Int32 AreaTableId3;
        public Int32 AreaTableId4;
        public Int32 MapPointX;
        public Int32 MapPointY;
        public UInt32 TextureName; // string
        public Int32 TextureWidth;
        public Int32 TextureHeight;
        public Int32 OffsetX;
        public Int32 OffsetY;
        public Int32 HitRectTop;
        public Int32 HitRectLeft;
        public Int32 HitRectBottom;
        public Int32 HitRectRight; }

    public struct worldsafelocsRecord {
        public Int32 Id;
        public Int32 MapId;
        public float X;
        public float Y;
        public float Z;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }
}

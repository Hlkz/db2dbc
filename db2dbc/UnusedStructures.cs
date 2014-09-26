using System;
using System.Runtime.InteropServices;

namespace DBtoDBC
{
    public struct achievement_categoryRecord {
        public Int32 Id;
        public Int32 ParentId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 SortOrder; }

    public struct animationdataRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public Int32 Weaponflags;
        public Int32 Bodyflags;
        public Int32 Flags;
        public Int32 Fallback;
        public Int32 BehaviourId;
        public Int32 BehaviorTier; }

    public struct attackanimkitsRecord {
        public Int32 Id;
        public Int32 Animation;
        public Int32 Type;
        public Int32 Flags;
        public Int32 Unknown; }

    public struct attackanimtypesRecord {
        public Int32 Id;
        public UInt32 Name; } // string

    public struct camerashakesRecord {
        public Int32 Id;
        public Int32 ShakeType;
        public Int32 Direction;
        public Single Amplitude;
        public Single Frequency;
        public Single Duration;
        public Single Phase;
        public Single Coefficient; }

    public struct cfg_categoriesRecord {
        public Int32 Id;
        public Int32 LocaleMask;
        public Int32 CharsetMask;
        public Int32 Flags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct cfg_configsRecord {
        public Int32 Id;
        public Int32 RealmType;
        public Int32 PlayerKillingAllowed;
        public Int32 Roleplaying; }

    public struct characterfacialhairstylesRecord {
        public Int32 Race;
        public Int32 Gender;
        public Int32 SpecificId;
        public Int32 GeosetId1;
        public Int32 GeosetId2;
        public Int32 GeosetId3;
        public Int32 GeosetId4;
        public Int32 GeosetId5; }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct charbaseinfoRecord {
        public Byte Race;
        public Byte Class; }

    public struct charhairgeosetsRecord {
        public Int32 Id;
        public Int32 Race;
        public Int32 Gender;
        public Int32 HairType;
        public Int32 Geoset;
        public Int32 Bald; }

    public struct charhairtexturesRecord {
        public Int32 Id;
        public Int32 Race;
        public Int32 Gender;
        public Int32 Unk1;
        public Int32 Unk2;
        public Int32 Unk3;
        public Int32 Unk4;
        public Int32 Unk5; }

    public struct charsectionsRecord {
        public Int32 Id;
        public Int32 Race;
        public Int32 Gender;
        public Int32 GeneralType;
        public UInt32 Texture1; // string
        public UInt32 Texture2; // string
        public UInt32 Texture3; // string
        public Int32 Flags;
        public Int32 Type;
        public Int32 Variation; }

    public struct chatprofanityRecord {
        public Int32 Id;
        public UInt32 DirtyWord; // string
        public Int32 LanguageId; }

    public struct cinematiccameraRecord {
        public Int32 Id;
        public UInt32 Filepath; // string
        public Int32 Voiceover;
        public Single X;
        public Single Y;
        public Single Z;
        public Single Rotation; }

    public struct creaturemovementinfoRecord {
        public Int32 Id;
        public Single SmoothFacingChaseRate; }

    public struct creaturesounddataRecord {
        public Int32 ID;
        public Int32 Exertion;
        public Int32 ExertionCritical;
        public Int32 Injury;
        public Int32 InjuryCritical;
        public Int32 InjuryCrushingBlow;
        public Int32 Death;
        public Int32 Stun;
        public Int32 Stand;
        public Int32 Footstep;
        public Int32 Aggro;
        public Int32 WingFlap;
        public Int32 WingGlide;
        public Int32 Alert;
        public Int32 Fidget1;
        public Int32 Fidget2;
        public Int32 Fidget3;
        public Int32 Fidget4;
        public Int32 Fidget5;
        public Int32 CustomAttack1;
        public Int32 CustomAttack2;
        public Int32 CustomAttack3;
        public Int32 CustomAttack4;
        public Int32 NPCSoundId;
        public Int32 LoopSoundId;
        public Int32 CreatureImpactType;
        public Int32 JumpStart;
        public Int32 JumpEnd;
        public Int32 PetAttack;
        public Int32 PetOrder;
        public Int32 PetDismiss;
        public Single FidgetDelaySecondsMin;
        public Single FidgetDelaySecondsMax;
        public Int32 Birth;
        public Int32 SpellCastDirected;
        public Int32 Submerge;
        public Int32 Submerged;
        public Int32 CreatureSoundDataIdPet; }

    public struct currencycategoryRecord {
        public Int32 ID;
        public Int32 Flags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct dancemovesRecord {
        public Int32 Id;
        public Int32 Type;
        public Int32 Value;
        public Int32 Fallback;
        public Int32 Racemask;
        public UInt32 Internal; // string
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 LockId; }

    public struct deaththudlookupsRecord {
        public Int32 Id;
        public Int32 Size;
        public Int32 TerrainTypeSound;
        public Int32 SoundId;
        public Int32 SoundIdWater; }

    public struct declinedwordRecord {
        public Int32 Id;
        public UInt32 Word; } // string

    public struct declinedwordcasesRecord {
        public Int32 Id;
        public Int32 Word;
        public Int32 Case;
        public UInt32 DeclinedWord; } // string

    public struct dungeonmapRecord {
        public Int32 Id;
        public Int32 Map;
        public Int32 Layer;
        public Single X;
        public Single Y;
        public Single Z;
        public Single O;
        public Int32 Area; }

    public struct dungeonmapchunkRecord {
        public Int32 Id;
        public Int32 Map;
        public Int32 WMOId;
        public Int32 DungeonMap;
        public Single MinZ; }

    public struct emotestextdataRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Text; }

    public struct emotestextsoundRecord {
        public Int32 Id;
        public Int32 EmoteText;
        public Int32 Race;
        public Int32 Gender;
        public Int32 Sound; }

    public struct environmentaldamageRecord {
        public Int32 Id;
        public Int32 EnumId;
        public Int32 SpellVisual; }

    public struct exhaustionRecord {
        public Int32 Id;
        public Int32 Xp;
        public Single Factor;
        public Single OutdoorHours;
        public Single InnHours;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 Threshold; }

    public struct factiongroupRecord {
        public Int32 Id;
        public Int32 Flags;
        public UInt32 InternalName; // string
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct filedataRecord {
        public Int32 Id;
        public UInt32 FileName; // string
        public UInt32 FilePath; } // string

    public struct footprinttexturesRecord {
        public Int32 Id;
        public UInt32 FileName; } // string

    public struct footstepterrainlookupRecord {
        public Int32 Id;
        public Int32 GroundEffectDoodad;
        public Int32 TerrainType;
        public Int32 SoundDry;
        public Int32 SoundWet; }

    public struct gameobjectartkitRecord {
        public Int32 Id;
        public UInt32 Texture1; // string
        public UInt32 Texture2; // string
        public UInt32 Texture3; // string
        public UInt32 Model1; // string
        public UInt32 Model2; // string
        public UInt32 Model3; // string
        public UInt32 Model4; } // string

    public struct gametablesRecord {
        public UInt32 Name; // string
        public Int32 NumRows;
        public Int32 NumColumns; }

    public struct gametipsRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Tip; }

    public struct gmsurveyanswersRecord {
        public Int32 Id;
        public Int32 SortIndex;
        public Int32 GMSurveyQuestionId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Answer; }

    public struct gmsurveycurrentsurveyRecord {
        public Int32 LangId;
        public Int32 GMSurveyId; }

    public struct gmsurveyquestionsRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Question; }

    public struct gmsurveysurveysRecord {
        public Int32 Id;
        public Int32 RefId1;
        public Int32 RefId2;
        public Int32 RefId3;
        public Int32 RefId4;
        public Int32 RefId5;
        public Int32 RefId6;
        public Int32 RefId7;
        public Int32 RefId8;
        public Int32 RefId9;
        public Int32 RefId10; }

    public struct gmticketcategoryRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct groundeffectdoodadRecord {
        public Int32 Id;
        public UInt32 Model; // string
        public Int32 Flags; }

    public struct groundeffecttextureRecord {
        public Int32 Id;
        public Int32 Doodad1;
        public Int32 Doodad2;
        public Int32 Doodad3;
        public Int32 Doodad4;
        public Int32 Weight1;
        public Int32 Weight2;
        public Int32 Weight3;
        public Int32 Weight4;
        public Int32 Amount;
        public Int32 TerrainType; }
    
    public struct gtbarbershopcostbaseRecord {
        public float Value; }
    public struct gtchancetomeleecritRecord {
        public float Value; }
    public struct gtchancetomeleecritbaseRecord {
        public float Value; }
    public struct gtchancetospellcritRecord {
        public float Value; }
    public struct gtchancetospellcritbaseRecord {
        public float Value; }
    public struct gtcombatratingsRecord {
        public float Value; }
    public struct gtnpcmanacostscalerRecord {
        public float Value; }
    public struct gtoctclasscombatratingscalarRecord {
        public Int32 Id;
        public float Value; }
    public struct gtoctregenhpRecord {
        public float Value; }
    public struct gtoctregenmpRecord {
        public float Value; }
    public struct gtregenhppersptRecord {
        public float Value; }
    public struct gtregenmppersptRecord {
        public float Value; }

    public struct helmetgeosetvisdataRecord {
        public Int32 Id;
        public Int32 HairFlags;
        public Int32 Facial1Flags;
        public Int32 Facial2Flags;
        public Int32 Facial3Flags;
        public Int32 EarsFlags;
        public Int32 Unk1;
        public Int32 Unk2; }

    public struct holidaydescriptionsRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description; }

    public struct holidaynamesRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct itemclassRecord {
        public Int32 Id;
        public Int32 SubClass;
        public Int32 IsWeapon;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct itemcondextcostsRecord {
        public Int32 Id;
        public Int32 Unk;
        public Int32 ExtendedCost;
        public Int32 Unk2; }

    public struct itemdisplayinfoRecord {
        public Int32 Id;
        public UInt32 LeftModel; // string
        public UInt32 RightModel; // string
        public UInt32 LeftModelTexture; // string
        public UInt32 RightModelTexture; // string
        public UInt32 Icon1; // string
        public UInt32 Icon2; // string
        public Int32 GeosetGroup1;
        public Int32 GeosetGroup2;
        public Int32 GeosetGroup3;
        public Int32 Flags;
        public Int32 SpellVisual;
        public Int32 GroupSound;
        public Int32 HelmetGeosetVisMale;
        public Int32 HelmetGeosetVisFemale;
        public UInt32 UpperArmTexture; // string
        public UInt32 LowerArmTexture; // string
        public UInt32 HandsTexture; // string
        public UInt32 UpperTorsoTexture; // string
        public UInt32 LowerTorsoTexture; // string
        public UInt32 UpperLegTexture; // string
        public UInt32 LowerLegTexture; // string
        public UInt32 FootTexture; // string
        public Int32 ItemVisual;
        public Int32 ParticleColorId; }

    public struct itemgroupsoundsRecord {
        public Int32 Id;
        public Int32 Pickup;
        public Int32 Putdown;
        public Int32 Unk1;
        public Int32 Unk2; }

    public struct itempetfoodRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct itempurchasegroupRecord {
        public Int32 Id;
        public Int32 Item1;
        public Int32 Item2;
        public Int32 Item3;
        public Int32 Item4;
        public Int32 Item5;
        public Int32 Item6;
        public Int32 Item7;
        public Int32 Item8;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description; }

    public struct itemsubclassRecord {
        public Int32 ItemClass;
        public Int32 SubClass;
        public Int32 PreRequisiteProficiency;
        public Int32 PostRequisiteProficiency;
        public Int32 Flags;
        public Int32 DisplayFlags;
        public Int32 WeaponParrySeq;
        public Int32 WeaponReadySeq;
        public Int32 WeaponAttackSeq;
        public Int32 WeaponSwingSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] DisplayName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] VerboseName; }

    public struct itemsubclassmaskRecord {
        public Int32 ItemClass;
        public Int32 SubClassMask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct itemvisualeffectsRecord {
        public Int32 Id;
        public UInt32 Model; } // string

    public struct itemvisualsRecord {
        public Int32 Id;
        public Int32 VisualEffect1;
        public Int32 VisualEffect2;
        public Int32 VisualEffect3;
        public Int32 VisualEffect4;
        public Int32 VisualEffect5; }

    public struct languagesRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct languagewordsRecord {
        public Int32 Id;
        public Int32 Language;
        public UInt32 Word; } // string

    public struct lfgdungeonexpansionRecord {
        public Int32 Id;
        public Int32 Dungeon;
        public Int32 Expansion;
        public Int32 RandomId;
        public Int32 HardModeLevelMin;
        public Int32 HardModeLevelMax;
        public Int32 TargetLevelMin;
        public Int32 TargetLevelMax; }

    public struct lfgdungeongroupRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 OrderIndex;
        public Int32 ParentGroup;
        public Int32 TypeId; }

    public struct lightfloatbandRecord {
        public Int32 Id;
        public Int32 UsedValues;
        public Int32 TimeValue1;
        public Int32 TimeValue2;
        public Int32 TimeValue3;
        public Int32 TimeValue4;
        public Int32 TimeValue5;
        public Int32 TimeValue6;
        public Int32 TimeValue7;
        public Int32 TimeValue8;
        public Int32 TimeValue9;
        public Int32 TimeValue10;
        public Int32 TimeValue11;
        public Int32 TimeValue12;
        public Int32 TimeValue13;
        public Int32 TimeValue14;
        public Int32 TimeValue15;
        public Int32 TimeValue16;
        public Single FloatValue1;
        public Single FloatValue2;
        public Single FloatValue3;
        public Single FloatValue4;
        public Single FloatValue5;
        public Single FloatValue6;
        public Single FloatValue7;
        public Single FloatValue8;
        public Single FloatValue9;
        public Single FloatValue10;
        public Single FloatValue11;
        public Single FloatValue12;
        public Single FloatValue13;
        public Single FloatValue14;
        public Single FloatValue15;
        public Single FloatValue16; }

    public struct lightintbandRecord {
        public Int32 Id;
        public Int32 UsedValues;
        public Int32 TimeValue1;
        public Int32 TimeValue2;
        public Int32 TimeValue3;
        public Int32 TimeValue4;
        public Int32 TimeValue5;
        public Int32 TimeValue6;
        public Int32 TimeValue7;
        public Int32 TimeValue8;
        public Int32 TimeValue9;
        public Int32 TimeValue10;
        public Int32 TimeValue11;
        public Int32 TimeValue12;
        public Int32 TimeValue13;
        public Int32 TimeValue14;
        public Int32 TimeValue15;
        public Int32 TimeValue16;
        public Int32 Color1;
        public Int32 Color2;
        public Int32 Color3;
        public Int32 Color4;
        public Int32 Color5;
        public Int32 Color6;
        public Int32 Color7;
        public Int32 Color8;
        public Int32 Color9;
        public Int32 Color10;
        public Int32 Color11;
        public Int32 Color12;
        public Int32 Color13;
        public Int32 Color14;
        public Int32 Color15;
        public Int32 Color16; }

    public struct lightparamsRecord {
        public Int32 Id;
        public Int32 HighLightSky;
        public Int32 SkyBox;
        public Int32 CloudType;
        public Single Glow;
        public Single WaterShallow;
        public Single WaterDeep;
        public Single OceanShallow;
        public Single OceanDeep; }

    public struct lightskyboxRecord {
        public Int32 Id;
        public UInt32 Model; // string
        public Int32 Flags; }

    public struct liquidmaterialRecord {
        public Int32 Id;
        public Int32 LiquidVertexFormat;
        public Int32 IsTransparent; }

    public struct loadingscreensRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public UInt32 Texture; // string
        public Int32 HasWideScreen; }

    public struct loadingscreentaxisplinesRecord {
        public Int32 Id;
        public Int32 TaxiPath;
        public Single X1;
        public Single X2;
        public Single X3;
        public Single X4;
        public Single X5;
        public Single X6;
        public Single X7;
        public Single X8;
        public Single Y1;
        public Single Y2;
        public Single Y3;
        public Single Y4;
        public Single Y5;
        public Single Y6;
        public Single Y7;
        public Single Y8;
        public Int32 LegIndex; }

    public struct locktypeRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] ItemStateName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] ProcessName;
        public UInt32 InternalName; } // string

    public struct materialRecord {
        public Int32 Id;
        public Int32 Flags;
        public Int32 FoleySound;
        public Int32 SheathSound;
        public Int32 UnSeathSound; }

    public struct moviefiledataRecord {
        public Int32 Id;
        public Int32 Resolution; }

    public struct movievariationRecord {
        public Int32 Id;
        public Int32 Movie;
        public Int32 FileData; }

    public struct namegenRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public Int32 Race;
        public Int32 Gender; }

    public struct namesprofanityRecord {
        public Int32 Id;
        public UInt32 Pattern; // string
        public Int32 Language; }

    public struct namesreservedRecord {
        public Int32 Id;
        public UInt32 Pattern; // string
        public Int32 Language; }

    public struct npcsoundsRecord {
        public Int32 Id;
        public Int32 Sound1;
        public Int32 Sound2;
        public Int32 Sound3;
        public Int32 Sound4; }

    public struct objecteffectRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public Int32 Group;
        public Int32 TriggerType;
        public Int32 EventType;
        public Int32 EffectRecType;
        public Int32 EffectRecId;
        public Int32 Attachment;
        public Single OffsetX;
        public Single OffsetY;
        public Single OffsetZ;
        public Int32 ModifierId; }

    public struct objecteffectgroupRecord {
        public Int32 Id;
        public UInt32 Name; } // string

    public struct objecteffectmodifierRecord {
        public Int32 Id;
        public Int32 InputType;
        public Int32 MapType;
        public Int32 OutputType;
        public Single Param1;
        public Single Param2;
        public Single Param3;
        public Single Param4; }

    public struct objecteffectpackageRecord {
        public Int32 Id;
        public UInt32 Name; } // string

    public struct objecteffectpackageelemRecord {
        public Int32 Id;
        public Int32 PackageId;
        public Int32 GroupId;
        public Int32 StateType; }

    public struct packageRecord {
        public Int32 Id;
        public UInt32 Icon; // string
        public Int32 Price;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description; }

    public struct pagetextmaterialRecord {
        public Int32 Id;
        public UInt32 Name; } // string

    public struct paperdollitemframeRecord {
        public UInt32 Slot; // string
        public UInt32 Icon; // string
        public Int32 SlotId; }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct particlecolorRecord {
        public Int32 Id;
        public Byte StartColorRed1;
        public Byte StartColorGreen1;
        public Byte StartColorBlue1;
        public Byte StartColorUnused1;
        public Byte StartColorRed2;
        public Byte StartColorGreen2;
        public Byte StartColorBlue2;
        public Byte StartColorUnused2;
        public Byte StartColorRed3;
        public Byte StartColorGreen3;
        public Byte StartColorBlue3;
        public Byte StartColorUnused3;
        public Byte MidColorRed1;
        public Byte MidColorGreen1;
        public Byte MidColorBlue1;
        public Byte MidColorUnused1;
        public Byte MidColorRed2;
        public Byte MidColorGreen2;
        public Byte MidColorBlue2;
        public Byte MidColorUnused2;
        public Byte MidColorRed3;
        public Byte MidColorGreen3;
        public Byte MidColorBlue3;
        public Byte MidColorUnused3;
        public Byte EndColorRed1;
        public Byte EndColorGreen1;
        public Byte EndColorBlue1;
        public Byte EndColorUnused1;
        public Byte EndColorRed2;
        public Byte EndColorGreen2;
        public Byte EndColorBlue2;
        public Byte EndColorUnused2;
        public Byte EndColorRed3;
        public Byte EndColorGreen3;
        public Byte EndColorBlue3;
        public Byte EndColorUnused3; }

    public struct petitiontypeRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public Int32 Unk; }

    public struct petpersonalityRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 UnhappyLevel;
        public Int32 ContentLevel;
        public Int32 HappyLevel;
        public Single UnhappyDamageModifier;
        public Single ContentDamageModifier;
        public Single HappyDamageModifier; }

    public struct questinfoRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct resistancesRecord {
        public Int32 Id;
        public Int32 Flags;
        public Int32 FizzleSound;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct screeneffectRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public Int32 Type;
        public Int32 Color;
        public Int32 Edge;
        public Int32 BlackWhite;
        public Int32 Unk;
        public Int32 LightParams;
        public Int32 SoundAmbience;
        public Int32 ZoneMusic; }

    public struct servermessagesRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Message; }

    public struct sheathesoundlookupsRecord {
        public Int32 Id;
        public Int32 UnkItemClass;
        public Int32 UnkItemSubClass;
        public Int32 UnkType;
        public Int32 UnkIsWeapon;
        public Int32 SheathSound;
        public Int32 UnSheathSound; }

    public struct skillcostsdataRecord {
        public Int32 Id;
        public Int32 SkillCostsId;
        public Int32 Cost1;
        public Int32 Cost2;
        public Int32 Cost3; }

    public struct skilllinecategoryRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 DisplayOrder; }

    public struct skillraceclassinfoRecord {
        public Int32 Id;
        public Int32 SkillLine;
        public Int32 Race;
        public Int32 Class;
        public Int32 Flags;
        public Int32 RequiredLevel;
        public Int32 SkillTierId;
        public Int32 SkillCostId; }

    public struct skilltiersRecord {
        public Int32 Id;
        public Int32 SkillValue1;
        public Int32 SkillValue2;
        public Int32 SkillValue3;
        public Int32 SkillValue4;
        public Int32 SkillValue5;
        public Int32 SkillValue6;
        public Int32 SkillValue7;
        public Int32 SkillValue8;
        public Int32 SkillValue9;
        public Int32 SkillValue10;
        public Int32 SkillValue11;
        public Int32 SkillValue12;
        public Int32 SkillValue13;
        public Int32 SkillValue14;
        public Int32 SkillValue15;
        public Int32 SkillValue16;
        public Int32 MaxValue1;
        public Int32 MaxValue2;
        public Int32 MaxValue3;
        public Int32 MaxValue4;
        public Int32 MaxValue5;
        public Int32 MaxValue6;
        public Int32 MaxValue7;
        public Int32 MaxValue8;
        public Int32 MaxValue9;
        public Int32 MaxValue0;
        public Int32 MaxValue11;
        public Int32 MaxValue12;
        public Int32 MaxValue13;
        public Int32 MaxValue14;
        public Int32 MaxValue15;
        public Int32 MaxValue16; }

    public struct soundambienceRecord {
        public Int32 Id;
        public Int32 SoundDay;
        public Int32 SoundNight; }

    public struct soundemittersRecord {
        public Int32 Id;
        public Single PositionX;
        public Single PositionY;
        public Single PositionZ;
        public Single DirectionX;
        public Single DirectionY;
        public Single DirectionZ;
        public Int32 Sound;
        public Int32 Map;
        public UInt32 Name; } // string

    public struct soundentriesadvancedRecord {
        public Int32 Id;
        public Int32 Sound;
        public Single InnerRadius;
        public Int32 TimeA;
        public Int32 TimeB;
        public Int32 TimeC;
        public Int32 TimeD;
        public Int32 RandomOffsetRange;
        public Int32 Usage;
        public Int32 IntervalMin;
        public Int32 IntervalMax;
        public Int32 VolumeSliderCategory;
        public Single DuckToSFX;
        public Single DuckToMusic;
        public Single DuckToAmbience;
        public Single InnerRadiusOfInfluence;
        public Single OuterRadiusOfInfluence;
        public Int32 TimeToDuck;
        public Int32 TimeToUnduck;
        public Single InsideAngle;
        public Single OutsideAngle;
        public Single OutsideVolume;
        public Single OuterRadius2D;
        public UInt32 Name; } // string

    public struct soundfilterRecord {
        public Int32 Id;
        public UInt32 Name; } // string

    public struct soundfilterelemRecord {
        public Int32 Id;
        public Int32 SoundFilter;
        public Int32 OrderIndex;
        public Int32 FilterType;
        public Int32 True;
        public Single Parameter1;
        public Single Parameter2;
        public Single Parameter3;
        public Single Parameter4;
        public Single Parameter5;
        public Single Parameter6;
        public Single Parameter7;
        public Single Parameter8; }

    public struct soundproviderpreferencesRecord {
        public Int32 Id;
        public UInt32 Description; // string
        public Int32 Flags;
        public Int32 EAXEnvSelection;
        public Single EAXDecayTime;
        public Single EAX2EnvSize;
        public Single EAX2EnvDiffusion;
        public Int32 EAX2Room;
        public Int32 EAX2RoomHF;
        public Single EAX2DecayHFRatio;
        public Int32 EAX2Reflections;
        public Single EAX2ReflectionsDelay;
        public Int32 EAX2Reverb;
        public Single EAX2ReverbDelay;
        public Single EAX2RoomRolloff;
        public Int32 EAX2AirAbsorption;
        public Int32 EAX3RoomLF;
        public Single EAX3DecayLFRatio;
        public Single EAX3EchoTime;
        public Single EAX3EchoDepth;
        public Single EAX3ModulationTime;
        public Single EAX3ModulationDepth;
        public Single EAX3HFReference;
        public Single EAX3LFReference; }

    public struct soundsamplepreferencesRecord {
        public Int32 Id;
        public Int32 Unk1;
        public Int32 Unk2;
        public Int32 Unk3;
        public Int32 Unk4;
        public Int32 Unk5;
        public Single Unk6;
        public Int32 Unk7;
        public Single Unk8;
        public Single Unk9;
        public Int32 Unk10;
        public Single Unk11;
        public Int32 Unk12;
        public Single Unk13;
        public Single Unk14;
        public Single Unk15;
        public Int32 Unk16; }

    public struct soundwatertypeRecord {
        public Int32 Id;
        public Int32 LiquidType;
        public Int32 FluidSpeed;
        public Int32 Sound; }

    public struct spammessagesRecord {
        public Int32 Id;
        public UInt32 RegEx; } // string
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct spellchaineffectsRecord {
        public Int32 ID;
        public float AvgSegLen;
        public float Width;
        public float NoiseScale;
        public float TexCoordScale;
        public Int32 SegDuration;
        public Int32 SegDelay;
        public UInt32 Texture; // string
        public Int32 Flags;
        public Int32 JointCount;
        public float JointOffsetRadius;
        public Int32 JointsPerMinorJoint;
        public Int32 MinorJointsPerMajorJoint;
        public float MinorJointScale;
        public float MajorJointScale;
        public float JointMoveSpeed;
        public float JointSmoothness;
        public float MinDurationBetweenJointJumps;
        public float MaxDurationBetweenJointJumps;
        public float WaveHeight;
        public float WaveFreq;
        public float WaveSpeed;
        public float MinWaveAngle;
        public float MaxWaveAngle;
        public float MinWaveSpin;
        public float MaxWaveSpin;
        public float ArcHeight;
        public float MinArcAngle;
        public float MaxArcAngle;
        public float MinArcSpin;
        public float MaxArcSpin;
        public float DelayBetweenEffects;
        public float MinFlickerOnDuration;
        public float MaxFlickerOnDuration;
        public float MinFlickerOffDuration;
        public float MaxFlickerOffDuration;
        public float PulseSpeed;
        public float PulseOnLength;
        public float PulseFadeLength;
        public Byte Alpha;
        public Byte Red;
        public Byte Green;
        public Byte Blue;
        public Byte BlendMode;
        public Int32 Combo;
        public Int32 RenderLayer;
        public float TextureLength;
        public float WavePhase; }

    public struct spelldescriptionvariablesRecord {
        public Int32 Id;
        public UInt32 Text; } // string

    public struct spelldispeltypeRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name;
        public Int32 Mask;
        public Int32 ImmunityPossible;
        public UInt32 InternalName; } // string

    public struct spelleffectcamerashakesRecord {
        public Int32 Id;
        public Int32 CameraShakes1;
        public Int32 CameraShakes2;
        public Int32 CameraShakes3; }

    public struct spelliconRecord {
        public Int32 Id;
        public UInt32 Name; } // string

    public struct spellmechanicRecord {
        public Int32 Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Name; }

    public struct spellmissileRecord {
        public Int32 Id;
        public Int32 Flags;
        public Single DefaultPitchMin;
        public Single DefaultPitchMax;
        public Single DefaultSpeedMin;
        public Single DefaultSpeedMax;
        public Single RandomizeFacingMin;
        public Single RandomizeFacingMax;
        public Single RandomizePitchMin;
        public Single RandomizePitchMax;
        public Single RandomizeSpeedMin;
        public Single RandomizeSpeedMax;
        public Single Gravity;
        public Single MaxDuration;
        public Single CollisionRadius; }

    public struct spellmissilemotionRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public UInt32 ScriptBody; // string
        public Int32 Flags;
        public Int32 MissileCount; }

    public struct spellvisualRecord {
        public Int32 Id;
        public Int32 PrecastKit;
        public Int32 CastKit;
        public Int32 ImpactKit;
        public Int32 StateKit;
        public Int32 StateDoneKit;
        public Int32 ChannelKit;
        public Int32 HasMissile;
        public Int32 MissileModel;
        public Int32 MissilePathType;
        public Int32 MissileDestinationAttachment;
        public Int32 MissileSound;
        public Int32 AnimEventSound;
        public Int32 Flags;
        public Int32 CasterImpactKit;
        public Int32 TargetImpactKit;
        public Int32 MissileAttachment;
        public Int32 MissileFollowGroundHeight;
        public Int32 MissileFollowGroundDropSpeed;
        public Int32 MissileFollowGroundApproach;
        public Int32 MissileFollowGroundFlags;
        public Int32 MissileMotion;
        public Int32 MissileTargetingKit;
        public Int32 InstantAreaKit;
        public Int32 ImpactAreaKit;
        public Int32 PersistentAreaKit;
        public Single MissileCastOffset1;
        public Single MissileCastOffset2;
        public Single MissileCastOffset3;
        public Single MissileImpactOffset1;
        public Single MissileImpactOffset2;
        public Single MissileImpactOffset3; }

    public struct spellvisualeffectnameRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public UInt32 Model; // string
        public Single AreaEffectSize;
        public Single Scale;
        public Single MinAllowedScale;
        public Single MaxAllowedScale; }

    public struct spellvisualkitRecord {
        public Int32 Id;
        public Int32 StartAnimId;
        public Int32 AnimId;
        public Int32 AnimKitId;
        public Int32 HeadEffect;
        public Int32 ChestEffect;
        public Int32 BaseEffect;
        public Int32 LeftHandEffect;
        public Int32 RightHandEffect;
        public Int32 BreathEffect;
        public Int32 LeftWeaponEffect;
        public Int32 RightWeaponEffect;
        public Int32 SpecialEffect;
        public Int32 WorldEffect;
        public Int32 Sound;
        public Int32 Shake;
        public Int32 CharProc;
        public Int32 Unk1;
        public Int32 Unk2;
        public Int32 Unk3;
        public Int32 Unk4;
        public Int32 Unk5;
        public Int32 Unk6;
        public Int32 Unk7;
        public Single Unk8;
        public Single Unk9;
        public Single Unk10;
        public Single Unk11;
        public Single Unk12;
        public Single Unk13;
        public Single Unk14;
        public Single Unk15;
        public Single Unk16;
        public Single Unk17;
        public Single Unk18;
        public Single Unk19;
        public Single Unk20;
        public Int32 Flags; }

    public struct spellvisualkitareamodelRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public Int32 EnumId; }

    public struct spellvisualkitmodelattachRecord {
        public Int32 Id;
        public Int32 ParentSpellVisualKit;
        public Int32 VisualEffectName;
        public Int32 AttachmentId;
        public Single OffsetX;
        public Single OffsetY;
        public Single OffsetZ;
        public Single Yaw;
        public Single Pitch;
        public Single Roll; }

    public struct spellvisualprecasttransitionsRecord {
        public Int32 Id;
        public UInt32 Load; // string
        public UInt32 Hold; } // string

    public struct startup_stringsRecord {
        public Int32 Id;
        public UInt32 InternalName; // string
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Text; }

    public struct stationeryRecord {
        public Int32 Id;
        public Int32 Item;
        public UInt32 Texture; // string
        public Int32 Flags; }

    public struct stringlookupsRecord {
        public Int32 Id;
        public UInt32 Model; } // string

    public struct terraintypeRecord {
        public Int32 Id;
        public UInt32 Description; // string
        public Int32 FootstepSprayRun;
        public Int32 FootstepSprayWalk;
        public Int32 Sound;
        public Int32 Flags; }
    
    public struct terraintypesoundsRecord {
        public Int32 Id; }

    public struct transportphysicsRecord {
        public Int32 Id;
        public Single WaveAmp;
        public Single WaveTimeScale;
        public Single RollAmp;
        public Single RollTimeScale;
        public Single PitchAmp;
        public Single PitchTimeScale;
        public Single MaxBank;
        public Single MaxBankTurnSpeed;
        public Single SpeedDampThresh;
        public Single SpeedDamp; }

    public struct uisoundlookupsRecord {
        public Int32 Id;
        public Int32 Sound;
        public UInt32 InternalName; } // string

    public struct unitbloodRecord {
        public Int32 Id;
        public Int32 CombatBloodSpurtFront1;
        public Int32 CombatBloodSpurtFront2;
        public Int32 CombatBloodSpurtBack1;
        public Int32 CombatBloodSpurtBack2;
        public UInt32 GroundBlood1; // string
        public UInt32 GroundBlood2; // string
        public UInt32 GroundBlood3; // string
        public UInt32 GroundBlood4; // string
        public UInt32 GroundBlood5; } // string

    public struct unitbloodlevelsRecord {
        public Int32 Id;
        public Int32 ViolenceLevel1;
        public Int32 ViolenceLevel2;
        public Int32 ViolenceLevel3; }

    public struct vehicleuiindicatorRecord {
        public Int32 Id;
        public UInt32 Texture; } // string

    public struct vehicleuiindseatRecord {
        public Int32 Id;
        public Int32 VehicleUIIndicator;
        public Int32 VirtualSeatIndex;
        public Single X;
        public Single Y; }

    public struct videohardwareRecord {
        public Int32 Id;
        public Int32 Vendor;
        public Int32 Device;
        public Int32 FarclipIdx;
        public Int32 TerrainLODDistIdx;
        public Int32 TerrainShadowLOD;
        public Int32 DetailDoodadDensityIdx;
        public Int32 DetailDoodadAlpha;
        public Int32 AnimatingDoodadIdx;
        public Int32 Trilinear;
        public Int32 NumLights;
        public Int32 Specularity;
        public Int32 WaterLODIdx;
        public Int32 ParticleDensityIdx;
        public Int32 UnitDrawDistIdx;
        public Int32 SmallCullDistIdx;
        public Int32 ResolutionIdx;
        public Int32 BaseMipLevel;
        public Int32 OglOverrides;
        public Int32 D3DOverrides;
        public Int32 FixLag;
        public Int32 Multisample;
        public Int32 AtlasDisable; }

    public struct vocaluisoundsRecord {
        public Int32 Id;
        public Int32 VocalUIEnum;
        public Int32 Race;
        public Int32 NormalSound1;
        public Int32 NormalSound2;
        public Int32 PissedSound1;
        public Int32 PissedSound2; }

    public struct weaponimpactsoundsRecord {
        public Int32 Id;
        public Int32 WeaponSubClass;
        public Int32 ParrySoundType;
        public Int32 ImpactSound1;
        public Int32 ImpactSound2;
        public Int32 ImpactSound3;
        public Int32 ImpactSound4;
        public Int32 ImpactSound5;
        public Int32 ImpactSound6;
        public Int32 ImpactSound7;
        public Int32 ImpactSound8;
        public Int32 ImpactSound9;
        public Int32 ImpactSound10;
        public Int32 CritImpactSound1;
        public Int32 CritImpactSound2;
        public Int32 CritImpactSound3;
        public Int32 CritImpactSound4;
        public Int32 CritImpactSound5;
        public Int32 CritImpactSound6;
        public Int32 CritImpactSound7;
        public Int32 CritImpactSound8;
        public Int32 CritImpactSound9;
        public Int32 CritImpactSound10; }

    public struct weaponswingsounds2Record {
        public Int32 Id;
        public Int32 Weight;
        public Int32 Critical;
        public Int32 Sound; }

    public struct weatherRecord {
        public Int32 Id;
        public Int32 AmbienceId;
        public Int32 EffectType;
        public Single EffectColor1;
        public Single EffectColor2;
        public Single EffectColor3;
        public Single EffectColor4;
        public UInt32 Texture; } // string

    public struct worldchunksoundsRecord {
        public Int32 Map;
        public Int32 ChunkX;
        public Int32 ChunkY;
        public Int32 SubChunkX;
        public Int32 SubChunkY;
        public Int32 ZoneIntroMusic;
        public Int32 ZoneMusic;
        public Int32 SoundAmbience;
        public Int32 SoundProviderPreference; }

    public struct worldmapareaRecord {
        public Int32 Id;
        public Int32 Map;
        public Int32 Area;
        public UInt32 InternalName; // string
        public Single LocLeft;
        public Single LocRight;
        public Single LocTop;
        public Single LocBottom;
        public Int32 DisplayMap;
        public Int32 DefaultDungeonFloor;
        public Int32 ParentWorldMap; }

    public struct worldmapcontinentRecord {
        public Int32 Id;
        public Int32 Map;
        public Int32 LeftBoundary;
        public Int32 RightBoundary;
        public Int32 TopBoundary;
        public Int32 BottomBoundary;
        public Single ContinentOffsetX;
        public Single ContinentOffsetY;
        public Single Scale;
        public Single TaxiMinX;
        public Single TaxiMinY;
        public Single TaxiMaxX;
        public Single TaxiMaxY;
        public Int32 WorldMap; }

    public struct worldmaptransformsRecord {
        public Int32 Id;
        public Int32 Map;
        public Single RegionMinX;
        public Single RegionMinY;
        public Single RegionMaxX;
        public Single RegionMaxY;
        public Int32 DestinationMap;
        public Single RegionOffsetX;
        public Single RegionOffsetY;
        public Int32 DestinationArea; }

    public struct worldstateuiRecord {
        public Int32 Id;
        public Int32 Map;
        public Int32 Zone;
        public Int32 PhaseShift;
        public UInt32 Icon; // string
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Text;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Description;
        public Int32 StateVariable;
        public Int32 Type;
        public UInt32 DynamicIcon; // string
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] DynamicTooltip;
        public Int32 ExtendedUI;
        public Int32 ExtendedUIStateVariable1;
        public Int32 ExtendedUIStateVariable2;
        public Int32 ExtendedUIStateVariable3; }

    public struct worldstatezonesoundsRecord {
        public Int32 Id;
        public Int32 Value;
        public Int32 Area;
        public Int32 WMOArea;
        public Int32 IntroMusic;
        public Int32 Music;
        public Int32 SoundAmbience;
        public Int32 Preferences; }

    public struct wowerror_stringsRecord {
        public Int32 Id;
        public UInt32 Name; // string
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public UInt32[] Text; }

    public struct zoneintromusictableRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public Int32 Sound;
        public Int32 Priority;
        public Int32 MinDelayMinutes; }

    public struct zonemusicRecord {
        public Int32 Id;
        public UInt32 Name; // string
        public Int32 SilenceIntervalMinDay;
        public Int32 SilenceIntervalMinNight;
        public Int32 SilenceIntervalMaxDay;
        public Int32 SilenceIntervalMaxNight;
        public Int32 DayMusic;
        public Int32 NightMusic; }
}

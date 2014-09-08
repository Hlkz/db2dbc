using System;

namespace DBtoDBC
{
    public struct achievement_criteriaMap {
        public achievement_criteriaRecord record;
        public string[] Description; }

    public struct achievementMap {
        public achievementRecord record;
        public string[] Name;
        public string[] Description;
        public string[] Reward; }

    public struct areagroupMap {
        public areagroupRecord record; };

    public struct areapoiMap {
        public areapoiRecord record;
        public string[] Name;
        public string[] Description; }

    public struct areatableMap {
        public areatableRecord record;
        public string[] Name; }

    public struct areatriggerMap {
        public areatriggerRecord record; };

    public struct auctionhouseMap {
        public auctionhouseRecord record;
        public string[] Name; }

    public struct bankbagslotpricesMap {
        public bankbagslotpricesRecord record; };

    public struct bannedaddonsMap {
        public bannedaddonsRecord record; };

    public struct barbershopstyleMap {
        public barbershopstyleRecord record;
        public string[] Name; }

    public struct battlemasterlistMap {
        public battlemasterlistRecord record;
        public string[] Name; }

    public struct charstartoutfitMap {
        public charstartoutfitRecord record; };

    public struct chartitlesMap {
        public chartitlesRecord record;
        public string[] Male;
        public string[] Female; }

    public struct chatchannelsMap {
        public chatchannelsRecord record;
        public string[] Pattern;
        public string[] Name; }

    public struct chrclassesMap {
        public chrclassesRecord record;
        public string[] Name;
        public string[] Female;
        public string[] Male; }

    public struct chrracesMap {
        public chrracesRecord record;
        public string[] Name;
        public string[] NameFemale;
        public string[] NameMale; }

    public struct cinematicsequencesMap {
        public cinematicsequencesRecord record; };

    public struct creaturedisplayinfoMap {
        public creaturedisplayinfoRecord record; };

    public struct creaturedisplayinfoextraMap {
        public creaturedisplayinfoextraRecord record; };

    public struct creaturefamilyMap {
        public creaturefamilyRecord record;
        public string[] Name; }

    public struct creaturemodeldataMap {
        public creaturemodeldataRecord record; };

    public struct creaturespelldataMap {
        public creaturespelldataRecord record; };

    public struct creaturetypeMap {
        public creaturetypeRecord record;
        public string[] Name; }

    public struct currencytypesMap {
        public currencytypesRecord record; };

    public struct destructiblemodeldataMap {
        public destructiblemodeldataRecord record; };

    public struct dungeonencounterMap {
        public dungeonencounterRecord record;
        public string[] EncounterName; }

    public struct durabilitycostsMap {
        public durabilitycostsRecord record; };

    public struct durabilityqualityMap {
        public durabilityqualityRecord record; };

    public struct emotesMap {
        public emotesRecord record; };

    public struct emotestextMap {
        public emotestextRecord record; };

    public struct factionMap {
        public factionRecord record;
        public string[] Name;
        public string[] Description; }

    public struct factiontemplateMap {
        public factiontemplateRecord record; };

    public struct gameobjectdisplayinfoMap {
        public gameobjectdisplayinfoRecord record; };

    public struct gempropertiesMap {
        public gempropertiesRecord record; };

    public struct glyphpropertiesMap {
        public glyphpropertiesRecord record; };

    public struct glyphslotMap {
        public glyphslotRecord record; };

    public struct holidaysMap {
        public holidaysRecord record; };

    public struct itembagfamilyMap {
        public itembagfamilyRecord record;
        public string[] Name; }

    public struct itemextendedcostMap {
        public itemextendedcostRecord record; };

    public struct itemlimitcategoryMap {
        public itemlimitcategoryRecord record;
        public string[] Name; }

    public struct itemrandompropertiesMap {
        public itemrandompropertiesRecord record;
        public string[] Name; }

    public struct itemrandomsuffixMap {
        public itemrandomsuffixRecord record;
        public string[] Name; }

    public struct itemsetMap {
        public itemsetRecord record;
        public string[] Name; }

    public struct lfgdungeonsMap {
        public lfgdungeonsRecord record;
        public string[] Name;
        public string[] Description; }

    public struct lightMap {
        public lightRecord record; };

    public struct liquidtypeMap {
        public liquidtypeRecord record; };

    public struct lockMap {
        public lockRecord record; };

    public struct mailtemplateMap {
        public mailtemplateRecord record;
        public string[] Subject;
        public string[] Content; }

    public struct mapMap {
        public mapRecord record;
        public string[] Name;
        public string[] HordeIntro;
        public string[] AllianceIntro; }

    public struct mapdifficultyMap {
        public mapdifficultyRecord record;
        public string[] AreaTriggerText; }

    public struct movieMap {
        public movieRecord record; };

    public struct overridespelldataMap {
        public overridespelldataRecord record; };

    public struct powerdisplayMap {
        public powerdisplayRecord record; };

    public struct pvpdifficultyMap {
        public pvpdifficultyRecord record; };

    public struct questfactionrewMap {
        public questfactionrewRecord record; };

    public struct questsortMap {
        public questsortRecord record;
        public string[] Name; }

    public struct questxpMap {
        public questxpRecord record; };

    public struct randproppointsMap {
        public randproppointsRecord record; };

    public struct scalingstatdistributionMap {
        public scalingstatdistributionRecord record; };

    public struct scalingstatvaluesMap {
        public scalingstatvaluesRecord record; };

    public struct skilllineabilityMap {
        public skilllineabilityRecord record; };

    public struct skilllineMap {
        public skilllineRecord record;
        public string[] Name;
        public string[] Description;
        public string[] AlternateVerb; }

    public struct soundentriesMap {
        public soundentriesRecord record; };

    public struct spellcasttimesMap {
        public spellcasttimesRecord record; };

    public struct spellcategoryMap {
        public spellcategoryRecord record; };

    public struct spellMap {
        public spellRecord record;
        public string[] Name;
        public string[] Rank;
        public string[] Description;
        public string[] Tooltip; }

    public struct spelldifficultyMap {
        public spelldifficultyRecord record; };

    public struct spelldurationMap {
        public spelldurationRecord record; };

    public struct spellfocusobjectMap {
        public spellfocusobjectRecord record;
        public string[] Name; }

    public struct spellitemenchantmentconditionMap {
        public spellitemenchantmentconditionRecord record; };

    public struct spellitemenchantmentMap {
        public spellitemenchantmentRecord record;
        public string[] Description; }

    public struct spellradiusMap {
        public spellradiusRecord record; };

    public struct spellrangeMap {
        public spellrangeRecord record;
        public string[] Name;
        public string[] Name2; }

    public struct spellrunecostMap {
        public spellrunecostRecord record; };

    public struct spellshapesshiftMap {
        public spellshapesshiftRecord record;
        public string[] Name; }

    public struct stableslotpricesMap {
        public stableslotpricesRecord record; };

    public struct summonpropertiesMap {
        public summonpropertiesRecord record; };

    public struct talentMap {
        public talentRecord record; };

    public struct talenttabMap {
        public talenttabRecord record;
        public string[] Name; }

    public struct taxinodesMap {
        public taxinodesRecord record;
        public string[] Name; }

    public struct taxipathMap {
        public taxipathRecord record; };

    public struct taxipathnodeMap {
        public taxipathnodeRecord record; };

    public struct teamcontributionpointsMap {
        public teamcontributionpointsRecord record; };

    public struct totemcategoryMap {
        public totemcategoryRecord record;
        public string[] Name; }

    public struct transportanimationMap {
        public transportanimationRecord record; };

    public struct transportrotationMap {
        public transportrotationRecord record; };

    public struct vehicleMap {
        public vehicleRecord record; };

    public struct vehicleseatMap {
        public vehicleseatRecord record; };

    public struct wmoareatableMap {
        public wmoareatableRecord record;
        public string[] Name; }

    public struct worldmapoverlayMap {
        public worldmapoverlayRecord record; };

    public struct worldsafelocsMap {
        public worldsafelocsRecord record;
        public string[] Name; }
}

using System;

namespace DBtoDBC
{
    public struct DBCHeader {
        public UInt32 magic;
        public UInt32 record_count;
        public UInt32 field_count;
        public UInt32 record_size;
        public Int32 string_block_size; };

    public struct achievement_criteriaBody {
        public achievement_criteriaMap[] records; };

    public struct achievementBody {
        public achievementMap[] records; };

    public struct areagroupBody {
        public areagroupMap[] records; };

    public struct areapoiBody {
        public areapoiMap[] records; };

    public struct areatableBody {
        public areatableMap[] records; };

    public struct areatriggerBody {
        public areatriggerMap[] records; };

    public struct auctionhouseBody {
        public auctionhouseMap[] records; };

    public struct bankbagslotpricesBody {
        public bankbagslotpricesMap[] records; };

    public struct bannedaddonsBody {
        public bannedaddonsMap[] records; };

    public struct barbershopstyleBody {
        public barbershopstyleMap[] records; };

    public struct battlemasterlistBody {
        public battlemasterlistMap[] records; };

    public struct charstartoutfitBody {
        public charstartoutfitMap[] records; };

    public struct chartitlesBody {
        public chartitlesMap[] records; };

    public struct chatchannelsBody {
        public chatchannelsMap[] records; };

    public struct chrclassesBody {
        public chrclassesMap[] records; };

    public struct chrracesBody {
        public chrracesMap[] records; };

    public struct cinematicsequencesBody {
        public cinematicsequencesMap[] records; };

    public struct creaturedisplayinfoBody {
        public creaturedisplayinfoMap[] records; };

    public struct creaturedisplayinfoextraBody {
        public creaturedisplayinfoextraMap[] records; };

    public struct creaturefamilyBody {
        public creaturefamilyMap[] records; };

    public struct creaturemodeldataBody {
        public creaturemodeldataMap[] records; };

    public struct creaturespelldataBody {
        public creaturespelldataMap[] records; };

    public struct creaturetypeBody {
        public creaturetypeMap[] records; };

    public struct currencytypesBody {
        public currencytypesMap[] records; };

    public struct destructiblemodeldataBody {
        public destructiblemodeldataMap[] records; };

    public struct dungeonencounterBody {
        public dungeonencounterMap[] records; };

    public struct durabilitycostsBody {
        public durabilitycostsMap[] records; };

    public struct durabilityqualityBody {
        public durabilityqualityMap[] records; };

    public struct emotesBody {
        public emotesMap[] records; };

    public struct emotestextBody {
        public emotestextMap[] records; };

    public struct factionBody {
        public factionMap[] records; };

    public struct factiontemplateBody {
        public factiontemplateMap[] records; };

    public struct gameobjectdisplayinfoBody {
        public gameobjectdisplayinfoMap[] records; };

    public struct gempropertiesBody {
        public gempropertiesMap[] records; };

    public struct glyphpropertiesBody {
        public glyphpropertiesMap[] records; };

    public struct glyphslotBody {
        public glyphslotMap[] records; };

    public struct holidaysBody {
        public holidaysMap[] records; };

    public struct itembagfamilyBody {
        public itembagfamilyMap[] records; };

    public struct itemextendedcostBody {
        public itemextendedcostMap[] records; };

    public struct itemlimitcategoryBody {
        public itemlimitcategoryMap[] records; };

    public struct itemrandompropertiesBody {
        public itemrandompropertiesMap[] records; };

    public struct itemrandomsuffixBody {
        public itemrandomsuffixMap[] records; };

    public struct itemsetBody {
        public itemsetMap[] records; };

    public struct lfgdungeonsBody {
        public lfgdungeonsMap[] records; };

    public struct lightBody {
        public lightMap[] records; };

    public struct liquidtypeBody {
        public liquidtypeMap[] records; };

    public struct lockBody {
        public lockMap[] records; };

    public struct mailtemplateBody {
        public mailtemplateMap[] records; };

    public struct mapBody {
        public mapMap[] records; };

    public struct mapdifficultyBody {
        public mapdifficultyMap[] records; };

    public struct movieBody {
        public movieMap[] records; };

    public struct overridespelldataBody {
        public overridespelldataMap[] records; };

    public struct powerdisplayBody {
        public powerdisplayMap[] records; };

    public struct pvpdifficultyBody {
        public pvpdifficultyMap[] records; };

    public struct questfactionrewBody {
        public questfactionrewMap[] records; };

    public struct questsortBody {
        public questsortMap[] records; };

    public struct questxpBody {
        public questxpMap[] records; };

    public struct randproppointsBody {
        public randproppointsMap[] records; };

    public struct scalingstatdistributionBody {
        public scalingstatdistributionMap[] records; };

    public struct scalingstatvaluesBody {
        public scalingstatvaluesMap[] records; };

    public struct skilllineabilityBody {
        public skilllineabilityMap[] records; };

    public struct skilllineBody {
        public skilllineMap[] records; };

    public struct soundentriesBody {
        public soundentriesMap[] records; };

    public struct spellcasttimesBody {
        public spellcasttimesMap[] records; };

    public struct spellcategoryBody {
        public spellcategoryMap[] records; };

    public struct spellBody {
        public spellMap[] records; };

    public struct spelldifficultyBody {
        public spelldifficultyMap[] records; };

    public struct spelldurationBody {
        public spelldurationMap[] records; };

    public struct spellfocusobjectBody {
        public spellfocusobjectMap[] records; };

    public struct spellitemenchantmentconditionBody {
        public spellitemenchantmentconditionMap[] records; };

    public struct spellitemenchantmentBody {
        public spellitemenchantmentMap[] records; };

    public struct spellradiusBody {
        public spellradiusMap[] records; };

    public struct spellrangeBody {
        public spellrangeMap[] records; };

    public struct spellrunecostBody {
        public spellrunecostMap[] records; };

    public struct spellshapesshiftBody {
        public spellshapesshiftMap[] records; };

    public struct stableslotpricesBody {
        public stableslotpricesMap[] records; };

    public struct summonpropertiesBody {
        public summonpropertiesMap[] records; };

    public struct talentBody {
        public talentMap[] records; };

    public struct talenttabBody {
        public talenttabMap[] records; };

    public struct taxinodesBody {
        public taxinodesMap[] records; };

    public struct taxipathBody {
        public taxipathMap[] records; };

    public struct taxipathnodeBody {
        public taxipathnodeMap[] records; };

    public struct teamcontributionpointsBody {
        public teamcontributionpointsMap[] records; };

    public struct totemcategoryBody {
        public totemcategoryMap[] records; };

    public struct transportanimationBody {
        public transportanimationMap[] records; };

    public struct transportrotationBody {
        public transportrotationMap[] records; };

    public struct vehicleBody {
        public vehicleMap[] records; };

    public struct vehicleseatBody {
        public vehicleseatMap[] records; };

    public struct wmoareatableBody {
        public wmoareatableMap[] records; };

    public struct worldmapoverlayBody {
        public worldmapoverlayMap[] records; };

    public struct worldsafelocsBody {
        public worldsafelocsMap[] records; };

}
using System;
using MySql.Data.MySqlClient;

namespace DBtoDBC
{
    class DBCExtract
    {
        public static void achievement_criteria(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            achievement_criteriadbc achievement_criteria = new achievement_criteriadbc();
            achievement_criteria.LoadDB(connection);
            achievement_criteria.SaveDBC("D:/A/DBC/temp/DBFilesClient/Achievement_Criteria.dbc"); }

        public static void achievement(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            achievementdbc achievement = new achievementdbc();
            achievement.LoadDB(connection);
            achievement.SaveDBC("D:/A/DBC/temp/DBFilesClient/Achievement.dbc"); }

        public static void areagroup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            areagroupdbc areagroup = new areagroupdbc();
            areagroup.LoadDB(connection);
            areagroup.SaveDBC("D:/A/DBC/temp/DBFilesClient/AreaGroup.dbc"); }

        public static void areapoi(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            areapoidbc areapoi = new areapoidbc();
            areapoi.LoadDB(connection);
            areapoi.SaveDBC("D:/A/DBC/temp/DBFilesClient/AreaPOI.dbc"); }

        public static void areatable(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            areatabledbc areatable = new areatabledbc();
            areatable.LoadDB(connection);
            areatable.SaveDBC("D:/A/DBC/temp/DBFilesClient/AreaTable.dbc"); }

        public static void areatrigger(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            areatriggerdbc areatrigger = new areatriggerdbc();
            areatrigger.LoadDB(connection);
            areatrigger.SaveDBC("D:/A/DBC/temp/DBFilesClient/AreaTrigger.dbc"); }

        public static void auctionhouse(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            auctionhousedbc auctionhouse = new auctionhousedbc();
            auctionhouse.LoadDB(connection);
            auctionhouse.SaveDBC("D:/A/DBC/temp/DBFilesClient/AuctionHouse.dbc"); }

        public static void bankbagslotprices(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            bankbagslotpricesdbc bankbagslotprices = new bankbagslotpricesdbc();
            bankbagslotprices.LoadDB(connection);
            bankbagslotprices.SaveDBC("D:/A/DBC/temp/DBFilesClient/BankBagSlotPrices.dbc"); }

        public static void bannedaddons(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            bannedaddonsdbc bannedaddons = new bannedaddonsdbc();
            bannedaddons.LoadDB(connection);
            bannedaddons.SaveDBC("D:/A/DBC/temp/DBFilesClient/BannedAddOns.dbc"); }

        public static void barbershopstyle(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            barbershopstyledbc barbershopstyle = new barbershopstyledbc();
            barbershopstyle.LoadDB(connection);
            barbershopstyle.SaveDBC("D:/A/DBC/temp/DBFilesClient/BarberShopStyle.dbc"); }

        public static void battlemasterlist(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            battlemasterlistdbc battlemasterlist = new battlemasterlistdbc();
            battlemasterlist.LoadDB(connection);
            battlemasterlist.SaveDBC("D:/A/DBC/temp/DBFilesClient/BattlemasterList.dbc"); }

        public static void chartitles(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            chartitlesdbc chartitles = new chartitlesdbc();
            chartitles.LoadDB(connection);
            chartitles.SaveDBC("D:/A/DBC/temp/DBFilesClient/CharTitles.dbc"); }

        public static void chatchannels(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            chatchannelsdbc chatchannels = new chatchannelsdbc();
            chatchannels.LoadDB(connection);
            chatchannels.SaveDBC("D:/A/DBC/temp/DBFilesClient/ChatChannels.dbc"); }

        public static void chrclasses(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            chrclassesdbc chrclasses = new chrclassesdbc();
            chrclasses.LoadDB(connection);
            chrclasses.SaveDBC("D:/A/DBC/temp/DBFilesClient/ChrClasses.dbc"); }

        public static void chrraces(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            chrracesdbc chrraces = new chrracesdbc();
            chrraces.LoadDB(connection);
            chrraces.SaveDBC("D:/A/DBC/temp/DBFilesClient/ChrRaces.dbc"); }

        public static void cinematicsequences(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            cinematicsequencesdbc cinematicsequences = new cinematicsequencesdbc();
            cinematicsequences.LoadDB(connection);
            cinematicsequences.SaveDBC("D:/A/DBC/temp/DBFilesClient/CinematicSequences.dbc"); }

        public static void creaturedisplayinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturedisplayinfodbc creaturedisplayinfo = new creaturedisplayinfodbc();
            creaturedisplayinfo.LoadDB(connection);
            creaturedisplayinfo.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureDisplayInfo.dbc"); }

        public static void creaturedisplayinfoextra(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturedisplayinfoextradbc creaturedisplayinfoextra = new creaturedisplayinfoextradbc();
            creaturedisplayinfoextra.LoadDB(connection);
            creaturedisplayinfoextra.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureDisplayInfoExtra.dbc"); }

        public static void creaturefamily(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturefamilydbc creaturefamily = new creaturefamilydbc();
            creaturefamily.LoadDB(connection);
            creaturefamily.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureFamily.dbc"); }

        public static void creaturemodeldata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturemodeldatadbc creaturemodeldata = new creaturemodeldatadbc();
            creaturemodeldata.LoadDB(connection);
            creaturemodeldata.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureModelData.dbc"); }

        public static void creaturespelldata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturespelldatadbc creaturespelldata = new creaturespelldatadbc();
            creaturespelldata.LoadDB(connection);
            creaturespelldata.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureSpellData.dbc"); }

        public static void creaturetype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturetypedbc creaturetype = new creaturetypedbc();
            creaturetype.LoadDB(connection);
            creaturetype.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureType.dbc"); }

        public static void currencytypes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            currencytypesdbc currencytypes = new currencytypesdbc();
            currencytypes.LoadDB(connection);
            currencytypes.SaveDBC("D:/A/DBC/temp/DBFilesClient/CurrencyTypes.dbc"); }

        public static void destructiblemodeldata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            destructiblemodeldatadbc destructiblemodeldata = new destructiblemodeldatadbc();
            destructiblemodeldata.LoadDB(connection);
            destructiblemodeldata.SaveDBC("D:/A/DBC/temp/DBFilesClient/DestructibleModelData.dbc"); }

        public static void dungeonencounter(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            dungeonencounterdbc dungeonencounter = new dungeonencounterdbc();
            dungeonencounter.LoadDB(connection);
            dungeonencounter.SaveDBC("D:/A/DBC/temp/DBFilesClient/DungeonEncounter.dbc"); }

        public static void durabilitycosts(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            durabilitycostsdbc durabilitycosts = new durabilitycostsdbc();
            durabilitycosts.LoadDB(connection);
            durabilitycosts.SaveDBC("D:/A/DBC/temp/DBFilesClient/DurabilityCosts.dbc"); }

        public static void durabilityquality(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            durabilityqualitydbc durabilityquality = new durabilityqualitydbc();
            durabilityquality.LoadDB(connection);
            durabilityquality.SaveDBC("D:/A/DBC/temp/DBFilesClient/DurabilityQuality.dbc"); }

        public static void emotes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            emotesdbc emotes = new emotesdbc();
            emotes.LoadDB(connection);
            emotes.SaveDBC("D:/A/DBC/temp/DBFilesClient/Emotes.dbc"); }

        public static void emotestext(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            emotestextdbc emotestext = new emotestextdbc();
            emotestext.LoadDB(connection);
            emotestext.SaveDBC("D:/A/DBC/temp/DBFilesClient/EmotesText.dbc"); }

        public static void faction(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            factiondbc faction = new factiondbc();
            faction.LoadDB(connection);
            faction.SaveDBC("D:/A/DBC/temp/DBFilesClient/Faction.dbc"); }

        public static void factiontemplate(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            factiontemplatedbc factiontemplate = new factiontemplatedbc();
            factiontemplate.LoadDB(connection);
            factiontemplate.SaveDBC("D:/A/DBC/temp/DBFilesClient/FactionTemplate.dbc"); }

        public static void gameobjectdisplayinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            gameobjectdisplayinfodbc gameobjectdisplayinfo = new gameobjectdisplayinfodbc();
            gameobjectdisplayinfo.LoadDB(connection);
            gameobjectdisplayinfo.SaveDBC("D:/A/DBC/temp/DBFilesClient/gameobjectdisplayinfo.dbc"); }

        public static void gemproperties(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            gempropertiesdbc gemproperties = new gempropertiesdbc();
            gemproperties.LoadDB(connection);
            gemproperties.SaveDBC("D:/A/DBC/temp/DBFilesClient/GemProperties.dbc"); }

        public static void glyphproperties(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            glyphpropertiesdbc glyphproperties = new glyphpropertiesdbc();
            glyphproperties.LoadDB(connection);
            glyphproperties.SaveDBC("D:/A/DBC/temp/DBFilesClient/GlyphProperties.dbc"); }

        public static void glyphslot(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            glyphslotdbc glyphslot = new glyphslotdbc();
            glyphslot.LoadDB(connection);
            glyphslot.SaveDBC("D:/A/DBC/temp/DBFilesClient/GlyphSlot.dbc"); }

        public static void holidays(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            holidaysdbc holidays = new holidaysdbc();
            holidays.LoadDB(connection);
            holidays.SaveDBC("D:/A/DBC/temp/DBFilesClient/Holidays.dbc"); }

        public static void itembagfamily(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itembagfamilydbc itembagfamily = new itembagfamilydbc();
            itembagfamily.LoadDB(connection);
            itembagfamily.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemBagFamily.dbc"); }

        public static void itemextendedcost(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itemextendedcostdbc itemextendedcost = new itemextendedcostdbc();
            itemextendedcost.LoadDB(connection);
            itemextendedcost.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemExtendedCost.dbc"); }

        public static void itemlimitcategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itemlimitcategorydbc itemlimitcategory = new itemlimitcategorydbc();
            itemlimitcategory.LoadDB(connection);
            itemlimitcategory.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemLimitCategory.dbc"); }

        public static void itemrandomproperties(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itemrandompropertiesdbc itemrandomproperties = new itemrandompropertiesdbc();
            itemrandomproperties.LoadDB(connection);
            itemrandomproperties.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemRandomProperties.dbc"); }

        public static void itemrandomsuffix(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itemrandomsuffixdbc itemrandomsuffix = new itemrandomsuffixdbc();
            itemrandomsuffix.LoadDB(connection);
            itemrandomsuffix.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemRandomSuffix.dbc"); }

        public static void itemset(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itemsetdbc itemset = new itemsetdbc();
            itemset.LoadDB(connection);
            itemset.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemSet.dbc"); }

        public static void lfgdungeons(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            lfgdungeonsdbc lfgdungeons = new lfgdungeonsdbc();
            lfgdungeons.LoadDB(connection);
            lfgdungeons.SaveDBC("D:/A/DBC/temp/DBFilesClient/LFGDungeons.dbc"); }

        public static void light(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            lightdbc light = new lightdbc();
            light.LoadDB(connection);
            light.SaveDBC("D:/A/DBC/temp/DBFilesClient/Light.dbc"); }

        public static void liquidtype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            liquidtypedbc liquidtype = new liquidtypedbc();
            liquidtype.LoadDB(connection);
            liquidtype.SaveDBC("D:/A/DBC/temp/DBFilesClient/Liquidtype.dbc"); }

        public static void mailtemplate(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            mailtemplatedbc mailtemplate = new mailtemplatedbc();
            mailtemplate.LoadDB(connection);
            mailtemplate.SaveDBC("D:/A/DBC/temp/DBFilesClient/MailTemplate.dbc"); }

        public static void map(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            mapdbc map = new mapdbc();
            map.LoadDB(connection);
            map.SaveDBC("D:/A/DBC/temp/DBFilesClient/Map.dbc"); }

        public static void mapdifficulty(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            mapdifficultydbc mapdifficulty = new mapdifficultydbc();
            mapdifficulty.LoadDB(connection);
            mapdifficulty.SaveDBC("D:/A/DBC/temp/DBFilesClient/MapDifficulty.dbc"); }

        public static void movie(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            moviedbc movie = new moviedbc();
            movie.LoadDB(connection);
            movie.SaveDBC("D:/A/DBC/temp/DBFilesClient/Movie.dbc"); }

        public static void overridespelldata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            overridespelldatadbc overridespelldata = new overridespelldatadbc();
            overridespelldata.LoadDB(connection);
            overridespelldata.SaveDBC("D:/A/DBC/temp/DBFilesClient/OverrideSpellData.dbc"); }

        public static void powerdisplay(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            powerdisplaydbc powerdisplay = new powerdisplaydbc();
            powerdisplay.LoadDB(connection);
            powerdisplay.SaveDBC("D:/A/DBC/temp/DBFilesClient/PowerDisplay.dbc"); }

        public static void pvpdifficulty(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            pvpdifficultydbc pvpdifficulty = new pvpdifficultydbc();
            pvpdifficulty.LoadDB(connection);
            pvpdifficulty.SaveDBC("D:/A/DBC/temp/DBFilesClient/PvpDifficulty.dbc"); }

        public static void questfactionrew(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            questfactionrewarddbc questfactionreward = new questfactionrewarddbc();
            questfactionreward.LoadDB(connection);
            questfactionreward.SaveDBC("D:/A/DBC/temp/DBFilesClient/QuestFactionReward.dbc"); }

        public static void questsort(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            questsortdbc questsort = new questsortdbc();
            questsort.LoadDB(connection);
            questsort.SaveDBC("D:/A/DBC/temp/DBFilesClient/QuestSort.dbc"); }

        public static void questxp(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            questxpdbc questxp = new questxpdbc();
            questxp.LoadDB(connection);
            questxp.SaveDBC("D:/A/DBC/temp/DBFilesClient/QuestXP.dbc"); }

        public static void randproppoints(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            randproppointsdbc randproppoints = new randproppointsdbc();
            randproppoints.LoadDB(connection);
            randproppoints.SaveDBC("D:/A/DBC/temp/DBFilesClient/RandPropPoints.dbc"); }

        public static void scalingstatdistribution(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            scalingstatdistributiondbc scalingstatdistribution = new scalingstatdistributiondbc();
            scalingstatdistribution.LoadDB(connection);
            scalingstatdistribution.SaveDBC("D:/A/DBC/temp/DBFilesClient/ScalingStatDistribution.dbc"); }

        public static void scalingstatvalues(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            scalingstatvaluesdbc scalingstatvalues = new scalingstatvaluesdbc();
            scalingstatvalues.LoadDB(connection);
            scalingstatvalues.SaveDBC("D:/A/DBC/temp/DBFilesClient/ScalingStatValues.dbc"); }

        public static void skilllineability(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            skilllineabilitydbc skilllineability = new skilllineabilitydbc();
            skilllineability.LoadDB(connection);
            skilllineability.SaveDBC("D:/A/DBC/temp/DBFilesClient/SkillLineAbility.dbc"); }

        public static void skillline(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            skilllinedbc skillline = new skilllinedbc();
            skillline.LoadDB(connection);
            skillline.SaveDBC("D:/A/DBC/temp/DBFilesClient/SkillLine.dbc"); }

        public static void soundentries(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            soundentriesdbc soundentries = new soundentriesdbc();
            soundentries.LoadDB(connection);
            soundentries.SaveDBC("D:/A/DBC/temp/DBFilesClient/SoundEntries.dbc"); }

        public static void spellcasttimes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellcasttimesdbc spellcasttimes = new spellcasttimesdbc();
            spellcasttimes.LoadDB(connection);
            spellcasttimes.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellCastTimes.dbc"); }

        public static void spellcategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellcategorydbc spellcategory = new spellcategorydbc();
            spellcategory.LoadDB(connection);
            spellcategory.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellCategory.dbc"); }

        public static void spell(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spelldbc spell = new spelldbc();
            spell.LoadDB(connection);
            spell.SaveDBC("D:/A/DBC/temp/DBFilesClient/Spell.dbc"); }

        public static void spelldifficulty(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spelldifficultydbc spelldifficulty = new spelldifficultydbc();
            spelldifficulty.LoadDB(connection);
            spelldifficulty.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellDifficulty.dbc"); }

        public static void spellduration(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spelldurationdbc spellduration = new spelldurationdbc();
            spellduration.LoadDB(connection);
            spellduration.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellDuration.dbc"); }

        public static void spellfocusobject(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellfocusobjectdbc spellfocusobject = new spellfocusobjectdbc();
            spellfocusobject.LoadDB(connection);
            spellfocusobject.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellFocusObject.dbc"); }

        public static void spellitemenchantmentcondition(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellitemenchantmentconditiondbc spellitemenchantmentcondition = new spellitemenchantmentconditiondbc();
            spellitemenchantmentcondition.LoadDB(connection);
            spellitemenchantmentcondition.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellItemEnchantmentCondition.dbc"); }

        public static void spellitemenchantment(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellitemenchantmentdbc spellitemenchantment = new spellitemenchantmentdbc();
            spellitemenchantment.LoadDB(connection);
            spellitemenchantment.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellItemEnchantment.dbc"); }

        public static void spellradius(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellradiusdbc spellradius = new spellradiusdbc();
            spellradius.LoadDB(connection);
            spellradius.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellRadius.dbc"); }

        public static void spellrange(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellrangedbc spellrange = new spellrangedbc();
            spellrange.LoadDB(connection);
            spellrange.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellRange.dbc"); }

        public static void spellrunecost(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellrunecostdbc spellrunecost = new spellrunecostdbc();
            spellrunecost.LoadDB(connection);
            spellrunecost.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellRuneCost.dbc"); }

        public static void spellshapeshiftform(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellshapeshiftformdbc spellshapeshiftform = new spellshapeshiftformdbc();
            spellshapeshiftform.LoadDB(connection);
            spellshapeshiftform.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellShapeshiftForm.dbc"); }

        public static void stableslotprices(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            stableslotpricesdbc stableslotprices = new stableslotpricesdbc();
            stableslotprices.LoadDB(connection);
            stableslotprices.SaveDBC("D:/A/DBC/temp/DBFilesClient/StableSlotPrices.dbc"); }

        public static void summonproperties(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            summonpropertiesdbc summonproperties = new summonpropertiesdbc();
            summonproperties.LoadDB(connection);
            summonproperties.SaveDBC("D:/A/DBC/temp/DBFilesClient/SummonProperties.dbc"); }

        public static void talent(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            talentdbc talent = new talentdbc();
            talent.LoadDB(connection);
            talent.SaveDBC("D:/A/DBC/temp/DBFilesClient/Talent.dbc"); }

        public static void talenttab(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            talenttabdbc talenttab = new talenttabdbc();
            talenttab.LoadDB(connection);
            talenttab.SaveDBC("D:/A/DBC/temp/DBFilesClient/TalentTab.dbc"); }

        public static void taxinodes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            taxinodesdbc taxinodes = new taxinodesdbc();
            taxinodes.LoadDB(connection);
            taxinodes.SaveDBC("D:/A/DBC/temp/DBFilesClient/TaxiNodes.dbc"); }

        public static void taxipath(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            taxipathdbc taxipath = new taxipathdbc();
            taxipath.LoadDB(connection);
            taxipath.SaveDBC("D:/A/DBC/temp/DBFilesClient/TaxiPath.dbc"); }

        public static void taxipathnode(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            taxipathnodedbc taxipathnode = new taxipathnodedbc();
            taxipathnode.LoadDB(connection);
            taxipathnode.SaveDBC("D:/A/DBC/temp/DBFilesClient/TaxiPathNode.dbc"); }

        public static void teamcontributionpoints(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            teamcontributionpointsdbc teamcontributionpoints = new teamcontributionpointsdbc();
            teamcontributionpoints.LoadDB(connection);
            teamcontributionpoints.SaveDBC("D:/A/DBC/temp/DBFilesClient/TeamContributionPoints.dbc"); }

        public static void totemcategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            totemcategorydbc totemcategory = new totemcategorydbc();
            totemcategory.LoadDB(connection);
            totemcategory.SaveDBC("D:/A/DBC/temp/DBFilesClient/TotemCategory.dbc"); }

        public static void transportanimation(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            transportanimationdbc transportanimation = new transportanimationdbc();
            transportanimation.LoadDB(connection);
            transportanimation.SaveDBC("D:/A/DBC/temp/DBFilesClient/TransportAnimation.dbc"); }

        public static void transportrotation(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            transportrotationdbc transportrotation = new transportrotationdbc();
            transportrotation.LoadDB(connection);
            transportrotation.SaveDBC("D:/A/DBC/temp/DBFilesClient/TransportRotation.dbc"); }

        public static void vehicle(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            vehicledbc vehicle = new vehicledbc();
            vehicle.LoadDB(connection);
            vehicle.SaveDBC("D:/A/DBC/temp/DBFilesClient/Vehicle.dbc"); }

        public static void vehicleseat(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            vehicleseatdbc vehicleseat = new vehicleseatdbc();
            vehicleseat.LoadDB(connection);
            vehicleseat.SaveDBC("D:/A/DBC/temp/DBFilesClient/VehicleSeat.dbc"); }

        public static void wmoareatable(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            wmoareatabledbc wmoareatable = new wmoareatabledbc();
            wmoareatable.LoadDB(connection);
            wmoareatable.SaveDBC("D:/A/DBC/temp/DBFilesClient/WMOAreaTable.dbc"); }

        public static void worldmapoverlay(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            worldmapoverlaydbc worldmapoverlay = new worldmapoverlaydbc();
            worldmapoverlay.LoadDB(connection);
            worldmapoverlay.SaveDBC("D:/A/DBC/temp/DBFilesClient/WorldMapOverlay.dbc"); }

        public static void worldsafelocsdbc(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            worldsafelocsdbc worldsafelocs = new worldsafelocsdbc();
            worldsafelocs.LoadDB(connection);
            worldsafelocs.SaveDBC("D:/A/DBC/temp/DBFilesClient/WorldSafeLocs.dbc"); }
        
        // iwpu
        public static void achievement_category(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            achievement_categorydbc achievement_category = new achievement_categorydbc();
            achievement_category.LoadDB(connection);
            achievement_category.SaveDBC("D:/A/DBC/temp/DBFilesClient/Achievement_Category.dbc"); }

        public static void animationdata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            animationdatadbc animationdata = new animationdatadbc();
            animationdata.LoadDB(connection);
            animationdata.SaveDBC("D:/A/DBC/temp/DBFilesClient/AnimationData.dbc"); }

        public static void attackanimkits(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            attackanimkitsdbc attackanimkits = new attackanimkitsdbc();
            attackanimkits.LoadDB(connection);
            attackanimkits.SaveDBC("D:/A/DBC/temp/DBFilesClient/AttackAnimKits.dbc"); }

        public static void attackanimtypes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            attackanimtypesdbc attackanimtypes = new attackanimtypesdbc();
            attackanimtypes.LoadDB(connection);
            attackanimtypes.SaveDBC("D:/A/DBC/temp/DBFilesClient/AttackAnimTypes.dbc"); }

        public static void camerashakes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            camerashakesdbc camerashakes = new camerashakesdbc();
            camerashakes.LoadDB(connection);
            camerashakes.SaveDBC("D:/A/DBC/temp/DBFilesClient/CameraShakes.dbc"); }

        public static void cfg_categories(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            cfg_categoriesdbc cfg_categories = new cfg_categoriesdbc();
            cfg_categories.LoadDB(connection);
            cfg_categories.SaveDBC("D:/A/DBC/temp/DBFilesClient/Cfg_Categories.dbc"); }

        public static void cfg_configs(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            cfg_configsdbc cfg_configs = new cfg_configsdbc();
            cfg_configs.LoadDB(connection);
            cfg_configs.SaveDBC("D:/A/DBC/temp/DBFilesClient/Cfg_Configs.dbc"); }

        public static void characterfacialhairstyles(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            characterfacialhairstylesdbc characterfacialhairstyles = new characterfacialhairstylesdbc();
            characterfacialhairstyles.LoadDB(connection);
            characterfacialhairstyles.SaveDBC("D:/A/DBC/temp/DBFilesClient/CharacterFacialHairStyles.dbc"); }

        public static void charbaseinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            charbaseinfodbc charbaseinfo = new charbaseinfodbc();
            charbaseinfo.LoadDB(connection);
            charbaseinfo.SaveDBC("D:/A/DBC/temp/DBFilesClient/CharBaseInfo.dbc"); }

        public static void charhairgeosets(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            charhairgeosetsdbc charhairgeosets = new charhairgeosetsdbc();
            charhairgeosets.LoadDB(connection);
            charhairgeosets.SaveDBC("D:/A/DBC/temp/DBFilesClient/CharHairGeosets.dbc"); }

        public static void charhairtextures(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            charhairtexturesdbc charhairtextures = new charhairtexturesdbc();
            charhairtextures.LoadDB(connection);
            charhairtextures.SaveDBC("D:/A/DBC/temp/DBFilesClient/CharHairTextures.dbc"); }

        public static void charsections(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            charsectionsdbc charsections = new charsectionsdbc();
            charsections.LoadDB(connection);
            charsections.SaveDBC("D:/A/DBC/temp/DBFilesClient/CharSections.dbc"); }

        public static void chatprofanity(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            chatprofanitydbc chatprofanity = new chatprofanitydbc();
            chatprofanity.LoadDB(connection);
            chatprofanity.SaveDBC("D:/A/DBC/temp/DBFilesClient/ChatProfanity.dbc"); }

        public static void cinematiccamera(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            cinematiccameradbc cinematiccamera = new cinematiccameradbc();
            cinematiccamera.LoadDB(connection);
            cinematiccamera.SaveDBC("D:/A/DBC/temp/DBFilesClient/CinematicCamera.dbc"); }

        public static void creaturemovementinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            creaturemovementinfodbc creaturemovementinfo = new creaturemovementinfodbc();
            creaturemovementinfo.LoadDB(connection);
            creaturemovementinfo.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureMovementInfo.dbc"); }

        public static void creaturesounddata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            creaturesounddatadbc creaturesounddata = new creaturesounddatadbc();
            creaturesounddata.LoadDB(connection);
            creaturesounddata.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureSoundData.dbc"); }

        public static void currencycategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            currencycategorydbc currencycategory = new currencycategorydbc();
            currencycategory.LoadDB(connection);
            currencycategory.SaveDBC("D:/A/DBC/temp/DBFilesClient/CurrencyCategory.dbc"); }

        public static void dancemoves(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            dancemovesdbc dancemoves = new dancemovesdbc();
            dancemoves.LoadDB(connection);
            dancemoves.SaveDBC("D:/A/DBC/temp/DBFilesClient/DanceMoves.dbc"); }

        public static void deaththudlookups(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            deaththudlookupsdbc deaththudlookups = new deaththudlookupsdbc();
            deaththudlookups.LoadDB(connection);
            deaththudlookups.SaveDBC("D:/A/DBC/temp/DBFilesClient/DeathThudLookups.dbc"); }

        public static void declinedword(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            declinedworddbc declinedword = new declinedworddbc();
            declinedword.LoadDB(connection);
            declinedword.SaveDBC("D:/A/DBC/temp/DBFilesClient/DeclinedWord.dbc"); }

        public static void declinedwordcases(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            declinedwordcasesdbc declinedwordcases = new declinedwordcasesdbc();
            declinedwordcases.LoadDB(connection);
            declinedwordcases.SaveDBC("D:/A/DBC/temp/DBFilesClient/declinedwordcases.dbc"); }

        public static void dungeonmap(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            dungeonmapdbc dungeonmap = new dungeonmapdbc();
            dungeonmap.LoadDB(connection);
            dungeonmap.SaveDBC("D:/A/DBC/temp/DBFilesClient/DungeonMap.dbc"); }

        public static void dungeonmapchunk(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            dungeonmapchunkdbc dungeonmapchunk = new dungeonmapchunkdbc();
            dungeonmapchunk.LoadDB(connection);
            dungeonmapchunk.SaveDBC("D:/A/DBC/temp/DBFilesClient/DungeonMapChunk.dbc"); }

        public static void emotestextdata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            emotestextdatadbc emotestextdata = new emotestextdatadbc();
            emotestextdata.LoadDB(connection);
            emotestextdata.SaveDBC("D:/A/DBC/temp/DBFilesClient/EmotesTextData.dbc"); }

        public static void emotestextsound(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            emotestextsounddbc emotestextsound = new emotestextsounddbc();
            emotestextsound.LoadDB(connection);
            emotestextsound.SaveDBC("D:/A/DBC/temp/DBFilesClient/EmotesTextSound.dbc"); }

        public static void environmentaldamage(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            environmentaldamagedbc environmentaldamage = new environmentaldamagedbc();
            environmentaldamage.LoadDB(connection);
            environmentaldamage.SaveDBC("D:/A/DBC/temp/DBFilesClient/EnvironmentalDamage.dbc"); }

        public static void exhaustion(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            exhaustiondbc exhaustion = new exhaustiondbc();
            exhaustion.LoadDB(connection);
            exhaustion.SaveDBC("D:/A/DBC/temp/DBFilesClient/Exhaustion.dbc"); }

        public static void factiongroup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            factiongroupdbc factiongroup = new factiongroupdbc();
            factiongroup.LoadDB(connection);
            factiongroup.SaveDBC("D:/A/DBC/temp/DBFilesClient/FactionGroup.dbc"); }

        public static void filedata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            filedatadbc filedata = new filedatadbc();
            filedata.LoadDB(connection);
            filedata.SaveDBC("D:/A/DBC/temp/DBFilesClient/FileData.dbc"); }

        public static void footprinttextures(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            footprinttexturesdbc footprinttextures = new footprinttexturesdbc();
            footprinttextures.LoadDB(connection);
            footprinttextures.SaveDBC("D:/A/DBC/temp/DBFilesClient/FootprintTextures.dbc"); }

        public static void footstepterrainlookup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            footstepterrainlookupdbc footstepterrainlookup = new footstepterrainlookupdbc();
            footstepterrainlookup.LoadDB(connection);
            footstepterrainlookup.SaveDBC("D:/A/DBC/temp/DBFilesClient/FootstepTerrainLookup.dbc"); }

        public static void gameobjectartkit(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gameobjectartkitdbc gameobjectartkit = new gameobjectartkitdbc();
            gameobjectartkit.LoadDB(connection);
            gameobjectartkit.SaveDBC("D:/A/DBC/temp/DBFilesClient/GameObjectArtKit.dbc"); }

        public static void gametables(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gametablesdbc gametables = new gametablesdbc();
            gametables.LoadDB(connection);
            gametables.SaveDBC("D:/A/DBC/temp/DBFilesClient/GameTables.dbc"); }

        public static void gametips(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gametipsdbc gametips = new gametipsdbc();
            gametips.LoadDB(connection);
            gametips.SaveDBC("D:/A/DBC/temp/DBFilesClient/GameTips.dbc"); }

        public static void gmsurveyanswers(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gmsurveyanswersdbc gmsurveyanswers = new gmsurveyanswersdbc();
            gmsurveyanswers.LoadDB(connection);
            gmsurveyanswers.SaveDBC("D:/A/DBC/temp/DBFilesClient/GMSurveyAnswers.dbc"); }

        public static void gmsurveycurrentsurvey(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gmsurveycurrentsurveydbc gmsurveycurrentsurvey = new gmsurveycurrentsurveydbc();
            gmsurveycurrentsurvey.LoadDB(connection);
            gmsurveycurrentsurvey.SaveDBC("D:/A/DBC/temp/DBFilesClient/GMSurveyCurrentSurvey.dbc"); }

        public static void gmsurveyquestions(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gmsurveyquestionsdbc gmsurveyquestions = new gmsurveyquestionsdbc();
            gmsurveyquestions.LoadDB(connection);
            gmsurveyquestions.SaveDBC("D:/A/DBC/temp/DBFilesClient/GMSurveyQuestions.dbc"); }

        public static void gmsurveysurveys(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gmsurveysurveysdbc gmsurveysurveys = new gmsurveysurveysdbc();
            gmsurveysurveys.LoadDB(connection);
            gmsurveysurveys.SaveDBC("D:/A/DBC/temp/DBFilesClient/GMSurveySurveys.dbc"); }

        public static void gmticketcategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gmticketcategorydbc gmticketcategory = new gmticketcategorydbc();
            gmticketcategory.LoadDB(connection);
            gmticketcategory.SaveDBC("D:/A/DBC/temp/DBFilesClient/GMTicketCategory.dbc"); }

        public static void groundeffectdoodad(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            groundeffectdoodaddbc groundeffectdoodad = new groundeffectdoodaddbc();
            groundeffectdoodad.LoadDB(connection);
            groundeffectdoodad.SaveDBC("D:/A/DBC/temp/DBFilesClient/GroundEffectDoodad.dbc"); }

        public static void groundeffecttexture(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            groundeffecttexturedbc groundeffecttexture = new groundeffecttexturedbc();
            groundeffecttexture.LoadDB(connection);
            groundeffecttexture.SaveDBC("D:/A/DBC/temp/DBFilesClient/GroundEffectTexture.dbc"); }

        public static void helmetgeosetvisdata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            helmetgeosetvisdatadbc helmetgeosetvisdata = new helmetgeosetvisdatadbc();
            helmetgeosetvisdata.LoadDB(connection);
            helmetgeosetvisdata.SaveDBC("D:/A/DBC/temp/DBFilesClient/HelmetGeosetVisData.dbc"); }

        public static void holidaydescriptions(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            holidaydescriptionsdbc holidaydescriptions = new holidaydescriptionsdbc();
            holidaydescriptions.LoadDB(connection);
            holidaydescriptions.SaveDBC("D:/A/DBC/temp/DBFilesClient/HolidayDescriptions.dbc"); }

        public static void holidaynames(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            holidaynamesdbc holidaynames = new holidaynamesdbc();
            holidaynames.LoadDB(connection);
            holidaynames.SaveDBC("D:/A/DBC/temp/DBFilesClient/HolidayNames.dbc"); }

        public static void itemclass(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemclassdbc itemclass = new itemclassdbc();
            itemclass.LoadDB(connection);
            itemclass.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemClass.dbc"); }

        public static void itemcondextcosts(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemcondextcostsdbc itemcondextcosts = new itemcondextcostsdbc();
            itemcondextcosts.LoadDB(connection);
            itemcondextcosts.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemCondExtCosts.dbc"); }

        public static void itemdisplayinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemdisplayinfodbc itemdisplayinfo = new itemdisplayinfodbc();
            itemdisplayinfo.LoadDB(connection);
            itemdisplayinfo.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemDisplayInfo.dbc"); }

        public static void itemgroupsounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemgroupsoundsdbc itemgroupsounds = new itemgroupsoundsdbc();
            itemgroupsounds.LoadDB(connection);
            itemgroupsounds.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemGroupSounds.dbc"); }

        public static void itempetfood(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itempetfooddbc itempetfood = new itempetfooddbc();
            itempetfood.LoadDB(connection);
            itempetfood.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemPetFood.dbc"); }

        public static void itempurchasegroup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itempurchasegroupdbc itempurchasegroup = new itempurchasegroupdbc();
            itempurchasegroup.LoadDB(connection);
            itempurchasegroup.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemPurchaseGroup.dbc"); }

        public static void itemsubclass(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemsubclassdbc itemsubclass = new itemsubclassdbc();
            itemsubclass.LoadDB(connection);
            itemsubclass.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemSubClass.dbc"); }

        public static void itemsubclassmask(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemsubclassmaskdbc itemsubclassmask = new itemsubclassmaskdbc();
            itemsubclassmask.LoadDB(connection);
            itemsubclassmask.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemSubClassMask.dbc"); }

        public static void itemvisualeffects(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemvisualeffectsdbc itemvisualeffects = new itemvisualeffectsdbc();
            itemvisualeffects.LoadDB(connection);
            itemvisualeffects.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemVisualEffects.dbc"); }

        public static void itemvisuals(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemvisualsdbc itemvisuals = new itemvisualsdbc();
            itemvisuals.LoadDB(connection);
            itemvisuals.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemVisuals.dbc"); }

        public static void languages(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            languagesdbc languages = new languagesdbc();
            languages.LoadDB(connection);
            languages.SaveDBC("D:/A/DBC/temp/DBFilesClient/Languages.dbc"); }

        public static void languagewords(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            languagewordsdbc languagewords = new languagewordsdbc();
            languagewords.LoadDB(connection);
            languagewords.SaveDBC("D:/A/DBC/temp/DBFilesClient/LanguageWords.dbc"); }

        public static void lfgdungeonexpansion(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lfgdungeonexpansiondbc lfgdungeonexpansion = new lfgdungeonexpansiondbc();
            lfgdungeonexpansion.LoadDB(connection);
            lfgdungeonexpansion.SaveDBC("D:/A/DBC/temp/DBFilesClient/LFGDungeonExpansion.dbc"); }

        public static void lfgdungeongroup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lfgdungeongroupdbc lfgdungeongroup = new lfgdungeongroupdbc();
            lfgdungeongroup.LoadDB(connection);
            lfgdungeongroup.SaveDBC("D:/A/DBC/temp/DBFilesClient/LFGDungeonGroup.dbc"); }

        public static void lightfloatband(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lightfloatbanddbc lightfloatband = new lightfloatbanddbc();
            lightfloatband.LoadDB(connection);
            lightfloatband.SaveDBC("D:/A/DBC/temp/DBFilesClient/LightFloatBand.dbc"); }

        public static void lightintband(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lightintbanddbc lightintband = new lightintbanddbc();
            lightintband.LoadDB(connection);
            lightintband.SaveDBC("D:/A/DBC/temp/DBFilesClient/LightIntBand.dbc"); }

        public static void lightparams(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lightparamsdbc lightparams = new lightparamsdbc();
            lightparams.LoadDB(connection);
            lightparams.SaveDBC("D:/A/DBC/temp/DBFilesClient/LightParams.dbc"); }

        public static void lightskybox(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lightskyboxdbc lightskybox = new lightskyboxdbc();
            lightskybox.LoadDB(connection);
            lightskybox.SaveDBC("D:/A/DBC/temp/DBFilesClient/LightSkybox.dbc"); }

        public static void liquidmaterial(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            liquidmaterialdbc liquidmaterial = new liquidmaterialdbc();
            liquidmaterial.LoadDB(connection);
            liquidmaterial.SaveDBC("D:/A/DBC/temp/DBFilesClient/LiquidMaterial.dbc"); }

        public static void loadingscreens(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            loadingscreensdbc loadingscreens = new loadingscreensdbc();
            loadingscreens.LoadDB(connection);
            loadingscreens.SaveDBC("D:/A/DBC/temp/DBFilesClient/LoadingScreens.dbc"); }

        public static void loadingscreentaxisplines(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            loadingscreentaxisplinesdbc loadingscreentaxisplines = new loadingscreentaxisplinesdbc();
            loadingscreentaxisplines.LoadDB(connection);
            loadingscreentaxisplines.SaveDBC("D:/A/DBC/temp/DBFilesClient/LoadingScreenTaxiSplines.dbc"); }

        public static void locktype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            locktypedbc locktype = new locktypedbc();
            locktype.LoadDB(connection);
            locktype.SaveDBC("D:/A/DBC/temp/DBFilesClient/LockType.dbc"); }

        public static void material(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            materialdbc material = new materialdbc();
            material.LoadDB(connection);
            material.SaveDBC("D:/A/DBC/temp/DBFilesClient/Material.dbc"); }

        public static void moviefiledata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            moviefiledatadbc moviefiledata = new moviefiledatadbc();
            moviefiledata.LoadDB(connection);
            moviefiledata.SaveDBC("D:/A/DBC/temp/DBFilesClient/MovieFileData.dbc"); }

        public static void movievariation(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            movievariationdbc movievariation = new movievariationdbc();
            movievariation.LoadDB(connection);
            movievariation.SaveDBC("D:/A/DBC/temp/DBFilesClient/MovieVariation.dbc"); }

        public static void namegen(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            namegendbc namegen = new namegendbc();
            namegen.LoadDB(connection);
            namegen.SaveDBC("D:/A/DBC/temp/DBFilesClient/NameGen.dbc"); }

        public static void namesprofanity(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            namesprofanitydbc namesprofanity = new namesprofanitydbc();
            namesprofanity.LoadDB(connection);
            namesprofanity.SaveDBC("D:/A/DBC/temp/DBFilesClient/NamesProfanity.dbc"); }

        public static void namesreserved(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            namesreserveddbc namesreserved = new namesreserveddbc();
            namesreserved.LoadDB(connection);
            namesreserved.SaveDBC("D:/A/DBC/temp/DBFilesClient/NamesReserved.dbc"); }

        public static void npcsounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            npcsoundsdbc npcsounds = new npcsoundsdbc();
            npcsounds.LoadDB(connection);
            npcsounds.SaveDBC("D:/A/DBC/temp/DBFilesClient/NPCSounds.dbc"); }

        public static void objecteffect(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            objecteffectdbc objecteffect = new objecteffectdbc();
            objecteffect.LoadDB(connection);
            objecteffect.SaveDBC("D:/A/DBC/temp/DBFilesClient/ObjectEffect.dbc"); }

        public static void objecteffectgroup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            objecteffectgroupdbc objecteffectgroup = new objecteffectgroupdbc();
            objecteffectgroup.LoadDB(connection);
            objecteffectgroup.SaveDBC("D:/A/DBC/temp/DBFilesClient/ObjectEffectGroup.dbc"); }

        public static void objecteffectmodifier(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            objecteffectmodifierdbc objecteffectmodifier = new objecteffectmodifierdbc();
            objecteffectmodifier.LoadDB(connection);
            objecteffectmodifier.SaveDBC("D:/A/DBC/temp/DBFilesClient/ObjectEffectModifier.dbc"); }

        public static void objecteffectpackage(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            objecteffectpackagedbc objecteffectpackage = new objecteffectpackagedbc();
            objecteffectpackage.LoadDB(connection);
            objecteffectpackage.SaveDBC("D:/A/DBC/temp/DBFilesClient/ObjectEffectPackage.dbc"); }

        public static void objecteffectpackageelem(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            objecteffectpackageelemdbc objecteffectpackageelem = new objecteffectpackageelemdbc();
            objecteffectpackageelem.LoadDB(connection);
            objecteffectpackageelem.SaveDBC("D:/A/DBC/temp/DBFilesClient/ObjectEffectPackageElem.dbc"); }

        public static void package(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            packagedbc package = new packagedbc();
            package.LoadDB(connection);
            package.SaveDBC("D:/A/DBC/temp/DBFilesClient/Package.dbc"); }

        public static void pagetextmaterial(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            pagetextmaterialdbc pagetextmaterial = new pagetextmaterialdbc();
            pagetextmaterial.LoadDB(connection);
            pagetextmaterial.SaveDBC("D:/A/DBC/temp/DBFilesClient/PageTextMaterial.dbc"); }

        public static void paperdollitemframe(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            paperdollitemframedbc paperdollitemframe = new paperdollitemframedbc();
            paperdollitemframe.LoadDB(connection);
            paperdollitemframe.SaveDBC("D:/A/DBC/temp/DBFilesClient/PaperDollItemFrame.dbc"); }

        public static void particlecolor(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            particlecolordbc particlecolor = new particlecolordbc();
            particlecolor.LoadDB(connection);
            particlecolor.SaveDBC("D:/A/DBC/temp/DBFilesClient/ParticleColor.dbc"); }

        public static void petitiontype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            petitiontypedbc petitiontype = new petitiontypedbc();
            petitiontype.LoadDB(connection);
            petitiontype.SaveDBC("D:/A/DBC/temp/DBFilesClient/PetitionType.dbc"); }

        public static void petpersonality(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            petpersonalitydbc petpersonality = new petpersonalitydbc();
            petpersonality.LoadDB(connection);
            petpersonality.SaveDBC("D:/A/DBC/temp/DBFilesClient/PetPersonality.dbc"); }

        public static void questinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            questinfodbc questinfo = new questinfodbc();
            questinfo.LoadDB(connection);
            questinfo.SaveDBC("D:/A/DBC/temp/DBFilesClient/QuestInfo.dbc"); }

        public static void resistances(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            resistancesdbc resistances = new resistancesdbc();
            resistances.LoadDB(connection);
            resistances.SaveDBC("D:/A/DBC/temp/DBFilesClient/Resistances.dbc"); }

        public static void screeneffect(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            screeneffectdbc screeneffect = new screeneffectdbc();
            screeneffect.LoadDB(connection);
            screeneffect.SaveDBC("D:/A/DBC/temp/DBFilesClient/ScreenEffect.dbc"); }

        public static void servermessages(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            servermessagesdbc servermessages = new servermessagesdbc();
            servermessages.LoadDB(connection);
            servermessages.SaveDBC("D:/A/DBC/temp/DBFilesClient/ServerMessages.dbc"); }

        public static void sheathesoundlookups(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            sheathesoundlookupsdbc sheathesoundlookups = new sheathesoundlookupsdbc();
            sheathesoundlookups.LoadDB(connection);
            sheathesoundlookups.SaveDBC("D:/A/DBC/temp/DBFilesClient/SheatheSoundLookups.dbc"); }

        public static void skillcostsdata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            skillcostsdatadbc skillcostsdata = new skillcostsdatadbc();
            skillcostsdata.LoadDB(connection);
            skillcostsdata.SaveDBC("D:/A/DBC/temp/DBFilesClient/SkillCostsData.dbc"); }

        public static void skilllinecategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            skilllinecategorydbc skilllinecategory = new skilllinecategorydbc();
            skilllinecategory.LoadDB(connection);
            skilllinecategory.SaveDBC("D:/A/DBC/temp/DBFilesClient/SkillLineCategory.dbc"); }

        public static void skillraceclassinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            skillraceclassinfodbc skillraceclassinfo = new skillraceclassinfodbc();
            skillraceclassinfo.LoadDB(connection);
            skillraceclassinfo.SaveDBC("D:/A/DBC/temp/DBFilesClient/SkillRaceClassInfo.dbc"); }

        public static void skilltiers(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            skilltiersdbc skilltiers = new skilltiersdbc();
            skilltiers.LoadDB(connection);
            skilltiers.SaveDBC("D:/A/DBC/temp/DBFilesClient/SkillTiers.dbc"); }

        public static void soundambience(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundambiencedbc soundambience = new soundambiencedbc();
            soundambience.LoadDB(connection);
            soundambience.SaveDBC("D:/A/DBC/temp/DBFilesClient/SoundAmbience.dbc"); }

        public static void soundemitters(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundemittersdbc soundemitters = new soundemittersdbc();
            soundemitters.LoadDB(connection);
            soundemitters.SaveDBC("D:/A/DBC/temp/DBFilesClient/SoundEmitters.dbc"); }

        public static void soundentriesadvanced(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundentriesadvanceddbc soundentriesadvanced = new soundentriesadvanceddbc();
            soundentriesadvanced.LoadDB(connection);
            soundentriesadvanced.SaveDBC("D:/A/DBC/temp/DBFilesClient/SoundEntriesAdvanced.dbc"); }

        public static void soundfilter(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundfilterdbc soundfilter = new soundfilterdbc();
            soundfilter.LoadDB(connection);
            soundfilter.SaveDBC("D:/A/DBC/temp/DBFilesClient/SoundFilter.dbc"); }

        public static void soundfilterelem(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundfilterelemdbc soundfilterelem = new soundfilterelemdbc();
            soundfilterelem.LoadDB(connection);
            soundfilterelem.SaveDBC("D:/A/DBC/temp/DBFilesClient/SoundFilterElem.dbc"); }

        public static void soundproviderpreferences(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundproviderpreferencesdbc soundproviderpreferences = new soundproviderpreferencesdbc();
            soundproviderpreferences.LoadDB(connection);
            soundproviderpreferences.SaveDBC("D:/A/DBC/temp/DBFilesClient/SoundProviderPreferences.dbc"); }

        public static void soundsamplepreferences(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundsamplepreferencesdbc soundsamplepreferences = new soundsamplepreferencesdbc();
            soundsamplepreferences.LoadDB(connection);
            soundsamplepreferences.SaveDBC("D:/A/DBC/temp/DBFilesClient/SoundSamplePreferences.dbc"); }

        public static void soundwatertype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundwatertypedbc soundwatertype = new soundwatertypedbc();
            soundwatertype.LoadDB(connection);
            soundwatertype.SaveDBC("D:/A/DBC/temp/DBFilesClient/SoundWaterType.dbc"); }

        public static void spammessages(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spammessagesdbc spammessages = new spammessagesdbc();
            spammessages.LoadDB(connection);
            spammessages.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpamMessages.dbc"); }

        public static void spelldescriptionvariables(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spelldescriptionvariablesdbc spelldescriptionvariables = new spelldescriptionvariablesdbc();
            spelldescriptionvariables.LoadDB(connection);
            spelldescriptionvariables.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellDescriptionVariables.dbc"); }

        public static void spelldispeltype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spelldispeltypedbc spelldispeltype = new spelldispeltypedbc();
            spelldispeltype.LoadDB(connection);
            spelldispeltype.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellDispelType.dbc"); }

        public static void spelleffectcamerashakes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spelleffectcamerashakesdbc spelleffectcamerashakes = new spelleffectcamerashakesdbc();
            spelleffectcamerashakes.LoadDB(connection);
            spelleffectcamerashakes.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellEffectCameraShakes.dbc"); }

        public static void spellicon(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellicondbc spellicon = new spellicondbc();
            spellicon.LoadDB(connection);
            spellicon.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellIcon.dbc"); }

        public static void spellmechanic(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellmechanicdbc spellmechanic = new spellmechanicdbc();
            spellmechanic.LoadDB(connection);
            spellmechanic.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellMechanic.dbc"); }

        public static void spellmissile(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellmissiledbc spellmissile = new spellmissiledbc();
            spellmissile.LoadDB(connection);
            spellmissile.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellMissile.dbc"); }

        public static void spellmissilemotion(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellmissilemotiondbc spellmissilemotion = new spellmissilemotiondbc();
            spellmissilemotion.LoadDB(connection);
            spellmissilemotion.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellMissileMotion.dbc"); }

        public static void spellvisual(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualdbc spellvisual = new spellvisualdbc();
            spellvisual.LoadDB(connection);
            spellvisual.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellVisual.dbc"); }

        public static void spellvisualeffectname(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualeffectnamedbc spellvisualeffectname = new spellvisualeffectnamedbc();
            spellvisualeffectname.LoadDB(connection);
            spellvisualeffectname.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellVisualEffectName.dbc"); }

        public static void spellvisualkit(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualkitdbc spellvisualkit = new spellvisualkitdbc();
            spellvisualkit.LoadDB(connection);
            spellvisualkit.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellVisualKit.dbc"); }

        public static void spellvisualkitareamodel(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualkitareamodeldbc spellvisualkitareamodel = new spellvisualkitareamodeldbc();
            spellvisualkitareamodel.LoadDB(connection);
            spellvisualkitareamodel.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellVisualKitAreaModel.dbc"); }

        public static void spellvisualkitmodelattach(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualkitmodelattachdbc spellvisualkitmodelattach = new spellvisualkitmodelattachdbc();
            spellvisualkitmodelattach.LoadDB(connection);
            spellvisualkitmodelattach.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellVisualKitModelAttach.dbc"); }

        public static void spellvisualprecasttransitions(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualprecasttransitionsdbc spellvisualprecasttransitions = new spellvisualprecasttransitionsdbc();
            spellvisualprecasttransitions.LoadDB(connection);
            spellvisualprecasttransitions.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellVisualPrecastTransitions.dbc"); }

        public static void startup_strings(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            startup_stringsdbc startup_strings = new startup_stringsdbc();
            startup_strings.LoadDB(connection);
            startup_strings.SaveDBC("D:/A/DBC/temp/DBFilesClient/Startup_Strings.dbc"); }

        public static void stationery(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            stationerydbc stationery = new stationerydbc();
            stationery.LoadDB(connection);
            stationery.SaveDBC("D:/A/DBC/temp/DBFilesClient/Stationery.dbc"); }

        public static void stringlookups(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            stringlookupsdbc stringlookups = new stringlookupsdbc();
            stringlookups.LoadDB(connection);
            stringlookups.SaveDBC("D:/A/DBC/temp/DBFilesClient/StringLookups.dbc"); }

        public static void terraintype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            terraintypedbc terraintype = new terraintypedbc();
            terraintype.LoadDB(connection);
            terraintype.SaveDBC("D:/A/DBC/temp/DBFilesClient/TerrainType.dbc"); }

        public static void transportphysics(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            transportphysicsdbc transportphysics = new transportphysicsdbc();
            transportphysics.LoadDB(connection);
            transportphysics.SaveDBC("D:/A/DBC/temp/DBFilesClient/TransportPhysics.dbc"); }

        public static void uisoundlookups(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            uisoundlookupsdbc uisoundlookups = new uisoundlookupsdbc();
            uisoundlookups.LoadDB(connection);
            uisoundlookups.SaveDBC("D:/A/DBC/temp/DBFilesClient/UISoundLookups.dbc"); }

        public static void unitblood(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            unitblooddbc unitblood = new unitblooddbc();
            unitblood.LoadDB(connection);
            unitblood.SaveDBC("D:/A/DBC/temp/DBFilesClient/UnitBlood.dbc"); }

        public static void unitbloodlevels(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            unitbloodlevelsdbc unitbloodlevels = new unitbloodlevelsdbc();
            unitbloodlevels.LoadDB(connection);
            unitbloodlevels.SaveDBC("D:/A/DBC/temp/DBFilesClient/UnitBloodLevels.dbc"); }

        public static void vehicleuiindicator(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            vehicleuiindicatordbc vehicleuiindicator = new vehicleuiindicatordbc();
            vehicleuiindicator.LoadDB(connection);
            vehicleuiindicator.SaveDBC("D:/A/DBC/temp/DBFilesClient/VehicleUIIndicator.dbc"); }

        public static void vehicleuiindseat(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            vehicleuiindseatdbc vehicleuiindseat = new vehicleuiindseatdbc();
            vehicleuiindseat.LoadDB(connection);
            vehicleuiindseat.SaveDBC("D:/A/DBC/temp/DBFilesClient/VehicleUIIndSeat.dbc"); }

        public static void videohardware(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            videohardwaredbc videohardware = new videohardwaredbc();
            videohardware.LoadDB(connection);
            videohardware.SaveDBC("D:/A/DBC/temp/DBFilesClient/VideoHardWare.dbc"); }

        public static void vocaluisounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            vocaluisoundsdbc vocaluisounds = new vocaluisoundsdbc();
            vocaluisounds.LoadDB(connection);
            vocaluisounds.SaveDBC("D:/A/DBC/temp/DBFilesClient/VocalUISounds.dbc"); }

        public static void weaponimpactsounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            weaponimpactsoundsdbc weaponimpactsounds = new weaponimpactsoundsdbc();
            weaponimpactsounds.LoadDB(connection);
            weaponimpactsounds.SaveDBC("D:/A/DBC/temp/DBFilesClient/WeaponImpactSounds.dbc"); }

        public static void weaponswingsounds2(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            weaponswingsounds2dbc weaponswingsounds2 = new weaponswingsounds2dbc();
            weaponswingsounds2.LoadDB(connection);
            weaponswingsounds2.SaveDBC("D:/A/DBC/temp/DBFilesClient/WeaponSwingSounds2.dbc"); }

        public static void weather(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            weatherdbc weather = new weatherdbc();
            weather.LoadDB(connection);
            weather.SaveDBC("D:/A/DBC/temp/DBFilesClient/Weather.dbc"); }

        public static void worldmaparea(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            worldmapareadbc worldmaparea = new worldmapareadbc();
            worldmaparea.LoadDB(connection);
            worldmaparea.SaveDBC("D:/A/DBC/temp/DBFilesClient/WorldMapArea.dbc"); }

        public static void worldmapcontinent(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            worldmapcontinentdbc worldmapcontinent = new worldmapcontinentdbc();
            worldmapcontinent.LoadDB(connection);
            worldmapcontinent.SaveDBC("D:/A/DBC/temp/DBFilesClient/WorldMapContinent.dbc"); }

        public static void worldmaptransforms(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            worldmaptransformsdbc worldmaptransforms = new worldmaptransformsdbc();
            worldmaptransforms.LoadDB(connection);
            worldmaptransforms.SaveDBC("D:/A/DBC/temp/DBFilesClient/WorldMapTransforms.dbc"); }

        public static void worldstateui(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            worldstateuidbc worldstateui = new worldstateuidbc();
            worldstateui.LoadDB(connection);
            worldstateui.SaveDBC("D:/A/DBC/temp/DBFilesClient/WorldStateuUI.dbc"); }

        public static void worldstatezonesounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            worldstatezonesoundsdbc worldstatezonesounds = new worldstatezonesoundsdbc();
            worldstatezonesounds.LoadDB(connection);
            worldstatezonesounds.SaveDBC("D:/A/DBC/temp/DBFilesClient/WorldStateZoneSounds.dbc"); }

        public static void wowerror_strings(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            wowerror_stringsdbc wowerror_strings = new wowerror_stringsdbc();
            wowerror_strings.LoadDB(connection);
            wowerror_strings.SaveDBC("D:/A/DBC/temp/DBFilesClient/WowError_Strings.dbc"); }

        public static void zoneintromusictable(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            zoneintromusictabledbc zoneintromusictable = new zoneintromusictabledbc();
            zoneintromusictable.LoadDB(connection);
            zoneintromusictable.SaveDBC("D:/A/DBC/temp/DBFilesClient/ZoneIntroMusicTable.dbc"); }

        public static void zonemusic(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            zonemusicdbc zonemusic = new zonemusicdbc();
            zonemusic.LoadDB(connection);
            zonemusic.SaveDBC("D:/A/DBC/temp/DBFilesClient/ZoneMusic.dbc"); }
    }
}

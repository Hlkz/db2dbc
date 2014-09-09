using System;
using MySql.Data.MySqlClient;

namespace DBtoDBC
{
    class DBCExtract
    {
        public static void achievement_criteria(MySqlConnection connection) {
            achievement_criteriadbc achievement_criteria = new achievement_criteriadbc();
            achievement_criteria.LoadDB(connection);
            achievement_criteria.SaveDBC("D:/A/DBC/temp/DBFilesClient/Achievement_Criteria.dbc"); }

        public static void achievement(MySqlConnection connection) {
            achievementdbc achievement = new achievementdbc();
            achievement.LoadDB(connection);
            achievement.SaveDBC("D:/A/DBC/temp/DBFilesClient/Achievement.dbc"); }

        public static void areagroup(MySqlConnection connection) {
            areagroupdbc areagroup = new areagroupdbc();
            areagroup.LoadDB(connection);
            areagroup.SaveDBC("D:/A/DBC/temp/DBFilesClient/AreaGroup.dbc"); }

        public static void areapoi(MySqlConnection connection) {
            areapoidbc areapoi = new areapoidbc();
            areapoi.LoadDB(connection);
            areapoi.SaveDBC("D:/A/DBC/temp/DBFilesClient/AreaPOI.dbc"); }

        public static void areatable(MySqlConnection connection) {
            areatabledbc areatable = new areatabledbc();
            areatable.LoadDB(connection);
            areatable.SaveDBC("D:/A/DBC/temp/DBFilesClient/AreaTable.dbc"); }

        public static void areatrigger(MySqlConnection connection) {
            areatriggerdbc areatrigger = new areatriggerdbc();
            areatrigger.LoadDB(connection);
            areatrigger.SaveDBC("D:/A/DBC/temp/DBFilesClient/AreaTrigger.dbc"); }

        public static void auctionhouse(MySqlConnection connection) {
            auctionhousedbc auctionhouse = new auctionhousedbc();
            auctionhouse.LoadDB(connection);
            auctionhouse.SaveDBC("D:/A/DBC/temp/DBFilesClient/AuctionHouse.dbc"); }

        public static void bankbagslotprices(MySqlConnection connection) {
            bankbagslotpricesdbc bankbagslotprices = new bankbagslotpricesdbc();
            bankbagslotprices.LoadDB(connection);
            bankbagslotprices.SaveDBC("D:/A/DBC/temp/DBFilesClient/BankBagSlotPrices.dbc"); }

        public static void bannedaddons(MySqlConnection connection) {
            bannedaddonsdbc bannedaddons = new bannedaddonsdbc();
            bannedaddons.LoadDB(connection);
            bannedaddons.SaveDBC("D:/A/DBC/temp/DBFilesClient/BannedAddOns.dbc"); }

        public static void barbershopstyle(MySqlConnection connection) {
            barbershopstyledbc barbershopstyle = new barbershopstyledbc();
            barbershopstyle.LoadDB(connection);
            barbershopstyle.SaveDBC("D:/A/DBC/temp/DBFilesClient/BarberShopStyle.dbc"); }

        public static void battlemasterlist(MySqlConnection connection) {
            battlemasterlistdbc battlemasterlist = new battlemasterlistdbc();
            battlemasterlist.LoadDB(connection);
            battlemasterlist.SaveDBC("D:/A/DBC/temp/DBFilesClient/BattlemasterList.dbc"); }

        public static void chartitles(MySqlConnection connection) {
            chartitlesdbc chartitles = new chartitlesdbc();
            chartitles.LoadDB(connection);
            chartitles.SaveDBC("D:/A/DBC/temp/DBFilesClient/CharTitles.dbc"); }

        public static void chatchannels(MySqlConnection connection) {
            chatchannelsdbc chatchannels = new chatchannelsdbc();
            chatchannels.LoadDB(connection);
            chatchannels.SaveDBC("D:/A/DBC/temp/DBFilesClient/ChatChannels.dbc"); }

        public static void chrclasses(MySqlConnection connection) {
            chrclassesdbc chrclasses = new chrclassesdbc();
            chrclasses.LoadDB(connection);
            chrclasses.SaveDBC("D:/A/DBC/temp/DBFilesClient/ChrClasses.dbc"); }

        public static void chrraces(MySqlConnection connection) {
            chrracesdbc chrraces = new chrracesdbc();
            chrraces.LoadDB(connection);
            chrraces.SaveDBC("D:/A/DBC/temp/DBFilesClient/ChrRaces.dbc"); }

        public static void cinematicsequences(MySqlConnection connection) {
            cinematicsequencesdbc cinematicsequences = new cinematicsequencesdbc();
            cinematicsequences.LoadDB(connection);
            cinematicsequences.SaveDBC("D:/A/DBC/temp/DBFilesClient/CinematicSequences.dbc"); }

        public static void creaturedisplayinfo(MySqlConnection connection) {
            creaturedisplayinfodbc creaturedisplayinfo = new creaturedisplayinfodbc();
            creaturedisplayinfo.LoadDB(connection);
            creaturedisplayinfo.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureDisplayInfo.dbc"); }

        public static void creaturedisplayinfoextra(MySqlConnection connection) {
            creaturedisplayinfoextradbc creaturedisplayinfoextra = new creaturedisplayinfoextradbc();
            creaturedisplayinfoextra.LoadDB(connection);
            creaturedisplayinfoextra.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureDisplayInfoExtra.dbc"); }

        public static void creaturefamily(MySqlConnection connection) {
            creaturefamilydbc creaturefamily = new creaturefamilydbc();
            creaturefamily.LoadDB(connection);
            creaturefamily.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureFamily.dbc"); }

        public static void creaturemodeldata(MySqlConnection connection) {
            creaturemodeldatadbc creaturemodeldata = new creaturemodeldatadbc();
            creaturemodeldata.LoadDB(connection);
            creaturemodeldata.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureModelData.dbc"); }

        public static void creaturespelldata(MySqlConnection connection) {
            creaturespelldatadbc creaturespelldata = new creaturespelldatadbc();
            creaturespelldata.LoadDB(connection);
            creaturespelldata.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureSpellData.dbc"); }

        public static void creaturetype(MySqlConnection connection) {
            creaturetypedbc creaturetype = new creaturetypedbc();
            creaturetype.LoadDB(connection);
            creaturetype.SaveDBC("D:/A/DBC/temp/DBFilesClient/CreatureType.dbc"); }

        public static void currencytypes(MySqlConnection connection) {
            currencytypesdbc currencytypes = new currencytypesdbc();
            currencytypes.LoadDB(connection);
            currencytypes.SaveDBC("D:/A/DBC/temp/DBFilesClient/CurrencyTypes.dbc"); }

        public static void destructiblemodeldata(MySqlConnection connection) {
            destructiblemodeldatadbc destructiblemodeldata = new destructiblemodeldatadbc();
            destructiblemodeldata.LoadDB(connection);
            destructiblemodeldata.SaveDBC("D:/A/DBC/temp/DBFilesClient/DestructibleModelData.dbc"); }

        public static void dungeonencounter(MySqlConnection connection) {
            dungeonencounterdbc dungeonencounter = new dungeonencounterdbc();
            dungeonencounter.LoadDB(connection);
            dungeonencounter.SaveDBC("D:/A/DBC/temp/DBFilesClient/DungeonEncounter.dbc"); }

        public static void durabilitycosts(MySqlConnection connection) {
            durabilitycostsdbc durabilitycosts = new durabilitycostsdbc();
            durabilitycosts.LoadDB(connection);
            durabilitycosts.SaveDBC("D:/A/DBC/temp/DBFilesClient/DurabilityCosts.dbc"); }

        public static void durabilityquality(MySqlConnection connection) {
            durabilityqualitydbc durabilityquality = new durabilityqualitydbc();
            durabilityquality.LoadDB(connection);
            durabilityquality.SaveDBC("D:/A/DBC/temp/DBFilesClient/DurabilityQuality.dbc"); }

        public static void emotes(MySqlConnection connection) {
            emotesdbc emotes = new emotesdbc();
            emotes.LoadDB(connection);
            emotes.SaveDBC("D:/A/DBC/temp/DBFilesClient/Emotes.dbc"); }

        public static void emotestext(MySqlConnection connection) {
            emotestextdbc emotestext = new emotestextdbc();
            emotestext.LoadDB(connection);
            emotestext.SaveDBC("D:/A/DBC/temp/DBFilesClient/EmotesText.dbc"); }

        public static void faction(MySqlConnection connection) {
            factiondbc faction = new factiondbc();
            faction.LoadDB(connection);
            faction.SaveDBC("D:/A/DBC/temp/DBFilesClient/Faction.dbc"); }

        public static void factiontemplate(MySqlConnection connection) {
            factiontemplatedbc factiontemplate = new factiontemplatedbc();
            factiontemplate.LoadDB(connection);
            factiontemplate.SaveDBC("D:/A/DBC/temp/DBFilesClient/FactionTemplate.dbc"); }

        public static void gameobjectdisplayinfo(MySqlConnection connection) {
            gameobjectdisplayinfodbc gameobjectdisplayinfo = new gameobjectdisplayinfodbc();
            gameobjectdisplayinfo.LoadDB(connection);
            gameobjectdisplayinfo.SaveDBC("D:/A/DBC/temp/DBFilesClient/gameobjectdisplayinfo.dbc"); }

        public static void gemproperties(MySqlConnection connection) {
            gempropertiesdbc gemproperties = new gempropertiesdbc();
            gemproperties.LoadDB(connection);
            gemproperties.SaveDBC("D:/A/DBC/temp/DBFilesClient/GemProperties.dbc"); }

        public static void glyphproperties(MySqlConnection connection) {
            glyphpropertiesdbc glyphproperties = new glyphpropertiesdbc();
            glyphproperties.LoadDB(connection);
            glyphproperties.SaveDBC("D:/A/DBC/temp/DBFilesClient/GlyphProperties.dbc"); }

        public static void glyphslot(MySqlConnection connection) {
            glyphslotdbc glyphslot = new glyphslotdbc();
            glyphslot.LoadDB(connection);
            glyphslot.SaveDBC("D:/A/DBC/temp/DBFilesClient/GlyphSlot.dbc"); }

        public static void holidays(MySqlConnection connection) {
            holidaysdbc holidays = new holidaysdbc();
            holidays.LoadDB(connection);
            holidays.SaveDBC("D:/A/DBC/temp/DBFilesClient/Holidays.dbc"); }

        public static void itembagfamily(MySqlConnection connection) {
            itembagfamilydbc itembagfamily = new itembagfamilydbc();
            itembagfamily.LoadDB(connection);
            itembagfamily.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemBagFamily.dbc"); }

        public static void itemextendedcost(MySqlConnection connection) {
            itemextendedcostdbc itemextendedcost = new itemextendedcostdbc();
            itemextendedcost.LoadDB(connection);
            itemextendedcost.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemExtendedCost.dbc"); }

        public static void itemlimitcategory(MySqlConnection connection) {
            itemlimitcategorydbc itemlimitcategory = new itemlimitcategorydbc();
            itemlimitcategory.LoadDB(connection);
            itemlimitcategory.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemLimitCategory.dbc"); }

        public static void itemrandomproperties(MySqlConnection connection) {
            itemrandompropertiesdbc itemrandomproperties = new itemrandompropertiesdbc();
            itemrandomproperties.LoadDB(connection);
            itemrandomproperties.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemRandomProperties.dbc"); }

        public static void itemrandomsuffix(MySqlConnection connection) {
            itemrandomsuffixdbc itemrandomsuffix = new itemrandomsuffixdbc();
            itemrandomsuffix.LoadDB(connection);
            itemrandomsuffix.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemRandomSuffix.dbc"); }

        public static void itemset(MySqlConnection connection) {
            itemsetdbc itemset = new itemsetdbc();
            itemset.LoadDB(connection);
            itemset.SaveDBC("D:/A/DBC/temp/DBFilesClient/ItemSet.dbc"); }

        public static void lfgdungeons(MySqlConnection connection) {
            lfgdungeonsdbc lfgdungeons = new lfgdungeonsdbc();
            lfgdungeons.LoadDB(connection);
            lfgdungeons.SaveDBC("D:/A/DBC/temp/DBFilesClient/LFGDungeons.dbc"); }

        public static void light(MySqlConnection connection) {
            lightdbc light = new lightdbc();
            light.LoadDB(connection);
            light.SaveDBC("D:/A/DBC/temp/DBFilesClient/Light.dbc"); }

        public static void liquidtype(MySqlConnection connection) {
            liquidtypedbc liquidtype = new liquidtypedbc();
            liquidtype.LoadDB(connection);
            liquidtype.SaveDBC("D:/A/DBC/temp/DBFilesClient/Liquidtype.dbc"); }

        public static void mailtemplate(MySqlConnection connection) {
            mailtemplatedbc mailtemplate = new mailtemplatedbc();
            mailtemplate.LoadDB(connection);
            mailtemplate.SaveDBC("D:/A/DBC/temp/DBFilesClient/MailTemplate.dbc"); }

        public static void map(MySqlConnection connection) {
            mapdbc map = new mapdbc();
            map.LoadDB(connection);
            map.SaveDBC("D:/A/DBC/temp/DBFilesClient/Map.dbc"); }

        public static void mapdifficulty(MySqlConnection connection) {
            mapdifficultydbc mapdifficulty = new mapdifficultydbc();
            mapdifficulty.LoadDB(connection);
            mapdifficulty.SaveDBC("D:/A/DBC/temp/DBFilesClient/MapDifficulty.dbc"); }

        public static void movie(MySqlConnection connection) {
            moviedbc movie = new moviedbc();
            movie.LoadDB(connection);
            movie.SaveDBC("D:/A/DBC/temp/DBFilesClient/Movie.dbc"); }

        public static void overridespelldata(MySqlConnection connection) {
            overridespelldatadbc overridespelldata = new overridespelldatadbc();
            overridespelldata.LoadDB(connection);
            overridespelldata.SaveDBC("D:/A/DBC/temp/DBFilesClient/OverrideSpellData.dbc"); }

        public static void powerdisplay(MySqlConnection connection) {
            powerdisplaydbc powerdisplay = new powerdisplaydbc();
            powerdisplay.LoadDB(connection);
            powerdisplay.SaveDBC("D:/A/DBC/temp/DBFilesClient/PowerDisplay.dbc"); }

        public static void pvpdifficulty(MySqlConnection connection) {
            pvpdifficultydbc pvpdifficulty = new pvpdifficultydbc();
            pvpdifficulty.LoadDB(connection);
            pvpdifficulty.SaveDBC("D:/A/DBC/temp/DBFilesClient/PvpDifficulty.dbc"); }

        public static void questfactionrew(MySqlConnection connection) {
            questfactionrewdbc questfactionrew = new questfactionrewdbc();
            questfactionrew.LoadDB(connection);
            questfactionrew.SaveDBC("D:/A/DBC/temp/DBFilesClient/QuestFactionReward.dbc"); }

        public static void questsort(MySqlConnection connection) {
            questsortdbc questsort = new questsortdbc();
            questsort.LoadDB(connection);
            questsort.SaveDBC("D:/A/DBC/temp/DBFilesClient/QuestSort.dbc"); }

        public static void questxp(MySqlConnection connection) {
            questxpdbc questxp = new questxpdbc();
            questxp.LoadDB(connection);
            questxp.SaveDBC("D:/A/DBC/temp/DBFilesClient/QuestXP.dbc"); }

        public static void randproppoints(MySqlConnection connection) {
            randproppointsdbc randproppoints = new randproppointsdbc();
            randproppoints.LoadDB(connection);
            randproppoints.SaveDBC("D:/A/DBC/temp/DBFilesClient/RandPropPoints.dbc"); }

        public static void scalingstatdistribution(MySqlConnection connection) {
            scalingstatdistributiondbc scalingstatdistribution = new scalingstatdistributiondbc();
            scalingstatdistribution.LoadDB(connection);
            scalingstatdistribution.SaveDBC("D:/A/DBC/temp/DBFilesClient/ScalingStatDistribution.dbc"); }

        public static void scalingstatvalues(MySqlConnection connection) {
            scalingstatvaluesdbc scalingstatvalues = new scalingstatvaluesdbc();
            scalingstatvalues.LoadDB(connection);
            scalingstatvalues.SaveDBC("D:/A/DBC/temp/DBFilesClient/ScalingStatValues.dbc"); }

        public static void skilllineability(MySqlConnection connection) {
            skilllineabilitydbc skilllineability = new skilllineabilitydbc();
            skilllineability.LoadDB(connection);
            skilllineability.SaveDBC("D:/A/DBC/temp/DBFilesClient/SkillLineAbility.dbc"); }

        public static void skillline(MySqlConnection connection) {
            skilllinedbc skillline = new skilllinedbc();
            skillline.LoadDB(connection);
            skillline.SaveDBC("D:/A/DBC/temp/DBFilesClient/SkillLine.dbc"); }

        public static void soundentries(MySqlConnection connection) {
            soundentriesdbc soundentries = new soundentriesdbc();
            soundentries.LoadDB(connection);
            soundentries.SaveDBC("D:/A/DBC/temp/DBFilesClient/SoundEntries.dbc"); }

        public static void spellcasttimes(MySqlConnection connection) {
            spellcasttimesdbc spellcasttimes = new spellcasttimesdbc();
            spellcasttimes.LoadDB(connection);
            spellcasttimes.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellCastTimes.dbc"); }

        public static void spellcategory(MySqlConnection connection) {
            spellcategorydbc spellcategory = new spellcategorydbc();
            spellcategory.LoadDB(connection);
            spellcategory.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellCategory.dbc"); }

        public static void spell(MySqlConnection connection) {
            spelldbc spell = new spelldbc();
            spell.LoadDB(connection);
            spell.SaveDBC("D:/A/DBC/temp/DBFilesClient/Spell.dbc"); }

        public static void spelldifficulty(MySqlConnection connection) {
            spelldifficultydbc spelldifficulty = new spelldifficultydbc();
            spelldifficulty.LoadDB(connection);
            spelldifficulty.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellDifficulty.dbc"); }

        public static void spellduration(MySqlConnection connection) {
            spelldurationdbc spellduration = new spelldurationdbc();
            spellduration.LoadDB(connection);
            spellduration.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellDuration.dbc"); }

        public static void spellfocusobject(MySqlConnection connection) {
            spellfocusobjectdbc spellfocusobject = new spellfocusobjectdbc();
            spellfocusobject.LoadDB(connection);
            spellfocusobject.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellFocusObject.dbc"); }

        public static void spellitemenchantmentcondition(MySqlConnection connection) {
            spellitemenchantmentconditiondbc spellitemenchantmentcondition = new spellitemenchantmentconditiondbc();
            spellitemenchantmentcondition.LoadDB(connection);
            spellitemenchantmentcondition.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellItemEnchantmentCondition.dbc"); }

        public static void spellitemenchantment(MySqlConnection connection) {
            spellitemenchantmentdbc spellitemenchantment = new spellitemenchantmentdbc();
            spellitemenchantment.LoadDB(connection);
            spellitemenchantment.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellItemEnchantment.dbc"); }

        public static void spellradius(MySqlConnection connection) {
            spellradiusdbc spellradius = new spellradiusdbc();
            spellradius.LoadDB(connection);
            spellradius.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellRadius.dbc"); }

        public static void spellrange(MySqlConnection connection) {
            spellrangedbc spellrange = new spellrangedbc();
            spellrange.LoadDB(connection);
            spellrange.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellRange.dbc"); }

        public static void spellrunecost(MySqlConnection connection) {
            spellrunecostdbc spellrunecost = new spellrunecostdbc();
            spellrunecost.LoadDB(connection);
            spellrunecost.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellRuneCost.dbc"); }

        public static void spellshapeshiftform(MySqlConnection connection) {
            spellshapeshiftformdbc spellshapeshiftform = new spellshapeshiftformdbc();
            spellshapeshiftform.LoadDB(connection);
            spellshapeshiftform.SaveDBC("D:/A/DBC/temp/DBFilesClient/SpellShapeshiftForm.dbc"); }

        public static void stableslotprices(MySqlConnection connection) {
            stableslotpricesdbc stableslotprices = new stableslotpricesdbc();
            stableslotprices.LoadDB(connection);
            stableslotprices.SaveDBC("D:/A/DBC/temp/DBFilesClient/StableSlotPrices.dbc"); }

        public static void summonproperties(MySqlConnection connection) {
            summonpropertiesdbc summonproperties = new summonpropertiesdbc();
            summonproperties.LoadDB(connection);
            summonproperties.SaveDBC("D:/A/DBC/temp/DBFilesClient/SummonProperties.dbc"); }

        public static void talent(MySqlConnection connection) {
            talentdbc talent = new talentdbc();
            talent.LoadDB(connection);
            talent.SaveDBC("D:/A/DBC/temp/DBFilesClient/Talent.dbc"); }

        public static void talenttab(MySqlConnection connection) {
            talenttabdbc talenttab = new talenttabdbc();
            talenttab.LoadDB(connection);
            talenttab.SaveDBC("D:/A/DBC/temp/DBFilesClient/TalentTab.dbc"); }

        public static void taxinodes(MySqlConnection connection) {
            taxinodesdbc taxinodes = new taxinodesdbc();
            taxinodes.LoadDB(connection);
            taxinodes.SaveDBC("D:/A/DBC/temp/DBFilesClient/TaxiNodes.dbc"); }

        public static void taxipath(MySqlConnection connection) {
            taxipathdbc taxipath = new taxipathdbc();
            taxipath.LoadDB(connection);
            taxipath.SaveDBC("D:/A/DBC/temp/DBFilesClient/TaxiPath.dbc"); }

        public static void taxipathnode(MySqlConnection connection) {
            taxipathnodedbc taxipathnode = new taxipathnodedbc();
            taxipathnode.LoadDB(connection);
            taxipathnode.SaveDBC("D:/A/DBC/temp/DBFilesClient/TaxiPathNode.dbc"); }

        public static void teamcontributionpoints(MySqlConnection connection) {
            teamcontributionpointsdbc teamcontributionpoints = new teamcontributionpointsdbc();
            teamcontributionpoints.LoadDB(connection);
            teamcontributionpoints.SaveDBC("D:/A/DBC/temp/DBFilesClient/TeamContributionPoints.dbc"); }

        public static void totemcategory(MySqlConnection connection) {
            totemcategorydbc totemcategory = new totemcategorydbc();
            totemcategory.LoadDB(connection);
            totemcategory.SaveDBC("D:/A/DBC/temp/DBFilesClient/TotemCategory.dbc"); }

        public static void transportanimation(MySqlConnection connection) {
            transportanimationdbc transportanimation = new transportanimationdbc();
            transportanimation.LoadDB(connection);
            transportanimation.SaveDBC("D:/A/DBC/temp/DBFilesClient/TransportAnimation.dbc"); }

        public static void transportrotation(MySqlConnection connection) {
            transportrotationdbc transportrotation = new transportrotationdbc();
            transportrotation.LoadDB(connection);
            transportrotation.SaveDBC("D:/A/DBC/temp/DBFilesClient/TransportRotation.dbc"); }

        public static void vehicle(MySqlConnection connection) {
            vehicledbc vehicle = new vehicledbc();
            vehicle.LoadDB(connection);
            vehicle.SaveDBC("D:/A/DBC/temp/DBFilesClient/Vehicle.dbc"); }

        public static void vehicleseat(MySqlConnection connection) {
            vehicleseatdbc vehicleseat = new vehicleseatdbc();
            vehicleseat.LoadDB(connection);
            vehicleseat.SaveDBC("D:/A/DBC/temp/DBFilesClient/VehicleSeat.dbc"); }

        public static void wmoareatable(MySqlConnection connection) {
            wmoareatabledbc wmoareatable = new wmoareatabledbc();
            wmoareatable.LoadDB(connection);
            wmoareatable.SaveDBC("D:/A/DBC/temp/DBFilesClient/WMOAreaTable.dbc"); }

        public static void worldmapoverlay(MySqlConnection connection) {
            worldmapoverlaydbc worldmapoverlay = new worldmapoverlaydbc();
            worldmapoverlay.LoadDB(connection);
            worldmapoverlay.SaveDBC("D:/A/DBC/temp/DBFilesClient/WorldMapOverlay.dbc"); }

        public static void worldsafelocsdbc(MySqlConnection connection) {
            worldsafelocsdbc worldsafelocs = new worldsafelocsdbc();
            worldsafelocs.LoadDB(connection);
            worldsafelocs.SaveDBC("D:/A/DBC/temp/DBFilesClient/WorldSafeLocs.dbc"); }
    }
}

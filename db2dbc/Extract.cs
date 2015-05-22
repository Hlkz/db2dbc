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
            achievement_criteria.SaveDBC(DB2DBC.OutPath + "\\Achievement_Criteria.dbc"); }

        public static void achievement(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            achievementdbc achievement = new achievementdbc();
            achievement.LoadDB(connection);
            achievement.SaveDBC(DB2DBC.OutPath + "\\Achievement.dbc"); }

        public static void areagroup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            areagroupdbc areagroup = new areagroupdbc();
            areagroup.LoadDB(connection);
            areagroup.SaveDBC(DB2DBC.OutPath + "\\AreaGroup.dbc"); }

        public static void areapoi(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            areapoidbc areapoi = new areapoidbc();
            areapoi.LoadDB(connection);
            areapoi.SaveDBC(DB2DBC.OutPath + "\\AreaPOI.dbc"); }

        public static void areatable(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            areatabledbc areatable = new areatabledbc();
            areatable.LoadDB(connection);
            areatable.SaveDBC(DB2DBC.OutPath + "\\AreaTable.dbc"); }

        public static void areatrigger(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            areatriggerdbc areatrigger = new areatriggerdbc();
            areatrigger.LoadDB(connection);
            areatrigger.SaveDBC(DB2DBC.OutPath + "\\AreaTrigger.dbc"); }

        public static void auctionhouse(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            auctionhousedbc auctionhouse = new auctionhousedbc();
            auctionhouse.LoadDB(connection);
            auctionhouse.SaveDBC(DB2DBC.OutPath + "\\AuctionHouse.dbc"); }

        public static void bankbagslotprices(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            bankbagslotpricesdbc bankbagslotprices = new bankbagslotpricesdbc();
            bankbagslotprices.LoadDB(connection);
            bankbagslotprices.SaveDBC(DB2DBC.OutPath + "\\BankBagSlotPrices.dbc"); }

        public static void bannedaddons(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            bannedaddonsdbc bannedaddons = new bannedaddonsdbc();
            bannedaddons.LoadDB(connection);
            bannedaddons.SaveDBC(DB2DBC.OutPath + "\\BannedAddOns.dbc"); }

        public static void barbershopstyle(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            barbershopstyledbc barbershopstyle = new barbershopstyledbc();
            barbershopstyle.LoadDB(connection);
            barbershopstyle.SaveDBC(DB2DBC.OutPath + "\\BarberShopStyle.dbc"); }

        public static void battlemasterlist(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            battlemasterlistdbc battlemasterlist = new battlemasterlistdbc();
            battlemasterlist.LoadDB(connection);
            battlemasterlist.SaveDBC(DB2DBC.OutPath + "\\BattlemasterList.dbc"); }

        public static void charsections(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            charsectionsdbc charsections = new charsectionsdbc();
            charsections.LoadDB(connection);
            charsections.SaveDBC(DB2DBC.OutPath + "\\CharSections.dbc"); }

        public static void charstartoutfit(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            charstartoutfitdbc charstartoutfit = new charstartoutfitdbc();
            charstartoutfit.LoadDB(connection);
            charstartoutfit.SaveDBC(DB2DBC.OutPath + "\\CharStartOutfit.dbc"); }

        public static void chartitles(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            chartitlesdbc chartitles = new chartitlesdbc();
            chartitles.LoadDB(connection);
            chartitles.SaveDBC(DB2DBC.OutPath + "\\CharTitles.dbc"); }

        public static void chatchannels(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            chatchannelsdbc chatchannels = new chatchannelsdbc();
            chatchannels.LoadDB(connection);
            chatchannels.SaveDBC(DB2DBC.OutPath + "\\ChatChannels.dbc"); }

        public static void chrclasses(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            chrclassesdbc chrclasses = new chrclassesdbc();
            chrclasses.LoadDB(connection);
            chrclasses.SaveDBC(DB2DBC.OutPath + "\\ChrClasses.dbc"); }

        public static void chrraces(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            chrracesdbc chrraces = new chrracesdbc();
            chrraces.LoadDB(connection);
            chrraces.SaveDBC(DB2DBC.OutPath + "\\ChrRaces.dbc"); }

        public static void cinematicsequences(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            cinematicsequencesdbc cinematicsequences = new cinematicsequencesdbc();
            cinematicsequences.LoadDB(connection);
            cinematicsequences.SaveDBC(DB2DBC.OutPath + "\\CinematicSequences.dbc"); }

        public static void creaturedisplayinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturedisplayinfodbc creaturedisplayinfo = new creaturedisplayinfodbc();
            creaturedisplayinfo.LoadDB(connection);
            creaturedisplayinfo.SaveDBC(DB2DBC.OutPath + "\\CreatureDisplayInfo.dbc"); }

        public static void creaturedisplayinfoextra(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturedisplayinfoextradbc creaturedisplayinfoextra = new creaturedisplayinfoextradbc();
            creaturedisplayinfoextra.LoadDB(connection);
            creaturedisplayinfoextra.SaveDBC(DB2DBC.OutPath + "\\CreatureDisplayInfoExtra.dbc"); }

        public static void creaturefamily(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturefamilydbc creaturefamily = new creaturefamilydbc();
            creaturefamily.LoadDB(connection);
            creaturefamily.SaveDBC(DB2DBC.OutPath + "\\CreatureFamily.dbc"); }

        public static void creaturemodeldata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturemodeldatadbc creaturemodeldata = new creaturemodeldatadbc();
            creaturemodeldata.LoadDB(connection);
            creaturemodeldata.SaveDBC(DB2DBC.OutPath + "\\CreatureModelData.dbc"); }

        public static void creaturespelldata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturespelldatadbc creaturespelldata = new creaturespelldatadbc();
            creaturespelldata.LoadDB(connection);
            creaturespelldata.SaveDBC(DB2DBC.OutPath + "\\CreatureSpellData.dbc"); }

        public static void creaturetype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            creaturetypedbc creaturetype = new creaturetypedbc();
            creaturetype.LoadDB(connection);
            creaturetype.SaveDBC(DB2DBC.OutPath + "\\CreatureType.dbc"); }

        public static void currencytypes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            currencytypesdbc currencytypes = new currencytypesdbc();
            currencytypes.LoadDB(connection);
            currencytypes.SaveDBC(DB2DBC.OutPath + "\\CurrencyTypes.dbc"); }

        public static void destructiblemodeldata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            destructiblemodeldatadbc destructiblemodeldata = new destructiblemodeldatadbc();
            destructiblemodeldata.LoadDB(connection);
            destructiblemodeldata.SaveDBC(DB2DBC.OutPath + "\\DestructibleModelData.dbc"); }

        public static void dungeonencounter(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            dungeonencounterdbc dungeonencounter = new dungeonencounterdbc();
            dungeonencounter.LoadDB(connection);
            dungeonencounter.SaveDBC(DB2DBC.OutPath + "\\DungeonEncounter.dbc"); }

        public static void durabilitycosts(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            durabilitycostsdbc durabilitycosts = new durabilitycostsdbc();
            durabilitycosts.LoadDB(connection);
            durabilitycosts.SaveDBC(DB2DBC.OutPath + "\\DurabilityCosts.dbc"); }

        public static void durabilityquality(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            durabilityqualitydbc durabilityquality = new durabilityqualitydbc();
            durabilityquality.LoadDB(connection);
            durabilityquality.SaveDBC(DB2DBC.OutPath + "\\DurabilityQuality.dbc"); }

        public static void emotes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            emotesdbc emotes = new emotesdbc();
            emotes.LoadDB(connection);
            emotes.SaveDBC(DB2DBC.OutPath + "\\Emotes.dbc"); }

        public static void emotestext(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            emotestextdbc emotestext = new emotestextdbc();
            emotestext.LoadDB(connection);
            emotestext.SaveDBC(DB2DBC.OutPath + "\\EmotesText.dbc"); }

        public static void faction(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            factiondbc faction = new factiondbc();
            faction.LoadDB(connection);
            faction.SaveDBC(DB2DBC.OutPath + "\\Faction.dbc"); }

        public static void factiontemplate(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            factiontemplatedbc factiontemplate = new factiontemplatedbc();
            factiontemplate.LoadDB(connection);
            factiontemplate.SaveDBC(DB2DBC.OutPath + "\\FactionTemplate.dbc"); }

        public static void gameobjectdisplayinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            gameobjectdisplayinfodbc gameobjectdisplayinfo = new gameobjectdisplayinfodbc();
            gameobjectdisplayinfo.LoadDB(connection);
            gameobjectdisplayinfo.SaveDBC(DB2DBC.OutPath + "\\gameobjectdisplayinfo.dbc"); }

        public static void gemproperties(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            gempropertiesdbc gemproperties = new gempropertiesdbc();
            gemproperties.LoadDB(connection);
            gemproperties.SaveDBC(DB2DBC.OutPath + "\\GemProperties.dbc"); }

        public static void glyphproperties(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            glyphpropertiesdbc glyphproperties = new glyphpropertiesdbc();
            glyphproperties.LoadDB(connection);
            glyphproperties.SaveDBC(DB2DBC.OutPath + "\\GlyphProperties.dbc"); }

        public static void glyphslot(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            glyphslotdbc glyphslot = new glyphslotdbc();
            glyphslot.LoadDB(connection);
            glyphslot.SaveDBC(DB2DBC.OutPath + "\\GlyphSlot.dbc"); }

        public static void holidays(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            holidaysdbc holidays = new holidaysdbc();
            holidays.LoadDB(connection);
            holidays.SaveDBC(DB2DBC.OutPath + "\\Holidays.dbc"); }
        
        public static void item(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itemdbc item = new itemdbc();
            item.LoadDB(connection);
            item.SaveDBC(DB2DBC.OutPath + "\\Item.dbc"); }

        public static void itembagfamily(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itembagfamilydbc itembagfamily = new itembagfamilydbc();
            itembagfamily.LoadDB(connection);
            itembagfamily.SaveDBC(DB2DBC.OutPath + "\\ItemBagFamily.dbc"); }

        public static void itemextendedcost(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itemextendedcostdbc itemextendedcost = new itemextendedcostdbc();
            itemextendedcost.LoadDB(connection);
            itemextendedcost.SaveDBC(DB2DBC.OutPath + "\\ItemExtendedCost.dbc"); }

        public static void itemlimitcategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itemlimitcategorydbc itemlimitcategory = new itemlimitcategorydbc();
            itemlimitcategory.LoadDB(connection);
            itemlimitcategory.SaveDBC(DB2DBC.OutPath + "\\ItemLimitCategory.dbc"); }

        public static void itemrandomproperties(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itemrandompropertiesdbc itemrandomproperties = new itemrandompropertiesdbc();
            itemrandomproperties.LoadDB(connection);
            itemrandomproperties.SaveDBC(DB2DBC.OutPath + "\\ItemRandomProperties.dbc"); }

        public static void itemrandomsuffix(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itemrandomsuffixdbc itemrandomsuffix = new itemrandomsuffixdbc();
            itemrandomsuffix.LoadDB(connection);
            itemrandomsuffix.SaveDBC(DB2DBC.OutPath + "\\ItemRandomSuffix.dbc"); }

        public static void itemset(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            itemsetdbc itemset = new itemsetdbc();
            itemset.LoadDB(connection);
            itemset.SaveDBC(DB2DBC.OutPath + "\\ItemSet.dbc"); }

        public static void lfgdungeons(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            lfgdungeonsdbc lfgdungeons = new lfgdungeonsdbc();
            lfgdungeons.LoadDB(connection);
            lfgdungeons.SaveDBC(DB2DBC.OutPath + "\\LFGDungeons.dbc"); }

        public static void light(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            lightdbc light = new lightdbc();
            light.LoadDB(connection);
            light.SaveDBC(DB2DBC.OutPath + "\\Light.dbc"); }
        
        public static void liquidtype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            liquidtypedbc liquidtype = new liquidtypedbc();
            liquidtype.LoadDB(connection);
            liquidtype.SaveDBC(DB2DBC.OutPath + "\\Liquidtype.dbc"); }

        public static void lockd(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            lockdbc lockd = new lockdbc();
            lockd.LoadDB(connection);
            lockd.SaveDBC(DB2DBC.OutPath + "\\Lock.dbc"); }

        public static void mailtemplate(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            mailtemplatedbc mailtemplate = new mailtemplatedbc();
            mailtemplate.LoadDB(connection);
            mailtemplate.SaveDBC(DB2DBC.OutPath + "\\MailTemplate.dbc"); }

        public static void map(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            mapdbc map = new mapdbc();
            map.LoadDB(connection);
            map.SaveDBC(DB2DBC.OutPath + "\\Map.dbc"); }

        public static void mapdifficulty(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            mapdifficultydbc mapdifficulty = new mapdifficultydbc();
            mapdifficulty.LoadDB(connection);
            mapdifficulty.SaveDBC(DB2DBC.OutPath + "\\MapDifficulty.dbc"); }

        public static void movie(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            moviedbc movie = new moviedbc();
            movie.LoadDB(connection);
            movie.SaveDBC(DB2DBC.OutPath + "\\Movie.dbc"); }

        public static void overridespelldata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            overridespelldatadbc overridespelldata = new overridespelldatadbc();
            overridespelldata.LoadDB(connection);
            overridespelldata.SaveDBC(DB2DBC.OutPath + "\\OverrideSpellData.dbc"); }

        public static void powerdisplay(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            powerdisplaydbc powerdisplay = new powerdisplaydbc();
            powerdisplay.LoadDB(connection);
            powerdisplay.SaveDBC(DB2DBC.OutPath + "\\PowerDisplay.dbc"); }

        public static void pvpdifficulty(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            pvpdifficultydbc pvpdifficulty = new pvpdifficultydbc();
            pvpdifficulty.LoadDB(connection);
            pvpdifficulty.SaveDBC(DB2DBC.OutPath + "\\PvpDifficulty.dbc"); }

        public static void questfactionrew(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            questfactionrewarddbc questfactionreward = new questfactionrewarddbc();
            questfactionreward.LoadDB(connection);
            questfactionreward.SaveDBC(DB2DBC.OutPath + "\\QuestFactionReward.dbc"); }

        public static void questsort(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            questsortdbc questsort = new questsortdbc();
            questsort.LoadDB(connection);
            questsort.SaveDBC(DB2DBC.OutPath + "\\QuestSort.dbc"); }

        public static void questxp(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            questxpdbc questxp = new questxpdbc();
            questxp.LoadDB(connection);
            questxp.SaveDBC(DB2DBC.OutPath + "\\QuestXP.dbc"); }

        public static void randproppoints(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            randproppointsdbc randproppoints = new randproppointsdbc();
            randproppoints.LoadDB(connection);
            randproppoints.SaveDBC(DB2DBC.OutPath + "\\RandPropPoints.dbc"); }

        public static void scalingstatdistribution(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            scalingstatdistributiondbc scalingstatdistribution = new scalingstatdistributiondbc();
            scalingstatdistribution.LoadDB(connection);
            scalingstatdistribution.SaveDBC(DB2DBC.OutPath + "\\ScalingStatDistribution.dbc"); }

        public static void scalingstatvalues(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            scalingstatvaluesdbc scalingstatvalues = new scalingstatvaluesdbc();
            scalingstatvalues.LoadDB(connection);
            scalingstatvalues.SaveDBC(DB2DBC.OutPath + "\\ScalingStatValues.dbc"); }

        public static void skilllineability(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            skilllineabilitydbc skilllineability = new skilllineabilitydbc();
            skilllineability.LoadDB(connection);
            skilllineability.SaveDBC(DB2DBC.OutPath + "\\SkillLineAbility.dbc"); }

        public static void skillline(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            skilllinedbc skillline = new skilllinedbc();
            skillline.LoadDB(connection);
            skillline.SaveDBC(DB2DBC.OutPath + "\\SkillLine.dbc"); }
        
        public static void skillraceclassinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            skillraceclassinfodbc skillraceclassinfo = new skillraceclassinfodbc();
            skillraceclassinfo.LoadDB(connection);
            skillraceclassinfo.SaveDBC(DB2DBC.OutPath + "\\SkillRaceClassInfo.dbc"); }

        public static void skilltiers(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            skilltiersdbc skilltiers = new skilltiersdbc();
            skilltiers.LoadDB(connection);
            skilltiers.SaveDBC(DB2DBC.OutPath + "\\SkillTiers.dbc"); }

        public static void soundentries(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            soundentriesdbc soundentries = new soundentriesdbc();
            soundentries.LoadDB(connection);
            soundentries.SaveDBC(DB2DBC.OutPath + "\\SoundEntries.dbc"); }

        public static void spellcasttimes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellcasttimesdbc spellcasttimes = new spellcasttimesdbc();
            spellcasttimes.LoadDB(connection);
            spellcasttimes.SaveDBC(DB2DBC.OutPath + "\\SpellCastTimes.dbc"); }

        public static void spellcategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellcategorydbc spellcategory = new spellcategorydbc();
            spellcategory.LoadDB(connection);
            spellcategory.SaveDBC(DB2DBC.OutPath + "\\SpellCategory.dbc"); }

        public static void spell(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spelldbc spell = new spelldbc();
            spell.LoadDB(connection);
            spell.SaveDBC(DB2DBC.OutPath + "\\Spell.dbc"); }

        public static void spelldifficulty(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spelldifficultydbc spelldifficulty = new spelldifficultydbc();
            spelldifficulty.LoadDB(connection);
            spelldifficulty.SaveDBC(DB2DBC.OutPath + "\\SpellDifficulty.dbc"); }

        public static void spellduration(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spelldurationdbc spellduration = new spelldurationdbc();
            spellduration.LoadDB(connection);
            spellduration.SaveDBC(DB2DBC.OutPath + "\\SpellDuration.dbc"); }

        public static void spellfocusobject(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellfocusobjectdbc spellfocusobject = new spellfocusobjectdbc();
            spellfocusobject.LoadDB(connection);
            spellfocusobject.SaveDBC(DB2DBC.OutPath + "\\SpellFocusObject.dbc"); }

        public static void spellitemenchantmentcondition(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellitemenchantmentconditiondbc spellitemenchantmentcondition = new spellitemenchantmentconditiondbc();
            spellitemenchantmentcondition.LoadDB(connection);
            spellitemenchantmentcondition.SaveDBC(DB2DBC.OutPath + "\\SpellItemEnchantmentCondition.dbc"); }

        public static void spellitemenchantment(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellitemenchantmentdbc spellitemenchantment = new spellitemenchantmentdbc();
            spellitemenchantment.LoadDB(connection);
            spellitemenchantment.SaveDBC(DB2DBC.OutPath + "\\SpellItemEnchantment.dbc"); }

        public static void spellradius(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellradiusdbc spellradius = new spellradiusdbc();
            spellradius.LoadDB(connection);
            spellradius.SaveDBC(DB2DBC.OutPath + "\\SpellRadius.dbc"); }

        public static void spellrange(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellrangedbc spellrange = new spellrangedbc();
            spellrange.LoadDB(connection);
            spellrange.SaveDBC(DB2DBC.OutPath + "\\SpellRange.dbc"); }

        public static void spellrunecost(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellrunecostdbc spellrunecost = new spellrunecostdbc();
            spellrunecost.LoadDB(connection);
            spellrunecost.SaveDBC(DB2DBC.OutPath + "\\SpellRuneCost.dbc"); }

        public static void spellshapeshiftform(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            spellshapeshiftformdbc spellshapeshiftform = new spellshapeshiftformdbc();
            spellshapeshiftform.LoadDB(connection);
            spellshapeshiftform.SaveDBC(DB2DBC.OutPath + "\\SpellShapeshiftForm.dbc"); }

        public static void stableslotprices(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            stableslotpricesdbc stableslotprices = new stableslotpricesdbc();
            stableslotprices.LoadDB(connection);
            stableslotprices.SaveDBC(DB2DBC.OutPath + "\\StableSlotPrices.dbc"); }

        public static void summonproperties(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            summonpropertiesdbc summonproperties = new summonpropertiesdbc();
            summonproperties.LoadDB(connection);
            summonproperties.SaveDBC(DB2DBC.OutPath + "\\SummonProperties.dbc"); }

        public static void talent(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            talentdbc talent = new talentdbc();
            talent.LoadDB(connection);
            talent.SaveDBC(DB2DBC.OutPath + "\\Talent.dbc"); }

        public static void talenttab(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            talenttabdbc talenttab = new talenttabdbc();
            talenttab.LoadDB(connection);
            talenttab.SaveDBC(DB2DBC.OutPath + "\\TalentTab.dbc"); }

        public static void taxinodes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            taxinodesdbc taxinodes = new taxinodesdbc();
            taxinodes.LoadDB(connection);
            taxinodes.SaveDBC(DB2DBC.OutPath + "\\TaxiNodes.dbc"); }

        public static void taxipath(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            taxipathdbc taxipath = new taxipathdbc();
            taxipath.LoadDB(connection);
            taxipath.SaveDBC(DB2DBC.OutPath + "\\TaxiPath.dbc"); }

        public static void taxipathnode(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            taxipathnodedbc taxipathnode = new taxipathnodedbc();
            taxipathnode.LoadDB(connection);
            taxipathnode.SaveDBC(DB2DBC.OutPath + "\\TaxiPathNode.dbc"); }

        public static void teamcontributionpoints(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            teamcontributionpointsdbc teamcontributionpoints = new teamcontributionpointsdbc();
            teamcontributionpoints.LoadDB(connection);
            teamcontributionpoints.SaveDBC(DB2DBC.OutPath + "\\TeamContributionPoints.dbc"); }

        public static void totemcategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            totemcategorydbc totemcategory = new totemcategorydbc();
            totemcategory.LoadDB(connection);
            totemcategory.SaveDBC(DB2DBC.OutPath + "\\TotemCategory.dbc"); }

        public static void transportanimation(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            transportanimationdbc transportanimation = new transportanimationdbc();
            transportanimation.LoadDB(connection);
            transportanimation.SaveDBC(DB2DBC.OutPath + "\\TransportAnimation.dbc"); }

        public static void transportrotation(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            transportrotationdbc transportrotation = new transportrotationdbc();
            transportrotation.LoadDB(connection);
            transportrotation.SaveDBC(DB2DBC.OutPath + "\\TransportRotation.dbc"); }

        public static void vehicle(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            vehicledbc vehicle = new vehicledbc();
            vehicle.LoadDB(connection);
            vehicle.SaveDBC(DB2DBC.OutPath + "\\Vehicle.dbc"); }

        public static void vehicleseat(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            vehicleseatdbc vehicleseat = new vehicleseatdbc();
            vehicleseat.LoadDB(connection);
            vehicleseat.SaveDBC(DB2DBC.OutPath + "\\VehicleSeat.dbc"); }

        public static void wmoareatable(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            wmoareatabledbc wmoareatable = new wmoareatabledbc();
            wmoareatable.LoadDB(connection);
            wmoareatable.SaveDBC(DB2DBC.OutPath + "\\WMOAreaTable.dbc"); }

        public static void worldmaparea(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            worldmapareadbc worldmaparea = new worldmapareadbc();
            worldmaparea.LoadDB(connection);
            worldmaparea.SaveDBC(DB2DBC.OutPath + "\\WorldMapArea.dbc"); }

        public static void worldmapoverlay(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            worldmapoverlaydbc worldmapoverlay = new worldmapoverlaydbc();
            worldmapoverlay.LoadDB(connection);
            worldmapoverlay.SaveDBC(DB2DBC.OutPath + "\\WorldMapOverlay.dbc"); }

        public static void worldsafelocsdbc(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.WorldDatabase);
            worldsafelocsdbc worldsafelocs = new worldsafelocsdbc();
            worldsafelocs.LoadDB(connection);
            worldsafelocs.SaveDBC(DB2DBC.OutPath + "\\WorldSafeLocs.dbc"); }
        
        // iwpu
        public static void achievement_category(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            achievement_categorydbc achievement_category = new achievement_categorydbc();
            achievement_category.LoadDB(connection);
            achievement_category.SaveDBC(DB2DBC.OutPath + "\\Achievement_Category.dbc"); }

        public static void animationdata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            animationdatadbc animationdata = new animationdatadbc();
            animationdata.LoadDB(connection);
            animationdata.SaveDBC(DB2DBC.OutPath + "\\AnimationData.dbc"); }

        public static void attackanimkits(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            attackanimkitsdbc attackanimkits = new attackanimkitsdbc();
            attackanimkits.LoadDB(connection);
            attackanimkits.SaveDBC(DB2DBC.OutPath + "\\AttackAnimKits.dbc"); }

        public static void attackanimtypes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            attackanimtypesdbc attackanimtypes = new attackanimtypesdbc();
            attackanimtypes.LoadDB(connection);
            attackanimtypes.SaveDBC(DB2DBC.OutPath + "\\AttackAnimTypes.dbc"); }

        public static void camerashakes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            camerashakesdbc camerashakes = new camerashakesdbc();
            camerashakes.LoadDB(connection);
            camerashakes.SaveDBC(DB2DBC.OutPath + "\\CameraShakes.dbc"); }

        public static void cfg_categories(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            cfg_categoriesdbc cfg_categories = new cfg_categoriesdbc();
            cfg_categories.LoadDB(connection);
            cfg_categories.SaveDBC(DB2DBC.OutPath + "\\Cfg_Categories.dbc"); }

        public static void cfg_configs(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            cfg_configsdbc cfg_configs = new cfg_configsdbc();
            cfg_configs.LoadDB(connection);
            cfg_configs.SaveDBC(DB2DBC.OutPath + "\\Cfg_Configs.dbc"); }

        public static void characterfacialhairstyles(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            characterfacialhairstylesdbc characterfacialhairstyles = new characterfacialhairstylesdbc();
            characterfacialhairstyles.LoadDB(connection);
            characterfacialhairstyles.SaveDBC(DB2DBC.OutPath + "\\CharacterFacialHairStyles.dbc"); }

        public static void charbaseinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            charbaseinfodbc charbaseinfo = new charbaseinfodbc();
            charbaseinfo.LoadDB(connection);
            charbaseinfo.SaveDBC(DB2DBC.OutPath + "\\CharBaseInfo.dbc"); }

        public static void charhairgeosets(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            charhairgeosetsdbc charhairgeosets = new charhairgeosetsdbc();
            charhairgeosets.LoadDB(connection);
            charhairgeosets.SaveDBC(DB2DBC.OutPath + "\\CharHairGeosets.dbc"); }

        public static void charhairtextures(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            charhairtexturesdbc charhairtextures = new charhairtexturesdbc();
            charhairtextures.LoadDB(connection);
            charhairtextures.SaveDBC(DB2DBC.OutPath + "\\CharHairTextures.dbc"); }

        public static void chatprofanity(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            chatprofanitydbc chatprofanity = new chatprofanitydbc();
            chatprofanity.LoadDB(connection);
            chatprofanity.SaveDBC(DB2DBC.OutPath + "\\ChatProfanity.dbc"); }

        public static void cinematiccamera(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            cinematiccameradbc cinematiccamera = new cinematiccameradbc();
            cinematiccamera.LoadDB(connection);
            cinematiccamera.SaveDBC(DB2DBC.OutPath + "\\CinematicCamera.dbc"); }

        public static void creaturemovementinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            creaturemovementinfodbc creaturemovementinfo = new creaturemovementinfodbc();
            creaturemovementinfo.LoadDB(connection);
            creaturemovementinfo.SaveDBC(DB2DBC.OutPath + "\\CreatureMovementInfo.dbc"); }

        public static void creaturesounddata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            creaturesounddatadbc creaturesounddata = new creaturesounddatadbc();
            creaturesounddata.LoadDB(connection);
            creaturesounddata.SaveDBC(DB2DBC.OutPath + "\\CreatureSoundData.dbc"); }

        public static void currencycategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            currencycategorydbc currencycategory = new currencycategorydbc();
            currencycategory.LoadDB(connection);
            currencycategory.SaveDBC(DB2DBC.OutPath + "\\CurrencyCategory.dbc"); }

        public static void dancemoves(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            dancemovesdbc dancemoves = new dancemovesdbc();
            dancemoves.LoadDB(connection);
            dancemoves.SaveDBC(DB2DBC.OutPath + "\\DanceMoves.dbc"); }

        public static void deaththudlookups(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            deaththudlookupsdbc deaththudlookups = new deaththudlookupsdbc();
            deaththudlookups.LoadDB(connection);
            deaththudlookups.SaveDBC(DB2DBC.OutPath + "\\DeathThudLookups.dbc"); }

        public static void declinedword(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            declinedworddbc declinedword = new declinedworddbc();
            declinedword.LoadDB(connection);
            declinedword.SaveDBC(DB2DBC.OutPath + "\\DeclinedWord.dbc"); }

        public static void declinedwordcases(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            declinedwordcasesdbc declinedwordcases = new declinedwordcasesdbc();
            declinedwordcases.LoadDB(connection);
            declinedwordcases.SaveDBC(DB2DBC.OutPath + "\\DeclinedWordCases.dbc"); }

        public static void dungeonmap(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            dungeonmapdbc dungeonmap = new dungeonmapdbc();
            dungeonmap.LoadDB(connection);
            dungeonmap.SaveDBC(DB2DBC.OutPath + "\\DungeonMap.dbc"); }

        public static void dungeonmapchunk(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            dungeonmapchunkdbc dungeonmapchunk = new dungeonmapchunkdbc();
            dungeonmapchunk.LoadDB(connection);
            dungeonmapchunk.SaveDBC(DB2DBC.OutPath + "\\DungeonMapChunk.dbc"); }

        public static void emotestextdata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            emotestextdatadbc emotestextdata = new emotestextdatadbc();
            emotestextdata.LoadDB(connection);
            emotestextdata.SaveDBC(DB2DBC.OutPath + "\\EmotesTextData.dbc"); }

        public static void emotestextsound(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            emotestextsounddbc emotestextsound = new emotestextsounddbc();
            emotestextsound.LoadDB(connection);
            emotestextsound.SaveDBC(DB2DBC.OutPath + "\\EmotesTextSound.dbc"); }

        public static void environmentaldamage(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            environmentaldamagedbc environmentaldamage = new environmentaldamagedbc();
            environmentaldamage.LoadDB(connection);
            environmentaldamage.SaveDBC(DB2DBC.OutPath + "\\EnvironmentalDamage.dbc"); }

        public static void exhaustion(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            exhaustiondbc exhaustion = new exhaustiondbc();
            exhaustion.LoadDB(connection);
            exhaustion.SaveDBC(DB2DBC.OutPath + "\\Exhaustion.dbc"); }

        public static void factiongroup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            factiongroupdbc factiongroup = new factiongroupdbc();
            factiongroup.LoadDB(connection);
            factiongroup.SaveDBC(DB2DBC.OutPath + "\\FactionGroup.dbc"); }

        public static void filedata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            filedatadbc filedata = new filedatadbc();
            filedata.LoadDB(connection);
            filedata.SaveDBC(DB2DBC.OutPath + "\\FileData.dbc"); }

        public static void footprinttextures(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            footprinttexturesdbc footprinttextures = new footprinttexturesdbc();
            footprinttextures.LoadDB(connection);
            footprinttextures.SaveDBC(DB2DBC.OutPath + "\\FootprintTextures.dbc"); }

        public static void footstepterrainlookup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            footstepterrainlookupdbc footstepterrainlookup = new footstepterrainlookupdbc();
            footstepterrainlookup.LoadDB(connection);
            footstepterrainlookup.SaveDBC(DB2DBC.OutPath + "\\FootstepTerrainLookup.dbc"); }

        public static void gameobjectartkit(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gameobjectartkitdbc gameobjectartkit = new gameobjectartkitdbc();
            gameobjectartkit.LoadDB(connection);
            gameobjectartkit.SaveDBC(DB2DBC.OutPath + "\\GameObjectArtKit.dbc"); }

        public static void gametables(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gametablesdbc gametables = new gametablesdbc();
            gametables.LoadDB(connection);
            gametables.SaveDBC(DB2DBC.OutPath + "\\GameTables.dbc"); }

        public static void gametips(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gametipsdbc gametips = new gametipsdbc();
            gametips.LoadDB(connection);
            gametips.SaveDBC(DB2DBC.OutPath + "\\GameTips.dbc"); }

        public static void gmsurveyanswers(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gmsurveyanswersdbc gmsurveyanswers = new gmsurveyanswersdbc();
            gmsurveyanswers.LoadDB(connection);
            gmsurveyanswers.SaveDBC(DB2DBC.OutPath + "\\GMSurveyAnswers.dbc"); }

        public static void gmsurveycurrentsurvey(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gmsurveycurrentsurveydbc gmsurveycurrentsurvey = new gmsurveycurrentsurveydbc();
            gmsurveycurrentsurvey.LoadDB(connection);
            gmsurveycurrentsurvey.SaveDBC(DB2DBC.OutPath + "\\GMSurveyCurrentSurvey.dbc"); }

        public static void gmsurveyquestions(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gmsurveyquestionsdbc gmsurveyquestions = new gmsurveyquestionsdbc();
            gmsurveyquestions.LoadDB(connection);
            gmsurveyquestions.SaveDBC(DB2DBC.OutPath + "\\GMSurveyQuestions.dbc"); }

        public static void gmsurveysurveys(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gmsurveysurveysdbc gmsurveysurveys = new gmsurveysurveysdbc();
            gmsurveysurveys.LoadDB(connection);
            gmsurveysurveys.SaveDBC(DB2DBC.OutPath + "\\GMSurveySurveys.dbc"); }

        public static void gmticketcategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gmticketcategorydbc gmticketcategory = new gmticketcategorydbc();
            gmticketcategory.LoadDB(connection);
            gmticketcategory.SaveDBC(DB2DBC.OutPath + "\\GMTicketCategory.dbc"); }

        public static void groundeffectdoodad(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            groundeffectdoodaddbc groundeffectdoodad = new groundeffectdoodaddbc();
            groundeffectdoodad.LoadDB(connection);
            groundeffectdoodad.SaveDBC(DB2DBC.OutPath + "\\GroundEffectDoodad.dbc"); }

        public static void groundeffecttexture(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            groundeffecttexturedbc groundeffecttexture = new groundeffecttexturedbc();
            groundeffecttexture.LoadDB(connection);
            groundeffecttexture.SaveDBC(DB2DBC.OutPath + "\\GroundEffectTexture.dbc"); }

        public static void gtbarbershopcostbase(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtbarbershopcostbasedbc gtbarbershopcostbase = new gtbarbershopcostbasedbc();
            gtbarbershopcostbase.LoadDB(connection);
            gtbarbershopcostbase.SaveDBC(DB2DBC.OutPath + "\\gtBarberShopCostBase.dbc"); }

        public static void gtchancetomeleecrit(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtchancetomeleecritdbc gtchancetomeleecrit = new gtchancetomeleecritdbc();
            gtchancetomeleecrit.LoadDB(connection);
            gtchancetomeleecrit.SaveDBC(DB2DBC.OutPath + "\\gtChanceToMeleeCrit.dbc"); }

        public static void gtchancetomeleecritbase(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtchancetomeleecritbasedbc gtchancetomeleecritbase = new gtchancetomeleecritbasedbc();
            gtchancetomeleecritbase.LoadDB(connection);
            gtchancetomeleecritbase.SaveDBC(DB2DBC.OutPath + "\\gtChanceToMeleeCritBase.dbc"); }

        public static void gtchancetospellcrit(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtchancetospellcritdbc gtchancetospellcrit = new gtchancetospellcritdbc();
            gtchancetospellcrit.LoadDB(connection);
            gtchancetospellcrit.SaveDBC(DB2DBC.OutPath + "\\gtChanceToSpellCrit.dbc"); }

        public static void gtchancetospellcritbase(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtchancetospellcritbasedbc gtchancetospellcritbase = new gtchancetospellcritbasedbc();
            gtchancetospellcritbase.LoadDB(connection);
            gtchancetospellcritbase.SaveDBC(DB2DBC.OutPath + "\\gtChanceToSpellCritBase.dbc"); }

        public static void gtcombatratings(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtcombatratingsdbc gtcombatratings = new gtcombatratingsdbc();
            gtcombatratings.LoadDB(connection);
            gtcombatratings.SaveDBC(DB2DBC.OutPath + "\\gtCombatRatings.dbc"); }

        public static void gtnpcmanacostscaler(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtnpcmanacostscalerdbc gtnpcmanacostscaler = new gtnpcmanacostscalerdbc();
            gtnpcmanacostscaler.LoadDB(connection);
            gtnpcmanacostscaler.SaveDBC(DB2DBC.OutPath + "\\gtNPCManaCostScaler.dbc"); }

        public static void gtoctclasscombatratingscalar(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtoctclasscombatratingscalardbc gtoctclasscombatratingscalar = new gtoctclasscombatratingscalardbc();
            gtoctclasscombatratingscalar.LoadDB(connection);
            gtoctclasscombatratingscalar.SaveDBC(DB2DBC.OutPath + "\\gtOCTClassCombatRatingScalar.dbc"); }

        public static void gtoctregenhp(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtoctregenhpdbc gtoctregenhp = new gtoctregenhpdbc();
            gtoctregenhp.LoadDB(connection);
            gtoctregenhp.SaveDBC(DB2DBC.OutPath + "\\gtOCTRegenHP.dbc"); }

        public static void gtoctregenmp(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtoctregenmpdbc gtoctregenmp = new gtoctregenmpdbc();
            gtoctregenmp.LoadDB(connection);
            gtoctregenmp.SaveDBC(DB2DBC.OutPath + "\\gtOCTRegenMP.dbc"); }

        public static void gtregenhpperspt(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtregenhppersptdbc gtregenhpperspt = new gtregenhppersptdbc();
            gtregenhpperspt.LoadDB(connection);
            gtregenhpperspt.SaveDBC(DB2DBC.OutPath + "\\gtRegenHPPerSpt.dbc"); }

        public static void gtregenmpperspt(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            gtregenmppersptdbc gtregenmpperspt = new gtregenmppersptdbc();
            gtregenmpperspt.LoadDB(connection);
            gtregenmpperspt.SaveDBC(DB2DBC.OutPath + "\\gtRegenMPPerSpt.dbc"); }

        public static void helmetgeosetvisdata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            helmetgeosetvisdatadbc helmetgeosetvisdata = new helmetgeosetvisdatadbc();
            helmetgeosetvisdata.LoadDB(connection);
            helmetgeosetvisdata.SaveDBC(DB2DBC.OutPath + "\\HelmetGeosetVisData.dbc"); }

        public static void holidaydescriptions(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            holidaydescriptionsdbc holidaydescriptions = new holidaydescriptionsdbc();
            holidaydescriptions.LoadDB(connection);
            holidaydescriptions.SaveDBC(DB2DBC.OutPath + "\\HolidayDescriptions.dbc"); }

        public static void holidaynames(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            holidaynamesdbc holidaynames = new holidaynamesdbc();
            holidaynames.LoadDB(connection);
            holidaynames.SaveDBC(DB2DBC.OutPath + "\\HolidayNames.dbc"); }

        public static void itemclass(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemclassdbc itemclass = new itemclassdbc();
            itemclass.LoadDB(connection);
            itemclass.SaveDBC(DB2DBC.OutPath + "\\ItemClass.dbc"); }

        public static void itemcondextcosts(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemcondextcostsdbc itemcondextcosts = new itemcondextcostsdbc();
            itemcondextcosts.LoadDB(connection);
            itemcondextcosts.SaveDBC(DB2DBC.OutPath + "\\ItemCondExtCosts.dbc"); }

        public static void itemdisplayinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemdisplayinfodbc itemdisplayinfo = new itemdisplayinfodbc();
            itemdisplayinfo.LoadDB(connection);
            itemdisplayinfo.SaveDBC(DB2DBC.OutPath + "\\ItemDisplayInfo.dbc"); }

        public static void itemgroupsounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemgroupsoundsdbc itemgroupsounds = new itemgroupsoundsdbc();
            itemgroupsounds.LoadDB(connection);
            itemgroupsounds.SaveDBC(DB2DBC.OutPath + "\\ItemGroupSounds.dbc"); }

        public static void itempetfood(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itempetfooddbc itempetfood = new itempetfooddbc();
            itempetfood.LoadDB(connection);
            itempetfood.SaveDBC(DB2DBC.OutPath + "\\ItemPetFood.dbc"); }

        public static void itempurchasegroup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itempurchasegroupdbc itempurchasegroup = new itempurchasegroupdbc();
            itempurchasegroup.LoadDB(connection);
            itempurchasegroup.SaveDBC(DB2DBC.OutPath + "\\ItemPurchaseGroup.dbc"); }

        public static void itemsubclass(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemsubclassdbc itemsubclass = new itemsubclassdbc();
            itemsubclass.LoadDB(connection);
            itemsubclass.SaveDBC(DB2DBC.OutPath + "\\ItemSubClass.dbc"); }

        public static void itemsubclassmask(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemsubclassmaskdbc itemsubclassmask = new itemsubclassmaskdbc();
            itemsubclassmask.LoadDB(connection);
            itemsubclassmask.SaveDBC(DB2DBC.OutPath + "\\ItemSubClassMask.dbc"); }

        public static void itemvisualeffects(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemvisualeffectsdbc itemvisualeffects = new itemvisualeffectsdbc();
            itemvisualeffects.LoadDB(connection);
            itemvisualeffects.SaveDBC(DB2DBC.OutPath + "\\ItemVisualEffects.dbc"); }

        public static void itemvisuals(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            itemvisualsdbc itemvisuals = new itemvisualsdbc();
            itemvisuals.LoadDB(connection);
            itemvisuals.SaveDBC(DB2DBC.OutPath + "\\ItemVisuals.dbc"); }

        public static void languages(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            languagesdbc languages = new languagesdbc();
            languages.LoadDB(connection);
            languages.SaveDBC(DB2DBC.OutPath + "\\Languages.dbc"); }

        public static void languagewords(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            languagewordsdbc languagewords = new languagewordsdbc();
            languagewords.LoadDB(connection);
            languagewords.SaveDBC(DB2DBC.OutPath + "\\LanguageWords.dbc"); }

        public static void lfgdungeonexpansion(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lfgdungeonexpansiondbc lfgdungeonexpansion = new lfgdungeonexpansiondbc();
            lfgdungeonexpansion.LoadDB(connection);
            lfgdungeonexpansion.SaveDBC(DB2DBC.OutPath + "\\LFGDungeonExpansion.dbc"); }

        public static void lfgdungeongroup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lfgdungeongroupdbc lfgdungeongroup = new lfgdungeongroupdbc();
            lfgdungeongroup.LoadDB(connection);
            lfgdungeongroup.SaveDBC(DB2DBC.OutPath + "\\LFGDungeonGroup.dbc"); }

        public static void lightfloatband(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lightfloatbanddbc lightfloatband = new lightfloatbanddbc();
            lightfloatband.LoadDB(connection);
            lightfloatband.SaveDBC(DB2DBC.OutPath + "\\LightFloatBand.dbc"); }

        public static void lightintband(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lightintbanddbc lightintband = new lightintbanddbc();
            lightintband.LoadDB(connection);
            lightintband.SaveDBC(DB2DBC.OutPath + "\\LightIntBand.dbc"); }

        public static void lightparams(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lightparamsdbc lightparams = new lightparamsdbc();
            lightparams.LoadDB(connection);
            lightparams.SaveDBC(DB2DBC.OutPath + "\\LightParams.dbc"); }

        public static void lightskybox(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            lightskyboxdbc lightskybox = new lightskyboxdbc();
            lightskybox.LoadDB(connection);
            lightskybox.SaveDBC(DB2DBC.OutPath + "\\LightSkybox.dbc"); }

        public static void liquidmaterial(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            liquidmaterialdbc liquidmaterial = new liquidmaterialdbc();
            liquidmaterial.LoadDB(connection);
            liquidmaterial.SaveDBC(DB2DBC.OutPath + "\\LiquidMaterial.dbc"); }

        public static void loadingscreens(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            loadingscreensdbc loadingscreens = new loadingscreensdbc();
            loadingscreens.LoadDB(connection);
            loadingscreens.SaveDBC(DB2DBC.OutPath + "\\LoadingScreens.dbc"); }

        public static void loadingscreentaxisplines(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            loadingscreentaxisplinesdbc loadingscreentaxisplines = new loadingscreentaxisplinesdbc();
            loadingscreentaxisplines.LoadDB(connection);
            loadingscreentaxisplines.SaveDBC(DB2DBC.OutPath + "\\LoadingScreenTaxiSplines.dbc"); }

        public static void locktype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            locktypedbc locktype = new locktypedbc();
            locktype.LoadDB(connection);
            locktype.SaveDBC(DB2DBC.OutPath + "\\LockType.dbc"); }

        public static void material(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            materialdbc material = new materialdbc();
            material.LoadDB(connection);
            material.SaveDBC(DB2DBC.OutPath + "\\Material.dbc"); }

        public static void moviefiledata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            moviefiledatadbc moviefiledata = new moviefiledatadbc();
            moviefiledata.LoadDB(connection);
            moviefiledata.SaveDBC(DB2DBC.OutPath + "\\MovieFileData.dbc"); }

        public static void movievariation(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            movievariationdbc movievariation = new movievariationdbc();
            movievariation.LoadDB(connection);
            movievariation.SaveDBC(DB2DBC.OutPath + "\\MovieVariation.dbc"); }

        public static void namegen(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            namegendbc namegen = new namegendbc();
            namegen.LoadDB(connection);
            namegen.SaveDBC(DB2DBC.OutPath + "\\NameGen.dbc"); }

        public static void namesprofanity(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            namesprofanitydbc namesprofanity = new namesprofanitydbc();
            namesprofanity.LoadDB(connection);
            namesprofanity.SaveDBC(DB2DBC.OutPath + "\\NamesProfanity.dbc"); }

        public static void namesreserved(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            namesreserveddbc namesreserved = new namesreserveddbc();
            namesreserved.LoadDB(connection);
            namesreserved.SaveDBC(DB2DBC.OutPath + "\\NamesReserved.dbc"); }

        public static void npcsounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            npcsoundsdbc npcsounds = new npcsoundsdbc();
            npcsounds.LoadDB(connection);
            npcsounds.SaveDBC(DB2DBC.OutPath + "\\NPCSounds.dbc"); }

        public static void objecteffect(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            objecteffectdbc objecteffect = new objecteffectdbc();
            objecteffect.LoadDB(connection);
            objecteffect.SaveDBC(DB2DBC.OutPath + "\\ObjectEffect.dbc"); }

        public static void objecteffectgroup(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            objecteffectgroupdbc objecteffectgroup = new objecteffectgroupdbc();
            objecteffectgroup.LoadDB(connection);
            objecteffectgroup.SaveDBC(DB2DBC.OutPath + "\\ObjectEffectGroup.dbc"); }

        public static void objecteffectmodifier(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            objecteffectmodifierdbc objecteffectmodifier = new objecteffectmodifierdbc();
            objecteffectmodifier.LoadDB(connection);
            objecteffectmodifier.SaveDBC(DB2DBC.OutPath + "\\ObjectEffectModifier.dbc"); }

        public static void objecteffectpackage(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            objecteffectpackagedbc objecteffectpackage = new objecteffectpackagedbc();
            objecteffectpackage.LoadDB(connection);
            objecteffectpackage.SaveDBC(DB2DBC.OutPath + "\\ObjectEffectPackage.dbc"); }

        public static void objecteffectpackageelem(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            objecteffectpackageelemdbc objecteffectpackageelem = new objecteffectpackageelemdbc();
            objecteffectpackageelem.LoadDB(connection);
            objecteffectpackageelem.SaveDBC(DB2DBC.OutPath + "\\ObjectEffectPackageElem.dbc"); }

        public static void package(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            packagedbc package = new packagedbc();
            package.LoadDB(connection);
            package.SaveDBC(DB2DBC.OutPath + "\\Package.dbc"); }

        public static void pagetextmaterial(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            pagetextmaterialdbc pagetextmaterial = new pagetextmaterialdbc();
            pagetextmaterial.LoadDB(connection);
            pagetextmaterial.SaveDBC(DB2DBC.OutPath + "\\PageTextMaterial.dbc"); }

        public static void paperdollitemframe(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            paperdollitemframedbc paperdollitemframe = new paperdollitemframedbc();
            paperdollitemframe.LoadDB(connection);
            paperdollitemframe.SaveDBC(DB2DBC.OutPath + "\\PaperDollItemFrame.dbc"); }

        public static void particlecolor(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            particlecolordbc particlecolor = new particlecolordbc();
            particlecolor.LoadDB(connection);
            particlecolor.SaveDBC(DB2DBC.OutPath + "\\ParticleColor.dbc"); }

        public static void petitiontype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            petitiontypedbc petitiontype = new petitiontypedbc();
            petitiontype.LoadDB(connection);
            petitiontype.SaveDBC(DB2DBC.OutPath + "\\PetitionType.dbc"); }

        public static void petpersonality(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            petpersonalitydbc petpersonality = new petpersonalitydbc();
            petpersonality.LoadDB(connection);
            petpersonality.SaveDBC(DB2DBC.OutPath + "\\PetPersonality.dbc"); }

        public static void questinfo(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            questinfodbc questinfo = new questinfodbc();
            questinfo.LoadDB(connection);
            questinfo.SaveDBC(DB2DBC.OutPath + "\\QuestInfo.dbc"); }

        public static void resistances(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            resistancesdbc resistances = new resistancesdbc();
            resistances.LoadDB(connection);
            resistances.SaveDBC(DB2DBC.OutPath + "\\Resistances.dbc"); }

        public static void screeneffect(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            screeneffectdbc screeneffect = new screeneffectdbc();
            screeneffect.LoadDB(connection);
            screeneffect.SaveDBC(DB2DBC.OutPath + "\\ScreenEffect.dbc"); }

        public static void servermessages(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            servermessagesdbc servermessages = new servermessagesdbc();
            servermessages.LoadDB(connection);
            servermessages.SaveDBC(DB2DBC.OutPath + "\\ServerMessages.dbc"); }

        public static void sheathesoundlookups(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            sheathesoundlookupsdbc sheathesoundlookups = new sheathesoundlookupsdbc();
            sheathesoundlookups.LoadDB(connection);
            sheathesoundlookups.SaveDBC(DB2DBC.OutPath + "\\SheatheSoundLookups.dbc"); }

        public static void skillcostsdata(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            skillcostsdatadbc skillcostsdata = new skillcostsdatadbc();
            skillcostsdata.LoadDB(connection);
            skillcostsdata.SaveDBC(DB2DBC.OutPath + "\\SkillCostsData.dbc"); }

        public static void skilllinecategory(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            skilllinecategorydbc skilllinecategory = new skilllinecategorydbc();
            skilllinecategory.LoadDB(connection);
            skilllinecategory.SaveDBC(DB2DBC.OutPath + "\\SkillLineCategory.dbc"); }

        public static void soundambience(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundambiencedbc soundambience = new soundambiencedbc();
            soundambience.LoadDB(connection);
            soundambience.SaveDBC(DB2DBC.OutPath + "\\SoundAmbience.dbc"); }

        public static void soundemitters(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundemittersdbc soundemitters = new soundemittersdbc();
            soundemitters.LoadDB(connection);
            soundemitters.SaveDBC(DB2DBC.OutPath + "\\SoundEmitters.dbc"); }

        public static void soundentriesadvanced(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundentriesadvanceddbc soundentriesadvanced = new soundentriesadvanceddbc();
            soundentriesadvanced.LoadDB(connection);
            soundentriesadvanced.SaveDBC(DB2DBC.OutPath + "\\SoundEntriesAdvanced.dbc"); }

        public static void soundfilter(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundfilterdbc soundfilter = new soundfilterdbc();
            soundfilter.LoadDB(connection);
            soundfilter.SaveDBC(DB2DBC.OutPath + "\\SoundFilter.dbc"); }

        public static void soundfilterelem(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundfilterelemdbc soundfilterelem = new soundfilterelemdbc();
            soundfilterelem.LoadDB(connection);
            soundfilterelem.SaveDBC(DB2DBC.OutPath + "\\SoundFilterElem.dbc"); }

        public static void soundproviderpreferences(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundproviderpreferencesdbc soundproviderpreferences = new soundproviderpreferencesdbc();
            soundproviderpreferences.LoadDB(connection);
            soundproviderpreferences.SaveDBC(DB2DBC.OutPath + "\\SoundProviderPreferences.dbc"); }

        public static void soundsamplepreferences(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundsamplepreferencesdbc soundsamplepreferences = new soundsamplepreferencesdbc();
            soundsamplepreferences.LoadDB(connection);
            soundsamplepreferences.SaveDBC(DB2DBC.OutPath + "\\SoundSamplePreferences.dbc"); }

        public static void soundwatertype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            soundwatertypedbc soundwatertype = new soundwatertypedbc();
            soundwatertype.LoadDB(connection);
            soundwatertype.SaveDBC(DB2DBC.OutPath + "\\SoundWaterType.dbc"); }

        public static void spammessages(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spammessagesdbc spammessages = new spammessagesdbc();
            spammessages.LoadDB(connection);
            spammessages.SaveDBC(DB2DBC.OutPath + "\\SpamMessages.dbc"); }
        
        public static void spellchaineffects(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellchaineffectsdbc spellchaineffects = new spellchaineffectsdbc();
            spellchaineffects.LoadDB(connection);
            spellchaineffects.SaveDBC(DB2DBC.OutPath + "\\SpellChainEffects.dbc"); }

        public static void spelldescriptionvariables(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spelldescriptionvariablesdbc spelldescriptionvariables = new spelldescriptionvariablesdbc();
            spelldescriptionvariables.LoadDB(connection);
            spelldescriptionvariables.SaveDBC(DB2DBC.OutPath + "\\SpellDescriptionVariables.dbc"); }

        public static void spelldispeltype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spelldispeltypedbc spelldispeltype = new spelldispeltypedbc();
            spelldispeltype.LoadDB(connection);
            spelldispeltype.SaveDBC(DB2DBC.OutPath + "\\SpellDispelType.dbc"); }

        public static void spelleffectcamerashakes(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spelleffectcamerashakesdbc spelleffectcamerashakes = new spelleffectcamerashakesdbc();
            spelleffectcamerashakes.LoadDB(connection);
            spelleffectcamerashakes.SaveDBC(DB2DBC.OutPath + "\\SpellEffectCameraShakes.dbc"); }

        public static void spellicon(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellicondbc spellicon = new spellicondbc();
            spellicon.LoadDB(connection);
            spellicon.SaveDBC(DB2DBC.OutPath + "\\SpellIcon.dbc"); }

        public static void spellmechanic(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellmechanicdbc spellmechanic = new spellmechanicdbc();
            spellmechanic.LoadDB(connection);
            spellmechanic.SaveDBC(DB2DBC.OutPath + "\\SpellMechanic.dbc"); }

        public static void spellmissile(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellmissiledbc spellmissile = new spellmissiledbc();
            spellmissile.LoadDB(connection);
            spellmissile.SaveDBC(DB2DBC.OutPath + "\\SpellMissile.dbc"); }

        public static void spellmissilemotion(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellmissilemotiondbc spellmissilemotion = new spellmissilemotiondbc();
            spellmissilemotion.LoadDB(connection);
            spellmissilemotion.SaveDBC(DB2DBC.OutPath + "\\SpellMissileMotion.dbc"); }

        public static void spellvisual(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualdbc spellvisual = new spellvisualdbc();
            spellvisual.LoadDB(connection);
            spellvisual.SaveDBC(DB2DBC.OutPath + "\\SpellVisual.dbc"); }

        public static void spellvisualeffectname(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualeffectnamedbc spellvisualeffectname = new spellvisualeffectnamedbc();
            spellvisualeffectname.LoadDB(connection);
            spellvisualeffectname.SaveDBC(DB2DBC.OutPath + "\\SpellVisualEffectName.dbc"); }

        public static void spellvisualkit(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualkitdbc spellvisualkit = new spellvisualkitdbc();
            spellvisualkit.LoadDB(connection);
            spellvisualkit.SaveDBC(DB2DBC.OutPath + "\\SpellVisualKit.dbc"); }

        public static void spellvisualkitareamodel(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualkitareamodeldbc spellvisualkitareamodel = new spellvisualkitareamodeldbc();
            spellvisualkitareamodel.LoadDB(connection);
            spellvisualkitareamodel.SaveDBC(DB2DBC.OutPath + "\\SpellVisualKitAreaModel.dbc"); }

        public static void spellvisualkitmodelattach(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualkitmodelattachdbc spellvisualkitmodelattach = new spellvisualkitmodelattachdbc();
            spellvisualkitmodelattach.LoadDB(connection);
            spellvisualkitmodelattach.SaveDBC(DB2DBC.OutPath + "\\SpellVisualKitModelAttach.dbc"); }

        public static void spellvisualprecasttransitions(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            spellvisualprecasttransitionsdbc spellvisualprecasttransitions = new spellvisualprecasttransitionsdbc();
            spellvisualprecasttransitions.LoadDB(connection);
            spellvisualprecasttransitions.SaveDBC(DB2DBC.OutPath + "\\SpellVisualPrecastTransitions.dbc"); }

        public static void startup_strings(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            startup_stringsdbc startup_strings = new startup_stringsdbc();
            startup_strings.LoadDB(connection);
            startup_strings.SaveDBC(DB2DBC.OutPath + "\\Startup_Strings.dbc"); }

        public static void stationery(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            stationerydbc stationery = new stationerydbc();
            stationery.LoadDB(connection);
            stationery.SaveDBC(DB2DBC.OutPath + "\\Stationery.dbc"); }

        public static void stringlookups(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            stringlookupsdbc stringlookups = new stringlookupsdbc();
            stringlookups.LoadDB(connection);
            stringlookups.SaveDBC(DB2DBC.OutPath + "\\StringLookups.dbc"); }

        public static void terraintype(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            terraintypedbc terraintype = new terraintypedbc();
            terraintype.LoadDB(connection);
            terraintype.SaveDBC(DB2DBC.OutPath + "\\TerrainType.dbc"); }
        
        public static void terraintypesounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            terraintypesoundsdbc terraintypesounds = new terraintypesoundsdbc();
            terraintypesounds.LoadDB(connection);
            terraintypesounds.SaveDBC(DB2DBC.OutPath + "\\TerrainTypeSounds.dbc"); }

        public static void transportphysics(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            transportphysicsdbc transportphysics = new transportphysicsdbc();
            transportphysics.LoadDB(connection);
            transportphysics.SaveDBC(DB2DBC.OutPath + "\\TransportPhysics.dbc"); }

        public static void uisoundlookups(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            uisoundlookupsdbc uisoundlookups = new uisoundlookupsdbc();
            uisoundlookups.LoadDB(connection);
            uisoundlookups.SaveDBC(DB2DBC.OutPath + "\\UISoundLookups.dbc"); }

        public static void unitblood(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            unitblooddbc unitblood = new unitblooddbc();
            unitblood.LoadDB(connection);
            unitblood.SaveDBC(DB2DBC.OutPath + "\\UnitBlood.dbc"); }

        public static void unitbloodlevels(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            unitbloodlevelsdbc unitbloodlevels = new unitbloodlevelsdbc();
            unitbloodlevels.LoadDB(connection);
            unitbloodlevels.SaveDBC(DB2DBC.OutPath + "\\UnitBloodLevels.dbc"); }

        public static void vehicleuiindicator(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            vehicleuiindicatordbc vehicleuiindicator = new vehicleuiindicatordbc();
            vehicleuiindicator.LoadDB(connection);
            vehicleuiindicator.SaveDBC(DB2DBC.OutPath + "\\VehicleUIIndicator.dbc"); }

        public static void vehicleuiindseat(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            vehicleuiindseatdbc vehicleuiindseat = new vehicleuiindseatdbc();
            vehicleuiindseat.LoadDB(connection);
            vehicleuiindseat.SaveDBC(DB2DBC.OutPath + "\\VehicleUIIndSeat.dbc"); }

        public static void videohardware(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            videohardwaredbc videohardware = new videohardwaredbc();
            videohardware.LoadDB(connection);
            videohardware.SaveDBC(DB2DBC.OutPath + "\\VideoHardWare.dbc"); }

        public static void vocaluisounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            vocaluisoundsdbc vocaluisounds = new vocaluisoundsdbc();
            vocaluisounds.LoadDB(connection);
            vocaluisounds.SaveDBC(DB2DBC.OutPath + "\\VocalUISounds.dbc"); }

        public static void weaponimpactsounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            weaponimpactsoundsdbc weaponimpactsounds = new weaponimpactsoundsdbc();
            weaponimpactsounds.LoadDB(connection);
            weaponimpactsounds.SaveDBC(DB2DBC.OutPath + "\\WeaponImpactSounds.dbc"); }

        public static void weaponswingsounds2(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            weaponswingsounds2dbc weaponswingsounds2 = new weaponswingsounds2dbc();
            weaponswingsounds2.LoadDB(connection);
            weaponswingsounds2.SaveDBC(DB2DBC.OutPath + "\\WeaponSwingSounds2.dbc"); }

        public static void weather(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            weatherdbc weather = new weatherdbc();
            weather.LoadDB(connection);
            weather.SaveDBC(DB2DBC.OutPath + "\\Weather.dbc"); }

        public static void worldchunksounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            worldchunksoundsdbc worldchunksounds = new worldchunksoundsdbc();
            worldchunksounds.LoadDB(connection);
            worldchunksounds.SaveDBC(DB2DBC.OutPath + "\\WorldChunkSounds.dbc"); }

        public static void worldmapcontinent(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            worldmapcontinentdbc worldmapcontinent = new worldmapcontinentdbc();
            worldmapcontinent.LoadDB(connection);
            worldmapcontinent.SaveDBC(DB2DBC.OutPath + "\\WorldMapContinent.dbc"); }

        public static void worldmaptransforms(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            worldmaptransformsdbc worldmaptransforms = new worldmaptransformsdbc();
            worldmaptransforms.LoadDB(connection);
            worldmaptransforms.SaveDBC(DB2DBC.OutPath + "\\WorldMapTransforms.dbc"); }

        public static void worldstateui(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            worldstateuidbc worldstateui = new worldstateuidbc();
            worldstateui.LoadDB(connection);
            worldstateui.SaveDBC(DB2DBC.OutPath + "\\WorldStateUI.dbc"); }

        public static void worldstatezonesounds(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            worldstatezonesoundsdbc worldstatezonesounds = new worldstatezonesoundsdbc();
            worldstatezonesounds.LoadDB(connection);
            worldstatezonesounds.SaveDBC(DB2DBC.OutPath + "\\WorldStateZoneSounds.dbc"); }

        public static void wowerror_strings(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            wowerror_stringsdbc wowerror_strings = new wowerror_stringsdbc();
            wowerror_strings.LoadDB(connection);
            wowerror_strings.SaveDBC(DB2DBC.OutPath + "\\WowError_Strings.dbc"); }

        public static void zoneintromusictable(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            zoneintromusictabledbc zoneintromusictable = new zoneintromusictabledbc();
            zoneintromusictable.LoadDB(connection);
            zoneintromusictable.SaveDBC(DB2DBC.OutPath + "\\ZoneIntroMusicTable.dbc"); }

        public static void zonemusic(MySqlConnection connection) {
            DB2DBC.CheckDatabase(connection, DB2DBC.UnusedDatabase);
            zonemusicdbc zonemusic = new zonemusicdbc();
            zonemusic.LoadDB(connection);
            zonemusic.SaveDBC(DB2DBC.OutPath + "\\ZoneMusic.dbc"); }
    }
}

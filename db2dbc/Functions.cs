using System;
using MySql.Data.MySqlClient;

namespace DBtoDBC
{
    class DB2DBC
    {
        public static uint GlobalLocalization;

        static void Extractachievement_criteria(MySqlConnection connection)
        {
            achievement_criteriadbc achievement_criteria = new achievement_criteriadbc();
            achievement_criteria.LoadDB(connection);
            achievement_criteria.SaveDBC("dbc/achievement_criteria.dbc");
        }

        static void Extractachievement(MySqlConnection connection)
        {
            achievementdbc achievement = new achievementdbc();
            achievement.LoadDB(connection);
            achievement.SaveDBC("dbc/achievement.dbc");
        }

        static void Extractareagroup(MySqlConnection connection)
        {
            areagroupdbc areagroup = new areagroupdbc();
            areagroup.LoadDB(connection);
            areagroup.SaveDBC("dbc/areagroup.dbc");
        }

        static void Extractareapoi(MySqlConnection connection)
        {
            areapoidbc areapoi = new areapoidbc();
            areapoi.LoadDB(connection);
            areapoi.SaveDBC("dbc/areapoi.dbc");
        }

        static void Extractareatable(MySqlConnection connection)
        {
            areatabledbc areatable = new areatabledbc();
            areatable.LoadDB(connection);
            areatable.SaveDBC("dbc/areatable.dbc");
        }

        static void Extractareatrigger(MySqlConnection connection)
        {
            areatriggerdbc areatrigger = new areatriggerdbc();
            areatrigger.LoadDB(connection);
            areatrigger.SaveDBC("dbc/areatrigger.dbc");
        }

        static void Extractauctionhouse(MySqlConnection connection)
        {
            auctionhousedbc auctionhouse = new auctionhousedbc();
            auctionhouse.LoadDB(connection);
            auctionhouse.SaveDBC("dbc/auctionhouse.dbc");
        }

        static void Extractbankbagslotprices(MySqlConnection connection)
        {
            bankbagslotpricesdbc bankbagslotprices = new bankbagslotpricesdbc();
            bankbagslotprices.LoadDB(connection);
            bankbagslotprices.SaveDBC("dbc/bankbagslotprices.dbc");
        }

        static void Extractbannedaddons(MySqlConnection connection)
        {
            bannedaddonsdbc bannedaddons = new bannedaddonsdbc();
            bannedaddons.LoadDB(connection);
            bannedaddons.SaveDBC("dbc/bannedaddons.dbc");
        }

        static void Extractbarbershopstyle(MySqlConnection connection)
        {
            barbershopstyledbc barbershopstyle = new barbershopstyledbc();
            barbershopstyle.LoadDB(connection);
            barbershopstyle.SaveDBC("dbc/barbershopstyle.dbc");
        }

        static void Extractbattlemasterlist(MySqlConnection connection)
        {
            battlemasterlistdbc battlemasterlist = new battlemasterlistdbc();
            battlemasterlist.LoadDB(connection);
            battlemasterlist.SaveDBC("dbc/battlemasterlist.dbc");
        }

        static void Extractchartitles(MySqlConnection connection)
        {
            chartitlesdbc chartitles = new chartitlesdbc();
            chartitles.LoadDB(connection);
            chartitles.SaveDBC("dbc/chartitles.dbc");
        }

        static void Extractchatchannels(MySqlConnection connection)
        {
            chatchannelsdbc chatchannels = new chatchannelsdbc();
            chatchannels.LoadDB(connection);
            chatchannels.SaveDBC("dbc/chatchannels.dbc");
        }

        static void Extractchrclasses(MySqlConnection connection)
        {
            chrclassesdbc chrclasses = new chrclassesdbc();
            chrclasses.LoadDB(connection);
            chrclasses.SaveDBC("dbc/chrclasses.dbc");
        }

        static void Extractchrraces(MySqlConnection connection)
        {
            chrracesdbc chrraces = new chrracesdbc();
            chrraces.LoadDB(connection);
            chrraces.SaveDBC("dbc/chrraces.dbc");
        }

        static void Extractcinematicsequences(MySqlConnection connection)
        {
            cinematicsequencesdbc cinematicsequences = new cinematicsequencesdbc();
            cinematicsequences.LoadDB(connection);
            cinematicsequences.SaveDBC("dbc/cinematicsequences.dbc");
        }

        static void Extractcreaturedisplayinfo(MySqlConnection connection)
        {
            creaturedisplayinfodbc creaturedisplayinfo = new creaturedisplayinfodbc();
            creaturedisplayinfo.LoadDB(connection);
            creaturedisplayinfo.SaveDBC("dbc/creaturedisplayinfo.dbc");
        }

        static void Extractcreaturedisplayinfoextra(MySqlConnection connection)
        {
            creaturedisplayinfoextradbc creaturedisplayinfoextra = new creaturedisplayinfoextradbc();
            creaturedisplayinfoextra.LoadDB(connection);
            creaturedisplayinfoextra.SaveDBC("dbc/creaturedisplayinfoextra.dbc");
        }

        static void Extractcreaturefamily(MySqlConnection connection)
        {
            creaturefamilydbc creaturefamily = new creaturefamilydbc();
            creaturefamily.LoadDB(connection);
            creaturefamily.SaveDBC("dbc/creaturefamily.dbc");
        }

        static void Extractcreaturemodeldata(MySqlConnection connection)
        {
            creaturemodeldatadbc creaturemodeldata = new creaturemodeldatadbc();
            creaturemodeldata.LoadDB(connection);
            creaturemodeldata.SaveDBC("dbc/creaturemodeldata.dbc");
        }

        static void Extractcreaturespelldata(MySqlConnection connection)
        {
            creaturespelldatadbc creaturespelldata = new creaturespelldatadbc();
            creaturespelldata.LoadDB(connection);
            creaturespelldata.SaveDBC("dbc/creaturespelldata.dbc");
        }

        static void Extractcreaturetype(MySqlConnection connection)
        {
            creaturetypedbc creaturetype = new creaturetypedbc();
            creaturetype.LoadDB(connection);
            creaturetype.SaveDBC("dbc/creaturetype.dbc");
        }

        static void Extractcurrencytypes(MySqlConnection connection)
        {
            currencytypesdbc currencytypes = new currencytypesdbc();
            currencytypes.LoadDB(connection);
            currencytypes.SaveDBC("dbc/currencytypes.dbc");
        }

        static void Extractdestructiblemodeldata(MySqlConnection connection)
        {
            destructiblemodeldatadbc destructiblemodeldata = new destructiblemodeldatadbc();
            destructiblemodeldata.LoadDB(connection);
            destructiblemodeldata.SaveDBC("dbc/destructiblemodeldata.dbc");
        }

        static void Extractdungeonencounter(MySqlConnection connection)
        {
            dungeonencounterdbc dungeonencounter = new dungeonencounterdbc();
            dungeonencounter.LoadDB(connection);
            dungeonencounter.SaveDBC("dbc/dungeonencounter.dbc");
        }

        static void Extractdurabilitycosts(MySqlConnection connection)
        {
            durabilitycostsdbc durabilitycosts = new durabilitycostsdbc();
            durabilitycosts.LoadDB(connection);
            durabilitycosts.SaveDBC("dbc/durabilitycosts.dbc");
        }

        static void Extractdurabilityquality(MySqlConnection connection)
        {
            durabilityqualitydbc durabilityquality = new durabilityqualitydbc();
            durabilityquality.LoadDB(connection);
            durabilityquality.SaveDBC("dbc/durabilityquality.dbc");
        }

        static void Extractemotes(MySqlConnection connection)
        {
            emotesdbc emotes = new emotesdbc();
            emotes.LoadDB(connection);
            emotes.SaveDBC("dbc/emotes.dbc");
        }

        static void Extractemotestext(MySqlConnection connection)
        {
            emotestextdbc emotestext = new emotestextdbc();
            emotestext.LoadDB(connection);
            emotestext.SaveDBC("dbc/emotestext.dbc");
        }

        static void Extractfaction(MySqlConnection connection)
        {
            factiondbc faction = new factiondbc();
            faction.LoadDB(connection);
            faction.SaveDBC("dbc/faction.dbc");
        }

        static void Extractfactiontemplate(MySqlConnection connection)
        {
            factiontemplatedbc factiontemplate = new factiontemplatedbc();
            factiontemplate.LoadDB(connection);
            factiontemplate.SaveDBC("dbc/factiontemplate.dbc");
        }

        static void Extractgameobjectdisplayinfo(MySqlConnection connection)
        {
            gameobjectdisplayinfodbc gameobjectdisplayinfo = new gameobjectdisplayinfodbc();
            gameobjectdisplayinfo.LoadDB(connection);
            gameobjectdisplayinfo.SaveDBC("dbc/gameobjectdisplayinfo.dbc");
        }

        static void Extractgemproperties(MySqlConnection connection)
        {
            gempropertiesdbc gemproperties = new gempropertiesdbc();
            gemproperties.LoadDB(connection);
            gemproperties.SaveDBC("dbc/gemproperties.dbc");
        }

        static void Extractglyphproperties(MySqlConnection connection)
        {
            glyphpropertiesdbc glyphproperties = new glyphpropertiesdbc();
            glyphproperties.LoadDB(connection);
            glyphproperties.SaveDBC("dbc/glyphproperties.dbc");
        }

        static void Extractglyphslot(MySqlConnection connection)
        {
            glyphslotdbc glyphslot = new glyphslotdbc();
            glyphslot.LoadDB(connection);
            glyphslot.SaveDBC("dbc/glyphslot.dbc");
        }

        static void Extractholidays(MySqlConnection connection)
        {
            holidaysdbc holidays = new holidaysdbc();
            holidays.LoadDB(connection);
            holidays.SaveDBC("dbc/holidays.dbc");
        }

        static void Extractitembagfamily(MySqlConnection connection)
        {
            itembagfamilydbc itembagfamily = new itembagfamilydbc();
            itembagfamily.LoadDB(connection);
            itembagfamily.SaveDBC("dbc/itembagfamily.dbc");
        }

        static void Extractitemextendedcost(MySqlConnection connection)
        {
            itemextendedcostdbc itemextendedcost = new itemextendedcostdbc();
            itemextendedcost.LoadDB(connection);
            itemextendedcost.SaveDBC("dbc/itemextendedcost.dbc");
        }

        static void Extractitemlimitcategory(MySqlConnection connection)
        {
            itemlimitcategorydbc itemlimitcategory = new itemlimitcategorydbc();
            itemlimitcategory.LoadDB(connection);
            itemlimitcategory.SaveDBC("dbc/itemlimitcategory.dbc");
        }

        static void Extractitemrandomproperties(MySqlConnection connection)
        {
            itemrandompropertiesdbc itemrandomproperties = new itemrandompropertiesdbc();
            itemrandomproperties.LoadDB(connection);
            itemrandomproperties.SaveDBC("dbc/itemrandomproperties.dbc");
        }

        static void Extractitemrandomsuffix(MySqlConnection connection)
        {
            itemrandomsuffixdbc itemrandomsuffix = new itemrandomsuffixdbc();
            itemrandomsuffix.LoadDB(connection);
            itemrandomsuffix.SaveDBC("dbc/itemrandomsuffix.dbc");
        }

        static void Extractitemset(MySqlConnection connection)
        {
            itemsetdbc itemset = new itemsetdbc();
            itemset.LoadDB(connection);
            itemset.SaveDBC("dbc/itemset.dbc");
        }

        static void Extractlfgdungeons(MySqlConnection connection)
        {
            lfgdungeonsdbc lfgdungeons = new lfgdungeonsdbc();
            lfgdungeons.LoadDB(connection);
            lfgdungeons.SaveDBC("dbc/lfgdungeons.dbc");
        }

        static void Extractlight(MySqlConnection connection)
        {
            lightdbc light = new lightdbc();
            light.LoadDB(connection);
            light.SaveDBC("dbc/light.dbc");
        }

        static void Extractliquidtype(MySqlConnection connection)
        {
            liquidtypedbc liquidtype = new liquidtypedbc();
            liquidtype.LoadDB(connection);
            liquidtype.SaveDBC("dbc/liquidtype.dbc");
        }

        static void Extractmailtemplate(MySqlConnection connection)
        {
            mailtemplatedbc mailtemplate = new mailtemplatedbc();
            mailtemplate.LoadDB(connection);
            mailtemplate.SaveDBC("dbc/mailtemplate.dbc");
        }

        static void Extractmap(MySqlConnection connection)
        {
            mapdbc map = new mapdbc();
            map.LoadDB(connection);
            map.SaveDBC("dbc/map.dbc");
        }

        static void Extractmapdifficulty(MySqlConnection connection)
        {
            mapdifficultydbc mapdifficulty = new mapdifficultydbc();
            mapdifficulty.LoadDB(connection);
            mapdifficulty.SaveDBC("dbc/mapdifficulty.dbc");
        }

        static void Extractmovie(MySqlConnection connection)
        {
            moviedbc movie = new moviedbc();
            movie.LoadDB(connection);
            movie.SaveDBC("dbc/movie.dbc");
        }

        static void Extractoverridespelldata(MySqlConnection connection)
        {
            overridespelldatadbc overridespelldata = new overridespelldatadbc();
            overridespelldata.LoadDB(connection);
            overridespelldata.SaveDBC("dbc/overridespelldata.dbc");
        }

        static void Extractpowerdisplay(MySqlConnection connection)
        {
            powerdisplaydbc powerdisplay = new powerdisplaydbc();
            powerdisplay.LoadDB(connection);
            powerdisplay.SaveDBC("dbc/powerdisplay.dbc");
        }

        static void Extractpvpdifficulty(MySqlConnection connection)
        {
            pvpdifficultydbc pvpdifficulty = new pvpdifficultydbc();
            pvpdifficulty.LoadDB(connection);
            pvpdifficulty.SaveDBC("dbc/pvpdifficulty.dbc");
        }

        static void Extractquestfactionrew(MySqlConnection connection)
        {
            questfactionrewdbc questfactionrew = new questfactionrewdbc();
            questfactionrew.LoadDB(connection);
            questfactionrew.SaveDBC("dbc/questfactionrew.dbc");
        }

        static void Extractquestsort(MySqlConnection connection)
        {
            questsortdbc questsort = new questsortdbc();
            questsort.LoadDB(connection);
            questsort.SaveDBC("dbc/questsort.dbc");
        }

        static void Extractquestxp(MySqlConnection connection)
        {
            questxpdbc questxp = new questxpdbc();
            questxp.LoadDB(connection);
            questxp.SaveDBC("dbc/questxp.dbc");
        }

        static void Extractrandproppoints(MySqlConnection connection)
        {
            randproppointsdbc randproppoints = new randproppointsdbc();
            randproppoints.LoadDB(connection);
            randproppoints.SaveDBC("dbc/randproppoints.dbc");
        }

        static void Extractscalingstatdistribution(MySqlConnection connection)
        {
            scalingstatdistributiondbc scalingstatdistribution = new scalingstatdistributiondbc();
            scalingstatdistribution.LoadDB(connection);
            scalingstatdistribution.SaveDBC("dbc/scalingstatdistribution.dbc");
        }

        static void Extractscalingstatvalues(MySqlConnection connection)
        {
            scalingstatvaluesdbc scalingstatvalues = new scalingstatvaluesdbc();
            scalingstatvalues.LoadDB(connection);
            scalingstatvalues.SaveDBC("dbc/scalingstatvalues.dbc");
        }

        static void Extractskilllineability(MySqlConnection connection)
        {
            skilllineabilitydbc skilllineability = new skilllineabilitydbc();
            skilllineability.LoadDB(connection);
            skilllineability.SaveDBC("dbc/skilllineability.dbc");
        }

        static void Extractskillline(MySqlConnection connection)
        {
            skilllinedbc skillline = new skilllinedbc();
            skillline.LoadDB(connection);
            skillline.SaveDBC("dbc/skillline.dbc");
        }

        static void Extractsoundentries(MySqlConnection connection)
        {
            soundentriesdbc soundentries = new soundentriesdbc();
            soundentries.LoadDB(connection);
            soundentries.SaveDBC("dbc/soundentries.dbc");
        }

        static void Extractspellcasttimes(MySqlConnection connection)
        {
            spellcasttimesdbc spellcasttimes = new spellcasttimesdbc();
            spellcasttimes.LoadDB(connection);
            spellcasttimes.SaveDBC("dbc/spellcasttimes.dbc");
        }

        static void Extractspellcategory(MySqlConnection connection)
        {
            spellcategorydbc spellcategory = new spellcategorydbc();
            spellcategory.LoadDB(connection);
            spellcategory.SaveDBC("dbc/spellcategory.dbc");
        }

        static void Extractspell(MySqlConnection connection)
        {
            spelldbc spell = new spelldbc();
            spell.LoadDB(connection);
            spell.SaveDBC("dbc/spell.dbc");
        }

        static void Extractspelldifficulty(MySqlConnection connection)
        {
            spelldifficultydbc spelldifficulty = new spelldifficultydbc();
            spelldifficulty.LoadDB(connection);
            spelldifficulty.SaveDBC("dbc/spelldifficulty.dbc");
        }

        static void Extractspellduration(MySqlConnection connection)
        {
            spelldurationdbc spellduration = new spelldurationdbc();
            spellduration.LoadDB(connection);
            spellduration.SaveDBC("dbc/spellduration.dbc");
        }

        static void Extractspellfocusobject(MySqlConnection connection)
        {
            spellfocusobjectdbc spellfocusobject = new spellfocusobjectdbc();
            spellfocusobject.LoadDB(connection);
            spellfocusobject.SaveDBC("dbc/spellfocusobject.dbc");
        }

        static void Extractspellitemenchantmentcondition(MySqlConnection connection)
        {
            spellitemenchantmentconditiondbc spellitemenchantmentcondition = new spellitemenchantmentconditiondbc();
            spellitemenchantmentcondition.LoadDB(connection);
            spellitemenchantmentcondition.SaveDBC("dbc/spellitemenchantmentcondition.dbc");
        }

        static void Extractspellitemenchantment(MySqlConnection connection)
        {
            spellitemenchantmentdbc spellitemenchantment = new spellitemenchantmentdbc();
            spellitemenchantment.LoadDB(connection);
            spellitemenchantment.SaveDBC("dbc/spellitemenchantment.dbc");
        }

        static void Extractspellradius(MySqlConnection connection)
        {
            spellradiusdbc spellradius = new spellradiusdbc();
            spellradius.LoadDB(connection);
            spellradius.SaveDBC("dbc/spellradius.dbc");
        }

        static void Extractspellrange(MySqlConnection connection)
        {
            spellrangedbc spellrange = new spellrangedbc();
            spellrange.LoadDB(connection);
            spellrange.SaveDBC("dbc/spellrange.dbc");
        }

        static void Extractspellrunecost(MySqlConnection connection)
        {
            spellrunecostdbc spellrunecost = new spellrunecostdbc();
            spellrunecost.LoadDB(connection);
            spellrunecost.SaveDBC("dbc/spellrunecost.dbc");
        }

        static void Extractspellshapesshift(MySqlConnection connection)
        {
            spellshapesshiftdbc spellshapesshift = new spellshapesshiftdbc();
            spellshapesshift.LoadDB(connection);
            spellshapesshift.SaveDBC("dbc/spellshapesshift.dbc");
        }

        static void Extractstableslotprices(MySqlConnection connection)
        {
            stableslotpricesdbc stableslotprices = new stableslotpricesdbc();
            stableslotprices.LoadDB(connection);
            stableslotprices.SaveDBC("dbc/stableslotprices.dbc");
        }

        static void Extractsummonproperties(MySqlConnection connection)
        {
            summonpropertiesdbc summonproperties = new summonpropertiesdbc();
            summonproperties.LoadDB(connection);
            summonproperties.SaveDBC("dbc/summonproperties.dbc");
        }

        static void Extracttalent(MySqlConnection connection)
        {
            talentdbc talent = new talentdbc();
            talent.LoadDB(connection);
            talent.SaveDBC("dbc/talent.dbc");
        }

        static void Extracttalenttab(MySqlConnection connection)
        {
            talenttabdbc talenttab = new talenttabdbc();
            talenttab.LoadDB(connection);
            talenttab.SaveDBC("dbc/talenttab.dbc");
        }

        static void Extracttaxinodes(MySqlConnection connection)
        {
            taxinodesdbc taxinodes = new taxinodesdbc();
            taxinodes.LoadDB(connection);
            taxinodes.SaveDBC("dbc/taxinodes.dbc");
        }

        static void Extracttaxipath(MySqlConnection connection)
        {
            taxipathdbc taxipath = new taxipathdbc();
            taxipath.LoadDB(connection);
            taxipath.SaveDBC("dbc/taxipath.dbc");
        }

        static void Extracttaxipathnode(MySqlConnection connection)
        {
            taxipathnodedbc taxipathnode = new taxipathnodedbc();
            taxipathnode.LoadDB(connection);
            taxipathnode.SaveDBC("dbc/taxipathnode.dbc");
        }

        static void Extractteamcontributionpoints(MySqlConnection connection)
        {
            teamcontributionpointsdbc teamcontributionpoints = new teamcontributionpointsdbc();
            teamcontributionpoints.LoadDB(connection);
            teamcontributionpoints.SaveDBC("dbc/teamcontributionpoints.dbc");
        }

        static void Extracttotemcategory(MySqlConnection connection)
        {
            totemcategorydbc totemcategory = new totemcategorydbc();
            totemcategory.LoadDB(connection);
            totemcategory.SaveDBC("dbc/totemcategory.dbc");
        }

        static void Extracttransportanimation(MySqlConnection connection)
        {
            transportanimationdbc transportanimation = new transportanimationdbc();
            transportanimation.LoadDB(connection);
            transportanimation.SaveDBC("dbc/transportanimation.dbc");
        }

        static void Extracttransportrotation(MySqlConnection connection)
        {
            transportrotationdbc transportrotation = new transportrotationdbc();
            transportrotation.LoadDB(connection);
            transportrotation.SaveDBC("dbc/transportrotation.dbc");
        }

        static void Extractvehicle(MySqlConnection connection)
        {
            vehicledbc vehicle = new vehicledbc();
            vehicle.LoadDB(connection);
            vehicle.SaveDBC("dbc/vehicle.dbc");
        }

        static void Extractvehicleseat(MySqlConnection connection)
        {
            vehicleseatdbc vehicleseat = new vehicleseatdbc();
            vehicleseat.LoadDB(connection);
            vehicleseat.SaveDBC("dbc/vehicleseat.dbc");
        }

        static void Extractwmoareatable(MySqlConnection connection)
        {
            wmoareatabledbc wmoareatable = new wmoareatabledbc();
            wmoareatable.LoadDB(connection);
            wmoareatable.SaveDBC("dbc/wmoareatable.dbc");
        }

        static void Extractworldmapoverlay(MySqlConnection connection)
        {
            worldmapoverlaydbc worldmapoverlay = new worldmapoverlaydbc();
            worldmapoverlay.LoadDB(connection);
            worldmapoverlay.SaveDBC("dbc/worldmapoverlay.dbc");
        }

        static void Extractworldsafelocsdbc(MySqlConnection connection)
        {
            worldsafelocsdbc worldsafelocs = new worldsafelocsdbc();
            worldsafelocs.LoadDB(connection);
            worldsafelocs.SaveDBC("dbc/worldsafelocs.dbc");
        }

        public static void ExtractAll(MySqlConnection connection)
        {
            Extractachievement_criteria(connection);
            Extractachievement(connection);
            Extractareagroup(connection);
            Extractareapoi(connection);
            Extractareatable(connection);
            Extractareatrigger(connection);
            Extractauctionhouse(connection);
            Extractbankbagslotprices(connection);
            Extractbannedaddons(connection);
            Extractbarbershopstyle(connection);
            Extractbattlemasterlist(connection);
            Extractchartitles(connection);
            Extractchatchannels(connection);
            Extractchrclasses(connection);
            Extractchrraces(connection);
            Extractcinematicsequences(connection);
            Extractcreaturedisplayinfo(connection);
            Extractcreaturedisplayinfoextra(connection);
            Extractcreaturefamily(connection);
            Extractcreaturemodeldata(connection);
            Extractcreaturespelldata(connection);
            Extractcreaturetype(connection);
            Extractcurrencytypes(connection);
            Extractdestructiblemodeldata(connection);
            Extractdungeonencounter(connection);
            Extractdurabilitycosts(connection);
            Extractdurabilityquality(connection);
            Extractemotes(connection);
            Extractemotestext(connection);
            Extractfaction(connection);
            Extractfactiontemplate(connection);
            Extractgameobjectdisplayinfo(connection);
            Extractgemproperties(connection);
            Extractglyphproperties(connection);
            Extractglyphslot(connection);
            Extractholidays(connection);
            Extractitembagfamily(connection);
            Extractitemextendedcost(connection);
            Extractitemlimitcategory(connection);
            Extractitemrandomproperties(connection);
            Extractitemrandomsuffix(connection);
            Extractitemset(connection);
            Extractlfgdungeons(connection);
            Extractlight(connection);
            Extractliquidtype(connection);
            Extractmailtemplate(connection);
            Extractmap(connection);
            Extractmapdifficulty(connection);
            Extractmovie(connection);
            Extractoverridespelldata(connection);
            Extractpowerdisplay(connection);
            Extractpvpdifficulty(connection);
            Extractquestfactionrew(connection);
            Extractquestsort(connection);
            Extractquestxp(connection);
            Extractrandproppoints(connection);
            Extractscalingstatdistribution(connection);
            Extractscalingstatvalues(connection);
            Extractskilllineability(connection);
            Extractskillline(connection);
            Extractsoundentries(connection);
            Extractspellcasttimes(connection);
            Extractspellcategory(connection);
            Extractspell(connection);
            Extractspelldifficulty(connection);
            Extractspellduration(connection);
            Extractspellfocusobject(connection);
            Extractspellitemenchantmentcondition(connection);
            Extractspellitemenchantment(connection);
            Extractspellradius(connection);
            Extractspellrange(connection);
            Extractspellrunecost(connection);
            Extractspellshapesshift(connection);
            Extractstableslotprices(connection);
            Extractsummonproperties(connection);
            Extracttalent(connection);
            Extracttalenttab(connection);
            Extracttaxinodes(connection);
            Extracttaxipath(connection);
            Extracttaxipathnode(connection);
            Extractteamcontributionpoints(connection);
            Extracttotemcategory(connection);
            Extracttransportanimation(connection);
            Extracttransportrotation(connection);
            Extractvehicle(connection);
            Extractvehicleseat(connection);
            Extractwmoareatable(connection);
            Extractworldmapoverlay(connection);
            Extractworldsafelocsdbc(connection);
        }
    }
}

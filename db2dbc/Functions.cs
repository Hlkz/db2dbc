using System;
using MySql.Data.MySqlClient;

namespace DBtoDBC
{
    class DB2DBC
    {
        public static uint GlobalLocalization = 0;
        public static string ConnectionServer = "localhost";
        public static string ConnectionUsername = "root";
        public static string ConnectionPassword = "";
        public static string WorldDatabase = "iwpw";
        public static string UnusedDatabase = "iwpu";

        public static void CheckDatabase(MySqlConnection connection, string database)
        {
            if (connection.Database != database)
                connection.ChangeDatabase(database);
        }

        public static void ExtractWorld(MySqlConnection connection)
        {
            DBCExtract.achievement_criteria(connection);
            DBCExtract.achievement(connection);
            DBCExtract.areagroup(connection);
            DBCExtract.areapoi(connection);
            DBCExtract.areatable(connection);
            DBCExtract.areatrigger(connection);
            DBCExtract.auctionhouse(connection);
            DBCExtract.bankbagslotprices(connection);
            DBCExtract.bannedaddons(connection);
            DBCExtract.barbershopstyle(connection);
            DBCExtract.battlemasterlist(connection);
            DBCExtract.charstartoutfit(connection);
            DBCExtract.chartitles(connection);
            DBCExtract.chatchannels(connection);
            DBCExtract.chrclasses(connection);
            DBCExtract.chrraces(connection);
            DBCExtract.cinematicsequences(connection);
            DBCExtract.creaturedisplayinfo(connection);
            DBCExtract.creaturedisplayinfoextra(connection);
            DBCExtract.creaturefamily(connection);
            DBCExtract.creaturemodeldata(connection);
            DBCExtract.creaturespelldata(connection);
            DBCExtract.creaturetype(connection);
            DBCExtract.currencytypes(connection);
            DBCExtract.destructiblemodeldata(connection);
            DBCExtract.dungeonencounter(connection);
            DBCExtract.durabilitycosts(connection);
            DBCExtract.durabilityquality(connection);
            DBCExtract.emotes(connection);
            DBCExtract.emotestext(connection);
            DBCExtract.faction(connection);
            DBCExtract.factiontemplate(connection);
            DBCExtract.gameobjectdisplayinfo(connection);
            DBCExtract.gemproperties(connection);
            DBCExtract.glyphproperties(connection);
            DBCExtract.glyphslot(connection);
            DBCExtract.holidays(connection);
            DBCExtract.item(connection);
            DBCExtract.itembagfamily(connection);
            DBCExtract.itemextendedcost(connection);
            DBCExtract.itemlimitcategory(connection);
            DBCExtract.itemrandomproperties(connection);
            DBCExtract.itemrandomsuffix(connection);
            DBCExtract.itemset(connection);
            DBCExtract.lfgdungeons(connection);
            DBCExtract.light(connection);
            DBCExtract.liquidtype(connection);
            DBCExtract.lockd(connection);
            DBCExtract.mailtemplate(connection);
            DBCExtract.map(connection);
            DBCExtract.mapdifficulty(connection);
            DBCExtract.movie(connection);
            DBCExtract.overridespelldata(connection);
            DBCExtract.powerdisplay(connection);
            DBCExtract.pvpdifficulty(connection);
            DBCExtract.questfactionrew(connection);
            DBCExtract.questsort(connection);
            DBCExtract.questxp(connection);
            DBCExtract.randproppoints(connection);
            DBCExtract.scalingstatdistribution(connection);
            DBCExtract.scalingstatvalues(connection);
            DBCExtract.skilllineability(connection);
            DBCExtract.skillline(connection);
            DBCExtract.skillraceclassinfo(connection);
            DBCExtract.skilltiers(connection);
            DBCExtract.soundentries(connection);
            DBCExtract.spellcasttimes(connection);
            DBCExtract.spellcategory(connection);
            DBCExtract.spell(connection);
            DBCExtract.spelldifficulty(connection);
            DBCExtract.spellduration(connection);
            DBCExtract.spellfocusobject(connection);
            DBCExtract.spellitemenchantmentcondition(connection);
            DBCExtract.spellitemenchantment(connection);
            DBCExtract.spellradius(connection);
            DBCExtract.spellrange(connection);
            DBCExtract.spellrunecost(connection);
            DBCExtract.spellshapeshiftform(connection);
            DBCExtract.stableslotprices(connection);
            DBCExtract.summonproperties(connection);
            DBCExtract.talent(connection);
            DBCExtract.talenttab(connection);
            DBCExtract.taxinodes(connection);
            DBCExtract.taxipath(connection);
            DBCExtract.taxipathnode(connection);
            DBCExtract.teamcontributionpoints(connection);
            DBCExtract.totemcategory(connection);
            DBCExtract.transportanimation(connection);
            DBCExtract.transportrotation(connection);
            DBCExtract.vehicle(connection);
            DBCExtract.vehicleseat(connection);
            DBCExtract.wmoareatable(connection);
            DBCExtract.worldmaparea(connection);
            DBCExtract.worldmapoverlay(connection);
            DBCExtract.worldsafelocsdbc(connection);
        }

        public static void ExtractUnused(MySqlConnection connection)
        {
            DBCExtract.achievement_category(connection);
            DBCExtract.animationdata(connection);
            DBCExtract.attackanimkits(connection);
            DBCExtract.attackanimtypes(connection);
            DBCExtract.camerashakes(connection);
            DBCExtract.cfg_categories(connection);
            DBCExtract.cfg_configs(connection);
            DBCExtract.characterfacialhairstyles(connection);
            DBCExtract.charbaseinfo(connection);
            DBCExtract.charhairgeosets(connection);
            DBCExtract.charhairtextures(connection);
            DBCExtract.charsections(connection);
            DBCExtract.chatprofanity(connection);
            DBCExtract.cinematiccamera(connection);
            DBCExtract.creaturemovementinfo(connection);
            DBCExtract.creaturesounddata(connection);
            DBCExtract.currencycategory(connection);
            DBCExtract.dancemoves(connection);
            DBCExtract.deaththudlookups(connection);
            DBCExtract.declinedword(connection);
            DBCExtract.declinedwordcases(connection);
            DBCExtract.dungeonmap(connection);
            DBCExtract.dungeonmapchunk(connection);
            DBCExtract.emotestextdata(connection);
            DBCExtract.emotestextsound(connection);
            DBCExtract.environmentaldamage(connection);
            DBCExtract.exhaustion(connection);
            DBCExtract.factiongroup(connection);
            DBCExtract.filedata(connection);
            DBCExtract.footprinttextures(connection);
            DBCExtract.footstepterrainlookup(connection);
            DBCExtract.gameobjectartkit(connection);
            DBCExtract.gametables(connection);
            DBCExtract.gametips(connection);
            DBCExtract.gmsurveyanswers(connection);
            DBCExtract.gmsurveycurrentsurvey(connection);
            DBCExtract.gmsurveyquestions(connection);
            DBCExtract.gmsurveysurveys(connection);
            DBCExtract.gmticketcategory(connection);
            DBCExtract.groundeffectdoodad(connection);
            DBCExtract.groundeffecttexture(connection);
            DBCExtract.gtbarbershopcostbase(connection);
            DBCExtract.gtchancetomeleecrit(connection);
            DBCExtract.gtchancetomeleecritbase(connection);
            DBCExtract.gtchancetospellcrit(connection);
            DBCExtract.gtchancetospellcritbase(connection);
            DBCExtract.gtcombatratings(connection);
            DBCExtract.gtnpcmanacostscaler(connection);
            DBCExtract.gtoctclasscombatratingscalar(connection);
            DBCExtract.gtoctregenhp(connection);
            DBCExtract.gtoctregenmp(connection);
            DBCExtract.gtregenhpperspt(connection);
            DBCExtract.gtregenmpperspt(connection);
            DBCExtract.helmetgeosetvisdata(connection);
            DBCExtract.holidaydescriptions(connection);
            DBCExtract.holidaynames(connection);
            DBCExtract.itemclass(connection);
            DBCExtract.itemcondextcosts(connection);
            DBCExtract.itemdisplayinfo(connection);
            DBCExtract.itemgroupsounds(connection);
            DBCExtract.itempetfood(connection);
            DBCExtract.itempurchasegroup(connection);
            DBCExtract.itemsubclass(connection);
            DBCExtract.itemsubclassmask(connection);
            DBCExtract.itemvisualeffects(connection);
            DBCExtract.itemvisuals(connection);
            DBCExtract.languages(connection);
            DBCExtract.languagewords(connection);
            DBCExtract.lfgdungeonexpansion(connection);
            DBCExtract.lfgdungeongroup(connection);
            DBCExtract.lightfloatband(connection);
            DBCExtract.lightintband(connection);
            DBCExtract.lightparams(connection);
            DBCExtract.lightskybox(connection);
            DBCExtract.liquidmaterial(connection);
            DBCExtract.loadingscreens(connection);
            DBCExtract.loadingscreentaxisplines(connection);
            DBCExtract.locktype(connection);
            DBCExtract.material(connection);
            DBCExtract.moviefiledata(connection);
            DBCExtract.movievariation(connection);
            DBCExtract.namegen(connection);
            DBCExtract.namesprofanity(connection);
            DBCExtract.namesreserved(connection);
            DBCExtract.npcsounds(connection);
            DBCExtract.objecteffect(connection);
            DBCExtract.objecteffectgroup(connection);
            DBCExtract.objecteffectmodifier(connection);
            DBCExtract.objecteffectpackage(connection);
            DBCExtract.objecteffectpackageelem(connection);
            DBCExtract.package(connection);
            DBCExtract.pagetextmaterial(connection);
            DBCExtract.paperdollitemframe(connection);
            DBCExtract.particlecolor(connection);
            DBCExtract.petitiontype(connection);
            DBCExtract.petpersonality(connection);
            DBCExtract.questinfo(connection);
            DBCExtract.resistances(connection);
            DBCExtract.screeneffect(connection);
            DBCExtract.servermessages(connection);
            DBCExtract.sheathesoundlookups(connection);
            DBCExtract.skillcostsdata(connection);
            DBCExtract.skilllinecategory(connection);
            DBCExtract.soundambience(connection);
            DBCExtract.soundemitters(connection);
            DBCExtract.soundentriesadvanced(connection);
            DBCExtract.soundfilter(connection);
            DBCExtract.soundfilterelem(connection);
            DBCExtract.soundproviderpreferences(connection);
            DBCExtract.soundsamplepreferences(connection);
            DBCExtract.soundwatertype(connection);
            DBCExtract.spammessages(connection);
            DBCExtract.spellchaineffects(connection);
            DBCExtract.spelldescriptionvariables(connection);
            DBCExtract.spelldispeltype(connection);
            DBCExtract.spelleffectcamerashakes(connection);
            DBCExtract.spellicon(connection);
            DBCExtract.spellmechanic(connection);
            DBCExtract.spellmissile(connection);
            DBCExtract.spellmissilemotion(connection);
            DBCExtract.spellvisual(connection);
            DBCExtract.spellvisualeffectname(connection);
            DBCExtract.spellvisualkit(connection);
            DBCExtract.spellvisualkitareamodel(connection);
            DBCExtract.spellvisualkitmodelattach(connection);
            DBCExtract.spellvisualprecasttransitions(connection);
            DBCExtract.startup_strings(connection);
            DBCExtract.stationery(connection);
            DBCExtract.stringlookups(connection);
            DBCExtract.terraintype(connection);
            DBCExtract.terraintypesounds(connection);
            DBCExtract.transportphysics(connection);
            DBCExtract.uisoundlookups(connection);
            DBCExtract.unitblood(connection);
            DBCExtract.unitbloodlevels(connection);
            DBCExtract.vehicleuiindicator(connection);
            DBCExtract.vehicleuiindseat(connection);
            DBCExtract.videohardware(connection);
            DBCExtract.vocaluisounds(connection);
            DBCExtract.weaponimpactsounds(connection);
            DBCExtract.weaponswingsounds2(connection);
            DBCExtract.weather(connection);
            DBCExtract.worldchunksounds(connection);
            DBCExtract.worldmapcontinent(connection);
            DBCExtract.worldmaptransforms(connection);
            DBCExtract.worldstateui(connection);
            DBCExtract.worldstatezonesounds(connection);
            DBCExtract.wowerror_strings(connection);
            DBCExtract.zoneintromusictable(connection);
            DBCExtract.zonemusic(connection);
        }
        
        public static void ExtractAll(MySqlConnection connection)
        {
            ExtractWorld(connection);
            connection.ChangeDatabase(DB2DBC.UnusedDatabase);
            ExtractUnused(connection);
        }

        public static void ExtractSpells(MySqlConnection connection)
        {
            DBCExtract.spellcasttimes(connection);
            DBCExtract.spellcategory(connection);
            DBCExtract.spell(connection);
            DBCExtract.spelldifficulty(connection);
            DBCExtract.spellduration(connection);
            DBCExtract.spellfocusobject(connection);
            DBCExtract.spellitemenchantmentcondition(connection);
            DBCExtract.spellitemenchantment(connection);
            DBCExtract.spellradius(connection);
            DBCExtract.spellrange(connection);
            DBCExtract.spellrunecost(connection);
            DBCExtract.spellshapeshiftform(connection);
        }
    }
}

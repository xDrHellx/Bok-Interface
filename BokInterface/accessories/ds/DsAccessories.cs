using System.Collections.Generic;
using System.Linq;

namespace BokInterface.Accessories {
    /// <summary>Class for Boktai DS / Lunar Knights accessories instances and lists</summary>
    class DsAccessories {

        public Dictionary<string, Accessory> Head = [],
            Torso = [],
            Foot = [],
            Shield = [],
            Equipment = [],
            All = [];

        public DsAccessories() {
            InitHeadList();
            InitTorsoList();
            InitFootList();
            InitShieldList();
            InitEquipmentsList();
            InitFullList();
        }

        ///<summary>Init accessory instances for Head accessories</summary>
        private void InitHeadList() {
            string type = "head";
            Head.Add("Dark Eye", new DsAccessory("Dark Eye", 0, type, "dark_eye", "+2 SPR"));
            Head.Add("Training Goggles", new DsAccessory("Training Goggles", 1, type, "training_goggles", "+4 SPR"));
            Head.Add("Muspell Headgear", new DsAccessory("Muspell Headgear", 2, type, "muspell_headgear", "+6 SPR under clear skies"));
            Head.Add("Hresvelgr Headgear", new DsAccessory("Hresvelgr Headgear", 3, type, "hresvelgr_headgear", "+6 SPR under cloudy skies"));
            Head.Add("Nidhoggr Headgear", new DsAccessory("Nidhoggr Headgear", 4, type, "nidhoggr_headgear", "+6 SPR under rainy skies"));
            Head.Add("Garmr Headgear", new DsAccessory("Garmr Headgear", 5, type, "garmr_headgear", "+6 SPR under snowy skies"));
            Head.Add("Moonlight Beauty", new DsAccessory("Moonlight Beauty", 6, type, "moonlight_beauty", "+50% ENE charge speed"));
            Head.Add("Earth Amulet", new DsAccessory("Earth Amulet", 7, type, "earth_amulet", "Immune to poison"));
            Head.Add("Wind Amulet", new DsAccessory("Wind Amulet", 8, type, "wind_amulet", "Immune to confusion and fainting"));
            Head.Add("Ice Flame Amulet", new DsAccessory("Ice Flame Amulet", 9, type, "ice_flame_amulet", "Immune to burning and freezing"));
            Head.Add("Burning Headband", new DsAccessory("Burning Headband", 10, type, "burning_headband", "Automatically uses a healing item when Life reaches 0"));
            Head.Add("Cool Bandanna", new DsAccessory("Cool Bandanna", 11, type, "cool_bandanna", "Automatically uses a healing item when ENE reaches 0"));
            Head.Add("Eclipse Eye", new DsAccessory("Eclipse Eye", 12, type, "eclipse_eye", "Lucian / Sabata only | Allows charging under sunlight"));
            Head.Add("Eclipse Goggles", new DsAccessory("Eclipse Goggles", 13, type, "eclipse_goggles", "Aaron / Django only | Allows charging under moonlight"));
            Head.Add("Cursed Mask", new DsAccessory("Cursed Mask", 14, type, "cursed_mask", "-8 SPR"));
            Head.Add("Warrior's Mask", new DsAccessory("Warrior's Mask", 15, type, "warrior_s_mask", "+8 SPR"));
            Head.Add("PegasusRockHead", new DsAccessory("PegasusRockHead", 16, type, "pegasusrockhead", "Increases items durability based on BrotherBand level", crossOver: true));
        }

        ///<summary>Init accessory instances for Torso accessories</summary>
        private void InitTorsoList() {
            string type = "torso";
            Torso.Add("Leather Suit", new DsAccessory("Leather Suit", 17, type, "leather_suit", "+2 VIT"));
            Torso.Add("Battle Suit", new DsAccessory("Battle Suit", 18, type, "battle_suit", "+4 VIT"));
            Torso.Add("Muspell Arms", new DsAccessory("Muspell Arms", 19, type, "muspell_arms", "+6 VIT under clear skies"));
            Torso.Add("Hresvelgr Arms", new DsAccessory("Hresvelgr Arms", 20, type, "hresvelgr_arms", "+6 VIT under cloudy skies"));
            Torso.Add("Nidhoggr Arms", new DsAccessory("Nidhoggr Arms", 21, type, "nidhoggr_arms", "+6 VIT under rainy skies"));
            Torso.Add("Garmr Arms", new DsAccessory("Garmr Arms", 22, type, "garmr_arms", "+6 VIT under snowy skies"));
            Torso.Add("Ninja_suit", new DsAccessory("Ninja_suit", 23, type, "ninja_suit", "Increases Dark resistance"));
            Torso.Add("Hot_space", new DsAccessory("Hot_space", 24, type, "hot_space", "Higher temperature = less damage taken"));
            Torso.Add("Winter's Tale", new DsAccessory("Winter's Tale", 25, type, "winter_s_tale", "Lower temperature = less damage taken"));
            Torso.Add("Wild Wind", new DsAccessory("Wild Wind", 26, type, "wild_wind", "Stronger wind = less damage taken"));
            Torso.Add("Staying Power", new DsAccessory("Staying Power", 27, type, "staying_power", "Higher humidity = less damage taken"));
            Torso.Add("Green Leaf Cape", new DsAccessory("Green Leaf Cape", 28, type, "green_leaf_cape", "Solar Bugs becomes twice as efficient"));
            Torso.Add("Blood Sucker", new DsAccessory("Blood Sucker", 29, type, "blood_sucker", "Lucian / Sabata only | Restores Life when defeating enemies"));
            Torso.Add("Soul Saver", new DsAccessory("Soul Saver", 30, type, "soul_saver", "Aaron / Django only | Restores ENE when defeating enemies"));
            Torso.Add("Cursed Scarf", new DsAccessory("Cursed Scarf", 31, type, "cursed_scarf", "-8 VIT"));
            Torso.Add("Warrior's Scarf", new DsAccessory("Warrior's Scarf", 32, type, "warrior_s_scarf", "+8 VIT"));
            Torso.Add("DragonRockBody", new DsAccessory("DragonRockBody", 33, type, "dragonrockbody", "Reduces damage taken based on BrotherBand level", crossOver: true));
        }

        ///<summary>Init accessory instances for Foot accessories</summary>
        private void InitFootList() {
            string type = "foot";
            Foot.Add("Leather Boots", new DsAccessory("Leather Boots", 34, type, "leather_boots", "+2 SKILL"));
            Foot.Add("Battle Boots", new DsAccessory("Battle Boots", 35, type, "battle_boots", "+4 SKILL"));
            Foot.Add("Muspell Legs", new DsAccessory("Muspell Legs", 36, type, "muspell_legs", "+6 SKILL under clear skies"));
            Foot.Add("Hresvelgr Legs", new DsAccessory("Hresvelgr Legs", 37, type, "hresvelgr_legs", "+6 SKILL under cloudy skies"));
            Foot.Add("Nidhoggr Legs", new DsAccessory("Nidhoggr Legs", 38, type, "nidhoggr_legs", "+6 SKILL under rainy skies"));
            Foot.Add("Garmr Legs", new DsAccessory("Garmr Legs", 39, type, "garmr_legs", "+6 SKILL under snowy skies"));
            Foot.Add("Fire Starter", new DsAccessory("Fire Starter", 40, type, "fire_starter", "Higher temperature = higher TRC rate"));
            Foot.Add("Ice Breaker", new DsAccessory("Ice Breaker", 41, type, "ice_breaker", "Lower temperature = higher TRC rate"));
            Foot.Add("Sky Runner", new DsAccessory("Sky Runner", 42, type, "sky_runner", "Stronger wind = higher TRC rate"));
            Foot.Add("Grass Dancer", new DsAccessory("Grass Dancer", 43, type, "grass_dancer", "Higher humidity = higher TRC rate"));
            Foot.Add("Day Walker", new DsAccessory("Day Walker", 44, type, "day_walker", "Aaron / Django only | Alters damage dealty based on Solar Gauge"));
            Foot.Add("Night Stalker", new DsAccessory("Night Stalker", 45, type, "night_stalker", "Lucian / Sabata only | Alters damage dealt based on Lunar Gauge"));
            Foot.Add("Training Boots", new DsAccessory("Training Boots", 46, type, "training_boots", "2x EXP from enemies | Enemies stops dropping Solls"));
            Foot.Add("Bounty Boots", new DsAccessory("Bounty Boots", 47, type, "bounty_boots", "2x Solls from enemies | Enemies stops giving EXP"));
            Foot.Add("Cursed Boots", new DsAccessory("Cursed Boots", 48, type, "cursed_boots", "-8 SKILL"));
            Foot.Add("Warrior's Boots", new DsAccessory("Warrior's Boots", 49, type, "warrior_s_boots", "+8 SKILL"));
            Foot.Add("LeoRockLeg", new DsAccessory("LeoRockLeg", 50, type, "leorockleg", "Disable gimmicks based on BrotherBand level", crossOver: true));
        }

        ///<summary>Init accessory instances for shields</summary>
        private void InitShieldList() {
            Shield.Add("Silver Star", new DsShield("Silver Star", 51, "silver_star", "Increases TRC rate when guarding"));
            Shield.Add("Red Cross", new DsShield("Red Cross", 52, "red_cross", "Increases status ailments resistance | Restores Life when guarding"));
            Shield.Add("Snake Eyes", new DsShield("Snake Eyes", 53, "snake_eyes", "Automatically guards when raising shield"));
            Shield.Add("Blue Spine", new DsShield("Blue Spine", 54, "blue_spine", "Reflects damage when guarding"));
            Shield.Add("Silver Star (duplicate)", new DsShield("Silver Star (duplicate)", 55, "silver_star", "Increases TRC rate when guarding"));
        }

        ///<summary>Init the list containing Equipments accessories related to equipments (mostly used for editors)</summary>
        private void InitEquipmentsList() {
            Equipment.Add("Empty slot", new DsAccessory("Empty slot", 65535, ""));
            Equipment = Equipment
                .Concat(Head)
                .Concat(Torso)
                .Concat(Foot)
                .ToDictionary(e => e.Key, e => e.Value);
        }

        ///<summary>Init the full list containing all accessories (mostly used for editors)</summary>
        private void InitFullList() {
            All = Equipment
                .Concat(Shield)
                .ToDictionary(e => e.Key, e => e.Value);
        }
    }
}

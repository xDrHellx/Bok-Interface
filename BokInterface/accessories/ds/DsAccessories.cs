using System.Collections.Generic;
using System.Linq;

namespace BokInterface.Accessories {
    /// <summary>Class for Boktai DS / Lunar Knights accessories instances and lists</summary>
    class DsAccessories {

        public Dictionary<string, Accessory> Head = [],
            Torso = [],
            Foot = [],
            Shield = [],
            Equipments = [],
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
            Head.Add("Dark Eye", new DsAccessory("Dark Eye", 0, type, "dark_eye"));
            Head.Add("Training Goggles", new DsAccessory("Training Goggles", 1, type, "training_goggles"));
            Head.Add("Muspell Headgear", new DsAccessory("Muspell Headgear", 2, type, "muspell_headgear"));
            Head.Add("Hresvelgr Headgear", new DsAccessory("Hresvelgr Headgear", 3, type, "hresvelgr_headgear"));
            Head.Add("Nidhoggr Headgear", new DsAccessory("Nidhoggr Headgear", 4, type, "nidhoggr_headgear"));
            Head.Add("Garmr Headgear", new DsAccessory("Garmr Headgear", 5, type, "garmr_headgear"));
            Head.Add("Moonlight Beauty", new DsAccessory("Moonlight Beauty", 6, type, "moonlight_beauty"));
            Head.Add("Earth Amulet", new DsAccessory("Earth Amulet", 7, type, "earth_amulet"));
            Head.Add("Wind Amulet", new DsAccessory("Wind Amulet", 8, type, "wind_amulet"));
            Head.Add("Ice Flame Amulet", new DsAccessory("Ice Flame Amulet", 9, type, "ice_flame_amulet"));
            Head.Add("Burning Headband", new DsAccessory("Burning Headband", 10, type, "burning_headband"));
            Head.Add("Cool Bandanna", new DsAccessory("Cool Bandanna", 11, type, "cool_bandanna"));
            Head.Add("Eclipse Eye", new DsAccessory("Eclipse Eye", 12, type, "eclipse_eye"));
            Head.Add("Eclipse Goggles", new DsAccessory("Eclipse Goggles", 13, type, "eclipse_goggles"));
            Head.Add("Cursed Mask", new DsAccessory("Cursed Mask", 14, type, "cursed_mask"));
            Head.Add("Warrior's Mask", new DsAccessory("Warrior's Mask", 15, type, "warrior_s_mask"));
            Head.Add("PegasusRockHead", new DsAccessory("PegasusRockHead", 16, type, "pegasusrockhead"));
        }

        ///<summary>Init accessory instances for Torso accessories</summary>
        private void InitTorsoList() {
            string type = "torso";
            Torso.Add("Leather Suit", new DsAccessory("Leather Suit", 17, type, "leather_suit"));
            Torso.Add("Battle Suit", new DsAccessory("Battle Suit", 18, type, "battle_suit"));
            Torso.Add("Muspell Arms", new DsAccessory("Muspell Arms", 19, type, "muspell_arms"));
            Torso.Add("Hresvelgr Arms", new DsAccessory("Hresvelgr Arms", 20, type, "hresvelgr_arms"));
            Torso.Add("Nidhoggr Arms", new DsAccessory("Nidhoggr Arms", 21, type, "nidhoggr_arms"));
            Torso.Add("Garmr Arms", new DsAccessory("Garmr Arms", 22, type, "garmr_arms"));
            Torso.Add("Ninja_suit", new DsAccessory("Ninja_suit", 23, type, "ninja_suit"));
            Torso.Add("Hot_space", new DsAccessory("Hot_space", 24, type, "hot_space"));
            Torso.Add("Winter's Tale", new DsAccessory("Winter's Tale", 25, type, "winter_s_tale"));
            Torso.Add("Wild Wind", new DsAccessory("Wild Wind", 26, type, "wild_wind"));
            Torso.Add("Staying Power", new DsAccessory("Staying Power", 27, type, "staying_power"));
            Torso.Add("Green Leaf Cape", new DsAccessory("Green Leaf Cape", 28, type, "green_leaf_cape"));
            Torso.Add("Blood Sucker", new DsAccessory("Blood Sucker", 29, type, "blood_sucker"));
            Torso.Add("Soul Saver", new DsAccessory("Soul Saver", 30, type, "soul_saver"));
            Torso.Add("Cursed Scarf", new DsAccessory("Cursed Scarf", 31, type, "cursed_scarf"));
            Torso.Add("Warrior's Scarf", new DsAccessory("Warrior's Scarf", 32, type, "warrior_s_scarf"));
            Torso.Add("DragonRockBody", new DsAccessory("DragonRockBody", 33, type, "dragonrockbody"));
        }

        ///<summary>Init accessory instances for Foot accessories</summary>
        private void InitFootList() {
            string type = "foot";
            Foot.Add("Leather Boots", new DsAccessory("Leather Boots", 34, type, "leather_boots"));
            Foot.Add("Battle Boots", new DsAccessory("Battle Boots", 35, type, "battle_boots"));
            Foot.Add("Muspell Legs", new DsAccessory("Muspell Legs", 36, type, "muspell_legs"));
            Foot.Add("Hresvelgr Legs", new DsAccessory("Hresvelgr Legs", 37, type, "hresvelgr_legs"));
            Foot.Add("Nidhoggr Legs", new DsAccessory("Nidhoggr Legs", 38, type, "nidhoggr_legs"));
            Foot.Add("Garmr Legs", new DsAccessory("Garmr Legs", 39, type, "garmr_legs"));
            Foot.Add("Fire Starter", new DsAccessory("Fire Starter", 40, type, "fire_starter"));
            Foot.Add("Ice Breaker", new DsAccessory("Ice Breaker", 41, type, "ice_breaker"));
            Foot.Add("Sky Runner", new DsAccessory("Sky Runner", 42, type, "sky_runner"));
            Foot.Add("Grass Dancer", new DsAccessory("Grass Dancer", 43, type, "grass_dancer"));
            Foot.Add("Day Walker", new DsAccessory("Day Walker", 44, type, "day_walker"));
            Foot.Add("Night Stalker", new DsAccessory("Night Stalker", 45, type, "night_stalker"));
            Foot.Add("Training Boots", new DsAccessory("Training Boots", 46, type, "training_boots"));
            Foot.Add("Bounty Boots", new DsAccessory("Bounty Boots", 47, type, "bounty_boots"));
            Foot.Add("Cursed Boots", new DsAccessory("Cursed Boots", 48, type, "cursed_boots"));
            Foot.Add("Warrior's Boots", new DsAccessory("Warrior's Boots", 49, type, "warrior_s_boots"));
            Foot.Add("LeoRockLeg", new DsAccessory("LeoRockLeg", 50, type, "leorockleg"));
        }

        ///<summary>Init accessory instances for shields</summary>
        private void InitShieldList() {
            string type = "shield";
            Shield.Add("Silver Star", new DsAccessory("Silver Star", 51, type, "silver_star"));
            Shield.Add("Red Cross", new DsAccessory("Red Cross", 52, type, "red_cross"));
            Shield.Add("Snake Eyes", new DsAccessory("Snake Eyes", 53, type, "snake_eyes"));
            Shield.Add("Blue Spine", new DsAccessory("Blue Spine", 54, type, "blue_spine"));
            Shield.Add("Silver Star (duplicate)", new DsAccessory("Silver Star (duplicate)", 55, type, "silver_star"));
        }

        ///<summary>Init the list containing Equipments accessories related to equipments (mostly used for editors)</summary>
        private void InitEquipmentsList() {
            Equipments.Add("Empty slot", new DsAccessory("Empty slot", 65535, ""));
            Equipments = Equipments
                .Concat(Head)
                .Concat(Torso)
                .Concat(Foot)
                .ToDictionary(e => e.Key, e => e.Value);
        }

        ///<summary>Init the full list containing all accessories (mostly used for editors)</summary>
        private void InitFullList() {
            All = Equipments
                .Concat(Shield)
                .ToDictionary(e => e.Key, e => e.Value);
        }
    }
}

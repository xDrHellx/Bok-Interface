using System.Collections.Generic;
using System.Linq;

namespace BokInterface.Accessories {
    /// <summary>Class for Shinbok accessories instances and lists</summary>
    class ShinbokAccessories {

        public Dictionary<string, Accessory> Head = [],
            Torso = [],
            Arm = [],
            Foot = [],
            All = [];

        public ShinbokAccessories() {
            InitHeadList();
            InitTorsoList();
            InitArmList();
            InitFootList();
            InitFullList();
        }

        ///<summary>Init accessory instances for Head accessories</summary>
        private void InitHeadList() {
            string type = "head";
            Head.Add("Circlet", new ShinbokAccessory("Circlet", 0, type, "circlet", level: 10));
            Head.Add("Cool Bandana", new ShinbokAccessory("Cool Bandana", 1, type, "cool_bandana", level: 20));
            Head.Add("Burning Headband", new ShinbokAccessory("Burning Headband", 2, type, "burning_headband", level: 30));
            Head.Add("Earth Amulet", new ShinbokAccessory("Earth Amulet", 3, type, "earth_amulet", level: 40));
            Head.Add("Proof Of Shinobi", new ShinbokAccessory("Proof Of Shinobi", 4, type, "proof_of_shinobi", level: 50));
            Head.Add("Alfar Tiara", new ShinbokAccessory("Alfar Tiara", 5, type, "alfar_tiara", level: 50));
            Head.Add("X-Ray Glasses", new ShinbokAccessory("X-Ray Glasses", 6, type, "x_ray_glasses", level: 60));
            Head.Add("Golden Mask", new ShinbokAccessory("Golden Mask", 7, type, "golden_mask", level: 60));
            Head.Add("Faded Hat", new ShinbokAccessory("Faded Hat", 8, type, "faded_hat", set: "Solar Django"));
            Head.Add("Wolf Fang", new ShinbokAccessory("Wolf Fang", 9, type, "wolf_fang", set: "Black Django"));
            Head.Add("Colonel Helm", new ShinbokAccessory("Colonel Helm", 10, type, "colonel_helm", crossOver: true, set: "Rockman"));
        }

        ///<summary>Init accessory instances for Head accessories</summary>
        private void InitTorsoList() {
            string type = "torso";
            Torso.Add("Leather Armor", new ShinbokAccessory("Leather Armor", 11, type, "leather_armor", level: 10));
            Torso.Add("Chain Mail", new ShinbokAccessory("Chain Mail", 12, type, "chain_mail", level: 20));
            Torso.Add("Plate Mail", new ShinbokAccessory("Plate Mail", 13, type, "plate_mail", level: 30));
            Torso.Add("Fire Dragon Armor", new ShinbokAccessory("Fire Dragon Armor", 14, type, "fire_dragon_armor", level: 10));
            Torso.Add("Ice Dragon Armor", new ShinbokAccessory("Ice Dragon Armor", 15, type, "ice_dragon_armor", level: 30));
            Torso.Add("Wind Dragon Armor", new ShinbokAccessory("Wind Dragon Armor", 16, type, "wind_dragon_armor", level: 40));
            Torso.Add("Earth Dragon Armor", new ShinbokAccessory("Earth Dragon Armor", 17, type, "earth_dragon_armor", level: 20));
            Torso.Add("Raincoat", new ShinbokAccessory("Raincoat", 18, type, "raincoat", level: 50));
            Torso.Add("Rune Armor", new ShinbokAccessory("Rune Armor", 19, type, "rune_armor", level: 40));
            Torso.Add("Spike Mail", new ShinbokAccessory("Spike Mail", 20, type, "spike_mail", level: 50));
            Torso.Add("Novice Mail", new ShinbokAccessory("Novice Mail", 21, type, "novice_mail", level: 60));
            Torso.Add("White Armor", new ShinbokAccessory("White Armor", 22, type, "white_armor", level: 60));
            Torso.Add("Black Armor", new ShinbokAccessory("Black Armor", 23, type, "black_armor", level: 60));
            Torso.Add("Worn-out Coat", new ShinbokAccessory("Worn-out Coat", 24, type, "worn_out_coat", set: "Solar Django"));
            Torso.Add("Bat Wing", new ShinbokAccessory("Bat Wing", 25, type, "bat_wing", set: "Black Django"));
            Torso.Add("Forte Mantle", new ShinbokAccessory("Forte Mantle", 26, type, "forte_mantle", crossOver: true, set: "Rockman"));
        }

        ///<summary>Init accessory instances for Head accessories</summary>
        private void InitArmList() {
            string type = "arm";
            Arm.Add("Power Wrist", new ShinbokAccessory("Power Wrist", 27, type, "power_wrist", level: 10));
            Arm.Add("Bracelet", new ShinbokAccessory("Bracelet", 28, type, "bracelet", level: 20));
            Arm.Add("Crest of Clubs", new ShinbokAccessory("Crest of Clubs", 29, type, "crest_of_clubs", level: 30));
            Arm.Add("Crest of Diamonds", new ShinbokAccessory("Crest of Diamonds", 30, type, "crest_of_diamonds", level: 30));
            Arm.Add("Buckler", new ShinbokAccessory("Buckler", 31, type, "buckler", level: 40));
            Arm.Add("Rune Gauntlet", new ShinbokAccessory("Rune Gauntlet", 32, type, "rune_gauntlet", level: 50));
            Arm.Add("White Gauntlet", new ShinbokAccessory("White Gauntlet", 33, type, "white_gauntlet", level: 60));
            Arm.Add("Black Gauntlet", new ShinbokAccessory("Black Gauntlet", 34, type, "black_gauntlet", level: 60));
            Arm.Add("Sweaty Gloves", new ShinbokAccessory("Sweaty Gloves", 35, type, "sweaty_gloves", set: "Solar Django"));
            Arm.Add("Undead Fingernail", new ShinbokAccessory("Undead Fingernail", 36, type, "undead_fingernail", set: "Black Django"));
            Arm.Add("Tomahawk Armor", new ShinbokAccessory("Tomahawk Armor", 37, type, "tomahawk_armor", crossOver: true, set: "Rockman"));
        }

        ///<summary>Init accessory instances for Head accessories</summary>
        private void InitFootList() {
            string type = "foot";
            Foot.Add("Iron Clogs", new ShinbokAccessory("Iron Clogs", 38, type, "iron_clogs", level: 10));
            Foot.Add("Funny Shoes", new ShinbokAccessory("Funny Shoes", 39, type, "funny_shoes", level: 20));
            Foot.Add("Power Ankle", new ShinbokAccessory("Power Ankle", 40, type, "power_ankle", level: 30));
            Foot.Add("Traveler's Shoes", new ShinbokAccessory("Traveler's Shoes", 41, type, "traveler_s_shoes", level: 40));
            Foot.Add("Sabaton", new ShinbokAccessory("Sabaton", 42, type, "sabaton", level: 50));
            Foot.Add("Winged Boots", new ShinbokAccessory("Winged Boots", 43, type, "winged_boots", level: 60));
            Foot.Add("Adventurer's Boots", new ShinbokAccessory("Adventurer's Boots", 44, type, "adventurer_s_boots", level: 60));
            Foot.Add("Worn-Out Boots", new ShinbokAccessory("Worn-Out Boots", 45, type, "worn_out_boots", set: "Solar Django"));
            Foot.Add("Rat Tail", new ShinbokAccessory("Rat Tail", 46, type, "rat_tail", set: "Black Django"));
            Foot.Add("Blues Leg", new ShinbokAccessory("Blues Leg", 47, type, "blues_leg", crossOver: true, set: "Rockman"));
        }

        ///<summary>Init the full list containing all accessories (mostly used for editors)</summary>
        private void InitFullList() {
            All.Add("Empty slot", new ShinbokAccessory("Empty slot", 0, ""));
            All = All
                .Concat(Head)
                .Concat(Torso)
                .Concat(Arm)
                .Concat(Foot)
                .ToDictionary(e => e.Key, e => e.Value);
        }
    }
}
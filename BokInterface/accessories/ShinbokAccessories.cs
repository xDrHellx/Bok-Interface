using System.Collections.Generic;
using System.Linq;

using BokInterface.Weapons.Accessories;

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
            Head.Add("Circlet", new ShinbokAccessory("Circlet", 0, type, "circlet"));
            Head.Add("Cool Bandana", new ShinbokAccessory("Cool Bandana", 1, type, "cool_bandana"));
            Head.Add("Burning Headband", new ShinbokAccessory("Burning Headband", 2, type, "burning_headband"));
            Head.Add("Earth Amulet", new ShinbokAccessory("Earth Amulet", 3, type, "earth_amulet"));
            Head.Add("Proof Of Shinobi", new ShinbokAccessory("Proof Of Shinobi", 4, type, "proof_of_shinobi"));
            Head.Add("Alfar Tiara", new ShinbokAccessory("Alfar Tiara", 5, type, "alfar_tiara"));
            Head.Add("X-Ray Glasses", new ShinbokAccessory("X-Ray Glasses", 6, type, "x_ray_glasses"));
            Head.Add("Golden Mask", new ShinbokAccessory("Golden Mask", 7, type, "golden_mask"));
            Head.Add("Faded Hat", new ShinbokAccessory("Faded Hat", 8, type, "faded_hat"));
            Head.Add("Wolf Fang", new ShinbokAccessory("Wolf Fang", 9, type, "wolf_fang"));
            Head.Add("Colonel Helm", new ShinbokAccessory("Colonel Helm", 10, type, "colonel_helm", crossOver: true));
        }

        ///<summary>Init accessory instances for Head accessories</summary>
        private void InitTorsoList() {
            string type = "torso";
            Torso.Add("Leather Armor", new ShinbokAccessory("Leather Armor", 11, type, "leather_armor"));
            Torso.Add("Chain Mail", new ShinbokAccessory("Chain Mail", 12, type, "chain_mail"));
            Torso.Add("Plate Mail", new ShinbokAccessory("Plate Mail", 13, type, "plate_mail"));
            Torso.Add("Fire Dragon Armor", new ShinbokAccessory("Fire Dragon Armor", 14, type, "fire_dragon_armor"));
            Torso.Add("Ice Dragon Armor", new ShinbokAccessory("Ice Dragon Armor", 15, type, "ice_dragon_armor"));
            Torso.Add("Wind Dragon Armor", new ShinbokAccessory("Wind Dragon Armor", 16, type, "wind_dragon_armor"));
            Torso.Add("Earth Dragon Armor", new ShinbokAccessory("Earth Dragon Armor", 17, type, "earth_dragon_armor"));
            Torso.Add("Raincoat", new ShinbokAccessory("Raincoat", 18, type, "raincoat"));
            Torso.Add("Rune Armor", new ShinbokAccessory("Rune Armor", 19, type, "rune_armor"));
            Torso.Add("Spike Mail", new ShinbokAccessory("Spike Mail", 20, type, "spike_mail"));
            Torso.Add("Novice Mail", new ShinbokAccessory("Novice Mail", 21, type, "novice_mail"));
            Torso.Add("White Armor", new ShinbokAccessory("White Armor", 22, type, "white_armor"));
            Torso.Add("Black Armor", new ShinbokAccessory("Black Armor", 23, type, "black_armor"));
            Torso.Add("Worn-out Coat", new ShinbokAccessory("Worn-out Coat", 24, type, "worn_out_coat"));
            Torso.Add("Bat Wing", new ShinbokAccessory("Bat Wing", 25, type, "bat_wing"));
            Torso.Add("Forte Mantle", new ShinbokAccessory("Forte Mantle", 26, type, "forte_mantle", crossOver: true));
        }

        ///<summary>Init accessory instances for Head accessories</summary>
        private void InitArmList() {
            string type = "arm";
            Arm.Add("Power Wrist", new ShinbokAccessory("Power Wrist", 27, type, "power_wrist"));
            Arm.Add("Bracelet", new ShinbokAccessory("Bracelet", 28, type, "bracelet"));
            Arm.Add("Crest of Clubs", new ShinbokAccessory("Crest of Clubs", 29, type, "crest_of_clubs"));
            Arm.Add("Crest of Diamonds", new ShinbokAccessory("Crest of Diamonds", 30, type, "crest_of_diamonds"));
            Arm.Add("Buckler", new ShinbokAccessory("Buckler", 31, type, "buckler"));
            Arm.Add("Rune Gauntlet", new ShinbokAccessory("Rune Gauntlet", 32, type, "rune_gauntlet"));
            Arm.Add("White Gauntlet", new ShinbokAccessory("White Gauntlet", 33, type, "white_gauntlet"));
            Arm.Add("Black Gauntlet", new ShinbokAccessory("Black Gauntlet", 34, type, "black_gauntlet"));
            Arm.Add("Sweaty Gloves", new ShinbokAccessory("Sweaty Gloves", 35, type, "sweaty_gloves"));
            Arm.Add("Undead Fingernail", new ShinbokAccessory("Undead Fingernail", 36, type, "undead_fingernail"));
            Arm.Add("Tomahawk Armor", new ShinbokAccessory("Tomahawk Armor", 37, type, "tomahawk_armor", crossOver: true));
        }

        ///<summary>Init accessory instances for Head accessories</summary>
        private void InitFootList() {
            string type = "foot";
            Foot.Add("Iron Clogs", new ShinbokAccessory("Iron Clogs", 38, type, "iron_clogs"));
            Foot.Add("Funny Shoes", new ShinbokAccessory("Funny Shoes", 39, type, "funny_shoes"));
            Foot.Add("Power Ankle", new ShinbokAccessory("Power Ankle", 40, type, "power_ankle"));
            Foot.Add("Traveler's Shoes", new ShinbokAccessory("Traveler's Shoes", 41, type, "traveler_s_shoes"));
            Foot.Add("Sabaton", new ShinbokAccessory("Sabaton", 42, type, "sabaton"));
            Foot.Add("Winged Boots", new ShinbokAccessory("Winged Boots", 43, type, "winged_boots"));
            Foot.Add("Adventurer's Boots", new ShinbokAccessory("Adventurer's Boots", 44, type, "adventurer_s_boots"));
            Foot.Add("Worn-Out Boots", new ShinbokAccessory("Worn-Out Boots", 45, type, "worn_out_boots"));
            Foot.Add("Rat Tail", new ShinbokAccessory("Rat Tail", 46, type, "rat_tail"));
            Foot.Add("Blues Leg", new ShinbokAccessory("Blues Leg", 47, type, "blues_leg", crossOver: true));
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
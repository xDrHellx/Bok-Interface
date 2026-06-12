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
            Head.Add("Circlet", new ShinbokAccessory("Circlet", 0, type, "circlet", "+4 SPR | Halves item drop rate", level: 10));
            Head.Add("Cool Bandana", new ShinbokAccessory("Cool Bandana", 1, type, "cool_bandana", "+8 SPR | -50% TRC rate", level: 20));
            Head.Add("Burning Headband", new ShinbokAccessory("Burning Headband", 2, type, "burning_headband", "x2 TRC rate", level: 30));
            Head.Add("Earth Amulet", new ShinbokAccessory("Earth Amulet", 3, type, "earth_amulet", "Immune to poison", level: 40));
            Head.Add("Proof Of Shinobi", new ShinbokAccessory("Proof Of Shinobi", 4, type, "proof_of_shinobi", "Silent footsteps | -50% TRC rate", level: 50));
            Head.Add("Alfar Tiara", new ShinbokAccessory("Alfar Tiara", 5, type, "alfar_tiara", "Invisibility | -50% EXP", level: 50));
            Head.Add("X-Ray Glasses", new ShinbokAccessory("X-Ray Glasses", 6, type, "x_ray_glasses", "See invisible chests, enemies & traps", level: 60));
            Head.Add("Golden Mask", new ShinbokAccessory("Golden Mask", 7, type, "golden_mask", "Doubles item drop rate", level: 60));
            Head.Add("Faded Hat", new ShinbokAccessory("Faded Hat", 8, type, "faded_hat", "Doubles Solar Gauge value (limited to 10 blocks)", set: "Solar Django"));
            Head.Add("Wolf Fang", new ShinbokAccessory("Wolf Fang", 9, type, "wolf_fang", "-50% EXP (2x EXP when wearing full set)", set: "Black Django"));
            Head.Add("Colonel Helm", new ShinbokAccessory("Colonel Helm", 10, type, "colonel_helm", "Immune to poison, paralysis & confusion", crossOver: true, set: "Rockman"));
        }

        ///<summary>Init accessory instances for Head accessories</summary>
        private void InitTorsoList() {
            string type = "torso";
            Torso.Add("Leather Armor", new ShinbokAccessory("Leather Armor", 11, type, "leather_armor", "+2 VIT", level: 10));
            Torso.Add("Chain Mail", new ShinbokAccessory("Chain Mail", 12, type, "chain_mail", "+4 VIT", level: 20));
            Torso.Add("Plate Mail", new ShinbokAccessory("Plate Mail", 13, type, "plate_mail", "+8 VIT", level: 30));
            Torso.Add("Fire Dragon Armor", new ShinbokAccessory("Fire Dragon Armor", 14, type, "fire_dragon_armor", "Increase Flame resistance | Lowers Frost resistance", level: 10));
            Torso.Add("Ice Dragon Armor", new ShinbokAccessory("Ice Dragon Armor", 15, type, "ice_dragon_armor", "Increase Frost resistance | Lowers Flame resistance", level: 30));
            Torso.Add("Wind Dragon Armor", new ShinbokAccessory("Wind Dragon Armor", 16, type, "wind_dragon_armor", "Increase Cloud resistance | Lowers Earth resistance", level: 40));
            Torso.Add("Earth Dragon Armor", new ShinbokAccessory("Earth Dragon Armor", 17, type, "earth_dragon_armor", "Increase Earth resistance | Lowers Cloud resistance", level: 20));
            Torso.Add("Raincoat", new ShinbokAccessory("Raincoat", 18, type, "raincoat", "Immune to Kaamos & prevent damage from rain", level: 50));
            Torso.Add("Rune Armor", new ShinbokAccessory("Rune Armor", 19, type, "rune_armor", "-1 STR | Increase defense based on SPR", level: 40));
            Torso.Add("Spike Mail", new ShinbokAccessory("Spike Mail", 20, type, "spike_mail", "Enemies takes damage when hitting Django", level: 50));
            Torso.Add("Novice Mail", new ShinbokAccessory("Novice Mail", 21, type, "novice_mail", "-10% damage taken", level: 60));
            Torso.Add("White Armor", new ShinbokAccessory("White Armor", 22, type, "white_armor", "-9% damage taken during daytime | +9% damage taken at night", level: 60));
            Torso.Add("Black Armor", new ShinbokAccessory("Black Armor", 23, type, "black_armor", "-9% damage taken at night | +9% damage taken during daytime", level: 60));
            Torso.Add("Worn-out Coat", new ShinbokAccessory("Worn-out Coat", 24, type, "worn_out_coat", "Increase defense based on Solar Gauge", set: "Solar Django"));
            Torso.Add("Bat Wing", new ShinbokAccessory("Bat Wing", 25, type, "bat_wing", "Lowers Flame, Frost, Cloud & Earth resistance (inverted when wearing full set)", set: "Black Django"));
            Torso.Add("Forte Mantle", new ShinbokAccessory("Forte Mantle", 26, type, "forte_mantle", "-50% damage taken", crossOver: true, set: "Rockman"));
        }

        ///<summary>Init accessory instances for Head accessories</summary>
        private void InitArmList() {
            string type = "arm";
            Arm.Add("Power Wrist", new ShinbokAccessory("Power Wrist", 27, type, "power_wrist", "+4 STR | Swords breaks twice as fast", level: 10));
            Arm.Add("Bracelet", new ShinbokAccessory("Bracelet", 28, type, "bracelet", "+8 STR | ENE consumption is doubled", level: 20));
            Arm.Add("Crest of Clubs", new ShinbokAccessory("Crest of Clubs", 29, type, "crest_of_clubs", "+20% damage dealt with Vertical hits", level: 30));
            Arm.Add("Crest of Diamonds", new ShinbokAccessory("Crest of Diamonds", 30, type, "crest_of_diamonds", "+20% damage dealt with Thrusting hits", level: 30));
            Arm.Add("Buckler", new ShinbokAccessory("Buckler", 31, type, "buckler", "Swords breaks half as fast", level: 40));
            Arm.Add("Rune Gauntlet", new ShinbokAccessory("Rune Gauntlet", 32, type, "rune_gauntlet", "-1 STR | Damage dealt increases based on SPR", level: 50));
            Arm.Add("White Gauntlet", new ShinbokAccessory("White Gauntlet", 33, type, "white_gauntlet", "+20% damage dealt with swords during daytime | -20% damage dealt with swords at night", level: 60));
            Arm.Add("Black Gauntlet", new ShinbokAccessory("Black Gauntlet", 34, type, "black_gauntlet", "+20% damage dealt with swords at night | -20% damage dealt with swords during daytime", level: 60));
            Arm.Add("Sweaty Gloves", new ShinbokAccessory("Sweaty Gloves", 35, type, "sweaty_gloves", "Sword damage increases based on Solar Gauge", set: "Solar Django"));
            Arm.Add("Undead Fingernail", new ShinbokAccessory("Undead Fingernail", 36, type, "undead_fingernail", "ENE consumption is doubled (inverted when wearing full set)", set: "Black Django"));
            Arm.Add("Tomahawk Armor", new ShinbokAccessory("Tomahawk Armor", 37, type, "tomahawk_armor", "+20% damage dealt with swords", crossOver: true, set: "Rockman"));
        }

        ///<summary>Init accessory instances for Head accessories</summary>
        private void InitFootList() {
            string type = "foot";
            Foot.Add("Iron Clogs", new ShinbokAccessory("Iron Clogs", 38, type, "iron_clogs", "Slow walking speed | Unaffected by Solar Wind", level: 10));
            Foot.Add("Funny Shoes", new ShinbokAccessory("Funny Shoes", 39, type, "funny_shoes", "+2 VIT | +2 STR | Footsteps makes noise", level: 20));
            Foot.Add("Power Ankle", new ShinbokAccessory("Power Ankle", 40, type, "power_ankle", "+4 VIT | +4 STR | Items spoils twice as fast", level: 30));
            Foot.Add("Traveler's Shoes", new ShinbokAccessory("Traveler's Shoes", 41, type, "traveler_s_shoes", "Solar Bugs becomes twice as efficient", level: 40));
            Foot.Add("Sabaton", new ShinbokAccessory("Sabaton", 42, type, "sabaton", "Immune to lava", level: 50));
            Foot.Add("Winged Boots", new ShinbokAccessory("Winged Boots", 43, type, "winged_boots", "Consumes ENE while walking | Move as fast as if using a Speed Nut until ENE reaches 0", level: 60));
            Foot.Add("Adventurer's Boots", new ShinbokAccessory("Adventurer's Boots", 44, type, "adventurer_s_boots", "Solar Nuts becomes twice as efficient", level: 60));
            Foot.Add("Worn-Out Boots", new ShinbokAccessory("Worn-Out Boots", 45, type, "worn_out_boots", "Restores Life while walking", set: "Solar Django"));
            Foot.Add("Rat Tail", new ShinbokAccessory("Rat Tail", 46, type, "rat_tail", "Consumes ENE while walking (restores ENE when wearing full set)", set: "Black Django"));
            Foot.Add("Blues Leg", new ShinbokAccessory("Blues Leg", 47, type, "blues_leg", "Can use Dash without sunlight | Dash speed is always equivalent to 6 Solar Gauge blocks", crossOver: true, set: "Rockman"));
        }

        ///<summary>Init the full list containing all accessories (mostly used for editors)</summary>
        private void InitFullList() {
            All.Add("Empty slot", new ShinbokAccessory("Empty slot", 65535, ""));
            All = All
                .Concat(Head)
                .Concat(Torso)
                .Concat(Arm)
                .Concat(Foot)
                .ToDictionary(e => e.Key, e => e.Value);
        }
    }
}

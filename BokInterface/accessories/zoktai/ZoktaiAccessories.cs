using System.Collections.Generic;

namespace BokInterface.Accessories {
    /// <summary>Class for Zoktai accessories (protectors) instances and lists</summary>
    class ZoktaiAccessories {

        public Dictionary<string, Accessory> All = [];

        public ZoktaiAccessories() {
            InitProtectorsList();
        }

        ///<summary>Init accessory instances for protectors</summary>
        private void InitProtectorsList() {
            All.Add("Empty slot", new ZoktaiAccessory("Empty slot", 65535));
            All.Add("Cloth Armor", new ZoktaiAccessory("Cloth Armor", 0, "cloth_armor", defense: 10, weight: 5));
            All.Add("Leather Armor", new ZoktaiAccessory("Leather Armor", 1, "leather_armor", defense: 15, weight: 10));
            All.Add("Chain Mail", new ZoktaiAccessory("Chain Mail", 2, "chain_mail", defense: 20, weight: 15));
            All.Add("Silver Chain", new ZoktaiAccessory("Silver Chain", 3, "silver_chain", "+1 stats as Red Django | -1 stats as Black Django", defense: 25, weight: 20));
            All.Add("Scale Mail", new ZoktaiAccessory("Scale Mail", 4, "scale_mail", defense: 30, weight: 25));
            All.Add("Samurai Armor", new ZoktaiAccessory("Samurai Armor", 5, "samurai_armor", "+2 STR", defense: 30, weight: 25));
            All.Add("Blade Mail", new ZoktaiAccessory("Blade Mail", 6, "blade_mail", defense: 40, weight: 35));
            All.Add("Brigandine", new ZoktaiAccessory("Brigandine", 7, "brigandine", "Unaffected by Solar Wind", defense: 45, weight: 40));
            All.Add("Mail of Sol", new ZoktaiAccessory("Mail of Sol", 8, "mail_of_sol", "Increases Sol resistance | Solls from the Solar Gauge accumulates twice as fast", defense: 25, weight: 22));
            All.Add("Mail of Darkness", new ZoktaiAccessory("Mail of Darkness", 9, "mail_of_darkness", "Increases Dark resistance", defense: 30, weight: 22));
            All.Add("Mail of Luna", new ZoktaiAccessory("Mail of Luna", 10, "mail_of_luna", "Increases resistance to all elements", defense: 25, weight: 22)); // Sabata's protector
            All.Add("Fire Dragon Fang", new ZoktaiAccessory("Fire Dragon Fang", 11, "fire_dragon_fang", "Increases Flame resistance", defense: 30, weight: 33));
            All.Add("Water Dragon Tail", new ZoktaiAccessory("Water Dragon Tail", 12, "water_dragon_tail", "Increases Frost resistance", defense: 30, weight: 33));
            All.Add("Wind Dragon Wing", new ZoktaiAccessory("Wind Dragon Wing", 13, "wind_dragon_wing", "Increases Cloud resistance", defense: 30, weight: 33));
            All.Add("Earth Dragon Claw", new ZoktaiAccessory("Earth Dragon Claw", 14, "earth_dragon_claw", "Increases Earth resistance", defense: 30, weight: 33));
            All.Add("Dragon Scale", new ZoktaiAccessory("Dragon Scale", 15, "dragon_scale", "Increases Flame, Frost, Cloud & Earth resistance", defense: 28, weight: 24));
            All.Add("Fairy Robe", new ZoktaiAccessory("Fairy Robe", 16, "fairy_robe", "Solar Bugs becomes twice as efficient", defense: 16, weight: 4));
            All.Add("Earthly Robe", new ZoktaiAccessory("Earthly Robe", 17, "earthly_robe", "Solar Nuts becomes twice as efficient", defense: 28, weight: 8));
            All.Add("Raincoat", new ZoktaiAccessory("Raincoat", 18, "raincoat", "Prevent damage from rain", defense: 12, weight: 6));
            All.Add("Garb of Light", new ZoktaiAccessory("Garb of Light", 19, "garb_of_light", "Doubles Solar Gauge value (limited to 10 blocks)", defense: 30, weight: 12));
            All.Add("Garb of Darkness", new ZoktaiAccessory("Garb of Darkness", 20, "garb_of_darkness", "Permanent Nighttime effect (1.5x damage dealt with Enchant Dark)", defense: 30, weight: 12));
            All.Add("Magic Robe", new ZoktaiAccessory("Magic Robe", 21, "magic_robe", "Magics costs 20% less ENE", defense: 26, weight: 16));
            All.Add("Blood-soaked Cape", new ZoktaiAccessory("Blood-soaked Cape", 22, "blood_soaked_cape", "-2 stats as Red Django | +2 stats as Black Django", defense: 22, weight: 12));
            All.Add("Skull Suit", new ZoktaiAccessory("Skull Suit", 23, "skull_suit", "Enemy sight range reduced by half", defense: 20, weight: 16));
            All.Add("Training Gear", new ZoktaiAccessory("Training Gear", 24, "training_gear", "1.5x EXP", defense: 25, weight: 16));
            All.Add("Thief's Clothes", new ZoktaiAccessory("Thief's Clothes", 25, "thiefs_clothes", "Common item drop rate +20%", defense: 18, weight: 24));
            All.Add("Hunter's Clothes", new ZoktaiAccessory("Hunter's Clothes", 26, "hunters_clothes", "Rare item drop rate +20%", defense: 24, weight: 24));
            All.Add("Poison Guard", new ZoktaiAccessory("Poison Guard", 27, "poison_guard", "Immune to poison", defense: 18, weight: 28));
            All.Add("Weapon Guard", new ZoktaiAccessory("Weapon Guard", 28, "weapon_guard", "Weapons cannot be damaged (outside of Solar Forging)", defense: 24, weight: 28));
            All.Add("Parade Armor", new ZoktaiAccessory("Parade Armor", 29, "parade_armor", "Crimson Monster spawn chance +20%", defense: 32, weight: 28));
            All.Add("Ninja Gi", new ZoktaiAccessory("Ninja Gi", 30, "ninja_gi", "+10 AGI", defense: 18, weight: 28));
            All.Add("Spike Mail", new ZoktaiAccessory("Spike Mail", 31, "spike_mail", "Enemies takes damage when hitting Django", defense: 40, weight: 40));
            All.Add("Pitch Black Armor", new ZoktaiAccessory("Pitch Black Armor", 32, "pitch_black_armor", "Constantly drains Life | Increases damage based on Life", defense: 50, weight: 50));
            All.Add("Mega Power", new ZoktaiAccessory("Mega Power", 33, "mega_power", "MegaBuster can be charged", defense: 25, weight: 30, crossOver: true));
            All.Add("Guts Power", new ZoktaiAccessory("Guts Power", 34, "guts_power", "-50% damage taken", defense: 30, weight: 40, crossOver: true));
            All.Add("Proto Power", new ZoktaiAccessory("Proto Power", 35, "proto_power", "1.5x attack speed as Red Django", defense: 25, weight: 20, crossOver: true));
            All.Add("Toad Power", new ZoktaiAccessory("Toad Power", 36, "toad_power", "Recovers Life under rain", defense: 20, weight: 20, crossOver: true));
        }
    }
}

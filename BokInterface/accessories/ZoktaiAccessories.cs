using System.Collections.Generic;

using BokInterface.Weapons.Accessories;

namespace BokInterface.Accessories {
    /// <summary>Class for Zoktai accessories (protectors) instances and lists</summary>
    class ZoktaiAccessories {

        public Dictionary<string, Accessory> All = [];

        public ZoktaiAccessories() {
            InitProtectorsList();
        }

        ///<summary>Init accessory instances for protectors</summary>
        private void InitProtectorsList() {
            All.Add("Empty slot", new Accessory("Empty slot", 65535, ""));
            All.Add("Cloth Armor", new Accessory("Cloth Armor", 0, "body", "cloth_armor"));
            All.Add("Leather Armor", new Accessory("Leather Armor", 1, "body", "leather_armor"));
            All.Add("Chain Mail", new Accessory("Chain Mail", 2, "body", "chain_mail"));
            All.Add("Silver Chain", new Accessory("Silver Chain", 3, "body", "silver_chain"));
            All.Add("Scale Mail", new Accessory("Scale Mail", 4, "body", "scale_mail"));
            All.Add("Samurai Armor", new Accessory("Samurai Armor", 5, "body", "samurai_armor"));
            All.Add("Blade Mail", new Accessory("Blade Mail", 6, "body", "blade_mail"));
            All.Add("Brigandine", new Accessory("Brigandine", 7, "body", "brigandine"));
            All.Add("Mail of Sol", new Accessory("Mail of Sol", 8, "body", "mail_of_sol"));
            All.Add("Mail of Darkness", new Accessory("Mail of Darkness", 9, "body", "mail_of_darkness"));
            All.Add("Mail of Luna", new Accessory("Mail of Luna", 10, "body", "mail_of_luna"));
            All.Add("Fire Dragon Fang", new Accessory("Fire Dragon Fang", 11, "body", "fire_dragon_fang"));
            All.Add("Water Dragon Tail", new Accessory("Water Dragon Tail", 12, "body", "water_dragon_tail"));
            All.Add("Wind Dragon Wing", new Accessory("Wind Dragon Wing", 13, "body", "wind_dragon_wing"));
            All.Add("Earth Dragon Claw", new Accessory("Earth Dragon Claw", 14, "body", "earth_dragon_claw"));
            All.Add("Dragon Scale", new Accessory("Dragon Scale", 15, "body", "dragon_scale"));
            All.Add("Fairy Robe", new Accessory("Fairy Robe", 16, "body", "fairy_robe"));
            All.Add("Earthly Robe", new Accessory("Earthly Robe", 17, "body", "earthly_robe"));
            All.Add("Raincoat", new Accessory("Raincoat", 18, "body", "raincoat"));
            All.Add("Garb of Light", new Accessory("Garb of Light", 19, "body", "garb_of_light"));
            All.Add("Garb of Darkness", new Accessory("Garb of Darkness", 20, "body", "garb_of_darkness"));
            All.Add("Magic Robe", new Accessory("Magic Robe", 21, "body", "magic_robe"));
            All.Add("Blood-soaked Cape", new Accessory("Blood-soaked Cape", 22, "body", "blood_soaked_cape"));
            All.Add("Skull Suit", new Accessory("Skull Suit", 23, "body", "skull_suit"));
            All.Add("Training Gear", new Accessory("Training Gear", 24, "body", "training_gear"));
            All.Add("Thief's Clothes", new Accessory("Thief's Clothes", 25, "body", "thiefs_clothes"));
            All.Add("Hunter's Clothes", new Accessory("Hunter's Clothes", 26, "body", "hunters_clothes"));
            All.Add("Poison Guard", new Accessory("Poison Guard", 27, "body", "poison_guard"));
            All.Add("Weapon Guard", new Accessory("Weapon Guard", 28, "body", "weapon_guard"));
            All.Add("Parade Armor", new Accessory("Parade Armor", 29, "body", "parade_armor"));
            All.Add("Ninja Gi", new Accessory("Ninja Gi", 30, "body", "ninja_gi"));
            All.Add("Spike Mail", new Accessory("Spike Mail", 31, "body", "spike_mail"));
            All.Add("Pitch Black Armor", new Accessory("Pitch Black Armor", 32, "body", "pitch_black_armor"));
            All.Add("Mega Power", new Accessory("Mega Power", 33, "body", "mega_power", crossOver: true));
            All.Add("Guts Power", new Accessory("Guts Power", 34, "body", "guts_power", crossOver: true));
            All.Add("Proto Power", new Accessory("Proto Power", 35, "body", "proto_power", crossOver: true));
            All.Add("Toad Power", new Accessory("Toad Power", 36, "body", "toad_power", crossOver: true));
        }
    }
}
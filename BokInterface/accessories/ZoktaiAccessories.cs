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
            All.Add("Empty slot", new ZoktaiAccessory("Empty slot", 65535, ""));
            All.Add("Cloth Armor", new ZoktaiAccessory("Cloth Armor", 0, "body", "cloth_armor"));
            All.Add("Leather Armor", new ZoktaiAccessory("Leather Armor", 1, "body", "leather_armor"));
            All.Add("Chain Mail", new ZoktaiAccessory("Chain Mail", 2, "body", "chain_mail"));
            All.Add("Silver Chain", new ZoktaiAccessory("Silver Chain", 3, "body", "silver_chain"));
            All.Add("Scale Mail", new ZoktaiAccessory("Scale Mail", 4, "body", "scale_mail"));
            All.Add("Samurai Armor", new ZoktaiAccessory("Samurai Armor", 5, "body", "samurai_armor"));
            All.Add("Blade Mail", new ZoktaiAccessory("Blade Mail", 6, "body", "blade_mail"));
            All.Add("Brigandine", new ZoktaiAccessory("Brigandine", 7, "body", "brigandine"));
            All.Add("Mail of Sol", new ZoktaiAccessory("Mail of Sol", 8, "body", "mail_of_sol"));
            All.Add("Mail of Darkness", new ZoktaiAccessory("Mail of Darkness", 9, "body", "mail_of_darkness"));
            All.Add("Mail of Luna", new ZoktaiAccessory("Mail of Luna", 10, "body", "mail_of_luna"));
            All.Add("Fire Dragon Fang", new ZoktaiAccessory("Fire Dragon Fang", 11, "body", "fire_dragon_fang"));
            All.Add("Water Dragon Tail", new ZoktaiAccessory("Water Dragon Tail", 12, "body", "water_dragon_tail"));
            All.Add("Wind Dragon Wing", new ZoktaiAccessory("Wind Dragon Wing", 13, "body", "wind_dragon_wing"));
            All.Add("Earth Dragon Claw", new ZoktaiAccessory("Earth Dragon Claw", 14, "body", "earth_dragon_claw"));
            All.Add("Dragon Scale", new ZoktaiAccessory("Dragon Scale", 15, "body", "dragon_scale"));
            All.Add("Fairy Robe", new ZoktaiAccessory("Fairy Robe", 16, "body", "fairy_robe"));
            All.Add("Earthly Robe", new ZoktaiAccessory("Earthly Robe", 17, "body", "earthly_robe"));
            All.Add("Raincoat", new ZoktaiAccessory("Raincoat", 18, "body", "raincoat"));
            All.Add("Garb of Light", new ZoktaiAccessory("Garb of Light", 19, "body", "garb_of_light"));
            All.Add("Garb of Darkness", new ZoktaiAccessory("Garb of Darkness", 20, "body", "garb_of_darkness"));
            All.Add("Magic Robe", new ZoktaiAccessory("Magic Robe", 21, "body", "magic_robe"));
            All.Add("Blood-soaked Cape", new ZoktaiAccessory("Blood-soaked Cape", 22, "body", "blood_soaked_cape"));
            All.Add("Skull Suit", new ZoktaiAccessory("Skull Suit", 23, "body", "skull_suit"));
            All.Add("Training Gear", new ZoktaiAccessory("Training Gear", 24, "body", "training_gear"));
            All.Add("Thief's Clothes", new ZoktaiAccessory("Thief's Clothes", 25, "body", "thiefs_clothes"));
            All.Add("Hunter's Clothes", new ZoktaiAccessory("Hunter's Clothes", 26, "body", "hunters_clothes"));
            All.Add("Poison Guard", new ZoktaiAccessory("Poison Guard", 27, "body", "poison_guard"));
            All.Add("Weapon Guard", new ZoktaiAccessory("Weapon Guard", 28, "body", "weapon_guard"));
            All.Add("Parade Armor", new ZoktaiAccessory("Parade Armor", 29, "body", "parade_armor"));
            All.Add("Ninja Gi", new ZoktaiAccessory("Ninja Gi", 30, "body", "ninja_gi"));
            All.Add("Spike Mail", new ZoktaiAccessory("Spike Mail", 31, "body", "spike_mail"));
            All.Add("Pitch Black Armor", new ZoktaiAccessory("Pitch Black Armor", 32, "body", "pitch_black_armor"));
            All.Add("Mega Power", new ZoktaiAccessory("Mega Power", 33, "body", "mega_power", crossOver: true));
            All.Add("Guts Power", new ZoktaiAccessory("Guts Power", 34, "body", "guts_power", crossOver: true));
            All.Add("Proto Power", new ZoktaiAccessory("Proto Power", 35, "body", "proto_power", crossOver: true));
            All.Add("Toad Power", new ZoktaiAccessory("Toad Power", 36, "body", "toad_power", crossOver: true));
        }
    }
}
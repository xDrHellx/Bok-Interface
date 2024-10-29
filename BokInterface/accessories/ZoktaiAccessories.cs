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
            string type = "body";
            All.Add("Empty slot", new ZoktaiAccessory("Empty slot", 65535, ""));
            All.Add("Cloth Armor", new ZoktaiAccessory("Cloth Armor", 0, type, "cloth_armor"));
            All.Add("Leather Armor", new ZoktaiAccessory("Leather Armor", 1, type, "leather_armor"));
            All.Add("Chain Mail", new ZoktaiAccessory("Chain Mail", 2, type, "chain_mail"));
            All.Add("Silver Chain", new ZoktaiAccessory("Silver Chain", 3, type, "silver_chain"));
            All.Add("Scale Mail", new ZoktaiAccessory("Scale Mail", 4, type, "scale_mail"));
            All.Add("Samurai Armor", new ZoktaiAccessory("Samurai Armor", 5, type, "samurai_armor"));
            All.Add("Blade Mail", new ZoktaiAccessory("Blade Mail", 6, type, "blade_mail"));
            All.Add("Brigandine", new ZoktaiAccessory("Brigandine", 7, type, "brigandine"));
            All.Add("Mail of Sol", new ZoktaiAccessory("Mail of Sol", 8, type, "mail_of_sol"));
            All.Add("Mail of Darkness", new ZoktaiAccessory("Mail of Darkness", 9, type, "mail_of_darkness"));
            All.Add("Mail of Luna", new ZoktaiAccessory("Mail of Luna", 10, type, "mail_of_luna"));
            All.Add("Fire Dragon Fang", new ZoktaiAccessory("Fire Dragon Fang", 11, type, "fire_dragon_fang"));
            All.Add("Water Dragon Tail", new ZoktaiAccessory("Water Dragon Tail", 12, type, "water_dragon_tail"));
            All.Add("Wind Dragon Wing", new ZoktaiAccessory("Wind Dragon Wing", 13, type, "wind_dragon_wing"));
            All.Add("Earth Dragon Claw", new ZoktaiAccessory("Earth Dragon Claw", 14, type, "earth_dragon_claw"));
            All.Add("Dragon Scale", new ZoktaiAccessory("Dragon Scale", 15, type, "dragon_scale"));
            All.Add("Fairy Robe", new ZoktaiAccessory("Fairy Robe", 16, type, "fairy_robe"));
            All.Add("Earthly Robe", new ZoktaiAccessory("Earthly Robe", 17, type, "earthly_robe"));
            All.Add("Raincoat", new ZoktaiAccessory("Raincoat", 18, type, "raincoat"));
            All.Add("Garb of Light", new ZoktaiAccessory("Garb of Light", 19, type, "garb_of_light"));
            All.Add("Garb of Darkness", new ZoktaiAccessory("Garb of Darkness", 20, type, "garb_of_darkness"));
            All.Add("Magic Robe", new ZoktaiAccessory("Magic Robe", 21, type, "magic_robe"));
            All.Add("Blood-soaked Cape", new ZoktaiAccessory("Blood-soaked Cape", 22, type, "blood_soaked_cape"));
            All.Add("Skull Suit", new ZoktaiAccessory("Skull Suit", 23, type, "skull_suit"));
            All.Add("Training Gear", new ZoktaiAccessory("Training Gear", 24, type, "training_gear"));
            All.Add("Thief's Clothes", new ZoktaiAccessory("Thief's Clothes", 25, type, "thiefs_clothes"));
            All.Add("Hunter's Clothes", new ZoktaiAccessory("Hunter's Clothes", 26, type, "hunters_clothes"));
            All.Add("Poison Guard", new ZoktaiAccessory("Poison Guard", 27, type, "poison_guard"));
            All.Add("Weapon Guard", new ZoktaiAccessory("Weapon Guard", 28, type, "weapon_guard"));
            All.Add("Parade Armor", new ZoktaiAccessory("Parade Armor", 29, type, "parade_armor"));
            All.Add("Ninja Gi", new ZoktaiAccessory("Ninja Gi", 30, type, "ninja_gi"));
            All.Add("Spike Mail", new ZoktaiAccessory("Spike Mail", 31, type, "spike_mail"));
            All.Add("Pitch Black Armor", new ZoktaiAccessory("Pitch Black Armor", 32, type, "pitch_black_armor"));
            All.Add("Mega Power", new ZoktaiAccessory("Mega Power", 33, type, "mega_power", crossOver: true));
            All.Add("Guts Power", new ZoktaiAccessory("Guts Power", 34, type, "guts_power", crossOver: true));
            All.Add("Proto Power", new ZoktaiAccessory("Proto Power", 35, type, "proto_power", crossOver: true));
            All.Add("Toad Power", new ZoktaiAccessory("Toad Power", 36, type, "toad_power", crossOver: true));
        }
    }
}
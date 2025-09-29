using System.Collections.Generic;
using System.Linq;

namespace BokInterface.Weapons {
    /// <summary>Class for Zoktai weapon instances and lists</summary>
    class ZoktaiWeapons {

        public Dictionary<string, Weapon> Swords = [],
            Hammers = [],
            Spears = [],
            Guns = [],
            Misc = [],
            All = [];

        public ZoktaiWeapons() {
            InitSwords();
            InitHammers();
            InitSpears();
            InitGuns();
            InitMisc();
            InitFullList();
        }

        ///<summary>Init weapon instances for Swords</summary>
        private void InitSwords() {
            string type = "Sword";
            Swords.Add("Gradius", new ZoktaiWeapon("Gradius", 1, type, "gradius"));
            Swords.Add("Short Sword", new ZoktaiWeapon("Short Sword", 2, type, "short_sword"));
            Swords.Add("Broadsword", new ZoktaiWeapon("Broadsword", 3, type, "broadsword"));
            Swords.Add("Long Sword", new ZoktaiWeapon("Long Sword", 4, type, "long_sword"));
            Swords.Add("Dull Blade", new ZoktaiWeapon("Dull Blade", 5, type, "dull_blade", rRank: true));
            Swords.Add("Zweihander", new ZoktaiWeapon("Zweihander", 6, type, "zweihander", row: 2));
            Swords.Add("Flamberge", new ZoktaiWeapon("Flamberge", 7, type, "flamberge", row: 2));
            Swords.Add("Claymore", new ZoktaiWeapon("Claymore", 8, type, "claymore", row: 2));
            Swords.Add("Magic Sword", new ZoktaiWeapon("Magic Sword", 9, type, "magic_sword", row: 2));
            Swords.Add("Katana", new ZoktaiWeapon("Katana", 10, type, "katana", row: 2, rRank: true));
            Swords.Add("Bastard Sword", new ZoktaiWeapon("Bastard Sword", 11, type, "bastard_sword", row: 3));
            Swords.Add("Great Sword", new ZoktaiWeapon("Great Sword", 12, type, "great_sword", row: 3));
            Swords.Add("Bushido Sword", new ZoktaiWeapon("Bushido Sword", 13, type, "bushido_sword", row: 3));
            Swords.Add("Blood Sword", new ZoktaiWeapon("Blood Sword", 14, type, "blood_sword", row: 3));
            Swords.Add("Muramasa", new ZoktaiWeapon("Muramasa", 15, type, "muramasa", row: 3, rRank: true));
            Swords.Add("Vorpal Sword", new ZoktaiWeapon("Vorpal Sword", 16, type, "vorpal_sword", row: 4));
            Swords.Add("Solar Sword", new ZoktaiWeapon("Solar Sword", 17, type, "solar_sword", row: 4));
            Swords.Add("Sword of Darkness", new ZoktaiWeapon("Sword of Darkness", 18, type, "sword_of_darkness", row: 4));
            Swords.Add("Gram", new ZoktaiWeapon("Gram", 19, type, "gram", row: 4, rRank: true));
        }

        ///<summary>Init weapon instances for Spears</summary>
        private void InitSpears() {
            string type = "Spear";
            Spears.Add("Short Spear", new ZoktaiWeapon("Short Spear", 20, type, "short_spear"));
            Spears.Add("Glaive", new ZoktaiWeapon("Glaive", 21, type, "glaive"));
            Spears.Add("Long Spear", new ZoktaiWeapon("Long Spear", 22, type, "long_spear"));
            Spears.Add("Lance", new ZoktaiWeapon("Lance", 23, type, "lance"));
            Spears.Add("Quarter Staff", new ZoktaiWeapon("Quarter Staff", 24, type, "quarter_staff", rRank: true));
            Spears.Add("Corsesca", new ZoktaiWeapon("Corsesca", 25, type, "corsesca", row: 2));
            Spears.Add("Fire Paw", new ZoktaiWeapon("Fire Paw", 26, type, "fire_paw", row: 2));
            Spears.Add("Bardiche", new ZoktaiWeapon("Bardiche", 27, type, "bardiche", row: 2));
            Spears.Add("Ice Glaive", new ZoktaiWeapon("Ice Glaive", 28, type, "ice_glaive", row: 2));
            Spears.Add("Rune Glaive", new ZoktaiWeapon("Rune Glaive", 29, type, "rune_glaive", row: 2, rRank: true));
            Spears.Add("Partisan", new ZoktaiWeapon("Partisan", 30, type, "partisan", row: 3));
            Spears.Add("Thunder Spear", new ZoktaiWeapon("Thunder Spear", 31, type, "thunder_spear", row: 3));
            Spears.Add("Blood Spear", new ZoktaiWeapon("Blood Spear", 32, type, "blood_spear", row: 3));
            Spears.Add("Grand Lance", new ZoktaiWeapon("Grand Lance", 33, type, "grand_lance", row: 3));
            Spears.Add("Rune Spear", new ZoktaiWeapon("Rune Spear", 34, type, "rune_spear", row: 3, rRank: true));
            Spears.Add("Halberd", new ZoktaiWeapon("Halberd", 35, type, "halberd", row: 4));
            Spears.Add("White Queen", new ZoktaiWeapon("White Queen", 36, type, "white_queen", row: 4));
            Spears.Add("Black Queen", new ZoktaiWeapon("Black Queen", 37, type, "black_queen", row: 4));
            Spears.Add("Gungnir", new ZoktaiWeapon("Gungnir", 38, type, "gungnir", row: 4, rRank: true));
        }

        ///<summary>Init weapon instances for Hammers</summary>
        private void InitHammers() {
            string type = "Hammer";
            Hammers.Add("Club", new ZoktaiWeapon("Club", 39, type, "club"));
            Hammers.Add("Hammer", new ZoktaiWeapon("Hammer", 40, type, "hammer"));
            Hammers.Add("Mace", new ZoktaiWeapon("Mace", 41, type, "mace"));
            Hammers.Add("Flail", new ZoktaiWeapon("Flail", 42, type, "flail"));
            Hammers.Add("Pounder", new ZoktaiWeapon("Pounder", 43, type, "pounder", rRank: true));
            Hammers.Add("Axe", new ZoktaiWeapon("Axe", 44, type, "axe", row: 2));
            Hammers.Add("Maul", new ZoktaiWeapon("Maul", 45, type, "maul", row: 2));
            Hammers.Add("Silver Mace", new ZoktaiWeapon("Silver Mace", 46, type, "silver_mace", row: 2));
            Hammers.Add("Silver Flail", new ZoktaiWeapon("Silver Flail", 47, type, "silver_flail", row: 2));
            Hammers.Add("Heavy Mace", new ZoktaiWeapon("Heavy Mace", 48, type, "heavy_mace", row: 2, rRank: true));
            Hammers.Add("Battle Axe", new ZoktaiWeapon("Battle Axe", 49, type, "battle_axe", row: 3));
            Hammers.Add("War Hammer", new ZoktaiWeapon("War Hammer", 50, type, "war_hammer", row: 3));
            Hammers.Add("Bloody Mace", new ZoktaiWeapon("Bloody Mace", 51, type, "bloody_mace", row: 3));
            Hammers.Add("Morning Star", new ZoktaiWeapon("Morning Star", 52, type, "morning_star", row: 3));
            Hammers.Add("Heavy Axe", new ZoktaiWeapon("Heavy Axe", 53, type, "heavy_axe", row: 3, rRank: true));
            Hammers.Add("Earthshaker", new ZoktaiWeapon("Earthshaker", 54, type, "earthshaker", row: 4));
            Hammers.Add("Daybreak", new ZoktaiWeapon("Daybreak", 55, type, "daybreak", row: 4));
            Hammers.Add("Twilight", new ZoktaiWeapon("Twilight", 56, type, "twilight", row: 4));
            Hammers.Add("Mjollnir", new ZoktaiWeapon("Mjollnir", 57, type, "mjollnir", row: 4, rRank: true));
        }

        ///<summary>Init weapon instances for Guns</summary>
        private void InitGuns() {
            string type = "Gun";
            Guns.Add("Broken Solar Gun", new ZoktaiWeapon("Broken Solar Gun", 58, type, "broken_solar_gun"));
            Guns.Add("Gun Del Sol", new ZoktaiWeapon("Gun Del Sol", 59, type, "gun_del_sol"));
            Guns.Add("Gun Del Hell", new ZoktaiWeapon("Gun Del Hell", 60, type, "gun_del_hell"));
            Guns.Add("Megabuster", new ZoktaiWeapon("Megabuster", 61, type, "megabuster"));
        }

        ///<summary>
        ///<para>Init weapon instances for Misc (ex: Astro weapons)</para>
        ///<remarks>These do not count towards Library completion</remarks>
        ///</summary>
        private void InitMisc() {
            Misc.Add("Star Piece", new ZoktaiWeapon("Star Piece", 62, "", "star_piece", eventWeapon: true));
            Misc.Add("Astro Sword", new ZoktaiWeapon("Astro Sword", 63, "Sword", "astro_sword", eventWeapon: true, adjustToLevel: true));
            Misc.Add("Astro Spear", new ZoktaiWeapon("Astro Spear", 64, "Spear", "astro_spear", eventWeapon: true, adjustToLevel: true));
            Misc.Add("Astro Hammer", new ZoktaiWeapon("Astro Hammer", 65, "Hammer", "astro_hammer", eventWeapon: true, adjustToLevel: true));
        }

        ///<summary>Init the full list containing all weapons (mostly used for editors)</summary>
        private void InitFullList() {
            All.Add("Empty slot", new ZoktaiWeapon("Empty slot", 0, ""));
            All = All
                .Concat(Swords)
                .Concat(Spears)
                .Concat(Hammers)
                .Concat(Guns)
                .Concat(Misc)
                .ToDictionary(e => e.Key, e => e.Value);
        }
    }
}

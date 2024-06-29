using System.Collections.Generic;
using System.Linq;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Weapons {
    /// <summary>Class for Zoktai weapon instances and lists</summary>
    class ZoktaiWeapons {

        public Dictionary<string, Weapon> Swords = [],
            Hammers = [],
            Spears = [],
            Guns = [],
            Misc = [],
            All = [];
        private readonly ZoktaiAddresses _memAddresses;
        private readonly MemoryValues _memoryValues;

        public ZoktaiWeapons(MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {
            _memoryValues = memoryValues;
            _memAddresses = zoktaiAddresses;

            // Init dictionnaries
            InitSwords();
            InitHammers();
            InitSpears();
            InitGuns();
            InitMisc();
            InitFullList();
        }

        ///<summary>Init weapon instances for Swords</summary>
        private void InitSwords() {
            Swords.Add("Gradius", new Weapon("Gradius", 1, "Sword", "gradius"));
            Swords.Add("Short Sword", new Weapon("Short Sword", 2, "Sword", "short_sword"));
            Swords.Add("Broadsword", new Weapon("Broadsword", 3, "Sword", "broadsword"));
            Swords.Add("Long Sword", new Weapon("Long Sword", 4, "Sword", "long_sword"));
            Swords.Add("Dull Blade", new Weapon("Dull Blade", 5, "Sword", "dull_blade", rRank: true));
            Swords.Add("Zweihander", new Weapon("Zweihander", 6, "Sword", "zweihander", row: 2));
            Swords.Add("Flamberge", new Weapon("Flamberge", 7, "Sword", "flamberge", row: 2));
            Swords.Add("Claymore", new Weapon("Claymore", 8, "Sword", "claymore", row: 2));
            Swords.Add("Magic Sword", new Weapon("Magic Sword", 9, "Sword", "magic_sword", row: 2));
            Swords.Add("Katana", new Weapon("Katana", 10, "Sword", "katana", row: 2, rRank: true));
            Swords.Add("Bastard Sword", new Weapon("Bastard Sword", 11, "Sword", "bastard_sword", row: 3));
            Swords.Add("Great Sword", new Weapon("Great Sword", 12, "Sword", "great_sword", row: 3));
            Swords.Add("Bushido Sword", new Weapon("Bushido Sword", 13, "Sword", "bushido_sword", row: 3));
            Swords.Add("Blood Sword", new Weapon("Blood Sword", 14, "Sword", "blood_sword", row: 3));
            Swords.Add("Muramasa", new Weapon("Muramasa", 15, "Sword", "muramasa", row: 3, rRank: true));
            Swords.Add("Vorpal Sword", new Weapon("Vorpal Sword", 16, "Sword", "vorpal_sword", row: 4));
            Swords.Add("Solar Sword", new Weapon("Solar Sword", 17, "Sword", "solar_sword", row: 4));
            Swords.Add("Sword of Darkness", new Weapon("Sword of Darkness", 18, "Sword", "sword_of_darkness", row: 4));
            Swords.Add("Gram", new Weapon("Gram", 19, "Sword", "Gram", row: 4));
        }

        ///<summary>Init weapon instances for Spears</summary>
        private void InitSpears() {
            Spears.Add("Short Spear", new Weapon("Short Spear", 20, "Spear", "short_spear"));
            Spears.Add("Glaive", new Weapon("Glaive", 21, "Spear", "glaive"));
            Spears.Add("Long Spear", new Weapon("Long Spear", 22, "Spear", "long_spear"));
            Spears.Add("Lance", new Weapon("Lance", 23, "Spear", "lance"));
            Spears.Add("Quarter Staff", new Weapon("Quarter Staff", 24, "Spear", "quarter_staff", rRank: true));
            Spears.Add("Corsesca", new Weapon("Corsesca", 25, "Spear", "corsesca", row: 2));
            Spears.Add("Fire Paw", new Weapon("Fire Paw", 26, "Spear", "fire_paw", row: 2));
            Spears.Add("Bardiche", new Weapon("Bardiche", 27, "Spear", "bardiche", row: 2));
            Spears.Add("Ice Glaive", new Weapon("Ice Glaive", 28, "Spear", "ice_glaive", row: 2));
            Spears.Add("Rune Glaive", new Weapon("Rune Glaive", 29, "Spear", "rune_glaive", row: 2, rRank: true));
            Spears.Add("Partisan", new Weapon("Partisan", 30, "Spear", "partisan", row: 3));
            Spears.Add("Thunder Spear", new Weapon("Thunder Spear", 31, "Spear", "thunder_spear", row: 3));
            Spears.Add("Blood Spear", new Weapon("Blood Spear", 32, "Spear", "blood_spear", row: 3));
            Spears.Add("Grand Lance", new Weapon("Grand Lance", 33, "Spear", "grand_lance", row: 3));
            Spears.Add("Rune Spear", new Weapon("Rune Spear", 34, "Spear", "rune_spear", row: 3, rRank: true));
            Spears.Add("Halberd", new Weapon("Halberd", 35, "Spear", "halberd", row: 4));
            Spears.Add("White Queen", new Weapon("White Queen", 36, "Spear", "white_queen", row: 4));
            Spears.Add("Black Queen", new Weapon("Black Queen", 37, "Spear", "black_queen", row: 4));
            Spears.Add("Gungnir", new Weapon("Gungnir", 38, "Spear", "gungnir", row: 4));
        }

        ///<summary>Init weapon instances for Hammers</summary>
        private void InitHammers() {
            Hammers.Add("Club", new Weapon("Club", 39, "Hammer", "club"));
            Hammers.Add("Hammer", new Weapon("Hammer", 40, "Hammer", "hammer"));
            Hammers.Add("Mace", new Weapon("Mace", 41, "Hammer", "mace"));
            Hammers.Add("Flail", new Weapon("Flail", 42, "Hammer", "flail"));
            Hammers.Add("Pounder", new Weapon("Pounder", 43, "Hammer", "pounder", rRank: true));
            Hammers.Add("Axe", new Weapon("Axe", 44, "Hammer", "axe", row: 2));
            Hammers.Add("Maul", new Weapon("Maul", 45, "Hammer", "maul", row: 2));
            Hammers.Add("Silver Mace", new Weapon("Silver Mace", 46, "Hammer", "silver_mace", row: 2));
            Hammers.Add("Silver Flail", new Weapon("Silver Flail", 47, "Hammer", "silver_flail", row: 2));
            Hammers.Add("Heavy Mace", new Weapon("Heavy Mace", 48, "Hammer", "heavy_mace", row: 2, rRank: true));
            Hammers.Add("Battle Axe", new Weapon("Battle Axe", 49, "Hammer", "battle_axe", row: 3));
            Hammers.Add("War Hammer", new Weapon("War Hammer", 50, "Hammer", "war_hammer", row: 3));
            Hammers.Add("Bloody Mace", new Weapon("Bloody Mace", 51, "Hammer", "bloody_mace", row: 3));
            Hammers.Add("Morning Star", new Weapon("Morning Star", 52, "Hammer", "morning_star", row: 3));
            Hammers.Add("Heavy Axe", new Weapon("Heavy Axe", 53, "Hammer", "heavy_axe", row: 3, rRank: true));
            Hammers.Add("Earthshaker", new Weapon("Earthshaker", 54, "Hammer", "earthshaker", row: 4));
            Hammers.Add("Daybreak", new Weapon("Daybreak", 55, "Hammer", "daybreak", row: 4));
            Hammers.Add("Twilight", new Weapon("Twilight", 56, "Hammer", "twilight", row: 4));
            Hammers.Add("Mjollnir", new Weapon("Mjollnir", 57, "Hammer", "mjollnir", row: 4));
        }

        ///<summary>Init weapon instances for Guns</summary>
        private void InitGuns() {
            Guns.Add("Broken Solar Gun", new Weapon("Broken Solar Gun", 58, "Gun", "broken_solar_gun"));
            Guns.Add("Gun Del Sol", new Weapon("Gun Del Sol", 59, "Gun", "gun_del_sol"));
            Guns.Add("Gun Del Hell", new Weapon("Gun Del Hell", 60, "Gun", "gun_del_hell"));
            Guns.Add("Megabuster", new Weapon("Megabuster", 61, "Gun", "megabuster"));
        }

        ///<summary>
        ///<para>Init weapon instances for Misc (ex: Astro weapons)</para>
        ///<remarks>These do not count towards Library completion</remarks>
        ///</summary>
        private void InitMisc() {
            Misc.Add("Star Piece", new Weapon("Star Piece", 62, "", "star_piece", eventWeapon: true));
            Misc.Add("Astro Sword", new Weapon("Astro Sword", 63, "Sword", "astro_sword", eventWeapon: true, adjustToLevel: true));
            Misc.Add("Astro Spear", new Weapon("Astro Spear", 64, "Spear", "astro_spear", eventWeapon: true, adjustToLevel: true));
            Misc.Add("Astro Hammer", new Weapon("Astro Hammer", 65, "Hammer", "astro_hammer", eventWeapon: true, adjustToLevel: true));
        }

        ///<summary>Init the full list containing all weapons (mostly used for editors)</summary>
        private void InitFullList() {
            All.Add("Empty slot", new Weapon("Empty slot", 0, ""));
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
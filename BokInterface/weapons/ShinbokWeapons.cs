using System.Collections.Generic;
using System.Linq;

namespace BokInterface.Weapons {
    /// <summary>Class for Shinbok weapon instances and lists</summary>
    class ShinbokWeapons {

        public Dictionary<string, Weapon> FencingSwords = [],
            CurvedSwords = [],
            Katanas = [],
            LongSwords = [],
            GreatSwords = [],
            LargeGuns = [],
            All = [];

        private string _type = "";

        public ShinbokWeapons() {
            InitFencingSwords();
            InitCurvedSwords();
            InitKatanas();
            InitLongSwords();
            InitGreatSwords();
            InitLargeGuns();
            InitFullList();
        }

        ///<summary>Init weapon instances for Fencing Swords</summary>
        private void InitFencingSwords() {
            _type = "Fencing sword";
            FencingSwords.Add("Estoc", new ShinbokWeapon("Estoc", 1, _type, "estoc", level: 5, baseDamage: 10, attackPatterns: ["P1----P3", "P1,P2------P4", "P1,P2,P4"]));
            FencingSwords.Add("Rapier", new ShinbokWeapon("Rapier", 5, _type, "rapier", level: 10, baseDamage: 15, attackPatterns: ["P1,P2------P4", "P1,P2------H4", "P1,P2,P4"]));
            FencingSwords.Add("Epee", new ShinbokWeapon("Epee", 10, _type, "epee", level: 15, baseDamage: 20, attackPatterns: ["P1,P2------H4", "P1,P2,P4", "P1,P2,P3------P4"]));
            FencingSwords.Add("Wild Rose", new ShinbokWeapon("Wild Rose", 12, _type, "wild_rose", level: 20, baseDamage: 25, attackPatterns: ["P1,P2,P4", "P1,P2,P3------P4", "P1,P2,P3------H4"]));
            FencingSwords.Add("Saber", new ShinbokWeapon("Saber", 19, _type, "saber", level: 30, baseDamage: 35, attackPatterns: ["P1,P2------H4", "P1,P2,P3------H4", "P1,P2,P3,P4------H6"]));
            FencingSwords.Add("Pallasch", new ShinbokWeapon("Pallasch", 24, _type, "pallasch", level: 35, baseDamage: 40, attackPatterns: ["P1,P2,P3------H4", "P1,P2,P3,P4------P6", "P1,P2,P3,P4------H6"]));
            FencingSwords.Add("Bloody Rose", new ShinbokWeapon("Bloody Rose", 26, _type, "bloody_rose", level: 40, baseDamage: 45, attackPatterns: ["P1,P2,P3------H4", "P1,P2,P3,P4------P6", "P1,P2,P3,P4------H6"]));
            FencingSwords.Add("Prime Rose", new ShinbokWeapon("Prime Rose", 34, _type, "prime_rose", level: 50, baseDamage: 55, attackPatterns: ["P1,P2,P3,P4------P6", "P1,P2,P3,P4------H6", "P1,P2,P3,H4------V6"]));
            FencingSwords.Add("Blue Rose", new ShinbokWeapon("Blue Rose", 40, _type, "blue_rose", level: 55, baseDamage: 60, attackPatterns: ["P1,P2,P3,H4------H6", "P1,P2,P3,H4------V6", "P1,P2,H3,V4------P4"]));
            FencingSwords.Add("La Vie En Rose", new ShinbokWeapon("La Vie En Rose", 47, _type, "la_vie_en_rose", adjustToLevel: true, attackPatterns: ["P1,P2,P3,P4------P6", "P1,P2,P3,P4------P6", "P1,P2,P3,P4------P6"]));
        }

        ///<summary>Init weapon instances for Curved Swords</summary>
        private void InitCurvedSwords() {
            _type = "Curved sword";
            CurvedSwords.Add("Kopis", new ShinbokWeapon("Kopis", 2, _type, "kopis", level: 5, baseDamage: 10, attackPatterns: ["H3", "H2,H3", "H2,H3------H6"]));
            CurvedSwords.Add("Bronze Edge", new ShinbokWeapon("Bronze Edge", 6, _type, "bronze_edge", level: 10, baseDamage: 15, attackPatterns: ["H2,H3", "H2,H3------H6", "H2--H3----H4"]));
            CurvedSwords.Add("Falcata", new ShinbokWeapon("Falcata", 13, _type, "falcata", level: 20, baseDamage: 25, attackPatterns: ["H2,H3------H6", "H2,H3------V6", "H2,H3------V6,H6"]));
            FencingSwords.Add("Falchion", new ShinbokWeapon("Falchion", 17, _type, "falchion", level: 25, baseDamage: 30, attackPatterns: ["H2,H3------V6", "H2,H3------V6,H6", "H2,H3--H6------V6"]));
            CurvedSwords.Add("Silver Edge", new ShinbokWeapon("Silver Edge", 20, _type, "silver_edge", level: 30, baseDamage: 35, attackPatterns: ["H2--H3----H4", "H2--H3----H4------H6", "H2--H3----H4------H4"]));
            CurvedSwords.Add("Kora", new ShinbokWeapon("Kora", 27, _type, "kora", level: 40, baseDamage: 45, attackPatterns: ["H2,H3------V6", "H2,H3--H6------V6", "H2,H3--H6----H6------V6"]));
            CurvedSwords.Add("Ice Edge", new ShinbokWeapon("Ice Edge", 31, _type, "ice_edge", level: 45, baseDamage: 50, attackPatterns: ["H2--H3----H4------H6", "H2--H3----H4------H4", "H2,H3--H4----H4------V6"]));
            CurvedSwords.Add("Damascus Edge", new ShinbokWeapon("Damascus Edge", 35, _type, "damascus_edge", level: 50, baseDamage: 55, attackPatterns: ["H2--H3----H4------H4", "H2,H3--H4----H4------V6", "H2,H3,H4--H4----V6"]));
            CurvedSwords.Add("Thunder Edge", new ShinbokWeapon("Thunder Edge", 41, _type, "thunder_edge", level: 55, baseDamage: 60, attackPatterns: ["H2,H3--H4----H4------V6", "H2,H3,H4--H4----V6", "H2,H3,H4--V4----H6"]));
        }

        ///<summary>Init weapon instances for Katanas</summary>
        private void InitKatanas() {
            _type = "Katana";
            Katanas.Add("Kagerou", new ShinbokWeapon("Kagerou", 7, _type, "kagerou", level: 10, baseDamage: 15, attackPatterns: ["V2,H3", "V2,H3--P4", "V2,H3,V3--P4"]));
            Katanas.Add("Shiranui", new ShinbokWeapon("Shiranui", 14, _type, "shiranui", level: 20, baseDamage: 25, attackPatterns: ["H2--P4", "H2,V4", "H2,V4--P4"]));
            Katanas.Add("Shinzan", new ShinbokWeapon("Shinzan", 21, _type, "shinzan", level: 30, baseDamage: 35, attackPatterns: ["V2,H3--P4", "V2,H3,V3--P4", "V2,H3,H3,H4,V4"]));
            Katanas.Add("Sakurabana", new ShinbokWeapon("Sakurabana", 28, _type, "sakurabana", level: 40, baseDamage: 45, attackPatterns: ["V2,H3,V3--P4", "V2,H3,H3,H4,V4", "V2,H3--H3,V4--P4"]));
            Katanas.Add("Shuusui", new ShinbokWeapon("Shuusui", 36, _type, "shuusui", level: 50, baseDamage: 55, attackPatterns: ["H2,V4", "H2,V4--P4", "H2,V4,H4--P4"]));
            Katanas.Add("Murasame", new ShinbokWeapon("Murasame", 42, _type, "murasame", level: 55, baseDamage: 60, attackPatterns: ["V2,H3,H3,H4,V4", "V2,H3--H3,V4--P4", "V2,H3,V3--H4--P4"]));
        }

        ///<summary>Init weapon instances for Long Swords</summary>
        private void InitLongSwords() {
            _type = "Long sword";
            LongSwords.Add("Gradius", new ShinbokWeapon("Gradius", 0, _type, "gradius", level: 1, baseDamage: 6, attackPatterns: ["H3", "H2------V4", "H2--V3"]));
            LongSwords.Add("Short Sword", new ShinbokWeapon("Short Sword", 3, _type, "short_sword", level: 5, baseDamage: 10, attackPatterns: ["H3", "H2------V4", "H2--V3"]));
            LongSwords.Add("Broadsword", new ShinbokWeapon("Broadsword", 8, _type, "broadsword", level: 10, baseDamage: 15, attackPatterns: ["H2------V4", "H2--V3", "H2--V2----P6"]));
            LongSwords.Add("Katzbalger", new ShinbokWeapon("Katzbalger", 11, _type, "katzbalger", level: 15, baseDamage: 20, attackPatterns: ["H2--V3", "H2--V2----P6", "H2----H2--V4"]));
            LongSwords.Add("Iron Blade", new ShinbokWeapon("Iron Blade", 15, _type, "iron_blade", level: 20, baseDamage: 25, attackPatterns: ["H2--V2----P6", "H2----H2--V4", "H2--H2--V4----P6"]));
            LongSwords.Add("Schiavona", new ShinbokWeapon("Schiavona", 22, _type, "schiavona", level: 30, baseDamage: 35, attackPatterns: ["H2----H2--V4", "", "H2,H2--V4----H4"]));
            LongSwords.Add("Bastard Sword", new ShinbokWeapon("Bastard Sword", 25, _type, "bastard_sword", level: 35, baseDamage: 40, attackPatterns: ["H2----H2--V4", "H2--H2--V4----P6", "H2,H2--V4----H4"]));
            LongSwords.Add("Nameless Sword", new ShinbokWeapon("Nameless Sword", 29, _type, "nameless_sword", level: 40, baseDamage: 45, attackPatterns: ["H2--V2----H4", "H2--H2--V4----P6", "H2,H2--V4----H4"]));
            LongSwords.Add("Air Blade", new ShinbokWeapon("Air Blade", 32, _type, "air_blade", level: 45, baseDamage: 50, attackPatterns: ["H2--H2--V4----P6", "H2,H2--V4----H4", "H2,V2----H4--V4"]));
            LongSwords.Add("Fire Blade", new ShinbokWeapon("Fire Blade", 43, _type, "fire_blade", level: 55, baseDamage: 60, attackPatterns: ["H2,H2--V4----H4", "H2,V2----H4----H4----P6", "H2,V2----P4----H4----V6"]));
            LongSwords.Add("Gram", new ShinbokWeapon("Gram", 37, _type, "gram", level: 50, baseDamage: 55, attackPatterns: ["H2--H2--V4----P6", "H2,H2--V4----H4", "H2,V2----H4--V4"]));
            LongSwords.Add("Platinum Blade", new ShinbokWeapon("Platinum Blade", 38, _type, "platinum_blade", level: 50, baseDamage: 55, attackPatterns: ["H2,H2--V4----H4", "H2,V2----H4--V4", "H2,V2----H4----H4----P6"]));
            LongSwords.Add("Sol Blade", new ShinbokWeapon("Sol Blade", 45, _type, "sol_blade", level: 60, baseDamage: 65, attackPatterns: ["H2,V2----H4----H4----P6", "H2,V2----P4----H4----V6", "H2,V2----H4--V4----P4"]));
        }

        ///<summary>Init weapon instances for Great Swords</summary>
        private void InitGreatSwords() {
            _type = "Great sword";
            GreatSwords.Add("Zweihander", new ShinbokWeapon("Zweihander", 4, _type, "zweihander", level: 5, baseDamage: 12, attackPatterns: ["--V4", "V4", "--V4--H6"]));
            GreatSwords.Add("Iron Sword", new ShinbokWeapon("Iron Sword", 9, _type, "iron_sword", level: 10, baseDamage: 17, attackPatterns: ["--V4", "------V4--V6", "------V4--V6"]));
            GreatSwords.Add("Flamberge", new ShinbokWeapon("Flamberge", 16, _type, "flamberge", level: 20, baseDamage: 27, attackPatterns: ["V4", "--V4--H6", "V4--H6"]));
            GreatSwords.Add("Claymore", new ShinbokWeapon("Claymore", 18, _type, "claymore", level: 25, baseDamage: 32, attackPatterns: ["--V4--H6", "V4--H6", "--V4--H4----H6"]));
            GreatSwords.Add("Steel Sword", new ShinbokWeapon("Steel Sword", 23, _type, "steel_sword", level: 30, baseDamage: 37, attackPatterns: ["------V4--V6", "------V4--V6", "------V4--V4------V6"]));
            GreatSwords.Add("Greatsword", new ShinbokWeapon("Greatsword", 30, _type, "greatsword", level: 40, baseDamage: 47, attackPatterns: ["V4--H6", "--V4--H4----H6", "--V4--H4--V6"]));
            GreatSwords.Add("King Of Swords", new ShinbokWeapon("King Of Swords", 33, _type, "king_of_swords", level: 45, baseDamage: 52, attackPatterns: ["--V4--H4----H6", "--V4--H4--V6", "--V4--H4--H4------P6"]));
            GreatSwords.Add("Adamant Sword", new ShinbokWeapon("Adamant Sword", 39, _type, "adamant_sword", level: 50, baseDamage: 57, attackPatterns: ["------V4--V4------V6", "----V4--V4------V6", "----V4--V4--V4------V6"]));
            GreatSwords.Add("Sword Of Earth", new ShinbokWeapon("Sword Of Earth", 44, _type, "sword_of_earth", level: 55, baseDamage: 62, attackPatterns: ["--V4--H4--V6", "--V4--H4--H4------P6", "V4--H4--H4------P6"]));
            GreatSwords.Add("Immortal Sword", new ShinbokWeapon("Immortal Sword", 46, _type, "immortal_sword", level: 60, baseDamage: 67, attackPatterns: ["--V4--H4--H4------P6", "V4--H4--H4------P6", "V4--H4--H4--V4--P6"]));
        }

        ///<summary>Init weapon instances for LargeGuns</summary>
        private void InitLargeGuns() {
            _type = "Large gun";
            LargeGuns.Add("Rockbuster", new ShinbokWeapon("Rockbuster", 48, _type, "rockbuster", adjustToLevel: true, baseDamage: 6));
        }

        ///<summary>Init the full list containing all weapons (mostly used for editors)</summary>
        private void InitFullList() {
            All.Add("Empty slot", new ShinbokWeapon("Empty slot", 255, ""));
            All = All
                .Concat(FencingSwords)
                .Concat(CurvedSwords)
                .Concat(Katanas)
                .Concat(LongSwords)
                .Concat(GreatSwords)
                .Concat(LargeGuns)
                .ToDictionary(e => e.Key, e => e.Value);

            All.OrderBy(x => x.Value.value);
        }
    }
}
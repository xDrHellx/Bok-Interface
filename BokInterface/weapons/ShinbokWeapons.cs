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
            FencingSwords.Add("Estoc", new ShinbokWeapon("Estoc", 1, _type, level: 5, baseDamage: 10));
            FencingSwords.Add("Rapier", new ShinbokWeapon("Rapier", 5, _type, level: 10, baseDamage: 15));
            FencingSwords.Add("Epee", new ShinbokWeapon("Epee", 10, _type, level: 15, baseDamage: 20));
            FencingSwords.Add("Wild Rose", new ShinbokWeapon("Wild Rose", 12, _type, level: 20, baseDamage: 25));
            FencingSwords.Add("Saber", new ShinbokWeapon("Saber", 19, _type, level: 30, baseDamage: 35));
            FencingSwords.Add("Pallasch", new ShinbokWeapon("Pallasch", 24, _type, level: 35, baseDamage: 40));
            FencingSwords.Add("Bloody Rose", new ShinbokWeapon("Bloody Rose", 26, _type, level: 40, baseDamage: 45));
            FencingSwords.Add("Prime Rose", new ShinbokWeapon("Prime Rose", 34, _type, level: 50, baseDamage: 55));
            FencingSwords.Add("Blue Rose", new ShinbokWeapon("Blue Rose", 40, _type, level: 55, baseDamage: 60));
            FencingSwords.Add("La Vie En Rose", new ShinbokWeapon("La Vie En Rose", 47, _type, adjustToLevel: true));
        }

        ///<summary>Init weapon instances for Curved Swords</summary>
        private void InitCurvedSwords() {
            _type = "Curved sword";
            CurvedSwords.Add("Kopis", new ShinbokWeapon("Kopis", 2, _type, level: 5, baseDamage: 10));
            CurvedSwords.Add("Bronze Edge", new ShinbokWeapon("Bronze Edge", 6, _type, level: 10, baseDamage: 15));
            CurvedSwords.Add("Falcata", new ShinbokWeapon("Falcata", 13, _type, level: 20, baseDamage: 25));
            FencingSwords.Add("Falchion", new ShinbokWeapon("Falchion", 17, _type, level: 25, baseDamage: 30));
            CurvedSwords.Add("Silver Edge", new ShinbokWeapon("Silver Edge", 20, _type, level: 30, baseDamage: 35));
            CurvedSwords.Add("Kora", new ShinbokWeapon("Kora", 27, _type, level: 40, baseDamage: 45));
            CurvedSwords.Add("Ice Edge", new ShinbokWeapon("Ice Edge", 31, _type, level: 45, baseDamage: 50));
            CurvedSwords.Add("Damascus Edge", new ShinbokWeapon("Damascus Edge", 35, _type, level: 50, baseDamage: 55));
            CurvedSwords.Add("Thunder Edge", new ShinbokWeapon("Thunder Edge", 41, _type, level: 55, baseDamage: 60));
        }

        ///<summary>Init weapon instances for Katanas</summary>
        private void InitKatanas() {
            _type = "Katana";
            Katanas.Add("Kagerou", new ShinbokWeapon("Kagerou", 7, _type, level: 10, baseDamage: 15));
            Katanas.Add("Shiranui", new ShinbokWeapon("Shiranui", 14, _type, level: 20, baseDamage: 25));
            Katanas.Add("Shinzan", new ShinbokWeapon("Shinzan", 21, _type, level: 30, baseDamage: 35));
            Katanas.Add("Sakurabana", new ShinbokWeapon("Sakurabana", 28, _type, level: 40, baseDamage: 45));
            Katanas.Add("Shuusui", new ShinbokWeapon("Shuusui", 36, _type, level: 50, baseDamage: 55));
            Katanas.Add("Murasame", new ShinbokWeapon("Murasame", 42, _type, level: 55, baseDamage: 60));
        }

        ///<summary>Init weapon instances for Long Swords</summary>
        private void InitLongSwords() {
            _type = "Long sword";
            LongSwords.Add("Gradius", new ShinbokWeapon("Gradius", 0, _type, level: 1, baseDamage: 6));
            LongSwords.Add("Short Sword", new ShinbokWeapon("Short Sword", 3, _type, level: 5, baseDamage: 10));
            LongSwords.Add("Broadsword", new ShinbokWeapon("Broadsword", 8, _type, level: 10, baseDamage: 15));
            LongSwords.Add("Katzbalger", new ShinbokWeapon("Katzbalger", 11, _type, level: 15, baseDamage: 20));
            LongSwords.Add("Iron Blade", new ShinbokWeapon("Iron Blade", 15, _type, level: 20, baseDamage: 25));
            LongSwords.Add("Schiavona", new ShinbokWeapon("Schiavona", 22, _type, level: 30, baseDamage: 35));
            LongSwords.Add("Bastard Sword", new ShinbokWeapon("Bastard Sword", 25, _type, level: 35, baseDamage: 40));
            LongSwords.Add("Nameless Sword", new ShinbokWeapon("Nameless Sword", 29, _type, level: 40, baseDamage: 45));
            LongSwords.Add("Air Blade", new ShinbokWeapon("Air Blade", 32, _type, level: 45, baseDamage: 50));
            LongSwords.Add("Fire Blade", new ShinbokWeapon("Fire Blade", 43, _type, level: 55, baseDamage: 60));
            LongSwords.Add("Gram", new ShinbokWeapon("Gram", 37, _type, level: 50, baseDamage: 55));
            LongSwords.Add("Platinum Blade", new ShinbokWeapon("Platinum Blade", 38, _type, level: 50, baseDamage: 55));
            LongSwords.Add("Sol Blade", new ShinbokWeapon("Sol Blade", 45, _type, level: 60, baseDamage: 65));
        }

        ///<summary>Init weapon instances for Great Swords</summary>
        private void InitGreatSwords() {
            _type = "Great sword";
            GreatSwords.Add("Zweihander", new ShinbokWeapon("Zweihander", 4, _type, level: 5, baseDamage: 12));
            GreatSwords.Add("Iron Sword", new ShinbokWeapon("Iron Sword", 9, _type, level: 10, baseDamage: 17));
            GreatSwords.Add("Flamberge", new ShinbokWeapon("Flamberge", 16, _type, level: 20, baseDamage: 27));
            GreatSwords.Add("Claymore", new ShinbokWeapon("Claymore", 18, _type, level: 25, baseDamage: 32));
            GreatSwords.Add("Steel Sword", new ShinbokWeapon("Steel Sword", 23, _type, level: 30, baseDamage: 37));
            GreatSwords.Add("Greatsword", new ShinbokWeapon("Greatsword", 30, _type, level: 40, baseDamage: 47));
            GreatSwords.Add("King Of Swords", new ShinbokWeapon("King Of Swords", 33, _type, level: 45, baseDamage: 52));
            GreatSwords.Add("Adamant Sword", new ShinbokWeapon("Adamant Sword", 39, _type, level: 50, baseDamage: 57));
            GreatSwords.Add("Sword Of Earth", new ShinbokWeapon("Sword Of Earth", 44, _type, level: 55, baseDamage: 62));
            GreatSwords.Add("Immortal Sword", new ShinbokWeapon("Immortal Sword", 46, _type, level: 60, baseDamage: 67));
        }

        ///<summary>Init weapon instances for LargeGuns</summary>
        private void InitLargeGuns() {
            _type = "Large gun";
            LargeGuns.Add("Rockbuster", new ShinbokWeapon("Rockbuster", 48, _type, adjustToLevel: true, baseDamage: 6));
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
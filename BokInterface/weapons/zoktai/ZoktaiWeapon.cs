using System.Drawing;

namespace BokInterface.Weapons {
    ///<summary>Class for representing a weapon in Zoktai</summary>
    class ZoktaiWeapon : Weapon {

        /// <summary>Bonus if applicable (can be negative, maluses are possible through forging)</summary>
        public int bonus;
        /// <summary>Indicates if R-Rank weapon</summary>
        public bool rRank;

        public ZoktaiWeapon(string name, uint value, string type, string icon = "", int durability = 0, int bonus = 0, string spEffect = "", int baseDamage = 1, int level = 1, int row = 1, bool rRank = false, int buyPrice = 0, bool eventWeapon = false, bool adjustToLevel = false) : base(name, value, type, icon, durability, spEffect, baseDamage, level, row, buyPrice, eventWeapon, adjustToLevel) {
            this.bonus = bonus;
            this.rRank = rRank;

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ZoktaiResources", icon);
                } catch { }
            }
        }
    }
}

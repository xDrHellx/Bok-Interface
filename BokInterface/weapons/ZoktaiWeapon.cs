using System.Drawing;

namespace BokInterface.Weapons {
    ///<summary>Class for representing a weapon in Zoktai</summary>
    class ZoktaiWeapon : Weapon {

        public ZoktaiWeapon(string name, uint value, string type, string icon = "", int durability = 0, int bonus = 0, string spEffect = "", int baseDamage = 1, int level = 1, int row = 1, bool rRank = false, int buyPrice = 0, bool eventWeapon = false, bool adjustToLevel = false) : base(name, value, type) {
            this.name = name;
            this.value = value;
            this.type = type;
            this.durability = durability;
            this.bonus = bonus;
            this.spEffect = spEffect;
            this.baseDamage = baseDamage;
            this.level = level;
            this.row = row;
            this.rRank = rRank;
            this.eventWeapon = eventWeapon;
            this.adjustToLevel = adjustToLevel;
            this.buyPrice = buyPrice;

            // Price for selling is always the buying price divided by 2 (or 0 if it cannot be sold)
            sellPrice = buyPrice > 0 ? buyPrice / 2 : 0;

            // If an icon was specified try getting & setting it to the property
            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ZoktaiResources", icon);
                } catch { }
            }
        }
    }
}
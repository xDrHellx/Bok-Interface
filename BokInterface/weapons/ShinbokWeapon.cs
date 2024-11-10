using System.Drawing;

namespace BokInterface.Weapons {
    ///<summary>Class for representing a weapon in Shinbok</summary>
    class ShinbokWeapon : Weapon {

        /// <summary>2nd SP effect the weapon can obtain (SP ability in-game)</summary>
        public string spEffect2;

        public ShinbokWeapon(string name, uint value, string type, string icon = "", int durability = 0, string spEffect = "", string spEffect2 = "", int baseDamage = 1, int level = 1, int row = 1, int buyPrice = 0, bool eventWeapon = false, bool adjustToLevel = false) : base(name, value, type) {
            this.name = name;
            this.value = value;
            this.type = type;
            this.durability = durability;
            this.spEffect = spEffect;
            this.spEffect2 = spEffect2;
            this.baseDamage = baseDamage;
            this.level = level;
            this.row = row;
            this.eventWeapon = eventWeapon;
            this.adjustToLevel = adjustToLevel;
            this.buyPrice = buyPrice;

            // Price for selling is always the buying price divided by 2 (or 0 if it cannot be sold)
            sellPrice = buyPrice > 0 ? buyPrice / 2 : 0;

            // If an icon was specified try getting & setting it to the property
            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ShinbokResources", icon);
                } catch { }
            }
        }
    }
}
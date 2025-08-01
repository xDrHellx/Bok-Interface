using System.Collections.Generic;
using System.Drawing;

namespace BokInterface.Weapons {
    ///<summary>Class for representing a weapon in Shinbok</summary>
    class ShinbokWeapon : Weapon {

        /// <summary>2nd SP effect the weapon can obtain (SP ability in-game)</summary>
        public string spEffect2;
        /// <summary>Instances of ShinbokSwordAttackPatterns for the attack patterns of this weapon (empty object[] if none is set)</summary>
        public List<string> attackPatterns;

        public ShinbokWeapon(string name, uint value, string type, string icon = "", int durability = 0, string spEffect = "", string spEffect2 = "", int baseDamage = 1, int level = 1, int row = 1, int buyPrice = 0, bool eventWeapon = false, bool adjustToLevel = false, List<string>? attackPatterns = null) : base(name, value, type, icon, durability, spEffect, baseDamage, level, row, buyPrice, eventWeapon, adjustToLevel) {
            this.spEffect2 = spEffect2;
            this.attackPatterns = attackPatterns ?? (["", "", ""]);

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ShinbokResources", icon);
                } catch { }
            }
        }
    }
}
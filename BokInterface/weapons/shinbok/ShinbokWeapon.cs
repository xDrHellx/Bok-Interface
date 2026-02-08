using System.Collections.Generic;

namespace BokInterface.Weapons {
    ///<summary>Class for representing a weapon in Shinbok</summary>
    class ShinbokWeapon(string name, uint value, string type, string icon = "", int durability = 0, string spEffect = "", string spEffect2 = "", int baseDamage = 1, int level = 1, int row = 1, int buyPrice = 0, bool eventWeapon = false, bool adjustToLevel = false, List<string>? attackPatterns = null) : Weapon(name, value, type, icon, durability, spEffect, baseDamage, level, row, buyPrice, eventWeapon, adjustToLevel) {

        /// <summary>2nd SP effect the weapon can obtain (SP ability in-game)</summary>
        public string spEffect2 = spEffect2;
        /// <summary>Instances of ShinbokSwordAttackPatterns for the attack patterns of this weapon (empty object[] if none is set)</summary>
        public List<string> attackPatterns = attackPatterns ?? (["", "", ""]);
        protected override string library { get => "ShinbokResources"; }
    }
}

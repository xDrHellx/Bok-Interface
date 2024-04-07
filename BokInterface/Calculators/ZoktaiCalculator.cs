namespace BokInterface.Tools.Calculator {
    /// <summary>Class for calculations related to Boktai 2</summary>
    partial class ZoktaiCalculator {

        #region Attack Power

        /// <summary>Get the player's attack power for Swords, Spears and Hammers</summary>
        /// <param name="level">Player level</param>
        /// <param name="strength">STR stat points</param>
        /// <param name="weaponPower">Weapon base damage</param>
        /// <param name="attackPowerIncrease">Effects affecting damage output</param>
        /// <returns><c>Float</c>Attack power</returns>
        public static float GetPlayerAttackPower(int level, int strength, int weaponPower, float attackPowerIncrease) {
            return (level + strength) / 2 + weaponPower + attackPowerIncrease;
        }

        /// <summary>Get the player's attack power for Fists (unarmed)</summary>
        /// <param name="level">Player level</param>
        /// <param name="strength">STR stat< points/param>
        /// <param name="fistSkillLevel">Fists skill level</param>
        /// <param name="attackPowerIncrease">Effects affecting damage output</param>
        /// <returns><c>Float</c>Attack power</returns>
        public static float GetPlayerFistsAttackPower(int level, int strength, int fistSkillLevel, float attackPowerIncrease) {
            return (level + strength) / 2 + (level + fistSkillLevel) / 4 + attackPowerIncrease;
        }

        /// <summary>Get the player's attack power for Guns</summary>
        /// <param name="gunSkillLevel">Gun skill level</param>
        /// <param name="weaponPower">Weapon base damage</param>
        /// <param name="attackPowerIncrease">Effects affecting damage output</param>
        /// <param name="charge">Gun charge (1, 1.5 or 2) (be default 1)</param>
        /// <returns><c>Float</c>Attack power</returns>
        public static float GetPlayerGunAttackPower(int gunSkillLevel, int weaponPower, int attackPowerIncrease, float charge = 1) {

            // If charge is invalid, set it to the default
            charge = charge != 1.5 || charge != 2 ? 1 : charge;

            // Extra damage based on the skill level
            int skillDamage = 0;
            if (gunSkillLevel == 99) {
                skillDamage = 20;
            } else if (gunSkillLevel >= 90) {
                skillDamage = 10;
            }

            return (skillDamage + weaponPower * 2 + attackPowerIncrease) * charge;
        }

        #endregion
    }
}

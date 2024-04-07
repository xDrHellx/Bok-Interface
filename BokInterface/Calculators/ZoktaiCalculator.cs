namespace BokInterface.Tools.Calculator {
    /// <summary>Class for calculations related to Boktai 2</summary>
    partial class ZoktaiCalculator {

        /// <summary>Get the difference in attack and defense</summary>
        /// <param name="attack">Attack</param>
        /// <param name="defense">Defense</param>
        /// <returns><c>Float</c>Difference (minimum is always 1)</returns>
        public static float GetAttackDefenseDifference(float attack, float defense) {
            float difference = attack - defense;
            return difference <= 0 ? 1 : difference;
        }

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

        #region Defense power

        /// <summary>Get the player's defense power</summary>
        /// <remarks>Corresponds to the "DFND" value on the status screen</remarks>
        /// <param name="level">Player level</param>
        /// <param name="agility">AGI stat points</param>
        /// <param name="protectorDurability">Protector durability (defense from armor)</param>
        /// <returns><c>Float</c>Defense power</returns>
        public static float GetPlayerDefensePower(int level, int agility, int protectorDurability) {
            return (level + agility) / 2 + protectorDurability;
        }

        #endregion

        #region Enchantments & effects

        /// <summary>Get the enchantment multiplier related to elemental weaknesses</summary>
        /// <param name="matchup">Result of matchup (neutral, resisted, effective, or not prevent) (by default not present)</param>
        /// <returns><c>Float</c>Multiplier</returns>
        public static float GetEnchantmentMultiplier(string matchup = "") {
            return (object)matchup switch {
                "neutral" => (float)1.25,
                "resisted" => (float)0.25,
                "effective" => 4,
                _ => 1,
            };
        }

        /// <summary>Get the multiplier for hitting the back of an enemy of using Enchant Dark at night</summary>
        /// <remarks>
        ///     <para>If performing both, the multiplier is only applied once.</para>
        ///     <para>
        ///         The Player can be hit in the back, as can some enemies.<br/>
        ///         If an enemy is frozen or knocked out, it cannot be hit in the back.
        ///     </para>
        /// </remarks>
        /// <param name="effective">True if hitting the back of an enemy of using Enchant Dark at night</param>
        /// <returns><c>Float</c>Multiplier</returns>
        public static float GetNightOrBackMultiplier(bool effective = false) {
            return (float)(effective == true ? 1.5 : 1);
        }

        /// <summary>Get the knock-out multiplier</summary>
        /// <remarks>
        ///     <para>Certain enemies can be knocked out (immobile, stars circling above), by typical gameplay, such as Golems colliding, or Cockatrices petrifying each other.</para>
        ///     <para>Many other normal enemies can also be knocked out by the SP effect “Randomly paralyzes enemies”.</para>
        ///     <para>Bosses and mini bosses cannot be knocked out.</para>
        ///     <para>The Player will incur this multiplier if they are hit while using Dash.</para>
        /// </remarks>
        /// <param name="knockedOut">True if knocked out</param>
        /// <returns><c>Int</c>Multiplier</returns>
        public static int GetKnockedOutMultiplier(bool knockedOut = false) {
            return knockedOut == true ? 2 : 1;
        }

        #endregion
    }
}

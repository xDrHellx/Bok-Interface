using System.Collections.Generic;

namespace BokInterface.Abilities {
    /// <summary>Class for Zoktai abilities (effect) instances and lists</summary>
    class ZoktaiAbilities {

        public Dictionary<string, Ability> Weapons = [],
            Armors = [];

        public ZoktaiAbilities() {
            InitWeaponAbilitiesList();
            InitArmorAbilitiesList();
        }

        ///<summary>Init ability instances for weapon effects</summary>
        private void InitWeaponAbilitiesList() {
            Weapons.Add("No ability", new Ability("No ability", 0, ""));
            Weapons.Add("Damage increases based on solar gauge", new Ability("Damage increases based on solar gauge", 1, "Increases attack power based on sunlight"));
            Weapons.Add("+10 damage at night", new Ability("+10 damage at night", 2, "Increases attack power at night"));
            Weapons.Add("Lower HP = higher damage", new Ability("Lower HP = higher damage", 3, "Increases attack power as Life diminishes"));
            Weapons.Add("Lower HP = lower damage", new Ability("Lower HP = lower damage", 4, "Decreases attack power as Life diminishes"));
            Weapons.Add("Use Solar Station for enchants", new Ability("Use Solar Station for enchants", 5, "Uses Solar Station energy directly for Enchantment Attacks"));
            // Weapons.Add("Use Solar Station for enchants", new Ability("Use Solar Station for enchants", 6, "Uses Solar Station energy directly for Enchantment Attacks"));
            // Weapons.Add("Use Solar Station for enchants", new Ability("Use Solar Station for enchants", 7, "Uses Solar Station energy directly for Enchantment Attacks"));
            Weapons.Add("Damage increases based on VIT ÷ 8", new Ability("Damage increases based on VIT ÷ 8", 8, "Increases attack power based on Vitality"));
            Weapons.Add("Damage increases based on SPR ÷ 8", new Ability("Damage increases based on SPR ÷ 8", 9, "Increases attack power based on Spirit"));
            Weapons.Add("Damage increases based on AGI ÷ 8", new Ability("Damage increases based on AGI ÷ 8", 10, "Increases attack power based on Agility"));
            Weapons.Add("Higher HP = higher damage", new Ability("Higher HP = higher damage", 11, "Increases attack power based on Life"));
            Weapons.Add("Higher ENE = higher damage", new Ability("Higher ENE = higher damage", 12, "Increases attack power based on Energy"));
            Weapons.Add("Damage increases while under a status effect", new Ability("Damage increases while under a status effect", 13, "Increases attack power during abnormal status"));
            // Weapons.Add("Damage increases based on solar gauge", new Ability("Damage increases based on solar gauge", 14, "Increases attack power based on sunlight"));
            // Weapons.Add("+10 damage at night", new Ability("+10 damage at night", 15, "Increases attack power at night"));
            Weapons.Add("10% chance of doing +10 damage", new Ability("10% chance of doing +10 damage", 16, "Randomly increases damage"));
            Weapons.Add("Up to +10 damage based on amount of enemies killed", new Ability("Up to +10 damage based on amount of enemies killed", 17, "Increases damage depending on the number of enemies defeated"));
            Weapons.Add("+10 Flame elemental damage", new Ability("+10 Flame elemental damage", 18, "Increases damage with the Flame property"));
            Weapons.Add("+10 Frost elemental damage", new Ability("+10 Frost elemental damage", 19, "Increases damage with the Frost property"));
            Weapons.Add("+10 Cloud elemental damage", new Ability("+10 Cloud elemental damage", 20, "Increases damage with the Cloud property"));
            Weapons.Add("+10 Earth elemental damage", new Ability("+10 Earth elemental damage", 21, "Increases damage with the Earth property"));
            Weapons.Add("+10 damage to Beast type", new Ability("+10 damage to Beast type", 22, "Increases damage to beast monsters"));
            Weapons.Add("+10 damage to Material type", new Ability("+10 damage to Material type", 23, "Increases damage to material monsters"));
            Weapons.Add("+10 damage to Phantom type", new Ability("+10 damage to Phantom type", 24, "Increases damage to phantom monsters"));
            Weapons.Add("+10 damage to Undead type", new Ability("+10 damage to Undead type", 25, "Increases damage to Undead monsters"));
            Weapons.Add("+10 damage to Immortal type", new Ability("+10 damage to Immortal type", 26, "Increases damage to Immortals"));
            Weapons.Add("10% chance to bypass defense", new Ability("10% chance to bypass defense", 27, "Randomly nullifies defense power"));
            Weapons.Add("10% chance to paralyze", new Ability("10% chance to paralyze", 28, "Randomly paralyzes enemies"));
            Weapons.Add("Kill enemy to lose 1 HP (Django) or gain 1 HP (Black Django)", new Ability("Kill enemy to lose 1 HP (Django) or gain 1 HP (Black Django)", 29, "Defeat an enemy and..."));
            Weapons.Add("-20% enchants cost", new Ability("-20% enchants cost", 30, "Decreases magic cost for Enchantment Attacks"));
            Weapons.Add("Reduce durability lost when attacking by 50%", new Ability("Reduce durability lost when attacking by 50%", 31, "Reduces damage to Weapons"));
        }

        ///<summary>Init ability instances for armor effects</summary>
        private void InitArmorAbilitiesList() {
            // Armors.Add("No ability", new Ability("No ability", 0, ""));
            Armors.Add("name", new Ability("name", 1));
        }
    }
}
using System.Collections.Generic;

namespace BokInterface.Abilities {
    /// <summary>Class for Shinbok abilities (effect) instances and lists</summary>
    class ShinbokAbilities {

        public Dictionary<string, Ability> Weapons = [];

        public ShinbokAbilities() {
            InitWeaponAbilitiesList();
        }

        ///<summary>Init ability instances for weapon effects</summary>
        private void InitWeaponAbilitiesList() {
            // TODO : Check these more, the gamefaqs guide for these don't match with other sources
            Weapons.Add("No ability", new Ability("No ability", 0));
            Weapons.Add("Damage +5 per level", new Ability("Damage +5 per level", 1, "Attack Power is relative to your level")); // Exclusive to "La Vie En Rose"
            Weapons.Add("+25% damage", new Ability("+25% damage", 2, "25% Attack Power boost"));
            Weapons.Add("Attack Power is relative to your Life", new Ability("Attack Power is relative to your Life", 3));
            Weapons.Add("Enchant Sol (5 ENE)", new Ability("Enchant Sol (5 ENE)", 4, "Adds Sol element to attacks (5 ENE)"));
            Weapons.Add("Enchant Flame (10 ENE)", new Ability("Enchant Flame (10 ENE)", 5, "Adds Flame element to attacks (10 ENE)"));
            Weapons.Add("Enchant Frost (10 ENE)", new Ability("Enchant Frost (10 ENE)", 6, "Adds Frost element to attacks (10 ENE)"));
            Weapons.Add("Enchant Cloud (10 ENE)", new Ability("Enchant Cloud (10 ENE)", 7, "Adds Cloud element to attacks (10 ENE)"));
            Weapons.Add("Enchant Earth (10 ENE)", new Ability("Enchant Earth (10 ENE)", 8, "Adds Earth element to attacks (10 ENE)"));
            Weapons.Add("10% chance to knockback", new Ability("10% chance to knockback", 9, "Low chance of knockback effect"));
            Weapons.Add("20% chance to knockback", new Ability("20% chance to knockback", 10, "High chance of knockback effect"));
            Weapons.Add("10% chance to paralyze", new Ability("10% chance to paralyze", 11, "Low chance of paralyzing enemies"));
            Weapons.Add("20% chance to paralyze", new Ability("20% chance to paralyze", 12, "High chance of paralyzing enemies"));
            Weapons.Add("Kill enemy = recover 1% HP", new Ability("Kill enemy = recover 1% HP", 13, "Restores Life as you defeat enemies"));
            Weapons.Add("Kill enemy = lose 1% HP", new Ability("Kill enemy = lose 1% HP", 14, "Drains Life as you defeat enemies"));
            Weapons.Add("Kill enemy = recover 1% ENE", new Ability("Kill enemy = recover 1% ENE", 15, "Restores Energy as you defeat enemies"));
            Weapons.Add("Kill enemy = lose 1% ENE", new Ability("Kill enemy = lose 1% ENE", 16, "Drains Energy as you defeat enemies"));
            Weapons.Add("+100% damage", new Ability("200% damage", 17, "Power decreases as level increases")); // This ability is bugged in-game and never decreases attack power based on level, making the calculation always return +100%
            Weapons.Add("Lowers Dark resistance", new Ability("Lowers Dark resistance", 18, "Makes you weak to Dark")); // This ability is bugged and will stack until changing room, very dangerous
            Weapons.Add("Lowers Flame resistance", new Ability("Lowers Flame resistance", 19, "Makes you weak to Flame")); // This ability is bugged and will stack until changing room, very dangerous
            Weapons.Add("Lowers Frost resistance", new Ability("Lowers Frost resistance", 20, "Makes you weak to Frost")); // This ability is bugged and will stack until changing room, very dangerous
            Weapons.Add("Lowers Cloud resistance", new Ability("Lowers Cloud resistance", 21, "Makes you weak to Cloud")); // This ability is bugged and will stack until changing room, very dangerous
            Weapons.Add("Lowers Earth resistance", new Ability("Lowers Earth resistance", 22, "Makes you weak to Earth")); // This ability is bugged and will stack until changing room, very dangerous
        }
    }
}
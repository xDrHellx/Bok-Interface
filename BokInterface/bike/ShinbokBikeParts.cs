using System.Collections.Generic;
using System.Linq;

namespace BokInterface.Bike {
    /// <summary>Class for Shinbok bike parts instances and lists</summary>
    public class ShinbokBikeParts {

        public Dictionary<string, ShinbokBikePart> All = [],
            Front = [],
            Tires = [],
            Body = [],
            Special = [],
            Colors = [],
            Options = [];

        public ShinbokBikeParts() {
            InitFrontParts();
            InitTires();
            InitBodyParts();
            InitSpecialParts();
            InitColors();
            InitOptions();
            InitFullList();
        }

        protected void InitFrontParts() {
            string type = "front";
            Front.Add("Blaster 1", new ShinbokBikePart("Blaster 1", 0, type, "ATK: 20 | ENE: 40 | A single bullet is fired forward."));
            Front.Add("Sidewinder", new ShinbokBikePart("Sidewinder", 1, type, "ATK: 30 | ENE: 120 | Two bullets are fired, curve around the front of the bike to opposite sides, and head backwards."));
            Front.Add("Blaster 2", new ShinbokBikePart("Blaster 2", 2, type, "ATK: 40 | ENE: 80 | A more powerful bullet."));
            Front.Add("Hammerhead", new ShinbokBikePart("Hammerhead", 3, type, "ATK: 40 | ENE: 180 | A rocket is fired straight forward."));
            Front.Add("Fairy Tale", new ShinbokBikePart("Fairy Tale", 4, type, "ATK: 45 | ENE: 150 | Two rockets are fired: one forward, one backwards."));
            Front.Add("Crusher", new ShinbokBikePart("Crusher", 5, type, "ATK: 60 | ENE: 8 | A close-range weapon to drill the opponent/obstacle."));
        }

        protected void InitTires() {
            string type = "tires";
            Tires.Add("Standard 1", new ShinbokBikePart("Standard 1", 0, type, "STEERING: 32 | BRAKES: 58 | ENE/BOOST: 120"));
            Tires.Add("Standard 2", new ShinbokBikePart("Standard 2", 1, type, "STEERING: 34 | BRAKES: 116 | ENE/BOOST: 102"));
            Tires.Add("Trail", new ShinbokBikePart("Trail", 2, type, "STEERING: 38 | BRAKES: 87 | ENE/BOOST: 68 | Drive over dirt."));
            Tires.Add("Chain", new ShinbokBikePart("Chain", 3, type, "STEERING: 36 | BRAKES: 90 | ENE/BOOST: 68 | Drive over ice."));
            Tires.Add("Multi-purpose", new ShinbokBikePart("Multi-purpose", 4, type, "STEERING: 58 | BRAKES: 116 | ENE/BOOST: 88"));
            Tires.Add("Booster", new ShinbokBikePart("Booster", 5, type, "STEERING: 28 | BRAKES: 77 | ENE/BOOST: 44"));
        }

        protected void InitBodyParts() {
            string type = "body";
            Body.Add("Viking", new ShinbokBikePart("Viking", 0, type, "ACCEL: 46 | SPD: 92 | HP: 360 | OPTIONS: 1"));
            Body.Add("Einherjar", new ShinbokBikePart("Einherjar", 1, type, "ACCEL: 32 | SPD: 108 | HP: 330 | OPTIONS: 2"));
            Body.Add("Ulfhedinn", new ShinbokBikePart("Ulfhedinn", 2, type, "ACCEL: 84 | SPD: 104 | HP: 290 | OPTIONS: 3"));
            Body.Add("Berserk", new ShinbokBikePart("Berserk", 3, type, "ACCEL: 22 | SPD: 96 | HP: 630 | OPTIONS: 4"));
            Body.Add("Valkyrie", new ShinbokBikePart("Valkyrie", 4, type, "ACCEL: 18 | SPD: 126 | HP: 240 | OPTIONS: 3"));
            Body.Add("Sleipnir", new ShinbokBikePart("Sleipnir", 5, type, "ACCEL: 62 | SPD: 114 | HP: 420 | OPTIONS: 2"));
        }

        protected void InitSpecialParts() {
            string type = "special";
            Special.Add("Repair", new ShinbokBikePart("Repair", 0, type, "ATK: 0 | ENE: 4 | Refills HP."));
            Special.Add("Mine", new ShinbokBikePart("Mine", 1, type, "ATK: 80 | ENE: 200 | Drops a mine behind."));
            Special.Add("Barrier", new ShinbokBikePart("Barrier", 2, type, "ATK: 0 | ENE: 320 | Invulnerability."));
            Special.Add("Spread", new ShinbokBikePart("Spread", 3, type, "ATK: 20 | ENE: 200 | Ramming shield."));
            Special.Add("Stealth", new ShinbokBikePart("Stealth", 4, type, "ATK: 0 | ENE: 300 | Invisibility."));
            Special.Add("Missiles", new ShinbokBikePart("Missiles", 5, type, "ATK: 120 | ENE: 900 | Launches a delayed ballistic missile."));
        }

        protected void InitColors() {
            string type = "color";
            Colors.Add("Coffin Gray", new ShinbokBikePart("Coffin Gray", 0, type));
            Colors.Add("Sol Yellow", new ShinbokBikePart("Sol Yellow", 1, type));
            Colors.Add("Luna White", new ShinbokBikePart("Luna White", 2, type));
            Colors.Add("Flame Red", new ShinbokBikePart("Flame Red", 3, type));
            Colors.Add("Frost Blue", new ShinbokBikePart("Frost Blue", 4, type));
            Colors.Add("Cloud Purple", new ShinbokBikePart("Cloud Purple", 5, type));
            Colors.Add("Earth Green", new ShinbokBikePart("Earth Green", 6, type));
            Colors.Add("Dark Black", new ShinbokBikePart("Dark Black", 7, type));
            Colors.Add("Django Red", new ShinbokBikePart("Django Red", 8, type));
            Colors.Add("Sabata Black", new ShinbokBikePart("Sabata Black", 9, type));
            Colors.Add("Trinity SP", new ShinbokBikePart("Trinity SP", 10, type));
            Colors.Add("Rock Blue", new ShinbokBikePart("Rock Blue", 11, type));
        }

        protected void InitOptions() {
            string type = "option";
            Options.Add("No option", new ShinbokBikePart("No option", 65535, type));
            Options.Add("Magic Handle", new ShinbokBikePart("Magic Handle", 0, type, "ATK & SPECIAL ATK +20"));
            Options.Add("Magic Step", new ShinbokBikePart("Magic Step", 1, type, "ATK & SPECIAL ATK +40"));
            Options.Add("Rock Emblem", new ShinbokBikePart("Rock Emblem", 2, type, "ATK & SPECIAL ATK +70"));
            Options.Add("Platinum Plug", new ShinbokBikePart("Platinum Plug", 3, type, "ACCEL +60%"));
            Options.Add("High Grade Oil", new ShinbokBikePart("High Grade Oil", 4, type, "ACCEL +120%"));
            Options.Add("Hyper Charger", new ShinbokBikePart("Hyper Charger", 5, type, "SPD +10%"));
            Options.Add("Blues Chain", new ShinbokBikePart("Blues Chain", 6, type, "SPD +10%"));
            Options.Add("Colonel Gear", new ShinbokBikePart("Colonel Gear", 7, type, "SPD +10%"));
            Options.Add("Reinforced Arm", new ShinbokBikePart("Reinforced Armor", 8, type, "HP +200"));
            Options.Add("Reinforced Frame", new ShinbokBikePart("Reinforced Frame", 9, type, "HP +300"));
            Options.Add("Knuckle Guard", new ShinbokBikePart("Knuckle Guard", 10, type, "DEFENSE +1"));
            Options.Add("Protector", new ShinbokBikePart("Protector", 11, type, "DEFENSE +2"));
            Options.Add("BST Battery", new ShinbokBikePart("BST Battery", 12, type, "BOOST COST -20%"));
            Options.Add("WPN Battery", new ShinbokBikePart("WPN Battery", 13, type, "WEAPON COST -60%"));
            Options.Add("SP Battery", new ShinbokBikePart("SP Battery", 14, type, "SPECIAL COST -60%"));
            Options.Add("Clutch Master", new ShinbokBikePart("Clutch Master", 15, type, "BRAKES EFFICIENCY +70%"));
            Options.Add("Brake Master", new ShinbokBikePart("Brake Master", 16, type, "BRAKES EFFICIENCY +100%"));
            Options.Add("Kid's Afro", new ShinbokBikePart("Kid's Afro", 17, type, "FASTER ENE RECOVERY"));
            Options.Add("Smith's Hammer", new ShinbokBikePart("Smith's Hammer", 18, type, "BIGGER POTIONS EFFECT"));
            Options.Add("Violet's Apron", new ShinbokBikePart("Violet's Apron", 19, type, "HIGHER DEFENSE WHEN HP < 50%"));
            Options.Add("Lady's Card", new ShinbokBikePart("Lady's Card", 20, type, "-50% ENE COSTS WHEN HP < 50%"));
            Options.Add("Cheyenne's Axe", new ShinbokBikePart("Cheyenne's Axe", 21, type, "SPEED INCREASE WHEN BEHIND RIVAL"));
            Options.Add("Lita's Ribbon", new ShinbokBikePart("Lita's Ribbon", 22, type, "FASTER ENE RECOVERY WHEN BEHIND RIVAL"));
            Options.Add("Zazie's Orb", new ShinbokBikePart("Zazie's Orb", 23, type, "FASTER ENE RECOVERY WHEN BEHIND RIVAL"));
        }

        ///<summary>Init the full list containing all bike parts (mostly used for editors)</summary>
        private void InitFullList() {
            All.Add("Empty slot", new ShinbokBikePart("Empty slot", 0, ""));
            All = All
                .Concat(Front)
                .Concat(Tires)
                .Concat(Body)
                .Concat(Special)
                .Concat(Colors)
                .Concat(Options)
                .ToDictionary(e => e.Key, e => e.Value);
        }
    }
}

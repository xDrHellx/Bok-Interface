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
            Front.Add("Blaster 1", new ShinbokBikePart("Blaster 1", 0, "front"));
            Front.Add("Sidewinder", new ShinbokBikePart("Sidewinder", 1, "front"));
            Front.Add("Blaster 2", new ShinbokBikePart("Blaster 2", 2, "front"));
            Front.Add("Hammerhead", new ShinbokBikePart("Hammerhead", 3, "front"));
            Front.Add("Fairy Tale", new ShinbokBikePart("Fairy Tale", 4, "front"));
            Front.Add("Crusher", new ShinbokBikePart("Crusher", 5, "front"));
        }

        protected void InitTires() {
            Tires.Add("Standard 1", new ShinbokBikePart("Standard 1", 0, "tires"));
            Tires.Add("Standard 2", new ShinbokBikePart("Standard 2", 1, "tires"));
            Tires.Add("Trail", new ShinbokBikePart("Trail", 2, "tires"));
            Tires.Add("Chain", new ShinbokBikePart("Chain", 3, "tires"));
            Tires.Add("Multi-purpose", new ShinbokBikePart("Multi-purpose", 4, "tires"));
            Tires.Add("Booster", new ShinbokBikePart("Booster", 5, "tires"));
        }

        protected void InitBodyParts() {
            Body.Add("Viking", new ShinbokBikePart("Viking", 0, "body"));
            Body.Add("Einherjar", new ShinbokBikePart("Einherjar", 1, "body"));
            Body.Add("Ulfhedinn", new ShinbokBikePart("Ulfhedinn", 2, "body"));
            Body.Add("Berserk", new ShinbokBikePart("Berserk", 3, "body"));
            Body.Add("Valkyrie", new ShinbokBikePart("Valkyrie", 4, "body"));
            Body.Add("Sleipnir", new ShinbokBikePart("Sleipnir", 5, "body"));
        }

        protected void InitSpecialParts() {
            Special.Add("Repair", new ShinbokBikePart("Repair", 0, "special"));
            Special.Add("Mine", new ShinbokBikePart("Mine", 1, "special"));
            Special.Add("Barrier", new ShinbokBikePart("Barrier", 2, "special"));
            Special.Add("Spread", new ShinbokBikePart("Spread", 3, "special"));
            Special.Add("Stealth", new ShinbokBikePart("Stealth", 4, "special"));
            Special.Add("Missiles", new ShinbokBikePart("Missiles", 5, "special"));
        }

        protected void InitColors() {
            Colors.Add("Coffin Gray", new ShinbokBikePart("Coffin Gray", 0, "color"));
            Colors.Add("Sol Yellow", new ShinbokBikePart("Sol Yellow", 1, "color"));
            Colors.Add("Luna White", new ShinbokBikePart("Luna White", 2, "color"));
            Colors.Add("Flame Red", new ShinbokBikePart("Flame Red", 3, "color"));
            Colors.Add("Frost Blue", new ShinbokBikePart("Frost Blue", 4, "color"));
            Colors.Add("Cloud Purple", new ShinbokBikePart("Cloud Purple", 5, "color"));
            Colors.Add("Earth Green", new ShinbokBikePart("Earth Green", 6, "color"));
            Colors.Add("Dark Black", new ShinbokBikePart("Dark Black", 7, "color"));
            Colors.Add("Django Red", new ShinbokBikePart("Django Red", 8, "color"));
            Colors.Add("Sabata Black", new ShinbokBikePart("Sabata Black", 9, "color"));
            Colors.Add("Trinity SP", new ShinbokBikePart("Trinity SP", 10, "color"));
            Colors.Add("Rock Blue", new ShinbokBikePart("Rock Blue", 11, "color"));
        }

        protected void InitOptions() {
            Options.Add("Magic Handle", new ShinbokBikePart("Magic Handle", 0, "option"));
            Options.Add("Magic Step", new ShinbokBikePart("Magic Step", 1, "option"));
            Options.Add("Rock Emblem", new ShinbokBikePart("Rock Emblem", 2, "option"));
            Options.Add("Platinum Plug", new ShinbokBikePart("Platinum Plug", 3, "option"));
            Options.Add("High Grade Oil", new ShinbokBikePart("High Grade Oil", 4, "option"));
            Options.Add("Hyper Charger", new ShinbokBikePart("Hyper Charger", 5, "option"));
            Options.Add("Blues Chain", new ShinbokBikePart("Blues Chain", 6, "option"));
            Options.Add("Colonel Gear", new ShinbokBikePart("Colonel Gear", 7, "option"));
            Options.Add("Reinforced Arm", new ShinbokBikePart("Reinforced Arm", 8, "option"));
            Options.Add("Reinforced Frame", new ShinbokBikePart("Reinforced Frame", 9, "option"));
            Options.Add("Knuckle Guard", new ShinbokBikePart("Knuckle Guard", 10, "option"));
            Options.Add("Protector", new ShinbokBikePart("Protector", 11, "option"));
            Options.Add("BST Battery", new ShinbokBikePart("BST Battery", 12, "option"));
            Options.Add("WPN Battery", new ShinbokBikePart("WPN Battery", 13, "option"));
            Options.Add("SP Battery", new ShinbokBikePart("SP Battery", 14, "option"));
            Options.Add("Clutch Master", new ShinbokBikePart("Clutch Master", 15, "option"));
            Options.Add("Brake Master", new ShinbokBikePart("Brake Master", 16, "option"));
            Options.Add("Kid's Afro", new ShinbokBikePart("Kid's Afro", 17, "option"));
            Options.Add("Smith's Hammer", new ShinbokBikePart("Smith's Hammer", 18, "option"));
            Options.Add("Violet's Apron", new ShinbokBikePart("Violet's Apron", 19, "option"));
            Options.Add("Lady's Card", new ShinbokBikePart("Lady's Card", 20, "option"));
            Options.Add("Cheyenne's Axe", new ShinbokBikePart("Cheyenne's Axe", 21, "option"));
            Options.Add("Lita's Ribbon", new ShinbokBikePart("Lita's Ribbon", 22, "option"));
            Options.Add("Zazie's Orb", new ShinbokBikePart("Zazie's Orb", 23, "option"));
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
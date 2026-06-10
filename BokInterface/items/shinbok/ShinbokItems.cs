using System.Collections.Generic;

namespace BokInterface.Items {
    /// <summary>Class for Shinbok item instances and lists</summary>
    class ShinbokItems {

        public Dictionary<string, Item> Items = [],
            KeyItems = [];

        public ShinbokItems() {
            InitItems();
            InitKeyItems();
        }

        ///<summary>Init items instances</summary>
        private void InitItems() {
            Items.Add("Empty slot", new ShinbokItem("Empty slot", 65535));
            Items.Add("Healer", new ShinbokItem("Healer", 0, "healer", "Recover 20% Life"));
            Items.Add("Earthly Nut", new ShinbokItem("Earthly Nut", 1, "earthly_nut", "Recover 40% Life", true));
            Items.Add("Rotten Nut", new ShinbokItem("Rotten Nut", 2, "rotten_nut", "Recover 5% & give poison status"));
            Items.Add("Jerky", new ShinbokItem("Jerky", 3, "jerky", "Recover 40% Life"));
            Items.Add("Tasty Meat", new ShinbokItem("Tasty Meat", 4, "tasty_meat", "Recover 60% Life", true));
            Items.Add("Rotten Meat", new ShinbokItem("Rotten Meat", 5, "rotten_meat", "Recover 5% Life & give poison status"));
            Items.Add("Chocolate", new ShinbokItem("Chocolate", 6, "chocolate", "Recover 20% Life", true));
            Items.Add("Deluxe Chocolate", new ShinbokItem("Deluxe Chocolate", 7, "deluxe_chocolate", "Recover 40% Life"));
            Items.Add("Melted Chocolate", new ShinbokItem("Melted Chocolate", 8, "melted_chocolate", "Recover 20% Life"));
            Items.Add("Chocolate-Covered", new ShinbokItem("Chocolate-Covered", 9, "chocolate_covered", "Recover 20% Life & reveal covered item"));
            Items.Add("GariGari Soda", new ShinbokItem("GariGari Soda", 10, "garigari_soda", "Recover 20% Life & randomly turn into Loser Stick or Winner Stick", true));
            Items.Add("GariGari Cola", new ShinbokItem("GariGari Cola", 11, "garigari_cola", "Recover 100% Life & randomly turn into Loser Stick or Winner Stick", true));
            Items.Add("Magical Potion", new ShinbokItem("Magical Potion", 12, "magical_potion", "Recover 20% ENE"));
            Items.Add("Solar Nut", new ShinbokItem("Solar Nut", 13, "solar_nut", "Recover 40% ENE", true));
            Items.Add("Bad Mushroom", new ShinbokItem("Bad Mushroom", 14, "bad_mushroom", "Recover 5% ENE & give confusion status"));
            Items.Add("Antidote", new ShinbokItem("Antidote", 15, "antidote", "Cure poison status"));
            Items.Add("Elixir", new ShinbokItem("Elixir", 16, "elixir", "Cure poison & confusion status"));
            Items.Add("Drop of Sun", new ShinbokItem("Drop of Sun", 17, "drop_of_sun", "Cure Kaamos status"));
            Items.Add("Bearnut", new ShinbokItem("Bearnut", 18, "bearnut", "Nullify damage taken for 15s", true));
            Items.Add("Speed Nut", new ShinbokItem("Speed Nut", 19, "speed_nut", "Run faster for 30s (slower than Dash)", true));
            Items.Add("Banana", new ShinbokItem("Banana", 20, "banana", "Push blocks faster for 30s", true));
            Items.Add("Chocolate Banana", new ShinbokItem("Chocolate Banana", 21, "chocolate_banana", "Push blocks faster for 60s"));
            Items.Add("Tiptoe Nut", new ShinbokItem("Tiptoe Nut", 22, "tiptoe_nut", "Steps don't make noise for 30s", true));
            Items.Add("Redshroom", new ShinbokItem("Redshroom", 23, "redshroom", "Become small for 15s", true));
            Items.Add("Blueshroom", new ShinbokItem("Blueshroom", 24, "blueshroom", "Become invisible for 15s", true));
            Items.Add("See-All Nut", new ShinbokItem("See-All Nut", 25, "see_all_nut", "See traps & invisible entities for 30s", true));
            Items.Add("Warp Leaf", new ShinbokItem("Warp Leaf", 26, "warp_leaf", "Warp outside of current dungeon"));
            Items.Add("Mr. Rainnot", new ShinbokItem("Mr. Rainnot", 27, "mr_rainnot", "Increment Solar Gauge by 2 for 24h (cannot exceed max value)"));
            Items.Add("tonniaR .rM", new ShinbokItem("tonniaR .rM", 28, "tonniar_rm", "Restrict Solar Gauge to 1 for 24h"));
            Items.Add("Sunny Clog", new ShinbokItem("Sunny Clog", 29, "sunny_clog", "Randomly trigger Mr. Rainnot or Tonniar .RM effect (50%-50%)"));
            Items.Add("Loser Stick", new ShinbokItem("Loser Stick", 30, "loser_stick"));
            Items.Add("Winner Stick", new ShinbokItem("Winner Stick", 31, "winner_stick", "Give to GariGari to obtain a GariGari Cola"));
            Items.Add("The High Priestess", new ShinbokItem("The High Priestess", 32, "red_card", "SPR +2"));
            Items.Add("The Empress", new ShinbokItem("The Empress", 33, "red_card", "VIT +2"));
            Items.Add("Strength", new ShinbokItem("Strength", 34, "red_card", "STR +2"));
            Items.Add("Wheel of Fortune", new ShinbokItem("Wheel of Fortune", 35, "red_card", "+4 to a random stat"));
            Items.Add("The Moon", new ShinbokItem("The Moon", 36, "green_card", "Fully restore Life"));
            Items.Add("The Sun", new ShinbokItem("The Sun", 37, "green_card", "Fully restore ENE"));
            Items.Add("Judgement", new ShinbokItem("Judgement", 38, "green_card", "Revive with 100% Life upon death"));
        }

        ///<summary>Init key items instances</summary>
        private void InitKeyItems() {
            KeyItems.Add("Empty slot", new ShinbokItem("Empty slot", 65535));
            KeyItems.Add("Dark Card", new ShinbokItem("Dark Card", 39, "dark_card", "Dark Loan"));
            KeyItems.Add("Master Otenko", new ShinbokItem("Master Otenko", 40, effect: "Unused item"));
            KeyItems.Add("Sword License", new ShinbokItem("Sword License", 41, "sword_license", "Equip any Sword regardless of Level"));
            KeyItems.Add("PET", new ShinbokItem("PET", 42, "pet", "Enable CrossOver shop via RockMan doll"));
            KeyItems.Add("Vector Coffin", new ShinbokItem("Vector Coffin", 43, "vector_coffin"));
            KeyItems.Add("Circle Key", new ShinbokItem("Circle Key", 44, "circle_key"));
            KeyItems.Add("Triangle Key", new ShinbokItem("Triangle Key", 45, "triangle_key"));
            KeyItems.Add("Square Key", new ShinbokItem("Square Key", 46, "square_key"));
            KeyItems.Add("Cross Key", new ShinbokItem("Cross Key", 47, "cross_key"));
            KeyItems.Add("Pile Parts", new ShinbokItem("Pile Parts", 48, "pile_parts", "Can be used to unlock Pile Trap magic"));
            KeyItems.Add("Dungeon Item 2", new ShinbokItem("Dungeon Item 2", 49, effect: "Unused item"));
            KeyItems.Add("Fate Goddess Statue", new ShinbokItem("Fate Goddess Statue", 50, "fate_goddess_statue"));
            KeyItems.Add("Existence Goddess Statue", new ShinbokItem("Existence Goddess Statue", 51, "existence_goddess_statue"));
            KeyItems.Add("Necessity Goddess Statue", new ShinbokItem("Necessity Goddess Statue", 52, "necessity_goddess_statue"));
            KeyItems.Add("Mission Item 4", new ShinbokItem("Mission Item 4", 53, effect: "Unused item"));
            KeyItems.Add("Mission Item 5", new ShinbokItem("Mission Item 5", 54, effect: "Unused item"));
        }
    }
}

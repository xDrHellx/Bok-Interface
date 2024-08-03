using System.Collections.Generic;

namespace BokInterface.Items {
    /// <summary>Class for Shinbok item instances and lists</summary>
    class ShinbokItems {

        public Dictionary<string, Item> Items = [];
        public Dictionary<string, Item> KeyItems = [];

        public ShinbokItems() {
            InitItems();
            InitKeyItems();
        }

        ///<summary>Init items instances</summary>
        private void InitItems() {
            // Items.Add("Empty slot", new ShinbokItem("Empty Slot", 65535));
            Items.Add("Healer", new ShinbokItem("Healer", 0, "healer"));
            Items.Add("Earthly Nut", new ShinbokItem("Earth Nut", 1, "earthly_nut"));
            Items.Add("Rotten Nut", new ShinbokItem("Rotten Nut", 2, "rotten_nut"));
            Items.Add("Jerky", new ShinbokItem("Jerky", 3, "jerky"));
            Items.Add("Tasty Meat", new ShinbokItem("Tasty Meat", 4, "tasty_meat"));
            Items.Add("Rotten Meat", new ShinbokItem("Rotten Meat", 5, "rotten_meat"));
            Items.Add("Chocolate", new ShinbokItem("Chocolate", 6, "chocolate"));
            Items.Add("Deluxe Chocolate", new ShinbokItem("Deluxe Chocolate", 7, "deluxe_chocolate"));
            Items.Add("Melted Chocolate", new ShinbokItem("Melted Chocolate", 8, "melted_chocolate"));
            Items.Add("Chocolate-Covered", new ShinbokItem("Chocolate-Covered", 9, "chocolate_covered"));
            Items.Add("GariGari Soda", new ShinbokItem("GariGari Soda", 10, "garigari_soda"));
            Items.Add("GariGari Cola", new ShinbokItem("GariGari Cola", 11, "garigari_cola"));
            Items.Add("Magical Potion", new ShinbokItem("Magical Potion", 12, "magical_potion"));
            Items.Add("Solar Nut", new ShinbokItem("Solar Nut", 13, "solar_nut"));
            Items.Add("Bad Mushroom", new ShinbokItem("Bad Mushroom", 14, "bad_mushroom"));
            Items.Add("Antidote", new ShinbokItem("Antidote", 15, "antidote"));
            Items.Add("Elixir", new ShinbokItem("Elixir", 16, "elixir"));
            Items.Add("Drop of Sun", new ShinbokItem("Drop of Sun", 17, "drop_of_sun"));
            Items.Add("Bearnut", new ShinbokItem("Bearnut", 18, "bearnut"));
            Items.Add("Speed Nut", new ShinbokItem("Speed Nut", 19, "speed_nut"));
            Items.Add("Banana", new ShinbokItem("Banana", 20, "banana"));
            Items.Add("Chocolate Banana", new ShinbokItem("Chocolate Banana", 20, "chocolate_banana"));
            Items.Add("Tiptoe Nut", new ShinbokItem("Tiptoe Nut", 21, "tiptoe_nut"));
            Items.Add("Redshroom", new ShinbokItem("Redshroom", 22, "redshroom"));
            Items.Add("Blueshroom", new ShinbokItem("Blueshroom", 23, "blueshroom"));
            Items.Add("See-All Nut", new ShinbokItem("See-All Nut", 24, "see_all_nut"));
            Items.Add("Warp Leaf", new ShinbokItem("Warp Leaf", 25, "warp_leaf"));
            Items.Add("Mr. Rainnot", new ShinbokItem("Mr. Rainnot", 26, "mr_rainnot"));
            Items.Add("tonniaR .rM", new ShinbokItem("tonniaR .rM", 27, "tonniar_rm"));
            Items.Add("Sunny Clog", new ShinbokItem("Sunny Clog", 28, "sunny_clog"));
            Items.Add("Loser Stick", new ShinbokItem("Loser Stick", 29, "loser_stick"));
            Items.Add("Winner Stick", new ShinbokItem("Winner Stick", 30, "winner_stick"));
            Items.Add("The High Priestess", new ShinbokItem("The High Priestess", 31, "the_high_priestess"));
            Items.Add("The Empress", new ShinbokItem("The Empress", 32, "the_empress"));
            Items.Add("Strength", new ShinbokItem("Strength", 33, "strength"));
            Items.Add("Wheel of Fortune", new ShinbokItem("Wheel of Fortune", 34, "wheel_of_fortune"));
            Items.Add("The Moon", new ShinbokItem("The Moon", 35, "the_moon"));
            Items.Add("The Sun", new ShinbokItem("The Sun", 36, "the_sun"));
            Items.Add("Judgement", new ShinbokItem("Judgement", 37, "judgement"));
        }

        ///<summary>Init key items instances</summary>
        private void InitKeyItems() {
            // KeyItems.Add("Empty slot", new ShinbokItem("Empty Slot", 65535));
            KeyItems.Add("Dark Card", new ShinbokItem("Dark Card", 38, "dark_card"));
            // KeyItems.Add("Master Otenko", new ShinbokItem("Master Otenko", 39)); // Unused
            KeyItems.Add("Sword License", new ShinbokItem("Sword License", 40, "sword_license"));
            KeyItems.Add("PET", new ShinbokItem("PET", 41, "pet"));
            KeyItems.Add("Vector Coffin", new ShinbokItem("Vector Coffin", 42, "vector_coffin"));
            KeyItems.Add("Red Key", new ShinbokItem("Red Key", 43, "red_key"));
            KeyItems.Add("Yellow Key", new ShinbokItem("Yellow Key", 44, "yellow_key"));
            KeyItems.Add("Green Key", new ShinbokItem("Green Key", 45, "green_key"));
            KeyItems.Add("Blue Key", new ShinbokItem("Blue Key", 46)); // Unused
            KeyItems.Add("Pile Parts", new ShinbokItem("Pile Parts", 47, "pile_parts"));
            // KeyItems.Add("Dungeon Item 2", new ShinbokItem("Dungeon Item 2", 48)); // Unused
            KeyItems.Add("Fate Goddess Statue", new ShinbokItem("Fate Goddess Statue", 49, "fate_goddess_statue"));
            KeyItems.Add("Existence Goddess Statue", new ShinbokItem("Existence Goddess Statue", 50, "existence_goddess_statue"));
            KeyItems.Add("Necessity Goddess Statue", new ShinbokItem("Necessity Goddess Statue", 51, "necessity_goddess_statue"));
            // KeyItems.Add("Mission Item 4", new ShinbokItem("Mission Item 4", 52)); // Unused
            // KeyItems.Add("Mission Item 5", new ShinbokItem("Mission Item 5", 53)); // Unused
        }
    }
}
using System.Collections.Generic;

namespace BokInterface.Items {
    /// <summary>Class for Zoktai item instances and lists</summary>
    class ZoktaiItems {

        public Dictionary<string, Item> Items = [];
        public Dictionary<string, Item> KeyItems = [];

        public ZoktaiItems() {
            InitItems();
            InitKeyItems();
        }

        ///<summary>Init items instances</summary>
        private void InitItems() {
            Items.Add("Empty slot", new ZoktaiItem("Empty Slot", 65535));
            Items.Add("Earthly Nut", new ZoktaiItem("Earthly Nut", 0, "earthly_nut", true));
            Items.Add("Solar Nut", new ZoktaiItem("Solar Nut", 1, "solar_nut", true));
            Items.Add("Speed Nut", new ZoktaiItem("Speed Nut", 2, "speed_nut", true));
            Items.Add("Tiptoe Nut", new ZoktaiItem("Tiptoe Nut", 3, "tiptoe_nut", true));
            Items.Add("Banana", new ZoktaiItem("Banana", 4, "banana", true));
            Items.Add("Chocolate Banana", new ZoktaiItem("Chocolate Banana", 5, "chocolate_banana"));
            Items.Add("Bearnut", new ZoktaiItem("Bearnut", 6, "bearnut", true));
            Items.Add("See-All Nut", new ZoktaiItem("See-All Nut", 7, "see_all_nut", true));
            Items.Add("Rotten Nut", new ZoktaiItem("Rotten Nut", 8, "rotten_nut"));
            Items.Add("Redshroom", new ZoktaiItem("Redshroom", 9, "redshroom", true));
            Items.Add("Blueshroom", new ZoktaiItem("Blueshroom", 10, "blueshroom", true));
            Items.Add("Bad Mushroom", new ZoktaiItem("Bad Mushroom", 11, "bad_mushroom"));
            Items.Add("Drop Of Sun", new ZoktaiItem("Drop Of Sun", 12, "drop_of_sun", true));
            Items.Add("Tomato Juice", new ZoktaiItem("Tomato Juice", 13, "tomato_juice", true));
            Items.Add("Rotten Water", new ZoktaiItem("Rotten Water", 14, "rotten_water"));
            Items.Add("Tasty Meat", new ZoktaiItem("Tasty Meat", 15, "tasty_meat", true));
            Items.Add("Jerky", new ZoktaiItem("Jerky", 16, "jerky"));
            Items.Add("Rotten Meat", new ZoktaiItem("Rotten Meat", 17, "rotten_meat"));
            Items.Add("Chocolate", new ZoktaiItem("Chocolate", 18, "chocolate", true));
            Items.Add("Melted Chocolate", new ZoktaiItem("Melted Chocolate", 19, "melted_chocolate"));
            Items.Add("Chocolate-Covered", new ZoktaiItem("Chocolate-Covered", 20, "chocolate_covered"));
            Items.Add("Deluxe Chocolate", new ZoktaiItem("Deluxe Chocolate", 21, "deluxe_chocolate"));
            Items.Add("Healer", new ZoktaiItem("Healer", 22, "healer"));
            Items.Add("Magical Potion", new ZoktaiItem("Magical Potion", 23, "magical_potion"));
            Items.Add("Antidote", new ZoktaiItem("Antidote", 24, "antidote"));
            Items.Add("Elixir", new ZoktaiItem("Elixir", 25, "elixir"));
            Items.Add("Sunblock", new ZoktaiItem("Sunblock", 26, "sunblock"));
            Items.Add("Mr Rainnot", new ZoktaiItem("Mr Rainnot", 27, "mr_rainnot"));
            Items.Add("Tonniar Rm", new ZoktaiItem("Tonniar Rm", 28, "tonniar_rm"));
            Items.Add("Sunny Clog", new ZoktaiItem("Sunny Clog", 29, "sunny_clog"));
            Items.Add("Warp Leaf", new ZoktaiItem("Warp Leaf", 30, "warp_leaf"));
            Items.Add("The Fool", new ZoktaiItem("The Fool", 31, "blue_card"));
            Items.Add("The High Priestess", new ZoktaiItem("The High Priestess", 32, "red_card"));
            Items.Add("The Empress", new ZoktaiItem("The Empress", 33, "red_card"));
            Items.Add("The Emperor", new ZoktaiItem("The Emperor", 34, "blue_card"));
            Items.Add("The Lovers", new ZoktaiItem("The Lovers", 35, "green_card"));
            Items.Add("The Chariot", new ZoktaiItem("The Chariot", 36, "red_card"));
            Items.Add("Strength", new ZoktaiItem("Strength", 37, "red_card"));
            Items.Add("Wheel Of Fortune", new ZoktaiItem("Wheel Of Fortune", 38, "red_card"));
            Items.Add("Justice", new ZoktaiItem("Justice", 39, "blue_card"));
            Items.Add("The Hanged Man", new ZoktaiItem("The Hanged Man", 40, "green_card"));
            Items.Add("Death", new ZoktaiItem("Death", 41, "green_card"));
            Items.Add("Temperance", new ZoktaiItem("Temperance", 42, "blue_card"));
            Items.Add("The Devil", new ZoktaiItem("The Devil", 43, "blue_card"));
            Items.Add("The Tower", new ZoktaiItem("The Tower", 44, "blue_card"));
            Items.Add("The Star", new ZoktaiItem("The Star", 45, "blue_card"));
            Items.Add("The Moon", new ZoktaiItem("The Moon", 46, "blue_card"));
            Items.Add("The Sun", new ZoktaiItem("The Sun", 47, "blue_card"));
            Items.Add("Judgement", new ZoktaiItem("Judgement", 48, "green_card"));
        }

        ///<summary>Init key items instances</summary>
        private void InitKeyItems() {
            KeyItems.Add("Empty slot", new ZoktaiItem("Empty Slot", 65535));
            KeyItems.Add("Dark Card", new ZoktaiItem("Dark Card", 49, "dark_card"));
            KeyItems.Add("Pet", new ZoktaiItem("Pet", 50, "pet"));
            KeyItems.Add("Spade Emblem", new ZoktaiItem("Spade Emblem", 51, "spade_emblem"));
            KeyItems.Add("Heart Emblem", new ZoktaiItem("Heart Emblem", 52, "heart_emblem"));
            KeyItems.Add("Diamond Emblem", new ZoktaiItem("Diamond Emblem", 53, "diamond_emblem"));
            KeyItems.Add("Club Emblem", new ZoktaiItem("Club Emblem", 54, "club_emblem"));
            KeyItems.Add("Joker Emblem", new ZoktaiItem("Joker Emblem", 55, "joker_emblem"));
            KeyItems.Add("Oak Coffin", new ZoktaiItem("Oak Coffin", 56, "oak_coffin"));
            KeyItems.Add("Bronze Coffin", new ZoktaiItem("Bronze Coffin", 57, "bronze_coffin"));
            KeyItems.Add("Iron Coffin", new ZoktaiItem("Iron Coffin", 58, "iron_coffin"));
            KeyItems.Add("Silver Coffin", new ZoktaiItem("Silver Coffin", 59, "silver_coffin"));
            KeyItems.Add("Solar Coffin", new ZoktaiItem("Solar Coffin", 60, "solar_coffin"));
            KeyItems.Add("Coffin Monster Elefan", new ZoktaiItem("Coffin Monster Elefan", 61, "coffin_monster_elefan"));
            KeyItems.Add("Vampire Coffin", new ZoktaiItem("Vampire Coffin", 62, "vampire_coffin"));
            KeyItems.Add("Iron Maiden", new ZoktaiItem("Iron Maiden", 63, "iron_maiden"));
            KeyItems.Add("Warehouse Key", new ZoktaiItem("Warehouse Key", 64, "warehouse_key"));
            KeyItems.Add("Circle Key", new ZoktaiItem("Circle Key", 65, "circle_key"));
            KeyItems.Add("Triangle Key", new ZoktaiItem("Triangle Key", 66, "triangle_key"));
            KeyItems.Add("Square Key", new ZoktaiItem("Square Key", 67, "square_key"));
            KeyItems.Add("Red Crystal", new ZoktaiItem("Red Crystal", 68, "red_crystal"));
            KeyItems.Add("Blue Crystal", new ZoktaiItem("Blue Crystal", 69, "blue_crystal"));
            KeyItems.Add("Green Crystal", new ZoktaiItem("Green Crystal", 70, "green_crystal"));
            KeyItems.Add("Yellow Crystal", new ZoktaiItem("Yellow Crystal", 71, "yellow_crystal"));
            KeyItems.Add("Stone Tablet Piece", new ZoktaiItem("Stone Tablet Piece", 72, "stone_tablet_piece"));
            KeyItems.Add("Tasty Water", new ZoktaiItem("Tasty Water", 73, "tasty_water"));
            KeyItems.Add("The Magician", new ZoktaiItem("The Magician", 74, "gray_card"));
            KeyItems.Add("The Hierophant", new ZoktaiItem("The Hierophant", 75, "gray_card"));
            KeyItems.Add("The Hermit", new ZoktaiItem("The Hermit", 76, "gray_card"));
            KeyItems.Add("The World", new ZoktaiItem("The World", 77, "gray_card"));
        }
    }
}
using System.Collections.Generic;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Items {
    /// <summary>Class for Zoktai item instances and lists</summary>
    class ZoktaiItems {

        public Dictionary<string, Item> Items = [];
        public Dictionary<string, Item> KeyItems = [];
        private readonly ZoktaiAddresses _memAddresses;
        private readonly MemoryValues _memoryValues;

        public ZoktaiItems(MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {
            _memoryValues = memoryValues;
            _memAddresses = zoktaiAddresses;
            InitItems();
            InitKeyItems();
        }

        ///<summary>Init items instances</summary>
        private void InitItems() {
            Items.Add("Empty slot", new Item("Empty Slot", 65535));
            Items.Add("Earthly Nut", new Item("Earthly Nut", 0, "earthly_nut", true));
            Items.Add("Solar Nut", new Item("Solar Nut", 1, "solar_nut", true));
            Items.Add("Speed Nut", new Item("Speed Nut", 2, "speed_nut", true));
            Items.Add("Tiptoe Nut", new Item("Tiptoe Nut", 3, "tiptoe_nut", true));
            Items.Add("Banana", new Item("Banana", 4, "banana", true));
            Items.Add("Chocolate Banana", new Item("Chocolate Banana", 5, "chocolate_banana"));
            Items.Add("Bearnut", new Item("Bearnut", 6, "bearnut", true));
            Items.Add("See-All Nut", new Item("See-All Nut", 7, "see_all_nut", true));
            Items.Add("Rotten Nut", new Item("Rotten Nut", 8, "rotten_nut"));
            Items.Add("Redshroom", new Item("Redshroom", 9, "redshroom", true));
            Items.Add("Blueshroom", new Item("Blueshroom", 10, "blueshroom", true));
            Items.Add("Bad Mushroom", new Item("Bad Mushroom", 11, "bad_mushroom"));
            Items.Add("Drop Of Sun", new Item("Drop Of Sun", 12, "drop_of_sun", true));
            Items.Add("Tomato Juice", new Item("Tomato Juice", 13, "tomato_juice", true));
            Items.Add("Rotten Water", new Item("Rotten Water", 14, "rotten_water"));
            Items.Add("Tasty Meat", new Item("Tasty Meat", 15, "tasty_meat", true));
            Items.Add("Jerky", new Item("Jerky", 16, "jerky"));
            Items.Add("Rotten Meat", new Item("Rotten Meat", 17, "rotten_meat"));
            Items.Add("Chocolate", new Item("Chocolate", 18, "chocolate", true));
            Items.Add("Melted Chocolate", new Item("Melted Chocolate", 19, "melted_chocolate"));
            Items.Add("Chocolate-Covered", new Item("Chocolate-Covered", 20, "chocolate_covered"));
            Items.Add("Deluxe Chocolate", new Item("Deluxe Chocolate", 21, "deluxe_chocolate"));
            Items.Add("Healer", new Item("Healer", 22, "healer"));
            Items.Add("Magical Potion", new Item("Magical Potion", 23, "magical_potion"));
            Items.Add("Antidote", new Item("Antidote", 24, "antidote"));
            Items.Add("Elixir", new Item("Elixir", 25, "elixir"));
            Items.Add("Sunblock", new Item("Sunblock", 26, "sunblock"));
            Items.Add("Mr Rainnot", new Item("Mr Rainnot", 27, "mr_rainnot"));
            Items.Add("Tonniar Rm", new Item("Tonniar Rm", 28, "tonniar_rm"));
            Items.Add("Sunny Clog", new Item("Sunny Clog", 29, "sunny_clog"));
            Items.Add("Warp Leaf", new Item("Warp Leaf", 30, "warp_leaf"));
            Items.Add("The Fool", new Item("The Fool", 31, "blue_card"));
            Items.Add("The High Priestess", new Item("The High Priestess", 32, "red_card"));
            Items.Add("The Empress", new Item("The Empress", 33, "red_card"));
            Items.Add("The Emperor", new Item("The Emperor", 34, "blue_card"));
            Items.Add("The Lovers", new Item("The Lovers", 35, "green_card"));
            Items.Add("The Chariot", new Item("The Chariot", 36, "red_card"));
            Items.Add("Strength", new Item("Strength", 37, "red_card"));
            Items.Add("Wheel Of Fortune", new Item("Wheel Of Fortune", 38, "red_card"));
            Items.Add("Justice", new Item("Justice", 39, "blue_card"));
            Items.Add("The Hanged Man", new Item("The Hanged Man", 40, "green_card"));
            Items.Add("Death", new Item("Death", 41, "green_card"));
            Items.Add("Temperance", new Item("Temperance", 42, "blue_card"));
            Items.Add("The Devil", new Item("The Devil", 43, "blue_card"));
            Items.Add("The Tower", new Item("The Tower", 44, "blue_card"));
            Items.Add("The Star", new Item("The Star", 45, "blue_card"));
            Items.Add("The Moon", new Item("The Moon", 46, "blue_card"));
            Items.Add("The Sun", new Item("The Sun", 47, "blue_card"));
            Items.Add("Judgement", new Item("Judgement", 48, "green_card"));
        }

        ///<summary>Init key items instances</summary>
        private void InitKeyItems() {
            KeyItems.Add("Empty slot", new Item("Empty Slot", 65535));
            KeyItems.Add("Dark Card", new Item("Dark Card", 49, "dark_card"));
            KeyItems.Add("Pet", new Item("Pet", 50, "pet"));
            KeyItems.Add("Spade Emblem", new Item("Spade Emblem", 51, "spade_emblem"));
            KeyItems.Add("Heart Emblem", new Item("Heart Emblem", 52, "heart_emblem"));
            KeyItems.Add("Diamond Emblem", new Item("Diamond Emblem", 53, "diamond_emblem"));
            KeyItems.Add("Club Emblem", new Item("Club Emblem", 54, "club_emblem"));
            KeyItems.Add("Joker Emblem", new Item("Joker Emblem", 55, "joker_emblem"));
            KeyItems.Add("Oak Coffin", new Item("Oak Coffin", 56, "oak_coffin"));
            KeyItems.Add("Bronze Coffin", new Item("Bronze Coffin", 57, "bronze_coffin"));
            KeyItems.Add("Iron Coffin", new Item("Iron Coffin", 58, "iron_coffin"));
            KeyItems.Add("Silver Coffin", new Item("Silver Coffin", 59, "silver_coffin"));
            KeyItems.Add("Solar Coffin", new Item("Solar Coffin", 60, "solar_coffin"));
            KeyItems.Add("Coffin Monster Elefan", new Item("Coffin Monster Elefan", 61, "coffin_monster_elefan"));
            KeyItems.Add("Vampire Coffin", new Item("Vampire Coffin", 62, "vampire_coffin"));
            KeyItems.Add("Iron Maiden", new Item("Iron Maiden", 63, "iron_maiden"));
            KeyItems.Add("Warehouse Key", new Item("Warehouse Key", 64, "warehouse_key"));
            KeyItems.Add("Circle Key", new Item("Circle Key", 65, "circle_key"));
            KeyItems.Add("Triangle Key", new Item("Triangle Key", 66, "triangle_key"));
            KeyItems.Add("Square Key", new Item("Square Key", 67, "square_key"));
            KeyItems.Add("Red Crystal", new Item("Red Crystal", 68, "red_crystal"));
            KeyItems.Add("Blue Crystal", new Item("Blue Crystal", 69, "blue_crystal"));
            KeyItems.Add("Green Crystal", new Item("Green Crystal", 70, "green_crystal"));
            KeyItems.Add("Yellow Crystal", new Item("Yellow Crystal", 71, "yellow_crystal"));
            KeyItems.Add("Stone Tablet Piece", new Item("Stone Tablet Piece", 72, "stone_tablet_piece"));
            KeyItems.Add("Tasty Water", new Item("Tasty Water", 73, "tasty_water"));
            KeyItems.Add("The Magician", new Item("The Magician", 74, "gray_card"));
            KeyItems.Add("The Hierophant", new Item("The Hierophant", 75, "gray_card"));
            KeyItems.Add("The Hermit", new Item("The Hermit", 76, "gray_card"));
            KeyItems.Add("The World", new Item("The World", 77, "gray_card"));
        }
    }
}
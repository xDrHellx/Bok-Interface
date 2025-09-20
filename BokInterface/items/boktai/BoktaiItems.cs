using System.Collections.Generic;
using System.Linq;

namespace BokInterface.Items {
    /// <summary>Class for Boktai item instances and lists</summary>
    class BoktaiItems {

        public Dictionary<string, BoktaiItem> Items = [],
            KeyItems = [],
            All = [];

        public BoktaiItems() {
            InitItems();
            InitKeyItems();
            InitFullList();
        }

        ///<summary>Init items instances</summary>
        private void InitItems() {
            Items.Add("Green Apple", new BoktaiItem("Green Apple", 0, "green_apple"));
            Items.Add("Red Apple", new BoktaiItem("Red Apple", 1, "red_apple"));
            Items.Add("Golden Apple", new BoktaiItem("Golden Apple", 2, "golden_apple"));
            Items.Add("Heal Fruit", new BoktaiItem("Heal Fruit", 3, "heal_fruit"));
            Items.Add("Solar Nut", new BoktaiItem("Solar Nut", 4, "solar_nut"));
            Items.Add("See-All Nut", new BoktaiItem("See-All Nut", 5, "see_all_nut"));
            Items.Add("Tiptoe Nut", new BoktaiItem("Tiptoe Nut", 6, "tiptoe_nut"));
            Items.Add("Speed Nut", new BoktaiItem("Speed Nut", 7, "speed_nut"));
            Items.Add("Banana", new BoktaiItem("Banana", 8, "banana"));
            Items.Add("Evil Banana", new BoktaiItem("Evil Banana", 9, "evil_banana"));
            Items.Add("Redshroom", new BoktaiItem("Redshroom", 10, "redshroom"));
            Items.Add("Blueshroom", new BoktaiItem("Blueshroom", 11, "blueshroom"));
            Items.Add("Flame Nut", new BoktaiItem("Flame Nut", 12, "flame_nut"));
            Items.Add("Ice Nut", new BoktaiItem("Ice Nut", 13, "ice_nut"));
            Items.Add("Bearnut", new BoktaiItem("Bearnut", 14, "bearnut"));
            Items.Add("Enduranut", new BoktaiItem("Enduranut", 15, "enduranut"));
            Items.Add("Empty Gourd", new BoktaiItem("Empty Gourd", 16, "empty_gourd"));
            Items.Add("Solar Leaf", new BoktaiItem("Solar Leaf", 17, "solar_leaf"));
            Items.Add("Bad Pumpkin", new BoktaiItem("Bad Pumpkin", 18, "bad_pumpkin"));
            Items.Add("Revivafruit", new BoktaiItem("Revivafruit", 19, "revivafruit"));
            Items.Add("X2 Carrot", new BoktaiItem("X2 Carrot", 20, "x2_carrot"));
            Items.Add("Fast Carrot", new BoktaiItem("Fast Carrot", 21, "fast_carrot"));
            Items.Add("Life Fruit", new BoktaiItem("Life Fruit", 22, "life_fruit"));
            Items.Add("Rotten Nut", new BoktaiItem("Rotten Nut", 23, "rotten_nut"));
            Items.Add("Mr. Rainnot", new BoktaiItem("Mr. Rainnot", 24, "mr_rainnot"));
            Items.Add("Tonniar .RM", new BoktaiItem("Tonniar .RM", 25, "tonniar_rm"));
            Items.Add("Sunny Clog", new BoktaiItem("Sunny Clog", 26, "sunny_clog"));
        }

        ///<summary>Init key items instances</summary>
        private void InitKeyItems() {
            KeyItems.Add("Star Card", new BoktaiItem("Star Card", 27, "star_card"));
            KeyItems.Add("Fool Card", new BoktaiItem("Fool Card", 28, "fool_card"));
            KeyItems.Add("Dark Card", new BoktaiItem("Dark Card", 29, "dark_card"));
            KeyItems.Add("Silver Coin", new BoktaiItem("Silver Coin", 30, "silver_coin"));
            KeyItems.Add("Circle Key", new BoktaiItem("Circle Key", 31, "circle_key"));
            KeyItems.Add("Triangle Key", new BoktaiItem("Triangle Key", 32, "triangle_key"));
            KeyItems.Add("Square Key", new BoktaiItem("Square Key", 33, "square_key"));
            KeyItems.Add("X Key", new BoktaiItem("X Key", 34, "cross_key"));
            KeyItems.Add("Sol Emblem", new BoktaiItem("Sol Emblem", 35, "sol_emblem"));
            KeyItems.Add("Luna Emblem", new BoktaiItem("Luna Emblem", 36, "luna_emblem"));
            KeyItems.Add("Flame Emblem", new BoktaiItem("Flame Emblem", 37, "flame_emblem"));
            KeyItems.Add("Frost Emblem", new BoktaiItem("Frost Emblem", 38, "frost_emblem"));
            KeyItems.Add("Cloud Emblem", new BoktaiItem("Cloud Emblem", 39, "cloud_emblem"));
            KeyItems.Add("Earth Emblem", new BoktaiItem("Earth Emblem", 40, "earth_emblem"));
            KeyItems.Add("Dark Emblem", new BoktaiItem("Dark Emblem", 41, "dark_emblem"));
        }

        private void InitFullList() {
            All = All
                .Concat(Items)
                .Concat(KeyItems)
                .ToDictionary(e => e.Key, e => e.Value);
        }
    }
}

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
            Items.Add("Green Apple", new BoktaiItem("Green Apple", 0, "green_apple", "Recover 10% Life"));
            Items.Add("Red Apple", new BoktaiItem("Red Apple", 1, "red_apple", "Recover 20% Life"));
            Items.Add("Golden Apple", new BoktaiItem("Golden Apple", 2, "golden_apple", "Recover 50% Life"));
            Items.Add("Heal Fruit", new BoktaiItem("Heal Fruit", 3, "heal_fruit", "Recover 100% Life"));
            Items.Add("Solar Nut", new BoktaiItem("Solar Nut", 4, "solar_nut", "Recover Energy (1/4 of bar)"));
            Items.Add("See-All Nut", new BoktaiItem("See-All Nut", 5, "see_all_nut", "See traps & invisible entities for 30s"));
            Items.Add("Tiptoe Nut", new BoktaiItem("Tiptoe Nut", 6, "tiptoe_nut", "Steps don't make noise for 30s"));
            Items.Add("Speed Nut", new BoktaiItem("Speed Nut", 7, "speed_nut", "Run faster for 30s"));
            Items.Add("Banana", new BoktaiItem("Banana", 8, "banana", "Push blocks faster for 30s"));
            Items.Add("Evil Banana", new BoktaiItem("Evil Banana", 9, "evil_banana", "Push blocks faster for 30s (each push costs Life)"));
            Items.Add("Redshroom", new BoktaiItem("Redshroom", 10, "redshroom", "Become small for 15s"));
            Items.Add("Blueshroom", new BoktaiItem("Blueshroom", 11, "blueshroom", "Become invisible for 15s"));
            Items.Add("Flame Nut", new BoktaiItem("Flame Nut", 12, "flame_nut", "Reduce Flame damage taken by half for 30s"));
            Items.Add("Ice Nut", new BoktaiItem("Ice Nut", 13, "ice_nut", "Reduce Frost damage taken by half for 30s"));
            Items.Add("Bearnut", new BoktaiItem("Bearnut", 14, "bearnut", "Nullify damage taken for 15s"));
            Items.Add("Enduranut", new BoktaiItem("Enduranut", 15, "enduranut", "Store damage taken for 15s, then gradually deal accumulated damage to Django"));
            Items.Add("Empty Gourd", new BoktaiItem("Empty Gourd", 16, "empty_gourd", "Warp outside of current dungeon"));
            Items.Add("Solar Leaf", new BoktaiItem("Solar Leaf", 17, "solar_leaf", "Warp to Solar Tree"));
            Items.Add("Bad Pumpkin", new BoktaiItem("Bad Pumpkin", 18, "bad_pumpkin", "Lower enemy level by one in next dungeon (can stack)"));
            Items.Add("Revivafruit", new BoktaiItem("Revivafruit", 19, "revivafruit", "Revive with 100% Life upon death"));
            Items.Add("X2 Carrot", new BoktaiItem("X2 Carrot", 20, "x2_carrot", "Plant with another item to harvest more"));
            Items.Add("Fast Carrot", new BoktaiItem("Fast Carrot", 21, "fast_carrot", "Plant with another item to make it grow faster"));
            Items.Add("Life Fruit", new BoktaiItem("Life Fruit", 22, "life_fruit", "Increase max Life when 4 are gathered"));
            Items.Add("Rotten Nut", new BoktaiItem("Rotten Nut", 23, "rotten_nut", "Recover 5% Life & make screen blurry when moving for 30s"));
            Items.Add("Mr. Rainnot", new BoktaiItem("Mr. Rainnot", 24, "mr_rainnot", "Increment Solar Gauge by 2 for 24h (cannot exceed max value)"));
            Items.Add("Tonniar .RM", new BoktaiItem("Tonniar .RM", 25, "tonniar_rm", "Restrict Solar Gauge to 1 for 24h"));
            Items.Add("Sunny Clog", new BoktaiItem("Sunny Clog", 26, "sunny_clog", "Randomly trigger Mr. Rainnot or Tonniar .RM effect (50%-50%)"));
        }

        ///<summary>Init key items instances</summary>
        private void InitKeyItems() {
            KeyItems.Add("Star Card", new BoktaiItem("Star Card", 27, "star_card", "Used for obtaining Astro Battery"));
            KeyItems.Add("Fool Card", new BoktaiItem("Fool Card", 28, "fool_card", "Reload current room"));
            KeyItems.Add("Dark Card", new BoktaiItem("Dark Card", 29, "dark_card", "Dark Loan"));
            KeyItems.Add("Silver Coin", new BoktaiItem("Silver Coin", 30, "silver_coin", "Gather 30 to unlock Sound Test"));
            KeyItems.Add("Circle Key", new BoktaiItem("Circle Key", 31, "circle_key"));
            KeyItems.Add("Triangle Key", new BoktaiItem("Triangle Key", 32, "triangle_key"));
            KeyItems.Add("Square Key", new BoktaiItem("Square Key", 33, "square_key"));
            KeyItems.Add("Cross Key", new BoktaiItem("Cross Key", 34, "cross_key"));
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

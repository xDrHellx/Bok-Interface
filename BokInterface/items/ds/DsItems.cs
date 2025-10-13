using System.Collections.Generic;

namespace BokInterface.Items {
    /// <summary>Class for Boktai DS / Lunar Knights item instances and lists</summary>
    class DsItems {

        public Dictionary<string, Item> Items = [],
            KeyItems = [];

        public DsItems() {
            InitItems();
            InitKeyItems();
        }

        ///<summary>Init items instances</summary>
        private void InitItems() {
            Items.Add("Empty Slot", new DsItem("Empty Slot", 65535));
            Items.Add("Rotten Fruit", new DsItem("Rotten Fruit", 0, "rotten_fruit"));
            Items.Add("Rotten Meat", new DsItem("Rotten Meat", 1, "rotten_meat"));
            Items.Add("Cure Drop", new DsItem("Cure Drop", 2, "cure_drop"));
            Items.Add("Earth Fruit", new DsItem("Earth Fruit", 3, "earth_fruit"));
            Items.Add("Delicious Meat", new DsItem("Delicious Meat", 4, "delicious_meat"));
            Items.Add("Healing Potion", new DsItem("Healing Potion", 5, "healing_potion"));
            Items.Add("Cure Bulb", new DsItem("Cure Bulb", 6, "cure_bulb"));
            Items.Add("Dried Meat", new DsItem("Dried Meat", 7, "dried_meat"));
            Items.Add("Heaven Fruit", new DsItem("Heaven Fruit", 8, "heaven_fruit"));
            Items.Add("Stagnant Water", new DsItem("Stagnant Water", 9, "stagnant_water"));
            Items.Add("Magic Drop", new DsItem("Magic Drop", 10, "magic_drop"));
            Items.Add("Solar Fruit", new DsItem("Solar Fruit", 11, "solar_fruit"));
            Items.Add("Magical Ice Pop", new DsItem("Magical Ice Pop", 12, "magical_ice_pop"));
            Items.Add("Magical Drink", new DsItem("Magical Drink", 13, "magical_drink"));
            Items.Add("Magic Potion", new DsItem("Magic Potion", 14, "magic_potion"));
            Items.Add("Magic Bulb", new DsItem("Magic Bulb", 15, "magic_bulb"));
            Items.Add("Miracle Ice Pop", new DsItem("Miracle Ice Pop", 16, "miracle_ice_pop"));
            Items.Add("Midnight Sun Fruit", new DsItem("Midnight Sun Fruit", 17, "midnight_sun_fruit"));
            Items.Add("Melted Chocolate", new DsItem("Melted Chocolate", 18, "melted_chocolate"));
            Items.Add("Chocolate Gob", new DsItem("Chocolate Gob", 19, "chocolate_gob"));
            Items.Add("Chocolate", new DsItem("Chocolate", 20, "chocolate"));
            Items.Add("Milk Chocolate", new DsItem("Milk Chocolate", 21, "milk_chocolate"));
            Items.Add("Deluxe Chocolate", new DsItem("Deluxe Chocolate", 22, "deluxe_chocolate"));
            Items.Add("Restorative", new DsItem("Restorative", 23, "restorative"));
            Items.Add("Wild Card", new DsItem("Wild Card", 24, "wild_card"));
            Items.Add("Resurrective", new DsItem("Resurrective", 25, "resurrective"));
            Items.Add("Moon Dew", new DsItem("Moon Dew", 26, "moon_dew"));
            Items.Add("Full Moon Dew", new DsItem("Full Moon Dew", 27, "full_moon_dew"));
            Items.Add("Protect Fruit", new DsItem("Protect Fruit", 28, "protect_fruit"));
            Items.Add("Spoiled Milk", new DsItem("Spoiled Milk", 29, "spoiled_milk"));
            Items.Add("Milk", new DsItem("Milk", 30, "milk"));
            Items.Add("Yogurt", new DsItem("Yogurt", 31, "yogurt"));
            Items.Add("Fruit Yogurt", new DsItem("Fruit Yogurt", 32, "fruit_yogurt"));
            Items.Add("Antidote", new DsItem("Antidote", 33, "antidote"));
            Items.Add("Elixir", new DsItem("Elixir", 34, "elixir"));
            Items.Add("Clairvoyance Fruit", new DsItem("Clairvoyance Fruit", 35, "clairvoyance_fruit"));
            Items.Add("Speed Fruit", new DsItem("Speed Fruit", 36, "speed_fruit"));
            Items.Add("Stealth Fruit", new DsItem("Stealth Fruit", 37, "stealth_fruit"));
            Items.Add("Power Fruit", new DsItem("Power Fruit", 38, "power_fruit"));
            Items.Add("Chocolate Banana", new DsItem("Chocolate Banana", 39, "chocolate_banana"));
            Items.Add("Endurance Fruit", new DsItem("Endurance Fruit", 40, "endurance_fruit"));
            Items.Add("Warrior's Ethic", new DsItem("Warrior's Ethic", 41, "warrior_s_ethic"));
            Items.Add("Loser Stick", new DsItem("Loser Stick", 42, "loser_stick"));
            Items.Add("Winner Stick", new DsItem("Winner Stick", 43, "winner_stick"));
            Items.Add("Topaz", new DsItem("Topaz", 44, "topaz"));
            Items.Add("Emerald", new DsItem("Emerald", 45, "emerald"));
            Items.Add("Jasper", new DsItem("Jasper", 46, "jasper"));
            Items.Add("Ruby", new DsItem("Ruby", 47, "ruby"));
            Items.Add("Gold Nugget", new DsItem("Gold Nugget", 48, "gold_nugget"));
            Items.Add("Garnet", new DsItem("Garnet", 49, "garnet"));
            Items.Add("Swordsman Photo", new DsItem("Swordsman Photo", 50, "swordsman_photo"));
            Items.Add("Gunslinger Photo", new DsItem("Gunslinger Photo", 51, "gunslinger_photo"));
            Items.Add("Duke Photo", new DsItem("Duke Photo", 52, "duke_photo"));
            Items.Add("Poster Girl Photo", new DsItem("Poster Girl Photo", 53, "poster_girl_photo"));
            Items.Add("Dark Card", new DsItem("Dark Card", 54, "dark_card"));
            Items.Add("Earthly Robe", new DsItem("Earthly Robe", 55, "earthly_robe"));
            Items.Add("Old Solar Gun", new DsItem("Old Solar Gun", 56, "old_solar_gun"));
            Items.Add("Broken Dark Gun", new DsItem("Broken Dark Gun", 57, "broken_dark_gun"));
            Items.Add("Iron (Junk Part)", new DsItem("Iron (Junk Part)", 58, "junk_part"));
            Items.Add("Steel (Junk Part)", new DsItem("Steel (Junk Part)", 59, "junk_part"));
            Items.Add("Solvent (Junk Part)", new DsItem("Solvent (Junk Part)", 60, "junk_part"));
            Items.Add("Throttle (Junk Part)", new DsItem("Throttle (Junk Part)", 61, "junk_part"));
            Items.Add("Leather (Junk Part)", new DsItem("Leather (Junk Part)", 62, "junk_part"));
            Items.Add("Lens (Junk Part)", new DsItem("Lens (Junk Part)", 63, "junk_part"));
            Items.Add("Adamant (Junk Part)", new DsItem("Adamant (Junk Part)", 64, "junk_part"));
            Items.Add("Mythril (Junk Part)", new DsItem("Mythril (Junk Part)", 65, "junk_part"));
            Items.Add("Crystal (Junk Part)", new DsItem("Crystal (Junk Part)", 66, "junk_part"));
            Items.Add("Gunpowder (Junk Part)", new DsItem("Gunpowder (Junk Part)", 67, "junk_part"));
            Items.Add("Convertor (Junk Part)", new DsItem("Convertor (Junk Part)", 68, "junk_part"));
            Items.Add("Meteorite (Junk Part)", new DsItem("Meteorite (Junk Part)", 69, "junk_part"));
            Items.Add("Lunasteel (Junk Part)", new DsItem("Lunasteel (Junk Part)", 70, "junk_part"));
            Items.Add("Fang (Junk Part)", new DsItem("Fang (Junk Part)", 71, "junk_part"));
            Items.Add("Generator (Junk Part)", new DsItem("Generator (Junk Part)", 72, "junk_part"));
            Items.Add("Boktai Sound Data", new DsItem("Boktai Sound Data", 73, "sound_data"));
            Items.Add("Boktai 2 Sound Data", new DsItem("Boktai 2 Sound Data", 74, "sound_data"));
            Items.Add("Boktai 3 Sound Data", new DsItem("Boktai 3 Sound Data", 75, "sound_data"));

            // Multiplayer effects
            Items.Add("Flame", new DsItem("Flame", 76, "flame"));
            Items.Add("Frost", new DsItem("Frost", 77, "frost"));
            Items.Add("Bomb", new DsItem("Bomb", 78, "bomb"));
            Items.Add("Trap", new DsItem("Trap", 79, "trap"));

            // DS System-related events
            Items.Add("BiometricsW", new DsItem("BiometricsW", 80, "biometricsw"));
            Items.Add("Christmas Cake", new DsItem("Christmas Cake", 81, "christmas_cake"));
            Items.Add("New Year's Money", new DsItem("New Year's Money", 82, "new_year_s_money"));
            Items.Add("Valentine's Day Chocolate", new DsItem("Valentine's Day Chocolate", 83, "valentine_s_day_chocolate"));

            // Ryuusei no Rockman transferrable items
            Items.Add("D Energy", new DsItem("D Energy", 84, "ryuusei"));
            Items.Add("Sun Key", new DsItem("Sun Key", 85, "ryuusei"));
            Items.Add("Solar Gun", new DsItem("Solar Gun", 86, "ryuusei"));
            Items.Add("Solar Gun V2", new DsItem("Solar Gun V2", 87, "ryuusei"));
            Items.Add("Dark Sword", new DsItem("Dark Sword", 88, "ryuusei"));
            Items.Add("Dark Sword V2", new DsItem("Dark Sword V2", 89, "ryuusei"));
            Items.Add("FM Bracelet", new DsItem("FM Bracelet", 90, "ryuusei"));
            Items.Add("Ryuusei Power", new DsItem("Ryuusei Power", 91, "ryuusei"));

            // Duplicate data, maybe for multiplayer ?
            Items.Add("Power Fruit (duplicate)", new DsItem("Power Fruit (duplicate)", 92, "power_fruit"));
            Items.Add("Stealth Fruit (duplicate)", new DsItem("Stealth Fruit (duplicate)", 93, "stealth_fruit"));
            Items.Add("Endurance Fruit (duplicate)", new DsItem("Endurance Fruit (duplicate)", 94, "endurance_fruit"));
            Items.Add("Speed Fruit (duplicate)", new DsItem("Speed Fruit (duplicate)", 95, "speed_fruit"));
        }

        ///<summary>Init key items instances</summary>
        private void InitKeyItems() {
            KeyItems.Add("Empty slot", new DsItem("Empty Slot", 65535));
            KeyItems.Add("Mobile Unit", new DsItem("Mobile Unit", 96, "mobile_unit"));
            KeyItems.Add("Solar Goggles", new DsItem("Solar Goggles", 97, "solar_goggles"));
            KeyItems.Add("Paper Bag", new DsItem("Paper Bag", 98, "paper_bag"));
            KeyItems.Add("Letter to Gatekeeper", new DsItem("Letter to Gatekeeper", 99, "letter_to_gatekeeper"));
            KeyItems.Add("Solar Sensor Ver. 1", new DsItem("Solar Sensor Ver. 1", 100, "solar_sensor_ver_1"));
            KeyItems.Add("Solar Sensor Ver. 2", new DsItem("Solar Sensor Ver. 2", 101, "solar_sensor_ver_2"));
            KeyItems.Add("Solar Sensor Ver. 3", new DsItem("Solar Sensor Ver. 3", 102, "solar_sensor_ver_3"));
            KeyItems.Add("Red Key", new DsItem("Red Key", 103, "red_key"));
            KeyItems.Add("Yellow Key", new DsItem("Yellow Key", 104, "yellow_key"));
            KeyItems.Add("Blue Key", new DsItem("Blue Key", 105, "blue_key"));
            KeyItems.Add("Transer", new DsItem("Transer", 106, "transer"));
            KeyItems.Add("Visualizer", new DsItem("Visualizer", 107, "visualizer"));
            KeyItems.Add("Fresh-Picked Herbs", new DsItem("Fresh-Picked Herbs", 108, "fresh_picked_herbs"));
            KeyItems.Add("Bouquet", new DsItem("Bouquet", 109, "bouquet"));
            KeyItems.Add("Caravan Package (Red)", new DsItem("Caravan Package (Red)", 110, "caravan_package_red"));
            KeyItems.Add("Caravan Package (Blue)", new DsItem("Caravan Package (Blue)", 111, "caravan_package_blue"));
            KeyItems.Add("Caravan Package (Yellow)", new DsItem("Caravan Package (Yellow)", 112, "caravan_package_yellow"));
            KeyItems.Add("Fresh-Picked Mushrooms", new DsItem("Fresh-Picked Mushrooms", 113, "fresh_picked_mushrooms"));
            KeyItems.Add("Spare Bottlecap", new DsItem("Spare Bottlecap", 114, "spare_bottlecap"));
            KeyItems.Add("Luna Emblem", new DsItem("Luna Emblem", 115, "luna_emblem"));
            KeyItems.Add("Sol Emblem", new DsItem("Sol Emblem", 116, "sol_emblem"));
            KeyItems.Add("Dark Emblem", new DsItem("Dark Emblem", 117, "dark_emblem"));
        }
    }
}

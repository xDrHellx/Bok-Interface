namespace BokInterface.Items {
    ///<summary>Class representing an item for Boktai DS / Lunar Knights</summary>
    class DsItem : Item {

        protected override string library { get => "DsResources"; }

        public DsItem(string name, uint value, string icon = "", bool perishable = false, int durability = 0, Item? coveredItem = null, int buyPrice = 0) : base(name, value, icon, perishable, durability, coveredItem, buyPrice) {

            /**
             * If this item is perishable, set the item it will turn into to the property
             * Also adjust the value at which it'll be rotten
             */
            if (this.perishable == true) {
                rottsInto = GetRottsInto(value);
                rottenAt = GetRottensAt(value);
            } else if (this.value == 19) {
                /**
                 * Special case for "Chocolate Gob" :
                 * If a covered item was passed, retrieve its rottenAt value & set it for the "Chocolate Gob" instance
                 * If none was passed, just call the GetRottensAt function to set the default value
                 *
                 * We do this because the game stores the value of the covered item's own durability
                 * It's possible to set this item without it covering anything, however by normal means, it only exists if chocolate did cover another item
                 */
                rottenAt = coveredItem != null ? coveredItem.rottenAt : GetRottensAt(value);
            }

            this.durability = durability < rottenAt ? durability : 0;
        }

        protected override string GetRottsInto(uint value) {
            return value switch {
                12 => GetStickItem(33),                                                     // Magical Ice Pop
                13 => "Stagnant Water",                                                     // Magical Drink
                4 => "Rotten Meat",                                                         // Delicious Meat
                20 => GetChocolateItem(coveredItem != null ? coveredItem.value : 65535),    // Chocolate
                30 => GetMilkItem(0, 0, false),                                             // Milk
                3 or 11 or 28 or 35 or 36 or 37 or 40 => "Rotten Fruit",                    // Fruits
                _ => ""
            };
        }

        protected override int GetRottensAt(uint value) {
            /**
             * Note: durability increments differently based on climate and day/night cycle
             * The interval seems to be the same but the amount is different
             */
            return value switch {
                3 or 11 or 28 or 35 or 36 or 37 or 40 or 4 or 13 or 12 => 56,   // Fruits, Delicious Meat, Magical Drink or Magical Ice Pop (in order)
                20 => 8704,                                                     // Chocolate
                30 => 2304,                                                     // Milk
                _ => 0                                                          // Non-perishable
            };
        }

        /// <summary>Get the item that Magical Ice Pop will turn into based on parameters</summary>
        /// <param name="temperature">Temperature (in Farenheit)</param>
        /// <param name="win">True if Winner Stick (False by default, in-game this is random)</param>
        /// <returns><c>string</c>Item</returns>
        protected string GetStickItem(int temperature, bool win = false) {
            // If below 32°F (0°C)
            if (temperature < 32) {
                return "Miracle Ice Pop";
            }

            return win == false ? "Loser Stick" : "Winner Stick";
        }

        /// <summary>Get the chocolate item that the current item will turn into</summary>
        /// <param name="coveredItemId">Current item ID</param>
        /// <returns><c>string</c>Chocolate item</returns>
        protected string GetChocolateItem(uint coveredItemId) {
            return coveredItemId switch {
                30 => "Milk Chocolate",             // Milk
                20 => "Deluxe Chocolate",           // Chocolate
                38 => "Chocolate Banana",           // Power Fruit
                65535 => "Melted Chocolate",        // No item
                _ => "Chocolate Gob"                // Any other item
            };
        }

        /// <summary>Get the item that milk will turn into based on parameters</summary>
        /// <param name="temperature">Temperature (in Farenheit)</param>
        /// <param name="humidity">Humidity (in %)</param>
        /// <param name="fruit">True if a fruit is directly below the milk</param>
        /// <returns><c>string</c>Item</returns>
        protected string GetMilkItem(int temperature, int humidity, bool fruit) {
            // If between 77-104°F (25-40°C) and 60-75% humidity
            if (temperature >= 77 && temperature <= 104 && humidity >= 60 && humidity <= 75) {
                // Fruit Yogurt if any Fruit is below the Milk
                return fruit == true ? "Fruit Yogurt" : "Yogurt";
            }

            return "Spoiled Milk";
        }
    }
}

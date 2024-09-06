using System.Drawing;

namespace BokInterface.Items {
    ///<summary>Class representing an item for Shinbok</summary>
    class ShinbokItem : Item {

        public ShinbokItem(string name, uint value, string icon = "", bool perishable = false, int durability = 0, Item? coveredItem = null, int buyPrice = 0) : base(name, value) {

            this.perishable = perishable;
            this.coveredItem = coveredItem;
            this.buyPrice = buyPrice;

            // Price for selling is always the buying price divided by 2 (or 0 if it cannot be sold)
            sellPrice = buyPrice > 0 ? buyPrice / 2 : 0;

            // If an icon was specified try getting & setting it to the property
            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ShinbokResources", icon);
                } catch { }
            }

            /**
             * If this item is perishable, set the item it will turn into to the property
             * Also adjust the value at which it'll be rotten
             */
            if (this.perishable == true) {
                rottsInto = GetRottsInto(value);
                rottenAt = GetRottensAt(value);
            } else if (this.value == 9) {
                /**
                 * Special case for "Chocolate-Covered" :
                 * If a covered item was passed, retrieve its rottenAt value & set it for the "Chocolate-Covered" instance
                 * If none was passed, just call the GetRottensAt function to set the default value
                 * 
                 * We do this because the game stores the value of the covered item's own durability
                 * It's possible to set this item without it covering anything, however by normal means, it only exists if chocolate did cover another item
                 */
                rottenAt = coveredItem != null ? coveredItem.rottenAt : GetRottensAt(value);
            }

            this.durability = durability < rottenAt ? durability : 0;
        }

        /// <summary>Returns the item this instance should rott into</summary>
        /// <param name="value">Instance item value</param>
        /// <returns><c>String</c>Item name</returns>
        protected override string GetRottsInto(uint value) {
            return value switch {
                // Redshroom & Blueshroom
                23 or 24 => "Bad Mushroom",
                // GariGari Soda & GariGari Cola
                10 or 11 => "Loser Stick",
                // Chocolate (if a Banana gets covered, it becomes a "Chocolate Banana", other items becomes "Chocolate-Covered")
                6 => coveredItem != null ? (coveredItem.value == 20 ? "Chocolate Banana" : "Chocolate-Covered") : "Melted Chocolate",
                // Melted Chocolate (only turns into Deluxe Chocolate if covering another Melted Chocolate)
                8 => coveredItem != null && coveredItem.value == 9 ? "Deluxe Chocolate" : "",
                // Tasty Meat
                4 => "Rotten Meat",
                // Nuts
                1 or 13 or 19 or 22 or 25 => "Rotten Nut",
                // Non-perishable item
                _ => ""
            };
        }

        /// <summary>Returns the value at which this instance should turn into a rotten item</summary>
        /// <param name="value">Instance item value</param>
        /// <returns><c>Int</c>Rottens at value</returns>
        protected override int GetRottensAt(uint value) {
            return value switch {
                4 or 6 or 10 or 11 => 3840,
                1 or 13 or 19 or 22 or 25 or 23 or 24 => 7680,
                _ => 0
            };
        }
    }
}
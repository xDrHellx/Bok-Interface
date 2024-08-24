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
             * Also adjust the time it takes for it to rott
             */
            if (this.perishable == true) {
                rottsInto = GetRottsInto(value);
                rottenAt = GetRottensAt(value);
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
                // Chocolate
                6 => coveredItem != null ? "Chocolate-Covered" : "Melted Chocolate",
                // Chocolate-Covered
                9 => "Deluxe Chocolate",
                // Tasty Meat
                4 => "Rotten Meat",
                // Nuts
                _ => "Rotten Nut",
            };
        }

        /// <summary>Returns the value at which this instance should turn into a rotten item</summary>
        /// <param name="value">Instance item value</param>
        /// <returns><c>Int</c>Rottens at value</returns>
        protected override int GetRottensAt(uint value) {
            return value switch {
                4 or 6 or 10 or 11 => 3840,
                _ => 7680
            };
        }
    }
}
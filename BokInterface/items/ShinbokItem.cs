using System.Drawing;

namespace BokInterface.Items {
    ///<summary>Class representing an item for Shinbok</summary>
    class ShinbokItem : Item {

        public ShinbokItem(string name, uint value, string icon = "", bool perishable = false, int durability = 0, Item? coveredItem = null, int buyPrice = 0) : base(name, value) {

            this.perishable = perishable;
            this.durability = durability < rottenAt ? durability : 0;
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

            // If this item is perishable, set the item it will turn into to the property
            if (this.perishable == true) {
                rottsInto = GetRottsInto(this.value);

                /**
                 * In some cases the item takes longer to rott
                 * Items concerned : Tomato Juice, Redshroom, Blueshroom
                 */
                rottenAt = GetRottensAt(this.value);
            }
        }

        /// <summary>Returns the item this instance should rott into</summary>
        /// <param name="value">Instance item value</param>
        /// <returns><c>String</c>Item name</returns>
        protected override string GetRottsInto(uint value) {
            return value switch {
                // Redshroom & Blueshroom
                23 or 24 => "Bad Mushroom",
                // Drop of Sun
                17 => "Rotten water",
                // Chocolate
                6 => coveredItem != null ? "Chocolate-covered" : "Melted Chocolate",
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
                23 or 24 => 7680,
                _ => 3840,
            };
        }
    }
}
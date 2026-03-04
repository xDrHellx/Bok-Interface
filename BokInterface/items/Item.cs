using System.Drawing;

namespace BokInterface.Items {
    ///<summary>Base class for representing an item</summary>
    abstract class Item {

        ///<summary>Item name</summary>
        public string name;
        ///<summary>Value (decimal)<summary>
        public uint value;
        ///<summary>Indicates if the item can is perishable (becomes rotten or melts)</summary>
        public bool perishable;
        ///<summary>Item durability if it can melt or become rotten</summary>
        public int durability;
        /// <summary>Item durability offset</summary>
        /// <remarks>This is used in special cases like "Chocolate-Covered" items that adds an offset on top of the covered item's own durability</remarks>
        public int durabilityOffset = 0;
        ///<summary>Value at which the item spoils and turns into the corresponding melted or rotten item </summary>
        public int rottenAt = 0;
        public Image? icon = null;
        ///<summary>Covered item if this instance is "Chocolated-covered"</summary>
        public Item? coveredItem;
        ///<summary>The name of the item this can rott or melt into, if perishable</summary>
        public string rottsInto = "";
        /// <summary>Price when buying</summary>
        public int buyPrice;
        /// <summary>Price when selling</summary>
        public int sellPrice;
        /// <summary>Resource library for retrieving the icon</summary>
        protected abstract string library { get; }

        public Item(string name, uint value, string icon = "", bool perishable = false, int durability = 0, Item? coveredItem = null, int buyPrice = 0) {
            this.name = name;
            this.value = value;
            this.perishable = perishable;
            this.coveredItem = coveredItem;
            this.buyPrice = buyPrice;

            // Price for selling is always the buying price divided by 2 (or 0 if it cannot be sold)
            sellPrice = buyPrice > 0 ? buyPrice / 2 : 0;

            // If an icon was specified try getting & setting it to the property
            SetIconResource(icon);

            // If this item is perishable, set the item it will turn into to the property
            if (this.perishable == true) {
                rottsInto = GetRottsInto(this.value);

                /**
                 * In some cases the item takes longer to rott
                 * Items concerned : Tomato Juice, Redshroom, Blueshroom
                 */
                rottenAt = GetRottensAt(this.value);
            }

            this.durability = durability < rottenAt ? durability : 0;
        }

        /// <summary>Get the item this instance should rott into</summary>
        /// <param name="value">Instance item value</param>
        /// <returns><c>String</c>Item name</returns>
        protected abstract string GetRottsInto(uint value);

        /// <summary>Get the value at which this instance should turn into a rotten item</summary>
        /// <param name="value">Instance item value</param>
        /// <returns><c>Int</c>Rottens at value</returns>
        protected abstract int GetRottensAt(uint value);

        /// <summary>Get the durability of the covered item (for "Chocolate-Covered" instances)</summary>
        /// <returns><c>Int</c>Covered item durability</returns>
        public int GetCoveredItemDurability() {
            return durability - durabilityOffset;
        }

        /// <summary>Simplified method for setting the instance's icon via resources</summary>
        /// <param name="icon">Icon string</param>
        /// <returns><c>Image</c>Resource</returns>
        protected void SetIconResource(string iconString) {
            icon = null;
            if (iconString != "") {
                try {
                    icon = library != "" ? (Image)ResourceLoader.LoadResource(library, iconString) : (Image)Properties.Resources.ResourceManager.GetObject(iconString);
                } catch { }
            }
        }
    }
}

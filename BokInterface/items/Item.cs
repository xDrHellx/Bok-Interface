using System.Drawing;

namespace BokInterface.Items {
    ///<summary>Class representing an item</summary>
    class Item {

        ///<summary>Item name</summary>
        public readonly string name;
        ///<summary>Value (decimal)<summary>
        public readonly uint value;
        ///<summary>Indicates if the item can is perishable (becomes rotten or melts)</summary>
        public readonly bool perishable;
        ///<summary>Item durability if it can melt or become rotten</summary>
        public readonly int durability;
        ///<summary>Value at which the item spoils and turns into the corresponding melted or rotten item </summary>
        public readonly int rottenAt = 3840;
        public readonly Image? icon = null;
        ///<summary>Covered item if this instance is "Chocolated-covered"</summary>
        ///<remarks>Currently unused (research for implementation is needed)</remarks>
        public readonly Item? coveredItem;
        ///<summary>The name of the item this can rott or melt into, if perishable</summary>
        public readonly string rottsInto = "";

        public Item(string name, uint value, string icon = "", bool perishable = false, int durability = 0, Item? coveredItem = null) {
            this.name = name;
            this.value = value;
            this.perishable = perishable;
            this.durability = durability < rottenAt ? durability : 0;
            this.coveredItem = coveredItem;

            // If an icon was specified try getting & setting it to the property
            if (icon != "") {
                try {
                    this.icon = (Image)Properties.Resources.ResourceManager.GetObject(icon);
                } catch { }
            }

            // If this item is perishable, set the item it will turn into to the property
            if (this.perishable == true) {
                rottsInto = this.value switch {
                    // Redshroom
                    9 or 10 => "Bad Mushroom",
                    // Drop of Sun
                    12 => "Rotten water",
                    // Chocolate
                    18 => coveredItem != null ? "Chocolate-covered" : "Melted Chocolate",
                    // Tasty Meat
                    15 => "Rotten Meat",
                    // Nuts
                    _ => "Rotten Nut",
                };
            }
        }
    }
}
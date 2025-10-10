using System.Drawing;

namespace BokInterface.Items {
    ///<summary>Class representing an item for Boktai DS / Lunar Knights</summary>
    class DsItem : Item {

        public DsItem(string name, uint value, string icon = "", bool perishable = false, int durability = 0, Item? coveredItem = null, int buyPrice = 0) : base(name, value, icon, perishable, durability, coveredItem, buyPrice) {

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("DsResources", icon);
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

        protected override string GetRottsInto(uint value) {
            return value switch {
                // TODO research needed for rotten items
                _ => ""
            };
        }

        protected override int GetRottensAt(uint value) {
            return value switch {
                // TODO research needed for rotten items values
                _ => 0
            };
        }
    }
}

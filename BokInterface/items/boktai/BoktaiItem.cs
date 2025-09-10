using System.Drawing;

namespace BokInterface.Items {
    ///<summary>Class representing an item for Boktai</summary>
    class BoktaiItem : Item {

        /// <summary>Amount for this item</summary>
        public int amount;
        /// <summary>Slot (position) in the inventory</summary>
        public int slot;

        public BoktaiItem(string name, uint value, string icon = "", int amount = 0, int slot = 0) : base(name, value, icon) {
            this.amount = amount;
            this.slot = slot;

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("BoktaiResources", icon);
                } catch { }
            }
        }

        #region Unused in Bok 1

        protected override string GetRottsInto(uint value) {
            return "";
        }

        protected override int GetRottensAt(uint value) {
            return 0;
        }

        #endregion
    }
}

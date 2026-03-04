namespace BokInterface.Items {
    ///<summary>Class representing an item for Boktai</summary>
    class BoktaiItem(string name, uint value, string icon = "", int amount = 0, int slot = 0) : Item(name, value, icon) {

        /// <summary>Amount for this item</summary>
        public int amount = amount;
        /// <summary>Slot (position) in the inventory</summary>
        public int slot = slot;
        protected override string library { get => "BoktaiResources"; }

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

namespace BokInterface.Accessories {
    ///<summary>Class for representing an accessory in Zoktai (also called protector)</summary>
    class ZoktaiAccessory(string name, uint value, string icon = "", int row = 1, int buyPrice = 0, bool crossOver = false, int defense = 0, int weight = 0) : Accessory(name, value, "body", icon, row, buyPrice, crossOver) {

        ///<summary>Accessory defense (refered as Durability in-game)</summary>
        public int defense = defense;
        /// <summary>Accessory weight</summary>
        public int weight = weight;
        protected override string library { get => "ZoktaiResources"; }
    }
}

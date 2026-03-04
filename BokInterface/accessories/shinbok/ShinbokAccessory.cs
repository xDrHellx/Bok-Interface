namespace BokInterface.Accessories {
    ///<summary>Class for representing an accessory in Shinbok</summary>
    class ShinbokAccessory(string name, uint value, string type, string icon = "", int row = 1, int buyPrice = 0, bool crossOver = false, int level = 0, string set = "") : Accessory(name, value, type, icon, row, buyPrice, crossOver) {

        /// <summary>Accessory level (determines results when used for solar forging)</summary>
        public int level = level;
        /// <summary>The name of the set the accessory belongs to (Solar Django, Black Django, Rockman, ...)</summary>
        public string set = set;
        protected override string library { get => "ShinbokResources"; }
    }
}

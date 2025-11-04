namespace BokInterface.Accessories {
    ///<summary>Class for representing an accessory in Boktai DS / Lunar Knights</summary>
    class DsAccessory(string name, uint value, string type, string icon = "", int buyPrice = 0, bool crossOver = false) : Accessory(name, value, type, icon, 1, buyPrice, crossOver) {
        protected override string library { get => "DsResources"; }
    }
}

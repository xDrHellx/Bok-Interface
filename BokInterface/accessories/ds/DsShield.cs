namespace BokInterface.Accessories {
    /// <summary>Class for representing a Shield in Boktai DS / Lunar Knights</summary>
    class DsShield(string name, uint value, string icon = "", int baseGuard = 25, int maxGuard = 99) : DsAccessory(name, value, "shield", icon) {
        /// <summary>Base guard for the shield</summary>
        public int baseGuard = baseGuard;
        /// <summary>Max guard counter for the shield</summary>
        public int maxGuard = maxGuard;
    }
}

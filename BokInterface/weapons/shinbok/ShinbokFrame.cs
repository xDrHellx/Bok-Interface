namespace BokInterface.Weapons {
    /// <summary>Class for representing a Gun Frame in Shinbok</summary>
    class ShinbokFrame(string name, uint value, string power, string cost, string icon = "") : Frame(name, power, icon) {

        /// <summary>Value (decimal)</summary>
        public uint value = value;
        /// <summary>Energy cost stat (S > A > B > C > D)</summary>
        public string cost = cost;
        protected override string library { get => "ShinbokResources"; }
    }
}

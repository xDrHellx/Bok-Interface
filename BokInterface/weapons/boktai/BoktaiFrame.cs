namespace BokInterface.Weapons {
    /// <summary>Class for representing a Gun Frame in Boktai</summary>
    class BoktaiFrame(string name, string power, string stun, string type, string icon = "", bool emblemRequired = false) : Frame(name, power, icon) {

        /// <summary>Indicate if an emblem is required to get the frame (some are locked behind emblem doors or triggers in Azure Sky Tower)</summary>
        public bool emblemRequired = emblemRequired;
        /// <summary>Stun stat (S > A > B > C > D > E)</summary>
        public string stun = stun;
        /// <summary>Type of Frame (Heavy, Spread, ...)</summary>
        public string type = type;
        protected override string library { get => "BoktaiResources"; }
    }
}

using System.Drawing;

namespace BokInterface.Weapons {
    /// <summary>Class for representing a Gun Frame in Boktai</summary>
    class BoktaiFrame : Frame {

        /// <summary>Indicate if an emblem is required to get the frame (some are locked behind emblem doors or triggers in Azure Sky Tower)</summary>
        public bool emblemRequired;
        /// <summary>Stun stat (S > A > B > C > D > E)</summary>
        public string stun;
        /// <summary>Type of Frame (Heavy, Spread, ...)</summary>
        public string type;

        public BoktaiFrame(string name, string power, string stun, string type, string icon = "", bool emblemRequired = false) : base(name, power, icon) {
            this.emblemRequired = emblemRequired;
            this.stun = stun;
            this.type = type;

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("BoktaiResources", icon);
                } catch { }
            }
        }
    }
}

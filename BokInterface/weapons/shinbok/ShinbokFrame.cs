using System.Drawing;

namespace BokInterface.Weapons {
    /// <summary>Class for representing a Gun Frame in Shinbok</summary>
    class ShinbokFrame : Frame {

        /// <summary>Value (decimal)</summary>
        public uint value;
        /// <summary>Energy cost stat (S > A > B > C > D)</summary>
        public string cost;

        public ShinbokFrame(string name, uint value, string power, string cost, string icon = "") : base(name, power, icon) {
            this.value = value;
            this.cost = cost;

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ShinbokResources", icon);
                } catch { }
            }
        }
    }
}

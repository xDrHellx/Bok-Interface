using System.Drawing;

namespace BokInterface.Magics {
    ///<summary>Class representing a magic for Zoktai</summary>
    class ZoktaiMagic : Magic {
        public ZoktaiMagic(string name, string type, string icon = "", string description = "") : base(name, type, icon, description) {
            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ZoktaiResources", icon);
                } catch { }
            }
        }
    }
}

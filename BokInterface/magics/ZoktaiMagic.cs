using System.Drawing;

namespace BokInterface.Magics {
    ///<summary>Class representing a magic for Zoktai</summary>
    class ZoktaiMagic : Magic {

        public ZoktaiMagic(string name, string type, string icon = "", string description = "") : base(name, type) {

            this.description = description;

            // If an icon was specified try getting & setting it to the property
            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ZoktaiResources", icon);
                } catch { }
            }
        }
    }
}
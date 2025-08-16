using System.Drawing;

namespace BokInterface.Weapons {
    /// <summary>Class for representing a Gun Lens in Boktai</summary>
    class BoktaiLens : Lens {

        /// <summary>Lens level (1 ~ 3)</summary>
        public int level;

        public BoktaiLens(string name, uint value, string element, string icon = "", int level = 1) : base(name, value, element, icon) {
            this.level = level < 1 ? 1 : (level > 3 ? 3 : level);

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("BoktaiResources", icon);
                } catch { }
            }
        }
    }
}

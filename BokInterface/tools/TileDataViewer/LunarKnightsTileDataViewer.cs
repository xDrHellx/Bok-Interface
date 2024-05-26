using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Tools.TileDataViewer {
    /// <summary>TDViewer tool for Lunar Knights / Boktai DS</summary>
    class LunarKnightsTileDataViewer : TileDataViewer {

        private readonly BokInterface _bokInterface;
        private readonly LunarKnightsAddresses _memAddresses;

        public LunarKnightsTileDataViewer(BokInterface bokInterface, LunarKnightsAddresses lunarKnightsAddresses) {
            Owner = _bokInterface = bokInterface;
            _memAddresses = lunarKnightsAddresses;
            Icon = _bokInterface.Icon;
            InitializeSubwindowProperties();
        }

        protected override void SetGameAddresses() {
            // Currently not handled, not enough addresses available
            mapDataAddress = djangoXposAddress = djangoYposAddress = 0;
        }

        protected override void DrawTileEffect(PaintEventArgs e, uint tileEffect, int posX, int posY, int scale) {

            // Only handle values between a certain range (4096 = 1000 in hexadecimal)
            if (tileEffect > 0 && tileEffect < 4096) {

                /**
                 * Get the hexadecimal value of the tile effect
                 * We'll use this for comparison because of current findings
                 */
                string hex = Utilities.IntToHex(tileEffect);

                // Handle the tile effect
                switch (hex) {
                    default:
                        // If tile effect is currently not handled, print its values on-screen & show its position on the tilemap to study it
                        // APIs.Gui.AddMessage("hex : " + hex.ToString() + " ( uint : " + tileEffect + ")");
                        // this.DrawTileImage(e, "qmark", 5 + posX * scale, 5 + posY * scale);
                        break;
                }
            }
        }
    }
}

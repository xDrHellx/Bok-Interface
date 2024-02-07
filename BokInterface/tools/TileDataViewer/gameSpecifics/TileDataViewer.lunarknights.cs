using System.Drawing;
using System.Windows.Forms;
using BokInterface.All;

/**
 * File for handling Lunar Knights / Boktai DS-specific values
 */ 

namespace BokInterface.Tools.TileDataViewer {
    partial class TileDataViewer : Form {

        /// <summary>Draws tile effect icons for Lunar Knights / Boktai DS</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="tileEffect">Value for the tile's effect</param>
        /// <param name="posX">X position of the tile</param>
        /// <param name="posY">Y position of the tile</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected void DrawLunarKnightsTileEffect(PaintEventArgs e, uint tileEffect, int posX, int posY, int scale) {

            // Only handle values between a certain range (4096 = 1000 in hexadecimal)
            if(tileEffect > 0 && tileEffect < 4096) {

                /**
                 * Get the hexadecimal value of the tile effect
                 * We'll use this for comparison because of current findings
                 */
                string hex = Utilities.IntToHex(tileEffect);

                // Handle the tile effect
                switch(hex) {
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
using System.Drawing;
using System.Windows.Forms;
using BokInterface.All;

/**
 * File for handling Boktai-specific values
 */ 

namespace BokInterface.Tools.TileDataViewer {
    partial class TileDataViewer : Form {

        /// <summary>Draws tile effect icons for Boktai</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="tileEffect">Value for the tile's effect</param>
        /// <param name="posX">X position of the tile</param>
        /// <param name="posY">Y position of the tile</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected void DrawBoktaiTileEffect(PaintEventArgs e, uint tileEffect, int posX, int posY, int scale) {

            // Only handle values between a certain range (4096 = 1000 in hexadecimal)
            if(tileEffect > 0 && tileEffect < 4096) {

                /**
                 * Get the hexadecimal value of the tile effect
                 * We'll use this for comparison because of current findings
                 */
                string hex = Utilities.IntToHex(tileEffect);

                // Handle the tile effect
                switch(hex) {
                    case "1":                   /// ??? (Something on stairs in the first room of Deserted Arsenal)
                        break;
                    case "2":                   /// Wall
                        /**
                         * Commented because if a block is pushed where the "wall" is,
                         * you can walk on it, making the tilemap misleading
                         * 
                         * For example the first area of Firetop Mountain has a "wall" that can be "filled" with a pushable block
                         */
                        // this.DrawFilledRectangle(e, blackColor, 5 + posX * scale, 5 + posY * scale, scale);
                        break;
                    case "3":                   /// Levers in Bloodrust Mansion before the garden
                        break;
                    case "4":                   // Exit / entry (inconsistent)
                        this.DrawTileImage(e, "exit", 5 + posX * scale, 5 + posY * scale);
                        break;
                    case "8":                   /// ??? (something right before Garmr cutscene)
                        break;
                    case "C":                   /// ??? (sometimes used for downward stairs that also have an exit, can be seen in Dark Castle)
                        break;
                    case "20":                  /// Noise tile (makes sound)
                        this.DrawTileImage(e, "sound", 5 + posX * scale, 5 + posY * scale);
                        break;
                    case "40":                  /// Ice
                        this.DrawTileImage(e, "ice", 5 + posX * scale, 5 + posY * scale);
                        break;
                    case "80":                  /// Lava
                    case "84":
                    case "86":
                        this.DrawTileImage(e, "lava", 5 + posX * scale, 5 + posY * scale);
                        break;
                    case "100":                 /// Void (fall & die)
                        this.DrawFilledRectangle(e, blackColor, 5 + posX * scale, 5 + posY * scale, scale);
                        break;
                    case "203":                 /// Unwalkable parts of the roofs in Bloodrust Mansion
                        break;
                    case "802":                 /// ??? (something in Permafrost before Garmr)
                        break;
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
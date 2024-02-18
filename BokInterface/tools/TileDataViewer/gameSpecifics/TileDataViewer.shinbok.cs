using System.Windows.Forms;

using BokInterface.All;

/**
 * File for handling Shinbok-specific values
 */

namespace BokInterface.Tools.TileDataViewer
{
    partial class TileDataViewer : Form
    {

        /// <summary>Draws tile effect icons for Shinbok</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="tileEffect">Value for the tile's effect</param>
        /// <param name="posX">X position of the tile</param>
        /// <param name="posY">Y position of the tile</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected void DrawShinbokTileEffect(PaintEventArgs e, uint tileEffect, int posX, int posY, int scale)
        {

            // Only handle values between a certain range (4096 = 1000 in hexadecimal)
            if (tileEffect > 0 && tileEffect < 4096)
            {

                /**
                 * Get the hexadecimal value of the tile effect
                 * We'll use this for comparison because of current findings
                 */
                string hex = Utilities.IntToHex(tileEffect);

                // Handle the tile effect
                switch (hex)
                {
                    case "3":                   /// Wall
                        break;
                    case "A":                   /// Exit / entry (inconsistent)
                        DrawTileImage(e, "exit", 5 + posX * scale, 5 + posY * scale);
                        break;
                    case "C6A":                 /// Void (fall & die)
                        DrawFilledRectangle(e, blackColor, 5 + posX * scale, 5 + posY * scale, scale);
                        break;
                    case "F":                   /// Wall (San Miguel)
                        break;
                    case "40":                  /// Solar panel
                        DrawTileImage(e, "solar_panel", 5 + posX * scale, 5 + posY * scale);
                        break;
                    case "403":                 /// Wall (Ancient Tree)
                        break;
                    case "40A":                 /// Walkable area (San Miguel)
                        break;
                    case "40B":                 /// Wall
                        break;
                    case "40F":                 /// Wall (San Miguel)
                        break;
                    case "42F":                 /// ??? (Sealed Dungeon)
                        break;
                    case "41B":                 /// Plant shooting needles, growing plant & giant mushroom in Ancient Tree (inconsistent)
                        break;
                    case "80":                  /// Vine (boss room in Ancient Tree, hit on contact)
                        break;
                    case "84A":                 /// Thin passage on void (in Ancient Tree)
                        break;
                    case "829":                 /// ??? (Unwalkable void in Ancient Tree)
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

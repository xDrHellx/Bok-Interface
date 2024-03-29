using System.Windows.Forms;

using BokInterface.All;

/**
 * File for handling Zoktai-specific values
 */

namespace BokInterface.Tools.TileDataViewer {
    partial class TileDataViewer : Form {

        /// <summary>Draws tile effect icons for Zoktai</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="tileEffect">Value for the tile's effect</param>
        /// <param name="posX">X position of the tile</param>
        /// <param name="posY">Y position of the tile</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected void DrawZoktaiTileEffect(PaintEventArgs e, uint tileEffect, int posX, int posY, int scale) {

            // Only handle values between a certain range (4096 = 1000 in hexadecimal)
            if (tileEffect > 0 && tileEffect < 4096) {

                /**
                 * Get the hexadecimal value of the tile effect
                 * We'll use this for comparison because of current findings
                 */
                string hex = Utilities.IntToHex(tileEffect);

                // Handle the tile effect
                switch (hex) {
                    case "2":                   /// ??? (sometimes on stairs going downard, walkable areas or walls)
                        break;
                    case "3":                   /// Wall
                        break;
                    case "7":                   /// ??? (decorative tree roots on tiles before Cathedral)
                        break;
                    case "8":                   /// Walkable area (Aqueduct)
                        break;
                    case "9":                   /// Water
                        break;
                    case "10":                  /// Walkable area (San Miguel outskirts, inconsistent, sometimes have a lever or dark root on it)
                        break;
                    case "13":                  /// ??? (prevent on walls in Ruins undead dungeon, before hidden sunny clog area, and Cathedral in library)
                        break;
                    case "17":                  /// Wall (Aqueduct, room with green chest after boss)
                        break;
                    case "21":                  /// Torch (Duneyrr boss room in Cathedral)
                        break;
                    case "A":                   /// Exit / entry (inconsistent)
                        DrawTileImage(e, "exit", 5 + posX * scale, 5 + posY * scale);
                        break;
                    case "B":                   /// ??? (2-heights wall in Aqueduct)
                        break;
                    case "F":                   /// Wall (San Miguel)
                        break;
                    case "40A":                 /// Walkable area (San Miguel)
                        break;
                    case "40B":                 /// Wall
                        break;
                    case "40F":                 /// ??? (solar tree)
                        break;
                    case "42":                  /// Void (fall & die)
                        break;
                    case "43":                  /// Thin passage on void (Cathedral undergrounds)
                        break;
                    case "80":                  /// Noise tile (makes sound)
                        DrawTileImage(e, "sound", 5 + posX * scale, 5 + posY * scale);
                        break;
                    case "400":                 /// Walkable area (Remains)
                        break;
                    case "800":                 /// Fall-able ground (Cathedral main room around Spear chest)
                    case "802":
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

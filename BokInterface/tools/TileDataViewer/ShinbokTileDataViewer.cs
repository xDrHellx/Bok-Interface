using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Tools.TileDataViewer {
    /// <summary>TDViewer tool for Boktai 3</summary>
    class ShinbokTileDataViewer : TileDataViewer {

        #region Properties

        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _memAddresses;

        #endregion

        #region Constructor | Init

        public ShinbokTileDataViewer(BokInterface bokInterface, ShinbokAddresses shinbokAddresses) {
            Owner = _bokInterface = bokInterface;
            _memAddresses = shinbokAddresses;
            Icon = _bokInterface.Icon;
            InitializeSubwindowProperties();
        }

        protected override void SetGameAddresses() {
            mapDataAddress = _memAddresses.Misc["map_data"].Address;
            djangoXposAddress = APIs.Memory.ReadU32(_memAddresses.Misc["stat"].Address) + _memAddresses.Django["x_position"].Address;
            djangoYposAddress = APIs.Memory.ReadU32(_memAddresses.Misc["stat"].Address) + _memAddresses.Django["y_position"].Address;
        }

        #endregion

        #region Drawing

        protected override void DrawTileEffects(PaintEventArgs e, uint tileEffect, int posX, int posY, int scale) {

            // Only handle values between a certain range (4096 = 1000 in hexadecimal)
            if (tileEffect > 0 && tileEffect < 4096) {

                /**
                 * Get the hexadecimal value of the tile effect
                 * We'll use this for comparison because of current findings
                 */
                string hex = Utilities.IntToHex(tileEffect);
                switch (hex) {
                    case "3":                   /// Wall
                        break;
                    case "A":                   /// Exit / entry (inconsistent)
                        DrawTileImage(e, "exit", 5 + posX * scale, 5 + posY * scale);
                        break;
                    case "C6A":                 /// Void (fall & die)
                        DrawFilledRectangle(e, s_blackColor, 5 + posX * scale, 5 + posY * scale, scale);
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
                        if (_debugMode == true) {
                            APIs.Gui.AddMessage("hex : " + hex.ToString() + " ( uint : " + tileEffect + ")");
                            DrawTileImage(e, "qmark", 5 + posX * scale, 5 + posY * scale);
                        }
                        break;
                }
            }
        }

        protected override string GetTileEffectName(int bitNb) {
            return "";
        }

        #endregion
    }
}
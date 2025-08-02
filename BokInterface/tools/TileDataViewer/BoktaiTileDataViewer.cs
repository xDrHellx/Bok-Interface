using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;

namespace BokInterface.Tools.TileDataViewer {
    /// <summary>TDViewer tool for Boktai</summary>
    class BoktaiTileDataViewer : TileDataViewer {

        #region Properties

        private readonly BokInterface _bokInterface;
        private readonly BoktaiAddresses _memAddresses;

        #endregion

        #region Constructor | Init

        public BoktaiTileDataViewer(BokInterface bokInterface, BoktaiAddresses boktaiAddresses) {
            Owner = _bokInterface = bokInterface;
            _memAddresses = boktaiAddresses;
            Icon = _bokInterface.Icon;
            InitializeSubwindowProperties();
        }

        protected override void SetGameAddresses() {
            mapDataAddress = _memAddresses.Map["map_data"].Address;
            djangoXposAddress = _memAddresses.Django["x_position"].Address;
            djangoYposAddress = _memAddresses.Django["y_position"].Address;
        }

        #endregion

        #region Drawing

        /// <summary>Draw tile effect icons</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="tileEffect">Value for the tile's effect</param>
        /// <param name="posX">X position of the tile</param>
        /// <param name="posY">Y position of the tile</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected override void DrawTileEffects(PaintEventArgs e, uint tileEffect, int posX, int posY, int scale) {

            // Only handle values between a certain range (4096 = 1000 in hexadecimal)
            if (tileEffect == 0 || tileEffect >= 4096) {
                return;
            }

            // Loop over effect bits
            for (int i = 0; i < 16; i++) {

                // If the effect is deactivated on this tile, skip to the next bit
                if (Utilities.IsBitOne(tileEffect, i) == false) {
                    continue;
                }

                // Show the icon for the effect on the tile
                string effectName = GetTileEffectName(i);
                switch (effectName) {
                    case "":
                        // Debug / study mode only
                        if (_debugMode == true) {
                            /**
                             * If tile effect is currently not handled:
                             * - Get the hexadecimal value of the tile effect
                             * - Print its values on-screen & show its position on the tilemap to study it
                             */
                            string hex = Utilities.IntToHex(tileEffect);
                            APIs.Gui.AddMessage("bit : " + i + " | hex : " + hex.ToString() + " ( uint : " + tileEffect + ")");
                            DrawTileImage(e, "qmark", posX * scale, posY * scale);
                        }
                        break;
                    case "ignore":
                        // Ignore effect, mostly used for debugging / studying effects
                        break;
                    case "wall":
                        // Walls are ignored to prevent the tilemap from looking messy
                        break;
                    case "void":
                        DrawFilledRectangle(e, s_blackColor, posX * scale, posY * scale, scale);
                        break;
                    default:
                        DrawTileImage(e, effectName, posX * scale, posY * scale);
                        break;
                }
            }
        }

        protected override string GetTileEffectName(int bitNb) {

            /*
                Bitmask refs:
                
                0000 0000 0000 0001 = ???
                0000 0000 0000 0010 = wall
                0000 0000 0000 0100 = ??? seems to be used for some room entrances/exits
                0000 0000 0000 1000 = ???
                0000 0000 0001 0000 = ???
                0000 0000 0010 0000 = noise tile
                0000 0000 0100 0000 = ice
                0000 0000 1000 0000 = lava
                0000 0001 0000 0000 = void (fall down and die)
                0000 0010 0000 0000 = ???
                0000 0100 0000 0000 = unknown (wall-ish)
                0000 1000 0000 0000 = unknown (sometimes used near loading zones pointing NW)
                0001 0000 0000 0000 = unknown (sometimes used near loading zones pointing NE)
                0010 0000 0000 0000 = ???
                0100 0000 0000 0000 = ???
                1000 0000 0000 0000 = ???
            */

            return bitNb switch {
                1 => "wall",
                2 => "exit",
                5 => "sound",   // Noise tile, makes sound on step
                6 => "ice",
                7 => "lava",
                8 => "void",    // Fall down and die
                _ => ""         // Unknown / undocumented effect
            };
        }

        #endregion
    }
}
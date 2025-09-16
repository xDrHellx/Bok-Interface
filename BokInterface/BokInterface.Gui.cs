using System.Windows.Forms;

using BokInterface.Utils;

/**
 * File for the GUI part of the Bok interface
 */

namespace BokInterface {

    partial class BokInterface {

        #region GUI related code

        public static int gbaScreenWidth = 0xF0;
        public static int gbaScreenHeight = 0xA0;

        /// <summary>Shows the indicator for the Bok Interface</summary>
        private void ShowInterfaceIndicator() {
            APIs.Gui.Text(3, 1, "Bok ON", System.Drawing.Color.Orange, "bottomright");

            // On DS add the indicator on the top screen too
            if (_isDS == true) {
                APIs.Gui.Text(3, GetScreenHeight(true) + 1, "Bok ON", System.Drawing.Color.Orange, "bottomright");
            }
        }

        /// <summary>Returns game screen height</summary>
        /// <param name="top">Set to true to return the height of the top screen for DS games</param>
        /// <returns><c>int</c>Height</returns>
        private int GetScreenHeight(bool top = false) {
            return top == true ? APIs.Client.ScreenHeight() / 2 : APIs.Client.ScreenHeight();
        }

        /// <summary>Returns game screen width</summary>
        /// <returns><c>int</c>Width</returns>
        private int GetScreenWidth() {
            return APIs.Client.ScreenWidth();
        }

        #endregion

        #region GUI data

        /// <summary>Generate the menu related to the HUD / GUI data</summary>
        private void GenerateHudMenu() {
            if (shorterGameName == "LunarKnights") {
                return;
            }

            ToolStripMenuItem hudMenu = WinFormHelpers.CreateToolStripMenuItem("hudMenu", "HUD", menuStrip: _menuBar);
            AddDropdownMenuItem("enableGui", "Enable GUI", hudMenu, (sender, e) => ToggleGuiData(sender, ref _showGui));
        }

        #endregion
    }
}

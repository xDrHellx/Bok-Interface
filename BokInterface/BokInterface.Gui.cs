using System.Windows.Forms;

using BokInterface.Utils;

/**
 * File for the GUI part of the Bok interface
 */

namespace BokInterface {

    partial class BokInterface {

        #region GUI properties

        public static readonly int gbaScreenWidth = 0xF0,
            gbaScreenHeight = 0xA0;

        private bool _showGui,
            _showRtc,
            _showIgtFrameCounter,
            _showInterestRate,
            _showBossHp,
            _showAstData;

        #endregion

        #region Interface indicator

        /// <summary>Shows the indicator for the Bok Interface</summary>
        private void ShowInterfaceIndicator() {
            APIs.Gui.Text(3, 1, "Bok ON", System.Drawing.Color.Orange, "bottomright");

            // On DS add the indicator on the top screen too
            if (_isDS == true) {
                APIs.Gui.Text(3, GetScreenHeight(true) + 1, "Bok ON", System.Drawing.Color.Orange, "bottomright");
            }
        }

        #endregion

        #region Helpers

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

        /// <summary>Generate the menu related to the GUI data</summary>
        private void GenerateGuiMenu() {
            if (_isDS == true) {
                return;
            }

            ToolStripMenuItem guiMenu = WinFormHelpers.CreateToolStripMenuItem("guiMenu", "GUI", menuStrip: _menuBar);
            AddDropdownMenuItem("enableGui", "Enable GUI", guiMenu, (sender, e) => ToggleGuiData(sender, ref _showGui), "Necessary to show GUI data on screen");
            AddDropdownMenuItem("showRtc", "Real-time clock", guiMenu, (sender, e) => ToggleGuiData(sender, ref _showRtc));
            AddDropdownMenuItem("showIgtFrameCounter", "IGT frames", guiMenu, (sender, e) => ToggleGuiData(sender, ref _showIgtFrameCounter), "Frames for the current save file's In-Game Time");
            AddDropdownMenuItem("showInterestRate", "Interest rate", guiMenu, (sender, e) => ToggleGuiData(sender, ref _showInterestRate), "Solar Bank interest rate");

            if (shorterGameName == "Boktai") {
                AddDropdownMenuItem("showBossHp", "Boss HP", guiMenu, (sender, e) => ToggleGuiData(sender, ref _showBossHp), "Only visible when data is available");
                AddDropdownMenuItem("showAstData", "AST Data", guiMenu, (sender, e) => ToggleGuiData(sender, ref _showAstData), "Azure Sky Tower floor data");
            }
        }

        #endregion
    }
}

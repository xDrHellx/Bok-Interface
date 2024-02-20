using BokInterface.All;

/**
 * File for the GUI part of the Bok interface
 */

namespace BokInterface {

    partial class BokInterfaceMainForm {

        #region GUI related code

        public static int gbaScreenWidth = 0xF0;
        public static int gbaScreenHeight = 0xA0;

        /// <summary>Shows the indicator for the Bok Interface</summary>
        private void ShowInterfaceIndicator() {
            APIs.Gui.Text(3, 1, interfaceActivated == true ? "Bok ON" : "Bok Off", System.Drawing.Color.Orange, "bottomright");

            // On DS add the indicator on the top screen too
            if (isDS == true) {
                APIs.Gui.Text(3, GetScreenHeight(true) + 1, interfaceActivated == true ? "Bok ON" : "Bok Off", System.Drawing.Color.Orange, "bottomright");
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
    }
}

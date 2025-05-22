using BokInterface.All;

/**
 * File for the "Game not recognized" interface
 */

namespace BokInterface {

    partial class BokInterface {

        /// <summary>Shows the "Game not recognized" window</summary>
        private void ShowGameNotRecognizedWindow() {

            // Current game name
            WinFormHelpers.CreateLabel("currentGameName", "Game not recognized!", 5, 5, 123, 20, this);

            // Window
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 350, 100);
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(BokInterface_FormClosing);
            Load += new System.EventHandler(BokInterface_Load);

            ResumeLayout(false);
        }
    }
}

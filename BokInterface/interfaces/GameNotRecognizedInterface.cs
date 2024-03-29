/**
 * File for the "Game not recognized" interface
 */

namespace BokInterface {

    partial class BokInterface {

        /// <summary>Shows the "Game not recognized" window</summary>
        private void GameNotRecognizedWindow() {

            // Current game name
            CreateLabel("currentGameName", "Game not recognized!", 5, 5, 123, 20, true);

            // Window
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 350, 100);
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(BokInterface_FormClosing);
            Load += new System.EventHandler(BokInterface_Load);

            ResumeLayout(false);
        }
    }
}

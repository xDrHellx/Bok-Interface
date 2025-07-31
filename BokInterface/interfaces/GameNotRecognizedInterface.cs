using BokInterface.Utils;

/**
 * File for the "Game not recognized" interface
 */

namespace BokInterface {
    partial class BokInterface {

        /// <summary>Shows the "Game not recognized" window</summary>
        private void ShowGameNotRecognizedWindow() {

            // Text
            WinFormHelpers.CreateLabel("currentGameName", "Game not recognized!", 0, 0, Width, 20, this, WinFormHelpers._gameNameBackground, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("extraText", "This tool only supports Boktai games.", 0, 20, Width, 20, this, WinFormHelpers._gameNameBackground, textAlignment: "MiddleLeft");

            // Window
            SetMainWindow("Bok Interface", 350, 40);
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(BokInterface_FormClosing);
            Load += new System.EventHandler(BokInterface_Load);
            ResumeLayout(false);
        }
    }
}

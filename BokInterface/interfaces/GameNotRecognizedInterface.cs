/**
 * File for the "Game not recognized" interface
 */

namespace BokInterface
{

    partial class BokInterfaceMainForm
    {

        /// <summary>Shows the "Game not recognized" window</summary>
        private void GameNotRecognizedWindow()
        {

            // Current game name
            this.CreateLabel("currentGameName", "Game not recognized!", 5, 5, 123, 20, true);

            // Window
            this.SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 350, 100);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BokInterfaceMainForm_FormClosing);
            this.Load += new System.EventHandler(this.BokInterfaceMainForm_Load);

            this.ResumeLayout(false);
        }
    }
}
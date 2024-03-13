/**
 * File for the Boktai DS / Lunar Knights interface itself
 */

namespace BokInterface {

    partial class BokInterface {

        #region Properties

        #endregion

        private void ShowLunarKnightsInterface() {

            // Current game name
            CreateLabel("currentGameName", currentGameName, 5, 5, 141, 20, true);

            // Current status section
            AddLunarKnightsCurrentStatusSection();

            // Extras / misc tools section
            AddToolsSection();

            // Main window
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 345, 500);

            ResumeLayout(false);
        }

        private void UpdateLunarKnightsInterface() {

        }

        private void AddLunarKnightsCurrentStatusSection() {

            // Section
            currentStatusGroupBox = CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, true);
        }
    }
}

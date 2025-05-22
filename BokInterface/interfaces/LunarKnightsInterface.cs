using BokInterface.Addresses;
using BokInterface.All;

/**
 * File for the Boktai DS / Lunar Knights interface itself
 */

namespace BokInterface {

    partial class BokInterface {

        #region Properties

        private readonly LunarKnightsAddresses _lunarKnightsAddresses = new();

        #endregion

        private void ShowLunarKnightsInterface() {

            // Current game name
            WinFormHelpers.CreateLabel("currentGameName", currentGameName, 5, 5, 141, 20, this);

            // Current status section
            AddLunarKnightsCurrentStatusSection();

            // Extras / misc tools section
            AddToolsSection();

            // Main window
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 345, 500);

            ResumeLayout(false);
        }

        private void UpdateLunarKnightsInterface() { }

        private void AddLunarKnightsCurrentStatusSection() {

            // Section
            currentStatusGroupBox = WinFormHelpers.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, this);
        }
    }
}

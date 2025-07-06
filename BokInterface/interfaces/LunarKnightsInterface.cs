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
            WinFormHelpers.CreateLabel("currentGameName", currentGameName, 0, 0, Width, 20, this, WinFormHelpers._gameNameBackground, textAlignment: "MiddleLeft");

            // Sections
            AddLunarKnightsCurrentStatusSection();
            AddMiscDataSection();
            AddToolsSection();

            // Main window
            SetMainWindow("Bok Interface", 345, 500);
            ResumeLayout(false);
        }

        private void UpdateLunarKnightsInterface() { }

        private void AddLunarKnightsCurrentStatusSection() {

            // Section
            _currentStatusGroupBox = WinFormHelpers.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, this);
        }
    }
}

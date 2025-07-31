using BokInterface.Addresses;
using BokInterface.Utils;

/**
 * File for the Boktai DS / Lunar Knights interface itself
 */

namespace BokInterface {
    partial class BokInterface {

        #region Properties

        private readonly LunarKnightsAddresses _lunarKnightsAddresses = new();

        #endregion

        #region Show interface

        private void ShowLunarKnightsInterface() {

            // GenerateMenu();

            // Current game name
            WinFormHelpers.CreateLabel("currentGameName", currentGameName, 0, 0, Width, 20, this, WinFormHelpers._gameNameBackground, textAlignment: "MiddleLeft");

            // Sections
            // AddMiscDataSection();

            // Main window
            SetMainWindow("Bok Interface", 350, 40);
            ResumeLayout(false);
        }

        #endregion

        #region Update

        private void UpdateLunarKnightsInterface() { }

        #endregion

        #region Elements

        #endregion
    }
}
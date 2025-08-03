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

            // Current game name
            WinFormHelpers.CreateLabel("currentGameName", currentGameName, 0, 0, Width, 20, this, WinFormHelpers.gameNameBackground, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("extraText", "No data available for this game yet.", 0, 20, Width, 20, this, WinFormHelpers.gameNameBackground, textAlignment: "MiddleLeft");

            // Main window
            SetMainWindow("Bok Interface", 236, 40);
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

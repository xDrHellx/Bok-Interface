using BokInterface.Addresses;
using BokInterface.Utils;

/**
 * File for the Boktai DS / Lunar Knights interface itself
 */

namespace BokInterface {
    partial class BokInterface {

        #region Properties

        private DsAddresses _dsAddresses = new BoktaiDsAddresses();

        #endregion

        #region Show interface

        private void ShowDsInterface() {

            // Memory addresses are different for each version of LK / Bok DS
            if (shorterGameName == "BoktaiDS") {
                _dsAddresses = new BoktaiDsAddresses();
            } else {
                _dsAddresses = region == "US" ? new LunarKnightsUsaAddresses() : new LunarKnightsEuropeanAddresses();
            }

            GenerateMenu();
            AddCurrentGameInfo();

            WinFormHelpers.CreateLabel("extraText", "No data available to show for this game.", 0, 20 + _menuBar.Height, Width, 20, this, WinFormHelpers.gameNameBackground, textAlignment: "MiddleLeft");

            // Main window
            SetMainWindow("Bok Interface", 236, 40 + _menuBar.Height);
            ResumeLayout(false);
        }

        #endregion

        #region Update

        private void UpdateDsInterface() { }

        #endregion

        #region Elements

        #endregion
    }
}

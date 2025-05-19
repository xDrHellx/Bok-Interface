using BokInterface.Addresses;
using BokInterface.All;

/**
 * File for the Boktai TSiiYH interface itself
 */

namespace BokInterface {

    partial class BokInterface {

        #region Properties

        private readonly BoktaiAddresses _boktaiAddresses = new();
        private System.Windows.Forms.Label _bok1_currentStatusHpValue = new();
        private System.Windows.Forms.Label _bok1_currentStatusEneValue = new();

        #endregion

        private void ShowBoktaiInterface() {

            // Current game name
            WinFormHelpers.CreateLabel("currentGameName", currentGameName, 5, 5, 171, 20, this);

            // Current status section
            AddBoktaiCurrentStatusSection();

            // Extras / misc tools section
            AddToolsSection();

            // Main window
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 345, 500);

            ResumeLayout(false);
        }

        private void UpdateBoktaiInterface() {

        }

        private void AddBoktaiCurrentStatusSection() {

            // Section
            currentStatusGroupBox = WinFormHelpers.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, this);

            // Current status labels
            WinFormHelpers.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15, currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15, currentStatusGroupBox);

            // Current status values
            _bok1_currentStatusHpValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15, currentStatusGroupBox);
            _bok1_currentStatusEneValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 34, 31, 15, currentStatusGroupBox);
        }
    }
}

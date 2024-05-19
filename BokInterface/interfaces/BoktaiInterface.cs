using BokInterface.All;

/**
 * File for the Boktai TSiiYH interface itself
 */
namespace BokInterface {

    partial class BokInterface {

        #region Properties

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
            currentStatusLabels.Add(WinFormHelpers.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15));
            currentStatusLabels.Add(WinFormHelpers.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15));

            // Current status values
            _bok1_currentStatusHpValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15);
            _bok1_currentStatusEneValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 34, 31, 15);

            // Add values labels to group
            currentStatusLabels.Add(_bok1_currentStatusHpValue);
            currentStatusLabels.Add(_bok1_currentStatusEneValue);

            // Add elements to group
            for (int i = 0; i < currentStatusLabels.Count; i++) {
                currentStatusGroupBox.Controls.Add(currentStatusLabels[i]);
            }
        }
    }
}

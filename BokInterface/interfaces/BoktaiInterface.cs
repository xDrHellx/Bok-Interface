/**
 * File for the Boktai TSiiYH interface itself
 */

namespace BokInterface {

    partial class BokInterfaceMainForm {

        #region Properties

        private System.Windows.Forms.Label bok1_currentStatusHpValue = new();
        private System.Windows.Forms.Label bok1_currentStatusEneValue = new();

        #endregion

        private void ShowBoktaiInterface() {

            // Current game name
            CreateLabel("currentGameName", currentGameName, 5, 5, 171, 20, true);

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
            currentStatusGroupBox = CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, true);

            // Current status labels
            currentStatusLabels.Add(CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15));
            currentStatusLabels.Add(CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15));

            // Current status values
            bok1_currentStatusHpValue = CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15);
            bok1_currentStatusEneValue = CreateLabel("djangoCurrentHpValue", "", 44, 34, 31, 15);

            // Add values labels to group
            currentStatusLabels.Add(bok1_currentStatusHpValue);
            currentStatusLabels.Add(bok1_currentStatusEneValue);

            // Add elements to group
            for (int i = 0; i < currentStatusLabels.Count; i++) {
                currentStatusGroupBox.Controls.Add(currentStatusLabels[i]);
            }
        }
    }
}

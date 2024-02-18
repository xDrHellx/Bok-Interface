using BokInterface.Addresses;

/**
 * File for the Boktai TSiiYH interface itself
 */

namespace BokInterface
{

    partial class BokInterfaceMainForm
    {

        #region Properties

        private System.Windows.Forms.Label bok1_currentStatusHpValue = new();
        private System.Windows.Forms.Label bok1_currentStatusEneValue = new();

        private readonly BoktaiAddresses boktaiAddresses = new();

        #endregion

        private void ShowBoktaiInterface()
        {

            // Current game name
            this.CreateLabel("currentGameName", currentGameName, 5, 5, 171, 20, true);

            // Current status section
            this.AddBoktaiCurrentStatusSection();

            // Extras / misc tools section
            this.AddToolsSection();

            // Main window
            this.SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 345, 500);

            this.ResumeLayout(false);
        }

        private void UpdateBoktaiInterface()
        {

        }

        private void AddBoktaiCurrentStatusSection()
        {

            // Section
            this.currentStatusGroupBox = this.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, true);

            // Current status labels
            this.currentStatusLabels.Add(this.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15));
            this.currentStatusLabels.Add(this.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15));

            // Current status values
            this.bok1_currentStatusHpValue = this.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15);
            this.bok1_currentStatusEneValue = this.CreateLabel("djangoCurrentHpValue", "", 44, 34, 31, 15);

            // Add values labels to group
            this.currentStatusLabels.Add(this.bok1_currentStatusHpValue);
            this.currentStatusLabels.Add(this.bok1_currentStatusEneValue);

            // Add elements to group
            for (int i = 0; i < this.currentStatusLabels.Count; i++)
            {
                this.currentStatusGroupBox.Controls.Add(this.currentStatusLabels[i]);
            }
        }
    }
}
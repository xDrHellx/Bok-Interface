using System;
using System.Collections.Generic;
using BizHawk.Client.EmuHawk;
using BokInterface.Boktai;

/**
 * File for the Boktai TSiiYH interface itself
 */

namespace BokInterface {
	
	partial class BokInterfaceMainForm {

		private System.Windows.Forms.Label bok1_currentStatusHpValue = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok1_currentStatusEneValue = new System.Windows.Forms.Label();

		private readonly BoktaiAddresses boktaiAddresses = new BoktaiAddresses();

        private void ShowBoktaiInterface() {
			
			// Current game name
			this.CreateLabel("currentGameName", currentGameName, 5, 5, 171, 20, true);

			// Current status section
			this.AddBoktaiCurrentStatusSection();
			
			// Main window
			this.SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 350, 500);
			
			this.ResumeLayout(false);
        }

		private void UpdateBoktaiInterface() {

		}

		private void AddBoktaiCurrentStatusSection() {
			
			// Section
			this.currentStatusGroupBox = this.CreateGroupBox("currentStatus", "Current status", 5, 25, 250, 70, true);

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
			for(int i = 0; i < this.currentStatusLabels.Count; i++) {
				this.currentStatusGroupBox.Controls.Add(this.currentStatusLabels[i]);
			}
		}
    }
}
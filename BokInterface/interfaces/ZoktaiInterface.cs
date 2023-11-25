using System;
using System.Collections.Generic;
using BizHawk.Client.EmuHawk;
using BokInterface.Zoktai;

/**
 * File for the Zoktai (Boktai 2) interface itself
 */

namespace BokInterface {
	
	partial class BokInterfaceMainForm {

		private System.Windows.Forms.Label bok2_currentStatusHpValue = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok2_currentStatusEneValue = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok2_djangoBaseVit = new System.Windows.Forms.Label();
		// private System.Windows.Forms.Label bok2_djangoEquipsVit = new System.Windows.Forms.Label();
		// private System.Windows.Forms.Label bok2_djangoTotalVit = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok2_djangoBaseSpr = new System.Windows.Forms.Label();
		// private System.Windows.Forms.Label bok2_djangoEquipsSpr = new System.Windows.Forms.Label();
		// private System.Windows.Forms.Label bok2_djangoTotalSpr = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok2_djangoBaseStr = new System.Windows.Forms.Label();
		// private System.Windows.Forms.Label bok2_djangoEquipsStr = new System.Windows.Forms.Label();
		// private System.Windows.Forms.Label bok2_djangoTotalStr = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok2_djangoBaseAgi = new System.Windows.Forms.Label();
		// private System.Windows.Forms.Label bok2_djangoEquipsAgi = new System.Windows.Forms.Label();
		// private System.Windows.Forms.Label bok2_djangoTotalAgi = new System.Windows.Forms.Label();

		private readonly ZoktaiAddresses zoktaiAddresses = new ZoktaiAddresses();

        private void ShowZoktaiInterface() {

			// Current game name
			this.CreateLabel("currentGameName", currentGameName, 5, 5, 35, 15, true);

			// Current status section
			this.currentStatusGroupBox = this.CreateGroupBox("currentStatus", "Current status", 5, 25, 250, 70, true);

			// Current status labels
			this.currentStatusLabels.Add(this.CreateLabel("djangoCurrentHp", "LIFE :", 7, 19, 34, 15));
			this.currentStatusLabels.Add(this.CreateLabel("djangoCurrentEne", "ENE :", 7, 34, 34, 15));

			// Add elements to group
			for(int i = 0; i < this.currentStatusLabels.Count; i++) {
				this.currentStatusGroupBox.Controls.Add(this.currentStatusLabels[i]);
			}
			
			// ToolboxMainForm
			this.Name = "Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : "");
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(350, 500);
			
			this.ResumeLayout(false);
        }

		private void UpdateZoktaiInterface() {
			
		}
    }
}
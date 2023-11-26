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
		private System.Windows.Forms.Label bok2_djangoBaseSpr = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok2_djangoBaseStr = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok2_djangoBaseAgi = new System.Windows.Forms.Label();
		
		private readonly ZoktaiAddresses zoktaiAddresses = new ZoktaiAddresses();

        private void ShowZoktaiInterface() {

			// Current game name
			this.CreateLabel("currentGameName", currentGameName, 5, 5, 145, 20, true);

			// Current status section
			this.AddZoktaiCurrentStatusSection();

			// Stats section
			this.AddZoktaiCurrentStatsSection();
			
			// Main window
			this.SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 350, 500);
			
			this.ResumeLayout(false);
        }

		private void UpdateZoktaiInterface() {
			
		}

		private void AddZoktaiCurrentStatusSection() {
			
			// Section
			this.currentStatusGroupBox = this.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 55, true);

			// Current status labels
			this.currentStatusLabels.Add(this.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15));
			this.currentStatusLabels.Add(this.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15));

			// Current status values
			this.bok2_currentStatusHpValue = this.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15);
			this.bok2_currentStatusEneValue = this.CreateLabel("djangoCurrentHpValue", "", 44, 34, 31, 15);

			// Add values labels to group
			this.currentStatusLabels.Add(this.bok2_currentStatusHpValue);
			this.currentStatusLabels.Add(this.bok2_currentStatusEneValue);

			// Add elements to group
			for(int i = 0; i < this.currentStatusLabels.Count; i++) {
				this.currentStatusGroupBox.Controls.Add(this.currentStatusLabels[i]);
			}
		}

		private void AddZoktaiCurrentStatsSection() {

			// Section
			this.currentStatsGroupBox = this.CreateGroupBox("currentStats", "Stats", 5, 86, 75, 90, true);

			// VIT
			this.currentStatsLabels.Add(this.CreateLabel("vitRowLabel", "VIT", 6, 19, 27, 15));
			this.bok2_djangoBaseVit = this.CreateLabel("djangoBaseVit", "", 35, 19, 31, 15, false, BokInterfaceMainForm.baseStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			
			// SPR
			this.currentStatsLabels.Add(this.CreateLabel("sprRowLabel", "SPR", 6, 34, 27, 15));
			this.bok2_djangoBaseSpr = this.CreateLabel("djangoBaseSpr", "", 35, 34, 31, 15, false, BokInterfaceMainForm.baseStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			
			// STR
			this.currentStatsLabels.Add(this.CreateLabel("strRowLabel", "STR", 6, 49, 27, 15));
			this.bok2_djangoBaseStr = this.CreateLabel("djangoBaseStr", "", 35, 49, 31, 15, false, BokInterfaceMainForm.baseStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			
			// AGI
			this.currentStatsLabels.Add(this.CreateLabel("strRowLabel", "AGI", 6, 64, 27, 15));
			this.bok2_djangoBaseAgi = this.CreateLabel("djangoBaseAgi", "", 35, 64, 31, 15, false, BokInterfaceMainForm.baseStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			
			// Add values labels to group
			this.currentStatsLabels.Add(this.bok2_djangoBaseVit);
			this.currentStatsLabels.Add(this.bok2_djangoBaseSpr);
			this.currentStatsLabels.Add(this.bok2_djangoBaseStr);
			this.currentStatsLabels.Add(this.bok2_djangoBaseAgi);

			// Add elements to group
			for(int i = 0; i < this.currentStatsLabels.Count; i++) {
				this.currentStatsGroupBox.Controls.Add(this.currentStatsLabels[i]);
			}
		}
    }
}
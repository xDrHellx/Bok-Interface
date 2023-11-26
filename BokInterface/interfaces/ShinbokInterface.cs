using System;
using System.Collections.Generic;
using BizHawk.Client.EmuHawk;
using BokInterface.Shinbok;

/**
 * File for the Shinbok (Boktai 3) interface itself
 */

namespace BokInterface {
	
	partial class BokInterfaceMainForm {

		private System.Windows.Forms.Label bok3_currentStatusHpValue = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok3_currentStatusEneValue = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok3_currentStatusTrcValue = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok3_djangoBaseVit = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok3_djangoEquipsVit = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok3_djangoTotalVit = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok3_djangoBaseSpr = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok3_djangoEquipsSpr = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok3_djangoTotalSpr = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok3_djangoBaseStr = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok3_djangoEquipsStr = new System.Windows.Forms.Label();
		private System.Windows.Forms.Label bok3_djangoTotalStr = new System.Windows.Forms.Label();

		private readonly ShinbokAddresses shinbokAddresses = new ShinbokAddresses();
		// private System.Windows.Forms.ProgressBar djangoCurrentHpBar = new System.Windows.Forms.ProgressBar();
		// private System.Windows.Forms.ProgressBar djangoCurrentEneBar = new System.Windows.Forms.ProgressBar();
		// private System.Windows.Forms.ProgressBar djangoCurrentTrcBar = new System.Windows.Forms.ProgressBar();

        private void ShowShinbokInterface() {

			// Current game name
			this.CreateLabel("currentGameName", currentGameName, 5, 5, 176, 15, true);

			// Current status section
			this.AddShinbokCurrentStatusSection();

			// Stats section
			this.AddShinbokCurrentStatsSection();

			// Inventory section
			// this.inventoryGroupBox = this.CreateGroupBox("inventory", "Inventory", 5, 101, 250, 55, true);

			// Main window
			this.SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 350, 500);
			
			this.ResumeLayout(false);
        }

		private void UpdateShinbokInterface() {

			/**
			 * Preparing memory addresses
			 * 
			 * This is necessary because some memory addresses changes based on areas
			 * So we need to combine multiple addresses to get the actual value all the time
			 */
			var djangoCurrentHp = utils.ReadDynamicAddress(shinbokAddresses.Misc["room"], shinbokAddresses.Django["hp"]);
			var djangoCurrentBaseVit = utils.ReadDynamicAddress(shinbokAddresses.Misc["stat"], shinbokAddresses.Django["baseVit"]);
			var djangoCurrentBaseSpr = utils.ReadDynamicAddress(shinbokAddresses.Misc["stat"], shinbokAddresses.Django["baseSpr"]);
			var djangoCurrentBaseStr = utils.ReadDynamicAddress(shinbokAddresses.Misc["stat"], shinbokAddresses.Django["baseStr"]);

			/**
			 * Updating values by retrieving from memory addresses
			 * 
			 * In some cases we only update when the value is "valid"
			 * For example Django's current HP goes below 0 or above 1000 when switching rooms, during bike races or on world map
			 */
			if(djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
				this.bok3_currentStatusHpValue.Text = djangoCurrentHp.ToString();
				this.bok3_djangoBaseVit.Text = djangoCurrentBaseVit.ToString();
				this.bok3_djangoBaseSpr.Text = djangoCurrentBaseSpr.ToString();
				this.bok3_djangoBaseStr.Text = djangoCurrentBaseStr.ToString();

				// utils.WriteDynamicAddress((uint)99, shinbokAddresses.Misc["room"], shinbokAddresses.Django["hp"]);
			}
		}

		private void AddShinbokCurrentStatusSection() {
			
			// Section
			this.currentStatusGroupBox = this.CreateGroupBox("currentStatus", "Current status", 5, 25, 250, 70, true);

			// Current status labels
			this.currentStatusLabels.Add(this.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15));
			this.currentStatusLabels.Add(this.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15));
			this.currentStatusLabels.Add(this.CreateLabel("currentGameNameTrcLabel", "TRC :", 7, 49, 34, 15));

			// Current status values
			this.bok3_currentStatusHpValue = this.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15);
			this.bok3_currentStatusEneValue = this.CreateLabel("djangoCurrentHpValue", "", 44, 34, 31, 15);
			this.bok3_currentStatusTrcValue = this.CreateLabel("djangoCurrentHpValue", "", 44, 49, 31, 15);

			// Add values labels to group
			this.currentStatusLabels.Add(this.bok3_currentStatusHpValue);
			this.currentStatusLabels.Add(this.bok3_currentStatusEneValue);
			this.currentStatusLabels.Add(this.bok3_currentStatusTrcValue);

			// Add elements to group
			for(int i = 0; i < this.currentStatusLabels.Count; i++) {
				this.currentStatusGroupBox.Controls.Add(this.currentStatusLabels[i]);
			}
		}

		private void AddShinbokCurrentStatsSection() {

			// Section
			this.currentStatsGroupBox = this.CreateGroupBox("currentStats", "Stats", 5, 101, 150, 90, true);

			// Column names
			this.currentStatsLabels.Add(this.CreateLabel("baseStatColumnName", "Base", 35, 19, 31, 15, false, BokInterfaceMainForm.baseStatColor, new System.Windows.Forms.Padding(0)));
			this.currentStatsLabels.Add(this.CreateLabel("equipsStatColumnName", "Equips", 66, 19, 42, 15, false, BokInterfaceMainForm.equipsStatColor, new System.Windows.Forms.Padding(0)));
			this.currentStatsLabels.Add(this.CreateLabel("totalStatColumnName", "Total", 108, 19, 32, 15, false, BokInterfaceMainForm.totalStatColor, new System.Windows.Forms.Padding(0)));

			// VIT
			this.currentStatsLabels.Add(this.CreateLabel("vitRowLabel", "VIT", 6, 34, 27, 15));
			this.bok3_djangoBaseVit = this.CreateLabel("djangoBaseVit", "", 35, 34, 31, 15, false, BokInterfaceMainForm.baseStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			this.bok3_djangoEquipsVit = this.CreateLabel("djangoEquipsVit", "", 66, 34, 42, 15, false, BokInterfaceMainForm.equipsStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			this.bok3_djangoTotalVit = this.CreateLabel("djangoTotalVit", "", 108, 34, 32, 15, false, BokInterfaceMainForm.totalStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			
			// SPR
			this.currentStatsLabels.Add(this.CreateLabel("sprRowLabel", "SPR", 6, 49, 27, 15));
			this.bok3_djangoBaseSpr = this.CreateLabel("djangoBaseSpr", "", 35, 49, 31, 15, false, BokInterfaceMainForm.baseStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			this.bok3_djangoEquipsSpr = this.CreateLabel("djangoEquipsSpr", "", 66, 49, 42, 15, false, BokInterfaceMainForm.equipsStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			this.bok3_djangoTotalSpr = this.CreateLabel("djangoTotalSpr", "", 108, 49, 32, 15, false, BokInterfaceMainForm.totalStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			
			// STR
			this.currentStatsLabels.Add(this.CreateLabel("strRowLabel", "STR", 6, 64, 27, 15));
			this.bok3_djangoBaseStr = this.CreateLabel("djangoBaseStr", "", 35, 64, 31, 15, false, BokInterfaceMainForm.baseStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			this.bok3_djangoEquipsStr = this.CreateLabel("djangoEquipsStr", "", 66, 64, 42, 15, false, BokInterfaceMainForm.equipsStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			this.bok3_djangoTotalStr = this.CreateLabel("djangoTotalStr", "", 108, 64, 32, 15, false, BokInterfaceMainForm.totalStatColor, new System.Windows.Forms.Padding(0), "MiddleRight");
			
			// Add values labels to group
			this.currentStatsLabels.Add(this.bok3_djangoBaseVit);
			this.currentStatsLabels.Add(this.bok3_djangoEquipsVit);
			this.currentStatsLabels.Add(this.bok3_djangoTotalVit);
			this.currentStatsLabels.Add(this.bok3_djangoBaseSpr);
			this.currentStatsLabels.Add(this.bok3_djangoEquipsSpr);
			this.currentStatsLabels.Add(this.bok3_djangoTotalSpr);
			this.currentStatsLabels.Add(this.bok3_djangoBaseStr);
			this.currentStatsLabels.Add(this.bok3_djangoEquipsStr);
			this.currentStatsLabels.Add(this.bok3_djangoTotalStr);

			// Add elements to group
			for(int i = 0; i < this.currentStatsLabels.Count; i++) {
				this.currentStatsGroupBox.Controls.Add(this.currentStatsLabels[i]);
			}
		}
	}
}
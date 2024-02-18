using BokInterface.Addresses;

/**
 * File for the Shinbok (Boktai 3) interface itself
 */

namespace BokInterface {
	
	partial class BokInterfaceMainForm {

		#region Properties

		private System.Windows.Forms.Label bok3_currentStatusHpValue = new();
		private System.Windows.Forms.Label bok3_currentStatusEneValue = new();
		private System.Windows.Forms.Label bok3_currentStatusTrcValue = new();
		private System.Windows.Forms.Label bok3_djangoBaseVit = new();
		private System.Windows.Forms.Label bok3_djangoEquipsVit = new();
		private System.Windows.Forms.Label bok3_djangoTotalVit = new();
		private System.Windows.Forms.Label bok3_djangoBaseSpr = new();
		private System.Windows.Forms.Label bok3_djangoEquipsSpr = new();
		private System.Windows.Forms.Label bok3_djangoTotalSpr = new();
		private System.Windows.Forms.Label bok3_djangoBaseStr = new();
		private System.Windows.Forms.Label bok3_djangoEquipsStr = new();
		private System.Windows.Forms.Label bok3_djangoTotalStr = new();
		private System.Windows.Forms.Button bok3_editStatusBtn = new();
		private System.Windows.Forms.Button bok3_editInventoryBtn = new();
		private System.Windows.Forms.Button bok3_editEquipsBtn = new();
		private System.Windows.Forms.Button bok3_editWeaponsBtn = new();
		private System.Windows.Forms.Button bok3_editSolarGunBtn = new();

		private readonly ShinbokAddresses shinbokAddresses = new();

		#endregion

        private void ShowShinbokInterface() {

			// Current game name
			this.CreateLabel("currentGameName", currentGameName, 5, 5, 176, 20, true);

			// Current status section
			this.AddShinbokCurrentStatusSection();

			// Stats section
			this.AddShinbokCurrentStatsSection();

			// Values setting / editing section
			this.AddShinbokEditSection();

			// Extras / misc tools section
			this.AddToolsSection();

			// Inventory section
			// this.inventoryGroupBox = this.CreateGroupBox("inventory", "Inventory", 5, 101, 250, 55, true);

			// Main window
			this.SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 345, 500);
			
			this.ResumeLayout(false);
        }

		private void UpdateShinbokInterface() {

			/**
			 * Preparing memory addresses
			 * 
			 * This is necessary because some memory addresses changes based on areas
			 * So we need to combine multiple addresses to get the actual value all the time
			 */
			var djangoCurrentHp = this.memoryValues.Django["current_hp"].Value;

			/**
			 * Updating values by retrieving from memory addresses
			 * 
			 * In some cases we only update when the value is "valid"
			 * For example Django's current HP goes below 0 or above 1000 when switching rooms, during bike races or on world map
			 */
			if(djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
				this.bok3_currentStatusHpValue.Text = djangoCurrentHp.ToString();
				this.bok3_djangoBaseVit.Text = this.memoryValues.Django["base_vit"].Value.ToString();
				this.bok3_djangoBaseSpr.Text = this.memoryValues.Django["base_spr"].Value.ToString();
				this.bok3_djangoBaseStr.Text = this.memoryValues.Django["base_str"].Value.ToString();
			}
		}

		private void AddShinbokCurrentStatusSection() {
			
			// Section
			this.currentStatusGroupBox = this.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, true);

			// Current status labels
			this.currentStatusLabels.Add(this.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15));
			this.currentStatusLabels.Add(this.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15));
			this.currentStatusLabels.Add(this.CreateLabel("currentGameNameTrcLabel", "TRC :", 7, 49, 34, 15));

			// Current status values
			this.bok3_currentStatusHpValue = this.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15);
			this.bok3_currentStatusEneValue = this.CreateLabel("djangoCurrentEneValue", "", 44, 34, 31, 15);
			this.bok3_currentStatusTrcValue = this.CreateLabel("djangoCurrentTrcValue", "", 44, 49, 31, 15);

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

		private void AddShinbokEditSection() {

			// Section
			this.editGroupBox = this.CreateGroupBox("editButtons", "Edit", 237, 25, 87, 157, true);

			this.bok3_editStatusBtn = CreateButton("editStatuts", "Status", 6, 19, 75, 23);
			this.bok3_editInventoryBtn = CreateButton("editItems", "Items", 6, 46, 75, 23);
			this.bok3_editEquipsBtn = CreateButton("editEquips", "Equips", 6, 73, 75, 23);
			this.bok3_editWeaponsBtn = CreateButton("editWeapons", "Weapons", 6, 100, 75, 23);
			this.bok3_editSolarGunBtn = CreateButton("editSolarGun", "Solar gun", 6, 127, 75, 23);

			// WIP features are disabled for now
			this.bok3_editInventoryBtn.Enabled = false;
			this.bok3_editEquipsBtn.Enabled = false;
			this.bok3_editWeaponsBtn.Enabled = false;
			this.bok3_editSolarGunBtn.Enabled = false;

			// Add onclick events
			this.bok3_editStatusBtn.Click += new System.EventHandler(this.OpenStatusEditor);
			// this.bok3_editInventoryBtn.Click += new System.EventHandler(this.OpenInventoryEditor);
			// this.bok3_editEquipsBtn.Click += new System.EventHandler(this.OpenEquipsEditor);
			// this.bok3_editWeaponsBtn.Click += new System.EventHandler(this.OpenWeaponsEditor);
			// this.bok3_editSolarGunBtn.Click += new System.EventHandler(this.OpenSolarGunEditor);

			// Add buttons to group
			this.editButtons.Add(this.bok3_editStatusBtn);
			this.editButtons.Add(this.bok3_editInventoryBtn);
			this.editButtons.Add(this.bok3_editEquipsBtn);
			this.editButtons.Add(this.bok3_editWeaponsBtn);
			this.editButtons.Add(this.bok3_editSolarGunBtn);

			for(int i = 0; i < this.editButtons.Count; i++) {
				this.editGroupBox.Controls.Add(this.editButtons[i]);
			}
		}
	}
}
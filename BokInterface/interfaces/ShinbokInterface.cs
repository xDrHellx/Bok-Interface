using System.Windows.Forms;

/**
 * File for the Shinbok (Boktai 3) interface itself
 */

namespace BokInterface {

    partial class BokInterface {

        #region Properties

        private Label bok3_currentStatusHpValue = new();
        private Label bok3_currentStatusEneValue = new();
        private Label bok3_currentStatusTrcValue = new();
        private Label bok3_djangoBaseVit = new();
        private Label bok3_djangoEquipsVit = new();
        private Label bok3_djangoTotalVit = new();
        private Label bok3_djangoBaseSpr = new();
        private Label bok3_djangoEquipsSpr = new();
        private Label bok3_djangoTotalSpr = new();
        private Label bok3_djangoBaseStr = new();
        private Label bok3_djangoEquipsStr = new();
        private Label bok3_djangoTotalStr = new();
        private Button bok3_editStatusBtn = new();
        private Button bok3_editInventoryBtn = new();
        private Button bok3_editEquipsBtn = new();
        private Button bok3_editWeaponsBtn = new();
        private Button bok3_editSolarGunBtn = new();

        #endregion

        private void ShowShinbokInterface() {

            // Current game name
            CreateLabel("currentGameName", currentGameName, 5, 5, 176, 20, true);

            // Current status section
            AddShinbokCurrentStatusSection();

            // Stats section
            AddShinbokCurrentStatsSection();

            // Values setting / editing section
            AddShinbokEditSection();

            // Extras / misc tools section
            AddToolsSection();

            // Inventory section
            // this.inventoryGroupBox = this.CreateGroupBox("inventory", "Inventory", 5, 101, 250, 55, true);

            // Main window
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 345, 500);

            ResumeLayout(false);
        }

        private void UpdateShinbokInterface() {

            /**
			 * Preparing memory addresses
			 *
			 * This is necessary because some memory addresses changes based on areas
			 * So we need to combine multiple addresses to get the actual value all the time
			 */
            uint djangoCurrentHp = memoryValues.Django["current_hp"].Value;

            /**
			 * Updating values by retrieving from memory addresses
			 *
			 * In some cases we only update when the value is "valid"
			 * For example Django's current HP goes below 0 or above 1000 when switching rooms, during bike races or on world map
			 */
            if (djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
                bok3_currentStatusHpValue.Text = djangoCurrentHp.ToString();
                bok3_djangoBaseVit.Text = memoryValues.Django["base_vit"].Value.ToString();
                bok3_djangoBaseSpr.Text = memoryValues.Django["base_spr"].Value.ToString();
                bok3_djangoBaseStr.Text = memoryValues.Django["base_str"].Value.ToString();
            }
        }

        private void AddShinbokCurrentStatusSection() {

            // Section
            currentStatusGroupBox = CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, true);

            // Current status labels
            currentStatusLabels.Add(CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15));
            currentStatusLabels.Add(CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15));
            currentStatusLabels.Add(CreateLabel("currentGameNameTrcLabel", "TRC :", 7, 49, 34, 15));

            // Current status values
            bok3_currentStatusHpValue = CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15);
            bok3_currentStatusEneValue = CreateLabel("djangoCurrentEneValue", "", 44, 34, 31, 15);
            bok3_currentStatusTrcValue = CreateLabel("djangoCurrentTrcValue", "", 44, 49, 31, 15);

            // Add values labels to group
            currentStatusLabels.Add(bok3_currentStatusHpValue);
            currentStatusLabels.Add(bok3_currentStatusEneValue);
            currentStatusLabels.Add(bok3_currentStatusTrcValue);

            // Add elements to group
            for (int i = 0; i < currentStatusLabels.Count; i++) {
                currentStatusGroupBox.Controls.Add(currentStatusLabels[i]);
            }
        }

        private void AddShinbokCurrentStatsSection() {

            // Section
            currentStatsGroupBox = CreateGroupBox("currentStats", "Stats", 5, 101, 150, 90, true);

            // Column names
            currentStatsLabels.Add(CreateLabel("baseStatColumnName", "Base", 35, 19, 31, 15, colorHex: baseStatColor));
            currentStatsLabels.Add(CreateLabel("equipsStatColumnName", "Equips", 66, 19, 42, 15, colorHex: equipsStatColor));
            currentStatsLabels.Add(CreateLabel("totalStatColumnName", "Total", 108, 19, 32, 15, colorHex: totalStatColor));

            // VIT
            currentStatsLabels.Add(CreateLabel("vitRowLabel", "VIT", 6, 34, 27, 15, textAlignment: "MiddleLeft"));
            bok3_djangoBaseVit = CreateLabel("djangoBaseVit", "", 35, 34, 31, 15, colorHex: baseStatColor, textAlignment: "MiddleRight");
            bok3_djangoEquipsVit = CreateLabel("djangoEquipsVit", "", 66, 34, 42, 15, colorHex: equipsStatColor, textAlignment: "MiddleRight");
            bok3_djangoTotalVit = CreateLabel("djangoTotalVit", "", 108, 34, 32, 15, colorHex: totalStatColor, textAlignment: "MiddleRight");

            // SPR
            currentStatsLabels.Add(CreateLabel("sprRowLabel", "SPR", 6, 49, 27, 15, textAlignment: "MiddleLeft"));
            bok3_djangoBaseSpr = CreateLabel("djangoBaseSpr", "", 35, 49, 31, 15, colorHex: baseStatColor, textAlignment: "MiddleRight");
            bok3_djangoEquipsSpr = CreateLabel("djangoEquipsSpr", "", 66, 49, 42, 15, colorHex: equipsStatColor, textAlignment: "MiddleRight");
            bok3_djangoTotalSpr = CreateLabel("djangoTotalSpr", "", 108, 49, 32, 15, colorHex: totalStatColor, textAlignment: "MiddleRight");

            // STR
            currentStatsLabels.Add(CreateLabel("strRowLabel", "STR", 6, 64, 27, 15, textAlignment: "MiddleLeft"));
            bok3_djangoBaseStr = CreateLabel("djangoBaseStr", "", 35, 64, 31, 15, colorHex: baseStatColor, textAlignment: "MiddleRight");
            bok3_djangoEquipsStr = CreateLabel("djangoEquipsStr", "", 66, 64, 42, 15, colorHex: equipsStatColor, textAlignment: "MiddleRight");
            bok3_djangoTotalStr = CreateLabel("djangoTotalStr", "", 108, 64, 32, 15, colorHex: totalStatColor, textAlignment: "MiddleRight");

            // Add values labels to group
            currentStatsLabels.Add(bok3_djangoBaseVit);
            currentStatsLabels.Add(bok3_djangoEquipsVit);
            currentStatsLabels.Add(bok3_djangoTotalVit);
            currentStatsLabels.Add(bok3_djangoBaseSpr);
            currentStatsLabels.Add(bok3_djangoEquipsSpr);
            currentStatsLabels.Add(bok3_djangoTotalSpr);
            currentStatsLabels.Add(bok3_djangoBaseStr);
            currentStatsLabels.Add(bok3_djangoEquipsStr);
            currentStatsLabels.Add(bok3_djangoTotalStr);

            // Add elements to group
            for (int i = 0; i < currentStatsLabels.Count; i++) {
                currentStatsGroupBox.Controls.Add(currentStatsLabels[i]);
            }
        }

        private void AddShinbokEditSection() {

            // Section
            editGroupBox = CreateGroupBox("editButtons", "Edit", 237, 25, 87, 157, true);

            bok3_editStatusBtn = CreateButton("editStatuts", "Status", 6, 19, 75, 23);
            bok3_editInventoryBtn = CreateButton("editItems", "Items", 6, 46, 75, 23);
            bok3_editEquipsBtn = CreateButton("editEquips", "Equips", 6, 73, 75, 23);
            bok3_editWeaponsBtn = CreateButton("editWeapons", "Weapons", 6, 100, 75, 23);
            bok3_editSolarGunBtn = CreateButton("editSolarGun", "Solar gun", 6, 127, 75, 23);

            // WIP features are disabled for now
            bok3_editInventoryBtn.Enabled = false;
            bok3_editEquipsBtn.Enabled = false;
            bok3_editWeaponsBtn.Enabled = false;
            bok3_editSolarGunBtn.Enabled = false;

            // Add onclick events
            bok3_editStatusBtn.Click += new System.EventHandler(OpenStatusEditor);
            // this.bok3_editInventoryBtn.Click += new System.EventHandler(this.OpenInventoryEditor);
            // this.bok3_editEquipsBtn.Click += new System.EventHandler(this.OpenEquipsEditor);
            // this.bok3_editWeaponsBtn.Click += new System.EventHandler(this.OpenWeaponsEditor);
            // this.bok3_editSolarGunBtn.Click += new System.EventHandler(this.OpenSolarGunEditor);

            // Add buttons to group
            editButtons.Add(bok3_editStatusBtn);
            editButtons.Add(bok3_editInventoryBtn);
            editButtons.Add(bok3_editEquipsBtn);
            editButtons.Add(bok3_editWeaponsBtn);
            editButtons.Add(bok3_editSolarGunBtn);

            for (int i = 0; i < editButtons.Count; i++) {
                editGroupBox.Controls.Add(editButtons[i]);
            }
        }
    }
}

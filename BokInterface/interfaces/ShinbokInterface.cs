using System.Windows.Forms;

using BokInterface.All;

/**
 * File for the Shinbok (Boktai 3) interface itself
 */

namespace BokInterface {

    partial class BokInterface {

        #region Properties

        private Label _bok3_currentStatusHpValue = new();
        private Label _bok3_currentStatusEneValue = new();
        private Label _bok3_currentStatusTrcValue = new();
        private Label _bok3_djangoBaseVit = new();
        private Label _bok3_djangoEquipsVit = new();
        private Label _bok3_djangoTotalVit = new();
        private Label _bok3_djangoBaseSpr = new();
        private Label _bok3_djangoEquipsSpr = new();
        private Label _bok3_djangoTotalSpr = new();
        private Label _bok3_djangoBaseStr = new();
        private Label _bok3_djangoEquipsStr = new();
        private Label _bok3_djangoTotalStr = new();
        private Button _bok3_editStatusBtn = new();
        private Button _bok3_editInventoryBtn = new();
        private Button _bok3_editEquipsBtn = new();
        private Button _bok3_editWeaponsBtn = new();
        private Button _bok3_editSolarGunBtn = new();

        #endregion

        private void ShowShinbokInterface() {

            // Current game name
            WinFormHelpers.CreateLabel("currentGameName", currentGameName, 5, 5, 176, 20, this);

            // Current status section
            AddShinbokCurrentStatusSection();

            // Stats section
            AddShinbokCurrentStatsSection();

            // Values setting / editing section
            AddShinbokEditSection();

            // Extras / misc tools section
            AddToolsSection();

            // Inventory section
            // this.inventoryGroupBox = WinFormHelpers.WinFormHelpers.CreateGroupBox("inventory", "Inventory", 5, 101, 250, 55, this);

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
            uint djangoCurrentHp = _memoryValues.Django["current_hp"].Value;

            /**
			 * Updating values by retrieving from memory addresses
			 *
			 * In some cases we only update when the value is "valid"
			 * For example Django's current HP goes below 0 or above 1000 when switching rooms, during bike races or on world map
			 */
            if (djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
                _bok3_currentStatusHpValue.Text = djangoCurrentHp.ToString();
                _bok3_djangoBaseVit.Text = _memoryValues.Django["base_vit"].Value.ToString();
                _bok3_djangoBaseSpr.Text = _memoryValues.Django["base_spr"].Value.ToString();
                _bok3_djangoBaseStr.Text = _memoryValues.Django["base_str"].Value.ToString();
            }
        }

        private void AddShinbokCurrentStatusSection() {

            // Section
            currentStatusGroupBox = WinFormHelpers.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, this);

            // Current status labels
            currentStatusLabels.Add(WinFormHelpers.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15));
            currentStatusLabels.Add(WinFormHelpers.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15));
            currentStatusLabels.Add(WinFormHelpers.CreateLabel("currentGameNameTrcLabel", "TRC :", 7, 49, 34, 15));

            // Current status values
            _bok3_currentStatusHpValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15);
            _bok3_currentStatusEneValue = WinFormHelpers.CreateLabel("djangoCurrentEneValue", "", 44, 34, 31, 15);
            _bok3_currentStatusTrcValue = WinFormHelpers.CreateLabel("djangoCurrentTrcValue", "", 44, 49, 31, 15);

            // Add values labels to group
            currentStatusLabels.Add(_bok3_currentStatusHpValue);
            currentStatusLabels.Add(_bok3_currentStatusEneValue);
            currentStatusLabels.Add(_bok3_currentStatusTrcValue);

            // Add elements to group
            for (int i = 0; i < currentStatusLabels.Count; i++) {
                currentStatusGroupBox.Controls.Add(currentStatusLabels[i]);
            }
        }

        private void AddShinbokCurrentStatsSection() {

            // Section
            currentStatsGroupBox = WinFormHelpers.CreateGroupBox("currentStats", "Stats", 5, 101, 150, 90, this);

            // Column names
            currentStatsLabels.Add(WinFormHelpers.CreateLabel("baseStatColumnName", "Base", 35, 19, 31, 15, colorHex: WinFormHelpers.baseStatColor));
            currentStatsLabels.Add(WinFormHelpers.CreateLabel("equipsStatColumnName", "Equips", 66, 19, 42, 15, colorHex: WinFormHelpers.equipsStatColor));
            currentStatsLabels.Add(WinFormHelpers.CreateLabel("totalStatColumnName", "Total", 108, 19, 32, 15, colorHex: WinFormHelpers.totalStatColor));

            // VIT
            currentStatsLabels.Add(WinFormHelpers.CreateLabel("vitRowLabel", "VIT", 6, 34, 27, 15, textAlignment: "MiddleLeft"));
            _bok3_djangoBaseVit = WinFormHelpers.CreateLabel("djangoBaseVit", "", 35, 34, 31, 15, colorHex: WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
            _bok3_djangoEquipsVit = WinFormHelpers.CreateLabel("djangoEquipsVit", "", 66, 34, 42, 15, colorHex: WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoTotalVit = WinFormHelpers.CreateLabel("djangoTotalVit", "", 108, 34, 32, 15, colorHex: WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");

            // SPR
            currentStatsLabels.Add(WinFormHelpers.CreateLabel("sprRowLabel", "SPR", 6, 49, 27, 15, textAlignment: "MiddleLeft"));
            _bok3_djangoBaseSpr = WinFormHelpers.CreateLabel("djangoBaseSpr", "", 35, 49, 31, 15, colorHex: WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
            _bok3_djangoEquipsSpr = WinFormHelpers.CreateLabel("djangoEquipsSpr", "", 66, 49, 42, 15, colorHex: WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoTotalSpr = WinFormHelpers.CreateLabel("djangoTotalSpr", "", 108, 49, 32, 15, colorHex: WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");

            // STR
            currentStatsLabels.Add(WinFormHelpers.CreateLabel("strRowLabel", "STR", 6, 64, 27, 15, textAlignment: "MiddleLeft"));
            _bok3_djangoBaseStr = WinFormHelpers.CreateLabel("djangoBaseStr", "", 35, 64, 31, 15, colorHex: WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
            _bok3_djangoEquipsStr = WinFormHelpers.CreateLabel("djangoEquipsStr", "", 66, 64, 42, 15, colorHex: WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoTotalStr = WinFormHelpers.CreateLabel("djangoTotalStr", "", 108, 64, 32, 15, colorHex: WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");

            // Add values labels to group
            currentStatsLabels.Add(_bok3_djangoBaseVit);
            currentStatsLabels.Add(_bok3_djangoEquipsVit);
            currentStatsLabels.Add(_bok3_djangoTotalVit);
            currentStatsLabels.Add(_bok3_djangoBaseSpr);
            currentStatsLabels.Add(_bok3_djangoEquipsSpr);
            currentStatsLabels.Add(_bok3_djangoTotalSpr);
            currentStatsLabels.Add(_bok3_djangoBaseStr);
            currentStatsLabels.Add(_bok3_djangoEquipsStr);
            currentStatsLabels.Add(_bok3_djangoTotalStr);

            // Add elements to group
            for (int i = 0; i < currentStatsLabels.Count; i++) {
                currentStatsGroupBox.Controls.Add(currentStatsLabels[i]);
            }
        }

        private void AddShinbokEditSection() {

            // Section
            editGroupBox = WinFormHelpers.CreateGroupBox("editButtons", "Edit", 237, 25, 87, 157, this);

            _bok3_editStatusBtn = WinFormHelpers.CreateButton("editStatuts", "Status", 6, 19, 75, 23);
            _bok3_editInventoryBtn = WinFormHelpers.CreateButton("editItems", "Items", 6, 46, 75, 23);
            _bok3_editEquipsBtn = WinFormHelpers.CreateButton("editEquips", "Equips", 6, 73, 75, 23);
            _bok3_editWeaponsBtn = WinFormHelpers.CreateButton("editWeapons", "Weapons", 6, 100, 75, 23);
            _bok3_editSolarGunBtn = WinFormHelpers.CreateButton("editSolarGun", "Solar gun", 6, 127, 75, 23);

            // WIP features are disabled for now
            _bok3_editInventoryBtn.Enabled = false;
            _bok3_editEquipsBtn.Enabled = false;
            _bok3_editWeaponsBtn.Enabled = false;
            _bok3_editSolarGunBtn.Enabled = false;

            // Add onclick events
            _bok3_editStatusBtn.Click += new System.EventHandler(OpenStatusEditor);
            // this.bok3_editInventoryBtn.Click += new System.EventHandler(this.OpenInventoryEditor);
            // this.bok3_editEquipsBtn.Click += new System.EventHandler(this.OpenEquipsEditor);
            // this.bok3_editWeaponsBtn.Click += new System.EventHandler(this.OpenWeaponsEditor);
            // this.bok3_editSolarGunBtn.Click += new System.EventHandler(this.OpenSolarGunEditor);

            // Add buttons to group
            editButtons.Add(_bok3_editStatusBtn);
            editButtons.Add(_bok3_editInventoryBtn);
            editButtons.Add(_bok3_editEquipsBtn);
            editButtons.Add(_bok3_editWeaponsBtn);
            editButtons.Add(_bok3_editSolarGunBtn);

            for (int i = 0; i < editButtons.Count; i++) {
                editGroupBox.Controls.Add(editButtons[i]);
            }
        }
    }
}

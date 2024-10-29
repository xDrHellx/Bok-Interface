using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

/**
 * File for the Shinbok (Boktai 3) interface itself
 */

namespace BokInterface {

    partial class BokInterface {

        #region Properties
        private readonly ShinbokAddresses _shinbokAddresses = new();
        private Label _bok3_currentStatusHpValue = new(),
            _bok3_currentStatusEneValue = new(),
            _bok3_currentStatusTrcValue = new(),
            _bok3_djangoBaseVit = new(),
            _bok3_djangoCardsVit = new(),
            _bok3_djangoEquipsVit = new(),
            _bok3_djangoTotalVit = new(),
            _bok3_djangoBaseSpr = new(),
            _bok3_djangoCardsSpr = new(),
            _bok3_djangoEquipsSpr = new(),
            _bok3_djangoTotalSpr = new(),
            _bok3_djangoBaseStr = new(),
            _bok3_djangoCardsStr = new(),
            _bok3_djangoEquipsStr = new(),
            _bok3_djangoTotalStr = new();
        private Button _bok3_editStatusBtn = new(),
            _bok3_editInventoryBtn = new(),
            _bok3_editKeyItemsBtn = new(),
            _bok3_editEquipsBtn = new(),
            _bok3_editWeaponsBtn = new(),
            _bok3_editSolarGunBtn = new();

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
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 339, 500);

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
                _bok3_currentStatusEneValue.Text = _memoryValues.Django["current_ene"].Value.ToString();
                _bok3_currentStatusTrcValue.Text = _memoryValues.Django["current_trc"].Value.ToString();
                _bok3_djangoBaseVit.Text = _memoryValues.Django["base_vit"].Value.ToString();
                _bok3_djangoBaseSpr.Text = _memoryValues.Django["base_spr"].Value.ToString();
                _bok3_djangoBaseStr.Text = _memoryValues.Django["base_str"].Value.ToString();
                _bok3_djangoCardsVit.Text = _memoryValues.Django["cards_vit"].Value.ToString();
                _bok3_djangoCardsSpr.Text = _memoryValues.Django["cards_spr"].Value.ToString();
                _bok3_djangoCardsStr.Text = _memoryValues.Django["cards_str"].Value.ToString();
                _bok3_djangoEquipsVit.Text = _memoryValues.Django["equips_vit"].Value.ToString();
                _bok3_djangoEquipsSpr.Text = _memoryValues.Django["equips_spr"].Value.ToString();
                _bok3_djangoEquipsStr.Text = _memoryValues.Django["equips_str"].Value.ToString();
                _bok3_djangoTotalVit.Text = (_memoryValues.Django["base_vit"].Value + _memoryValues.Django["cards_vit"].Value + _memoryValues.Django["equips_vit"].Value).ToString();
                _bok3_djangoTotalSpr.Text = (_memoryValues.Django["base_spr"].Value + _memoryValues.Django["cards_spr"].Value + _memoryValues.Django["equips_spr"].Value).ToString();
                _bok3_djangoTotalStr.Text = (_memoryValues.Django["base_str"].Value + _memoryValues.Django["cards_str"].Value + _memoryValues.Django["equips_str"].Value).ToString();
            }
        }

        private void AddShinbokCurrentStatusSection() {

            // Section
            currentStatusGroupBox = WinFormHelpers.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, this);

            // Current status labels
            WinFormHelpers.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15, currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15, currentStatusGroupBox);
            WinFormHelpers.CreateLabel("currentGameNameTrcLabel", "TRC :", 7, 49, 34, 15, currentStatusGroupBox);

            // Current status values
            _bok3_currentStatusHpValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15, currentStatusGroupBox);
            _bok3_currentStatusEneValue = WinFormHelpers.CreateLabel("djangoCurrentEneValue", "", 44, 34, 31, 15, currentStatusGroupBox);
            _bok3_currentStatusTrcValue = WinFormHelpers.CreateLabel("djangoCurrentTrcValue", "", 44, 49, 31, 15, currentStatusGroupBox);
        }

        private void AddShinbokCurrentStatsSection() {

            // Section
            currentStatsGroupBox = WinFormHelpers.CreateGroupBox("currentStats", "Stats", 5, 101, 184, 87, this);

            // Column names
            WinFormHelpers.CreateLabel("baseStatColumnName", "Base", 35, 19, 31, 15, currentStatsGroupBox, WinFormHelpers.baseStatColor);
            WinFormHelpers.CreateLabel("cardsStatColumnName", "Cards", 66, 19, 37, 15, currentStatsGroupBox, WinFormHelpers.cardsStatColor);
            WinFormHelpers.CreateLabel("equipsStatColumnName", "Equips", 103, 19, 42, 15, currentStatsGroupBox, WinFormHelpers.equipsStatColor);
            WinFormHelpers.CreateLabel("totalStatColumnName", "Total", 145, 19, 32, 15, currentStatsGroupBox, WinFormHelpers.totalStatColor);

            // VIT
            WinFormHelpers.CreateLabel("vitRowLabel", "VIT", 6, 34, 27, 15, currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok3_djangoBaseVit = WinFormHelpers.CreateLabel("djangoBaseVit", "", 35, 34, 31, 15, currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
            _bok3_djangoCardsVit = WinFormHelpers.CreateLabel("djangoCardsVit", "", 66, 34, 37, 15, currentStatsGroupBox, WinFormHelpers.cardsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoEquipsVit = WinFormHelpers.CreateLabel("djangoEquipsVit", "", 103, 34, 42, 15, currentStatsGroupBox, WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoTotalVit = WinFormHelpers.CreateLabel("djangoTotalVit", "", 145, 34, 32, 15, currentStatsGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");

            // SPR
            WinFormHelpers.CreateLabel("sprRowLabel", "SPR", 6, 49, 27, 15, currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok3_djangoBaseSpr = WinFormHelpers.CreateLabel("djangoBaseSpr", "", 35, 49, 31, 15, currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
            _bok3_djangoCardsSpr = WinFormHelpers.CreateLabel("djangoCardsSpr", "", 66, 49, 37, 15, currentStatsGroupBox, WinFormHelpers.cardsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoEquipsSpr = WinFormHelpers.CreateLabel("djangoEquipsSpr", "", 103, 49, 42, 15, currentStatsGroupBox, WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoTotalSpr = WinFormHelpers.CreateLabel("djangoTotalSpr", "", 145, 49, 32, 15, currentStatsGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");

            // STR
            WinFormHelpers.CreateLabel("strRowLabel", "STR", 6, 64, 27, 15, currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok3_djangoBaseStr = WinFormHelpers.CreateLabel("djangoBaseStr", "", 35, 64, 31, 15, currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
            _bok3_djangoCardsStr = WinFormHelpers.CreateLabel("djangoCardsStr", "", 66, 64, 37, 15, currentStatsGroupBox, WinFormHelpers.cardsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoEquipsStr = WinFormHelpers.CreateLabel("djangoEquipsStr", "", 103, 64, 42, 15, currentStatsGroupBox, WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoTotalStr = WinFormHelpers.CreateLabel("djangoTotalStr", "", 145, 64, 32, 15, currentStatsGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");
        }

        private void AddShinbokEditSection() {

            // Section
            editGroupBox = WinFormHelpers.CreateGroupBox("editButtons", "Edit", 237, 25, 97, 157, this);

            _bok3_editStatusBtn = WinFormHelpers.CreateButton("editStatuts", "Status", 6, 19, 85, 23, editGroupBox);
            _bok3_editInventoryBtn = WinFormHelpers.CreateButton("editItems", "Items", 6, 46, 85, 23, editGroupBox);
            _bok3_editKeyItemsBtn = WinFormHelpers.CreateButton("editKeyItems", "Key items", 6, 73, 85, 23, editGroupBox);
            _bok3_editWeaponsBtn = WinFormHelpers.CreateButton("editWeapons", "Weapons", 6, 100, 85, 23, editGroupBox);
            _bok3_editEquipsBtn = WinFormHelpers.CreateButton("editEquips", "Accessories", 6, 127, 85, 23, editGroupBox);
            // _bok3_editSolarGunBtn = WinFormHelpers.CreateButton("editSolarGun", "Solar gun", 6, 127, 85, 23, editGroupBox);

            // WIP features are disabled for now
            _bok3_editWeaponsBtn.Enabled = false;
            // _bok3_editSolarGunBtn.Enabled = false;

            // Add onclick events
            _bok3_editStatusBtn.Click += new System.EventHandler(OpenStatusEditor);
            _bok3_editInventoryBtn.Click += new System.EventHandler(OpenInventoryEditor);
            _bok3_editKeyItemsBtn.Click += new System.EventHandler(OpenKeyItemsEditor);
            // _bok3_editWeaponsBtn.Click += new System.EventHandler(OpenWeaponsEditor);
            _bok3_editEquipsBtn.Click += new System.EventHandler(OpenEquipsEditor);
            // _bok3_editSolarGunBtn.Click += new System.EventHandler(OpenSolarGunEditor);
        }
    }
}

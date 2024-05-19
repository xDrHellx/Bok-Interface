using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

/**
 * File for the Zoktai (Boktai 2) interface itself
 */

namespace BokInterface {

    partial class BokInterface {

        #region Properties

        private readonly ZoktaiAddresses _zoktaiAddresses = new();
        private Label _bok2_currentStatusHpValue = new();
        private Label _bok2_currentStatusEneValue = new();
        private Label _bok2_djangoLevel = new();
        private Label _bok2_djangoExp = new();
        private Label _bok2_djangoVit = new();
        private Label _bok2_djangoSpr = new();
        private Label _bok2_djangoStr = new();
        private Label _bok2_djangoAgi = new();
        private Label _bok2_djangoStatPoints = new();
        private Label _bok2_djangoSwordSkill = new();
        private Label _bok2_djangoSpearSkill = new();
        private Label _bok2_djangoHammerSkill = new();
        private Label _bok2_djangoFistsSkill = new();
        private Label _bok2_djangoGunSkill = new();
        private Button _bok2_editStatusBtn = new();
        private Button _bok2_editInventoryBtn = new();
        private Button _bok2_editEquipsBtn = new();
        private Button _bok2_editWeaponsBtn = new();
        private Button _bok2_editMagicsBtn = new();
        private GroupBox _bok2_currentSkillGroupBox = new();
        private readonly List<Label> _bok2_currentSkillLabels = [];

        #endregion

        private void ShowZoktaiInterface() {

            // If JP version, update the game name label to add the version
            string version = "";
            int gameNameLabelWidth = 145;
            if (currentGameId == 1244803925) {
                version = Utilities.GetGameVersion() == 1 ? " (v1.1)" : " (v1.0)";
                gameNameLabelWidth = 180;
            }

            // Current game name
            WinFormHelpers.CreateLabel("currentGameName", currentGameName + version, 5, 5, gameNameLabelWidth, 20, this);

            // Current status section
            AddZoktaiCurrentStatusSection();

            // Stats section
            AddZoktaiCurrentStatsSection();

            // Skill sections
            AddZoktaiCurrentSkillSection();

            // Values setting / editing section
            AddZoktaiEditSection();

            // Extras / misc tools section
            AddToolsSection();

            // Main window
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 345, 500);

            ResumeLayout(false);
        }

        private void UpdateZoktaiInterface() {

            // Get one of the values used for reading current stats
            uint currentStat = _zoktaiAddresses.Misc["current_stat"].Value;

            /**
             * Update values by retrieving from memory addresses
             * 
             * In some cases we only update when the values are "valid"
             * For example "stat" is 0 during room transitions or at the title screen
             */
            if (currentStat > 0) {
                _bok2_currentStatusHpValue.Text = _memoryValues.Django["current_hp"].Value.ToString();
                _bok2_currentStatusEneValue.Text = _memoryValues.Django["current_ene"].Value.ToString();

                _bok2_djangoLevel.Text = _memoryValues.Django["level"].Value.ToString();
                _bok2_djangoExp.Text = _memoryValues.Django["exp"].Value.ToString();

                _bok2_djangoVit.Text = _memoryValues.Misc["vit"].Value.ToString();
                _bok2_djangoSpr.Text = _memoryValues.Misc["spr"].Value.ToString();
                _bok2_djangoStr.Text = _memoryValues.Misc["str"].Value.ToString();
                _bok2_djangoAgi.Text = _memoryValues.Misc["agi"].Value.ToString();
                _bok2_djangoStatPoints.Text = _memoryValues.Django["stat_points"].Value.ToString();

                _bok2_djangoSwordSkill.Text = Utilities.ExpToLevel(_memoryValues.Django["sword_skill"].Value).ToString();
                _bok2_djangoSpearSkill.Text = Utilities.ExpToLevel(_memoryValues.Django["spear_skill"].Value).ToString();
                _bok2_djangoHammerSkill.Text = Utilities.ExpToLevel(_memoryValues.Django["hammer_skill"].Value).ToString();
                _bok2_djangoFistsSkill.Text = Utilities.ExpToLevel(_memoryValues.Django["fists_skill"].Value).ToString();
                _bok2_djangoGunSkill.Text = Utilities.ExpToLevel(_memoryValues.Django["gun_skill"].Value).ToString();

                // Add / refresh tooltips for skill containing the EXP amount
                WinFormHelpers.AddToolTip(_bok2_djangoSwordSkill, _memoryValues.Django["sword_skill"].Value + " EXP");
                WinFormHelpers.AddToolTip(_bok2_djangoSpearSkill, _memoryValues.Django["spear_skill"].Value + " EXP");
                WinFormHelpers.AddToolTip(_bok2_djangoHammerSkill, _memoryValues.Django["hammer_skill"].Value + " EXP");
                WinFormHelpers.AddToolTip(_bok2_djangoFistsSkill, _memoryValues.Django["fists_skill"].Value + " EXP");
                WinFormHelpers.AddToolTip(_bok2_djangoGunSkill, _memoryValues.Django["gun_skill"].Value + " EXP");
            }
        }

        private void AddZoktaiCurrentStatusSection() {

            // Section
            currentStatusGroupBox = WinFormHelpers.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 55, this);

            // Current status labels
            currentStatusLabels.Add(WinFormHelpers.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15));
            currentStatusLabels.Add(WinFormHelpers.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15));
            currentStatusLabels.Add(WinFormHelpers.CreateLabel("djangoCurrentLevelLabel", "Level :", 93, 19, 40, 15));
            currentStatusLabels.Add(WinFormHelpers.CreateLabel("djangoCurrentExpLabel", "EXP :", 93, 34, 33, 15));

            // Current status values
            _bok2_currentStatusHpValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15);
            _bok2_currentStatusEneValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 34, 31, 15);
            _bok2_djangoLevel = WinFormHelpers.CreateLabel("djangoCurrentLevelValue", "", 132, 19, 31, 15);
            _bok2_djangoExp = WinFormHelpers.CreateLabel("djangoCurrentExpValue", "", 132, 34, 43, 15);

            // Add values labels to group
            currentStatusLabels.Add(_bok2_currentStatusHpValue);
            currentStatusLabels.Add(_bok2_currentStatusEneValue);
            currentStatusLabels.Add(_bok2_djangoLevel);
            currentStatusLabels.Add(_bok2_djangoExp);

            // Add elements to group
            for (int i = 0; i < currentStatusLabels.Count; i++) {
                currentStatusGroupBox.Controls.Add(currentStatusLabels[i]);
            }
        }

        private void AddZoktaiCurrentSkillSection() {

            // Section
            _bok2_currentSkillGroupBox = WinFormHelpers.CreateGroupBox("currentSkill", "Skill", 92, 86, 110, 104, this);

            // Sword
            _bok2_currentSkillLabels.Add(WinFormHelpers.CreateLabel("swordSkillLabel", "Sword", 6, 20, 54, 15, textAlignment: "MiddleLeft"));
            _bok2_djangoSwordSkill = WinFormHelpers.CreateLabel("djangoSwordSkill", "", 60, 19, 41, 15, colorHex: WinFormHelpers.totalStatColor, textAlignment: "MiddleLeft");

            // Spear
            _bok2_currentSkillLabels.Add(WinFormHelpers.CreateLabel("spearSkillLabel", "Spear", 6, 34, 54, 15, textAlignment: "MiddleLeft"));
            _bok2_djangoSpearSkill = WinFormHelpers.CreateLabel("djangoSpearSkill", "", 60, 34, 41, 15, colorHex: WinFormHelpers.totalStatColor, textAlignment: "MiddleLeft");

            // Hammer
            _bok2_currentSkillLabels.Add(WinFormHelpers.CreateLabel("hammerSkillLabel", "Hammer", 6, 49, 54, 15, textAlignment: "MiddleLeft"));
            _bok2_djangoHammerSkill = WinFormHelpers.CreateLabel("djangoHammerSkill", "", 60, 49, 41, 15, colorHex: WinFormHelpers.totalStatColor, textAlignment: "MiddleLeft");

            // Fists
            _bok2_currentSkillLabels.Add(WinFormHelpers.CreateLabel("fistsSkillLabel", "Fists", 6, 64, 54, 15, textAlignment: "MiddleLeft"));
            _bok2_djangoFistsSkill = WinFormHelpers.CreateLabel("djangoFistsSkill", "", 60, 64, 41, 15, colorHex: WinFormHelpers.totalStatColor, textAlignment: "MiddleLeft");

            // Gun
            _bok2_currentSkillLabels.Add(WinFormHelpers.CreateLabel("gunSkillLabel", "Gun", 6, 79, 54, 15, textAlignment: "MiddleLeft"));
            _bok2_djangoGunSkill = WinFormHelpers.CreateLabel("djangoGunSkill", "", 60, 79, 41, 15, colorHex: WinFormHelpers.totalStatColor, textAlignment: "MiddleLeft");

            // Add values labels to group
            _bok2_currentSkillLabels.Add(_bok2_djangoSwordSkill);
            _bok2_currentSkillLabels.Add(_bok2_djangoSpearSkill);
            _bok2_currentSkillLabels.Add(_bok2_djangoHammerSkill);
            _bok2_currentSkillLabels.Add(_bok2_djangoFistsSkill);
            _bok2_currentSkillLabels.Add(_bok2_djangoGunSkill);

            for (int i = 0; i < _bok2_currentSkillLabels.Count; i++) {
                _edit_statusLabels.Add(_bok2_currentSkillLabels[i]);
            }

            // Add elements to group
            for (int i = 0; i < _bok2_currentSkillLabels.Count; i++) {
                _bok2_currentSkillGroupBox.Controls.Add(_bok2_currentSkillLabels[i]);
            }
        }

        private void AddZoktaiCurrentStatsSection() {

            // Section
            currentStatsGroupBox = WinFormHelpers.CreateGroupBox("currentStats", "Stats", 5, 86, 75, 106, this);

            // VIT
            currentStatsLabels.Add(WinFormHelpers.CreateLabel("vitRowLabel", "VIT", 6, 19, 27, 15, textAlignment: "MiddleLeft"));
            _bok2_djangoVit = WinFormHelpers.CreateLabel("djangoVit", "", 35, 19, 31, 15, colorHex: WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // SPR
            currentStatsLabels.Add(WinFormHelpers.CreateLabel("sprRowLabel", "SPR", 6, 34, 27, 15, textAlignment: "MiddleLeft"));
            _bok2_djangoSpr = WinFormHelpers.CreateLabel("djangoSpr", "", 35, 34, 31, 15, colorHex: WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // STR
            currentStatsLabels.Add(WinFormHelpers.CreateLabel("strRowLabel", "STR", 6, 49, 27, 15, textAlignment: "MiddleLeft"));
            _bok2_djangoStr = WinFormHelpers.CreateLabel("djangoStr", "", 35, 49, 31, 15, colorHex: WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // AGI
            currentStatsLabels.Add(WinFormHelpers.CreateLabel("agiRowLabel", "AGI", 6, 64, 27, 15, textAlignment: "MiddleLeft"));
            _bok2_djangoAgi = WinFormHelpers.CreateLabel("djangoAgi", "", 35, 64, 31, 15, colorHex: WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // Stat points to allocate
            currentStatsLabels.Add(WinFormHelpers.CreateLabel("statPointsLabel", "Add", 6, 84, 29, 15, textAlignment: "MiddleLeft"));
            _bok2_djangoStatPoints = WinFormHelpers.CreateLabel("djangoStatPoints", "", 35, 84, 31, 15, colorHex: WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");

            // Add values labels to group
            currentStatsLabels.Add(_bok2_djangoVit);
            currentStatsLabels.Add(_bok2_djangoSpr);
            currentStatsLabels.Add(_bok2_djangoStr);
            currentStatsLabels.Add(_bok2_djangoAgi);
            currentStatsLabels.Add(_bok2_djangoStatPoints);

            // Add elements to group
            for (int i = 0; i < currentStatsLabels.Count; i++) {
                currentStatsGroupBox.Controls.Add(currentStatsLabels[i]);
            }
        }

        private void AddZoktaiEditSection() {

            // Section
            editGroupBox = WinFormHelpers.CreateGroupBox("editButtons", "Edit", 237, 25, 87, 157, this);

            _bok2_editStatusBtn = WinFormHelpers.CreateButton("editStatuts", "Status", 6, 19, 75, 23);
            _bok2_editInventoryBtn = WinFormHelpers.CreateButton("editItems", "Items", 6, 46, 75, 23);
            _bok2_editEquipsBtn = WinFormHelpers.CreateButton("editEquips", "Equips", 6, 73, 75, 23);
            _bok2_editWeaponsBtn = WinFormHelpers.CreateButton("editWeapons", "Weapons", 6, 100, 75, 23);
            _bok2_editMagicsBtn = WinFormHelpers.CreateButton("editMagics", "Magics", 6, 127, 75, 23);

            // WIP features are disabled for now
            _bok2_editInventoryBtn.Enabled = false;
            _bok2_editEquipsBtn.Enabled = false;
            _bok2_editWeaponsBtn.Enabled = false;
            _bok2_editMagicsBtn.Enabled = false;

            // Add onclick events
            _bok2_editStatusBtn.Click += new System.EventHandler(OpenStatusEditor);
            // this.bok2_editInventoryBtn.Click += new System.EventHandler(this.OpenInventoryEditor);
            // this.bok2_editEquipsBtn.Click += new System.EventHandler(this.OpenEquipsEditor);
            // this.bok2_editWeaponsBtn.Click += new System.EventHandler(this.OpenWeaponsEditor);
            // this.bok2_editMagicsBtn.Click += new System.EventHandler(this.OpenMagicsEditor);

            // Add buttons to group
            editButtons.Add(_bok2_editStatusBtn);
            editButtons.Add(_bok2_editInventoryBtn);
            editButtons.Add(_bok2_editEquipsBtn);
            editButtons.Add(_bok2_editWeaponsBtn);
            editButtons.Add(_bok2_editMagicsBtn);

            for (int i = 0; i < editButtons.Count; i++) {
                editGroupBox.Controls.Add(editButtons[i]);
            }
        }
    }
}

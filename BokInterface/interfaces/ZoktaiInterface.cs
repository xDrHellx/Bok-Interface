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
        private Label _bok2_currentStatusHpValue = new(),
            _bok2_currentStatusEneValue = new(),
            _bok2_djangoLevel = new(),
            _bok2_djangoExp = new(),
            _bok2_djangoVit = new(),
            _bok2_djangoSpr = new(),
            _bok2_djangoStr = new(),
            _bok2_djangoAgi = new(),
            _bok2_djangoStatPoints = new(),
            _bok2_djangoSwordSkill = new(),
            _bok2_djangoSpearSkill = new(),
            _bok2_djangoHammerSkill = new(),
            _bok2_djangoFistsSkill = new(),
            _bok2_djangoGunSkill = new();
        private Button _bok2_editStatusBtn = new(),
            _bok2_editInventoryBtn = new(),
            _bok2_editKeyItemsBtn = new(),
            _bok2_editEquipsBtn = new(),
            _bok2_editWeaponsBtn = new(),
            _bok2_editMagicsBtn = new();
        private GroupBox _bok2_currentSkillGroupBox = new();

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
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 329, 268);

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
            WinFormHelpers.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15, currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15, currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentLevelLabel", "Level :", 93, 19, 40, 15, currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentExpLabel", "EXP :", 93, 34, 33, 15, currentStatusGroupBox);

            // Current status values
            _bok2_currentStatusHpValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15, currentStatusGroupBox);
            _bok2_currentStatusEneValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 34, 31, 15, currentStatusGroupBox);
            _bok2_djangoLevel = WinFormHelpers.CreateLabel("djangoCurrentLevelValue", "", 132, 19, 31, 15, currentStatusGroupBox);
            _bok2_djangoExp = WinFormHelpers.CreateLabel("djangoCurrentExpValue", "", 132, 34, 43, 15, currentStatusGroupBox);
        }

        private void AddZoktaiCurrentSkillSection() {

            // Section
            _bok2_currentSkillGroupBox = WinFormHelpers.CreateGroupBox("currentSkill", "Skill", 92, 86, 110, 104, this);

            // Sword
            WinFormHelpers.CreateLabel("swordSkillLabel", "Sword", 6, 20, 54, 15, _bok2_currentSkillGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoSwordSkill = WinFormHelpers.CreateLabel("djangoSwordSkill", "", 60, 19, 41, 15, _bok2_currentSkillGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleLeft");

            // Spear
            WinFormHelpers.CreateLabel("spearSkillLabel", "Spear", 6, 34, 54, 15, _bok2_currentSkillGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoSpearSkill = WinFormHelpers.CreateLabel("djangoSpearSkill", "", 60, 34, 41, 15, _bok2_currentSkillGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleLeft");

            // Hammer
            WinFormHelpers.CreateLabel("hammerSkillLabel", "Hammer", 6, 49, 54, 15, _bok2_currentSkillGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoHammerSkill = WinFormHelpers.CreateLabel("djangoHammerSkill", "", 60, 49, 41, 15, _bok2_currentSkillGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleLeft");

            // Fists
            WinFormHelpers.CreateLabel("fistsSkillLabel", "Fists", 6, 64, 54, 15, _bok2_currentSkillGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoFistsSkill = WinFormHelpers.CreateLabel("djangoFistsSkill", "", 60, 64, 41, 15, _bok2_currentSkillGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleLeft");

            // Gun
            WinFormHelpers.CreateLabel("gunSkillLabel", "Gun", 6, 79, 54, 15, _bok2_currentSkillGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoGunSkill = WinFormHelpers.CreateLabel("djangoGunSkill", "", 60, 79, 41, 15, _bok2_currentSkillGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleLeft");
        }

        private void AddZoktaiCurrentStatsSection() {

            // Section
            currentStatsGroupBox = WinFormHelpers.CreateGroupBox("currentStats", "Stats", 5, 86, 75, 106, this);

            // VIT
            WinFormHelpers.CreateLabel("vitRowLabel", "VIT", 6, 19, 27, 15, currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoVit = WinFormHelpers.CreateLabel("djangoVit", "", 35, 19, 31, 15, currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // SPR
            WinFormHelpers.CreateLabel("sprRowLabel", "SPR", 6, 34, 27, 15, currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoSpr = WinFormHelpers.CreateLabel("djangoSpr", "", 35, 34, 31, 15, currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // STR
            WinFormHelpers.CreateLabel("strRowLabel", "STR", 6, 49, 27, 15, currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoStr = WinFormHelpers.CreateLabel("djangoStr", "", 35, 49, 31, 15, currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // AGI
            WinFormHelpers.CreateLabel("agiRowLabel", "AGI", 6, 64, 27, 15, currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoAgi = WinFormHelpers.CreateLabel("djangoAgi", "", 35, 64, 31, 15, currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // Stat points to allocate
            WinFormHelpers.CreateLabel("statPointsLabel", "Add", 6, 84, 29, 15, currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoStatPoints = WinFormHelpers.CreateLabel("djangoStatPoints", "", 35, 84, 31, 15, currentStatsGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");
        }

        private void AddZoktaiEditSection() {

            // Section
            editGroupBox = WinFormHelpers.CreateGroupBox("editButtons", "Edit", 237, 25, 87, 184, this);

            _bok2_editStatusBtn = WinFormHelpers.CreateButton("editStatuts", "Status", 6, 19, 75, 23, editGroupBox);
            _bok2_editInventoryBtn = WinFormHelpers.CreateButton("editItems", "Items", 6, 46, 75, 23, editGroupBox);
            _bok2_editKeyItemsBtn = WinFormHelpers.CreateButton("editKeyItems", "Key items", 6, 73, 75, 23, editGroupBox);
            _bok2_editWeaponsBtn = WinFormHelpers.CreateButton("editWeapons", "Weapons", 6, 100, 75, 23, editGroupBox);
            _bok2_editEquipsBtn = WinFormHelpers.CreateButton("editEquips", "Protectors", 6, 127, 75, 23, editGroupBox);
            _bok2_editMagicsBtn = WinFormHelpers.CreateButton("editMagics", "Magics", 6, 154, 75, 23, editGroupBox);

            // Add onclick events
            _bok2_editStatusBtn.Click += new System.EventHandler(OpenStatusEditor);
            _bok2_editInventoryBtn.Click += new System.EventHandler(OpenInventoryEditor);
            _bok2_editKeyItemsBtn.Click += new System.EventHandler(OpenKeyItemsEditor);
            _bok2_editWeaponsBtn.Click += new System.EventHandler(OpenWeaponsEditor);
            _bok2_editEquipsBtn.Click += new System.EventHandler(OpenEquipsEditor);
            _bok2_editMagicsBtn.Click += new System.EventHandler(OpenMagicsEditor);
        }
    }
}

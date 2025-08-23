using System;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;

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
        private GroupBox _bok2_currentSkillGroupBox = new();
        private ToolStripMenuItem _enableBlindboxLvl3 = new(),
            _enableBlindboxLvl4 = new(),
            _enableBlindboxLvl5ValentineDay = new(),
            _enableStarPiece = new();

        #endregion

        #region Show interface

        private void ShowZoktaiInterface() {

            GenerateMenu();
            AddZoktaiDownloadableEventsMenu();

            // If JP version, update the game name label to add the version
            string version = "";
            if (currentGameId == 1244803925) {
                version = Utilities.GetGameVersion() == 1 ? " (v1.1)" : " (v1.0)";
            }

            // Current game name
            WinFormHelpers.CreateLabel("currentGameName", currentGameName + version, 0, _menuBar.Height, Width, 20, this, WinFormHelpers.gameNameBackground, textAlignment: "MiddleLeft");

            // Sections
            AddZoktaiCurrentStatusSection();
            AddZoktaiCurrentStatsSection();
            AddZoktaiCurrentSkillSection();
            AddMiscDataSection();

            // Main window
            SetMainWindow("Bok Interface", 236, 277);
            ResumeLayout(false);
        }

        #endregion

        #region Update

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

                // Update the current & average speed
                int positionX = (int)_memoryValues.Django["x_position"].Value;
                int positionY = (int)_memoryValues.Django["y_position"].Value;
                int positionZ = (int)_memoryValues.Django["z_position"].Value;

                // Get the movement speed in 3D & the average speed
                double speed3D = _movementCalculator.Get3dMovementSpeed(positionX, positionY, positionZ);
                double averageSpeed = Math.Round(_movementCalculator.GetAverageSpeed(speed3D, 60), 3);

                // Update the fields
                _currentSpeedLabel.Text = "Current movement speed : " + Math.Round(speed3D, 3);
                _averageSpeedLabel.Text = "Average over 60 frames : " + averageSpeed.ToString();
            }

            // Update the menus for in-game events
            UpdateZoktaiEvent(_enableBlindboxLvl3, "blindbox_lvl_3", 0x8E67);
            UpdateZoktaiEvent(_enableBlindboxLvl4, "blindbox_lvl_4", 0x8FAA);
            UpdateZoktaiEvent(_enableBlindboxLvl5ValentineDay, "blindbox_lvl_5_valentine_day", 0x90E0);
            UpdateZoktaiEvent(_enableStarPiece, "star_piece", 0x8FD6);
        }

        #endregion

        #region Elements

        private void AddZoktaiCurrentStatusSection() {

            // Section
            _currentStatusGroupBox = WinFormHelpers.CreateGroupBox("currentStatus", "Current status", 5, 45, 226, 55, this);

            // Current status labels
            WinFormHelpers.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15, _currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15, _currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentLevelLabel", "Level :", 93, 19, 40, 15, _currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentExpLabel", "EXP :", 93, 34, 33, 15, _currentStatusGroupBox);

            // Current status values
            _bok2_currentStatusHpValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15, _currentStatusGroupBox, textAlignment: "MiddleRight");
            _bok2_currentStatusEneValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 34, 31, 15, _currentStatusGroupBox, textAlignment: "MiddleRight");
            _bok2_djangoLevel = WinFormHelpers.CreateLabel("djangoCurrentLevelValue", "", 144, 19, 31, 15, _currentStatusGroupBox, textAlignment: "MiddleRight");
            _bok2_djangoExp = WinFormHelpers.CreateLabel("djangoCurrentExpValue", "", 132, 34, 43, 15, _currentStatusGroupBox, textAlignment: "MiddleRight");
        }

        private void AddZoktaiCurrentSkillSection() {

            // Section
            _bok2_currentSkillGroupBox = WinFormHelpers.CreateGroupBox("currentSkill", "Skill", 92, 106, 110, 104, this);

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
            _currentStatsGroupBox = WinFormHelpers.CreateGroupBox("currentStats", "Stats", 5, 106, 75, 106, this);

            // VIT
            WinFormHelpers.CreateLabel("vitRowLabel", "VIT", 6, 19, 27, 15, _currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoVit = WinFormHelpers.CreateLabel("djangoVit", "", 35, 19, 31, 15, _currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // SPR
            WinFormHelpers.CreateLabel("sprRowLabel", "SPR", 6, 34, 27, 15, _currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoSpr = WinFormHelpers.CreateLabel("djangoSpr", "", 35, 34, 31, 15, _currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // STR
            WinFormHelpers.CreateLabel("strRowLabel", "STR", 6, 49, 27, 15, _currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoStr = WinFormHelpers.CreateLabel("djangoStr", "", 35, 49, 31, 15, _currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // AGI
            WinFormHelpers.CreateLabel("agiRowLabel", "AGI", 6, 64, 27, 15, _currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoAgi = WinFormHelpers.CreateLabel("djangoAgi", "", 35, 64, 31, 15, _currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");

            // Stat points to allocate
            WinFormHelpers.CreateLabel("statPointsLabel", "Add", 6, 84, 29, 15, _currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok2_djangoStatPoints = WinFormHelpers.CreateLabel("djangoStatPoints", "", 35, 84, 31, 15, _currentStatsGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");
        }

        #endregion

        #region Game-specific menus

        private void AddZoktaiDownloadableEventsMenu() {
            if (shorterGameName != "Zoktai") {
                return;
            }

            string eventToolTipText = "Cannot be disabled except by loading savestates or deleting savefiles.";
            ToolStripMenuItem eventsMenu = WinFormHelpers.CreateToolStripMenuItem("eventsMenu", "JoySpots events", menuStrip: _menuBar);

            _enableBlindboxLvl3 = WinFormHelpers.CreateToolStripMenuItem("enableBlindboxLvl3", "&Blindbox Lv. 3", toolTipText: eventToolTipText, menuItem: eventsMenu);
            AddZoktaiJoySpotsEventHandler(_enableBlindboxLvl3, "blindbox_lvl_3", 0x8E67);

            _enableBlindboxLvl4 = WinFormHelpers.CreateToolStripMenuItem("enableBlindboxLvl4", "&Blindbox Lv. 4", toolTipText: eventToolTipText, menuItem: eventsMenu);
            AddZoktaiJoySpotsEventHandler(_enableBlindboxLvl4, "blindbox_lvl_4", 0x8FAA);

            _enableBlindboxLvl5ValentineDay = WinFormHelpers.CreateToolStripMenuItem("enableBlindboxLvl5ValentineDay", "&Blindbox Lv. 5 && Valentine's Day", toolTipText: eventToolTipText + " Valentine's Day only triggers on February 14th.", menuItem: eventsMenu);
            AddZoktaiJoySpotsEventHandler(_enableBlindboxLvl5ValentineDay, "blindbox_lvl_5_valentine_day", 0x90E0);

            _enableStarPiece = WinFormHelpers.CreateToolStripMenuItem("enableStarPiece", "&Star Piece from ???", toolTipText: eventToolTipText, menuItem: eventsMenu);
            AddZoktaiJoySpotsEventHandler(_enableStarPiece, "star_piece", 0x8FD6);
        }

        /// <summary>Add a Click event on a menu item for a Zoktai / Bok 2 JoySpots event</summary>
        /// <param name="menuItem">Menu item</param>
        /// <param name="memKey">Related key within the _zoktaiAddresses.JoySpots dictionnary</param>
        /// <param name="onCheckedValue">Value to set on the key when the menu item is checked</param>
        private void AddZoktaiJoySpotsEventHandler(ToolStripMenuItem menuItem, string memKey, uint onCheckedValue, uint onUncheckedValue = 0x0) {
            if (shorterGameName != "Zoktai" || _zoktaiAddresses.JoySpots.ContainsKey(memKey) == false) {
                return;
            }

            // Add the Click event
            menuItem.Click += (s, e) => {
                /**
                 * If the downloadable event is already enabled, check the menu item & stop
                 * Downloadable events cannot be disabled except by deleting savefiles or loading savestates
                 */
                if (_zoktaiAddresses.JoySpots[memKey].Value == onCheckedValue) {
                    menuItem.Checked = true;
                    return;
                }

                menuItem.Checked = !menuItem.Checked;
                _zoktaiAddresses.JoySpots[memKey].Value = menuItem.Checked == true ? onCheckedValue : onUncheckedValue;
            };
        }

        /// <summary>Update an event menu item for a Zoktai / Bok 2 downloadable event</summary>
        /// <param name="menuItem">Menu item</param>
        /// <param name="memKey">Related key within the _zoktaiAddresses.JoySpots dictionnary</param>
        /// <param name="onCheckedValue">Value used for enabling the downloadable event</param>
        private void UpdateZoktaiEvent(ToolStripMenuItem menuItem, string memKey, uint onCheckedValue) {
            if (shorterGameName != "Zoktai" || _zoktaiAddresses.JoySpots.ContainsKey(memKey) == false) {
                return;
            }

            // Check / uncheck the menu item based on if the downloadable event is enabled or not
            menuItem.Checked = _zoktaiAddresses.JoySpots[memKey].Value == onCheckedValue;
        }

        #endregion
    }
}

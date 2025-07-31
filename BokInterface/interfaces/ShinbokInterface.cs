using System;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;

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
            _bok3_djangoLevel = new(),
            _bok3_djangoExp = new(),
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

        #endregion

        #region Show interface

        private void ShowShinbokInterface() {

            GenerateMenu();

            // Current game name
            WinFormHelpers.CreateLabel("currentGameName", currentGameName, 0, 0, Width, 20, this, WinFormHelpers._gameNameBackground, textAlignment: "MiddleLeft");

            // Sections
            AddShinbokCurrentStatusSection();
            AddShinbokCurrentStatsSection();
            AddMiscDataSection();

            // Main window
            SetMainWindow("Bok Interface", 236, 253);
            ResumeLayout(false);
        }

        #endregion

        #region Update

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
                _bok3_djangoLevel.Text = _memoryValues.Django["level"].Value.ToString();
                _bok3_djangoExp.Text = _memoryValues.Django["exp"].Value.ToString();
                _bok3_djangoBaseVit.Text = _memoryValues.Misc["base_vit"].Value.ToString();
                _bok3_djangoBaseSpr.Text = _memoryValues.Misc["base_spr"].Value.ToString();
                _bok3_djangoBaseStr.Text = _memoryValues.Misc["base_str"].Value.ToString();
                _bok3_djangoCardsVit.Text = _memoryValues.Misc["cards_vit"].Value.ToString();
                _bok3_djangoCardsSpr.Text = _memoryValues.Misc["cards_spr"].Value.ToString();
                _bok3_djangoCardsStr.Text = _memoryValues.Misc["cards_str"].Value.ToString();
                _bok3_djangoEquipsVit.Text = _memoryValues.Django["equips_vit"].Value.ToString();
                _bok3_djangoEquipsSpr.Text = _memoryValues.Django["equips_spr"].Value.ToString();
                _bok3_djangoEquipsStr.Text = _memoryValues.Django["equips_str"].Value.ToString();
                _bok3_djangoTotalVit.Text = (_memoryValues.Misc["base_vit"].Value + _memoryValues.Misc["cards_vit"].Value + _memoryValues.Django["equips_vit"].Value).ToString();
                _bok3_djangoTotalSpr.Text = (_memoryValues.Misc["base_spr"].Value + _memoryValues.Misc["cards_spr"].Value + _memoryValues.Django["equips_spr"].Value).ToString();
                _bok3_djangoTotalStr.Text = (_memoryValues.Misc["base_str"].Value + _memoryValues.Misc["cards_str"].Value + _memoryValues.Django["equips_str"].Value).ToString();

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
        }

        #endregion

        #region Elements

        private void AddShinbokCurrentStatusSection() {

            // Section
            _currentStatusGroupBox = WinFormHelpers.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, this);

            // Current status labels
            WinFormHelpers.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15, _currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15, _currentStatusGroupBox);
            WinFormHelpers.CreateLabel("currentGameNameTrcLabel", "TRC :", 7, 49, 34, 15, _currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentLevelLabel", "Level :", 93, 19, 40, 15, _currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentExpLabel", "EXP :", 93, 34, 33, 15, _currentStatusGroupBox);

            // Current status values
            _bok3_currentStatusHpValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15, _currentStatusGroupBox, textAlignment: "MiddleRight");
            _bok3_currentStatusEneValue = WinFormHelpers.CreateLabel("djangoCurrentEneValue", "", 44, 34, 31, 15, _currentStatusGroupBox, textAlignment: "MiddleRight");
            _bok3_currentStatusTrcValue = WinFormHelpers.CreateLabel("djangoCurrentTrcValue", "", 44, 49, 31, 15, _currentStatusGroupBox, textAlignment: "MiddleRight");
            _bok3_djangoLevel = WinFormHelpers.CreateLabel("djangoCurrentLevelValue", "", 144, 19, 31, 15, _currentStatusGroupBox, textAlignment: "MiddleRight");
            _bok3_djangoExp = WinFormHelpers.CreateLabel("djangoCurrentExpValue", "", 132, 34, 43, 15, _currentStatusGroupBox, textAlignment: "MiddleRight");
        }

        private void AddShinbokCurrentStatsSection() {

            // Section
            _currentStatsGroupBox = WinFormHelpers.CreateGroupBox("currentStats", "Stats", 5, 101, 184, 87, this);

            // Column names
            WinFormHelpers.CreateLabel("baseStatColumnName", "Base", 35, 19, 31, 15, _currentStatsGroupBox, WinFormHelpers.baseStatColor);
            WinFormHelpers.CreateLabel("cardsStatColumnName", "Cards", 66, 19, 37, 15, _currentStatsGroupBox, WinFormHelpers.cardsStatColor);
            WinFormHelpers.CreateLabel("equipsStatColumnName", "Equips", 103, 19, 42, 15, _currentStatsGroupBox, WinFormHelpers.equipsStatColor);
            WinFormHelpers.CreateLabel("totalStatColumnName", "Total", 145, 19, 32, 15, _currentStatsGroupBox, WinFormHelpers.totalStatColor);

            // VIT
            WinFormHelpers.CreateLabel("vitRowLabel", "VIT", 6, 34, 27, 15, _currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok3_djangoBaseVit = WinFormHelpers.CreateLabel("djangoBaseVit", "", 35, 34, 31, 15, _currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
            _bok3_djangoCardsVit = WinFormHelpers.CreateLabel("djangoCardsVit", "", 66, 34, 37, 15, _currentStatsGroupBox, WinFormHelpers.cardsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoEquipsVit = WinFormHelpers.CreateLabel("djangoEquipsVit", "", 103, 34, 42, 15, _currentStatsGroupBox, WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoTotalVit = WinFormHelpers.CreateLabel("djangoTotalVit", "", 145, 34, 32, 15, _currentStatsGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");

            // SPR
            WinFormHelpers.CreateLabel("sprRowLabel", "SPR", 6, 49, 27, 15, _currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok3_djangoBaseSpr = WinFormHelpers.CreateLabel("djangoBaseSpr", "", 35, 49, 31, 15, _currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
            _bok3_djangoCardsSpr = WinFormHelpers.CreateLabel("djangoCardsSpr", "", 66, 49, 37, 15, _currentStatsGroupBox, WinFormHelpers.cardsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoEquipsSpr = WinFormHelpers.CreateLabel("djangoEquipsSpr", "", 103, 49, 42, 15, _currentStatsGroupBox, WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoTotalSpr = WinFormHelpers.CreateLabel("djangoTotalSpr", "", 145, 49, 32, 15, _currentStatsGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");

            // STR
            WinFormHelpers.CreateLabel("strRowLabel", "STR", 6, 64, 27, 15, _currentStatsGroupBox, textAlignment: "MiddleLeft");
            _bok3_djangoBaseStr = WinFormHelpers.CreateLabel("djangoBaseStr", "", 35, 64, 31, 15, _currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
            _bok3_djangoCardsStr = WinFormHelpers.CreateLabel("djangoCardsStr", "", 66, 64, 37, 15, _currentStatsGroupBox, WinFormHelpers.cardsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoEquipsStr = WinFormHelpers.CreateLabel("djangoEquipsStr", "", 103, 64, 42, 15, _currentStatsGroupBox, WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
            _bok3_djangoTotalStr = WinFormHelpers.CreateLabel("djangoTotalStr", "", 145, 64, 32, 15, _currentStatsGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");
        }

        #endregion
    }
}
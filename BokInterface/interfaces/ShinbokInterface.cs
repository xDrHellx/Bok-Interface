using System;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;

/**
 * File for the Shinbok (Boktai 3) interface itself
 */

namespace BokInterface;

partial class BokInterface {

    #region Properties

    readonly ShinbokAddresses _shinbokAddresses = new();
    Label _bok3_currentStatusHpValue = new(),
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

    void ShowShinbokInterface() {

        GenerateMenu();
        AddCurrentGameInfo();

        // Sections
        AddShinbokCurrentStatusSection();
        AddShinbokCurrentStatsSection();
        AddMiscDataSection();

        // Main window
        SetMainWindow("Bok Interface", 236, 358 + _menuBar.Height);
        ResumeLayout(false);
    }

    #endregion

    #region Update

    void UpdateShinbokInterface() {

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
            _bok3_currentStatusHpValue.Text = $"{djangoCurrentHp} / {_memoryValues.Django["max_hp"].Value}";
            _bok3_currentStatusEneValue.Text = $"{_memoryValues.Django["current_ene"].Value} / {_memoryValues.Django["max_ene"].Value}";
            _bok3_currentStatusTrcValue.Text = $"{_memoryValues.Django["current_trc"].Value} / 1000";
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
            int positionX = (int)_memoryValues.Django["x_position"].Value,
                positionY = (int)_memoryValues.Django["y_position"].Value,
                positionZ = (int)_memoryValues.Django["z_position"].Value;

            // Get the movement speed in 3D & the average speed
            double speed3D = _movementCalculator.Get3dMovementSpeed(positionX, positionY, positionZ),
                averageSpeed = Math.Round(_movementCalculator.GetAverageSpeed(speed3D, 60), 3);

            // Update the fields
            _currentSpeedLabel.Text = $"Current movement speed: {Math.Round(speed3D, 3)}";
            _averageSpeedLabel.Text = $"Average over 60 frames: {averageSpeed}";
        }

        // If the coffin data if available
        uint coffinActorPointer = _shinbokAddresses.Coffin["actor"].Value;
        if (coffinActorPointer > 0) {

            // Calculate the distance to the coffin
            double distance = Utilities.GetDistance(
                new Point((int)_memoryValues.Coffin["x_position"].Value, (int)_memoryValues.Coffin["y_position"].Value),
                new Point((int)_shinbokAddresses.Django["x_position"].Value, (int)_shinbokAddresses.Django["y_position"].Value)
            );

            _coffinDamageLabel.Text = "Damage : " + _memoryValues.Coffin["damage"].Value + " / 1200";
            _coffinWindupTimerLabel.Text = "Windup begins in : " + _memoryValues.Coffin["windup_timer"].Value;
            _coffinShakeTimerLabel.Text = "Windup : " + _memoryValues.Coffin["shake_timer"].Value + " / 127";
            _coffinShakeDurationLabel.Text = "Duration : " + _memoryValues.Coffin["shake_duration"].Value;
            _coffinEscapeTimerLabel.Text = "Begins escaping in : " + _memoryValues.Coffin["own_movement_timer"].Value;
            _coffinDistanceLabel.Text = "Distance : " + distance;
        }

        if (_showGui == true) {
            ShowShinbokGui();
        }
    }

    #endregion

    #region Elements

    void AddShinbokCurrentStatusSection() {

        // Section
        _currentStatusGroupBox = WinFormHelpers.CreateGroupBox("currentStatus", "Current status", new Point(5, 45), new Size(226, 70), this);

        // Current status labels
        WinFormHelpers.CreateLabel("djangoCurrentHpLabel", "LIFE:", new Point(7, 19), new Size(34, 15), _currentStatusGroupBox);
        WinFormHelpers.CreateLabel("djangoCurrentEneLabel", "ENE:", new Point(7, 34), new Size(34, 15), _currentStatusGroupBox);
        WinFormHelpers.CreateLabel("currentGameNameTrcLabel", "TRC:", new Point(7, 49), new Size(34, 15), _currentStatusGroupBox);
        WinFormHelpers.CreateLabel("djangoCurrentLevelLabel", "Level:", new Point(123, 19), new Size(40, 15), _currentStatusGroupBox);
        WinFormHelpers.CreateLabel("djangoCurrentExpLabel", "EXP:", new Point(123, 34), new Size(33, 15), _currentStatusGroupBox);

        // Current status values
        _bok3_currentStatusHpValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", new Point(44, 19), new Size(72, 15), _currentStatusGroupBox, textAlignment: "MiddleLeft");
        _bok3_currentStatusEneValue = WinFormHelpers.CreateLabel("djangoCurrentEneValue", "", new Point(44, 34), new Size(72, 15), _currentStatusGroupBox, textAlignment: "MiddleLeft");
        _bok3_currentStatusTrcValue = WinFormHelpers.CreateLabel("djangoCurrentTrcValue", "", new Point(44, 49), new Size(72, 15), _currentStatusGroupBox, textAlignment: "MiddleLeft");
        _bok3_djangoLevel = WinFormHelpers.CreateLabel("djangoCurrentLevelValue", "", new Point(174, 19), new Size(31, 15), _currentStatusGroupBox, textAlignment: "MiddleRight");
        _bok3_djangoExp = WinFormHelpers.CreateLabel("djangoCurrentExpValue", "", new Point(162, 34), new Size(43, 15), _currentStatusGroupBox, textAlignment: "MiddleRight");
    }

    void AddShinbokCurrentStatsSection() {

        // Section
        _currentStatsGroupBox = WinFormHelpers.CreateGroupBox("currentStats", "Stats", new Point(5, 121), new Size(184, 87), this);

        // Column names
        WinFormHelpers.CreateLabel("baseStatColumnName", "Base", new Point(35, 19), new Size(31, 15), _currentStatsGroupBox, WinFormHelpers.baseStatColor);
        WinFormHelpers.CreateLabel("cardsStatColumnName", "Cards", new Point(66, 19), new Size(37, 15), _currentStatsGroupBox, WinFormHelpers.cardsStatColor);
        WinFormHelpers.CreateLabel("equipsStatColumnName", "Equips", new Point(103, 19), new Size(42, 15), _currentStatsGroupBox, WinFormHelpers.equipsStatColor);
        WinFormHelpers.CreateLabel("totalStatColumnName", "Total", new Point(145, 19), new Size(32, 15), _currentStatsGroupBox, WinFormHelpers.totalStatColor);

        // VIT
        WinFormHelpers.CreateLabel("vitRowLabel", "VIT", new Point(6, 34), new Size(27, 15), _currentStatsGroupBox, textAlignment: "MiddleLeft");
        _bok3_djangoBaseVit = WinFormHelpers.CreateLabel("djangoBaseVit", "", new Point(35, 34), new Size(31, 15), _currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
        _bok3_djangoCardsVit = WinFormHelpers.CreateLabel("djangoCardsVit", "", new Point(66, 34), new Size(37, 15), _currentStatsGroupBox, WinFormHelpers.cardsStatColor, textAlignment: "MiddleRight");
        _bok3_djangoEquipsVit = WinFormHelpers.CreateLabel("djangoEquipsVit", "", new Point(103, 34), new Size(42, 15), _currentStatsGroupBox, WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
        _bok3_djangoTotalVit = WinFormHelpers.CreateLabel("djangoTotalVit", "", new Point(145, 34), new Size(32, 15), _currentStatsGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");

        // SPR
        WinFormHelpers.CreateLabel("sprRowLabel", "SPR", new Point(6, 49), new Size(27, 15), _currentStatsGroupBox, textAlignment: "MiddleLeft");
        _bok3_djangoBaseSpr = WinFormHelpers.CreateLabel("djangoBaseSpr", "", new Point(35, 49), new Size(31, 15), _currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
        _bok3_djangoCardsSpr = WinFormHelpers.CreateLabel("djangoCardsSpr", "", new Point(66, 49), new Size(37, 15), _currentStatsGroupBox, WinFormHelpers.cardsStatColor, textAlignment: "MiddleRight");
        _bok3_djangoEquipsSpr = WinFormHelpers.CreateLabel("djangoEquipsSpr", "", new Point(103, 49), new Size(42, 15), _currentStatsGroupBox, WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
        _bok3_djangoTotalSpr = WinFormHelpers.CreateLabel("djangoTotalSpr", "", new Point(145, 49), new Size(32, 15), _currentStatsGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");

        // STR
        WinFormHelpers.CreateLabel("strRowLabel", "STR", new Point(6, 64), new Size(27, 15), _currentStatsGroupBox, textAlignment: "MiddleLeft");
        _bok3_djangoBaseStr = WinFormHelpers.CreateLabel("djangoBaseStr", "", new Point(35, 64), new Size(31, 15), _currentStatsGroupBox, WinFormHelpers.baseStatColor, textAlignment: "MiddleRight");
        _bok3_djangoCardsStr = WinFormHelpers.CreateLabel("djangoCardsStr", "", new Point(66, 64), new Size(37, 15), _currentStatsGroupBox, WinFormHelpers.cardsStatColor, textAlignment: "MiddleRight");
        _bok3_djangoEquipsStr = WinFormHelpers.CreateLabel("djangoEquipsStr", "", new Point(103, 64), new Size(42, 15), _currentStatsGroupBox, WinFormHelpers.equipsStatColor, textAlignment: "MiddleRight");
        _bok3_djangoTotalStr = WinFormHelpers.CreateLabel("djangoTotalStr", "", new Point(145, 64), new Size(32, 15), _currentStatsGroupBox, WinFormHelpers.totalStatColor, textAlignment: "MiddleRight");
    }

    #endregion

    #region GUI

    void ShowShinbokGui() {

        // RTC
        int halfScreenHeight = GetScreenHeight() / 2;
        if (_showRtc == true) {
            APIs.Gui.Text(3, halfScreenHeight, "RTC:");
            APIs.Gui.Text(50, halfScreenHeight, Utilities.FormatTimeTo24(_shinbokAddresses.Misc["rtc_hours"].Value, _shinbokAddresses.Misc["rtc_minutes"].Value, _shinbokAddresses.Misc["rtc_seconds"].Value));
        }

        // Frames since game start
        if (_showIgtFrameCounter == true) {
            APIs.Gui.Text(3, halfScreenHeight + 15, "IGT:");
            APIs.Gui.Text(50, halfScreenHeight + 15, _memoryValues.Misc["igt_frame_counter"].Value.ToString());
        }

        // Interest rate
        if (_showInterestRate == true) {
            APIs.Gui.Text(3, halfScreenHeight + 30, "Interest rate:");
            APIs.Gui.Text(150, halfScreenHeight + 30, Utilities.GetInterestRateFromValue(_memoryValues.Solls["interest_rate"].Value));
        }
    }

    #endregion
}

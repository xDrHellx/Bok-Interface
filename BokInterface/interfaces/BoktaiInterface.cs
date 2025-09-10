using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;
using BokInterface.Weapons;

/**
 * File for the Boktai TSiiYH interface itself
 */

namespace BokInterface {
    partial class BokInterface {

        #region Properties

        private readonly BoktaiAddresses _boktaiAddresses = new();
        private readonly BoktaiGuns _boktaiGuns = new();
        private Label _bok1_currentStatusHpValue = new();
        private Label _bok1_currentStatusEneValue = new();
        private ToolStripMenuItem _enableAstroBattery = new();

        #endregion

        #region Show interface

        private void ShowBoktaiInterface() {

            GenerateMenu();
            AddBoktaiEventsMenu();

            // If E3 demo / beta, update the game name label
            string version = "";
            if (currentGameId == 1246311233) {
                version = " (E3 demo)";
            }

            // Current game name
            WinFormHelpers.CreateLabel("currentGameName", currentGameName + version, 0, _menuBar.Height, Width, 20, this, WinFormHelpers.gameNameBackground, textAlignment: "MiddleLeft");

            // Sections
            AddBoktaiCurrentStatusSection();
            AddMiscDataSection();

            // Main window
            SetMainWindow("Bok Interface", 236, 250 + _menuBar.Height);
            ResumeLayout(false);
        }

        #endregion

        #region Update

        private void UpdateBoktaiInterface() {
            /**
             * Check if the pointer to the stat structure is available, ie if the value is "valid"
             * The value for the stat structure would be 0 during room transitions or at the title screen
             *
             * Also check if Django isn't dead (current HP > 0), which means that he can move
             */
            uint statStructurePointer = _boktaiAddresses.Misc["stat"].Value,
                currentHp = _boktaiAddresses.Django["current_hp"].Value;
            if (statStructurePointer > 0 && currentHp > 0) {

                _bok1_currentStatusHpValue.Text = currentHp + " / " + _boktaiAddresses.Django["max_hp"].Value;

                // Get the current battery's energy
                uint battery = _boktaiAddresses.Inventory["equipped_battery"].Value;
                if (battery == 5 || battery == 7) {
                    // Values aren't available for Infinite & Astro batteries
                    _bok1_currentStatusEneValue.Text = "-";
                } else {
                    // Otherwise get the capacity for that battery
                    int capacity = _boktaiGuns.Batteries.ElementAt((int)battery).Value.capacity;
                    _bok1_currentStatusEneValue.Text = APIs.Memory.ReadU16(_boktaiAddresses.Inventory["battery_charges"].Address + (battery * 2)) + " / " + (capacity * 1024);
                }

                // Update the current & average speed
                int positionX = (int)_boktaiAddresses.Django["x_position"].Value,
                    positionY = (int)_boktaiAddresses.Django["y_position"].Value,
                    positionZ = (int)_boktaiAddresses.Django["z_position"].Value;

                // Get the movement speed in 3D & the average speed
                double speed3D = _movementCalculator.Get3dMovementSpeed(positionX, positionY, positionZ),
                    averageSpeed = Math.Round(_movementCalculator.GetAverageSpeed(speed3D, 60), 3);

                // Update the fields
                _currentSpeedLabel.Text = "Current movement speed : " + Math.Round(speed3D, 3);
                _averageSpeedLabel.Text = "Average over 60 frames : " + averageSpeed.ToString();

                // Indicate if the Astro Battery event is enabled
                _enableAstroBattery.Checked = _memoryValues.Misc["astro_battery_unlocked"].Value == _boktaiAddresses.Misc["astro_battery_unlock_value"].Value;
            }

            // If the coffin data if available
            uint coffinActorPointer = _boktaiAddresses.Coffin["actor"].Value;
            if (coffinActorPointer > 0) {

                // Calculate the distance to the coffin
                double distance = Utilities.GetDistance(
                    new Point((int)_memoryValues.Coffin["x_position"].Value, (int)_memoryValues.Coffin["y_position"].Value),
                    new Point((int)_boktaiAddresses.Django["x_position"].Value, (int)_boktaiAddresses.Django["y_position"].Value)
                );

                _coffinDamageLabel.Text = "Damage : " + _memoryValues.Coffin["damage"].Value + " / 1200";
                _coffinWindupTimerLabel.Text = "Windup begins in : " + _memoryValues.Coffin["windup_timer"].Value;
                _coffinShakeTimerLabel.Text = "Windup : " + _memoryValues.Coffin["shake_timer"].Value + " / 127";
                _coffinShakeDurationLabel.Text = "Duration : " + _memoryValues.Coffin["shake_duration"].Value;
                _coffinEscapeTimerLabel.Text = "Begins escaping in : " + _memoryValues.Coffin["own_movement_timer"].Value;
                _coffinDistanceLabel.Text = "Distance : " + distance;
            }

            if (_showGui == true) {
                ShowBoktaiGui();
            }
        }

        #endregion

        #region Elements

        private void AddBoktaiCurrentStatusSection() {

            // Section
            _currentStatusGroupBox = WinFormHelpers.CreateGroupBox("currentStatus", "Current status", 5, 45, 226, 55, this);

            // Current status labels
            WinFormHelpers.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15, _currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15, _currentStatusGroupBox);

            // Current status values
            _bok1_currentStatusHpValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 19, 67, 15, _currentStatusGroupBox, textAlignment: "MiddleRight");
            _bok1_currentStatusEneValue = WinFormHelpers.CreateLabel("djangoCurrentEneValue", "", 44, 34, 67, 15, _currentStatusGroupBox, textAlignment: "MiddleRight");
        }

        #endregion

        #region Game-specific menus

        private void AddBoktaiEventsMenu() {
            if (shorterGameName != "Boktai") {
                return;
            }

            ToolStripMenuItem eventsMenu = WinFormHelpers.CreateToolStripMenuItem("eventsMenu", "Events", menuStrip: _menuBar);

            _enableAstroBattery = WinFormHelpers.CreateToolStripMenuItem("enableAstroBattery", "&Enable Astro Battery", toolTipText: "Enable the event for allowing the Astro Battery to be shown in the inventory. Cannot be disabled except by loading savestates or deleting savefiles.", menuItem: eventsMenu);
            _enableAstroBattery.Click += (s, e) => {
                /**
                 * If the downloadable event is already enabled, check the menu item & stop
                 * That event cannot be disabled except by deleting savefiles or loading savestates
                 */
                if (_memoryValues.Misc["astro_battery_unlocked"].Value == _boktaiAddresses.Misc["astro_battery_unlock_value"].Value) {
                    _enableAstroBattery.Checked = true;
                    return;
                }

                _enableAstroBattery.Checked = !_enableAstroBattery.Checked;
                _memoryValues.Misc["astro_battery_unlocked"].Value = _enableAstroBattery.Checked == true ? _boktaiAddresses.Misc["astro_battery_unlock_value"].Value : 0x0;
            };
        }

        #endregion

        #region GUI

        private void ShowBoktaiGui() {

            // Frames since game start
            int halfScreenHeight = GetScreenHeight() / 2;
            APIs.Gui.Text(3, halfScreenHeight + 15, "IGT:");
            APIs.Gui.Text(50, halfScreenHeight + 15, _boktaiAddresses.Misc["igt_frame_counter"].Value.ToString());

            // Boss HP (if available)
            uint bossHp = _boktaiAddresses.Misc["boss_hp"].Value;
            if (bossHp > 0) {
                APIs.Gui.Text(GetScreenWidth() - 127, 1, "Boss: " + bossHp);
            }
        }

        #endregion
    }
}

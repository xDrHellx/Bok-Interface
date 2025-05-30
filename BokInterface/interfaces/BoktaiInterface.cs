using System;
using System.Drawing;

using BokInterface.Addresses;
using BokInterface.All;

/**
 * File for the Boktai TSiiYH interface itself
 */

namespace BokInterface {

    partial class BokInterface {

        #region Properties

        private readonly BoktaiAddresses _boktaiAddresses = new();
        private System.Windows.Forms.Label _bok1_currentStatusHpValue = new();
        private System.Windows.Forms.Label _bok1_currentStatusEneValue = new();

        #endregion

        private void ShowBoktaiInterface() {

            // Current game name
            WinFormHelpers.CreateLabel("currentGameName", currentGameName, 5, 5, 171, 20, this);

            // Sections
            AddBoktaiCurrentStatusSection();
            AddMiscDataSection();
            AddToolsSection();

            // Main window
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 345, 500);

            ResumeLayout(false);
        }

        private void UpdateBoktaiInterface() {
            /**
             * Check if the pointer to the stat structure is available, ie if the value is "valid"
             * The value for the stat structure would be 0 during room transitions or at the title screen
             * 
             * Also check if Django isn't dead (current HP > 0), which means that he can move
             */
            uint statStructurePointer = _boktaiAddresses.Misc["stat"].Value;
            uint currentHp = _boktaiAddresses.Django["current_hp"].Value;
            if (statStructurePointer > 0 && currentHp > 0) {

                // Update the current & average speed
                int positionX = (int)_boktaiAddresses.Django["x_position"].Value;
                int positionY = (int)_boktaiAddresses.Django["y_position"].Value;
                int positionZ = (int)_boktaiAddresses.Django["z_position"].Value;

                // Get the movement speed in 3D & the average speed
                double speed3D = _movementCalculator.Get3dMovementSpeed(positionX, positionY, positionZ);
                double averageSpeed = Math.Round(_movementCalculator.GetAverageSpeed(speed3D, 60), 3);

                // Update the fields
                _currentSpeedLabel.Text = "Current movement speed : " + Math.Round(speed3D, 3);
                _averageSpeedLabel.Text = "Average over 60 frames : " + averageSpeed.ToString();
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
        }

        private void AddBoktaiCurrentStatusSection() {

            // Section
            currentStatusGroupBox = WinFormHelpers.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 55, this);

            // Current status labels
            WinFormHelpers.CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15, currentStatusGroupBox);
            WinFormHelpers.CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15, currentStatusGroupBox);

            // Current status values
            _bok1_currentStatusHpValue = WinFormHelpers.CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15, currentStatusGroupBox);
            _bok1_currentStatusEneValue = WinFormHelpers.CreateLabel("djangoCurrentEneValue", "", 44, 34, 31, 15, currentStatusGroupBox);
        }
    }
}

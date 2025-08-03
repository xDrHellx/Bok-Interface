using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;

namespace BokInterface.Magics {
    /// <summary>Magics editor for Boktai 2</summary>
    class ZoktaiMagicsEditor : MagicsEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ZoktaiAddresses _zoktaiAddresses;
        private readonly ZoktaiMagics _zoktaiMagics;
        protected GroupBox lunaGroupBox = new(),
            solGroupBox = new(),
            darkGroupBox = new(),
            sabataGroupBox = new();

        #endregion

        #region Constructor

        public ZoktaiMagicsEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;
            _zoktaiMagics = new();

            SetFormParameters(281, 249);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.magicsEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Sections
            lunaGroupBox = WinFormHelpers.CreateGroupBox("lunaGroupBox", "Luna", 5, 5, 271, 95, control: this);
            solGroupBox = WinFormHelpers.CreateGroupBox("solGroupBox", "Sol", 5, 102, 271, 58, control: this);
            darkGroupBox = WinFormHelpers.CreateGroupBox("darkGroupBox", "Dark", 5, 162, 271, 58, control: this);
            sabataGroupBox = WinFormHelpers.CreateGroupBox("sabataGroupBox", "Sabata", 5, 222, 271, 38, control: this);

            // Hide the Sabata section, see why below
            sabataGroupBox.Hide();

            // Loop over all magics & add elements
            int yPositionOffset = 1, n = 1;
            string previousType = "";
            foreach (KeyValuePair<string, Magic> entry in _zoktaiMagics.All) {

                Magic magic = entry.Value;

                // Indicate which groupBox the elements will belong to
                GroupBox groupBox;
                switch (magic.type) {
                    case "Luna":
                        groupBox = lunaGroupBox;
                        break;
                    case "Sol":
                        groupBox = solGroupBox;
                        break;
                    case "Dark":
                        groupBox = darkGroupBox;
                        break;
                    case "Sabata":
                        /**
                         * Note :
                         * Sabata's magics cannot be updated through this
                         * the game might be forcing them when playing as Sabata
                         */
                        groupBox = sabataGroupBox;
                        break;
                    default:
                        // Not handled : skip to the next dictionnary entry
                        continue;
                }

                // If we moved to a different type of magic, reset the offsets for positioning elements
                if (previousType != magic.type) {
                    yPositionOffset = n = 1;
                }

                previousType = magic.type;

                // Prepare the label name by converting the magic's name to snake_case
                string labelName = string.Join("_", magic.name.Split([" "], StringSplitOptions.RemoveEmptyEntries)).ToLower();

                // Label with image & checkbox
                if (n % 2 == 0) {
                    // Even (right side)
                    WinFormHelpers.CreateImageLabel(labelName + "_img", magic.icon, 146, 18 * yPositionOffset, groupBox);
                    WinFormHelpers.CreateLabel(labelName, magic.name, 162, 18 * yPositionOffset, 85, 15, groupBox, textAlignment: "MiddleLeft");
                    checkBoxes.Add(WinFormHelpers.CreateCheckBox("magic_" + labelName, "", 253, 18 * yPositionOffset, 16, 16, control: groupBox));
                } else {
                    // Odd (left side)
                    WinFormHelpers.CreateImageLabel(labelName + "_img", magic.icon, 5, 18 * yPositionOffset, groupBox);
                    WinFormHelpers.CreateLabel(labelName, magic.name, 21, 18 * yPositionOffset, 95, 15, groupBox, textAlignment: "MiddleLeft");
                    checkBoxes.Add(WinFormHelpers.CreateCheckBox("magic_" + labelName, "", 122, 18 * yPositionOffset, 16, 16, control: groupBox));
                }

                // If n > 1 && n is an even number, it means we're moving to another row next
                if (n % 2 == 0 && n > 1) {
                    yPositionOffset++;
                }

                n++;
            }

            // Set default values for each field
            SetDefaultValues();

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setMagicsButton", "Set values", 202, 223, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        #endregion

        #region Values setting

        protected override void SetValues() {
            /**
             * Retrieve the current magics value from the memory address
             * We'll use it as a base for updating the bitmask it contains
             */
            int bitPosition = 0;
            int magic = (int)_memoryValues.Inventory["magics"].Value;
            uint newMagicValue = (uint)magic;
            foreach (CheckBox checkbox in checkBoxes) {
                if (checkbox.Enabled == false) {
                    continue;
                }

                /**
                 * Set the value for the bitmask corresponding to the magic
                 * (checked : 1 | unchecked : 0)
                 */
                if (checkbox.Checked == true) {
                    newMagicValue = (uint)Utilities.SetBitToOne((int)newMagicValue, bitPosition);
                } else {
                    newMagicValue = (uint)Utilities.SetBitToZero((int)newMagicValue, bitPosition);
                }

                bitPosition++;
            }

            // Set the updated value to the memory address
            _memoryValues.Inventory["magics"].Value = newMagicValue;
        }

        protected override void SetDefaultValues() {

            // If "current stat" is a valid value, get the current inventory
            uint currentStat = APIs.Memory.ReadU32(_zoktaiAddresses.Misc["current_stat"].Address);
            if (currentStat > 0) {
                int bitPosition = 0;
                int magic = (int)_memoryValues.Inventory["magics"].Value;
                foreach (CheckBox checkBox in checkBoxes) {
                    if (checkBox.Enabled == false) {
                        continue;
                    }

                    checkBox.Checked = Utilities.IsBitOne(magic, bitPosition);
                    bitPosition++;
                }
            } else {
                // If current stat is unvalid (for example because we are on the title screen or in a room transition), uncheck all checkboxes
                foreach (CheckBox checkBox in checkBoxes) {
                    if (checkBox.Enabled == false) {
                        continue;
                    }

                    checkBox.Checked = false;
                }
            }
        }

        #endregion
    }
}

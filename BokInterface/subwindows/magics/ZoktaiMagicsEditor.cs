using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Magics {
    /// <summary>Magics editor for Boktai 2</summary>
    class ZoktaiMagicsEditor : MagicsEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ZoktaiAddresses _zoktaiAddresses;
        private readonly ZoktaiMagics _zoktaiMagics;

        #endregion

        #region Form elements

        protected GroupBox lunaGroupBox = new(),
            solGroupBox = new(),
            darkGroupBox = new(),
            sabataGroupBox = new();

        #endregion

        public ZoktaiMagicsEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;
            _zoktaiMagics = new();

            SetFormParameters(271, 295);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.magicsEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() {

            // Sections
            lunaGroupBox = WinFormHelpers.CreateGroupBox("lunaGroupBox", "Luna", 5, 5, 261, 95, control: this);
            solGroupBox = WinFormHelpers.CreateGroupBox("solGroupBox", "Sol", 5, 102, 261, 57, control: this);
            darkGroupBox = WinFormHelpers.CreateGroupBox("darkGroupBox", "Dark", 5, 161, 261, 57, control: this);
            sabataGroupBox = WinFormHelpers.CreateGroupBox("sabataGroupBox", "Sabata", 5, 229, 261, 38, control: this);

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
                string labelName = string.Join("_", magic.name.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)).ToLower();

                // Label with image & checkbox
                if (n % 2 == 0) {
                    // Even (right side)
                    WinFormHelpers.CreateImageLabel(labelName + "_img", magic.icon, 136, 18 * yPositionOffset, groupBox);
                    WinFormHelpers.CreateLabel(labelName, magic.name, 152, 18 * yPositionOffset, 85, 15, groupBox, textAlignment: "MiddleLeft");
                    checkBoxes.Add(WinFormHelpers.CreateCheckBox("magic_" + labelName, "", 243, 18 * yPositionOffset, 16, 16, control: groupBox));
                } else {
                    // Odd (left side)
                    WinFormHelpers.CreateImageLabel(labelName + "_img", magic.icon, 5, 18 * yPositionOffset, groupBox);
                    WinFormHelpers.CreateLabel(labelName, magic.name, 21, 18 * yPositionOffset, 85, 15, groupBox, textAlignment: "MiddleLeft");
                    checkBoxes.Add(WinFormHelpers.CreateCheckBox("magic_" + labelName, "", 112, 18 * yPositionOffset, 16, 16, control: groupBox));
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
            Button setValuesButton = WinFormHelpers.CreateButton("setMagicsButton", "Set values", 192, 269, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        protected override void SetValues() {

            // Get checkboxes
            // List<CheckBox> checkBoxes = this.checkBoxes;

            /**
             * TODO Explode the magics memory address to set values for each magic (since it's a 32 bytes bitmask & each magic is 4 bytes)
             */
        }

        protected override void SetDefaultValues() {

            // If "current stat" is a valid value, get the current inventory
            uint currentStat = APIs.Memory.ReadU32(_zoktaiAddresses.Misc["current_stat"].Address);
            if (currentStat > 0) {
                // foreach (CheckBox checkBox in checkBoxes) { }
            } else {
                // If current stat is unvalid (for example because we are on the title screen or in a room transition), uncheck all checkboxes
                foreach (CheckBox checkBox in checkBoxes) {
                    checkBox.Checked = false;
                }
            }
        }
    }
}

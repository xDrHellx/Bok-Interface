using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.JunkParts;
using BokInterface.Utils;

namespace BokInterface.Weapons {
    /// <summary>Editor for the Junk Parts inventory in Boktai DS / Lunar Knights</summary>
    class DsJunkPartsEditor : Editor {

        #region Properties

        protected new readonly string name = "junkPartsEditWindow",
            text = "Junk Parts editor";

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly DsAddresses _memoryAddresses;
        private readonly DsJunkParts _junkParts;
        protected readonly List<CheckBox> checkBoxes = [];

        #endregion

        #region Constructor

        public DsJunkPartsEditor(BokInterface bokInterface, MemoryValues memoryValues, DsAddresses memoryAddresses) {

            _memoryValues = memoryValues;
            _memoryAddresses = memoryAddresses;
            _junkParts = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(572, 295, name, text);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            /**
             * add checkgroupboxes
             * add labels
             * 1x numericupdown for amount
             * 1x checkbox for "Unlocked" ?
             */
            int posX = 5,
                posY = 5,
                n = 1;
            foreach (KeyValuePair<string, DsJunkPart> part in _junkParts.All) {

                string partName = part.Key.ToLower();
                CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"group_{partName}", part.Key, posX, posY, 184, 48, control: this);
                WinFormHelpers.CreateLabel($"lbl_amount_{partName}", "Amount", 2, 21, 51, 15, group);
                numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown($"amount_{partName}", 0, 60, 19, 39, 23, control: group));
                checkBoxes.Add(WinFormHelpers.CreateCheckBox($"unlock_{partName}", "Unlocked", 104, 20, 76, 19, control: group, checkboxOnRight: true));

                posX += 189;

                // If 3rd group on current row, move to another row
                if (n % 3 == 0 && n > 1) {
                    posY += 53;
                    posX = 5;
                }

                n++;
            }

            // Add warning
            WinFormHelpers.CreateImageLabel("tooltip", "warning", 5, 272, this);
            WinFormHelpers.CreateLabel("warning", "Screen will be updated upon switching tab in-game or closing and reopening the menu.", 23, 265, 471, 30, this, textAlignment: "MiddleLeft");

            SetDefaultValues();
            AddSetValuesButton(493, 268, this);
        }

        #endregion

        #region Values setting

        protected override void SetValues() {

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            // Retrieve the current value for the unlocked junk parts bitmask
            int unlockedBitmask = (int)_memoryAddresses.Inventory["unlocked_junk_parts"].Value,
                bitPosition = 0;
            uint newBitmask = (uint)unlockedBitmask;
            foreach (CheckBox field in checkBoxes) {
                if (field.Enabled == true) {
                    // Set the value for the bit corresponding to the junk part
                    newBitmask = field.Checked == true ? (uint)Utilities.SetBitToOne((int)newBitmask, bitPosition) : (uint)Utilities.SetBitToZero((int)newBitmask, bitPosition);
                }

                bitPosition++;
            }

            // Update the bitmask
            _memoryAddresses.Inventory["unlocked_junk_parts"].Value = newBitmask;

            // Update amounts for each junk part
            foreach (NumericUpDown field in numericUpDowns) {
                if (field.Enabled == true && _memoryAddresses.Inventory.ContainsKey(field.Name) == true) {
                    _memoryAddresses.Inventory[field.Name].Value = (uint)field.Value;
                }
            }

            /**
             * If BizHawk was not paused before setting values, unpause it
             * Otherwise keep it paused
             */
            if (_bokInterface._previousIsPauseSetting == true) {
                APIs.Client.Unpause();
            }
        }

        protected override void SetDefaultValues() {

            /**
             * If Lucian or Aaron HP value is valid
             * (Invalid when below 0 or above 9999, ie when switching rooms or on world map)
             */
            uint lucianCurrentHp = _memoryAddresses.Player["lucian_current_hp"].Value,
                aaronCurrentHp = _memoryAddresses.Player.ContainsKey("aaron_current_hp") ? _memoryAddresses.Player["aaron_current_hp"].Value : 0;
            if (
                (lucianCurrentHp > 0 && lucianCurrentHp <= 9999)
                ||
                (aaronCurrentHp > 0 && aaronCurrentHp <= 9999)
            ) {
                int unlockedBitmask = (int)_memoryAddresses.Inventory["unlocked_junk_parts"].Value,
                    bitPosition = 0;
                foreach (CheckBox field in checkBoxes) {
                    field.Checked = Utilities.IsBitOne(unlockedBitmask, bitPosition);
                    bitPosition++;
                }

                foreach (NumericUpDown field in numericUpDowns) {
                    if (_memoryAddresses.Inventory.ContainsKey(field.Name) == true) {
                        field.Value = _memoryAddresses.Inventory[field.Name].Value;
                    }
                }
            } else {
                SetNumericUpDownsToMin(numericUpDowns);
                foreach (CheckBox field in checkBoxes) {
                    field.Checked = false;
                }
            }
        }

        #endregion
    }
}

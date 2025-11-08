using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Items;
using BokInterface.Utils;

namespace BokInterface.Inventory {
    /// <summary>Inventory editor for Lunar Knights / Boktai DS</summary>
    class dsInventoryEditor : InventoryEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly DsAddresses _memoryAddresses;
        private readonly DsItems _dsItems;
        protected CheckGroupBox? slot17group { get; set; }
        protected CheckGroupBox? slot18group { get; set; }
        protected CheckGroupBox? slot19group { get; set; }
        protected CheckGroupBox? slot20group { get; set; }
        /// <summary>
        ///     Default maximum durability value that can be set.<br/>
        ///     This is eventually replaced based on dropdown selected items.
        /// </summary>
        private readonly int _defaultMaxDurability = 8704;
        /// <summary>Durabiliy offset for "Chocolate-covered" items</summary>
        private readonly int _chocolateCoveredDurabilityOffset = 32768;

        #endregion

        #region Constructor

        public dsInventoryEditor(BokInterface bokInterface, MemoryValues memoryValues, DsAddresses memoryAddresses) {

            _memoryValues = memoryValues;
            _memoryAddresses = memoryAddresses;
            _dsItems = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(623, 538, name, text);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {
            GenerateGroups();

            // Generate & add options to dropdowns
            GenerateDropDownOptions();

            // Set default values for each field
            SetDefaultValues();

            /**
             * Due to some items taking longer to rott,
             * For each slot, when another item is selected
             * we update the highest durability value that can be set based on the selected item
             */
            foreach (ImageComboBox dropdown in dropDownLists) {

                // Add on-change event
                dropdown.SelectionChangeCommitted += new EventHandler(delegate (object sender, EventArgs e) {
                    UpdateMaxDurabilityField(dropdown);
                });

                /**
                 * We also call the method directly to update the durability fields
                 * when the subwindow is generated
                 *
                 * It's possible that we retrieved the current inventory so we need to do that
                 */
                UpdateMaxDurabilityField(dropdown);
            }

            // Add warning
            Label expWarning = WinFormHelpers.CreateImageLabel("tooltip", "warning", 5, 515, this);
            WinFormHelpers.CreateLabel("warning", "Inventory will be updated upon switching tab in-game or closing and reopening the menu.", 23, 508, 503, 30, this, textAlignment: "MiddleLeft");

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setValuesButton", "Set values", 544, 511, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        ///<summary>Separated method for generating groups with subelements</summary>
        protected void GenerateGroups() {
            int xPos = 5,
                yPos = 5;
            for (int i = 1; i < 21; i++) {

                // Generate the group for each property dynamically
                PropertyInfo property = GetType().GetProperty($"slot{i}group", BindingFlags.Instance | BindingFlags.NonPublic);
                if (property != null) {
                    CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"slot{i}group", $"Slot {i}", xPos, yPos, 150, 95, control: this);
                    property.SetValue(this, group);

                    // Add elements
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_item_slot_{i}", 5, 19, 140, 23, group, visibleOptions: 5));
                    WinFormHelpers.CreateLabel($"slot{i}", "Durability", 2, 50, 58, 15, group);
                    numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown($"inventory_item_slot_durability_{i}", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: group));
                    checkBoxes.Add(WinFormHelpers.CreateCheckBox($"item_slot_{i}_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: group));
                }

                // Offsets for position
                xPos += 156;
                if ((i % 4) == 0) {
                    xPos = 5;
                    yPos += 102;
                }
            }
        }

        ///<summary>Generates the options for the dropdowns</summary>
        private void GenerateDropDownOptions() {
            foreach (ImageComboBox dropdown in dropDownLists) {
                dropdown.DataSource = new BindingSource(_dsItems.Items, null);
                dropdown.DisplayMember = "Key";
                dropdown.ValueMember = "Value";
            }
        }

        /// <summary>Updates the Maximum parameter for a durability field</summary>
        /// <param name="dropdown">The dropdown that the durability field is related to</param>
        private void UpdateMaxDurabilityField(ImageComboBox dropdown) {

            // Separate the dropdown's name into parts & get the selected item
            string[] fieldParts = dropdown.Name.Split(['_'], 4);
            KeyValuePair<string, Item> selectedOption = (KeyValuePair<string, Item>)dropdown.SelectedItem;
            Item? selectedItem = selectedOption.Value;
            if (selectedItem != null) {

                // Get the related durability field by using the dropdown's name
                string durabilityFieldName = fieldParts[0] + "_" + fieldParts[1] + "_" + fieldParts[2] + "_durability_" + fieldParts[3];
                foreach (NumericUpDown field in numericUpDowns) {
                    /**
                     * If it's the field we're looking for,
                     * update the max value based on the selected item & stop the loop
                     */
                    if (field.Name == durabilityFieldName) {
                        field.Maximum = selectedItem.rottenAt > 0 ? selectedItem.rottenAt - 1 : 0;
                        break;
                    }
                }
            }
        }

        #endregion

        #region Values setting

        protected override void SetValues() {

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            // Sets values for each dropdown (slot)
            for (int i = 0; i < dropDownLists.Count; i++) {

                // If the slot is disabled, skip it
                if (dropDownLists[i].Enabled == false) {
                    continue;
                }

                KeyValuePair<string, Item> selectedOption = (KeyValuePair<string, Item>)dropDownLists[i].SelectedItem;
                Item selectedItem = selectedOption.Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the slot's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = dropDownLists[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], selectedItem.value);
            }

            // Durabilities (numericUpDowns)
            for (int i = 0; i < numericUpDowns.Count; i++) {

                if (numericUpDowns[i].Enabled == false) {
                    continue;
                }

                string[] fieldParts = numericUpDowns[i].Name.Split(['_'], 2);

                // Check if the chocolate-covered checkbox is enabled & checked
                bool isChocolateCovered = false;
                string[] nameParts = fieldParts[1].Split(['_'], 4);
                string checkboxName = nameParts[0] + "_" + nameParts[1] + "_" + nameParts[3] + "_chocolate_covered";
                foreach (CheckBox checkBox in checkBoxes) {
                    if (checkBox.Name == checkboxName && checkBox.Checked == true) {
                        isChocolateCovered = true;
                        break;
                    }
                }

                /**
                 * If the checkbox is checked, adds the offset for chocolate-covered items to the value
                 * Then set the value to the memory address
                 */
                decimal value = numericUpDowns[i].Value + (isChocolateCovered == true ? _chocolateCoveredDurabilityOffset : 0);
                SetMemoryValue(fieldParts[0], fieldParts[1], value);
            }

            /**
             * If BizHawk was not paused before setting values, unpause it
             * Otherwise keep it paused
             */
            if (_bokInterface._previousIsPauseSetting == true) {
                APIs.Client.Unpause();
            }
        }

        ///<summary>
        ///     Method for setting memory values.<br/>
        ///     This is separated to keep things simple.
        ///</summary>
        ///<param name="subList"><c>Dictionnary the key belongs to</c></param>
        ///<param name="valueKey"><c>string</c>Key within the dictionnary</param>
        ///<param name="value"><c>decimal</c>Value to set</param>
        private void SetMemoryValue(string subList, string valueKey, decimal value) {
            if (subList == "inventory" && _memoryAddresses.Inventory.ContainsKey(valueKey) == true) {
                _memoryAddresses.Inventory[valueKey].Value = (uint)value;
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
                foreach (ImageComboBox dropdown in dropDownLists) {
                    /**
                     * Get the name of the field to retrieve the value from based on the dropdown's name (for example inventory_slotX_item => slotX_item)
                     * Then try getting the corresponding item & preselect it
                     */
                    string[] fieldParts = dropdown.Name.Split(['_'], 2);
                    Item? selectedItem = GetItemByValue(_memoryAddresses.Inventory[fieldParts[1]].Value, _dsItems.Items);
                    if (selectedItem != null) {
                        dropdown.SelectedIndex = dropdown.FindStringExact(selectedItem.name);
                    }
                }

                /**
                 * For fields we need to handle a special case related to the "Chocolate-Covered" item
                 * For that one in particular, durability has an offset of 32768
                 */
                foreach (NumericUpDown durabilityField in numericUpDowns) {

                    // Get the different parts of the field's name
                    string[] fieldParts = durabilityField.Name.Split(['_'], 2);

                    // Get the current in-game value
                    decimal ingameValue = _memoryAddresses.Inventory[fieldParts[1]].Value;

                    /**
                     * If the value is 32768 or higher : it's a chocolate-covered item
                     *
                     * In that case we need to pre-select the checkbox for the slot
                     * We'll also remove the offset from the value in the durability field to keep it simple for the user
                     */
                    if (ingameValue >= _chocolateCoveredDurabilityOffset) {
                        string[] nameParts = fieldParts[1].Split(['_'], 4);
                        string checkboxName = nameParts[0] + "_" + nameParts[1] + "_" + nameParts[3] + "_chocolate_covered";
                        foreach (CheckBox checkBox in checkBoxes) {
                            if (checkBox.Name == checkboxName) {
                                checkBox.Checked = true;
                                break;
                            }
                        }

                        durabilityField.Value = _memoryAddresses.Inventory[fieldParts[1]].Value - _chocolateCoveredDurabilityOffset;
                    } else {
                        /**
                         * Otherwise it's another item : we can set the value directly
                         * As for the chocolate-covered checkbox, it's unchecked by default
                         */
                        durabilityField.Value = ingameValue;
                    }
                }
            } else {
                // If current stat is unvalid (for example because we are on the title screen or in a room transition), use specific values
                SelectFirstDropdownsIndex(dropDownLists);
                SetNumericUpDownsToMin(numericUpDowns);
            }
        }

        #endregion
    }
}

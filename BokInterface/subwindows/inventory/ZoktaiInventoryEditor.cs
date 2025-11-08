using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;
using BokInterface.Items;
using System.Reflection;

namespace BokInterface.Inventory {
    /// <summary>Inventory editor for Boktai 2</summary>
    class ZoktaiInventoryEditor : InventoryEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ZoktaiAddresses _zoktaiAddresses;
        private readonly ZoktaiItems _zoktaiItems;
        /// <summary>
        ///     Default maximum durability value that can be set.<br/>
        ///     This is eventually replaced based on dropdown selected items.
        /// </summary>
        private readonly int _defaultMaxDurability = 7679;
        /// <summary>Durabiliy offset for "Chocolate-covered" items</summary>
        private readonly int _chocolateCoveredDurabilityOffset = 32768;

        #endregion

        #region Constructor

        public ZoktaiInventoryEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            _zoktaiItems = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(628, 433, name, text);
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

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 549, 406, 75, 23, this);
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
            for (int i = 1; i < 17; i++) {

                // Generate the group for each property dynamically
                PropertyInfo property = GetType().GetProperty($"slot{i}group", BindingFlags.Instance | BindingFlags.NonPublic);
                if (property != null) {
                    CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"slot{i}group", $"Slot {i}", xPos, yPos, 150, 95, control: this);
                    property.SetValue(this, group);

                    // Add elements to it
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_item", 5, 19, 140, 23, group, visibleOptions: 5));
                    WinFormHelpers.CreateLabel($"slot{i}", "Durability", 2, 50, 58, 15, group);
                    numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown($"inventory_slot{i}_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: group));
                    checkBoxes.Add(WinFormHelpers.CreateCheckBox($"slot{i}_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: group));
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
                dropdown.DataSource = new BindingSource(_zoktaiItems.Items, null);
                dropdown.DisplayMember = "Key";
                dropdown.ValueMember = "Value";
            }
        }

        /// <summary>Updates the Maximum parameter for a durability field</summary>
        /// <param name="dropdown">The dropdown that the durability field is related to</param>
        private void UpdateMaxDurabilityField(ImageComboBox dropdown) {

            // Separate the dropdown's name into parts & get the selected item
            string[] fieldParts = dropdown.Name.Split(['_'], 3);
            KeyValuePair<string, Item> selectedOption = (KeyValuePair<string, Item>)dropdown.SelectedItem;
            Item? selectedItem = selectedOption.Value;
            if (selectedItem != null) {

                // Get the related durability field by using the dropdown's name
                string durabilityFieldName = fieldParts[0] + "_" + fieldParts[1] + "_durability";
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

            // Repeat the above process for Durabilities (numericUpDowns)
            for (int i = 0; i < numericUpDowns.Count; i++) {

                if (numericUpDowns[i].Enabled == false) {
                    continue;
                }

                string[] fieldParts = numericUpDowns[i].Name.Split(['_'], 3);

                // Check if the chocolate-covered checkbox is enabled & checked
                bool isChocolateCovered = false;
                string checkboxName = fieldParts[1] + "_chocolate_covered";
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
                SetMemoryValue(fieldParts[0], fieldParts[1] + "_" + fieldParts[2], value);
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
        ///<para>Method for setting memory values</para>
        ///<para>This is separated because we use the switch inside on different types</para>
        ///</summary>
        ///<param name="subList"><c>Sublit / dictionnary the key belongs to</c></param>
        ///<param name="valueKey"><c>strng</c>Key withint the dictionnary</param>
        ///<param name="value"><c>decimal</c>Value to set</param>
        private void SetMemoryValue(string subList, string valueKey, decimal value) {
            if (subList == "inventory" && _memoryValues.Inventory.ContainsKey(valueKey) == true) {
                _memoryValues.Inventory[valueKey].Value = (uint)value;
            }
        }

        protected override void SetDefaultValues() {

            // If "current stat" is a valid value, get the current inventory
            uint currentStat = _zoktaiAddresses.Misc["current_stat"].Value;
            if (currentStat > 0) {
                foreach (ImageComboBox dropdown in dropDownLists) {
                    /**
                     * Get the name of the field to retrieve the value from based on the dropdown's name (for example inventory_slotX_item => slotX_item)
                     * Then try getting the corresponding item & preselect it
                     */
                    string[] fieldParts = dropdown.Name.Split(['_'], 2);
                    Item? selectedItem = GetItemByValue(_memoryValues.Inventory[fieldParts[1]].Value, _zoktaiItems.Items);
                    if (selectedItem != null) {
                        dropdown.SelectedIndex = dropdown.FindStringExact(selectedItem.name);
                    }
                }

                foreach (NumericUpDown durabilityField in numericUpDowns) {

                    // Get the different parts of the field's name
                    string[] fieldParts = durabilityField.Name.Split(['_'], 3);

                    // Get the current in-game value
                    decimal ingameValue = _memoryValues.Inventory[fieldParts[1] + "_" + fieldParts[2]].Value;

                    /**
                     * If the value is 32768 or higher : it's a chocolate-covered item
                     *
                     * In that case we need to pre-select the checkbox for the slot
                     * We'll also remove the offset from the value in the durability field to keep it simple for the user
                     */
                    if (ingameValue >= _chocolateCoveredDurabilityOffset) {
                        string checkboxName = fieldParts[1] + "_chocolate_covered";
                        foreach (CheckBox checkBox in checkBoxes) {
                            if (checkBox.Name == checkboxName) {
                                checkBox.Checked = true;
                                break;
                            }
                        }

                        durabilityField.Value = _memoryValues.Inventory[fieldParts[1] + "_" + fieldParts[2]].Value - _chocolateCoveredDurabilityOffset;
                    } else {
                        /**
                         * Otherwise it's another item : we can set the value directly
                         * As for the chocolate-covered checkbox, it's unchecked by default
                         */
                        durabilityField.Value = _memoryValues.Inventory[fieldParts[1] + "_" + fieldParts[2]].Value;
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

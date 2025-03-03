using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;
using BokInterface.Items;

namespace BokInterface.Inventory {
    /// <summary>Inventory editor for Boktai 2</summary>
    class ZoktaiInventoryEditor : InventoryEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ZoktaiAddresses _zoktaiAddresses;
        private readonly ZoktaiItems _zoktaiItems;
        protected readonly List<CheckBox> checkBoxes = [];
        /// <summary>
        /// <para>Default maximum durability value that can be set</para>
        /// <para>This is eventually replaced based on dropdown selected items</para>
        /// </summary>
        private readonly int _defaultMaxDurability = 7679;
        /// <summary>Durabiliy offset for "Chocolate-covered" items</summary>
        private readonly int _chocolateCoveredDurabilityOffset = 32768;

        #endregion

        public ZoktaiInventoryEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            _zoktaiItems = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(628, 433);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.inventoryEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() {

            // Instanciate checkGroupBoxes
            InstanciateCheckGroupBoxes();

            // 1st row
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_item", 5, 19, 140, 23, slot1group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot1", "Durability", 2, 50, 58, 15, slot1group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot1_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot1group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot1_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot1group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_item", 5, 19, 140, 23, slot2group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot2", "Durability", 2, 50, 58, 15, slot2group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot2_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot2group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot2_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot2group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_item", 5, 19, 140, 23, slot3group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot3", "Durability", 2, 50, 58, 15, slot3group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot3_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot3group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot3_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot3group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_item", 5, 19, 140, 23, slot4group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot4", "Durability", 2, 50, 58, 15, slot4group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot4_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot4group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot4_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot4group));

            // 2nd row
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_item", 5, 19, 140, 23, slot5group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot5", "Durability", 2, 50, 58, 15, slot5group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot5_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot5group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot5_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot5group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_item", 5, 19, 140, 23, slot6group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot6", "Durability", 2, 50, 58, 15, slot6group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot6_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot6group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot6_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot6group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_item", 5, 19, 140, 23, slot7group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot7", "Durability", 2, 50, 58, 15, slot7group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot7_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot7group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot7_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot7group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_item", 5, 19, 140, 23, slot8group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot8", "Durability", 2, 50, 58, 15, slot8group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot8_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot8group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot8_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot8group));

            // 3rd row
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_item", 5, 19, 140, 23, slot9group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot9", "Durability", 2, 50, 58, 15, slot9group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot9_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot9group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot9_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot9group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_item", 5, 19, 140, 23, slot10group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot10", "Durability", 2, 50, 58, 15, slot10group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot10_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot10group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot10_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot10group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_item", 5, 19, 140, 23, slot11group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot11", "Durability", 2, 50, 58, 15, slot11group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot11_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot11group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot11_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot11group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_item", 5, 19, 140, 23, slot12group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot12", "Durability", 2, 50, 58, 15, slot12group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot12_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot12group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot12_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot12group));

            // 4th row
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot13_item", 5, 19, 140, 23, slot13group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot13", "Durability", 2, 50, 58, 15, slot13group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot13_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot13group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot13_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot13group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot14_item", 5, 19, 140, 23, slot14group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot14", "Durability", 2, 50, 58, 15, slot14group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot14_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot14group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot14_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot14group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot15_item", 5, 19, 140, 23, slot15group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot15", "Durability", 2, 50, 58, 15, slot15group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot15_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot15group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot15_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot15group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot16_item", 5, 19, 140, 23, slot16group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot16", "Durability", 2, 50, 58, 15, slot16group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot16_durability", 0, 95, 48, 50, 23, 0, _defaultMaxDurability, control: slot16group));
            checkBoxes.Add(WinFormHelpers.CreateCheckBox("slot16_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: slot16group));

            // Generate & add options to dropdowns
            GenerateDropDownOptions();

            // Set default values for each field
            SetDefaultValues();

            /**
             * Due to some items taking longer to rott,
             * For each slot, when the another item is selected
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

        ///<summary>Separated method for instanciating checkGroupBox instances</summary>
        protected void InstanciateCheckGroupBoxes() {
            slot1group = WinFormHelpers.CreateCheckGroupBox("slot1group", "Slot 1", 5, 5, 150, 95, control: this);
            slot2group = WinFormHelpers.CreateCheckGroupBox("slot2group", "Slot 2", 161, 5, 150, 95, control: this);
            slot3group = WinFormHelpers.CreateCheckGroupBox("slot3group", "Slot 3", 317, 5, 150, 95, control: this);
            slot4group = WinFormHelpers.CreateCheckGroupBox("slot4group", "Slot 4", 473, 5, 150, 95, control: this);
            slot5group = WinFormHelpers.CreateCheckGroupBox("slot5group", "Slot 5", 5, 107, 150, 95, control: this);
            slot6group = WinFormHelpers.CreateCheckGroupBox("slot6group", "Slot 6", 161, 107, 150, 95, control: this);
            slot7group = WinFormHelpers.CreateCheckGroupBox("slot7group", "Slot 7", 317, 107, 150, 95, control: this);
            slot8group = WinFormHelpers.CreateCheckGroupBox("slot8group", "Slot 8", 473, 107, 150, 95, control: this);
            slot9group = WinFormHelpers.CreateCheckGroupBox("slot9group", "Slot 9", 5, 207, 150, 95, control: this);
            slot10group = WinFormHelpers.CreateCheckGroupBox("slot10group", "Slot 10", 161, 207, 150, 95, control: this);
            slot11group = WinFormHelpers.CreateCheckGroupBox("slot11group", "Slot 11", 317, 207, 150, 95, control: this);
            slot12group = WinFormHelpers.CreateCheckGroupBox("slot12group", "Slot 12", 473, 207, 150, 95, control: this);
            slot13group = WinFormHelpers.CreateCheckGroupBox("slot13group", "Slot 13", 5, 310, 150, 95, control: this);
            slot14group = WinFormHelpers.CreateCheckGroupBox("slot14group", "Slot 14", 161, 310, 150, 95, control: this);
            slot15group = WinFormHelpers.CreateCheckGroupBox("slot15group", "Slot 15", 317, 310, 150, 95, control: this);
            slot16group = WinFormHelpers.CreateCheckGroupBox("slot16group", "Slot 16", 473, 310, 150, 95, control: this);
        }

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
            switch (subList) {
                case "inventory":
                    if (_memoryValues.Inventory.ContainsKey(valueKey) == true) {
                        _memoryValues.Inventory[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
                default:
                    if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
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

        ///<summary>Get an item from the items list by using its value</summary>
        ///<param name="value"><c>decimal</c>Value</param>
        ///<returns><c>Item</c>Item</returns>
        private Item? GetItemByValue(decimal value) {
            foreach (KeyValuePair<string, Item> index in _zoktaiItems.Items) {
                Item item = index.Value;
                if (item.value == value) {
                    return item;
                }
            }

            return null;
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

        protected override void SetDefaultValues() {

            // If "current stat" is a valid value, get the current inventory
            uint currentStat = APIs.Memory.ReadU32(_zoktaiAddresses.Misc["current_stat"].Address);
            if (currentStat > 0) {
                foreach (ImageComboBox dropdown in dropDownLists) {
                    /**
                     * Get the name of the field to retrieve the value from based on the dropdown's name (for example inventory_slotX_item => slotX_item)
                     * Then try getting the corresponding item & preselect it
                     */
                    string[] fieldParts = dropdown.Name.Split(['_'], 2);
                    Item? selectedItem = GetItemByValue(_memoryValues.Inventory[fieldParts[1]].Value);
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
                foreach (ImageComboBox dropdown in dropDownLists) {
                    dropdown.SelectedIndex = 0;
                }

                foreach (NumericUpDown durabilityField in numericUpDowns) {
                    durabilityField.Value = 0;
                }
            }
        }
    }
}
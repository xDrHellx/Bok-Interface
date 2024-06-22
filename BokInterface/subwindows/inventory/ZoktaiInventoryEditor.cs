using System;
using System.Collections.Generic;
using System.Drawing;
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
        /// <summary>
        /// <para>Default maximum durability value that can be set</para>
        /// <para>This is eventually replaced based on dropdown selected items</para>
        /// </summary>
        private readonly int _defaultMaxDurability = 7679;

        #endregion

        public ZoktaiInventoryEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            _zoktaiItems = new(_memoryValues, _zoktaiAddresses);

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(629, 346);

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
            WinFormHelpers.CreateLabel("slot1", "Durability", 5, 50, 58, 15, slot1group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot1_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot1group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_item", 5, 19, 140, 23, slot2group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot2", "Durability", 5, 50, 58, 15, slot2group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot2_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot2group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_item", 5, 19, 140, 23, slot3group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot3", "Durability", 5, 50, 58, 15, slot3group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot3_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot3group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_item", 5, 19, 140, 23, slot4group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot4", "Durability", 5, 50, 58, 15, slot4group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot4_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot4group));

            // 2nd row
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_item", 5, 19, 140, 23, slot5group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot5", "Durability", 5, 50, 58, 15, slot5group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot5_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot5group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_item", 5, 19, 140, 23, slot6group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot6", "Durability", 5, 50, 58, 15, slot6group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot6_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot6group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_item", 5, 19, 140, 23, slot7group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot7", "Durability", 5, 50, 58, 15, slot7group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot7_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot7group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_item", 5, 19, 140, 23, slot8group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot8", "Durability", 5, 50, 58, 15, slot8group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot8_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot8group));

            // 3rd row
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_item", 5, 19, 140, 23, slot9group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot9", "Durability", 5, 50, 58, 15, slot9group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot9_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot9group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_item", 5, 19, 140, 23, slot10group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot10", "Durability", 5, 50, 58, 15, slot10group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot10_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot10group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_item", 5, 19, 140, 23, slot11group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot11", "Durability", 5, 50, 58, 15, slot11group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot11_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot11group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_item", 5, 19, 140, 23, slot12group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot12", "Durability", 5, 50, 58, 15, slot12group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot12_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot12group));

            // 4th row
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot13_item", 5, 19, 140, 23, slot13group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot13", "Durability", 5, 50, 58, 15, slot13group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot13_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot13group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot14_item", 5, 19, 140, 23, slot14group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot14", "Durability", 5, 50, 58, 15, slot14group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot14_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot14group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot15_item", 5, 19, 140, 23, slot15group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot15", "Durability", 5, 50, 58, 15, slot15group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot15_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot15group));

            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot16_item", 5, 19, 140, 23, slot16group, visibleOptions: 5));
            WinFormHelpers.CreateLabel("slot16", "Durability", 5, 50, 58, 15, slot16group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot16_durability", 0, 95, 47, 50, 23, 0, _defaultMaxDurability, control: slot16group));

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
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 549, 320, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        ///<summary>Separated method for instanciating checkGroupBox instances</summary>
        protected void InstanciateCheckGroupBoxes() {
            slot1group = WinFormHelpers.CreateCheckGroupBox("slot1group", "Slot 1", 5, 5, 150, 76, control: this);
            slot2group = WinFormHelpers.CreateCheckGroupBox("slot2group", "Slot 2", 161, 5, 150, 76, control: this);
            slot3group = WinFormHelpers.CreateCheckGroupBox("slot3group", "Slot 3", 317, 5, 150, 76, control: this);
            slot4group = WinFormHelpers.CreateCheckGroupBox("slot4group", "Slot 4", 473, 5, 150, 76, control: this);
            slot5group = WinFormHelpers.CreateCheckGroupBox("slot5group", "Slot 5", 5, 84, 150, 76, control: this);
            slot6group = WinFormHelpers.CreateCheckGroupBox("slot6group", "Slot 6", 161, 84, 150, 76, control: this);
            slot7group = WinFormHelpers.CreateCheckGroupBox("slot7group", "Slot 7", 317, 84, 150, 76, control: this);
            slot8group = WinFormHelpers.CreateCheckGroupBox("slot8group", "Slot 8", 473, 84, 150, 76, control: this);
            slot9group = WinFormHelpers.CreateCheckGroupBox("slot9group", "Slot 9", 5, 163, 150, 76, control: this);
            slot10group = WinFormHelpers.CreateCheckGroupBox("slot10group", "Slot 10", 161, 163, 150, 76, control: this);
            slot11group = WinFormHelpers.CreateCheckGroupBox("slot11group", "Slot 11", 317, 163, 150, 76, control: this);
            slot12group = WinFormHelpers.CreateCheckGroupBox("slot12group", "Slot 12", 473, 163, 150, 76, control: this);
            slot13group = WinFormHelpers.CreateCheckGroupBox("slot13group", "Slot 13", 5, 242, 150, 76, control: this);
            slot14group = WinFormHelpers.CreateCheckGroupBox("slot14group", "Slot 14", 161, 242, 150, 76, control: this);
            slot15group = WinFormHelpers.CreateCheckGroupBox("slot15group", "Slot 15", 317, 242, 150, 76, control: this);
            slot16group = WinFormHelpers.CreateCheckGroupBox("slot16group", "Slot 16", 473, 242, 150, 76, control: this);
        }


        protected override void SetValues() {

            // Retrieve all input fields
            List<ImageComboBox> slots = dropDownLists;
            List<NumericUpDown> durabilities = numericUpDowns;

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            // Sets values for each slot
            for (int i = 0; i < slots.Count; i++) {

                // If the slot is disabled, skip it
                if (slots[i].Enabled == false) {
                    continue;
                }

                KeyValuePair<string, Item> selectedOption = (KeyValuePair<string, Item>)slots[i].SelectedItem;
                Item selectedItem = selectedOption.Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the slot's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = slots[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], selectedItem.value);
            }

            // Repeat the above process for Durabilities
            for (int i = 0; i < durabilities.Count; i++) {

                if (durabilities[i].Enabled == false) {
                    continue;
                }

                decimal value = durabilities[i].Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the slot's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = durabilities[i].Name.Split(['_'], 2);
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
        ///<para>Method for setting memory values</para>
        ///<para>This is separated because we use the switch inside on different types</para>
        ///</summary>
        ///<param name="subList"><c>Sublit / dictionnary the key belongs to</c></param>
        ///<param name="valueKey"><c>strng</c>Key withint the dictionnary</param>
        ///<param name="value"><c>decimal</c>Value to set</param>
        private void SetMemoryValue(string subList, string valueKey, decimal value) {
            string memoryValueKey = valueKey;
            switch (subList) {
                case "django":
                    if (_memoryValues.Django.ContainsKey(memoryValueKey) == true) {

                        // Depending on the key, we treat the value setting differently
                        switch (memoryValueKey) {
                            case "vit":                     // Stats
                            case "spr":
                            case "str":
                            case "agi":
                                /**
                                 * For stats wz also update the "persistent" stat address
                                 * 
                                 * We do this because updating "current" stat value is not enough,
                                 * when switching room the game would set back the old values
                                 */
                                _memoryValues.Django[memoryValueKey].Value = (uint)value;
                                if (_memoryValues.Misc.ContainsKey(memoryValueKey) == true) {
                                    _memoryValues.Misc[memoryValueKey].Value = (uint)value;
                                }
                                break;
                            case "sword_skill":             // Skill
                            case "spear_skill":
                            case "hammer_skill":
                            case "fists_skill":
                            case "gun_skill":
                                _memoryValues.Django[memoryValueKey].Value = Utilities.LevelToExp(value);
                                break;
                            default:                        // Default treatment
                                _memoryValues.Django[memoryValueKey].Value = (uint)value;
                                break;
                        }

                    } else if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U16[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U32[memoryValueKey].Value = (uint)value;
                    }
                    break;
                case "solls":
                    if (_memoryValues.Solls.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.Solls[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U16[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U32[memoryValueKey].Value = (uint)value;
                    }
                    break;
                case "inventory":
                    if (_memoryValues.Inventory.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.Inventory[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U16[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U32[memoryValueKey].Value = (uint)value;
                    }
                    break;
                case "misc":
                    if (_memoryValues.Misc.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.Misc[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U16[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U32[memoryValueKey].Value = (uint)value;
                    }
                    break;
                default:
                    if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U16[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U32[memoryValueKey].Value = (uint)value;
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
                        field.Maximum = selectedItem.rottenAt - 1;
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
                    // Same treatment as above
                    string[] fieldParts = durabilityField.Name.Split(['_'], 2);
                    durabilityField.Value = _memoryValues.Inventory[fieldParts[1]].Value;
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
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Accessories {
    class ShinbokAccessoriesEditor : AccessoriesEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _shinbokAddresses;
        private readonly ShinbokAccessories _shinbokAccessories;

        #endregion

        public ShinbokAccessoriesEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses shinbokAddresses) {

            _memoryValues = memoryValues;
            _shinbokAddresses = shinbokAddresses;

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;
            _shinbokAccessories = new();

            SetFormParameters(691, 240);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.equipsEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() {

            // Instanciate checkGroupBoxes
            InstanciateCheckGroupBoxes();

            // 1st row
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_accessory", 5, 19, 160, 23, slot1group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_accessory", 5, 19, 160, 23, slot2group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_accessory", 5, 19, 160, 23, slot3group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_accessory", 5, 19, 160, 23, slot4group, visibleOptions: 5));

            // 2nd row
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_accessory", 5, 19, 160, 23, slot5group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_accessory", 5, 19, 160, 23, slot6group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_accessory", 5, 19, 160, 23, slot7group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_accessory", 5, 19, 160, 23, slot8group, visibleOptions: 5));

            // 3rd row
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_accessory", 5, 19, 160, 23, slot9group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_accessory", 5, 19, 160, 23, slot10group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_accessory", 5, 19, 160, 23, slot11group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_accessory", 5, 19, 160, 23, slot12group, visibleOptions: 5));

            // 4th row
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot13_accessory", 5, 19, 160, 23, slot13group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot14_accessory", 5, 19, 160, 23, slot14group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot15_accessory", 5, 19, 160, 23, slot15group, visibleOptions: 5));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot16_accessory", 5, 19, 160, 23, slot16group, visibleOptions: 5));

            // Generate & add options to dropdowns
            GenerateDropDownOptions();

            // Set default values for each field
            SetDefaultValues();

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 629, 213, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        ///<summary>Separated method for instanciating checkGroupBox instances</summary>
        protected void InstanciateCheckGroupBoxes() {
            slot1group = WinFormHelpers.CreateCheckGroupBox("slot1group", "Slot 1", 5, 5, 170, 49, control: this);
            slot2group = WinFormHelpers.CreateCheckGroupBox("slot2group", "Slot 2", 181, 5, 170, 49, control: this);
            slot3group = WinFormHelpers.CreateCheckGroupBox("slot3group", "Slot 3", 357, 5, 170, 49, control: this);
            slot4group = WinFormHelpers.CreateCheckGroupBox("slot4group", "Slot 4", 533, 5, 170, 49, control: this);
            slot5group = WinFormHelpers.CreateCheckGroupBox("slot5group", "Slot 5", 5, 57, 170, 49, control: this);
            slot6group = WinFormHelpers.CreateCheckGroupBox("slot6group", "Slot 6", 181, 57, 170, 49, control: this);
            slot7group = WinFormHelpers.CreateCheckGroupBox("slot7group", "Slot 7", 357, 57, 170, 49, control: this);
            slot8group = WinFormHelpers.CreateCheckGroupBox("slot8group", "Slot 8", 533, 57, 170, 49, control: this);
            slot9group = WinFormHelpers.CreateCheckGroupBox("slot9group", "Slot 9", 5, 109, 170, 49, control: this);
            slot10group = WinFormHelpers.CreateCheckGroupBox("slot10group", "Slot 10", 181, 109, 170, 49, control: this);
            slot11group = WinFormHelpers.CreateCheckGroupBox("slot11group", "Slot 11", 357, 109, 170, 49, control: this);
            slot12group = WinFormHelpers.CreateCheckGroupBox("slot12group", "Slot 12", 533, 109, 170, 49, control: this);
            slot13group = WinFormHelpers.CreateCheckGroupBox("slot13group", "Slot 13", 5, 161, 170, 49, control: this);
            slot14group = WinFormHelpers.CreateCheckGroupBox("slot14group", "Slot 14", 181, 161, 170, 49, control: this);
            slot15group = WinFormHelpers.CreateCheckGroupBox("slot15group", "Slot 15", 357, 161, 170, 49, control: this);
            slot16group = WinFormHelpers.CreateCheckGroupBox("slot16group", "Slot 16", 533, 161, 170, 49, control: this);
        }

        protected override void SetValues() {

            // Retrieve all input fields
            List<ImageComboBox> slots = dropDownLists;

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

                KeyValuePair<string, Accessory> selectedOption = (KeyValuePair<string, Accessory>)slots[i].SelectedItem;
                Accessory selectedAccessory = selectedOption.Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the slot's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = slots[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], selectedAccessory.value);
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
                        _memoryValues.Django[memoryValueKey].Value = (uint)value;
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
                dropdown.DataSource = new BindingSource(_shinbokAccessories.All, null);
                dropdown.DisplayMember = "Key";
                dropdown.ValueMember = "Value";
            }
        }

        ///<summary>Get an accessory from the accessories list by using its value</summary>
        ///<param name="value"><c>decimal</c>Value</param>
        ///<returns><c>Accessory</c>Accessory</returns>
        private Accessory? GetAccessoryByValue(decimal value) {
            foreach (KeyValuePair<string, Accessory> index in _shinbokAccessories.All) {
                Accessory accessory = index.Value;
                if (accessory.value == value) {
                    return accessory;
                }
            }

            return null;
        }

        protected override void SetDefaultValues() {

            /**
			 * If Django's current HP is a valid, try retrieving the current accessory inventory
			 * (Django's current HP goes below 0 or above 1000 when switching rooms, during bike races or on world map)
			 */
            uint djangoCurrentHp = _memoryValues.Django["current_hp"].Value;
            if (djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
                foreach (ImageComboBox dropdown in dropDownLists) {
                    /**
                     * Get the name of the field to retrieve the value from based on the dropdown's name (for example inventory_slotX_accessory => slotX_accessory)
                     * Then try getting the corresponding item & preselect it
                     */
                    string[] fieldParts = dropdown.Name.Split(['_'], 2);
                    Accessory? selectedAccessory = GetAccessoryByValue(_memoryValues.Inventory[fieldParts[1]].Value);
                    if (selectedAccessory != null) {
                        dropdown.SelectedIndex = dropdown.FindStringExact(selectedAccessory.name);
                    }
                }
            } else {
                // Otherwise set default values in the editor subwindow
                foreach (ImageComboBox dropdown in dropDownLists) {
                    dropdown.SelectedIndex = 0;
                }
            }
        }
    }
}
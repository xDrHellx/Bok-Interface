using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;
using BokInterface.Items;
using System.Reflection;

namespace BokInterface.KeyItems {
    /// <summary>Key items editor for Boktai 2</summary>
    class ZoktaiKeyItemsEditor : KeyItemsEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ZoktaiAddresses _zoktaiAddresses;
        private readonly ZoktaiItems _zoktaiItems;

        #endregion

        #region Constructor

        public ZoktaiKeyItemsEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            _zoktaiItems = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(691, 240);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Generate groups with subelements & add options to dropdowns
            GenerateGroups();
            AddDropDownOptions(dropDownLists, _zoktaiItems.KeyItems);

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

        ///<summary>Separated method for generating groups with subelements</summary>
        protected void GenerateGroups() {
            int xPos = 5,
                yPos = 5;
            for (int i = 1; i < 17; i++) {

                // Generate the group for each property dynamically
                PropertyInfo property = GetType().GetProperty($"slot{i}group", BindingFlags.Instance | BindingFlags.NonPublic);
                if (property != null) {
                    CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"slot{i}group", $"Slot {i}", xPos, yPos, 170, 49, control: this);
                    property.SetValue(this, group);

                    // Add the dropdown to it
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_key_item", 5, 19, 160, 23, group, visibleOptions: 5));
                }

                // Offsets for position
                xPos += 176;
                if ((i % 4) == 0) {
                    xPos = 5;
                    yPos += 52;
                }
            }
        }

        ///<summary>Add the options for a list of dropdowns</summary>
        ///<param name="list">List of dropdowns</param>
        ///<param name="dictionnary">Dictionnary containing the data to use for the dropdown options</param>
        private void AddDropDownOptions(List<ImageComboBox> list, object dictionnary) {
            foreach (ImageComboBox dropdown in list) {
                dropdown.DataSource = new BindingSource(dictionnary, null);
                dropdown.DisplayMember = "Key";
                dropdown.ValueMember = "Value";
            }
        }

        #endregion

        #region Values setting

        protected override void SetValues() {

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            // Sets values for each slot (dropdown)
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
            if (subList == "inventory" && _memoryValues.Inventory.ContainsKey(valueKey) == true) {
                _memoryValues.Inventory[valueKey].Value = (uint)value;
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
            } else {
                // If current stat is unvalid (for example because we are on the title screen or in a room transition), use specific values
                foreach (ImageComboBox dropdown in dropDownLists) {
                    dropdown.SelectedIndex = 0;
                }
            }
        }

        ///<summary>Get an item from the items list by using its value</summary>
        ///<param name="value"><c>decimal</c>Value</param>
        ///<returns><c>Item</c>Item</returns>
        private Item? GetItemByValue(decimal value) {
            foreach (KeyValuePair<string, Item> index in _zoktaiItems.KeyItems) {
                Item item = index.Value;
                if (item.value == value) {
                    return item;
                }
            }

            return null;
        }

        #endregion
    }
}

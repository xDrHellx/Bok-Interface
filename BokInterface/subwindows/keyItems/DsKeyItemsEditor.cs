using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Items;
using BokInterface.Utils;

namespace BokInterface.KeyItems {
    /// <summary>Key items editor for Lunar Knights / Boktai DS</summary>
    class DsKeyItemsEditor : KeyItemsEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly DsAddresses _memoryAddresses;
        private readonly DsItems _dsItems;
        protected CheckGroupBox? slot17group { get; set; }
        protected CheckGroupBox? slot18group { get; set; }
        protected CheckGroupBox? slot19group { get; set; }
        protected CheckGroupBox? slot20group { get; set; }

        #endregion

        #region Constructor

        public DsKeyItemsEditor(BokInterface bokInterface, MemoryValues memoryValues, DsAddresses memoryAddresses) {

            _memoryValues = memoryValues;
            _memoryAddresses = memoryAddresses;
            _dsItems = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(788, 292);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Generate groups with subelements & add options to dropdowns
            GenerateGroups();
            AddDropDownOptions(dropDownLists, _dsItems.KeyItems);

            // Add warning
            Label expWarning = WinFormHelpers.CreateImageLabel("tooltip", "warning", 5, 268, this);
            WinFormHelpers.CreateLabel("warning", "Inventory will be updated upon switching tab in-game or closing and reopening the menu.", 23, 261, 503, 30, this, textAlignment: "MiddleLeft");

            // Set default values for each field
            SetDefaultValues();

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setValuesButton", "Set values", 709, 265, 75, 23, this);
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
                    CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"slot{i}group", $"Slot {i}", xPos, yPos, 190, 49, control: this);
                    property.SetValue(this, group);

                    // Add the dropdown to it
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_key_item_slot_{i}", 5, 19, 180, 23, group, visibleOptions: 5));
                }

                // Offsets for position
                xPos += 196;
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
                    Item? selectedItem = GetItemByValue(_memoryAddresses.Inventory[fieldParts[1]].Value);
                    if (selectedItem != null) {
                        dropdown.SelectedIndex = dropdown.FindStringExact(selectedItem.name);
                    }
                }
            } else {
                // If current HP values are unvalid (for example because we are on the title screen or in a room transition), use specific values
                foreach (ImageComboBox dropdown in dropDownLists) {
                    dropdown.SelectedIndex = 0;
                }
            }
        }

        ///<summary>Get an item from the items list by using its value</summary>
        ///<param name="value">Value</param>
        ///<returns><c>Item</c>Item</returns>
        private Item? GetItemByValue(decimal value) {
            foreach (KeyValuePair<string, Item> index in _dsItems.KeyItems) {
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

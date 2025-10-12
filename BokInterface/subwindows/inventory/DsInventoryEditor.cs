using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Items;
using BokInterface.Utils;

namespace BokInterface.Inventory {
    /// <summary>Inventory editor for Lunar Knights & Boktai DS</summary>
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

        #endregion

        #region Constructor

        public dsInventoryEditor(BokInterface bokInterface, MemoryValues memoryValues, DsAddresses memoryAddresses) {

            _memoryValues = memoryValues;
            _memoryAddresses = memoryAddresses;
            _dsItems = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(623, 538);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {
            InstanciateCheckGroupBoxes();
            for (int i = 1; i < 21; i++) {

                // Get the group dynamically from the property
                PropertyInfo property = GetType().GetProperty($"slot{i}group", BindingFlags.Instance | BindingFlags.NonPublic);
                if (property != null && property.GetValue(this) is CheckGroupBox group) {

                    // Add elements
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_item_slot_{i}", 5, 19, 140, 23, group, visibleOptions: 5));
                    // WinFormHelpers.CreateLabel($"slot{i}", "Durability", 2, 50, 58, 15, group);
                    // numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown($"inventory_slot{i}_durability", 0, 95, 48, 50, 23, 0, 9999, control: group)); // 9999 = durability
                    // checkBoxes.Add(WinFormHelpers.CreateCheckBox($"slot{i}_chocolate_covered", "Chocolate-covered", 12, 74, 134, 19, checkboxOnRight: true, control: group));
                }
            }

            // Generate & add options to dropdowns
            GenerateDropDownOptions();

            // Set default values for each field
            SetDefaultValues();

            // TODO Find item durability address
            // TODO Find chocolated-covered offset
            // TODO Find durability values for items

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setValuesButton", "Set values", 544, 511, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        ///<summary>Separated method for instanciating checkGroupBox instances</summary>
        protected void InstanciateCheckGroupBoxes() {
            int xPos = 5,
                yPos = 5;
            for (int i = 1; i < 21; i++) {

                // Generate the group for each property dynamically
                PropertyInfo property = GetType().GetProperty($"slot{i}group", BindingFlags.Instance | BindingFlags.NonPublic);
                if (property != null) {
                    CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"slot{i}group", $"Slot {i}", xPos, yPos, 150, 95, control: this);
                    property.SetValue(this, group);
                }

                // Offsets for position
                xPos += 156;
                if ((i % 4) == 0) {
                    xPos = 0;
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
            switch (subList) {
                case "inventory":
                    if (_memoryAddresses.Inventory.ContainsKey(valueKey) == true) {
                        _memoryAddresses.Inventory[valueKey].Value = (uint)value;
                    }
                    break;
                default: break;
            }
        }

        protected override void SetDefaultValues() {

            /**
             * If Lucian or Aaron HP value is valid
             * (Invalid when below 0 or above 9999, ie when switching rooms or on world map)
             */
            uint lucianCurrentHp = _memoryAddresses.Player["lucian_current_hp"].Value,
                aaronCurrentHp = _memoryAddresses.Player["aaron_current_hp"].Value;
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
                // If current stat is unvalid (for example because we are on the title screen or in a room transition), use specific values
                foreach (ImageComboBox dropdown in dropDownLists) {
                    dropdown.SelectedIndex = 0;
                }
            }
        }

        ///<summary>Get an item from the items list by using its value</summary>
        ///<param name="value">Value</param>
        ///<returns><c>Item</c>Item</returns>
        private Item? GetItemByValue(decimal value) {
            foreach (KeyValuePair<string, Item> index in _dsItems.Items) {
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

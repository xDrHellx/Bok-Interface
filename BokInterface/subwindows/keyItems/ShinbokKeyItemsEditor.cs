using System.Collections.Generic;

using BokInterface.Addresses;
using BokInterface.Utils;
using BokInterface.Items;
using System.Reflection;

namespace BokInterface.KeyItems {
    /// <summary>Key items editor for Boktai 3</summary>
    class ShinbokKeyItemsEditor : KeyItemsEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _shinbokAddresses;
        private readonly ShinbokItems _shinbokItems;

        #endregion

        #region Constructor

        public ShinbokKeyItemsEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses shinbokAddresses) {

            _memoryValues = memoryValues;
            _shinbokAddresses = shinbokAddresses;
            _shinbokItems = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(691, 240, name, text);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Generate groups with subelements & add options to dropdowns
            GenerateGroups();
            AddDropDownOptions(dropDownLists, _shinbokItems.KeyItems);

            // Set default values for each field
            SetDefaultValues();

            AddSetValuesButton(629, 213, this);
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

            /**
             * If Django's HP value is valid
             * (Invalid when below 0 or above 1000, ie when switching rooms, during bike races or on world map)
             */
            uint djangoCurrentHp = _memoryValues.Django["current_hp"].Value;
            if (djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
                foreach (ImageComboBox dropdown in dropDownLists) {
                    /**
                     * Get the name of the field to retrieve the value from based on the dropdown's name (for example inventory_slotX_item => slotX_item)
                     * Then try getting the corresponding item & preselect it
                     */
                    string[] fieldParts = dropdown.Name.Split(['_'], 2);
                    Item? selectedItem = GetItemByValue(_memoryValues.Inventory[fieldParts[1]].Value, _shinbokItems.KeyItems);
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

        #endregion
    }
}

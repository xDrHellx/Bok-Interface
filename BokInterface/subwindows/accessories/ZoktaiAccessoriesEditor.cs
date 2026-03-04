using System.Collections.Generic;
using System.Reflection;

using BokInterface.Addresses;
using BokInterface.Utils;

namespace BokInterface.Accessories {
    class ZoktaiAccessoriesEditor : AccessoriesEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ZoktaiAddresses _zoktaiAddresses;
        private readonly ZoktaiAccessories _zoktaiAccessories;

        #endregion

        #region Constructor

        public ZoktaiAccessoriesEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            _zoktaiAccessories = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(691, 240, name, "Protectors editor");
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Generate tabs, subelements & dropdown options
            GenerateTabs();
            AddDropDownOptions(dropDownLists, _zoktaiAccessories.All);

            // Set default values for each field
            SetDefaultValues();

            AddSetValuesButton(629, 213, this);
        }

        ///<summary>Separated method for generating groups with subelements</summary>
        protected void GenerateTabs() {
            int xPos = 5,
                yPos = 5;
            for (int i = 1; i < 17; i++) {

                // Generate the group for each property dynamically & add the dropdown to it
                PropertyInfo property = GetType().GetProperty($"slot{i}group", BindingFlags.Instance | BindingFlags.NonPublic);
                if (property != null) {
                    CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"slot{i}group", $"Slot {i}", xPos, yPos, 170, 49, control: this);
                    property.SetValue(this, group);
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_accessory", 5, 19, 160, 23, group, visibleOptions: 5));
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

                KeyValuePair<string, Accessory> selectedOption = (KeyValuePair<string, Accessory>)dropDownLists[i].SelectedItem;
                Accessory selectedAccessory = selectedOption.Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the slot's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = dropDownLists[i].Name.Split(['_'], 2);
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
        ///     Method for setting memory values.<br/>
        ///     This is separated because we use the switch inside on different types.
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
            foreach (ImageComboBox dropdown in dropDownLists) {
                /**
                 * Get the name of the field to retrieve the value from based on the dropdown's name (for example inventory_slotX_accessory => slotX_accessory)
                 * Then try getting the corresponding item & preselect it
                 */
                string[] fieldParts = dropdown.Name.Split(['_'], 2);
                Accessory? selectedAccessory = GetAccessoryByValue(_memoryValues.Inventory[fieldParts[1]].Value, _zoktaiAccessories.All);
                if (selectedAccessory != null) {
                    dropdown.SelectedIndex = dropdown.FindStringExact(selectedAccessory.name);
                }
            }
        }

        #endregion
    }
}

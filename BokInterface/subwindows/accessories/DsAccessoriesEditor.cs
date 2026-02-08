using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;

namespace BokInterface.Accessories {
    class DsAccessoriesEditor : AccessoriesEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly DsAddresses _dsAddresses;
        private readonly DsAccessories _dsAccessories;
        protected readonly List<ImageComboBox> shieldDropdowns = [];
        protected Dictionary<string, Accessory> shields = [];
        protected TabControl inventoryTabControl = new();
        protected TabPage accessoriesTab = new(),
            shieldsTab = new();
        protected CheckGroupBox? shieldSlot1group { get; set; }
        protected CheckGroupBox? shieldSlot2group { get; set; }
        protected CheckGroupBox? shieldSlot3group { get; set; }
        protected CheckGroupBox? shieldSlot4group { get; set; }

        #endregion

        #region Constructor

        public DsAccessoriesEditor(BokInterface bokInterface, MemoryValues memoryValues, DsAddresses dsAddresses) {

            _memoryValues = memoryValues;
            _dsAddresses = dsAddresses;
            _dsAccessories = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(726, 278, name, text);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Generate the shields dictionnary for the dropdowns
            GenerateShieldsDictionnary();

            // Generate tabs, subelements & dropdown options
            GenerateTabs();
            AddDropDownOptions(dropDownLists, _dsAccessories.Equipment);
            AddDropDownOptions(shieldDropdowns, shields);

            // Add warning
            Label expWarning = WinFormHelpers.CreateImageLabel("tooltip", "warning", 5, 255, this);
            WinFormHelpers.CreateLabel("warning", "Inventory will be updated upon switching tab in-game or closing and reopening the menu.", 23, 248, 503, 30, this, textAlignment: "MiddleLeft");

            // Set default values for each field
            SetDefaultValues();

            AddSetValuesButton(648, 252, this);
        }

        /// <summary>Generate the dictionnary for shields (including empty slot)</summary>
        protected void GenerateShieldsDictionnary() {
            shields.Add("Empty slot", new DsAccessory("Empty slot", 65535, ""));
            shields = shields
                .Concat(_dsAccessories.Shield)
                .ToDictionary(e => e.Key, e => e.Value);
        }

        ///<summary>Separated method for generating tabs and subelements</summary>
        protected void GenerateTabs() {

            // Tabs
            inventoryTabControl = WinFormHelpers.CreateTabControl("inventory_ctrl", 5, 5, 718, 243, this);
            accessoriesTab = WinFormHelpers.CreateTabPage("accessories_tab", "Accessories", tabControl: inventoryTabControl);
            shieldsTab = WinFormHelpers.CreateTabPage("shields_tab", "Shields", tabControl: inventoryTabControl);

            // Accessory slots
            int xPos = 5,
                yPos = 5;
            for (int i = 1; i < 17; i++) {

                // Generate the group for each property dynamically & add the dropdown to it
                PropertyInfo property = GetType().GetProperty($"slot{i}group", BindingFlags.Instance | BindingFlags.NonPublic);
                if (property != null) {
                    CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"slot{i}group", $"Slot {i}", xPos, yPos, 170, 49, control: accessoriesTab);
                    property.SetValue(this, group);
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_accessory_slot_{i}", 5, 19, 160, 23, group, visibleOptions: 5));
                }

                // Offsets for position
                xPos += 176;
                if ((i % 4) == 0) {
                    xPos = 5;
                    yPos += 52;
                }
            }

            // Shield slots
            xPos = yPos = 5;
            for (int i = 1; i < 5; i++) {

                // Generate the group for each property dynamically & add the dropdown to it
                PropertyInfo property = GetType().GetProperty($"shieldSlot{i}group", BindingFlags.Instance | BindingFlags.NonPublic);
                if (property != null) {
                    CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"shieldSlot{i}group", $"Slot {i}", xPos, yPos, 170, 49, control: shieldsTab);
                    property.SetValue(this, group);
                    shieldDropdowns.Add(WinFormHelpers.CreateImageDropdownList($"inventory_shield_slot_{i}", 5, 19, 160, 23, group, visibleOptions: 5));
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

            // Sets values for each accessory slot
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

            // Same as above for shield slots
            for (int i = 0; i < shieldDropdowns.Count; i++) {
                if (shieldDropdowns[i].Enabled == false) {
                    continue;
                }

                KeyValuePair<string, Accessory> selectedOption = (KeyValuePair<string, Accessory>)shieldDropdowns[i].SelectedItem;
                Accessory selectedShield = selectedOption.Value;

                string[] fieldParts = shieldDropdowns[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], selectedShield.value);
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
            if (subList == "inventory" && _dsAddresses.Inventory.ContainsKey(valueKey) == true) {
                _dsAddresses.Inventory[valueKey].Value = (uint)value;
            }
        }

        protected override void SetDefaultValues() {

            // Accessory slots
            foreach (ImageComboBox dropdown in dropDownLists) {
                /**
                 * Get the name of the field to retrieve the value from based on the dropdown's name (for example inventory_slotX_accessory => slotX_accessory)
                 * Then try getting the corresponding item & preselect it
                 */
                string[] fieldParts = dropdown.Name.Split(['_'], 2);
                Accessory? selectedAccessory = GetAccessoryByValue(_dsAddresses.Inventory[fieldParts[1]].Value, _dsAccessories.All);
                if (selectedAccessory != null) {
                    dropdown.SelectedIndex = dropdown.FindStringExact(selectedAccessory.name);
                }
            }

            // Same as above for shield slots
            foreach (ImageComboBox dropdown in shieldDropdowns) {
                string[] fieldParts = dropdown.Name.Split(['_'], 2);
                Accessory? selectedShield = GetAccessoryByValue(_dsAddresses.Inventory[fieldParts[1]].Value, _dsAccessories.All);
                if (selectedShield != null) {
                    dropdown.SelectedIndex = dropdown.FindStringExact(selectedShield.name);
                }
            }
        }

        #endregion
    }
}

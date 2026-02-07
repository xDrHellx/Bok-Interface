using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

using BokInterface.Abilities;
using BokInterface.Addresses;
using BokInterface.Utils;

namespace BokInterface.Weapons {
    /// <summary>Weapons editor for Boktai 3</summary>
    class ShinbokWeaponsEditor : WeaponsEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _shinbokAddresses;
        private readonly ShinbokWeapons _shinbokWeapons;
        private readonly ShinbokAbilities _shinbokAbilities;
        private readonly ShinbokSwordAttackPatterns _shinbokSwordAttackPatterns;
        protected readonly List<RadioButton> radioButtons = [];

        #endregion

        #region Constructor

        public ShinbokWeaponsEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses ShinbokAddresses) {

            _memoryValues = memoryValues;
            _shinbokAddresses = ShinbokAddresses;
            _shinbokWeapons = new();
            _shinbokAbilities = new();
            _shinbokSwordAttackPatterns = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(699, 312, name, text);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Add Panel, slot groups & options for dropdowns
            slotsPanel = WinFormHelpers.CreatePanel("slots_panel", 5, 94, 690, 214, this);
            GenerateGroups();
            GenerateDropDownOptions();

            // Add informative text
            AddInformativeText();

            // Set default values for each field
            SetDefaultValues();

            AddSetValuesButton(620, 65, this);
        }

        /// <summary>Separated method for generating groups with subelements</summary>
        protected void GenerateGroups() {
            int xPos = 0,
                yPos = 3;
            for (int i = 1; i < 17; i++) {

                // Generate group
                PropertyInfo property = GetType().GetProperty($"slot{i}group", BindingFlags.Instance | BindingFlags.NonPublic);
                if (property != null) {
                    CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"slot{i}group", $"Slot {i}", xPos, yPos, 160, 212, control: slotsPanel);
                    property.SetValue(this, group);

                    // Dropdown
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_weapon", 5, 19, 150, 23, group, visibleOptions: 5, enabled: false));

                    // Refine & durability
                    CheckGroupBox propertiesGroup = WinFormHelpers.CreateCheckGroupBox($"slot{i}_properties_group", "Properties", 5, 48, 150, 77, control: group);
                    WinFormHelpers.CreateLabel($"slot{i}_refine_label", "Refine", 2, 19, 40, 15, propertiesGroup);
                    radioButtons.Add(WinFormHelpers.CreateRadioButton($"inventory_slot{i}_weapon_refine_option_1", "I", 52, 19, 24, 19, propertiesGroup, 0));
                    radioButtons.Add(WinFormHelpers.CreateRadioButton($"inventory_slot{i}_weapon_refine_option_2", "II", 81, 19, 31, 19, propertiesGroup, 1));
                    radioButtons.Add(WinFormHelpers.CreateRadioButton($"inventory_slot{i}_weapon_refine_option_3", "III", 113, 19, 34, 19, propertiesGroup, 2));
                    WinFormHelpers.CreateLabel($"slot{i}_durability_label", "Durability", 2, 50, 58, 15, control: propertiesGroup);
                    numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown($"inventory_slot{i}_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: propertiesGroup, enabled: false));

                    // SP abilities
                    CheckGroupBox spAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox($"slot{i}_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: group);
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_weapon_sp_ability_1", 5, 19, 140, 23, spAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_weapon_sp_ability_2", 5, 48, 140, 23, spAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
                }

                // Offsets for position
                xPos += 166;
                if ((i % 4) == 0) {
                    xPos = 0;
                    yPos += 219;
                }
            }
        }

        /// <summary>Generate the options for the weapon selection and SP abilities dropdowns</summary>
        private void GenerateDropDownOptions() {
            foreach (ImageComboBox dropdown in dropDownLists) {

                // Indicate what the dropdown field is for
                string[] fieldParts = dropdown.Name.Split(['_'], 4);
                if (fieldParts.Length >= 4 && fieldParts[3] != null && fieldParts[3][..10] == "sp_ability") {
                    // If dropdown is for an SP ability
                    dropdown.DataSource = new BindingSource(_shinbokAbilities.Weapons, null);
                    dropdown.DisplayMember = "Key";
                    dropdown.ValueMember = "Value";
                } else {
                    // If dropdown is for the weapon itself
                    dropdown.DataSource = new BindingSource(_shinbokWeapons.All, null);
                    dropdown.DisplayMember = "Key";
                    dropdown.ValueMember = "Value";
                }
            }
        }

        /// <summary>Add informative text regarding weapons</summary>
        protected void AddInformativeText() {
            WinFormHelpers.CreateTextBox("weaponInfoText",
                "Regarding weapons :"
                + "\r\n- A weapon becomes broken (reduced base attack) once its durability value reaches 0 (except La Vie En Rose)"
                + "\r\n- SP effects that lowers resistance to an element will stack everytime the weapon is equipped (game bug)"
                + "\r\n- These effects goes back to normal when switching rooms"
                + "\r\n- If you want a weapon to have its proper attack pattern, set the refine for it",
                5, 5, 611, 82, this
            );
        }

        #endregion

        #region Values setting

        protected override void SetValues() {

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            // Sets values for each slot's dropdown
            for (int i = 0; i < dropDownLists.Count; i++) {

                // If the dropdown is disabled, skip it
                if (dropDownLists[i].Enabled == false) {
                    continue;
                }

                /**
                 * Indicate which sublist to use for setting the value, based on the slot's name
                 * Also indicate if we're setting the value for a weapon or an SP ability
                 */
                string[] fieldParts = dropDownLists[i].Name.Split(['_'], 4);
                if (fieldParts.Length >= 4 && fieldParts[3] != null && fieldParts[3].Substring(0, 10) == "sp_ability") {
                    // SP ability
                    string key = fieldParts[1] + "_" + fieldParts[2] + "_" + fieldParts[3];
                    KeyValuePair<string, Ability> selectedOption = (KeyValuePair<string, Ability>)dropDownLists[i].SelectedItem;
                    Ability selectedItem = selectedOption.Value;
                    SetMemoryValue(fieldParts[0], key, selectedItem.value);
                } else {
                    // Weapon
                    string key = fieldParts[1] + "_" + fieldParts[2];
                    KeyValuePair<string, Weapon> selectedOption = (KeyValuePair<string, Weapon>)dropDownLists[i].SelectedItem;
                    Weapon selectedItem = selectedOption.Value;
                    SetMemoryValue(fieldParts[0], key, selectedItem.value);
                }
            }

            // Repeat the above process for durabilities
            for (int i = 0; i < numericUpDowns.Count; i++) {

                if (numericUpDowns[i].Enabled == false) {
                    continue;
                }

                // Get the field's value & indicate which sublist to use for setting the value to the memory
                decimal value = numericUpDowns[i].Value;
                string[] fieldParts = numericUpDowns[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], value);
            }

            // For refine, update the values based on the currently checked RadioButton of each slot
            for (int i = 0; i < radioButtons.Count; i++) {

                if (radioButtons[i].Enabled == false || radioButtons[i].Checked == false || radioButtons[i].Tag == null) {
                    continue;
                }

                // Get the field's value & indicate which sublist to use for setting the value to the memory
                string[] fieldParts = radioButtons[i].Name.Split(['_'], 5);
                int value = (int)radioButtons[i].Tag;

                // Set the value to the memory address
                SetMemoryValue(fieldParts[0], fieldParts[1] + "_" + fieldParts[2] + "_" + fieldParts[3], value);

                // Get the selected weapon to retrieve the list of possible attack patterns
                string dropdownName = fieldParts[0] + "_" + fieldParts[1] + "_" + fieldParts[2];
                ImageComboBox dropdown = dropDownLists.Find(i => i.Name == dropdownName);
                if (dropdown == null) {
                    continue;
                }

                // Get the possible pattern from the selected weapon, if no weapon found or empty slot, skip
                KeyValuePair<string, Weapon> selectedOption = (KeyValuePair<string, Weapon>)dropdown.SelectedItem;
                ShinbokWeapon selectedWeapon = (ShinbokWeapon)selectedOption.Value;
                if (selectedWeapon == null || selectedWeapon.value == 255) {
                    continue;
                }

                // Get the pattern instance
                string patternKey = selectedWeapon.attackPatterns[value];
                ShinbokSwordAttackPattern attackPattern = _shinbokSwordAttackPatterns.All[patternKey];
                if (attackPattern == null) {
                    continue;
                }

                SetMemoryValue(fieldParts[0], fieldParts[1] + "_weapon_pattern", attackPattern.value);
            }

            /**
             * If BizHawk was not paused before setting values, unpause it
             * Otherwise keep it paused
             */
            if (_bokInterface._previousIsPauseSetting == true) {
                APIs.Client.Unpause();
            }
        }

        /// <summary>
        ///     Method for setting memory values.<br/>
        ///     This is separated because we use the switch inside on different types.
        /// </summary>
        /// <param name="subList"><c>Sublit / dictionnary the key belongs to</c></param>
        /// <param name="valueKey"><c>strng</c>Key within the dictionnary</param>
        /// <param name="value"><c>decimal</c>Value to set</param>
        private void SetMemoryValue(string subList, string valueKey, decimal value) {
            if (subList == "inventory" && _memoryValues.Inventory.ContainsKey(valueKey) == true) {
                _memoryValues.Inventory[valueKey].Value = (uint)value;
            }
        }

        protected override void SetDefaultValues() {

            foreach (ImageComboBox dropdown in dropDownLists) {

                // Indicate what the dropdown field is for
                string[] fieldParts = dropdown.Name.Split(['_'], 4);
                if (fieldParts.Length >= 4 && fieldParts[3] != null && fieldParts[3].Substring(0, 10) == "sp_ability") {
                    /**
                     * If it's for an SP ability
                     *
                     * Set the name of the key to retrieve the value from based on the dropdown's name (for example inventory_slotX_weapon => slotX_weapon)
                     * Then try getting the corresponding ability & preselect it
                     */
                    string key = fieldParts[1] + "_" + fieldParts[2] + "_" + fieldParts[3];
                    Ability? selectedAbility = GetAbilityByValue(_memoryValues.Inventory[key].Value);
                    if (selectedAbility != null) {
                        dropdown.SelectedIndex = dropdown.FindStringExact(selectedAbility.name);
                    }
                } else {
                    // If it's for the weapon itself, do the same as above & try preselecting the corresponding weapon
                    string key = fieldParts[1] + "_" + fieldParts[2];
                    Weapon? selectedWeapon = GetWeaponByValue(_memoryValues.Inventory[key].Value, _shinbokWeapons.All);
                    if (selectedWeapon != null) {
                        dropdown.SelectedIndex = dropdown.FindStringExact(selectedWeapon.name);
                    }
                }
            }

            // Get & set the durability
            foreach (NumericUpDown durabilityField in numericUpDowns) {
                string[] fieldParts = durabilityField.Name.Split(['_'], 2);
                durabilityField.Value = _memoryValues.Inventory[fieldParts[1]].Value;
            }

            // Check the corresponding refine radio button
            foreach (RadioButton refineButton in radioButtons) {
                string[] fieldParts = refineButton.Name.Split(['_'], 5);
                string key = fieldParts[1] + "_" + fieldParts[2] + "_" + fieldParts[3];

                /**
                 * Retrieve the current value for the slot the refine button is related to
                 * If it matches the Tag, check the button
                 */
                uint value = _memoryValues.Inventory[key].Value;
                if ((int)refineButton.Tag == value) {
                    refineButton.Checked = true;
                }
            }
        }

        /// <summary>Get an SP ability from the weapons abilities list by using its value</summary>
        /// <param name="value"><c>decimal</c>Value</param>
        /// <returns><c>Ability</c>Ability</returns>
        private Ability? GetAbilityByValue(decimal value) {
            foreach (KeyValuePair<string, Ability> index in _shinbokAbilities.Weapons) {
                Ability ability = index.Value;
                if (ability.value == value) {
                    return ability;
                }
            }

            return null;
        }

        #endregion
    }
}

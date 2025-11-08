using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

using BokInterface.Abilities;
using BokInterface.Addresses;
using BokInterface.Utils;

namespace BokInterface.Weapons {
    /// <summary>Weapons editor for Boktai 2</summary>
    class ZoktaiWeaponsEditor : WeaponsEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ZoktaiAddresses _zoktaiAddresses;
        private readonly ZoktaiWeapons _zoktaiWeapons;
        private readonly ZoktaiAbilities _zoktaiAbilities;

        #endregion

        #region Constructor

        public ZoktaiWeaponsEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            _zoktaiWeapons = new();
            _zoktaiAbilities = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(699, 326, name, text);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Add Panel, slot groups & options for dropdowns
            slotsPanel = WinFormHelpers.CreatePanel("slots_panel", 5, 79, 690, 243, this);
            GenerateGroups();
            GenerateDropDownOptions();

            // Add informative text
            AddInformativeText();

            // Set default values for each field
            SetDefaultValues();

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 620, 50, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        /// <summary>Separated method for generating groups with subelements</summary>
        protected void GenerateGroups() {
            int xPos = 0,
                yPos = 3;
            for (int i = 1; i < 17; i++) {

                // Generate group
                PropertyInfo property = GetType().GetProperty($"slot{i}group", BindingFlags.Instance | BindingFlags.NonPublic);
                if (property != null) {
                    CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"slot{i}group", $"Slot {i}", xPos, yPos, 160, 241, control: slotsPanel);
                    property.SetValue(this, group);

                    // Dropdown
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_weapon", 5, 19, 150, 23, group, visibleOptions: 5, enabled: false));

                    // Bonus / malus & durability
                    CheckGroupBox bonusGroup = WinFormHelpers.CreateCheckGroupBox($"slot{i}_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: group);
                    WinFormHelpers.CreateLabel($"slot{i}_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: bonusGroup);
                    numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown($"inventory_slot{i}_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: bonusGroup, enabled: false));
                    WinFormHelpers.CreateLabel($"slot{i}_durability_label", "Durability", 2, 50, 58, 15, control: bonusGroup);
                    numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown($"inventory_slot{i}_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: bonusGroup, enabled: false));

                    // SP abilities
                    CheckGroupBox spAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox($"slot{i}_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: group);
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_weapon_sp_ability_1", 5, 19, 140, 23, spAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_weapon_sp_ability_2", 5, 48, 140, 23, spAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
                    dropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_weapon_sp_ability_3", 5, 77, 140, 23, spAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
                }

                // Offsets for position
                xPos += 166;
                if ((i % 4) == 0) {
                    xPos = 0;
                    yPos += 248;
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
                    dropdown.DataSource = new BindingSource(_zoktaiAbilities.Weapons, null);
                    dropdown.DisplayMember = "Key";
                    dropdown.ValueMember = "Value";
                } else {
                    // If dropdown is for the weapon itself
                    dropdown.DataSource = new BindingSource(_zoktaiWeapons.All, null);
                    dropdown.DisplayMember = "Key";
                    dropdown.ValueMember = "Value";
                }
            }
        }

        /// <summary>Add informative text regarding weapons</summary>
        protected void AddInformativeText() {
            WinFormHelpers.CreateTextBox("weaponInfoText",
                "Regarding SP abilities and bonus or malus for a weapon :"
                + "\r\n- A weapon can have SP abilities and a bonus or malus"
                + "\r\n- Weapons with a bonus or malus have a durability value"
                + "\r\n- When the value reaches a certain threshold, the bonus decreases by 1",
                5, 5, 424, 68, this, readOnly: true
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

                    // In this case also set the "Forged by" name on the weapon
                    string slotNumber = fieldParts[1].Substring(4, fieldParts[1].Length == 6 ? 2 : 1);
                    SetWeaponForgedByName(Convert.ToInt32(slotNumber));
                }
            }

            // Repeat the above process for bonuses / maluses & durabilities (numericUpDowns)
            for (int i = 0; i < numericUpDowns.Count; i++) {

                if (numericUpDowns[i].Enabled == false) {
                    continue;
                }

                // Get the field's value & indicate which sublist to use for setting the value to the memory
                decimal value = numericUpDowns[i].Value;
                string[] fieldParts = numericUpDowns[i].Name.Split(['_'], 2);
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

        /// <summary>
        ///     Method for setting memory values.<br/>
        ///     This is separated because we use the switch inside on different types.
        /// </summary>
        /// <param name="subList"><c>Sublit / dictionnary the key belongs to</c></param>
        /// <param name="valueKey"><c>strng</c>Key withint the dictionnary</param>
        /// <param name="value"><c>decimal</c>Value to set</param>
        private void SetMemoryValue(string subList, string valueKey, decimal value) {
            if (subList == "inventory" && _memoryValues.Inventory.ContainsKey(valueKey) == true) {
                /**
                 * Split the key to check if it corresponds to a weapon bonus or malus
                 * If it does, the value needs to be converted
                 */
                string[] keyParts = valueKey.Split(['_'], 2);
                _memoryValues.Inventory[valueKey].Value = keyParts[1] == "weapon_bonus" ? Utilities.ConvertWeaponBonusToValue(value) : (uint)value;
            }
        }

        protected override void SetDefaultValues() {

            // If "current stat" is a valid value, get the current inventory
            uint currentStat = _zoktaiAddresses.Misc["current_stat"].Value;
            if (currentStat > 0) {
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
                        Weapon? selectedWeapon = GetWeaponByValue(_memoryValues.Inventory[key].Value, _zoktaiWeapons.All);
                        if (selectedWeapon != null) {
                            dropdown.SelectedIndex = dropdown.FindStringExact(selectedWeapon.name);
                        }
                    }
                }

                // Get the name of the field & retrieve the value for other fields
                foreach (NumericUpDown bonusRelatedField in numericUpDowns) {

                    // Retrieve some parts of the field's name to check if it correspond to the malus or bonus field
                    string[] fieldParts = bonusRelatedField.Name.Split(['_'], 3);
                    string memValuesKey = fieldParts[1] + "_" + fieldParts[2];

                    /**
                     * Set the value
                     *
                     * If the field corresponds to a weapon bonus or malus, we adjust the value
                     * This is because maluses are handled differently by the game
                     *
                     * For example : 255 = -01 & 246 = -10
                     */
                    bonusRelatedField.Value = fieldParts[2] == "weapon_bonus" ? Utilities.ConvertValueToWeaponBonus(_memoryValues.Inventory[memValuesKey].Value) : _memoryValues.Inventory[memValuesKey].Value;
                }
            } else {
                // If current stat is unvalid (for example because we are on the title screen or in a room transition), use specific values
                SelectFirstDropdownsIndex(dropDownLists);
                SetNumericUpDownsToMin(numericUpDowns);
            }
        }

        /// <summary>Get an SP ability from the weapons abilities list by using its value</summary>
        /// <param name="value"><c>decimal</c>Value</param>
        /// <returns><c>Ability</c>Ability</returns>
        private Ability? GetAbilityByValue(decimal value) {

            /**
             * For some reason there are duplicates within the game,
             * so in these cases we set it to the "original" instead of keeping the duplicate's value
             *
             * This is also to prevent having to show duplicate SP abilities in dropdown lists
             */
            switch (value) {
                case 6:             // Uses Solar Station energy for enchants
                case 7:
                    value = 5;
                    break;
                case 14:            // Damage bonus based on solar gauge
                    value = 1;
                    break;
                case 15:            // +10 damage at night
                    value = 2;
                    break;
                default:
                    break;
            }

            foreach (KeyValuePair<string, Ability> index in _zoktaiAbilities.Weapons) {
                Ability ability = index.Value;
                if (ability.value == value) {
                    return ability;
                }
            }

            return null;
        }

        /// <summary>Sets the "Forged by" name on the weapon in the specified slot to "TaiyohNetwrk"</summary>
        /// <param name="slot">Slot number</param>
        private void SetWeaponForgedByName(int slot) {
            if (slot > 0 && slot < 17) {
                _memoryValues.Inventory["slot" + slot + "_weapon_forgedBy_1"].Value = 2036949332;
                _memoryValues.Inventory["slot" + slot + "_weapon_forgedBy_2"].Value = 1699637359;
                _memoryValues.Inventory["slot" + slot + "_weapon_forgedBy_3"].Value = 1802663796;
            }
        }

        #endregion
    }
}

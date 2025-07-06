using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Abilities;
using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Weapons {
    /// <summary>Weapons editor for Boktai 3</summary>
    class ShinbokWeaponsEditor : WeaponsEditor {

        #region Instances

        protected readonly List<RadioButton> radioButtons = [];
        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _shinbokAddresses;
        private readonly ShinbokWeapons _shinbokWeapons;
        private readonly ShinbokAbilities _shinbokAbilities;
        private readonly ShinbokSwordAttackPatterns _shinbokSwordAttackPatterns;

        #endregion

        public ShinbokWeaponsEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses ShinbokAddresses) {

            _memoryValues = memoryValues;
            _shinbokAddresses = ShinbokAddresses;
            _shinbokWeapons = new();
            _shinbokAbilities = new();
            _shinbokSwordAttackPatterns = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(699, 312);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.weaponsEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() {

            // Add Panel along with each slot's main CheckGroupBox
            slotsPanel = WinFormHelpers.CreatePanel("slots_panel", 5, 94, 690, 214, this);
            InstanciateCheckGroupBoxes();

            // Add informative text
            AddInformativeText();

            // Slot 1
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_weapon", 5, 19, 150, 23, slot1group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot1PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot1_properties_group", "Properties", 5, 48, 150, 77, control: slot1group);
            WinFormHelpers.CreateLabel("slot1_refine_label", "Refine", 2, 19, 40, 15, slot1PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot1_weapon_refine_option_1", "I", 52, 19, 24, 19, slot1PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot1_weapon_refine_option_2", "II", 81, 19, 31, 19, slot1PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot1_weapon_refine_option_3", "III", 113, 19, 34, 19, slot1PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot1_durability_label", "Durability", 2, 50, 58, 15, control: slot1PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot1_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot1PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot1SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot1_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot1group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_weapon_sp_ability_1", 5, 19, 140, 23, slot1SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_weapon_sp_ability_2", 5, 48, 140, 23, slot1SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 2
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_weapon", 5, 19, 150, 23, slot2group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot2PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot2_properties_group", "Properties", 5, 48, 150, 77, control: slot2group);
            WinFormHelpers.CreateLabel("slot2_refine_label", "Refine", 2, 19, 40, 15, slot2PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot2_weapon_refine_option_1", "I", 52, 19, 24, 19, slot2PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot2_weapon_refine_option_2", "II", 81, 19, 31, 19, slot2PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot2_weapon_refine_option_3", "III", 113, 19, 34, 19, slot2PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot2_durability_label", "Durability", 2, 50, 58, 15, control: slot2PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot2_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot2PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot2SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot2_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot2group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_weapon_sp_ability_1", 5, 19, 140, 23, slot2SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_weapon_sp_ability_2", 5, 48, 140, 23, slot2SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 3
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_weapon", 5, 19, 150, 23, slot3group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot3PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot3_properties_group", "Properties", 5, 48, 150, 77, control: slot3group);
            WinFormHelpers.CreateLabel("slot3_refine_label", "Refine", 2, 19, 40, 15, slot3PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot3_weapon_refine_option_1", "I", 52, 19, 24, 19, slot3PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot3_weapon_refine_option_2", "II", 81, 19, 31, 19, slot3PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot3_weapon_refine_option_3", "III", 113, 19, 34, 19, slot3PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot3_durability_label", "Durability", 2, 50, 58, 15, control: slot3PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot3_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot3PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot3SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot3_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot3group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_weapon_sp_ability_1", 5, 19, 140, 23, slot3SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_weapon_sp_ability_2", 5, 48, 140, 23, slot3SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 4
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_weapon", 5, 19, 150, 23, slot4group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot4PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot4_properties_group", "Properties", 5, 48, 150, 77, control: slot4group);
            WinFormHelpers.CreateLabel("slot4_refine_label", "Refine", 2, 19, 40, 15, slot4PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot4_weapon_refine_option_1", "I", 52, 19, 24, 19, slot4PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot4_weapon_refine_option_2", "II", 81, 19, 31, 19, slot4PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot4_weapon_refine_option_3", "III", 113, 19, 34, 19, slot4PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot4_durability_label", "Durability", 2, 50, 58, 15, control: slot4PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot4_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot4PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot4SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot4_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot4group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_weapon_sp_ability_1", 5, 19, 140, 23, slot4SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_weapon_sp_ability_2", 5, 48, 140, 23, slot4SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 5
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_weapon", 5, 19, 150, 23, slot5group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot5PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot5_properties_group", "Properties", 5, 48, 150, 77, control: slot5group);
            WinFormHelpers.CreateLabel("slot5_refine_label", "Refine", 2, 19, 40, 15, slot5PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot5_weapon_refine_option_1", "I", 52, 19, 24, 19, slot5PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot5_weapon_refine_option_2", "II", 81, 19, 31, 19, slot5PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot5_weapon_refine_option_3", "III", 113, 19, 34, 19, slot5PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot5_durability_label", "Durability", 2, 50, 58, 15, control: slot5PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot5_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot5PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot5SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot5_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot5group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_weapon_sp_ability_1", 5, 19, 140, 23, slot5SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_weapon_sp_ability_2", 5, 48, 140, 23, slot5SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 6
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_weapon", 5, 19, 150, 23, slot6group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot6PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot6_properties_group", "Properties", 5, 48, 150, 77, control: slot6group);
            WinFormHelpers.CreateLabel("slot6_refine_label", "Refine", 2, 19, 40, 15, slot6PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot6_weapon_refine_option_1", "I", 52, 19, 24, 19, slot6PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot6_weapon_refine_option_2", "II", 81, 19, 31, 19, slot6PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot6_weapon_refine_option_3", "III", 113, 19, 34, 19, slot6PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot6_durability_label", "Durability", 2, 50, 58, 15, control: slot6PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot6_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot6PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot6SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot6_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot6group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_weapon_sp_ability_1", 5, 19, 140, 23, slot6SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_weapon_sp_ability_2", 5, 48, 140, 23, slot6SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 7
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_weapon", 5, 19, 150, 23, slot7group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot7PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot7_properties_group", "Properties", 5, 48, 150, 77, control: slot7group);
            WinFormHelpers.CreateLabel("slot7_refine_label", "Refine", 2, 19, 40, 15, slot7PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot7_weapon_refine_option_1", "I", 52, 19, 24, 19, slot7PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot7_weapon_refine_option_2", "II", 81, 19, 31, 19, slot7PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot7_weapon_refine_option_3", "III", 113, 19, 34, 19, slot7PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot7_durability_label", "Durability", 2, 50, 58, 15, control: slot7PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot7_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot7PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot7SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot7_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot7group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_weapon_sp_ability_1", 5, 19, 140, 23, slot7SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_weapon_sp_ability_2", 5, 48, 140, 23, slot7SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 8
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_weapon", 5, 19, 150, 23, slot8group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot8PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot8_properties_group", "Properties", 5, 48, 150, 77, control: slot8group);
            WinFormHelpers.CreateLabel("slot8_refine_label", "Refine", 2, 19, 40, 15, slot8PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot8_weapon_refine_option_1", "I", 52, 19, 24, 19, slot8PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot8_weapon_refine_option_2", "II", 81, 19, 31, 19, slot8PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot8_weapon_refine_option_3", "III", 113, 19, 34, 19, slot8PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot8_durability_label", "Durability", 2, 50, 58, 15, control: slot8PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot8_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot8PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot8SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot8_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot8group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_weapon_sp_ability_1", 5, 19, 140, 23, slot8SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_weapon_sp_ability_2", 5, 48, 140, 23, slot8SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 9
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_weapon", 5, 19, 150, 23, slot9group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot9PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot9_properties_group", "Properties", 5, 48, 150, 77, control: slot9group);
            WinFormHelpers.CreateLabel("slot9_refine_label", "Refine", 2, 19, 40, 15, slot9PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot9_weapon_refine_option_1", "I", 52, 19, 24, 19, slot9PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot9_weapon_refine_option_2", "II", 81, 19, 31, 19, slot9PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot9_weapon_refine_option_3", "III", 113, 19, 34, 19, slot9PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot9_durability_label", "Durability", 2, 50, 58, 15, control: slot9PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot9_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot9PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot9SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot9_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot9group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_weapon_sp_ability_1", 5, 19, 140, 23, slot9SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_weapon_sp_ability_2", 5, 48, 140, 23, slot9SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 10
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_weapon", 5, 19, 150, 23, slot10group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot10PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot10_properties_group", "Properties", 5, 48, 150, 77, control: slot10group);
            WinFormHelpers.CreateLabel("slot10_refine_label", "Refine", 2, 19, 40, 15, slot10PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot10_weapon_refine_option_1", "I", 52, 19, 24, 19, slot10PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot10_weapon_refine_option_2", "II", 81, 19, 31, 19, slot10PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot10_weapon_refine_option_3", "III", 113, 19, 34, 19, slot10PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot10_durability_label", "Durability", 2, 50, 58, 15, control: slot10PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot10_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot10PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot10SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot10_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot10group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_weapon_sp_ability_1", 5, 19, 140, 23, slot10SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_weapon_sp_ability_2", 5, 48, 140, 23, slot10SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 11
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_weapon", 5, 19, 150, 23, slot11group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot11PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot11_properties_group", "Properties", 5, 48, 150, 77, control: slot11group);
            WinFormHelpers.CreateLabel("slot11_refine_label", "Refine", 2, 19, 40, 15, slot11PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot11_weapon_refine_option_1", "I", 52, 19, 24, 19, slot11PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot11_weapon_refine_option_2", "II", 81, 19, 31, 19, slot11PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot11_weapon_refine_option_3", "III", 113, 19, 34, 19, slot11PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot11_durability_label", "Durability", 2, 50, 58, 15, control: slot11PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot11_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot11PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot11SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot11_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot11group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_weapon_sp_ability_1", 5, 19, 140, 23, slot11SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_weapon_sp_ability_2", 5, 48, 140, 23, slot11SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 12
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_weapon", 5, 19, 150, 23, slot12group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot12PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot12_properties_group", "Properties", 5, 48, 150, 77, control: slot12group);
            WinFormHelpers.CreateLabel("slot12_refine_label", "Refine", 2, 19, 40, 15, slot12PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot12_weapon_refine_option_1", "I", 52, 19, 24, 19, slot12PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot12_weapon_refine_option_2", "II", 81, 19, 31, 19, slot12PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot12_weapon_refine_option_3", "III", 113, 19, 34, 19, slot12PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot12_durability_label", "Durability", 2, 50, 58, 15, control: slot12PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot12_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot12PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot12SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot12_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot12group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_weapon_sp_ability_1", 5, 19, 140, 23, slot12SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_weapon_sp_ability_2", 5, 48, 140, 23, slot12SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 13
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot13_weapon", 5, 19, 150, 23, slot13group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot13PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot13_properties_group", "Properties", 5, 48, 150, 77, control: slot13group);
            WinFormHelpers.CreateLabel("slot13_refine_label", "Refine", 2, 19, 40, 15, slot13PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot13_weapon_refine_option_1", "I", 52, 19, 24, 19, slot13PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot13_weapon_refine_option_2", "II", 81, 19, 31, 19, slot13PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot13_weapon_refine_option_3", "III", 113, 19, 34, 19, slot13PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot13_durability_label", "Durability", 2, 50, 58, 15, control: slot13PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot13_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot13PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot13SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot13_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot13group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot13_weapon_sp_ability_1", 5, 19, 140, 23, slot13SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot13_weapon_sp_ability_2", 5, 48, 140, 23, slot13SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 14
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot14_weapon", 5, 19, 150, 23, slot14group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot14PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot14_properties_group", "Properties", 5, 48, 150, 77, control: slot14group);
            WinFormHelpers.CreateLabel("slot14_refine_label", "Refine", 2, 19, 40, 15, slot14PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot14_weapon_refine_option_1", "I", 52, 19, 24, 19, slot14PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot14_weapon_refine_option_2", "II", 81, 19, 31, 19, slot14PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot14_weapon_refine_option_3", "III", 113, 19, 34, 19, slot14PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot14_durability_label", "Durability", 2, 50, 58, 15, control: slot14PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot14_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot14PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot14SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot14_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot14group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot14_weapon_sp_ability_1", 5, 19, 140, 23, slot14SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot14_weapon_sp_ability_2", 5, 48, 140, 23, slot14SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 15
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot15_weapon", 5, 19, 150, 23, slot15group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot15PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot15_properties_group", "Properties", 5, 48, 150, 77, control: slot15group);
            WinFormHelpers.CreateLabel("slot15_refine_label", "Refine", 2, 19, 40, 15, slot15PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot15_weapon_refine_option_1", "I", 52, 19, 24, 19, slot15PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot15_weapon_refine_option_2", "II", 81, 19, 31, 19, slot15PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot15_weapon_refine_option_3", "III", 113, 19, 34, 19, slot15PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot15_durability_label", "Durability", 2, 50, 58, 15, control: slot15PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot15_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot15PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot15SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot15_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot15group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot15_weapon_sp_ability_1", 5, 19, 140, 23, slot15SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot15_weapon_sp_ability_2", 5, 48, 140, 23, slot15SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 16
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot16_weapon", 5, 19, 150, 23, slot16group, visibleOptions: 5, enabled: false));

            // Refine & durability
            CheckGroupBox slot16PropertiesGroup = WinFormHelpers.CreateCheckGroupBox("slot16_properties_group", "Properties", 5, 48, 150, 77, control: slot16group);
            WinFormHelpers.CreateLabel("slot16_refine_label", "Refine", 2, 19, 40, 15, slot16PropertiesGroup);
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot16_weapon_refine_option_1", "I", 52, 19, 24, 19, slot16PropertiesGroup, 0));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot16_weapon_refine_option_2", "II", 81, 19, 31, 19, slot16PropertiesGroup, 1));
            radioButtons.Add(WinFormHelpers.CreateRadioButton("inventory_slot16_weapon_refine_option_3", "III", 113, 19, 34, 19, slot16PropertiesGroup, 2));
            WinFormHelpers.CreateLabel("slot16_durability_label", "Durability", 2, 50, 58, 15, control: slot16PropertiesGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot16_weapon_durability", 0, 98, 48, 47, 23, maxValue: 1000, control: slot16PropertiesGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot16SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot16_sp_abilities_group", "SP abilities", 5, 129, 150, 78, control: slot16group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot16_weapon_sp_ability_1", 5, 19, 140, 23, slot16SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot16_weapon_sp_ability_2", 5, 48, 140, 23, slot16SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Generate & add options to weapon selection & SP abilities dropdowns
            GenerateDropDownOptions();

            // Set default values for each field
            SetDefaultValues();

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 620, 65, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        ///<summary>Separated method for instanciating checkGroupBox instances</summary>
        protected void InstanciateCheckGroupBoxes() {
            slot1group = WinFormHelpers.CreateCheckGroupBox("slot1group", "Slot 1", 0, 3, 160, 212, control: slotsPanel);
            slot2group = WinFormHelpers.CreateCheckGroupBox("slot2group", "Slot 2", 166, 3, 160, 212, control: slotsPanel);
            slot3group = WinFormHelpers.CreateCheckGroupBox("slot3group", "Slot 3", 332, 3, 160, 212, control: slotsPanel);
            slot4group = WinFormHelpers.CreateCheckGroupBox("slot4group", "Slot 4", 498, 3, 160, 212, control: slotsPanel);
            slot5group = WinFormHelpers.CreateCheckGroupBox("slot5group", "Slot 5", 0, 222, 160, 212, control: slotsPanel);
            slot6group = WinFormHelpers.CreateCheckGroupBox("slot6group", "Slot 6", 166, 222, 160, 212, control: slotsPanel);
            slot7group = WinFormHelpers.CreateCheckGroupBox("slot7group", "Slot 7", 332, 222, 160, 212, control: slotsPanel);
            slot8group = WinFormHelpers.CreateCheckGroupBox("slot8group", "Slot 8", 498, 222, 160, 212, control: slotsPanel);
            slot9group = WinFormHelpers.CreateCheckGroupBox("slot9group", "Slot 9", 0, 438, 160, 212, control: slotsPanel);
            slot10group = WinFormHelpers.CreateCheckGroupBox("slot10group", "Slot 10", 166, 438, 160, 212, control: slotsPanel);
            slot11group = WinFormHelpers.CreateCheckGroupBox("slot11group", "Slot 11", 332, 438, 160, 212, control: slotsPanel);
            slot12group = WinFormHelpers.CreateCheckGroupBox("slot12group", "Slot 12", 498, 438, 160, 212, control: slotsPanel);
            slot13group = WinFormHelpers.CreateCheckGroupBox("slot13group", "Slot 13", 0, 654, 160, 212, control: slotsPanel);
            slot14group = WinFormHelpers.CreateCheckGroupBox("slot14group", "Slot 14", 166, 654, 160, 212, control: slotsPanel);
            slot15group = WinFormHelpers.CreateCheckGroupBox("slot15group", "Slot 15", 332, 654, 160, 212, control: slotsPanel);
            slot16group = WinFormHelpers.CreateCheckGroupBox("slot16group", "Slot 16", 498, 654, 160, 212, control: slotsPanel);
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

        ///<summary>Generates the options for the weapon selection and SP abilities dropdowns</summary>
        private void GenerateDropDownOptions() {
            foreach (ImageComboBox dropdown in dropDownLists) {

                // Indicate what the dropdown field is for
                string[] fieldParts = dropdown.Name.Split(['_'], 4);
                if (fieldParts.Length >= 4 && fieldParts[3] != null && fieldParts[3].Substring(0, 10) == "sp_ability") {
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

        ///<summary>
        ///<para>Method for setting memory values</para>
        ///<para>This is separated because we use the switch inside on different types</para>
        ///</summary>
        ///<param name="subList"><c>Sublit / dictionnary the key belongs to</c></param>
        ///<param name="valueKey"><c>strng</c>Key within the dictionnary</param>
        ///<param name="value"><c>decimal</c>Value to set</param>
        private void SetMemoryValue(string subList, string valueKey, decimal value) {
            switch (subList) {
                case "inventory":
                    if (_memoryValues.Inventory.ContainsKey(valueKey) == true) {
                        _memoryValues.Inventory[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
                default:
                    if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
            }
        }

        protected override void SetDefaultValues() {

            /**
			 * If Django's current HP is a valid, try retrieving the current weapons inventory
			 * (Django's current HP goes below 0 or above 1000 when switching rooms, during bike races or on world map)
			 */
            uint djangoCurrentHp = _memoryValues.Django["current_hp"].Value;
            if (djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
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
                        Weapon? selectedWeapon = GetWeaponByValue(_memoryValues.Inventory[key].Value);
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
            } else {
                // If current stat is unvalid (for example because we are on the title screen or in a room transition), use specific values
                foreach (ImageComboBox dropdown in dropDownLists) {
                    dropdown.SelectedIndex = 0;
                }

                foreach (NumericUpDown propertiesRelatedFields in numericUpDowns) {
                    propertiesRelatedFields.Value = 0;
                }

                foreach (RadioButton refineButton in radioButtons) {
                    if ((int)refineButton.Tag == 0) {
                        refineButton.Checked = true;
                    }
                }
            }
        }

        ///<summary>Get a weapon from the full weapons list by using its value</summary>
        ///<param name="value"><c>decimal</c>Value</param>
        ///<returns><c>Weapon</c>Weapon</returns>
        private Weapon? GetWeaponByValue(decimal value) {
            foreach (KeyValuePair<string, Weapon> index in _shinbokWeapons.All) {
                Weapon weapon = index.Value;
                if (weapon.value == value) {
                    return weapon;
                }
            }

            return null;
        }

        ///<summary>Get an SP ability from the weapons abilities list by using its value</summary>
        ///<param name="value"><c>decimal</c>Value</param>
        ///<returns><c>Ability</c>Ability</returns>
        private Ability? GetAbilityByValue(decimal value) {
            foreach (KeyValuePair<string, Ability> index in _shinbokAbilities.Weapons) {
                Ability ability = index.Value;
                if (ability.value == value) {
                    return ability;
                }
            }

            return null;
        }
    }
}
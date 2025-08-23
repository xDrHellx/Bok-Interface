using System;
using System.Collections.Generic;
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

            SetFormParameters(699, 326);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Add Panel along with each slot's main CheckGroupBox
            slotsPanel = WinFormHelpers.CreatePanel("slots_panel", 5, 79, 690, 243, this);
            InstanciateCheckGroupBoxes();

            // Add informative text
            AddInformativeText();

            // Add 1st row slots
            // Slot 1
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_weapon", 5, 19, 150, 23, slot1group, visibleOptions: 5, enabled: false));

            // Bonus / malus & durability
            CheckGroupBox slot1BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot1_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot1group);
            WinFormHelpers.CreateLabel("slot1_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot1BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot1_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot1BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot1_durability_label", "Durability", 2, 50, 58, 15, control: slot1BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot1_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot1BonusGroup, enabled: false));

            // SP abilities
            CheckGroupBox slot1SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot1_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot1group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_weapon_sp_ability_1", 5, 19, 140, 23, slot1SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_weapon_sp_ability_2", 5, 48, 140, 23, slot1SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_weapon_sp_ability_3", 5, 77, 140, 23, slot1SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 2
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_weapon", 5, 19, 150, 23, slot2group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot2BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot2_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot2group);
            WinFormHelpers.CreateLabel("slot2_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot2BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot2_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot2BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot2_durability_label", "Durability", 2, 50, 58, 15, control: slot2BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot2_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot2BonusGroup, enabled: false));

            CheckGroupBox slot2SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot2_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot2group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_weapon_sp_ability_1", 5, 19, 140, 23, slot2SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_weapon_sp_ability_2", 5, 48, 140, 23, slot2SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_weapon_sp_ability_3", 5, 77, 140, 23, slot2SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 3
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_weapon", 5, 19, 150, 23, slot3group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot3BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot3_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot3group);
            WinFormHelpers.CreateLabel("slot3_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot3BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot3_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot3BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot3_durability_label", "Durability", 2, 50, 58, 15, control: slot3BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot3_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot3BonusGroup, enabled: false));

            CheckGroupBox slot3SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot3_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot3group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_weapon_sp_ability_1", 5, 19, 140, 23, slot3SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_weapon_sp_ability_2", 5, 48, 140, 23, slot3SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_weapon_sp_ability_3", 5, 77, 140, 23, slot3SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 4
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_weapon", 5, 19, 150, 23, slot4group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot4BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot4_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot4group);
            WinFormHelpers.CreateLabel("slot4_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot4BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot4_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot4BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot4_durability_label", "Durability", 2, 50, 58, 15, control: slot4BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot4_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot4BonusGroup, enabled: false));

            CheckGroupBox slot4SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot4_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot4group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_weapon_sp_ability_1", 5, 19, 140, 23, slot4SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_weapon_sp_ability_2", 5, 48, 140, 23, slot4SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_weapon_sp_ability_3", 5, 77, 140, 23, slot4SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // 2nd row
            // Slot 5
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_weapon", 5, 19, 150, 23, slot5group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot5BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot5_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot5group);
            WinFormHelpers.CreateLabel("slot5_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot5BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot5_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot5BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot5_durability_label", "Durability", 2, 50, 58, 15, control: slot5BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot5_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot5BonusGroup, enabled: false));

            CheckGroupBox slot5SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot5_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot5group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_weapon_sp_ability_1", 5, 19, 140, 23, slot5SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_weapon_sp_ability_2", 5, 48, 140, 23, slot5SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_weapon_sp_ability_3", 5, 77, 140, 23, slot5SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 6
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_weapon", 5, 19, 150, 23, slot6group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot6BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot6_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot6group);
            WinFormHelpers.CreateLabel("slot6_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot6BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot6_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot6BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot6_durability_label", "Durability", 2, 50, 58, 15, control: slot6BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot6_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot6BonusGroup, enabled: false));

            CheckGroupBox slot6SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot6_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot6group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_weapon_sp_ability_1", 5, 19, 140, 23, slot6SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_weapon_sp_ability_2", 5, 48, 140, 23, slot6SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_weapon_sp_ability_3", 5, 77, 140, 23, slot6SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 7
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_weapon", 5, 19, 150, 23, slot7group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot7BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot7_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot7group);
            WinFormHelpers.CreateLabel("slot7_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot7BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot7_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot7BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot7_durability_label", "Durability", 2, 50, 58, 15, control: slot7BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot7_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot7BonusGroup, enabled: false));

            CheckGroupBox slot7SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot7_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot7group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_weapon_sp_ability_1", 5, 19, 140, 23, slot7SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_weapon_sp_ability_2", 5, 48, 140, 23, slot7SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_weapon_sp_ability_3", 5, 77, 140, 23, slot7SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 8
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_weapon", 5, 19, 150, 23, slot8group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot8BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot8_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot8group);
            WinFormHelpers.CreateLabel("slot8_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot8BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot8_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot8BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot8_durability_label", "Durability", 2, 50, 58, 15, control: slot8BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot8_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot8BonusGroup, enabled: false));

            CheckGroupBox slot8SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot8_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot8group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_weapon_sp_ability_1", 5, 19, 140, 23, slot8SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_weapon_sp_ability_2", 5, 48, 140, 23, slot8SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_weapon_sp_ability_3", 5, 77, 140, 23, slot8SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // 3rd row
            // Slot 9
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_weapon", 5, 19, 150, 23, slot9group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot9BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot9_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot9group);
            WinFormHelpers.CreateLabel("slot9_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot9BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot9_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot9BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot9_durability_label", "Durability", 2, 50, 58, 15, control: slot9BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot9_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot9BonusGroup, enabled: false));

            CheckGroupBox slot9SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot9_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot9group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_weapon_sp_ability_1", 5, 19, 140, 23, slot9SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_weapon_sp_ability_2", 5, 48, 140, 23, slot9SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_weapon_sp_ability_3", 5, 77, 140, 23, slot9SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 10
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_weapon", 5, 19, 150, 23, slot10group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot10BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot10_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot10group);
            WinFormHelpers.CreateLabel("slot10_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot10BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot10_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot10BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot10_durability_label", "Durability", 2, 50, 58, 15, control: slot10BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot10_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot10BonusGroup, enabled: false));

            CheckGroupBox slot10SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot10_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot10group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_weapon_sp_ability_1", 5, 19, 140, 23, slot10SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_weapon_sp_ability_2", 5, 48, 140, 23, slot10SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_weapon_sp_ability_3", 5, 77, 140, 23, slot10SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 11
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_weapon", 5, 19, 150, 23, slot11group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot11BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot11_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot11group);
            WinFormHelpers.CreateLabel("slot11_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot11BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot11_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot11BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot11_durability_label", "Durability", 2, 50, 58, 15, control: slot11BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot11_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot11BonusGroup, enabled: false));

            CheckGroupBox slot11SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot11_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot11group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_weapon_sp_ability_1", 5, 19, 140, 23, slot11SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_weapon_sp_ability_2", 5, 48, 140, 23, slot11SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_weapon_sp_ability_3", 5, 77, 140, 23, slot11SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 12
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_weapon", 5, 19, 150, 23, slot12group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot12BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot12_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot12group);
            WinFormHelpers.CreateLabel("slot12_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot12BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot12_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot12BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot12_durability_label", "Durability", 2, 50, 58, 15, control: slot12BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot12_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot12BonusGroup, enabled: false));

            CheckGroupBox slot12SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot12_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot12group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_weapon_sp_ability_1", 5, 19, 140, 23, slot12SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_weapon_sp_ability_2", 5, 48, 140, 23, slot12SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_weapon_sp_ability_3", 5, 77, 140, 23, slot12SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // 4th row
            // Slot 13
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot13_weapon", 5, 19, 150, 23, slot13group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot13BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot13_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot13group);
            WinFormHelpers.CreateLabel("slot13_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot13BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot13_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot13BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot13_durability_label", "Durability", 2, 50, 58, 15, control: slot13BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot13_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot13BonusGroup, enabled: false));

            CheckGroupBox slot13SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot13_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot13group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot13_weapon_sp_ability_1", 5, 19, 140, 23, slot13SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot13_weapon_sp_ability_2", 5, 48, 140, 23, slot13SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot13_weapon_sp_ability_3", 5, 77, 140, 23, slot13SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 14
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot14_weapon", 5, 19, 150, 23, slot14group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot14BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot14_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot14group);
            WinFormHelpers.CreateLabel("slot14_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot14BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot14_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot14BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot14_durability_label", "Durability", 2, 50, 58, 15, control: slot14BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot14_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot14BonusGroup, enabled: false));

            CheckGroupBox slot14SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot14_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot14group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot14_weapon_sp_ability_1", 5, 19, 140, 23, slot14SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot14_weapon_sp_ability_2", 5, 48, 140, 23, slot14SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot14_weapon_sp_ability_3", 5, 77, 140, 23, slot14SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 15
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot15_weapon", 5, 19, 150, 23, slot15group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot15BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot15_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot15group);
            WinFormHelpers.CreateLabel("slot15_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot15BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot15_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot15BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot15_durability_label", "Durability", 2, 50, 58, 15, control: slot15BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot15_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot15BonusGroup, enabled: false));

            CheckGroupBox slot15SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot15_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot15group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot15_weapon_sp_ability_1", 5, 19, 140, 23, slot15SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot15_weapon_sp_ability_2", 5, 48, 140, 23, slot15SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot15_weapon_sp_ability_3", 5, 77, 140, 23, slot15SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Slot 16
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot16_weapon", 5, 19, 150, 23, slot16group, visibleOptions: 5, enabled: false));
            CheckGroupBox slot16BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot16_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot16group);
            WinFormHelpers.CreateLabel("slot16_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot16BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot16_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot16BonusGroup, enabled: false));
            WinFormHelpers.CreateLabel("slot16_durability_label", "Durability", 2, 50, 58, 15, control: slot16BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot16_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot16BonusGroup, enabled: false));

            CheckGroupBox slot16SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot16_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot16group);
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot16_weapon_sp_ability_1", 5, 19, 140, 23, slot16SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot16_weapon_sp_ability_2", 5, 48, 140, 23, slot16SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot16_weapon_sp_ability_3", 5, 77, 140, 23, slot16SpAbilitiesGroup, visibleOptions: 5, dropDownWidth: 350, enabled: false));

            // Generate & add options to weapon selection & SP abilities dropdowns
            GenerateDropDownOptions();

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

        ///<summary>Separated method for instanciating checkGroupBox instances</summary>
        protected void InstanciateCheckGroupBoxes() {
            slot1group = WinFormHelpers.CreateCheckGroupBox("slot1group", "Slot 1", 0, 3, 160, 241, control: slotsPanel);
            slot2group = WinFormHelpers.CreateCheckGroupBox("slot2group", "Slot 2", 166, 3, 160, 241, control: slotsPanel);
            slot3group = WinFormHelpers.CreateCheckGroupBox("slot3group", "Slot 3", 332, 3, 160, 241, control: slotsPanel);
            slot4group = WinFormHelpers.CreateCheckGroupBox("slot4group", "Slot 4", 498, 3, 160, 241, control: slotsPanel);
            slot5group = WinFormHelpers.CreateCheckGroupBox("slot5group", "Slot 5", 0, 251, 160, 241, control: slotsPanel);
            slot6group = WinFormHelpers.CreateCheckGroupBox("slot6group", "Slot 6", 166, 251, 160, 241, control: slotsPanel);
            slot7group = WinFormHelpers.CreateCheckGroupBox("slot7group", "Slot 7", 332, 251, 160, 241, control: slotsPanel);
            slot8group = WinFormHelpers.CreateCheckGroupBox("slot8group", "Slot 8", 498, 251, 160, 241, control: slotsPanel);
            slot9group = WinFormHelpers.CreateCheckGroupBox("slot9group", "Slot 9", 0, 499, 160, 241, control: slotsPanel);
            slot10group = WinFormHelpers.CreateCheckGroupBox("slot10group", "Slot 10", 166, 499, 160, 241, control: slotsPanel);
            slot11group = WinFormHelpers.CreateCheckGroupBox("slot11group", "Slot 11", 332, 499, 160, 241, control: slotsPanel);
            slot12group = WinFormHelpers.CreateCheckGroupBox("slot12group", "Slot 12", 498, 499, 160, 241, control: slotsPanel);
            slot13group = WinFormHelpers.CreateCheckGroupBox("slot13group", "Slot 13", 0, 747, 160, 241, control: slotsPanel);
            slot14group = WinFormHelpers.CreateCheckGroupBox("slot14group", "Slot 14", 166, 747, 160, 241, control: slotsPanel);
            slot15group = WinFormHelpers.CreateCheckGroupBox("slot15group", "Slot 15", 332, 747, 160, 241, control: slotsPanel);
            slot16group = WinFormHelpers.CreateCheckGroupBox("slot16group", "Slot 16", 498, 747, 160, 241, control: slotsPanel);
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

        ///<summary>Generates the options for the weapon selection & SP abilities dropdowns</summary>
        private void GenerateDropDownOptions() {
            foreach (ImageComboBox dropdown in dropDownLists) {

                // Indicate what the dropdown field is for
                string[] fieldParts = dropdown.Name.Split(['_'], 4);
                if (fieldParts.Length >= 4 && fieldParts[3] != null && fieldParts[3].Substring(0, 10) == "sp_ability") {
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

        ///<summary>
        ///<para>Method for setting memory values</para>
        ///<para>This is separated because we use the switch inside on different types</para>
        ///</summary>
        ///<param name="subList"><c>Sublit / dictionnary the key belongs to</c></param>
        ///<param name="valueKey"><c>strng</c>Key withint the dictionnary</param>
        ///<param name="value"><c>decimal</c>Value to set</param>
        private void SetMemoryValue(string subList, string valueKey, decimal value) {
            switch (subList) {
                case "inventory":

                    /**
                     * Split the key to check if it corresponds to a weapon bonus or malus
                     * If it does, the value needs to be converted
                     */
                    string[] keyParts = valueKey.Split(['_'], 2);
                    uint convertedValue = keyParts[1] == "weapon_bonus" ? Utilities.ConvertWeaponBonusToValue(value) : (uint)value;

                    if (_memoryValues.Inventory.ContainsKey(valueKey) == true) {
                        _memoryValues.Inventory[valueKey].Value = convertedValue;
                    } else if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = convertedValue;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = convertedValue;
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

            // If "current stat" is a valid value, get the current inventory
            uint currentStat = APIs.Memory.ReadU32(_zoktaiAddresses.Misc["current_stat"].Address);
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
                        Weapon? selectedWeapon = GetWeaponByValue(_memoryValues.Inventory[key].Value);
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
                foreach (ImageComboBox dropdown in dropDownLists) {
                    dropdown.SelectedIndex = 0;
                }

                foreach (NumericUpDown bonusRelatedField in numericUpDowns) {
                    bonusRelatedField.Value = 0;
                }
            }
        }

        ///<summary>Get a weapon from the full weapons list by using its value</summary>
        ///<param name="value"><c>decimal</c>Value</param>
        ///<returns><c>Weapon</c>Weapon</returns>
        private Weapon? GetWeaponByValue(decimal value) {
            foreach (KeyValuePair<string, Weapon> index in _zoktaiWeapons.All) {
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

        /// <summary>Sets the "Forged by" name on the weapon in the specified slot to "TayiohNetwrk"</summary>
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

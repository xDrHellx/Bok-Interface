using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Weapons {
    /// <summary>Weapons editor for Boktai 2</summary>
    class ZoktaiWeaponsEditor : WeaponsEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ZoktaiAddresses _zoktaiAddresses;
        private readonly ZoktaiWeapons _zoktaiWeapons;

        #endregion

        public ZoktaiWeaponsEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            _zoktaiWeapons = new(_memoryValues, _zoktaiAddresses);

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(699, 326);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.weaponsEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() {

            //TODO : Add SP abilities
            //TODO : Add retrieving current in-game values
            //TODO : Add setting values for bonus / malus, durability & SP abilities

            // Add Panel along with each slot's main CheckGroupBox
            slotsPanel = WinFormHelpers.CreatePanel("slots_panel", 5, 79, 690, 243, this);
            InstanciateCheckGroupBoxes();

            // Add informative text
            AddInformativeText();

            // Add 1st row slots
            // Slot 1
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_weapon", 5, 19, 150, 23, slot1group, visibleOptions: 5));

            CheckGroupBox slot1BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot1_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot1group);
            WinFormHelpers.CreateLabel("slot1_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot1BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot1_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot1BonusGroup));
            WinFormHelpers.CreateLabel("slot1_durability_label", "Durability", 2, 50, 58, 15, control: slot1BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot1_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot1BonusGroup));

            CheckGroupBox slot1SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot1_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot1group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot1_weapon_sp_ability_1", 5, 19, 140, 23, slot1SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot1_weapon_sp_ability_2", 5, 48, 140, 23, slot1SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot1_weapon_sp_ability_3", 5, 77, 140, 23, slot1SpAbilitiesGroup, visibleOptions: 5);

            // Slot 2
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_weapon", 5, 19, 150, 23, slot2group, visibleOptions: 5));

            CheckGroupBox slot2BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot2_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot2group);
            WinFormHelpers.CreateLabel("slot2_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot2BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot2_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot2BonusGroup));
            WinFormHelpers.CreateLabel("slot2_durability_label", "Durability", 2, 50, 58, 15, control: slot2BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot2_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot2BonusGroup));

            CheckGroupBox slot2SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot2_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot2group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot2_weapon_sp_ability_1", 5, 19, 140, 23, slot2SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot2_weapon_sp_ability_2", 5, 48, 140, 23, slot2SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot2_weapon_sp_ability_3", 5, 77, 140, 23, slot2SpAbilitiesGroup, visibleOptions: 5);

            // Slot 3
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_weapon", 5, 19, 150, 23, slot3group, visibleOptions: 5));

            CheckGroupBox slot3BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot3_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot3group);
            WinFormHelpers.CreateLabel("slot3_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot3BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot3_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot3BonusGroup));
            WinFormHelpers.CreateLabel("slot3_durability_label", "Durability", 2, 50, 58, 15, control: slot3BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot3_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot3BonusGroup));

            CheckGroupBox slot3SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot3_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot3group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot3_weapon_sp_ability_1", 5, 19, 140, 23, slot3SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot3_weapon_sp_ability_2", 5, 48, 140, 23, slot3SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot3_weapon_sp_ability_3", 5, 77, 140, 23, slot3SpAbilitiesGroup, visibleOptions: 5);

            // Slot 4
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_weapon", 5, 19, 150, 23, slot4group, visibleOptions: 5));

            CheckGroupBox slot4BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot4_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot4group);
            WinFormHelpers.CreateLabel("slot4_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot4BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot4_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot4BonusGroup));
            WinFormHelpers.CreateLabel("slot4_durability_label", "Durability", 2, 50, 58, 15, control: slot4BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot4_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot4BonusGroup));

            CheckGroupBox slot4SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot4_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot4group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot4_weapon_sp_ability_1", 5, 19, 140, 23, slot4SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot4_weapon_sp_ability_2", 5, 48, 140, 23, slot4SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot4_weapon_sp_ability_3", 5, 77, 140, 23, slot4SpAbilitiesGroup, visibleOptions: 5);

            // 2nd row
            // Slot 5
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_weapon", 5, 19, 150, 23, slot5group, visibleOptions: 5));

            CheckGroupBox slot5BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot5_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot5group);
            WinFormHelpers.CreateLabel("slot5_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot5BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot5_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot5BonusGroup));
            WinFormHelpers.CreateLabel("slot5_durability_label", "Durability", 2, 50, 58, 15, control: slot5BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot5_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot5BonusGroup));

            CheckGroupBox slot5SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot5_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot5group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot5_weapon_sp_ability_1", 5, 19, 140, 23, slot5SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot5_weapon_sp_ability_2", 5, 48, 140, 23, slot5SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot5_weapon_sp_ability_3", 5, 77, 140, 23, slot5SpAbilitiesGroup, visibleOptions: 5);

            // Slot 6
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_weapon", 5, 19, 150, 23, slot6group, visibleOptions: 5));

            CheckGroupBox slot6BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot6_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot6group);
            WinFormHelpers.CreateLabel("slot6_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot6BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot6_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot6BonusGroup));
            WinFormHelpers.CreateLabel("slot6_durability_label", "Durability", 2, 50, 58, 15, control: slot6BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot6_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot6BonusGroup));

            CheckGroupBox slot6SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot6_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot6group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot6_weapon_sp_ability_1", 5, 19, 140, 23, slot6SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot6_weapon_sp_ability_2", 5, 48, 140, 23, slot6SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot6_weapon_sp_ability_3", 5, 77, 140, 23, slot6SpAbilitiesGroup, visibleOptions: 5);

            // Slot 7
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_weapon", 5, 19, 150, 23, slot7group, visibleOptions: 5));

            CheckGroupBox slot7BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot7_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot7group);
            WinFormHelpers.CreateLabel("slot7_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot7BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot7_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot7BonusGroup));
            WinFormHelpers.CreateLabel("slot7_durability_label", "Durability", 2, 50, 58, 15, control: slot7BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot7_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot7BonusGroup));

            CheckGroupBox slot7SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot7_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot7group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot7_weapon_sp_ability_1", 5, 19, 140, 23, slot7SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot7_weapon_sp_ability_2", 5, 48, 140, 23, slot7SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot7_weapon_sp_ability_3", 5, 77, 140, 23, slot7SpAbilitiesGroup, visibleOptions: 5);

            // Slot 8
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_weapon", 5, 19, 150, 23, slot8group, visibleOptions: 5));

            CheckGroupBox slot8BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot8_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot8group);
            WinFormHelpers.CreateLabel("slot8_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot8BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot8_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot8BonusGroup));
            WinFormHelpers.CreateLabel("slot8_durability_label", "Durability", 2, 50, 58, 15, control: slot8BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot8_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot8BonusGroup));

            CheckGroupBox slot8SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot8_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot8group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot8_weapon_sp_ability_1", 5, 19, 140, 23, slot8SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot8_weapon_sp_ability_2", 5, 48, 140, 23, slot8SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot8_weapon_sp_ability_3", 5, 77, 140, 23, slot8SpAbilitiesGroup, visibleOptions: 5);

            // 3rd row
            // Slot 9
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_weapon", 5, 19, 150, 23, slot9group, visibleOptions: 5));

            CheckGroupBox slot9BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot9_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot9group);
            WinFormHelpers.CreateLabel("slot9_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot9BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot9_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot9BonusGroup));
            WinFormHelpers.CreateLabel("slot9_durability_label", "Durability", 2, 50, 58, 15, control: slot9BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot9_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot9BonusGroup));

            CheckGroupBox slot9SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot9_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot9group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot9_weapon_sp_ability_1", 5, 19, 140, 23, slot9SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot9_weapon_sp_ability_2", 5, 48, 140, 23, slot9SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot9_weapon_sp_ability_3", 5, 77, 140, 23, slot9SpAbilitiesGroup, visibleOptions: 5);

            // Slot 10
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_weapon", 5, 19, 150, 23, slot10group, visibleOptions: 5));

            CheckGroupBox slot10BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot10_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot10group);
            WinFormHelpers.CreateLabel("slot10_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot10BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot10_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot10BonusGroup));
            WinFormHelpers.CreateLabel("slot10_durability_label", "Durability", 2, 50, 58, 15, control: slot10BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot10_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot10BonusGroup));

            CheckGroupBox slot10SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot10_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot10group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot10_weapon_sp_ability_1", 5, 19, 140, 23, slot10SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot10_weapon_sp_ability_2", 5, 48, 140, 23, slot10SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot10_weapon_sp_ability_3", 5, 77, 140, 23, slot10SpAbilitiesGroup, visibleOptions: 5);

            // Slot 11
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_weapon", 5, 19, 150, 23, slot11group, visibleOptions: 5));

            CheckGroupBox slot11BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot11_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot11group);
            WinFormHelpers.CreateLabel("slot11_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot11BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot11_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot11BonusGroup));
            WinFormHelpers.CreateLabel("slot11_durability_label", "Durability", 2, 50, 58, 15, control: slot11BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot11_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot11BonusGroup));

            CheckGroupBox slot11SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot11_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot11group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot11_weapon_sp_ability_1", 5, 19, 140, 23, slot11SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot11_weapon_sp_ability_2", 5, 48, 140, 23, slot11SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot11_weapon_sp_ability_3", 5, 77, 140, 23, slot11SpAbilitiesGroup, visibleOptions: 5);

            // Slot 12
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_weapon", 5, 19, 150, 23, slot12group, visibleOptions: 5));

            CheckGroupBox slot12BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot12_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot12group);
            WinFormHelpers.CreateLabel("slot12_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot12BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot12_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot12BonusGroup));
            WinFormHelpers.CreateLabel("slot12_durability_label", "Durability", 2, 50, 58, 15, control: slot12BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot12_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot12BonusGroup));

            CheckGroupBox slot12SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot12_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot12group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot12_weapon_sp_ability_1", 5, 19, 140, 23, slot12SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot12_weapon_sp_ability_2", 5, 48, 140, 23, slot12SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot12_weapon_sp_ability_3", 5, 77, 140, 23, slot12SpAbilitiesGroup, visibleOptions: 5);

            // 4th row
            // Slot 13
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot13_weapon", 5, 19, 150, 23, slot13group, visibleOptions: 5));

            CheckGroupBox slot13BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot13_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot13group);
            WinFormHelpers.CreateLabel("slot13_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot13BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot13_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot13BonusGroup));
            WinFormHelpers.CreateLabel("slot13_durability_label", "Durability", 2, 50, 58, 15, control: slot13BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot13_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot13BonusGroup));

            CheckGroupBox slot13SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot13_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot13group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot13_weapon_sp_ability_1", 5, 19, 140, 23, slot13SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot13_weapon_sp_ability_2", 5, 48, 140, 23, slot13SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot13_weapon_sp_ability_3", 5, 77, 140, 23, slot13SpAbilitiesGroup, visibleOptions: 5);

            // Slot 14
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot14_weapon", 5, 19, 150, 23, slot14group, visibleOptions: 5));

            CheckGroupBox slot14BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot14_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot14group);
            WinFormHelpers.CreateLabel("slot14_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot14BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot14_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot14BonusGroup));
            WinFormHelpers.CreateLabel("slot14_durability_label", "Durability", 2, 50, 58, 15, control: slot14BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot14_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot14BonusGroup));

            CheckGroupBox slot14SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot14_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot14group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot14_weapon_sp_ability_1", 5, 19, 140, 23, slot14SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot14_weapon_sp_ability_2", 5, 48, 140, 23, slot14SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot14_weapon_sp_ability_3", 5, 77, 140, 23, slot14SpAbilitiesGroup, visibleOptions: 5);

            // Slot 15
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot15_weapon", 5, 19, 150, 23, slot15group, visibleOptions: 5));

            CheckGroupBox slot15BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot15_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot15group);
            WinFormHelpers.CreateLabel("slot15_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot15BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot15_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot15BonusGroup));
            WinFormHelpers.CreateLabel("slot15_durability_label", "Durability", 2, 50, 58, 15, control: slot15BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot15_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot15BonusGroup));

            CheckGroupBox slot15SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot15_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot15group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot15_weapon_sp_ability_1", 5, 19, 140, 23, slot15SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot15_weapon_sp_ability_2", 5, 48, 140, 23, slot15SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot15_weapon_sp_ability_3", 5, 77, 140, 23, slot15SpAbilitiesGroup, visibleOptions: 5);

            // Slot 16
            dropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot16_weapon", 5, 19, 150, 23, slot16group, visibleOptions: 5));

            CheckGroupBox slot16BonusGroup = WinFormHelpers.CreateCheckGroupBox("slot16_bonus_group", "Bonus | Malus", 5, 48, 150, 77, control: slot16group);
            WinFormHelpers.CreateLabel("slot16_bonus_label", "Bonus | malus", 2, 21, 81, 15, control: slot16BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot16_weapon_bonus", 0, 98, 19, 47, 23, -10, 10, control: slot16BonusGroup));
            WinFormHelpers.CreateLabel("slot16_durability_label", "Durability", 2, 50, 58, 15, control: slot16BonusGroup);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("inventory_slot16_weapon_durability", 0, 98, 48, 47, 23, maxValue: 200, control: slot16BonusGroup));

            CheckGroupBox slot16SpAbilitiesGroup = WinFormHelpers.CreateCheckGroupBox("slot16_sp_abilities_group", "SP abilities", 5, 129, 150, 107, control: slot16group);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot16_weapon_sp_ability_1", 5, 19, 140, 23, slot16SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot16_weapon_sp_ability_2", 5, 48, 140, 23, slot16SpAbilitiesGroup, visibleOptions: 5);
            // WinFormHelpers.CreateImageDropdownList("inventory_slot16_weapon_sp_ability_3", 5, 77, 140, 23, slot16SpAbilitiesGroup, visibleOptions: 5);

            /**
             * Generate & add options to weapon selection dropdowns
             * Then add the options for SP abilities
             */
            GenerateWeaponDropDownOptions();
            // GenerateSpAbilitiesDropDownOptions();

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
            TextBox textBox = WinFormHelpers.CreateTextBox("weapon_info_text",
                "Regarding SP abilities and bonus or malus for a weapon :"
                + "\r\n- A weapon cannot have both SP abilities and bonus or malus"
                + "\r\n- Weapons with a bonus or malus have a durability value"
                + "\r\n- When the value reaches a certain threshold, the bonus or malus is removed",
                5, 5, 424, 68, this
            );

            textBox.ReadOnly = true;
        }

        protected override void SetValues() {

            // Retrieve all input fields
            List<ImageComboBox> slots = dropDownLists;
            List<NumericUpDown> bonusRelatedFields = numericUpDowns;

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            // Sets values for each slot
            for (int i = 0; i < slots.Count; i++) {

                // If the slot is disabled, skip it
                if (slots[i].Enabled == false) {
                    continue;
                }

                KeyValuePair<string, Weapon> selectedOption = (KeyValuePair<string, Weapon>)slots[i].SelectedItem;
                Weapon selectedItem = selectedOption.Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the slot's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = slots[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], selectedItem.value);
            }

            // Repeat the above process for bonuses / maluses & durabilities
            for (int i = 0; i < bonusRelatedFields.Count; i++) {

                if (bonusRelatedFields[i].Enabled == false) {
                    continue;
                }

                // Get the field's value & indicate which sublist to use for setting the value to the memory
                decimal value = bonusRelatedFields[i].Value;
                string[] fieldParts = bonusRelatedFields[i].Name.Split(['_'], 2);
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
            string memoryValueKey = valueKey;
            switch (subList) {
                case "django":
                    if (_memoryValues.Django.ContainsKey(memoryValueKey) == true) {

                        // Depending on the key, we treat the value setting differently
                        switch (memoryValueKey) {
                            case "vit":                     // Stats
                            case "spr":
                            case "str":
                            case "agi":
                                /**
                                 * For stats we also update the "persistent" stat address
                                 * 
                                 * We do this because updating "current" stat value is not enough,
                                 * when switching room the game would set back the old values
                                 */
                                _memoryValues.Django[memoryValueKey].Value = (uint)value;
                                if (_memoryValues.Misc.ContainsKey(memoryValueKey) == true) {
                                    _memoryValues.Misc[memoryValueKey].Value = (uint)value;
                                }
                                break;
                            case "sword_skill":             // Skill
                            case "spear_skill":
                            case "hammer_skill":
                            case "fists_skill":
                            case "gun_skill":
                                _memoryValues.Django[memoryValueKey].Value = Utilities.LevelToExp(value);
                                break;
                            default:                        // Default treatment
                                _memoryValues.Django[memoryValueKey].Value = (uint)value;
                                break;
                        }

                    } else if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U16[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U32[memoryValueKey].Value = (uint)value;
                    }
                    break;
                case "solls":
                    if (_memoryValues.Solls.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.Solls[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U16[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U32[memoryValueKey].Value = (uint)value;
                    }
                    break;
                case "inventory":

                    /**
                     * Split the key to check if it corresponds to a weapon bonus or malus
                     * If it does, the value needs to be converted
                     */
                    string[] keyParts = memoryValueKey.Split(['_'], 2);
                    uint convertedValue = keyParts[1] == "weapon_bonus" ? Utilities.ConvertWeaponBonusToValue(value) : (uint)value;

                    if (_memoryValues.Inventory.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.Inventory[memoryValueKey].Value = convertedValue;
                    } else if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U16[memoryValueKey].Value = convertedValue;
                    } else if (_memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U32[memoryValueKey].Value = convertedValue;
                    }
                    break;
                case "misc":
                    if (_memoryValues.Misc.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.Misc[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U16[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U32[memoryValueKey].Value = (uint)value;
                    }
                    break;
                default:
                    if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U16[memoryValueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.U32[memoryValueKey].Value = (uint)value;
                    }
                    break;
            }
        }

        ///<summary>Generates the options for the weapon selection dropdowns</summary>
        private void GenerateWeaponDropDownOptions() {
            foreach (ImageComboBox dropdown in dropDownLists) {
                dropdown.DataSource = new BindingSource(_zoktaiWeapons.All, null);
                dropdown.DisplayMember = "Key";
                dropdown.ValueMember = "Value";
            }
        }

        protected override void SetDefaultValues() {

            // If "current stat" is a valid value, get the current inventory
            uint currentStat = APIs.Memory.ReadU32(_zoktaiAddresses.Misc["current_stat"].Address);
            if (currentStat > 0) {
                foreach (ImageComboBox dropdown in dropDownLists) {
                    /**
                     * Get the name of the field to retrieve the value from based on the dropdown's name (for example inventory_slotX_weapon => slotX_weapon)
                     * Then try getting the corresponding weapon & preselect it
                     */
                    string[] fieldParts = dropdown.Name.Split(['_'], 2);
                    Weapon? selectedWeapon = GetWeaponByValue(_memoryValues.Inventory[fieldParts[1]].Value);
                    if (selectedWeapon != null) {
                        dropdown.SelectedIndex = dropdown.FindStringExact(selectedWeapon.name);
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
    }
}
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Inventory {
    /// <summary>Inventory editor for Boktai 2</summary>
    class ZoktaiInventoryEditor : InventoryEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ZoktaiAddresses _zoktaiAddresses;

        #endregion

        public ZoktaiInventoryEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(587, 346);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.inventoryEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() {

            // Instanciate checkGroupBoxes
            InstanciateCheckGroupBoxes();

            // 1st row
            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot1_item", 5, 19, 130, 23, slot1group));
            WinFormHelpers.CreateLabel("slot1", "Durability", 5, 50, 58, 15, slot1group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot1_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot1group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot2_item", 5, 19, 130, 23, slot2group));
            WinFormHelpers.CreateLabel("slot2", "Durability", 5, 50, 58, 15, slot2group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot2_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot2group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot3_item", 5, 19, 130, 23, slot3group));
            WinFormHelpers.CreateLabel("slot3", "Durability", 5, 50, 58, 15, slot3group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot3_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot3group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot4_item", 5, 19, 130, 23, slot4group));
            WinFormHelpers.CreateLabel("slot4", "Durability", 5, 50, 58, 15, slot4group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot4_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot4group));

            // 2nd row
            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot5_item", 5, 19, 130, 23, slot5group));
            WinFormHelpers.CreateLabel("slot5", "Durability", 5, 50, 58, 15, slot5group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot5_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot5group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot6_item", 5, 19, 130, 23, slot6group));
            WinFormHelpers.CreateLabel("slot6", "Durability", 5, 50, 58, 15, slot6group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot6_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot6group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot7_item", 5, 19, 130, 23, slot7group));
            WinFormHelpers.CreateLabel("slot7", "Durability", 5, 50, 58, 15, slot7group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot7_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot7group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot8_item", 5, 19, 130, 23, slot8group));
            WinFormHelpers.CreateLabel("slot8", "Durability", 5, 50, 58, 15, slot8group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot8_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot8group));

            // 3rd row
            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot9_item", 5, 19, 130, 23, slot9group));
            WinFormHelpers.CreateLabel("slot9", "Durability", 5, 50, 58, 15, slot9group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot9_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot9group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot10_item", 5, 19, 130, 23, slot10group));
            WinFormHelpers.CreateLabel("slot10", "Durability", 5, 50, 58, 15, slot10group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot10_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot10group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot11_item", 5, 19, 130, 23, slot11group));
            WinFormHelpers.CreateLabel("slot11", "Durability", 5, 50, 58, 15, slot11group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot11_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot11group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot12_item", 5, 19, 130, 23, slot12group));
            WinFormHelpers.CreateLabel("slot12", "Durability", 5, 50, 58, 15, slot12group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot12_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot12group));

            // 4th row
            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot13_item", 5, 19, 130, 23, slot13group));
            WinFormHelpers.CreateLabel("slot13", "Durability", 5, 50, 58, 15, slot13group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot13_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot13group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot14_item", 5, 19, 130, 23, slot14group));
            WinFormHelpers.CreateLabel("slot14", "Durability", 5, 50, 58, 15, slot14group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot14_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot14group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot15_item", 5, 19, 130, 23, slot15group));
            WinFormHelpers.CreateLabel("slot15", "Durability", 5, 50, 58, 15, slot15group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot15_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot15group));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot16_item", 5, 19, 130, 23, slot16group));
            WinFormHelpers.CreateLabel("slot16", "Durability", 5, 50, 58, 15, slot16group);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot16_durability", 0, 85, 47, 50, 23, 0, 3839, control: slot16group));

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 509, 320, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        ///<summary>Separated method for instanciating checkGroupBox instances</summary>
        protected void InstanciateCheckGroupBoxes() {
            slot1group = WinFormHelpers.CreateCheckGroupBox("slot1group", "Slot 1", 5, 5, 140, 76, true, this);
            slot2group = WinFormHelpers.CreateCheckGroupBox("slot2group", "Slot 2", 151, 5, 140, 76, true, this);
            slot3group = WinFormHelpers.CreateCheckGroupBox("slot3group", "Slot 3", 297, 5, 140, 76, true, this);
            slot4group = WinFormHelpers.CreateCheckGroupBox("slot4group", "Slot 4", 443, 5, 140, 76, true, this);
            slot5group = WinFormHelpers.CreateCheckGroupBox("slot5group", "Slot 5", 5, 84, 140, 76, true, this);
            slot6group = WinFormHelpers.CreateCheckGroupBox("slot6group", "Slot 6", 151, 84, 140, 76, true, this);
            slot7group = WinFormHelpers.CreateCheckGroupBox("slot7group", "Slot 7", 297, 84, 140, 76, true, this);
            slot8group = WinFormHelpers.CreateCheckGroupBox("slot8group", "Slot 8", 443, 84, 140, 76, true, this);
            slot9group = WinFormHelpers.CreateCheckGroupBox("slot9group", "Slot 9", 5, 163, 140, 76, true, this);
            slot10group = WinFormHelpers.CreateCheckGroupBox("slot10group", "Slot 10", 151, 163, 140, 76, true, this);
            slot11group = WinFormHelpers.CreateCheckGroupBox("slot11group", "Slot 11", 297, 163, 140, 76, true, this);
            slot12group = WinFormHelpers.CreateCheckGroupBox("slot12group", "Slot 12", 443, 163, 140, 76, true, this);
            slot13group = WinFormHelpers.CreateCheckGroupBox("slot13group", "Slot 13", 5, 242, 140, 76, true, this);
            slot14group = WinFormHelpers.CreateCheckGroupBox("slot14group", "Slot 14", 151, 242, 140, 76, true, this);
            slot15group = WinFormHelpers.CreateCheckGroupBox("slot15group", "Slot 15", 297, 242, 140, 76, true, this);
            slot16group = WinFormHelpers.CreateCheckGroupBox("slot16group", "Slot 16", 443, 242, 140, 76, true, this);
        }


        protected override void SetValues() {

            // Retrieve all input fields
            List<ComboBox> slots = dropDownLists;
            List<NumericUpDown> durabilities = numericUpDowns;

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            // // Sets values for each slot
            // for (int i = 0; i < slots.Count; i++) {

            //     // If the slot is disabled, skip it
            //     if (slots[i].Enabled == false) {
            //         continue;
            //     }

            //     decimal value = slots[i].SelectedItem;

            //     /**
            //      * Indicate which sublist to use for setting the value, based on the slot's name
            //      * We only split on the first "_"
            //      */
            //     string[] fieldParts = slots[i].Name.Split(['_'], 2);
            //     SetMemoryValue(fieldParts[0], fieldParts[1], value);
            // }

            // Repeat the above process for Durabilitys
            for (int i = 0; i < durabilities.Count; i++) {

                if (durabilities[i].Enabled == false) {
                    continue;
                }

                decimal value = durabilities[i].Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the slot's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = durabilities[i].Name.Split(['_'], 2);
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
                                 * For stats wz also update the "persistent" stat address
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
    }
}
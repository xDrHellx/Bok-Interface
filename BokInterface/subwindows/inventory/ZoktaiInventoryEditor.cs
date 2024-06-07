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

            SetFormParameters(559, 279);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.inventoryEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() {

            inventoryGroupbox = WinFormHelpers.CreateGroupBox("inventoryGroup", "Items", 5, 5, 549, 244, this);

            // 1st row
            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot1_item", 5, 16, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot1", "Durability", 5, 47, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot1_durability", 0, 85, 44, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot2_item", 141, 16, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot2", "Durability", 141, 47, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot2_durability", 0, 221, 44, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot3_item", 277, 16, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot3", "Durability", 277, 47, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot3_durability", 0, 357, 44, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot4_item", 413, 16, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot4", "Durability", 413, 47, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot4_durability", 0, 493, 44, 50, 23, 0, 3839, control: inventoryGroupbox));

            // 2nd row
            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot5_item", 5, 73, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot5", "Durability", 5, 104, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot5_durability", 0, 85, 101, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot6_item", 141, 73, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot6", "Durability", 141, 104, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot6_durability", 0, 221, 101, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot7_item", 277, 73, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot7", "Durability", 277, 104, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot7_durability", 0, 357, 101, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot8_item", 413, 73, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot8", "Durability", 413, 104, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot8_durability", 0, 493, 101, 50, 23, 0, 3839, control: inventoryGroupbox));

            // 3rd row
            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot9_item", 5, 130, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot9", "Durability", 5, 161, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot9_durability", 0, 85, 158, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot10_item", 141, 130, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot10", "Durability", 141, 161, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot10_durability", 0, 221, 158, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot11_item", 277, 130, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot11", "Durability", 277, 161, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot11_durability", 0, 357, 158, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot12_item", 413, 130, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot12", "Durability", 413, 161, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot12_durability", 0, 493, 158, 50, 23, 0, 3839, control: inventoryGroupbox));

            // 4th row
            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot13_item", 5, 187, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot13", "Durability", 5, 218, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot13_durability", 0, 85, 215, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot14_item", 141, 187, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot14", "Durability", 141, 218, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot14_durability", 0, 221, 215, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot15_item", 277, 187, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot15", "Durability", 277, 218, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot15_durability", 0, 357, 215, 50, 23, 0, 3839, control: inventoryGroupbox));

            dropDownLists.Add(WinFormHelpers.CreateDowndownList("slot16_item", 413, 187, 130, 23, inventoryGroupbox));
            WinFormHelpers.CreateLabel("slot16", "Durability", 413, 218, 58, 15, inventoryGroupbox);
            numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("slot16_durability", 0, 493, 215, 50, 23, 0, 3839, control: inventoryGroupbox));

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 479, 252, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        protected override void SetValues() {

            // Retrieve all input fields
            List<ComboBox> slots = dropDownLists;
            List<NumericUpDown> durabilities = numericUpDowns;

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            /**
             * If the total EXP until next level & current level are available,
             * we'll use these to prevent the game from adjusting the level while setting new values
             * 
             * We'll set the total EXP until next level to the maximum possible to prevent that from happening
             */
            if (_memoryValues.U32.ContainsKey("total_exp_until_next_level") == true) {
                _memoryValues.U32["total_exp_until_next_level"].Value = 99999999;
            }

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
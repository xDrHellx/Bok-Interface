using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;
using BokInterface.ExpTables;

namespace BokInterface.Status {
    /// <summary>Status editor for Boktai 2</summary>
    class ZoktaiStatusEditor : StatusEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ZoktaiAddresses _zoktaiAddresses;

        #endregion

        #region Form elements

        protected readonly List<CheckBox> statusCheckBoxes = [];
        protected CheckGroupBox kaamosGroupBox = new();

        #endregion

        public ZoktaiStatusEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(351, 313);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.statusEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() {

            // Get default values, depending on availability, these can be the current in-game values
            IDictionary<string, decimal> defaultValues = GetDefaultValues();

            // Sections
            statusGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatusGroup", "Status", 5, 5, 97, 74, control: this);
            skillGroupBox = WinFormHelpers.CreateCheckGroupBox("editSkillGroup", "Skill", 110, 85, 221, 103, control: this);
            statsGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatsGroup", "Stats", 5, 85, 99, 161, control: this);
            expGroupBox = WinFormHelpers.CreateCheckGroupBox("editExpGroup", "Leven and EXP", 108, 5, 117, 74, control: this);
            kaamosGroupBox = WinFormHelpers.CreateCheckGroupBox("editKaamosGroup", "Kaamos", 233, 5, 83, 75, control: this);

            // Status
            WinFormHelpers.CreateLabel("djangoEditHpLabel", "LIFE :", 2, 19, 34, 15, statusGroupBox);
            WinFormHelpers.CreateLabel("djangoEditEneLabel", "ENE :", 2, 47, 34, 15, statusGroupBox);

            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_hp", defaultValues["django_current_hp"], 42, 17, 50, 23, maxValue: 1000, control: statusGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_ene", defaultValues["django_current_ene"], 42, 45, 50, 23, maxValue: 1000, control: statusGroupBox));

            // Skill
            WinFormHelpers.CreateLabel("djangoEditSwordSkillLabel", "Sword", 2, 19, 54, 15, skillGroupBox, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("djangoEditSpearSkillLabel", "Spear", 123, 19, 36, 15, skillGroupBox, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("djangoEditHammerSkillLabel", "Hammer", 2, 47, 54, 15, skillGroupBox, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("djangoEditFistsSkillLabel", "Fists", 123, 47, 36, 15, skillGroupBox, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("djangoEditGunSkillLabel", "Gun", 2, 76, 54, 15, skillGroupBox, textAlignment: "MiddleLeft");

            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_sword_skill", defaultValues["django_sword_skill"], 58, 16, 51, 23, nbDecimals: 2, control: skillGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_spear_skill", defaultValues["django_spear_skill"], 165, 16, 51, 23, nbDecimals: 2, control: skillGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_hammer_skill", defaultValues["django_hammer_skill"], 58, 45, 51, 23, nbDecimals: 2, control: skillGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_fists_skill", defaultValues["django_fists_skill"], 165, 45, 51, 23, nbDecimals: 2, control: skillGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_gun_skill", defaultValues["django_gun_skill"], 58, 74, 51, 23, nbDecimals: 2, control: skillGroupBox));

            // Stats
            WinFormHelpers.CreateLabel("djangoEditVitLabel", "VIT", 2, 19, 27, 15, statsGroupBox);
            WinFormHelpers.CreateLabel("djangoEditSprLabel", "SPR", 2, 47, 27, 15, statsGroupBox);
            WinFormHelpers.CreateLabel("djangoEditStrLabel", "STR", 2, 76, 27, 15, statsGroupBox);
            WinFormHelpers.CreateLabel("djangoEditAgiLabel", "AGI", 2, 105, 27, 15, statsGroupBox);
            WinFormHelpers.CreateLabel("djangoEditStatPointsLabel", "Points", 2, 134, 40, 15, statsGroupBox);

            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_vit", defaultValues["django_vit"], 53, 16, 41, 23, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_spr", defaultValues["django_spr"], 53, 45, 41, 23, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_str", defaultValues["django_str"], 53, 74, 41, 23, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_agi", defaultValues["django_agi"], 53, 103, 41, 23, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_stat_points", defaultValues["django_stat_points"], 48, 132, 46, 23, minValue: 0, maxValue: 255, control: statsGroupBox));

            // Level & EXP
            WinFormHelpers.CreateLabel("djangoEditLevelLabel", "Level", 2, 19, 34, 15, expGroupBox);
            WinFormHelpers.CreateLabel("djangoEditExpLabel", "EXP", 2, 47, 27, 15, expGroupBox);

            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_level", defaultValues["django_level"], 42, 16, 50, 23, control: expGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_exp", defaultValues["django_exp"], 32, 45, 60, 23, minValue: 0, maxValue: 999999, control: expGroupBox));

            // Kaamos
            statusCheckBoxes.Add(WinFormHelpers.CreateCheckBox("django_kaamos", "Django", 10, 22, 64, 19, isCheckedByDefault: defaultValues["django_kaamos"] > 0, control: kaamosGroupBox));
            statusCheckBoxes.Add(WinFormHelpers.CreateCheckBox("sabata_kaamos", "Sabata", 10, 44, 64, 19, isCheckedByDefault: defaultValues["sabata_kaamos"] > 0, control: kaamosGroupBox));

            // Add tooltips & warnings
            Label expWarning = WinFormHelpers.CreateImageLabel("tooltip", "warning", 97, 48, expGroupBox);
            WinFormHelpers.AddToolTip(expWarning, "Level will be automatically adjusted if EXP is high enough to reach higher levels");

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 257, 248, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        protected override IDictionary<string, decimal> GetDefaultValues() {

            IDictionary<string, decimal> defaultValues = new Dictionary<string, decimal>();
            uint currentStat = APIs.Memory.ReadU32(_zoktaiAddresses.Misc["current_stat"].Address);

            // If "current stat" is a valid value
            if (currentStat > 0) {
                defaultValues.Add("django_current_hp", _memoryValues.Django["current_hp"].Value);
                defaultValues.Add("django_current_ene", _memoryValues.Django["current_ene"].Value);

                defaultValues.Add("django_exp", _memoryValues.Django["exp"].Value);
                defaultValues.Add("django_level", _memoryValues.Django["level"].Value);

                defaultValues.Add("django_vit", _memoryValues.Django["vit"].Value);
                defaultValues.Add("django_spr", _memoryValues.Django["spr"].Value);
                defaultValues.Add("django_str", _memoryValues.Django["str"].Value);
                defaultValues.Add("django_agi", _memoryValues.Django["agi"].Value);
                defaultValues.Add("django_stat_points", _memoryValues.Django["stat_points"].Value);

                defaultValues.Add("django_sword_skill", Utilities.ExpToLevel(_memoryValues.Django["sword_skill"].Value));
                defaultValues.Add("django_spear_skill", Utilities.ExpToLevel(_memoryValues.Django["spear_skill"].Value));
                defaultValues.Add("django_hammer_skill", Utilities.ExpToLevel(_memoryValues.Django["hammer_skill"].Value));
                defaultValues.Add("django_fists_skill", Utilities.ExpToLevel(_memoryValues.Django["fists_skill"].Value));
                defaultValues.Add("django_gun_skill", Utilities.ExpToLevel(_memoryValues.Django["gun_skill"].Value));

                defaultValues.Add("django_kaamos", _memoryValues.Django["kaamos"].Value);
                defaultValues.Add("sabata_kaamos", _memoryValues.Sabata["kaamos"].Value);
            } else {
                // If current stat is unvalid (for example because we are on the title screen or in a room transition), use specific values
                defaultValues.Add("django_current_hp", 100);
                defaultValues.Add("django_current_ene", 100);

                defaultValues.Add("django_exp", 0);
                defaultValues.Add("django_level", 1);

                defaultValues.Add("django_vit", 10);
                defaultValues.Add("django_spr", 10);
                defaultValues.Add("django_str", 10);
                defaultValues.Add("django_agi", 10);

                defaultValues.Add("django_stat_points", 0);

                defaultValues.Add("django_sword_skill", 1);
                defaultValues.Add("django_spear_skill", 1);
                defaultValues.Add("django_hammer_skill", 1);
                defaultValues.Add("django_fists_skill", 1);
                defaultValues.Add("django_gun_skill", 80);

                defaultValues.Add("django_kaamos", 0);
                defaultValues.Add("sabata_kaamos", 0);
            }

            return defaultValues;
        }

        protected override void SetValues() {

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

            // Sets values for each field
            for (int i = 0; i < statusNumericUpDowns.Count; i++) {

                // If the slot is disabled, skip it
                if (statusNumericUpDowns[i].Enabled == false) {
                    continue;
                }

                /**
                 * Indicate which sublist to use for setting the value, based on the slot's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = statusNumericUpDowns[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], statusNumericUpDowns[i].Value);
            }

            // Repeat the process above for each checkbox
            for (int i = 0; i < statusCheckBoxes.Count; i++) {

                if (statusCheckBoxes[i].Enabled == false) {
                    continue;
                }

                /**
                 * For now we just do a simple set 0 or 1 value
                 * This might need to be changed later as more is learned, for now it works
                 */
                string[] fieldParts = statusCheckBoxes[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], statusCheckBoxes[i].Checked ? 1 : 0);
            }

            /**
             * If the total EXP until next level & current level were available before setting values,
             * we set it to what it should be to reach the next level (except for lvl 99 which is always 0)
             */
            if (_memoryValues.U32.ContainsKey("total_exp_until_next_level") == true && _memoryValues.Django.ContainsKey("level")) {
                int level = (int)_memoryValues.Django["level"].Value;
                _memoryValues.U32["total_exp_until_next_level"].Value = level < 99 ? DjangoExpTable.zoktai[level] : 0;
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
                case "sabata":
                    if (_memoryValues.Sabata.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.Sabata[memoryValueKey].Value = (uint)value;
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
                    if (_memoryValues.Inventory.ContainsKey(memoryValueKey) == true) {
                        _memoryValues.Inventory[memoryValueKey].Value = (uint)value;
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

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.All;
using BokInterface.ExpTables;

/**
 * File for Zoktai's status edit subwindow
 */

namespace BokInterface {
    partial class BokInterface {

        private CheckGroupBox _edit_skillGroupBox = new();
        private CheckGroupBox _edit_ExpGroupBox = new();
        private CheckGroupBox _edit_StatPointsGroupBox = new();

        private void ZoktaiStatusEditSubwindow() {
            int l = 0;
            int n = 0;

            // Get default values, depending on availability, these can be the current in-game values
            IDictionary<string, decimal> defaultValues = GetDefaultStatusValues();

            // Sections
            _edit_statusGroupBox = CreateCheckGroupBox("editStatusGroup", "Status", 5, 5, 103, 110);
            _edit_skillGroupBox = CreateCheckGroupBox("editSkillGroup", "Skill", 114, 5, 220, 110);
            _edit_statsGroupBox = CreateCheckGroupBox("editStatsGroup", "Stats", 340, 5, 84, 139);
            _edit_StatPointsGroupBox = CreateCheckGroupBox("editStatPointsGroup", "Points", 340, 144, 84, 53);
            _edit_ExpGroupBox = CreateCheckGroupBox("editExpGroup", "EXP", 5, 117, 97, 52);

            // Status
            _edit_statusLabels.Add(CreateLabel("djangoEditLevelLabel", "Level", 7, 24, 34, 15));
            _edit_statusLabels.Add(CreateLabel("djangoEditHpLabel", "LIFE :", 7, 52, 34, 15));
            _edit_statusLabels.Add(CreateLabel("djangoEditEneLabel", "ENE :", 7, 81, 34, 15));

            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_level", defaultValues["django_level"], 47, 21, 50, 23));
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_current_hp", defaultValues["django_current_hp"], 47, 50, 50, 23, maxValue: 1000));
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_current_ene", defaultValues["django_current_ene"], 47, 79, 50, 23, maxValue: 1000));

            // Add elements to group boxes / sections
            for (int i = 0; i < _edit_statusLabels.Count; i++) {
                l++;
                _edit_statusGroupBox.Controls.Add(_edit_statusLabels[i]);
            }

            for (int i = 0; i < _edit_statusNumericUpDowns.Count; i++) {
                n++;
                _edit_statusGroupBox.Controls.Add(_edit_statusNumericUpDowns[i]);
            }

            // Skill
            _edit_statusLabels.Add(CreateLabel("djangoEditSwordSkillLabel", "Sword", 8, 24, 54, 15, textAlignment: "MiddleLeft"));
            _edit_statusLabels.Add(CreateLabel("djangoEditSpearSkillLabel", "Spear", 121, 24, 36, 15, textAlignment: "MiddleLeft"));
            _edit_statusLabels.Add(CreateLabel("djangoEditHammerSkillLabel", "Hammer", 8, 52, 54, 15, textAlignment: "MiddleLeft"));
            _edit_statusLabels.Add(CreateLabel("djangoEditFistsSkillLabel", "Fists", 121, 52, 36, 15, textAlignment: "MiddleLeft"));
            _edit_statusLabels.Add(CreateLabel("djangoEditGunSkillLabel", "Gun", 8, 81, 54, 15, textAlignment: "MiddleLeft"));

            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_sword_skill", defaultValues["django_sword_skill"], 64, 21, 51, 23, nbDecimals: 2));
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_spear_skill", defaultValues["django_spear_skill"], 163, 21, 51, 23, nbDecimals: 2));
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_hammer_skill", defaultValues["django_hammer_skill"], 64, 50, 51, 23, nbDecimals: 2));
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_fists_skill", defaultValues["django_fists_skill"], 163, 50, 51, 23, nbDecimals: 2));
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_gun_skill", defaultValues["django_gun_skill"], 64, 79, 51, 23, nbDecimals: 2));

            // Add elements to group
            for (int i = l; i < _edit_statusLabels.Count; i++) {
                l++;
                _edit_skillGroupBox.Controls.Add(_edit_statusLabels[i]);
            }

            for (int i = n; i < _edit_statusNumericUpDowns.Count; i++) {
                n++;
                _edit_skillGroupBox.Controls.Add(_edit_statusNumericUpDowns[i]);
            }

            // Stats
            _edit_statusLabels.Add(CreateLabel("djangoEditVitLabel", "VIT", 8, 24, 27, 15));
            _edit_statusLabels.Add(CreateLabel("djangoEditSprLabel", "SPR", 8, 52, 27, 15));
            _edit_statusLabels.Add(CreateLabel("djangoEditStrLabel", "STR", 8, 81, 27, 15));
            _edit_statusLabels.Add(CreateLabel("djangoEditAgiLabel", "AGI", 8, 110, 27, 15));

            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_vit", defaultValues["django_vit"], 36, 21, 41, 23));
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_spr", defaultValues["django_spr"], 36, 50, 41, 23));
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_str", defaultValues["django_str"], 36, 79, 41, 23));
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_agi", defaultValues["django_agi"], 36, 110, 41, 23));

            // Add elements to group
            for (int i = l; i < _edit_statusLabels.Count; i++) {
                l++;
                _edit_statsGroupBox.Controls.Add(_edit_statusLabels[i]);
            }

            for (int i = n; i < _edit_statusNumericUpDowns.Count; i++) {
                n++;
                _edit_statsGroupBox.Controls.Add(_edit_statusNumericUpDowns[i]);
            }

            // Stat points available
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_stat_points", defaultValues["django_stat_points"], 32, 22, 46, 23, minValue: 0, maxValue: 255));

            // Add elements to group
            for (int i = n; i < _edit_statusNumericUpDowns.Count; i++) {
                n++;
                _edit_StatPointsGroupBox.Controls.Add(_edit_statusNumericUpDowns[i]);
            }

            // EXP
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_exp", defaultValues["django_exp"], 8, 22, 60, 23, minValue: 0, maxValue: 999999));

            // Add elements to group
            for (int i = n; i < _edit_statusNumericUpDowns.Count; i++) {
                n++;
                _edit_ExpGroupBox.Controls.Add(_edit_statusNumericUpDowns[i]);
            }

            // Add groups to subwindow
            statusEditWindow.Controls.Add(_edit_statusGroupBox);
            statusEditWindow.Controls.Add(_edit_skillGroupBox);
            statusEditWindow.Controls.Add(_edit_statsGroupBox);
            statusEditWindow.Controls.Add(_edit_StatPointsGroupBox);
            statusEditWindow.Controls.Add(_edit_ExpGroupBox);

            // Add tooltips & warnings
            Label expWarning = CreateImageLabel("tooltip", "warning", 75, 25);
            AddToolTip(expWarning, "Level will be automatically adjusted if EXP is high enough to reach higher levels");
            _edit_ExpGroupBox.Controls.Add(expWarning);

            // Button for setting values & its events
            Button setValuesButton = CreateButton("setStatusButton", "Set values", 350, 203, 75, 23);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetStatusValues();
                }
            });

            // Add button to subwindow, we do this here because the elements need to be added to the form already
            statusEditWindow.Controls.Add(setValuesButton);
        }

        /// <summary>Get default values for Zoktai</summary>
        /// <returns><c>IDictionary<string, decimal></c>Default values</returns>
        private IDictionary<string, decimal> GetZoktaiDefaultValues() {
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
            } else {
                // If stat is unvalid (if we are on the title screen or in a room transition), use specific values
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
            }

            return defaultValues;
        }

        /// <summary>Specific method for setting status values</summary>
        /// <param name="fields">List of fields to parse through</param>
        private void SetZoktaiStatusValues(List<NumericUpDown> fields) {

            /**
             * If the total EXP until next level & current level are available,
             * we'll use these to prevent the game from adjusting the level while setting new values
             * 
             * We'll set the total EXP until next level to the maximum possible to prevent that from happening
             */
            if (_memoryValues.U32.ContainsKey("total_exp_until_next_level") == true) {
                _memoryValues.U32["total_exp_until_next_level"].Value = 99999999;
            }

            // Sets values based on fields
            for (int i = 0; i < fields.Count; i++) {

                // If the field is disabled, skip it
                if (fields[i].Enabled == false) {
                    continue;
                }

                decimal value = fields[i].Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the input field's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = fields[i].Name.Split(['_'], 2);
                string subList = fieldParts[0];
                string memoryValueKey = fieldParts[1];
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

            /**
             * If the total EXP until next level & current level were available before setting values,
             * we set it to what it should be to reach the next level (except for lvl 99 which is always 0)
             */
            if (_memoryValues.U32.ContainsKey("total_exp_until_next_level") == true && _memoryValues.Django.ContainsKey("level")) {
                int level = (int)_memoryValues.Django["level"].Value;
                _memoryValues.U32["total_exp_until_next_level"].Value = level < 99 ? Django.zoktai[level] : 0;
            }
        }
    }
}

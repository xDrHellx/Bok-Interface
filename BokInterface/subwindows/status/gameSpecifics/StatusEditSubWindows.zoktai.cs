using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.All;

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
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_stat_points", defaultValues["django_stat_points"], 32, 22, 46, 23, minValue: 0, maxValue: 255));

            // Add elements to group
            for (int i = n; i < edit_statusNumericUpDowns.Count; i++) {
                n++;
                edit_StatPointsGroupBox.Controls.Add(edit_statusNumericUpDowns[i]);
            }

            // EXP
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_exp", defaultValues["django_exp"], 8, 22, 60, 23, minValue: 0, maxValue: 999999));

            // Add elements to group
            for (int i = n; i < edit_statusNumericUpDowns.Count; i++) {
                n++;
                edit_ExpGroupBox.Controls.Add(edit_statusNumericUpDowns[i]);
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
            edit_ExpGroupBox.Controls.Add(expWarning);

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
            uint stat = APIs.Memory.ReadU32(_zoktaiAddresses.Misc["stat"]);

            // If stat is a valid value
            if (stat > 0) {
                defaultValues.Add("django_current_hp", _memoryValues.Django["current_hp"].Value);
                defaultValues.Add("django_current_ene", _memoryValues.Django["current_ene"].Value);

                defaultValues.Add("django_exp", _memoryValues.U32["exp"].Value);
                defaultValues.Add("django_level", _memoryValues.U16["level"].Value);

                defaultValues.Add("django_vit", _memoryValues.Django["vit"].Value);
                defaultValues.Add("django_spr", _memoryValues.Django["spr"].Value);
                defaultValues.Add("django_str", _memoryValues.Django["str"].Value);
                defaultValues.Add("django_agi", _memoryValues.Django["agi"].Value);
                defaultValues.Add("django_stat_points", _memoryValues.U16["stat_points"].Value);

                defaultValues.Add("django_sword_skill", Utilities.ExpToLevel(_memoryValues.U16["sword_skill"].Value));
                defaultValues.Add("django_spear_skill", Utilities.ExpToLevel(_memoryValues.U16["spear_skill"].Value));
                defaultValues.Add("django_hammer_skill", Utilities.ExpToLevel(_memoryValues.U16["hammer_skill"].Value));
                defaultValues.Add("django_fists_skill", Utilities.ExpToLevel(_memoryValues.U16["fists_skill"].Value));
                defaultValues.Add("django_gun_skill", Utilities.ExpToLevel(_memoryValues.U16["gun_skill"].Value));
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

        /// <summary>
        /// Specific method for Bok 2 to update both "current" and "persistent" stats addresses at once <br>
        /// For some reason updating "current" is not enough, when switching room the game sets back the old values
        /// </summary>
        /// <param name="stat">Stat name</param>
        /// <param name="value">Value to set</param>
        private void ZoktaiUpdateStats(string stat, uint value) {
            if (memoryValues.U16.ContainsKey(stat) == true) {
                memoryValues.U16[stat].Value = value;
            }
        }
    }
}

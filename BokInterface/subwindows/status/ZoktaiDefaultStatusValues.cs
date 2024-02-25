using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.All;

/**
 * File for Zoktai's status edit subwindow
 */

namespace BokInterface {
    partial class BokInterfaceMainForm {

        private GroupBox edit_skillGroupBox = new();

        private void ZoktaiStatusEditSubwindow() {
            int l = 0;
            int n = 0;

            // Get default values, depending on availability, these can be the current in-game values
            IDictionary<string, decimal> defaultValues = GetDefaultStatusValues();

            // Sections
            edit_statusGroupBox = CreateGroupBox("editStatusGroup", "Status", 5, 5, 103, 76);
            edit_skillGroupBox = CreateGroupBox("editSkillGroup", "Skill", 114, 5, 220, 105);
            edit_statsGroupBox = CreateGroupBox("editStatsGroup", "Stats", 340, 5, 84, 134);

            // Status
            edit_statusLabels.Add(CreateLabel("djangoEditHpLabel", "LIFE :", 7, 19, 34, 15));
            edit_statusLabels.Add(CreateLabel("djangoEditEneLabel", "ENE :", 7, 47, 34, 15));

            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_current_hp", defaultValues["django_current_hp"], 47, 16, 50, 23, maxValue: 1000));
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_current_ene", defaultValues["django_current_ene"], 47, 45, 50, 23, maxValue: 1000));

            // Add elements to group boxes / sections
            for (int i = 0; i < edit_statusLabels.Count; i++) {
                l++;
                edit_statusGroupBox.Controls.Add(edit_statusLabels[i]);
            }

            for (int i = 0; i < edit_statusNumericUpDowns.Count; i++) {
                n++;
                edit_statusGroupBox.Controls.Add(edit_statusNumericUpDowns[i]);
            }

            // Skill
            edit_statusLabels.Add(CreateLabel("djangoEditSwordSkillLabel", "Sword", 8, 19, 54, 15));
            edit_statusLabels.Add(CreateLabel("djangoEditSpearSkillLabel", "Spear", 121, 19, 36, 15));
            edit_statusLabels.Add(CreateLabel("djangoEditHammerSkillLabel", "Hammer", 8, 47, 54, 15));
            edit_statusLabels.Add(CreateLabel("djangoEditFistsSkillLabel", "Fists", 121, 47, 36, 15));
            edit_statusLabels.Add(CreateLabel("djangoEditGunSkillLabel", "Gun", 8, 76, 54, 15));

            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_sword_skill", defaultValues["django_sword_skill"], 64, 16, 51, 23, nbDecimals: 2));
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_spear_skill", defaultValues["django_spear_skill"], 163, 16, 51, 23, nbDecimals: 2));
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_hammer_skill", defaultValues["django_hammer_skill"], 64, 45, 51, 23, nbDecimals: 2));
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_fists_skill", defaultValues["django_fists_skill"], 163, 45, 51, 23, nbDecimals: 2));
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_gun_skill", defaultValues["django_gun_skill"], 64, 74, 51, 23, nbDecimals: 2));

            // Add elements to group
            for (int i = l; i < edit_statusLabels.Count; i++) {
                l++;
                edit_skillGroupBox.Controls.Add(edit_statusLabels[i]);
            }

            for (int i = n; i < edit_statusNumericUpDowns.Count; i++) {
                n++;
                edit_skillGroupBox.Controls.Add(edit_statusNumericUpDowns[i]);
            }

            // Stats
            edit_statusLabels.Add(CreateLabel("djangoEditVitLabel", "VIT", 8, 19, 27, 15));
            edit_statusLabels.Add(CreateLabel("djangoEditSprLabel", "SPR", 8, 47, 27, 15));
            edit_statusLabels.Add(CreateLabel("djangoEditStrLabel", "STR", 8, 76, 27, 15));
            edit_statusLabels.Add(CreateLabel("djangoEditAgiLabel", "AGI", 8, 105, 27, 15));

            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_vit", defaultValues["django_vit"], 36, 16, 41, 23));
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_spr", defaultValues["django_spr"], 36, 45, 41, 23));
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_str", defaultValues["django_str"], 36, 74, 41, 23));
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_agi", defaultValues["django_agi"], 36, 105, 41, 23));

            // Add elements to group
            for (int i = l; i < edit_statusLabels.Count; i++) {
                l++;
                edit_statsGroupBox.Controls.Add(edit_statusLabels[i]);
            }

            for (int i = n; i < edit_statusNumericUpDowns.Count; i++) {
                n++;
                edit_statsGroupBox.Controls.Add(edit_statusNumericUpDowns[i]);
            }

            // Add groups to subwindow
            statusEditWindow.Controls.Add(edit_statusGroupBox);
            statusEditWindow.Controls.Add(edit_skillGroupBox);
            statusEditWindow.Controls.Add(edit_statsGroupBox);

            // Button for setting values & its events
            Button setValuesButton = CreateButton("setStatusButton", "Set values", 349, 145, 75, 23);
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
            uint stat = APIs.Memory.ReadU32(zoktaiAddresses.Misc["stat"]);

            // If stat is a valid value
            if (stat > 0) {
                defaultValues.Add("django_current_hp", memoryValues.Django["current_hp"].Value);
                defaultValues.Add("django_current_ene", memoryValues.Django["current_ene"].Value);
                defaultValues.Add("django_vit", memoryValues.Django["vit"].Value);
                defaultValues.Add("django_spr", memoryValues.Django["spr"].Value);
                defaultValues.Add("django_str", memoryValues.Django["str"].Value);
                defaultValues.Add("django_agi", memoryValues.Django["agi"].Value);

                defaultValues.Add("django_sword_skill", Utilities.ExpToLevel(memoryValues.U16["sword_skill"].Value));
                defaultValues.Add("django_spear_skill", Utilities.ExpToLevel(memoryValues.U16["spear_skill"].Value));
                defaultValues.Add("django_hammer_skill", Utilities.ExpToLevel(memoryValues.U16["hammer_skill"].Value));
                defaultValues.Add("django_fists_skill", Utilities.ExpToLevel(memoryValues.U16["fists_skill"].Value));
                defaultValues.Add("django_gun_skill", Utilities.ExpToLevel(memoryValues.U16["gun_skill"].Value));
            } else {
                // If stat is unvalid (if we are on the title screen or in a room transition), use specific values
                defaultValues.Add("django_current_hp", 100);
                defaultValues.Add("django_current_ene", 100);
                defaultValues.Add("django_vit", 10);
                defaultValues.Add("django_spr", 10);
                defaultValues.Add("django_str", 10);
                defaultValues.Add("django_agi", 10);

                defaultValues.Add("django_sword_skill", 1);
                defaultValues.Add("django_spear_skill", 1);
                defaultValues.Add("django_hammer_skill", 1);
                defaultValues.Add("django_fists_skill", 1);
                defaultValues.Add("django_gun_skill", 80);
            }

            return defaultValues;
        }
    }
}

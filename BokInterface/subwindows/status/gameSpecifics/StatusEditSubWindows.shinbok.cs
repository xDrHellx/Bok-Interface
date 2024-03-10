using System;
using System.Collections.Generic;
using System.Windows.Forms;

/**
 * File for Shinbok's status edit subwindow
 */

namespace BokInterface {
    partial class BokInterface {

        private void ShinbokStatusEditSubwindow() {

            int l = 0;
            int n = 0;

            // Get default values, depending on availability, these can be the current in-game values
            IDictionary<string, decimal> defaultValues = GetDefaultStatusValues();

            // Sections
            edit_statusGroupBox = CreateCheckGroupBox("editStatusGroup", "Status", 5, 5, 103, 110);
            edit_statsGroupBox = CreateCheckGroupBox("editStatsGroup", "Stats", 114, 5, 107, 110);

            // Status
            edit_statusLabels.Add(CreateLabel("djangoEditHpLabel", "LIFE :", 7, 24, 34, 15));
            // this.edit_statusLabels.Add(this.CreateLabel("djangoEditEneLabel", "ENE :", 7, 52, 34, 15));
            // this.edit_statusLabels.Add(this.CreateLabel("djangoEditTrcLabel", "TRC :", 7, 81, 34, 15));

            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_current_hp", defaultValues["django_current_hp"], 47, 21, 50, 23, maxValue: 1000));
            // this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_current_ene", defaultValues["django_current_ene"], 47, 50, 50, 23, maxValue: 1000));
            // this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_current_trc", defaultValues["django_current_trc"], 47, 79, 50, 23, maxValue: 1000));

            // Add elements to group boxes / sections
            for (int i = 0; i < edit_statusLabels.Count; i++) {
                l++;
                edit_statusGroupBox.Controls.Add(edit_statusLabels[i]);
            }

            for (int i = 0; i < edit_statusNumericUpDowns.Count; i++) {
                n++;
                edit_statusGroupBox.Controls.Add(edit_statusNumericUpDowns[i]);
            }

            // Stats
            edit_statusLabels.Add(CreateLabel("djangoEditHpLabel", "VIT", 8, 24, 27, 15));
            edit_statusLabels.Add(CreateLabel("djangoEditEneLabel", "SPR", 8, 51, 27, 15));
            edit_statusLabels.Add(CreateLabel("djangoEditTrcLabel", "STR", 8, 81, 27, 15));

            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_base_vit", defaultValues["django_base_vit"], 36, 21, 41, 23, maxValue: 100));
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_base_spr", defaultValues["django_base_spr"], 36, 50, 41, 23, maxValue: 100));
            edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_base_str", defaultValues["django_base_str"], 36, 79, 41, 23, maxValue: 100));

            // Tooltips & warnings
            List<Label> warningLabels = [
                CreateImageLabel("tooltip", "warning", 83, 23),
                CreateImageLabel("tooltip", "warning", 83, 52),
                CreateImageLabel("tooltip", "warning", 83, 81)
            ];

            // Add tooltips to labels group
            for (int i = 0; i < warningLabels.Count; i++) {
                edit_statusLabels.Add(warningLabels[i]);
            }

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
            statusEditWindow.Controls.Add(edit_statsGroupBox);

            // Button for setting values & its events
            Button setValuesButton = CreateButton("setStatusButton", "Set values", 147, 121, 75, 23);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetStatusValues();
                }
            });

            // Add button to subwindow, we do this here because the elements need to be added to the form already
            statusEditWindow.Controls.Add(setValuesButton);

            // Add tooltips
            AddValuesWarningToolTip(warningLabels);
        }


        /// <summary>Get default values for Shinbok</summary>
        /// <returns><c>IDictionary<string, decimal></c>Default values</returns>
        private IDictionary<string, decimal> GetShinbokDefaultValues() {

            IDictionary<string, decimal> defaultValues = new Dictionary<string, decimal>();
            uint djangoCurrentHp = memoryValues.Django["current_hp"].Value;

            // If HP value is valid, get the other in-game values
            if (djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
                defaultValues.Add("django_current_hp", djangoCurrentHp);
                defaultValues.Add("django_base_vit", memoryValues.Django["base_vit"].Value);
                defaultValues.Add("django_base_spr", memoryValues.Django["base_spr"].Value);
                defaultValues.Add("django_base_str", memoryValues.Django["base_str"].Value);
            } else {
                // If HP is unvalid (for example if we are on the title screen or in bike races), use specific values
                defaultValues.Add("django_current_hp", 100);
                // defaultValues.Add("django_current_ene", 100);
                // defaultValues.Add("django_current_trc", 1000);
                defaultValues.Add("django_base_vit", 10);
                defaultValues.Add("django_base_spr", 10);
                defaultValues.Add("django_base_str", 10);
            }

            return defaultValues;
        }
    }
}

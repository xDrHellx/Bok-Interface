using System;
using System.Collections.Generic;

/**
 * File for Shinbok's status edit subwindow
 */

namespace BokInterface
{
    partial class BokInterfaceMainForm
    {

        private void ShinbokStatusEditSubwindow()
        {

            int l = 0;
            int n = 0;

            // Get default values, depending on availability, these can be the current in-game values
            var defaultValues = this.GetDefaultStatusValues();

            // Sections
            this.edit_statusGroupBox = this.CreateGroupBox("editStatusGroup", "Status", 5, 5, 103, 105, true);
            this.edit_statsGroupBox = this.CreateGroupBox("editStatsGroup", "Stats", 114, 5, 107, 105, true);

            // Status
            this.edit_statusLabels.Add(this.CreateLabel("djangoEditHpLabel", "LIFE :", 7, 19, 34, 15));
            // this.edit_statusLabels.Add(this.CreateLabel("djangoEditEneLabel", "ENE :", 7, 47, 34, 15));
            // this.edit_statusLabels.Add(this.CreateLabel("djangoEditTrcLabel", "TRC :", 7, 76, 34, 15));

            this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_current_hp", defaultValues["django_current_hp"], 47, 16, 50, 23, 1, 1000));
            // this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_current_ene", defaultValues["django_current_ene"], 47, 45, 50, 23, 1, 1000));
            // this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_current_trc", defaultValues["django_current_trc"], 47, 74, 50, 23, 1, 1000));

            // Add elements to group boxes / sections
            for (int i = 0; i < this.edit_statusLabels.Count; i++)
            {
                l++;
                this.edit_statusGroupBox.Controls.Add(this.edit_statusLabels[i]);
            }

            for (int i = 0; i < this.edit_statusNumericUpDowns.Count; i++)
            {
                n++;
                this.edit_statusGroupBox.Controls.Add(this.edit_statusNumericUpDowns[i]);
            }

            // Stats
            this.edit_statusLabels.Add(this.CreateLabel("djangoEditHpLabel", "VIT", 8, 19, 27, 15));
            this.edit_statusLabels.Add(this.CreateLabel("djangoEditEneLabel", "SPR", 8, 47, 27, 15));
            this.edit_statusLabels.Add(this.CreateLabel("djangoEditTrcLabel", "STR", 8, 76, 27, 15));

            this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_base_vit", defaultValues["django_base_vit"], 36, 16, 41, 23));
            this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_base_spr", defaultValues["django_base_spr"], 36, 45, 41, 23));
            this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_base_str", defaultValues["django_base_str"], 36, 74, 41, 23));

            // Tooltips & warnings
            List<System.Windows.Forms.Label> warningLabels = new() {
                this.CreateImageLabel("tooltip", "warning", 83, 18),
                this.CreateImageLabel("tooltip", "warning", 83, 47),
                this.CreateImageLabel("tooltip", "warning", 83, 76)
            };

            // Add tooltips to labels group
            for (int i = 0; i < warningLabels.Count; i++)
            {
                this.edit_statusLabels.Add(warningLabels[i]);
            }

            // Add elements to group
            for (int i = l; i < this.edit_statusLabels.Count; i++)
            {
                l++;
                this.edit_statsGroupBox.Controls.Add(this.edit_statusLabels[i]);
            }

            for (int i = n; i < this.edit_statusNumericUpDowns.Count; i++)
            {
                n++;
                this.edit_statsGroupBox.Controls.Add(this.edit_statusNumericUpDowns[i]);
            }

            // Add groups to subwindow
            this.statusEditWindow.Controls.Add(this.edit_statusGroupBox);
            this.statusEditWindow.Controls.Add(this.edit_statsGroupBox);

            // Button for setting values & its events
            System.Windows.Forms.Button setValuesButton = this.CreateButton("setStatusButton", "Set values", 123, 116, 75, 23);
            setValuesButton.Click += new System.EventHandler(delegate (object sender, EventArgs e)
            {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++)
                {
                    this.SetStatusValues();
                }
            });

            // Add button to subwindow, we do this here because the elements need to be added to the form already
            this.statusEditWindow.Controls.Add(setValuesButton);

            // Add tooltips
            BokInterfaceMainForm.AddValuesWarningToolTip(warningLabels);
        }


        /// <summary>Get default values for Shinbok</summary>
        /// <returns><c>IDictionary<string, uint></c>Default values</returns>
        private IDictionary<string, uint> GetShinbokDefaultValues()
        {

            IDictionary<string, uint> defaultValues = new Dictionary<string, uint>();
            uint djangoCurrentHp = this.memoryValues.Django["current_hp"].Value;

            // If HP value is valid, get the other in-game values
            if (djangoCurrentHp >= 0 && djangoCurrentHp <= 1000)
            {
                defaultValues.Add("django_current_hp", djangoCurrentHp);
                defaultValues.Add("django_base_vit", this.memoryValues.Django["base_vit"].Value);
                defaultValues.Add("django_base_spr", this.memoryValues.Django["base_spr"].Value);
                defaultValues.Add("django_base_str", this.memoryValues.Django["base_str"].Value);
            }
            else
            {
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
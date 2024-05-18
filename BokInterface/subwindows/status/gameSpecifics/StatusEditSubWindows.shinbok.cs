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
            _edit_statusGroupBox = CreateCheckGroupBox("editStatusGroup", "Status", 5, 5, 103, 110);
            _edit_statsGroupBox = CreateCheckGroupBox("editStatsGroup", "Stats", 114, 5, 107, 110);

            // Status
            _edit_statusLabels.Add(CreateLabel("djangoEditHpLabel", "LIFE :", 7, 24, 34, 15));
            // _edit_statusLabels.Add(this.CreateLabel("djangoEditEneLabel", "ENE :", 7, 52, 34, 15));
            // _edit_statusLabels.Add(this.CreateLabel("djangoEditTrcLabel", "TRC :", 7, 81, 34, 15));

            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_current_hp", defaultValues["django_current_hp"], 47, 21, 50, 23, maxValue: 1000));
            // _edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_current_ene", defaultValues["django_current_ene"], 47, 50, 50, 23, maxValue: 1000));
            // _edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_current_trc", defaultValues["django_current_trc"], 47, 79, 50, 23, maxValue: 1000));

            // Add elements to group boxes / sections
            for (int i = 0; i < _edit_statusLabels.Count; i++) {
                l++;
                _edit_statusGroupBox.Controls.Add(_edit_statusLabels[i]);
            }

            for (int i = 0; i < _edit_statusNumericUpDowns.Count; i++) {
                n++;
                _edit_statusGroupBox.Controls.Add(_edit_statusNumericUpDowns[i]);
            }

            // Stats
            _edit_statusLabels.Add(CreateLabel("djangoEditHpLabel", "VIT", 8, 24, 27, 15));
            _edit_statusLabels.Add(CreateLabel("djangoEditEneLabel", "SPR", 8, 51, 27, 15));
            _edit_statusLabels.Add(CreateLabel("djangoEditTrcLabel", "STR", 8, 81, 27, 15));

            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_base_vit", defaultValues["django_base_vit"], 36, 21, 41, 23, maxValue: 100));
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_base_spr", defaultValues["django_base_spr"], 36, 50, 41, 23, maxValue: 100));
            _edit_statusNumericUpDowns.Add(CreateNumericUpDown("django_base_str", defaultValues["django_base_str"], 36, 79, 41, 23, maxValue: 100));

            // Tooltips & warnings
            List<Label> warningLabels = [
                CreateImageLabel("tooltip", "warning", 83, 23),
                CreateImageLabel("tooltip", "warning", 83, 52),
                CreateImageLabel("tooltip", "warning", 83, 81)
            ];

            // Add tooltips to labels group
            for (int i = 0; i < warningLabels.Count; i++) {
                _edit_statusLabels.Add(warningLabels[i]);
            }

            // Add elements to group
            for (int i = l; i < _edit_statusLabels.Count; i++) {
                l++;
                _edit_statsGroupBox.Controls.Add(_edit_statusLabels[i]);
            }

            for (int i = n; i < _edit_statusNumericUpDowns.Count; i++) {
                n++;
                _edit_statsGroupBox.Controls.Add(_edit_statusNumericUpDowns[i]);
            }

            // Add groups to subwindow
            statusEditWindow.Controls.Add(_edit_statusGroupBox);
            statusEditWindow.Controls.Add(_edit_statsGroupBox);

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
            uint djangoCurrentHp = _memoryValues.Django["current_hp"].Value;

            // If HP value is valid, get the other in-game values
            if (djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
                defaultValues.Add("django_current_hp", djangoCurrentHp);
                defaultValues.Add("django_base_vit", _memoryValues.Django["base_vit"].Value);
                defaultValues.Add("django_base_spr", _memoryValues.Django["base_spr"].Value);
                defaultValues.Add("django_base_str", _memoryValues.Django["base_str"].Value);
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

        /// <summary>Specific method for setting status values</summary>
        /// <param name="fields">List of fields to parse through</param>
        private void SetShinbokStatusValues(List<NumericUpDown> fields) {

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
                            _memoryValues.Django[memoryValueKey].Value = (uint)value;
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
                    case "bike":
                        if (_memoryValues.Bike.ContainsKey(memoryValueKey) == true) {
                            _memoryValues.Bike[memoryValueKey].Value = (uint)value;
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
}

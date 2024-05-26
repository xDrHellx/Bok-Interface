using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Status {
    /// <summary>Status editor for Boktai 3</summary>
    class ShinbokStatusEditor : StatusEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _shinbokAddresses;

        #endregion

        public ShinbokStatusEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses shinbokAddresses) {

            _memoryValues = memoryValues;
            _bokInterface = bokInterface;
            _shinbokAddresses = shinbokAddresses;

            Name = name;
            Text = text;
            Icon = _bokInterface.Icon;
            AutoScaleDimensions = new SizeF(6F, 15F);
            AutoScaleMode = AutoScaleMode.Inherit;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = SystemColors.Control;
            Font = WinFormHelpers.defaultFont;
            AutoScroll = true;
            Owner = _bokInterface;
            ClientSize = new Size(227, 149);

            // Generate the subwindow & add the onClose event to it
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
            _statusGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatusGroup", "Status", 5, 5, 103, 110, control: this);
            _statsGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatsGroup", "Stats", 114, 5, 107, 110, control: this);

            // Status
            WinFormHelpers.CreateLabel("djangoEditHpLabel", "LIFE :", 7, 24, 34, 15, _statusGroupBox);
            // WinFormHelpers.CreateLabel("djangoEditEneLabel", "ENE :", 7, 52, 34, 15, _statusGroupBox);
            // WinFormHelpers.CreateLabel("djangoEditTrcLabel", "TRC :", 7, 81, 34, 15, _statusGroupBox);

            _statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_hp", defaultValues["django_current_hp"], 47, 21, 50, 23, maxValue: 1000, control: _statusGroupBox));
            // _statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_ene", defaultValues["django_current_ene"], 47, 50, 50, 23, maxValue: 1000, control: _statusGroupBox));
            // _statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_trc", defaultValues["django_current_trc"], 47, 79, 50, 23, maxValue: 1000, control: _statusGroupBox));

            // Stats
            WinFormHelpers.CreateLabel("djangoEditHpLabel", "VIT", 8, 24, 27, 15, control: _statsGroupBox);
            WinFormHelpers.CreateLabel("djangoEditEneLabel", "SPR", 8, 51, 27, 15, control: _statsGroupBox);
            WinFormHelpers.CreateLabel("djangoEditTrcLabel", "STR", 8, 81, 27, 15, control: _statsGroupBox);

            _statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_base_vit", defaultValues["django_base_vit"], 36, 21, 41, 23, maxValue: 100, control: _statsGroupBox));
            _statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_base_spr", defaultValues["django_base_spr"], 36, 50, 41, 23, maxValue: 100, control: _statsGroupBox));
            _statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_base_str", defaultValues["django_base_str"], 36, 79, 41, 23, maxValue: 100, control: _statsGroupBox));

            // Tooltips & warnings
            List<Label> warningLabels = [
                WinFormHelpers.CreateImageLabel("tooltip", "warning", 83, 23, _statsGroupBox),
                WinFormHelpers.CreateImageLabel("tooltip", "warning", 83, 52, _statsGroupBox),
                WinFormHelpers.CreateImageLabel("tooltip", "warning", 83, 81, _statsGroupBox)
            ];

            // Add tooltips
            WinFormHelpers.AddValuesWarningToolTip(warningLabels);

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 147, 121, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        /// <summary>Get default values</summary>
        /// <returns><c>IDictionary<string, decimal></c>Default values</returns>
        protected override IDictionary<string, decimal> GetDefaultValues() {

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
        protected override void SetValues() {

            // Retrieve all input fields
            List<NumericUpDown> fields = _statusNumericUpDowns;

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

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

            /**
             * If BizHawk was not paused before setting values, unpause it
             * Otherwise keep it paused
             */
            if (_bokInterface._previousIsPauseSetting == true) {
                APIs.Client.Unpause();
            }
        }
    }
}
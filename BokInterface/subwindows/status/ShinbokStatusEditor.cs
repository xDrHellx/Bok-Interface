using System;
using System.Collections.Generic;
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
            _shinbokAddresses = shinbokAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(283, 157);

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
            statusGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatusGroup", "Status", 5, 5, 102, 106, control: this);
            statsGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatsGroup", "Stats", 113, 5, 166, 124, control: this);

            // Status
            WinFormHelpers.CreateLabel("djangoEditHpLabel", "LIFE :", 7, 22, 34, 15, statusGroupBox);
            WinFormHelpers.CreateLabel("djangoEditEneLabel", "ENE :", 7, 50, 34, 15, statusGroupBox);
            WinFormHelpers.CreateLabel("djangoEditTrcLabel", "TRC :", 7, 79, 34, 15, statusGroupBox);

            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_hp", defaultValues["django_current_hp"], 47, 19, 50, 23, maxValue: 1000, control: statusGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_ene", defaultValues["django_current_ene"], 47, 48, 50, 23, maxValue: 1000, control: statusGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_trc", defaultValues["django_current_trc"], 47, 77, 50, 23, maxValue: 1000, control: statusGroupBox));

            // Stats
            WinFormHelpers.CreateLabel("djangoEditVitLabel", "VIT", 7, 39, 27, 15, control: statsGroupBox);
            WinFormHelpers.CreateLabel("djangoEditSprLabel", "SPR", 7, 68, 27, 15, control: statsGroupBox);
            WinFormHelpers.CreateLabel("djangoEditStrLabel", "STR", 7, 97, 27, 15, control: statsGroupBox);

            WinFormHelpers.CreateLabel("djangoStatsBaseLabel", "Base", 35, 19, 42, 15, control: statsGroupBox, WinFormHelpers.baseStatColor);
            WinFormHelpers.CreateLabel("djangoStatsCardsLabel", "Cards", 77, 19, 42, 15, control: statsGroupBox, WinFormHelpers.cardsStatColor);
            WinFormHelpers.CreateLabel("djangoStatsEquipsLabel", "Equips", 119, 19, 42, 15, control: statsGroupBox, WinFormHelpers.equipsStatColor);

            // Base
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_base_vit", defaultValues["django_base_vit"], 39, 37, 38, 23, maxValue: 100, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_base_spr", defaultValues["django_base_spr"], 39, 66, 38, 23, maxValue: 100, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_base_str", defaultValues["django_base_str"], 39, 95, 38, 23, maxValue: 100, control: statsGroupBox));

            // Cards
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_cards_vit", defaultValues["django_cards_vit"], 81, 37, 38, 23, maxValue: 100, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_cards_spr", defaultValues["django_cards_spr"], 81, 66, 38, 23, maxValue: 100, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_cards_str", defaultValues["django_cards_str"], 81, 95, 38, 23, maxValue: 100, control: statsGroupBox));

            // Equips / accessories
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_equips_vit", defaultValues["django_equips_vit"], 123, 37, 38, 23, maxValue: 100, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_equips_spr", defaultValues["django_equips_spr"], 123, 66, 38, 23, maxValue: 100, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_equips_str", defaultValues["django_equips_str"], 123, 95, 38, 23, maxValue: 100, control: statsGroupBox));

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 205, 131, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        protected override IDictionary<string, decimal> GetDefaultValues() {

            IDictionary<string, decimal> defaultValues = new Dictionary<string, decimal>();
            uint djangoCurrentHp = _memoryValues.Django["current_hp"].Value;

            // If HP value is valid, get the other in-game values
            if (djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
                defaultValues.Add("django_current_hp", djangoCurrentHp);
                defaultValues.Add("django_current_ene", _memoryValues.Django["current_ene"].Value);
                defaultValues.Add("django_current_trc", _memoryValues.Django["current_trc"].Value);
                defaultValues.Add("django_base_vit", _memoryValues.Django["base_vit"].Value);
                defaultValues.Add("django_base_spr", _memoryValues.Django["base_spr"].Value);
                defaultValues.Add("django_base_str", _memoryValues.Django["base_str"].Value);
                defaultValues.Add("django_cards_vit", _memoryValues.Django["cards_vit"].Value);
                defaultValues.Add("django_cards_spr", _memoryValues.Django["cards_spr"].Value);
                defaultValues.Add("django_cards_str", _memoryValues.Django["cards_str"].Value);
                defaultValues.Add("django_equips_vit", _memoryValues.Django["equips_vit"].Value);
                defaultValues.Add("django_equips_spr", _memoryValues.Django["equips_spr"].Value);
                defaultValues.Add("django_equips_str", _memoryValues.Django["equips_str"].Value);
            } else {
                // If HP is unvalid (for example if we are on the title screen or in bike races), use specific values
                defaultValues.Add("django_current_hp", 100);
                defaultValues.Add("django_current_ene", 100);
                defaultValues.Add("django_current_trc", 1000);
                defaultValues.Add("django_base_vit", 10);
                defaultValues.Add("django_base_spr", 10);
                defaultValues.Add("django_base_str", 10);
                defaultValues.Add("django_cards_vit", 0);
                defaultValues.Add("django_cards_spr", 0);
                defaultValues.Add("django_cards_str", 0);
                defaultValues.Add("django_equips_vit", 0);
                defaultValues.Add("django_equips_spr", 0);
                defaultValues.Add("django_equips_str", 0);
            }

            return defaultValues;
        }

        protected override void SetValues() {

            // Retrieve all input fields
            List<NumericUpDown> fields = statusNumericUpDowns;

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
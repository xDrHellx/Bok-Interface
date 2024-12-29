using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

/**
 * Note :
 * 
 * Due to how the game works, stat points from accessories cannot be updated here
 * The game checks equipped accessories when switching room
 */

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

            SetFormParameters(241, 157);

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
            statsGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatsGroup", "Stats", 113, 5, 124, 124, control: this);

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

            // Base
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_base_vit", defaultValues["django_base_vit"], 39, 37, 38, 23, maxValue: 100, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_base_spr", defaultValues["django_base_spr"], 39, 66, 38, 23, maxValue: 100, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_base_str", defaultValues["django_base_str"], 39, 95, 38, 23, maxValue: 100, control: statsGroupBox));

            // Cards
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_cards_vit", defaultValues["django_cards_vit"], 81, 37, 38, 23, maxValue: 100, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_cards_spr", defaultValues["django_cards_spr"], 81, 66, 38, 23, maxValue: 100, control: statsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_cards_str", defaultValues["django_cards_str"], 81, 95, 38, 23, maxValue: 100, control: statsGroupBox));

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 163, 131, 75, 23, this);
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
                defaultValues.Add("django_base_vit", _memoryValues.Misc["base_vit"].Value);
                defaultValues.Add("django_base_spr", _memoryValues.Misc["base_spr"].Value);
                defaultValues.Add("django_base_str", _memoryValues.Misc["base_str"].Value);
                defaultValues.Add("django_cards_vit", _memoryValues.Misc["cards_vit"].Value);
                defaultValues.Add("django_cards_spr", _memoryValues.Misc["cards_spr"].Value);
                defaultValues.Add("django_cards_str", _memoryValues.Misc["cards_str"].Value);
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
                string[] fieldParts = fields[i].Name.Split(['_'], 3);
                string subList = fieldParts[0];
                string memoryValueKey = fieldParts[1] + "_" + fieldParts[2];
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
                    case "misc":
                        if (_memoryValues.Misc.ContainsKey(memoryValueKey) == true) {
                            switch (memoryValueKey) {
                                case "base_vit":                // Base points from level ups
                                case "base_spr":
                                case "base_str":
                                case "cards_vit":               // Points from cards
                                case "cards_spr":
                                case "cards_str":
                                    /**
                                     * Due to how the game works we have to set the sum of base + card stats
                                     * 
                                     * This allows stats to be updated in the current room
                                     * and stay when switching room
                                     */
                                    _memoryValues.Misc[memoryValueKey].Value = (uint)value;
                                    UpdateSumBaseCardStat(fieldParts[2], _memoryValues.Misc["base_" + fieldParts[2]].Value, _memoryValues.Misc["cards_" + fieldParts[2]].Value);
                                    break;
                                default:                        // Default treatment
                                    _memoryValues.Misc[memoryValueKey].Value = (uint)value;
                                    break;
                            }
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

        /// <summary>Updates the sum of base + card points for a stat</summary>
        /// <param name="stat">Stat to update (str, spr, str)</param>
        private void UpdateSumBaseCardStat(string stat, uint basePoints, uint cardPoints) {
            string key = "sum_base_cards_" + stat;
            if (_memoryValues.Django.ContainsKey(key) == true) {
                _memoryValues.Django[key].Value = basePoints + cardPoints;
            }
        }
    }
}
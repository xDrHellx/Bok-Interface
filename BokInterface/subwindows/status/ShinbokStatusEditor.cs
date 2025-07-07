using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;
using BokInterface.ExpTables;

/**
 * Note :
 * 
 * Due to how the game works, stat points from accessories cannot be updated here
 * The game checks equipped accessories when switching room
 */

namespace BokInterface.Status {
    /// <summary>Status editor for Boktai 3</summary>
    class ShinbokStatusEditor : StatusEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _shinbokAddresses;

        #endregion

        #region Constructor

        public ShinbokStatusEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses shinbokAddresses) {

            _memoryValues = memoryValues;
            _shinbokAddresses = shinbokAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(406, 195);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.statusEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Get default values, depending on availability, these can be the current in-game values
            IDictionary<string, decimal> defaultValues = GetDefaultValues();

            // Sections
            statusGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatusGroup", "Status", 5, 5, 123, 106, control: this);
            statsGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatsGroup", "Stats", 134, 5, 124, 153, control: this);
            expGroupBox = WinFormHelpers.CreateCheckGroupBox("editExpGroup", "Level && EXP", 5, 114, 123, 77, control: this);
            sollsGroupBox = WinFormHelpers.CreateCheckGroupBox("editSollsGroup", "Solls", 264, 5, 137, 103, control: this);

            // Status
            WinFormHelpers.CreateLabel("djangoEditHpLabel", "LIFE", 7, 22, 34, 15, statusGroupBox);
            WinFormHelpers.CreateLabel("djangoEditEneLabel", "ENE", 7, 50, 34, 15, statusGroupBox);
            WinFormHelpers.CreateLabel("djangoEditTrcLabel", "TRC", 7, 79, 34, 15, statusGroupBox);

            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_hp", defaultValues["django_current_hp"], 68, 19, 50, 23, maxValue: 1000, control: statusGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_ene", defaultValues["django_current_ene"], 68, 48, 50, 23, maxValue: 1000, control: statusGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_trc", defaultValues["django_current_trc"], 68, 77, 50, 23, maxValue: 1000, control: statusGroupBox));

            // Stats
            WinFormHelpers.CreateLabel("djangoEditVitLabel", "VIT", 7, 39, 27, 15, control: statsGroupBox);
            WinFormHelpers.CreateLabel("djangoEditSprLabel", "SPR", 7, 68, 27, 15, control: statsGroupBox);
            WinFormHelpers.CreateLabel("djangoEditStrLabel", "STR", 7, 97, 27, 15, control: statsGroupBox);
            WinFormHelpers.CreateLabel("djangoEditStatPointsLabel", "Points", 7, 126, 40, 15, control: statsGroupBox);
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

            // Points to allocate
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_stat_points", defaultValues["django_stat_points"], 73, 124, 46, 23, maxValue: 196, control: statsGroupBox));

            // Level & EXP
            WinFormHelpers.CreateLabel("djangoEditLevelLabel", "Level", 2, 22, 34, 15, expGroupBox);
            WinFormHelpers.CreateLabel("djangoEditExpLabel", "EXP", 2, 50, 27, 15, expGroupBox);
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_level", defaultValues["django_level"], 47, 19, 50, 23, control: expGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_exp", defaultValues["django_exp"], 37, 48, 60, 23, minValue: 0, maxValue: 999999, control: expGroupBox));

            // Solls
            WinFormHelpers.CreateLabel("solarStationLbl", "Solar station", 7, 19, 72, 15, sollsGroupBox, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("solarBankLbl", "Solar bank", 7, 47, 72, 15, sollsGroupBox, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("darkLoansLbl", "Dark loans", 7, 76, 72, 15, sollsGroupBox, textAlignment: "MiddleLeft");
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("solls_solar_station", defaultValues["solls_solar_station"], 82, 16, 50, 23, maxValue: 9999, control: sollsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("solls_solar_bank", defaultValues["solls_solar_bank"], 82, 45, 50, 23, maxValue: 9999, control: sollsGroupBox));
            statusNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("solls_dark_loans", defaultValues["solls_dark_loans"], 82, 74, 50, 23, maxValue: 9999, control: sollsGroupBox));

            // Add tooltips & warnings
            Label expWarning = WinFormHelpers.CreateImageLabel("tooltip", "warning", 102, 51, expGroupBox);
            WinFormHelpers.AddToolTip(expWarning, "Level will be automatically adjusted if EXP is high enough to reach higher levels");

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 327, 168, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        #endregion

        #region Values setting

        protected override IDictionary<string, decimal> GetDefaultValues() {

            IDictionary<string, decimal> defaultValues = new Dictionary<string, decimal>();
            uint djangoCurrentHp = _memoryValues.Django["current_hp"].Value;

            // If HP value is valid, get the other in-game values
            if (djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
                defaultValues.Add("django_current_hp", djangoCurrentHp);
                defaultValues.Add("django_current_ene", _memoryValues.Django["current_ene"].Value);
                defaultValues.Add("django_current_trc", _memoryValues.Django["current_trc"].Value);
                defaultValues.Add("django_exp", _memoryValues.Django["exp"].Value);
                defaultValues.Add("django_level", _memoryValues.Django["level"].Value);
                defaultValues.Add("django_base_vit", _memoryValues.Misc["base_vit"].Value);
                defaultValues.Add("django_base_spr", _memoryValues.Misc["base_spr"].Value);
                defaultValues.Add("django_base_str", _memoryValues.Misc["base_str"].Value);
                defaultValues.Add("django_cards_vit", _memoryValues.Misc["cards_vit"].Value);
                defaultValues.Add("django_cards_spr", _memoryValues.Misc["cards_spr"].Value);
                defaultValues.Add("django_cards_str", _memoryValues.Misc["cards_str"].Value);
                defaultValues.Add("django_stat_points", _memoryValues.Django["stat_points"].Value);
                defaultValues.Add("solls_solar_station", _memoryValues.Solls["solar_station"].Value);
                defaultValues.Add("solls_solar_bank", _memoryValues.Solls["solar_bank"].Value);
                defaultValues.Add("solls_dark_loans", _memoryValues.Solls["dark_loans"].Value);
            } else {
                // If HP is unvalid (for example if we are on the title screen or in bike races), use specific values
                defaultValues.Add("django_current_hp", 100);
                defaultValues.Add("django_current_ene", 100);
                defaultValues.Add("django_current_trc", 1000);
                defaultValues.Add("django_exp", 0);
                defaultValues.Add("django_level", 1);
                defaultValues.Add("django_base_vit", 10);
                defaultValues.Add("django_base_spr", 10);
                defaultValues.Add("django_base_str", 10);
                defaultValues.Add("django_cards_vit", 0);
                defaultValues.Add("django_cards_spr", 0);
                defaultValues.Add("django_cards_str", 0);
                defaultValues.Add("django_stat_points", 0);
                defaultValues.Add("solls_solar_station", 0);
                defaultValues.Add("solls_solar_bank", 0);
                defaultValues.Add("solls_dark_loans", 0);
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

            // Sets values based on fields (numericUpDowns)
            for (int i = 0; i < statusNumericUpDowns.Count; i++) {

                // If the field is disabled, skip it
                if (statusNumericUpDowns[i].Enabled == false) {
                    continue;
                }

                decimal value = statusNumericUpDowns[i].Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the input field's name
                 * The first "_" indicates the sublist while the rest indicates the key within that sublist
                 */
                string[] fieldParts = statusNumericUpDowns[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], value);
            }

            /**
             * If the total EXP until next level & current level were available before setting values,
             * we set it to what it should be to reach the next level (except for lvl 99 which is always 0)
             */
            if (_memoryValues.U32.ContainsKey("total_exp_until_next_level") == true && _memoryValues.Django.ContainsKey("level")) {
                int level = (int)_memoryValues.Django["level"].Value;
                _memoryValues.U32["total_exp_until_next_level"].Value = level < 99 ? DjangoExpTable.shinbok[level] : 0;
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
            switch (subList) {
                case "django":
                    if (_memoryValues.Django.ContainsKey(valueKey) == true) {
                        _memoryValues.Django[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
                case "misc":
                    if (_memoryValues.Misc.ContainsKey(valueKey) == true) {
                        switch (valueKey) {
                            case "base_vit":                // Base points from level ups
                            case "base_spr":
                            case "base_str":
                            case "cards_vit":               // Points from cards
                            case "cards_spr":
                            case "cards_str":
                                /**
                                 * Due to how the game works we have to set the sum of base + card stats
                                 * This allows stats to be updated in the current roomand stay when switching room
                                 * 
                                 * We'll start by getting the key needed for the stat
                                 */
                                string[] valueKeyParts = valueKey.Split(['_'], 2);
                                string statKey = valueKeyParts[1];

                                _memoryValues.Misc[valueKey].Value = (uint)value;
                                UpdateSumBaseCardStat(statKey, _memoryValues.Misc["base_" + statKey].Value, _memoryValues.Misc["cards_" + statKey].Value);
                                break;
                            default:                        // Default treatment
                                _memoryValues.Misc[valueKey].Value = (uint)value;
                                break;
                        }
                    } else if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
                case "solls":
                    if (_memoryValues.Solls.ContainsKey(valueKey) == true) {
                        _memoryValues.Solls[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
                default:
                    if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
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

        #endregion
    }
}
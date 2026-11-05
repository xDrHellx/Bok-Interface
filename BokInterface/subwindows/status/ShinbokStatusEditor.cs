using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;
using BokInterface.Tables;
using System.Drawing;

/**
 * Note :
 *
 * Due to how the game works, stat points from accessories cannot be updated here
 * The game checks equipped accessories when switching room
 */

namespace BokInterface.Status;

/// <summary>Status editor for Boktai 3</summary>
class ShinbokStatusEditor : StatusEditor {

    #region Properties

    readonly MemoryValues _memoryValues;
    readonly BokInterface _bokInterface;
    readonly ShinbokAddresses _shinbokAddresses;

    #endregion

    #region Constructor

    public ShinbokStatusEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses shinbokAddresses) {

        _memoryValues = memoryValues;
        _shinbokAddresses = shinbokAddresses;
        Owner = _bokInterface = bokInterface;
        Icon = _bokInterface.Icon;

        SetFormParameters(406, 195);
        AddElements();
        Show();
    }

    #endregion

    #region Elements

    protected override void AddElements() {

        // Get default values, depending on availability, these can be the current in-game values
        IDictionary<string, decimal> defaultValues = GetDefaultValues();

        // Sections
        statusGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatusGroup", "Status", new Point(5, 5), new Size(123, 106), control: this);
        statsGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatsGroup", "Stats", new Point(134, 5), new Size(124, 153), control: this);
        expGroupBox = WinFormHelpers.CreateCheckGroupBox("editExpGroup", WinFormHelpers.EscapeAmpersand("Level & EXP"), new Point(5, 114), new Size(123, 77), control: this);
        sollsGroupBox = WinFormHelpers.CreateCheckGroupBox("editSollsGroup", "Solls", new Point(264, 5), new Size(137, 103), control: this);

        // Status
        WinFormHelpers.CreateLabel("djangoEditHpLabel", "LIFE", new Point(7, 22), new Size(34, 15), statusGroupBox);
        WinFormHelpers.CreateLabel("djangoEditEneLabel", "ENE", new Point(7, 50), new Size(34, 15), statusGroupBox);
        WinFormHelpers.CreateLabel("djangoEditTrcLabel", "TRC", new Point(7, 79), new Size(34, 15), statusGroupBox);

        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_hp", defaultValues["django_current_hp"], new Point(68, 19), new Size(50, 23), maxValue: 1000, control: statusGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_ene", defaultValues["django_current_ene"], new Point(68, 48), new Size(50, 23), maxValue: 1000, control: statusGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_trc", defaultValues["django_current_trc"], new Point(68, 77), new Size(50, 23), maxValue: 1000, control: statusGroupBox));

        // Stats
        WinFormHelpers.CreateLabel("djangoEditVitLabel", "VIT", new Point(7, 39), new Size(27, 15), control: statsGroupBox);
        WinFormHelpers.CreateLabel("djangoEditSprLabel", "SPR", new Point(7, 68), new Size(27, 15), control: statsGroupBox);
        WinFormHelpers.CreateLabel("djangoEditStrLabel", "STR", new Point(7, 97), new Size(27, 15), control: statsGroupBox);
        WinFormHelpers.CreateLabel("djangoEditStatPointsLabel", "Points", new Point(7, 126), new Size(40, 15), control: statsGroupBox);
        WinFormHelpers.CreateLabel("djangoStatsBaseLabel", "Base", new Point(35, 19), new Size(42, 15), control: statsGroupBox, WinFormHelpers.baseStatColor);
        WinFormHelpers.CreateLabel("djangoStatsCardsLabel", "Cards", new Point(77, 19), new Size(42, 15), control: statsGroupBox, WinFormHelpers.cardsStatColor);

        // Base
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_base_vit", defaultValues["django_base_vit"], new Point(39, 37), new Size(38, 23), maxValue: 100, control: statsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_base_spr", defaultValues["django_base_spr"], new Point(39, 66), new Size(38, 23), maxValue: 100, control: statsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_base_str", defaultValues["django_base_str"], new Point(39, 95), new Size(38, 23), maxValue: 100, control: statsGroupBox));

        // Cards
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_cards_vit", defaultValues["django_cards_vit"], new Point(81, 37), new Size(38, 23), maxValue: 100, control: statsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_cards_spr", defaultValues["django_cards_spr"], new Point(81, 66), new Size(38, 23), maxValue: 100, control: statsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("misc_cards_str", defaultValues["django_cards_str"], new Point(81, 95), new Size(38, 23), maxValue: 100, control: statsGroupBox));

        // Points to allocate
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_stat_points", defaultValues["django_stat_points"], new Point(73, 124), new Size(46, 23), maxValue: 196, control: statsGroupBox));

        // Level & EXP
        WinFormHelpers.CreateLabel("djangoEditLevelLabel", "Level", new Point(2, 22), new Size(34, 15), expGroupBox);
        WinFormHelpers.CreateLabel("djangoEditExpLabel", "EXP", new Point(2, 50), new Size(27, 15), expGroupBox);
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_level", defaultValues["django_level"], new Point(47, 19), new Size(50, 23), control: expGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_exp", defaultValues["django_exp"], new Point(37, 48), new Size(60, 23), minValue: 0, maxValue: 999999, control: expGroupBox));

        // Solls
        WinFormHelpers.CreateLabel("solarStationLbl", "Solar station", new Point(7, 19), new Size(72, 15), sollsGroupBox, textAlignment: "MiddleLeft");
        WinFormHelpers.CreateLabel("solarBankLbl", "Solar bank", new Point(7, 47), new Size(72, 15), sollsGroupBox, textAlignment: "MiddleLeft");
        WinFormHelpers.CreateLabel("darkLoansLbl", "Dark loans", new Point(7, 76), new Size(72, 15), sollsGroupBox, textAlignment: "MiddleLeft");
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("solls_solar_station", defaultValues["solls_solar_station"], new Point(82, 16), new Size(50, 23), maxValue: 9999, control: sollsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("solls_solar_bank", defaultValues["solls_solar_bank"], new Point(82, 45), new Size(50, 23), maxValue: 9999, control: sollsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("solls_dark_loans", defaultValues["solls_dark_loans"], new Point(82, 74), new Size(50, 23), maxValue: 9999, control: sollsGroupBox));

        // Add tooltips & warnings
        Label expWarning = WinFormHelpers.CreateImageLabel("tooltip", "warning", new Point(102, 51), expGroupBox);
        WinFormHelpers.AddToolTip(expWarning, "Level will be automatically adjusted if EXP is high enough to reach higher levels");

        AddSetValuesButton(new Point(327, 168), this);
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
        for (int i = 0; i < numericUpDowns.Count; i++) {

            // If the field is disabled, skip it
            if (numericUpDowns[i].Enabled == false) {
                continue;
            }

            decimal value = numericUpDowns[i].Value;

            /**
             * Indicate which sublist to use for setting the value, based on the input field's name
             * The first "_" indicates the sublist while the rest indicates the key within that sublist
             */
            string[] fieldParts = numericUpDowns[i].Name.Split(['_'], 2);
            SetMemoryValue(fieldParts[0], fieldParts[1], value);
        }

        /**
         * If the total EXP until next level & current level were available before setting values,
         * we set it to what it should be to reach the next level (except for lvl 99 which is always 0)
         */
        if (_memoryValues.U32.ContainsKey("total_exp_until_next_level") == true && _memoryValues.Django.ContainsKey("level")) {
            int level = (int)_memoryValues.Django["level"].Value;
            _memoryValues.U32["total_exp_until_next_level"].Value = level < 99 ? BokTables.djangoExp[level] : 0;
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
    void SetMemoryValue(string subList, string valueKey, decimal value) {
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
                            UpdateSumBaseCardStat(statKey, _memoryValues.Misc[$"base_{statKey}"].Value, _memoryValues.Misc[$"cards_{statKey}"].Value);
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
    void UpdateSumBaseCardStat(string stat, uint basePoints, uint cardPoints) {
        string key = $"sum_base_cards_{stat}";
        if (_memoryValues.Django.ContainsKey(key) == true) {
            _memoryValues.Django[key].Value = basePoints + cardPoints;
        }
    }

    #endregion
}

using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;
using BokInterface.Tables;
using System.Drawing;

namespace BokInterface.Status;

/// <summary>Status editor for Boktai 2</summary>
class ZoktaiStatusEditor : StatusEditor {

    #region Properties

    readonly MemoryValues _memoryValues;
    readonly BokInterface _bokInterface;
    readonly ZoktaiAddresses _zoktaiAddresses;
    protected readonly List<CheckBox> statusCheckBoxes = [];
    protected CheckGroupBox kaamosGroupBox = new();

    #endregion

    #region Constructor

    public ZoktaiStatusEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

        _memoryValues = memoryValues;
        _zoktaiAddresses = zoktaiAddresses;
        Owner = _bokInterface = bokInterface;
        Icon = _bokInterface.Icon;

        SetFormParameters(447, 279);
        AddElements();
        Show();
    }

    #endregion

    #region Elements

    protected override void AddElements() {

        // Get default values, depending on availability, these can be the current in-game values
        IDictionary<string, decimal> defaultValues = GetDefaultValues();

        // Sections
        statusGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatusGroup", "Status", new Point(5, 5), new Size(97, 77), control: this);
        skillGroupBox = WinFormHelpers.CreateCheckGroupBox("editSkillGroup", "Skill", new Point(110, 85), new Size(203, 106), control: this);
        statsGroupBox = WinFormHelpers.CreateCheckGroupBox("editStatsGroup", "Stats", new Point(5, 85), new Size(99, 164), control: this);
        expGroupBox = WinFormHelpers.CreateCheckGroupBox("editExpGroup", WinFormHelpers.EscapeAmpersand("Level & EXP"), new Point(108, 5), new Size(118, 77), control: this);
        kaamosGroupBox = WinFormHelpers.CreateCheckGroupBox("editKaamosGroup", "Kaamos", new Point(232, 5), new Size(81, 68), control: this);
        sollsGroupBox = WinFormHelpers.CreateCheckGroupBox("editSollsGroup", "Solls", new Point(322, 5), new Size(137, 103), control: this);

        // Status
        WinFormHelpers.CreateLabel("djangoEditHpLabel", "LIFE", new Point(2, 22), new Size(34, 15), statusGroupBox);
        WinFormHelpers.CreateLabel("djangoEditEneLabel", "ENE", new Point(2, 50), new Size(34, 15), statusGroupBox);

        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_hp", defaultValues["django_current_hp"], new Point(42, 20), new Size(50, 23), maxValue: 1000, control: statusGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_current_ene", defaultValues["django_current_ene"], new Point(42, 48), new Size(50, 23), maxValue: 1000, control: statusGroupBox));

        // Skill
        WinFormHelpers.CreateLabel("djangoEditSwordSkillLabel", "Sword", new Point(2, 22), new Size(54, 15), skillGroupBox, textAlignment: "MiddleLeft");
        WinFormHelpers.CreateLabel("djangoEditSpearSkillLabel", "Spear", new Point(110, 22), new Size(36, 15), skillGroupBox, textAlignment: "MiddleLeft");
        WinFormHelpers.CreateLabel("djangoEditHammerSkillLabel", "Hammer", new Point(2, 50), new Size(54, 15), skillGroupBox, textAlignment: "MiddleLeft");
        WinFormHelpers.CreateLabel("djangoEditFistsSkillLabel", "Fists", new Point(110, 50), new Size(36, 15), skillGroupBox, textAlignment: "MiddleLeft");
        WinFormHelpers.CreateLabel("djangoEditGunSkillLabel", "Gun", new Point(2, 79), new Size(54, 15), skillGroupBox, textAlignment: "MiddleLeft");

        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_sword_skill", defaultValues["django_sword_skill"], new Point(56, 19), new Size(51, 23), nbDecimals: 2, control: skillGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_spear_skill", defaultValues["django_spear_skill"], new Point(147, 19), new Size(51, 23), nbDecimals: 2, control: skillGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_hammer_skill", defaultValues["django_hammer_skill"], new Point(56, 48), new Size(51, 23), nbDecimals: 2, control: skillGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_fists_skill", defaultValues["django_fists_skill"], new Point(147, 48), new Size(51, 23), nbDecimals: 2, control: skillGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_gun_skill", defaultValues["django_gun_skill"], new Point(56, 77), new Size(51, 23), nbDecimals: 2, control: skillGroupBox));

        // Stats
        WinFormHelpers.CreateLabel("djangoEditVitLabel", "VIT", new Point(2, 22), new Size(27, 15), statsGroupBox);
        WinFormHelpers.CreateLabel("djangoEditSprLabel", "SPR", new Point(2, 50), new Size(27, 15), statsGroupBox);
        WinFormHelpers.CreateLabel("djangoEditStrLabel", "STR", new Point(2, 79), new Size(27, 15), statsGroupBox);
        WinFormHelpers.CreateLabel("djangoEditAgiLabel", "AGI", new Point(2, 108), new Size(27, 15), statsGroupBox);
        WinFormHelpers.CreateLabel("djangoEditStatPointsLabel", "Points", new Point(2, 137), new Size(40, 15), statsGroupBox);

        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_vit", defaultValues["django_vit"], new Point(53, 19), new Size(41, 23), control: statsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_spr", defaultValues["django_spr"], new Point(53, 48), new Size(41, 23), control: statsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_str", defaultValues["django_str"], new Point(53, 77), new Size(41, 23), control: statsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_agi", defaultValues["django_agi"], new Point(53, 106), new Size(41, 23), control: statsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_stat_points", defaultValues["django_stat_points"], new Point(48, 135), new Size(46, 23), minValue: 0, maxValue: 255, control: statsGroupBox));

        // Level & EXP
        WinFormHelpers.CreateLabel("djangoEditLevelLabel", "Level", new Point(2, 22), new Size(34, 15), expGroupBox);
        WinFormHelpers.CreateLabel("djangoEditExpLabel", "EXP", new Point(2, 50), new Size(27, 15), expGroupBox);

        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_level", defaultValues["django_level"], new Point(42, 19), new Size(50, 23), control: expGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("django_exp", defaultValues["django_exp"], new Point(32, 48), new Size(60, 23), minValue: 0, maxValue: 999999, control: expGroupBox));

        // Kaamos
        statusCheckBoxes.Add(WinFormHelpers.CreateCheckBox("django_kaamos", "Django", new Point(10, 25), new Size(64, 19), isCheckedByDefault: defaultValues["django_kaamos"] > 0, control: kaamosGroupBox));
        statusCheckBoxes.Add(WinFormHelpers.CreateCheckBox("sabata_kaamos", "Sabata", new Point(10, 47), new Size(64, 19), isCheckedByDefault: defaultValues["sabata_kaamos"] > 0, control: kaamosGroupBox));

        // Add tooltips & warnings
        Label expWarning = WinFormHelpers.CreateImageLabel("tooltip", "warning", new Point(97, 51), expGroupBox);
        WinFormHelpers.AddToolTip(expWarning, "Level will be automatically adjusted if EXP is high enough to reach higher levels");

        // Solls
        WinFormHelpers.CreateLabel("solarStationLbl", "Solar station", new Point(7, 19), new Size(72, 15), sollsGroupBox, textAlignment: "MiddleLeft");
        WinFormHelpers.CreateLabel("solarBankLbl", "Solar bank", new Point(7, 47), new Size(72, 15), sollsGroupBox, textAlignment: "MiddleLeft");
        WinFormHelpers.CreateLabel("darkLoansLbl", "Dark loans", new Point(7, 76), new Size(72, 15), sollsGroupBox, textAlignment: "MiddleLeft");
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("solls_solar_station", defaultValues["solls_solar_station"], new Point(82, 16), new Size(50, 23), maxValue: 9999, control: sollsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("solls_solar_bank", defaultValues["solls_solar_bank"], new Point(82, 45), new Size(50, 23), maxValue: 9999, control: sollsGroupBox));
        numericUpDowns.Add(WinFormHelpers.CreateNumericUpDown("solls_dark_loans", defaultValues["solls_dark_loans"], new Point(82, 74), new Size(50, 23), maxValue: 9999, control: sollsGroupBox));

        AddSetValuesButton(new Point(385, 252), this);
    }

    #endregion

    #region Values setting

    protected override IDictionary<string, decimal> GetDefaultValues() {

        IDictionary<string, decimal> defaultValues = new Dictionary<string, decimal>();
        uint currentStat = APIs.Memory.ReadU32(_zoktaiAddresses.Misc["current_stat"].Address);

        // If "current stat" is a valid value
        if (currentStat > 0) {
            defaultValues.Add("django_current_hp", _memoryValues.Django["current_hp"].Value);
            defaultValues.Add("django_current_ene", _memoryValues.Django["current_ene"].Value);

            defaultValues.Add("django_exp", _memoryValues.Django["exp"].Value);
            defaultValues.Add("django_level", _memoryValues.Django["level"].Value);

            defaultValues.Add("django_vit", _memoryValues.Django["vit"].Value);
            defaultValues.Add("django_spr", _memoryValues.Django["spr"].Value);
            defaultValues.Add("django_str", _memoryValues.Django["str"].Value);
            defaultValues.Add("django_agi", _memoryValues.Django["agi"].Value);
            defaultValues.Add("django_stat_points", _memoryValues.Django["stat_points"].Value);

            defaultValues.Add("django_sword_skill", Utilities.ExpToLevel(_memoryValues.Django["sword_skill"].Value));
            defaultValues.Add("django_spear_skill", Utilities.ExpToLevel(_memoryValues.Django["spear_skill"].Value));
            defaultValues.Add("django_hammer_skill", Utilities.ExpToLevel(_memoryValues.Django["hammer_skill"].Value));
            defaultValues.Add("django_fists_skill", Utilities.ExpToLevel(_memoryValues.Django["fists_skill"].Value));
            defaultValues.Add("django_gun_skill", Utilities.ExpToLevel(_memoryValues.Django["gun_skill"].Value));

            defaultValues.Add("django_kaamos", _memoryValues.Django["kaamos"].Value);
            defaultValues.Add("sabata_kaamos", _memoryValues.Sabata["kaamos"].Value);

            defaultValues.Add("solls_solar_station", _memoryValues.Solls["solar_station"].Value);
            defaultValues.Add("solls_solar_bank", _memoryValues.Solls["solar_bank"].Value);
            defaultValues.Add("solls_dark_loans", _memoryValues.Solls["dark_loans"].Value);
        } else {
            // If current stat is unvalid (for example because we are on the title screen or in a room transition), use specific values
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

            defaultValues.Add("django_kaamos", 0);
            defaultValues.Add("sabata_kaamos", 0);

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

        // Sets values for each field
        for (int i = 0; i < numericUpDowns.Count; i++) {

            // If the slot is disabled, skip it
            if (numericUpDowns[i].Enabled == false) {
                continue;
            }

            /**
             * Indicate which sublist to use for setting the value, based on the slot's name
             * We only split on the first "_"
             */
            string[] fieldParts = numericUpDowns[i].Name.Split(['_'], 2);
            SetMemoryValue(fieldParts[0], fieldParts[1], numericUpDowns[i].Value);
        }

        // Repeat the process above for each checkbox
        for (int i = 0; i < statusCheckBoxes.Count; i++) {

            if (statusCheckBoxes[i].Enabled == false) {
                continue;
            }

            /**
             * For now we just do a simple set 0 or 1 value
             * This might need to be changed later as more is learned, for now it works
             */
            string[] fieldParts = statusCheckBoxes[i].Name.Split(['_'], 2);
            SetMemoryValue(fieldParts[0], fieldParts[1], statusCheckBoxes[i].Checked ? 1 : 0);
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

                    // Depending on the key, we treat the value setting differently
                    switch (valueKey) {
                        case "vit":                     // Stats
                        case "spr":
                        case "str":
                        case "agi":
                            /**
                             * For stats we also update the "persistent" stat address
                             *
                             * We do this because updating "current" stat value is not enough,
                             * when switching room the game would set back the old values
                             */
                            _memoryValues.Django[valueKey].Value = (uint)value;
                            if (_memoryValues.Misc.ContainsKey(valueKey) == true) {
                                _memoryValues.Misc[valueKey].Value = (uint)value;
                            }
                            break;
                        case "sword_skill":             // Skill
                        case "spear_skill":
                        case "hammer_skill":
                        case "fists_skill":
                        case "gun_skill":
                            _memoryValues.Django[valueKey].Value = Utilities.LevelToExp(value);
                            break;
                        default:                        // Default treatment
                            _memoryValues.Django[valueKey].Value = (uint)value;
                            break;
                    }

                } else if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                    _memoryValues.U16[valueKey].Value = (uint)value;
                } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                    _memoryValues.U32[valueKey].Value = (uint)value;
                }
                break;
            case "sabata":
                if (_memoryValues.Sabata.ContainsKey(valueKey) == true) {
                    _memoryValues.Sabata[valueKey].Value = (uint)value;
                } else if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                    _memoryValues.U16[valueKey].Value = (uint)value;
                } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                    _memoryValues.U32[valueKey].Value = (uint)value;
                }
                break;
            case "misc":
                if (_memoryValues.Misc.ContainsKey(valueKey) == true) {
                    _memoryValues.Misc[valueKey].Value = (uint)value;
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

    #endregion
}

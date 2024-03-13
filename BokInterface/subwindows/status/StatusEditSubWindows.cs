using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.All;
using BokInterface.ExpTables;

/**
 * Main file for status editing subwindows
 */

namespace BokInterface {
    partial class BokInterface {

        #region Properties for subwindow elements

        private CheckGroupBox _edit_statusGroupBox = new();
        private CheckGroupBox _edit_statsGroupBox = new();
        private readonly List<Label> _edit_statusLabels = [];
        private readonly List<NumericUpDown> _edit_statusNumericUpDowns = [];

        #endregion

        #region General status edit subwindow methods

        /// <summary>Clears the Status editing subwindow and all other sections within it</summary>
        private void ClearStatusEditControls() {
            _edit_statusGroupBox.Controls.Clear();
            _edit_statsGroupBox.Controls.Clear();
            _edit_statusLabels.Clear();
            _edit_statusNumericUpDowns.Clear();
        }

        /// <summary>
        /// <para>Get default values for the "Edit Status" subwindow</para>
        /// <para>Default values are the current in-games values unless these are either invalid or unavailable</para>
        /// <para>For example Django's HP is an unvalid value on the title screen</para>
        /// </summary>
        /// <returns><c>IDictionary</c>Dictionnary of key => values pairs</returns>
        private IDictionary<string, decimal> GetDefaultStatusValues() {

            // Add default values according to current game
            return shorterGameName switch {
                "Boktai" => GetBoktaiDefaultValues(),
                "Zoktai" => GetZoktaiDefaultValues(),
                "Shinbok" => GetShinbokDefaultValues(),
                "LunarKnights" => GetLunarKnightsDefaultValues(),
                _ => new Dictionary<string, decimal>(),
            };
        }

        /// <summary>Sets values related to Django's status</summary>
        private void SetStatusValues() {

            // Retrieve all input fields
            List<NumericUpDown> fields = _edit_statusNumericUpDowns;

            // Store the previous setting for BizHawk being paused
            _previousIsPauseSetting = APIs.Client.IsPaused();

            /**
             * If the total EXP until next level & current level are available,
             * we'll use these to prevent the game from adjusting the level while setting new values
             * 
             * We'll set the total EXP until next level to the maximum possible to prevent that from happening
             */
            if (memoryValues.U32.ContainsKey("total_exp_until_next_level") == true) {
                memoryValues.U32["total_exp_until_next_level"].Value = 99999999;
            }

            // Pause BizHawk
            APIs.Client.Pause();

            // Sets values based on fields for the current game
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

                            // Specific treatment for stats in Bok 2
                            switch (memoryValueKey) {
                                case "vit":
                                case "spr":
                                case "str":
                                case "agi":
                                    ZoktaiUpdateStats(memoryValueKey, (uint)value);
                                    break;
                                default:
                                    // Do nothing
                                    break;
                            }
                        } else if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            _memoryValues.U16[memoryValueKey].Value = memoryValueKey switch {
                                "sword_skill" or "spear_skill" or "hammer_skill" or "fists_skill" or "gun_skill" => Utilities.LevelToExp(value),
                                _ => (uint)value
                            };
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
             * If the total EXP until next level & current level were available before setting values,
             * we set it to what it should be to reach the next level (except for lvl 99 which is always 0)
             */
            if (_memoryValues.U32.ContainsKey("total_exp_until_next_level") == true && _memoryValues.U16.ContainsKey("level")) {
                int level = (int)_memoryValues.U16["level"].Value;
                _memoryValues.U32["total_exp_until_next_level"].Value = level < 99 ? Django.zoktai[level] : 0;
            }

            /**
             * If BizHawk was not paused before setting values, unpause it
             * Otherwise keep it paused
             */
            if (_previousIsPauseSetting == true) {
                APIs.Client.Unpause();
            }
        }

        #endregion
    }
}

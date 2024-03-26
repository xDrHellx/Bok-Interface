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

        private CheckGroupBox edit_statusGroupBox = new();
        private CheckGroupBox edit_statsGroupBox = new();
        private readonly List<Label> edit_statusLabels = [];
        private readonly List<NumericUpDown> edit_statusNumericUpDowns = [];

        #endregion

        #region General status edit subwindow methods

        /// <summary>Clears the Status editing subwindow and all other sections within it</summary>
        private void ClearStatusEditControls() {
            edit_statusGroupBox.Controls.Clear();
            edit_statsGroupBox.Controls.Clear();
            edit_statusLabels.Clear();
            edit_statusNumericUpDowns.Clear();
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
            List<NumericUpDown> fields = edit_statusNumericUpDowns;

            // Store the previous setting for BizHawk being paused
            previousIsPauseSetting = APIs.Client.IsPaused();

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
                        if (memoryValues.Django.ContainsKey(memoryValueKey) == true) {

                            // Depending on the key, we treat the value setting differently
                            switch (memoryValueKey) {
                                case "vit":                     // Stats
                                case "spr":
                                case "str":
                                case "agi":
                                    memoryValues.Django[memoryValueKey].Value = (uint)value;
                                    ZoktaiUpdateStats(memoryValueKey, (uint)value);
                                    break;
                                case "sword_skill":             // Skill
                                case "spear_skill":
                                case "hammer_skill":
                                case "fists_skill":
                                case "gun_skill":
                                    memoryValues.Django[memoryValueKey].Value = Utilities.LevelToExp(value);
                                    break;
                                default:                        // Default treatment
                                    memoryValues.Django[memoryValueKey].Value = (uint)value;
                                    break;
                            }

                        } else if (memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U16[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U32[memoryValueKey].Value = (uint)value;
                        }
                        break;
                    case "solls":
                        if (memoryValues.Solls.ContainsKey(memoryValueKey) == true) {
                            memoryValues.Solls[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U16[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U32[memoryValueKey].Value = (uint)value;
                        }
                        break;
                    case "bike":
                        if (memoryValues.Bike.ContainsKey(memoryValueKey) == true) {
                            memoryValues.Bike[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U16[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U32[memoryValueKey].Value = (uint)value;
                        }
                        break;
                    case "misc":
                        if (memoryValues.Misc.ContainsKey(memoryValueKey) == true) {
                            memoryValues.Misc[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U16[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U32[memoryValueKey].Value = (uint)value;
                        }
                        break;
                    default:
                        if (memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U16[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U32[memoryValueKey].Value = (uint)value;
                        }
                        break;
                }
            }

            /**
             * If the total EXP until next level & current level were available before setting values,
             * we set it to what it should be to reach the next level (except for lvl 99 which is always 0)
             */
            if (memoryValues.U32.ContainsKey("total_exp_until_next_level") == true && memoryValues.Django.ContainsKey("level")) {
                int level = (int)memoryValues.Django["level"].Value;
                memoryValues.U32["total_exp_until_next_level"].Value = level < 99 ? Django.zoktai[level] : 0;
            }

            /**
             * If BizHawk was not paused before setting values, unpause it
             * Otherwise keep it paused
             */
            if (previousIsPauseSetting == true) {
                APIs.Client.Unpause();
            }
        }

        #endregion
    }
}

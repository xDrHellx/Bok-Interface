using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.All;

/**
 * Main file for status editing subwindows
 */

namespace BokInterface {
    partial class BokInterfaceMainForm {

        #region Properties for subwindow elements

<<<<<<< HEAD
        private GroupBox edit_statusGroupBox = new();
        private GroupBox edit_statsGroupBox = new();
        private readonly List<Label> edit_statusLabels = new();
        private readonly List<NumericUpDown> edit_statusNumericUpDowns = new();
=======
        private System.Windows.Forms.GroupBox edit_statusGroupBox = new();
        private System.Windows.Forms.GroupBox edit_statsGroupBox = new();
        private List<System.Windows.Forms.Label> edit_statusLabels = [];
        private List<System.Windows.Forms.NumericUpDown> edit_statusNumericUpDowns = [];
>>>>>>> 11184bc (add new style rules and partially apply them)

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
            switch (shorterGameName) {
                case "Boktai":
                    return GetBoktaiDefaultValues();
                case "Zoktai":
                    return GetZoktaiDefaultValues();
                case "Shinbok":
                    return GetShinbokDefaultValues();
                case "LunarKnights":
                    return GetLunarKnightsDefaultValues();
                default:
                    return new Dictionary<string, decimal>();
            }
        }

        /// <summary>Sets values related to Django's status</summary>
        private void SetStatusValues() {

            // Retrieve all input fields
            List<NumericUpDown> fields = edit_statusNumericUpDowns;

            // Sets values based on fields for the current game
            for (int i = 0; i < fields.Count; i++) {
                decimal value = fields[i].Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the input field's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = fields[i].Name.Split(new char[] { '_' }, 2);
                string subList = fieldParts[0];
                string memoryValueKey = fieldParts[1];
                switch (subList) {
                    case "django":
                        if (memoryValues.Django.ContainsKey(memoryValueKey) == true) {
                            memoryValues.Django[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            switch (memoryValueKey) {
                                case "sword_skill":
                                case "spear_skill":
                                case "hammer_skill":
                                case "fists_skill":
                                case "gun_skill":
                                    memoryValues.U16[memoryValueKey].Value = Utilities.LevelToExp(value);
                                    break;
                                default:
                                    memoryValues.U16[memoryValueKey].Value = (uint)value;
                                    break;
                            }
                        }
                        break;
                    case "solls":
                        if (memoryValues.Solls.ContainsKey(memoryValueKey) == true) {
                            memoryValues.Solls[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U16[memoryValueKey].Value = (uint)value;
                        }
                        break;
                    case "bike":
                        if (memoryValues.Bike.ContainsKey(memoryValueKey) == true) {
                            memoryValues.Bike[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U16[memoryValueKey].Value = (uint)value;
                        }
                        break;
                    case "misc":
                        if (memoryValues.Misc.ContainsKey(memoryValueKey) == true) {
                            memoryValues.Misc[memoryValueKey].Value = (uint)value;
                        } else if (memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U16[memoryValueKey].Value = (uint)value;
                        }
                        break;
                    default:
                        if (memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            memoryValues.U16[memoryValueKey].Value = (uint)value;
                        }
                        break;
                }
            }
        }

        #endregion
    }
}

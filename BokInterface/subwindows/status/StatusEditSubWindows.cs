using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.All;

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

            // Pause BizHawk
            APIs.Client.Pause();

            // Add default values according to current game
            switch (shorterGameName) {
                case "Boktai":
                    SetBoktaiStatusValues(fields);
                    break;
                case "Zoktai":
                    SetZoktaiStatusValues(fields);
                    break;
                case "Shinbok":
                    SetShinbokStatusValues(fields);
                    break;
                case "LunarKnights":
                    SetLunarKnightsStatusValues(fields);
                    break;
                default:
                    // Do nothing, game is not handled
                    break;
            };

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

using System.Windows.Forms;

using BokInterface.Addresses;

/**
 * File for the Zoktai (Boktai 2) interface itself
 */

namespace BokInterface {

    partial class BokInterfaceMainForm {

        #region Properties

        private Label bok2_currentStatusHpValue = new();
        private Label bok2_currentStatusEneValue = new();
        private Label bok2_djangoBaseVit = new();
        private Label bok2_djangoBaseSpr = new();
        private Label bok2_djangoBaseStr = new();
        private Label bok2_djangoBaseAgi = new();

        private readonly ZoktaiAddresses zoktaiAddresses = new();

        #endregion

        private void ShowZoktaiInterface() {

            // Current game name
            CreateLabel("currentGameName", currentGameName, 5, 5, 145, 20, true);

            // Current status section
            AddZoktaiCurrentStatusSection();

            // Stats section
            AddZoktaiCurrentStatsSection();

            // Extras / misc tools section
            AddToolsSection();

            // Main window
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 345, 500);

            ResumeLayout(false);
        }

        private void UpdateZoktaiInterface() {

        }

        private void AddZoktaiCurrentStatusSection() {

            // Section
            currentStatusGroupBox = CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 55, true);

            // Current status labels
            currentStatusLabels.Add(CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15));
            currentStatusLabels.Add(CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15));

            // Current status values
            bok2_currentStatusHpValue = CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15);
            bok2_currentStatusEneValue = CreateLabel("djangoCurrentHpValue", "", 44, 34, 31, 15);

            // Add values labels to group
            currentStatusLabels.Add(bok2_currentStatusHpValue);
            currentStatusLabels.Add(bok2_currentStatusEneValue);

            // Add elements to group
            for (int i = 0; i < currentStatusLabels.Count; i++) {
                currentStatusGroupBox.Controls.Add(currentStatusLabels[i]);
            }
        }

        private void AddZoktaiCurrentStatsSection() {

            // Section
            currentStatsGroupBox = CreateGroupBox("currentStats", "Stats", 5, 86, 75, 90, true);

            // VIT
            currentStatsLabels.Add(CreateLabel("vitRowLabel", "VIT", 6, 19, 27, 15));
            bok2_djangoBaseVit = CreateLabel("djangoBaseVit", "", 35, 19, 31, 15, colorHex: baseStatColor, textAlignment: "MiddleRight");

            // SPR
            currentStatsLabels.Add(CreateLabel("sprRowLabel", "SPR", 6, 34, 27, 15));
            bok2_djangoBaseSpr = CreateLabel("djangoBaseSpr", "", 35, 34, 31, 15, colorHex: baseStatColor, textAlignment: "MiddleRight");

            // STR
            currentStatsLabels.Add(CreateLabel("strRowLabel", "STR", 6, 49, 27, 15));
            bok2_djangoBaseStr = CreateLabel("djangoBaseStr", "", 35, 49, 31, 15, colorHex: baseStatColor, textAlignment: "MiddleRight");

            // AGI
            currentStatsLabels.Add(CreateLabel("strRowLabel", "AGI", 6, 64, 27, 15));
            bok2_djangoBaseAgi = CreateLabel("djangoBaseAgi", "", 35, 64, 31, 15, colorHex: baseStatColor, textAlignment: "MiddleRight");

            // Add values labels to group
            currentStatsLabels.Add(bok2_djangoBaseVit);
            currentStatsLabels.Add(bok2_djangoBaseSpr);
            currentStatsLabels.Add(bok2_djangoBaseStr);
            currentStatsLabels.Add(bok2_djangoBaseAgi);

            // Add elements to group
            for (int i = 0; i < currentStatsLabels.Count; i++) {
                currentStatsGroupBox.Controls.Add(currentStatsLabels[i]);
            }
        }
    }
}

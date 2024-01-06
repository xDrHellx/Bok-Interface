using System;
using System.Collections.Generic;
using BizHawk.Client.EmuHawk;
using BokInterface.LunarKnights;

/**
 * File for the Boktai DS / Lunar Knights interface itself
 */

namespace BokInterface {
	
	partial class BokInterfaceMainForm {

		private readonly LunarKnightsAddresses lunarKnightsAddresses = new();

        private void ShowLunarKnightsInterface() {
			
			// Current game name
			this.CreateLabel("currentGameName", currentGameName, 5, 5, 141, 20, true);

			// Current status section
			this.AddLunarKnightsCurrentStatusSection();

			// Extras / misc tools section
			this.AddToolsSection();
			
			// Main window
			this.SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 345, 500);
			
			this.ResumeLayout(false);
        }

		private void UpdateLunarKnightsInterface() {

		}

		private void AddLunarKnightsCurrentStatusSection() {
			
			// Section
			this.currentStatusGroupBox = this.CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 70, true);
		}
    }
}
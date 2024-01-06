using System;
using System.Windows.Forms;
using BokInterface.Tools;

/**
 * Main file for tools selection subwindows
 */

namespace BokInterface {
    partial class BokInterfaceMainForm {

        private void BoktaiToolsSubwindow() {

            System.Windows.Forms.Label availableToolsLabel = this.CreateLabel("availableToolsLabel", "-- Tools available --", 5, 5, 176, 15);
            this.miscToolsSelectionWindow.Controls.Add(availableToolsLabel);
        }

        private void ZoktaiToolsSubwindow() {

        }

        private void ShinbokToolsSubwindow() {

        }

        private void LunarKnightsToolsSubwindow() {
            
        }
    }
}
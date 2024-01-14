using System;
using System.Windows.Forms;

/**
 * Main file for subwindows
 */

namespace BokInterface {
    partial class BokInterfaceMainForm {
    
        #region Variables indicating if specific subwindows are activated or not

        protected bool statusEditing = false;
        protected bool inventoryEditing = false;
        protected bool equipsEditing = false;
        protected bool solarGunEditing = false;
        protected bool weaponsEditing = false;
        protected bool magicsEditing = false;
        protected bool miscToolsSelecting = false;

        #endregion

        #region Subwindows activation methods
        protected void OpenStatusEditor(object sender, EventArgs e) {
            if(this.statusEditing == false) {

                // Create subwindow & add on close event
                this.statusEditWindow = CreateSubWindow("statusEditWindow", "Bok Edit - Status", 203, 144);
                this.statusEditWindow.FormClosing += new FormClosingEventHandler(delegate(object sender, FormClosingEventArgs e) {
                    this.statusEditing = false;
                    this.statusEditWindow.Controls.Clear();
                });

                // Clears subwindow
                ClearStatusEditControls();

                // Add subwindow elements corresponding to the current game
                switch(shorterGameName) {
                    case "Boktai":
                        BoktaiStatusEditSubwindow();
                        break;
                    case "Zoktai":
                        ZoktaiStatusEditSubwindow();
                        break;
                    case "Shinbok":
                        ShinbokStatusEditSubwindow();
                        break;
                    case "LunarKnights":
                        LunarKnightsStatusEditSubwindow();
                        break;
                    default:
                        // If game is not handled, stop
                        return;
                }

                // statusEditWindow.ShowDialog(); // focus & stops BizHawk
                this.statusEditWindow.Show(); // focus & let BizHawk continue
                this.statusEditing = true;
            }
        }

        protected void OpenInventoryEditor(object sender, EventArgs e) {
            if(this.inventoryEditing == false) {

                // Create subwindow & add on close event
                this.inventoryEditWindow = CreateSubWindow("inventoryEditWindow", "Bok Edit - Inventory", 200, 100);
                this.inventoryEditWindow.FormClosing += new FormClosingEventHandler(delegate(object sender, FormClosingEventArgs e) {
                    this.inventoryEditing = false;
                    this.inventoryEditWindow.Controls.Clear();
                });

                this.inventoryEditWindow.Show();
                this.inventoryEditing = true;
            }
        }

        protected void OpenEquipsEditor(object sender, EventArgs e) {
            if(this.equipsEditing == false) {

                // Create subwindow & add on close event
                this.equipsEditWindow = CreateSubWindow("equipsEditWindow", "Bok Edit - Equips", 200, 100);
                this.equipsEditWindow.FormClosing += new FormClosingEventHandler(delegate(object sender, FormClosingEventArgs e) {
                    this.equipsEditing = false;
                    this.equipsEditWindow.Controls.Clear();
                });

                this.equipsEditWindow.Show();
                this.equipsEditing = true;
            }
        }

        protected void OpenSolarGunEditor(object sender, EventArgs e) {
            if(this.solarGunEditing == false) {

                // Create subwindow & add on close event
                this.solarGunEditWindow = CreateSubWindow("solarGunEditWindow", "Bok Edit - Solar Gun", 200, 100);
                this.solarGunEditWindow.FormClosing += new FormClosingEventHandler(delegate(object sender, FormClosingEventArgs e) {
                    this.solarGunEditing = false;
                    this.solarGunEditWindow.Controls.Clear();
                });

                this.solarGunEditWindow.Show();
                this.solarGunEditing = true;
            }
        }

        protected void OpenWeaponsEditor(object sender, EventArgs e) {
            if(this.weaponsEditing == false) {

                // Create subwindow & add on close event
                this.weaponsEditWindow = CreateSubWindow("weaponsEditWindow", "Bok Edit - Weapons", 200, 100);
                this.weaponsEditWindow.FormClosing += new FormClosingEventHandler(delegate(object sender, FormClosingEventArgs e) {
                    this.weaponsEditing = false;
                    this.weaponsEditWindow.Controls.Clear();
                });

                this.weaponsEditWindow.Show();
                this.weaponsEditing = true;
            }
        }

        protected void OpenMagicsEditor(object sender, EventArgs e) {
            if(this.magicsEditing == false) {

                // Create subwindow & add on close event
                this.magicsEditWindow = CreateSubWindow("magicsEditWindow", "Bok Edit - Magics", 200, 100);
                this.magicsEditWindow.FormClosing += new FormClosingEventHandler(delegate(object sender, FormClosingEventArgs e) {
                    this.magicsEditing = false;
                    this.magicsEditWindow.Controls.Clear();
                });

                this.magicsEditWindow.Show();
                this.magicsEditing = true;
            }
        }

        protected void OpenMiscToolsSelection(object sender, EventArgs e) {
            if(this.miscToolsSelecting == false) {

                // Create subwindow & add on close event
                this.miscToolsSelectionWindow = CreateSubWindow("miscToolsSelectWindow", "Bok Tools - Select", 200, 100);
                this.miscToolsSelectionWindow.FormClosing += new FormClosingEventHandler(delegate(object sender, FormClosingEventArgs e) {
                    this.miscToolsSelecting = false;
                    this.miscToolsSelectionWindow.Controls.Clear();
                });

                // Add subwindow elements corresponding to the current game
                switch(shorterGameName) {
                    case "Boktai":
                        BoktaiToolsSubwindow();
                        break;
                    case "Zoktai":
                        ZoktaiToolsSubwindow();
                        break;
                    case "Shinbok":
                        ShinbokToolsSubwindow();
                        break;
                    case "LunarKnights":
                        LunarKnightsToolsSubwindow();
                        break;
                    default:
                        // If game is not handled, stop
                        return;
                }

                this.miscToolsSelectionWindow.Show();
                this.miscToolsSelecting = true;
            }
        }

        #endregion
    }
}
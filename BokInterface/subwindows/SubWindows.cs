using System;
using System.Windows.Forms;

using BokInterface.All;
using BokInterface.Status;

/**
 * Main file for subwindows
 */

namespace BokInterface {
    partial class BokInterface {

        #region Properties indicating if subwindows are opened or not

        public bool statusEditorOpened = false;
        public bool inventoryEditorOpened = false;
        public bool equipsEditorOpened = false;
        public bool solarGunEditorOpened = false;
        public bool weaponsEditorOpened = false;
        public bool magicsEditorOpened = false;
        public bool miscToolsSelectorOpened = false;

        #endregion

        #region Subwindows generation methods
        protected void OpenStatusEditor(object sender, EventArgs e) {
            if (statusEditorOpened == false) {

                // Add subwindow elements corresponding to the current game
                switch (shorterGameName) {
                    case "Boktai":
                        _statusEditorInstance = new BoktaiStatusEditor(this, _memoryValues, _boktaiAddresses);
                        break;
                    case "Zoktai":
                        _statusEditorInstance = new ZoktaiStatusEditor(this, _memoryValues, _zoktaiAddresses);
                        break;
                    case "Shinbok":
                        _statusEditorInstance = new ShinbokStatusEditor(this, _memoryValues, _shinbokAddresses);
                        break;
                    case "LunarKnights":
                        _statusEditorInstance = new LunarKnightsStatusEditor(this, _memoryValues, _lunarKnightsAddresses);
                        break;
                    default:
                        // If game is not handled, do nothing
                        return;
                }

                statusEditorOpened = true;
            }
        }

        protected void OpenInventoryEditor(object sender, EventArgs e) {
            if (inventoryEditorOpened == false) {

                // Create subwindow & add on close event
                inventoryEditWindow = WinFormHelpers.CreateSubWindow("inventoryEditWindow", "Bok Edit - Inventory", 200, 100, this);
                inventoryEditWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                    inventoryEditorOpened = false;
                    inventoryEditWindow.Controls.Clear();
                });

                inventoryEditWindow.Show();
                inventoryEditorOpened = true;
            }
        }

        protected void OpenEquipsEditor(object sender, EventArgs e) {
            if (equipsEditorOpened == false) {

                // Create subwindow & add on close event
                equipsEditWindow = WinFormHelpers.CreateSubWindow("equipsEditWindow", "Bok Edit - Equips", 200, 100, this);
                equipsEditWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                    equipsEditorOpened = false;
                    equipsEditWindow.Controls.Clear();
                });

                equipsEditWindow.Show();
                equipsEditorOpened = true;
            }
        }

        protected void OpenSolarGunEditor(object sender, EventArgs e) {
            if (solarGunEditorOpened == false) {

                // Create subwindow & add on close event
                solarGunEditWindow = WinFormHelpers.CreateSubWindow("solarGunEditWindow", "Bok Edit - Solar Gun", 200, 100, this);
                solarGunEditWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                    solarGunEditorOpened = false;
                    solarGunEditWindow.Controls.Clear();
                });

                solarGunEditWindow.Show();
                solarGunEditorOpened = true;
            }
        }

        protected void OpenWeaponsEditor(object sender, EventArgs e) {
            if (weaponsEditorOpened == false) {

                // Create subwindow & add on close event
                weaponsEditWindow = WinFormHelpers.CreateSubWindow("weaponsEditWindow", "Bok Edit - Weapons", 200, 100, this);
                weaponsEditWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                    weaponsEditorOpened = false;
                    weaponsEditWindow.Controls.Clear();
                });

                weaponsEditWindow.Show();
                weaponsEditorOpened = true;
            }
        }

        protected void OpenMagicsEditor(object sender, EventArgs e) {
            if (magicsEditorOpened == false) {

                // Create subwindow & add on close event
                magicsEditWindow = WinFormHelpers.CreateSubWindow("magicsEditWindow", "Bok Edit - Magics", 200, 100, this);
                magicsEditWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                    magicsEditorOpened = false;
                    magicsEditWindow.Controls.Clear();
                });

                magicsEditWindow.Show();
                magicsEditorOpened = true;
            }
        }

        protected void OpenMiscToolsSelector(object sender, EventArgs e) {
            if (miscToolsSelectorOpened == false) {

                // Create subwindow & add on close event
                miscToolsSelectionWindow = WinFormHelpers.CreateSubWindow("miscToolsSelectWindow", "Bok Tools - Select", 186, 78, this);
                miscToolsSelectionWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                    miscToolsSelectorOpened = false;
                    miscToolsSelectionWindow.Controls.Clear();
                });

                // Add subwindow elements corresponding to the current game
                switch (shorterGameName) {
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

                miscToolsSelectionWindow.Show();
                miscToolsSelectorOpened = true;
            }
        }

        #endregion
    }
}

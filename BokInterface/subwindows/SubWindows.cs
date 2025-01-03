using System;
using System.Windows.Forms;

using BokInterface.Accessories;
using BokInterface.All;
using BokInterface.Inventory;
using BokInterface.KeyItems;
using BokInterface.Magics;
using BokInterface.Status;
using BokInterface.Weapons;

/**
 * Main file for subwindows
 */

namespace BokInterface {
    partial class BokInterface {

        #region Properties indicating if subwindows are opened or not

        public bool statusEditorOpened = false,
            inventoryEditorOpened = false,
            keyItemsEditorOpened = false,
            equipsEditorOpened = false,
            solarGunEditorOpened = false,
            weaponsEditorOpened = false,
            magicsEditorOpened = false,
            miscToolsSelectorOpened = false;

        #endregion

        #region Subwindows generation methods
        protected void OpenStatusEditor(object sender, EventArgs e) {
            if (statusEditorOpened == false) {

                // Open editor for the current game
                switch (shorterGameName) {
                    case "Boktai":
                        new BoktaiStatusEditor(this, _memoryValues, _boktaiAddresses);
                        break;
                    case "Zoktai":
                        new ZoktaiStatusEditor(this, _memoryValues, _zoktaiAddresses);
                        break;
                    case "Shinbok":
                        new ShinbokStatusEditor(this, _memoryValues, _shinbokAddresses);
                        break;
                    case "LunarKnights":
                        new LunarKnightsStatusEditor(this, _memoryValues, _lunarKnightsAddresses);
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

                // Open editor for the current game
                switch (shorterGameName) {
                    case "Boktai":
                        new BoktaiInventoryEditor(this, _memoryValues, _boktaiAddresses);
                        break;
                    case "Zoktai":
                        new ZoktaiInventoryEditor(this, _memoryValues, _zoktaiAddresses);
                        break;
                    case "Shinbok":
                        new ShinbokInventoryEditor(this, _memoryValues, _shinbokAddresses);
                        break;
                    case "LunarKnights":
                        new LunarKnightsInventoryEditor(this, _memoryValues, _lunarKnightsAddresses);
                        break;
                    default:
                        // If game is not handled, do nothing
                        return;
                }

                inventoryEditorOpened = true;
            }
        }

        protected void OpenKeyItemsEditor(object sender, EventArgs e) {
            if (keyItemsEditorOpened == false) {

                // Open editor for the current game
                switch (shorterGameName) {
                    case "Boktai":
                        new BoktaiKeyItemsEditor(this, _memoryValues, _boktaiAddresses);
                        break;
                    case "Zoktai":
                        new ZoktaiKeyItemsEditor(this, _memoryValues, _zoktaiAddresses);
                        break;
                    case "Shinbok":
                        new ShinbokKeyItemsEditor(this, _memoryValues, _shinbokAddresses);
                        break;
                    case "LunarKnights":
                        new LunarKnightsKeyItemsEditor(this, _memoryValues, _lunarKnightsAddresses);
                        break;
                    default:
                        // If game is not handled, do nothing
                        return;
                }

                keyItemsEditorOpened = true;
            }
        }

        protected void OpenEquipsEditor(object sender, EventArgs e) {
            if (equipsEditorOpened == false) {

                // Open editor for the current game
                switch (shorterGameName) {
                    case "Boktai":
                        // new BoktaiAccessoriesEditor(this, _memoryValues, _boktaiAddresses);
                        break;
                    case "Zoktai":
                        new ZoktaiAccessoriesEditor(this, _memoryValues, _zoktaiAddresses);
                        break;
                    case "Shinbok":
                        new ShinbokAccessoriesEditor(this, _memoryValues, _shinbokAddresses);
                        break;
                    case "LunarKnights":
                        // new LunarKnightsAccessoriesEditor(this, _memoryValues, _lunarKnightsAddresses);
                        break;
                    default:
                        // If game is not handled, do nothing
                        return;
                }

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

                // Open editor for the current game
                switch (shorterGameName) {
                    case "Boktai":
                        new BoktaiWeaponsEditor(this, _memoryValues, _boktaiAddresses);
                        break;
                    case "Zoktai":
                        new ZoktaiWeaponsEditor(this, _memoryValues, _zoktaiAddresses);
                        break;
                    case "Shinbok":
                        new ShinbokWeaponsEditor(this, _memoryValues, _shinbokAddresses);
                        break;
                    case "LunarKnights":
                        new LunarKnightsWeaponsEditor(this, _memoryValues, _lunarKnightsAddresses);
                        break;
                    default:
                        // If game is not handled, do nothing
                        return;
                }

                weaponsEditorOpened = true;
            }
        }

        protected void OpenMagicsEditor(object sender, EventArgs e) {
            if (magicsEditorOpened == false) {
                switch (shorterGameName) {
                    case "Boktai":
                        // No magics in Bok 1
                        return;
                    case "Zoktai":
                        new ZoktaiMagicsEditor(this, _memoryValues, _zoktaiAddresses);
                        break;
                    case "Shinbok":
                        // new ShinbokMagicsEditor(this, _memoryValues, _shinbokAddresses);
                        // break;
                        return;
                    case "LunarKnights":
                        // No magics in LK / DS
                        return;
                    default:
                        // If game is not handled, do nothing
                        return;
                }

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

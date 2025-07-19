using System;

using BokInterface.Accessories;
using BokInterface.Inventory;
using BokInterface.KeyItems;
using BokInterface.Magics;
using BokInterface.solarGun;
using BokInterface.Status;
using BokInterface.Weapons;

/**
 * Main file for subwindows
 */

namespace BokInterface {
    partial class BokInterface {

        #region Subwindows states

        public bool statusEditorOpened = false,
            inventoryEditorOpened = false,
            keyItemsEditorOpened = false,
            equipsEditorOpened = false,
            solarGunEditorOpened = false,
            weaponsEditorOpened = false,
            magicsEditorOpened = false,
            miscToolsSelectorOpened = false;

        #endregion

        #region Openers
        protected void OpenStatusEditor(object sender, EventArgs e) {
            if (statusEditorOpened == false) {

                // Open editor for the current game
                switch (shorterGameName) {
                    case "Boktai":
                        _subwindows.Add(new BoktaiStatusEditor(this, _memoryValues, _boktaiAddresses));
                        break;
                    case "Zoktai":
                        _subwindows.Add(new ZoktaiStatusEditor(this, _memoryValues, _zoktaiAddresses));
                        break;
                    case "Shinbok":
                        _subwindows.Add(new ShinbokStatusEditor(this, _memoryValues, _shinbokAddresses));
                        break;
                    case "LunarKnights":
                        _subwindows.Add(new LunarKnightsStatusEditor(this, _memoryValues, _lunarKnightsAddresses));
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
                        _subwindows.Add(new BoktaiInventoryEditor(this, _memoryValues, _boktaiAddresses));
                        break;
                    case "Zoktai":
                        _subwindows.Add(new ZoktaiInventoryEditor(this, _memoryValues, _zoktaiAddresses));
                        break;
                    case "Shinbok":
                        _subwindows.Add(new ShinbokInventoryEditor(this, _memoryValues, _shinbokAddresses));
                        break;
                    case "LunarKnights":
                        _subwindows.Add(new LunarKnightsInventoryEditor(this, _memoryValues, _lunarKnightsAddresses));
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
                        _subwindows.Add(new BoktaiKeyItemsEditor(this, _memoryValues, _boktaiAddresses));
                        break;
                    case "Zoktai":
                        _subwindows.Add(new ZoktaiKeyItemsEditor(this, _memoryValues, _zoktaiAddresses));
                        break;
                    case "Shinbok":
                        _subwindows.Add(new ShinbokKeyItemsEditor(this, _memoryValues, _shinbokAddresses));
                        break;
                    case "LunarKnights":
                        _subwindows.Add(new LunarKnightsKeyItemsEditor(this, _memoryValues, _lunarKnightsAddresses));
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
                        // _subwindows.Add(new BoktaiAccessoriesEditor(this, _memoryValues, _boktaiAddresses));
                        break;
                    case "Zoktai":
                        _subwindows.Add(new ZoktaiAccessoriesEditor(this, _memoryValues, _zoktaiAddresses));
                        break;
                    case "Shinbok":
                        _subwindows.Add(new ShinbokAccessoriesEditor(this, _memoryValues, _shinbokAddresses));
                        break;
                    case "LunarKnights":
                        // _subwindows.Add(new LunarKnightsAccessoriesEditor(this, _memoryValues, _lunarKnightsAddresses));
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
                // Open editor for the current game
                switch (shorterGameName) {
                    case "Boktai":
                        // None yet
                        break;
                    case "Zoktai":
                        // None, solar gun is handled via Weapons Inventory
                        break;
                    case "Shinbok":
                        _subwindows.Add(new ShinbokSolarGunEditor(this, _memoryValues, _shinbokAddresses));
                        break;
                    case "LunarKnights":
                    default:
                        // If game is not handled, do nothing
                        return;
                }
                solarGunEditorOpened = true;
            }
        }

        protected void OpenWeaponsEditor(object sender, EventArgs e) {
            if (weaponsEditorOpened == false) {

                // Open editor for the current game
                switch (shorterGameName) {
                    case "Boktai":
                        _subwindows.Add(new BoktaiWeaponsEditor(this, _memoryValues, _boktaiAddresses));
                        break;
                    case "Zoktai":
                        _subwindows.Add(new ZoktaiWeaponsEditor(this, _memoryValues, _zoktaiAddresses));
                        break;
                    case "Shinbok":
                        _subwindows.Add(new ShinbokWeaponsEditor(this, _memoryValues, _shinbokAddresses));
                        break;
                    case "LunarKnights":
                        _subwindows.Add(new LunarKnightsWeaponsEditor(this, _memoryValues, _lunarKnightsAddresses));
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
                        _subwindows.Add(new ZoktaiMagicsEditor(this, _memoryValues, _zoktaiAddresses));
                        break;
                    case "Shinbok":
                        // _subwindows.Add(new ShinbokMagicsEditor(this, _memoryValues, _shinbokAddresses));
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

        #endregion
    }
}

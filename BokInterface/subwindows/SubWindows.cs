using System;
using System.Windows.Forms;

/**
 * Main file for subwindows
 */

namespace BokInterface
{
    partial class BokInterfaceMainForm
    {

        #region Properties indicating if subwindows are opened or not

        protected bool statusEditorOpened = false;
        protected bool inventoryEditorOpened = false;
        protected bool equipsEditorOpened = false;
        protected bool solarGunEditorOpened = false;
        protected bool weaponsEditorOpened = false;
        protected bool magicsEditorOpened = false;
        protected bool miscToolsSelectorOpened = false;

        #endregion

        #region Subwindows generation methods
        protected void OpenStatusEditor(object sender, EventArgs e)
        {
            if (this.statusEditorOpened == false)
            {

                // Create subwindow & add on close event
                this.statusEditWindow = CreateSubWindow("statusEditWindow", "Bok Edit - Status", 203, 144);
                this.statusEditWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e)
                {
                    this.statusEditorOpened = false;
                    this.statusEditWindow.Controls.Clear();
                });

                // Clears subwindow
                ClearStatusEditControls();

                // Add subwindow elements corresponding to the current game
                switch (shorterGameName)
                {
                    case "Boktai":
                        BoktaiStatusEditSubwindow();
                        break;
                    case "Zoktai":
                        ZoktaiStatusEditSubwindow();
                        break;
                    case "Shinbok":
                        this.statusEditWindow.ClientSize = new System.Drawing.Size(227, 144);
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
                this.statusEditorOpened = true;
            }
        }

        protected void OpenInventoryEditor(object sender, EventArgs e)
        {
            if (this.inventoryEditorOpened == false)
            {

                // Create subwindow & add on close event
                this.inventoryEditWindow = CreateSubWindow("inventoryEditWindow", "Bok Edit - Inventory", 200, 100);
                this.inventoryEditWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e)
                {
                    this.inventoryEditorOpened = false;
                    this.inventoryEditWindow.Controls.Clear();
                });

                this.inventoryEditWindow.Show();
                this.inventoryEditorOpened = true;
            }
        }

        protected void OpenEquipsEditor(object sender, EventArgs e)
        {
            if (this.equipsEditorOpened == false)
            {

                // Create subwindow & add on close event
                this.equipsEditWindow = CreateSubWindow("equipsEditWindow", "Bok Edit - Equips", 200, 100);
                this.equipsEditWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e)
                {
                    this.equipsEditorOpened = false;
                    this.equipsEditWindow.Controls.Clear();
                });

                this.equipsEditWindow.Show();
                this.equipsEditorOpened = true;
            }
        }

        protected void OpenSolarGunEditor(object sender, EventArgs e)
        {
            if (this.solarGunEditorOpened == false)
            {

                // Create subwindow & add on close event
                this.solarGunEditWindow = CreateSubWindow("solarGunEditWindow", "Bok Edit - Solar Gun", 200, 100);
                this.solarGunEditWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e)
                {
                    this.solarGunEditorOpened = false;
                    this.solarGunEditWindow.Controls.Clear();
                });

                this.solarGunEditWindow.Show();
                this.solarGunEditorOpened = true;
            }
        }

        protected void OpenWeaponsEditor(object sender, EventArgs e)
        {
            if (this.weaponsEditorOpened == false)
            {

                // Create subwindow & add on close event
                this.weaponsEditWindow = CreateSubWindow("weaponsEditWindow", "Bok Edit - Weapons", 200, 100);
                this.weaponsEditWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e)
                {
                    this.weaponsEditorOpened = false;
                    this.weaponsEditWindow.Controls.Clear();
                });

                this.weaponsEditWindow.Show();
                this.weaponsEditorOpened = true;
            }
        }

        protected void OpenMagicsEditor(object sender, EventArgs e)
        {
            if (this.magicsEditorOpened == false)
            {

                // Create subwindow & add on close event
                this.magicsEditWindow = CreateSubWindow("magicsEditWindow", "Bok Edit - Magics", 200, 100);
                this.magicsEditWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e)
                {
                    this.magicsEditorOpened = false;
                    this.magicsEditWindow.Controls.Clear();
                });

                this.magicsEditWindow.Show();
                this.magicsEditorOpened = true;
            }
        }

        protected void OpenMiscToolsSelector(object sender, EventArgs e)
        {
            if (this.miscToolsSelectorOpened == false)
            {

                // Create subwindow & add on close event
                this.miscToolsSelectionWindow = CreateSubWindow("miscToolsSelectWindow", "Bok Tools - Select", 200, 100);
                this.miscToolsSelectionWindow.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e)
                {
                    this.miscToolsSelectorOpened = false;
                    this.miscToolsSelectionWindow.Controls.Clear();
                });

                // Add subwindow elements corresponding to the current game
                switch (shorterGameName)
                {
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
                this.miscToolsSelectorOpened = true;
            }
        }

        #endregion
    }
}
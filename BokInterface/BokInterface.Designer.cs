using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using BokInterface;
using BokInterface.Utils;
using BokInterface.Inventory;
using BokInterface.KeyItems;
using BokInterface.Magics;
using BokInterface.solarGun;
using BokInterface.Status;
using BokInterface.Tools.MemoryValuesListing;
using BokInterface.Tools.SolarBankInterestsSimulator;
using BokInterface.Tools.TileDataViewer;
using BokInterface.Weapons;
using BokInterface.Accessories;

/**
 * File for the external window part of the Bok interface
 */

namespace BokInterface {

    partial class BokInterface {

        #region Designer variable

        /// <summary>Required designer variable</summary>
        private IContainer _components = null;

        #endregion

        /// <summary>Clean up any resources being used</summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (_components != null)) {
                _components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region WinForm Designer

        /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor</summary>
        private void InitializeComponent() {

            /**
             * Clear the interface
             * The Bok Interface supports all 4 games, so we need to do that
             */
            ClearInterface();

            // Set corresponding game icon (or default icon if not available)
            Icon = WinFormHelpers.GetIcon(WinFormHelpers.GetGameIconName());

            // Try initializing the memory values instances dictionnaries
            _memoryValues = new(shorterGameName);

            // Re-initialize the MovementCalculator to reset its properties values
            _movementCalculator = new();

            // Show corresponding interface
            switch (shorterGameName) {
                case "Boktai":
                    ShowBoktaiInterface();
                    break;
                case "Zoktai":
                    ShowZoktaiInterface();
                    break;
                case "Shinbok":
                    ShowShinbokInterface();
                    break;
                case "BoktaiDS":
                case "LunarKnights":
                    ShowDsInterface();
                    break;
                default:
                    // If not a Boktai game, show the "Game not recognized" window & stop here
                    _interfaceActivated = false;
                    ShowGameNotRecognizedWindow();
                    return;
            }

            _interfaceActivated = true;
        }

        #endregion

        #region Clearing methods

        /// <summary>Clears the interface window and all other sections within it</summary>
        private void ClearInterface() {

            // Close all subwindows
            foreach (Form subwindow in _subwindows) {
                if (subwindow != null && subwindow.IsDisposed == false) {
                    subwindow.Close();
                }
            }

            // Main window elements
            Controls.Clear();
            _subwindows.Clear();
            _currentStatusGroupBox.Controls.Clear();
            _currentStatsGroupBox.Controls.Clear();
        }

        #endregion

        #region Elements & window

        /// <summary>Simplified method for setting the main window of the interface</summary>
        /// <param name="name">Window name</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        private void SetMainWindow(string name, Int32 width, Int32 height) {
            Name = name;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            BackColor = System.Drawing.SystemColors.Control;
            Font = WinFormHelpers.defaultFont;
            ClientSize = new System.Drawing.Size(width, height);
            MaximizeBox = MinimizeBox = false;
        }

        /// <summary>Add the labels with the game info to the main window</summary>
        private void AddCurrentGameInfo() {
            WinFormHelpers.CreateLabel("currentGameName", WinFormHelpers.EscapeAmpersand(currentGameName), 0, _menuBar.Height, Width, 20, this, WinFormHelpers.gameNameBackground, textAlignment: "MiddleLeft");
            Label regionVersionLabel = WinFormHelpers.CreateLabel("currentGameRegionVersion", region + " " + version, 0, _menuBar.Height, 20, 20, this, WinFormHelpers.gameVersionBackground, textAlignment: "MiddleLeft");
            regionVersionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            regionVersionLabel.Width = TextRenderer.MeasureText(regionVersionLabel.Text, regionVersionLabel.Font).Width;
            regionVersionLabel.Location = new Point(ClientSize.Width - regionVersionLabel.Width, regionVersionLabel.Location.Y);
            regionVersionLabel.BringToFront();
        }

        /// <summary>Adds the Misc. data section for the corresponding game</summary>
        private void AddMiscDataSection() {

            int positionY = 110,
                groupHeight = 55;
            bool enableCoffinSection = false;
            switch (BokInterface.shorterGameName) {
                case "Boktai":
                    positionY = 106;
                    groupHeight = 164;
                    enableCoffinSection = true;
                    break;
                case "Zoktai":
                    positionY = 218;
                    break;
                case "Shinbok":
                    positionY = 214;
                    break;
                case "BoktaiDS":
                case "LunarKnights":
                    positionY = 125;
                    break;
                default:
                    // If game is not handled, don't add anything & stop here
                    return;
            }

            _miscDataGroupBox = WinFormHelpers.CreateGroupBox("miscData", "Misc. data", 5, positionY, 226, groupHeight, this);

            // Average speed
            _currentSpeedLabel = WinFormHelpers.CreateLabel("currentMovementSpeed", "Current movement speed : ", 7, 19, 200, 15, _miscDataGroupBox, textAlignment: "MiddleLeft");
            _averageSpeedLabel = WinFormHelpers.CreateLabel("averageMovementSpeed", "Average over 60 frames : ", 7, 34, 200, 15, _miscDataGroupBox, textAlignment: "MiddleLeft");

            // Add the coffin section if enabled
            if (enableCoffinSection == true) {

                WinFormHelpers.CreateSeparator("miscDataSeparator", 5, 51, 216, _miscDataGroupBox);
                WinFormHelpers.CreateLabel("coffinSection", "Coffin data", 5, 53, 80, 15, _miscDataGroupBox, textAlignment: "MiddleLeft").Font = new("Segoe UI", 9, FontStyle.Underline, GraphicsUnit.Point);

                // Coffin data
                _coffinDamageLabel = WinFormHelpers.CreateLabel("coffinDamage", "Damage : ", 5, 68, 200, 15, _miscDataGroupBox, textAlignment: "MiddleLeft");
                _coffinWindupTimerLabel = WinFormHelpers.CreateLabel("coffinWindupTimer", "Windup begins in : ", 5, 83, 200, 15, _miscDataGroupBox, textAlignment: "MiddleLeft");
                _coffinShakeTimerLabel = WinFormHelpers.CreateLabel("coffnShakeTimer", "Windup : ", 5, 98, 200, 15, _miscDataGroupBox, textAlignment: "MiddleLeft");
                _coffinShakeDurationLabel = WinFormHelpers.CreateLabel("coffinShakeDuration", "Duration : ", 5, 113, 200, 15, _miscDataGroupBox, textAlignment: "MiddleLeft");
                _coffinEscapeTimerLabel = WinFormHelpers.CreateLabel("coffinEscapeTimer", "Begins escaping in : ", 5, 128, 200, 15, _miscDataGroupBox, textAlignment: "MiddleLeft");
                _coffinDistanceLabel = WinFormHelpers.CreateLabel("coffinDistance", "Distance : ", 5, 143, 200, 15, _miscDataGroupBox, textAlignment: "MiddleLeft");
            }
        }

        #endregion

        #region Menu

        /// <summary>Generate the menu for the main window</summary>
        private void GenerateMenu() {

            _menuBar = WinFormHelpers.CreateMenuStrip("menuBar", "", control: this);

            // Edit section
            ToolStripMenuItem editMenu = WinFormHelpers.CreateToolStripMenuItem("editMenu", "Edit", menuStrip: _menuBar);

            if (shorterGameName == "Zoktai" || shorterGameName == "Shinbok") {
                ToolStripMenuItem editStatusMenu = WinFormHelpers.CreateToolStripMenuItem("editStatusMenu", "&Status", menuItem: editMenu);
                editStatusMenu.Click += new EventHandler(OpenStatusEditor);
                editMenu.DropDownItems.Add(editStatusMenu);
            }

            ToolStripMenuItem editItemsMenu = WinFormHelpers.CreateToolStripMenuItem("edititemsMenu", "&Items", menuItem: editMenu);
            editItemsMenu.Click += new EventHandler(OpenInventoryEditor);
            editMenu.DropDownItems.Add(editItemsMenu);

            // If DS / LK, stop here
            if (_isDS == true) {
                return;
            }

            if (shorterGameName != "Boktai") {

                ToolStripMenuItem editKeyItemsMenu = WinFormHelpers.CreateToolStripMenuItem("editKeyitemsMenu", "&Key items", menuItem: editMenu);
                editKeyItemsMenu.Click += new EventHandler(OpenKeyItemsEditor);
                editMenu.DropDownItems.Add(editKeyItemsMenu);

                ToolStripMenuItem editWeaponsMenu = WinFormHelpers.CreateToolStripMenuItem("editWeaponsMenu", "&Weapons", menuItem: editMenu);
                editWeaponsMenu.Click += new EventHandler(OpenWeaponsEditor);
                editMenu.DropDownItems.Add(editWeaponsMenu);
            }

            if (shorterGameName == "Shinbok" || shorterGameName == "Boktai") {
                AddDropdownMenuItem("editGunMenu", "Solar gun", editMenu, OpenSolarGunEditor);
            }

            if (shorterGameName != "Boktai") {

                AddDropdownMenuItem("editAccessoriesMenu", shorterGameName == "Zoktai" ? "Protectors" : "Accessories", editMenu, OpenEquipsEditor);
                if (shorterGameName == "Zoktai") {
                    AddDropdownMenuItem("editMagicsMenu", "Magics", editMenu, OpenMagicsEditor);
                }
            }

            // Misc tools & GUI sections
            GenerateToolsMenu();
            GenerateGuiMenu();
        }

        /// <summary>Generate the menu related to misc tools</summary>
        private void GenerateToolsMenu() {

            ToolStripMenuItem toolsMenu = WinFormHelpers.CreateToolStripMenuItem("toolsMenu", "Tools", menuStrip: _menuBar);
            AddDropdownMenuItem("memoryValuesListMenu", "Memory values list", toolsMenu, OpenMemoryValuesList);
            if (_isDS == true) {
                return;
            }

            AddDropdownMenuItem("tileDataViewerMenu", "Tile data viewer", toolsMenu, OpenTileDataViewer);
            if (shorterGameName == "Zoktai" || shorterGameName == "Shinbok") {
                AddDropdownMenuItem("solarBankInterestsSimMenu", "Solar bank interests simulator", toolsMenu, OpenSolarBankInterestsSim);
            }
        }

        #endregion

        #region Openers - Editors
        protected void OpenStatusEditor(object sender, EventArgs e) {

            // Check if the subwindow has already been instanciated and if so, show & focus on it
            if (statusEditorOpened == true) {
                ShowExistingSubwindow("BokInterface.Status.StatusEditor");
                return;
            }

            // Otherwise instanciate the subwindow
            StatusEditor statusEditor = null;
            switch (shorterGameName) {
                case "Boktai":
                    statusEditor = new BoktaiStatusEditor(this, _memoryValues, _boktaiAddresses);
                    break;
                case "Zoktai":
                    statusEditor = new ZoktaiStatusEditor(this, _memoryValues, _zoktaiAddresses);
                    break;
                case "Shinbok":
                    statusEditor = new ShinbokStatusEditor(this, _memoryValues, _shinbokAddresses);
                    break;
                // case "BoktaiDS":
                // case "LunarKnights":
                //     statusEditor = new DsStatusEditor(this, _memoryValues, _dsAddresses);
                //     break;
                default:
                    // If game is not handled, stop
                    return;
            }

            /**
             * Add the subwindow to the list of activate subwindows
             * Indicate that it's active via the menuItem check & the boolean
             */
            _subwindows.Add(statusEditor);
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            statusEditorOpened = menuItem.Checked = true;

            // Add the on-close event handler
            statusEditor.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                statusEditorOpened = menuItem.Checked = false;
                statusEditor.Dispose();
            });
        }

        protected void OpenInventoryEditor(object sender, EventArgs e) {
            if (inventoryEditorOpened == true) {
                ShowExistingSubwindow("BokInterface.Inventory.InventoryEditor");
                return;
            }

            InventoryEditor inventoryEditor = null;
            switch (shorterGameName) {
                case "Boktai":
                    inventoryEditor = new BoktaiInventoryEditor(this, _memoryValues, _boktaiAddresses);
                    break;
                case "Zoktai":
                    inventoryEditor = new ZoktaiInventoryEditor(this, _memoryValues, _zoktaiAddresses);
                    break;
                case "Shinbok":
                    inventoryEditor = new ShinbokInventoryEditor(this, _memoryValues, _shinbokAddresses);
                    break;
                // case "BoktaiDS":
                // case "LunarKnights":
                //     inventoryEditor = new dsInventoryEditor(this, _memoryValues, _dsAddresses);
                //     break;
                default:
                    // If game is not handled, stop
                    return;
            }

            _subwindows.Add(inventoryEditor);
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            inventoryEditorOpened = menuItem.Checked = true;

            inventoryEditor.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                inventoryEditorOpened = menuItem.Checked = false;
                inventoryEditor.Dispose();
            });
        }

        protected void OpenKeyItemsEditor(object sender, EventArgs e) {
            if (keyItemsEditorOpened == true) {
                ShowExistingSubwindow("BokInterface.KeyItems.KeyItemsEditor");
                return;
            }

            KeyItemsEditor keyItemsEditor = null;
            switch (shorterGameName) {
                case "Boktai":
                    keyItemsEditor = new BoktaiKeyItemsEditor(this, _memoryValues, _boktaiAddresses);
                    break;
                case "Zoktai":
                    keyItemsEditor = new ZoktaiKeyItemsEditor(this, _memoryValues, _zoktaiAddresses);
                    break;
                case "Shinbok":
                    keyItemsEditor = new ShinbokKeyItemsEditor(this, _memoryValues, _shinbokAddresses);
                    break;
                // case "BoktaiDS":
                // case "LunarKnights":
                //     keyItemsEditor = new DsKeyItemsEditor(this, _memoryValues, _dsAddresses);
                //     break;
                default:
                    // If game is not handled, stop
                    return;
            }

            _subwindows.Add(keyItemsEditor);
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            keyItemsEditorOpened = menuItem.Checked = true;

            keyItemsEditor.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                keyItemsEditorOpened = menuItem.Checked = false;
                keyItemsEditor.Dispose();
            });
        }

        protected void OpenEquipsEditor(object sender, EventArgs e) {
            if (equipsEditorOpened == true) {
                ShowExistingSubwindow("BokInterface.Accessories.AccessoriesEditor");
                return;
            }

            AccessoriesEditor accessoriesEditor = null;
            switch (shorterGameName) {
                case "Zoktai":
                    accessoriesEditor = new ZoktaiAccessoriesEditor(this, _memoryValues, _zoktaiAddresses);
                    break;
                case "Shinbok":
                    accessoriesEditor = new ShinbokAccessoriesEditor(this, _memoryValues, _shinbokAddresses);
                    break;
                // case "BoktaiDS":
                // case "LunarKnights":
                //     accessoriesEditor = new LunarKnightsAccessoriesEditor(this, _memoryValues, _dsAddresses);
                //     break;
                default:
                    // If game is not handled or does not have accessories, stop
                    return;
            }

            _subwindows.Add(accessoriesEditor);
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            equipsEditorOpened = menuItem.Checked = true;

            accessoriesEditor.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                equipsEditorOpened = menuItem.Checked = false;
                accessoriesEditor.Dispose();
            });
        }

        protected void OpenSolarGunEditor(object sender, EventArgs e) {
            if (solarGunEditorOpened == true) {
                ShowExistingSubwindow("BokInterface.solarGun.SolarGunEditor");
                return;
            }

            SolarGunEditor solarGunEditor = null;
            switch (shorterGameName) {
                case "Boktai":
                    solarGunEditor = new BoktaiSolarGunEditor(this, _memoryValues, _boktaiAddresses);
                    break;
                case "Shinbok":
                    solarGunEditor = new ShinbokSolarGunEditor(this, _memoryValues, _shinbokAddresses);
                    break;
                default:
                    // If game is not handled, stop
                    // Note: for Zoktai / Bok 2 the solar gun is handled via the Weapons Inventory
                    return;
            }

            _subwindows.Add(solarGunEditor);
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            solarGunEditorOpened = menuItem.Checked = true;

            solarGunEditor.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                solarGunEditorOpened = menuItem.Checked = false;
                solarGunEditor.Dispose();
            });
        }

        protected void OpenWeaponsEditor(object sender, EventArgs e) {
            if (weaponsEditorOpened == true) {
                ShowExistingSubwindow("BokInterface.Weapons.WeaponsEditor");
                return;
            }

            WeaponsEditor weaponsEditor = null;
            switch (shorterGameName) {
                case "Zoktai":
                    weaponsEditor = new ZoktaiWeaponsEditor(this, _memoryValues, _zoktaiAddresses);
                    break;
                case "Shinbok":
                    weaponsEditor = new ShinbokWeaponsEditor(this, _memoryValues, _shinbokAddresses);
                    break;
                // case "BoktaiDS":
                // case "LunarKnights":
                //     weaponsEditor = new DsWeaponsEditor(this, _memoryValues, _dsAddresses);
                //     break;
                default:
                    // If game is not handled, stop
                    // Note: Bok 1 only has the solar gun
                    return;
            }

            _subwindows.Add(weaponsEditor);
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            weaponsEditorOpened = menuItem.Checked = true;

            weaponsEditor.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                weaponsEditorOpened = menuItem.Checked = false;
                weaponsEditor.Dispose();
            });
        }

        protected void OpenMagicsEditor(object sender, EventArgs e) {
            if (magicsEditorOpened == true) {
                ShowExistingSubwindow("BokInterface.Magics.MagicsEditor");
                return;
            }

            MagicsEditor magicsEditor = null;
            switch (shorterGameName) {
                case "Zoktai":
                    magicsEditor = new ZoktaiMagicsEditor(this, _memoryValues, _zoktaiAddresses);
                    break;
                // case "Shinbok":
                //     magicsEditor = new ShinbokMagicsEditor(this, _memoryValues, _dsAddresses);
                //     break;
                default:
                    // If game is not handled or does not have magics, stop
                    return;
            }

            _subwindows.Add(magicsEditor);
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            magicsEditorOpened = menuItem.Checked = true;

            magicsEditor.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                magicsEditorOpened = menuItem.Checked = false;
                magicsEditor.Dispose();
            });
        }

        #endregion

        #region Openers - Tools

        private void OpenTileDataViewer(object sender, EventArgs e) {
            if (tileDataViewerActive == true) {
                ShowExistingSubwindow("BokInterface.Tools.TileDataViewer.TileDataViewer");
                return;
            }

            TileDataViewer tileDataViewer = null;
            switch (shorterGameName) {
                case "Boktai":
                    tileDataViewer = new BoktaiTileDataViewer(this, _boktaiAddresses);
                    break;
                case "Zoktai":
                    tileDataViewer = new ZoktaiTileDataViewer(this, _zoktaiAddresses);
                    break;
                case "Shinbok":
                    tileDataViewer = new ShinbokTileDataViewer(this, _shinbokAddresses);
                    break;
                // case "BoktaiDS":
                // case "LunarKnights":
                //     tileDataViewer = new DsTileDataViewer(this, _dsAddresses);
                //     break;
                default:
                    // If game not handled, stop
                    return;
            }

            _subwindows.Add(tileDataViewer);
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            tileDataViewerActive = menuItem.Checked = true;

            // Initialize the loop to update / redraw automatically & add the on-close event handler
            tileDataViewer.InitializeFrameLoop();
            tileDataViewer.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                /**
                 * Remove the function from the list of functions to call each frame
                 * Also set instance with null to prevent it from doing anything else
                 */
                tileDataViewerActive = menuItem.Checked = false;
                functionsList.RemoveAt(tileDataViewer.index);
                tileDataViewer.Dispose();
            });
        }

        private void OpenMemoryValuesList(object sender, EventArgs e) {
            if (memValuesListingActive == true) {
                ShowExistingSubwindow("BokInterface.Tools.MemoryValuesListing.MemoryValuesListing");
                return;
            }

            MemoryValuesListing memoryValuesListing = null;
            switch (shorterGameName) {
                case "Boktai":
                    memoryValuesListing = new MemoryValuesListing(this, _boktaiAddresses);
                    break;
                case "Zoktai":
                    memoryValuesListing = new MemoryValuesListing(this, _zoktaiAddresses);
                    break;
                case "Shinbok":
                    memoryValuesListing = new MemoryValuesListing(this, _shinbokAddresses);
                    break;
                // case "BoktaiDS":
                // case "LunarKnights":
                //     memoryValuesListing = new MemoryValuesListing(this, _dsAddresses);
                //     break;
                default:
                    // If game not handled, stop
                    return;
            }

            _subwindows.Add(memoryValuesListing);
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            memValuesListingActive = menuItem.Checked = true;

            memoryValuesListing.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                memValuesListingActive = menuItem.Checked = false;
                memoryValuesListing.Dispose();
            });
        }

        private void OpenSolarBankInterestsSim(object sender, EventArgs e) {
            if (solarBankInterestsSimActive == true) {
                ShowExistingSubwindow("BokInterface.Tools.SolarBankInterestsSimulator.SolarBankInterestsSimulator");
                return;
            }

            SolarBankInterestsSimulator solarBankInterestsSimulator = null;
            switch (shorterGameName) {
                case "Zoktai":
                case "Shinbok":
                    solarBankInterestsSimulator = new SolarBankInterestsSimulator(this);
                    break;
                default:
                    // If game not handled, stop
                    return;
            }

            _subwindows.Add(solarBankInterestsSimulator);
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            solarBankInterestsSimActive = menuItem.Checked = true;

            solarBankInterestsSimulator.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                solarBankInterestsSimActive = menuItem.Checked = false;
                solarBankInterestsSimulator.Dispose();
            });
        }

        #endregion

        #region Other methods

        /// <summary>Show an already existing subwindow</summary>
        /// <param name="type">Full type of the subwindow with namespace</param>
        private void ShowExistingSubwindow(string type) {

            // Store the type into a proper variable
            Type instanceType = Type.GetType(type);
            if (instanceType == null) {
                return;
            }

            // Get the instance that is of this type (there can only be one due to how )
            Form instance = _subwindows.FirstOrDefault(x => instanceType.IsInstanceOfType(x));
            if (instance != null) {

                // Show the subwindow (even if it's minimized)
                if (instance.WindowState == FormWindowState.Minimized) {
                    instance.WindowState = FormWindowState.Normal;
                }

                instance.Activate();
            }
        }

        /// <summary>Add an item (submenu) to a menu dropdown</summary>
        /// <param name="name">Submenu name</param>
        /// <param name="text">Submenu text (without the <![CDATA[&]]> at the start)</param>
        /// <param name="parent">Menu the submenu is attached to</param>
        /// <param name="onClick">OnClick event</param>
        /// <param name="tooltip">ToolTipText (by default none)</param>
        private void AddDropdownMenuItem(string name, string text, ToolStripMenuItem parent, EventHandler onClick, string tooltip = "") {
            ToolStripMenuItem menuItem = WinFormHelpers.CreateToolStripMenuItem(name, "&" + text, toolTipText: tooltip, menuItem: parent);
            menuItem.Click += new EventHandler(onClick);
            parent.DropDownItems.Add(menuItem);
        }

        /// <summary>Reusable method for toggling the property and MenuItem related to GUI data shown on screen</summary>
        /// <param name="menuItem">Menu / submenu</param>
        /// <param name="property">Property</param>
        private void ToggleGuiData(object menuItem, ref bool property) {
            if (menuItem is ToolStripMenuItem menu) {
                menu.Checked = property = !property;
            }
        }

        #endregion
    }
}

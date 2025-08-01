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
					_interfaceActivated = true;
					ShowBoktaiInterface();
					break;
				case "Zoktai":
					_interfaceActivated = true;
					ShowZoktaiInterface();
					break;
				case "Shinbok":
					_interfaceActivated = true;
					ShowShinbokInterface();
					break;
				case "LunarKnights":
					_interfaceActivated = true;
					ShowLunarKnightsInterface();
					break;
				default:
					// If not a Boktai game, show the "Game not recognized" window & stop here
					_interfaceActivated = false;
					ShowGameNotRecognizedWindow();
					break;
			}
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
			_inventoryGroupBox.Controls.Clear();
			_editGroupBox.Controls.Clear();
			_extrasGroupBox.Controls.Clear();

			// Tools selection subwindow elements
			_miscToolsSelectionWindow.Controls.Clear();
			_miscToolsSelectionWindow.Close();
			miscToolsSelectorOpened = false;

			// Extra tools
			ClearExtraTools();
		}

		/// <summary>Clears subwindows related to extra tools</summary>
		private void ClearExtraTools() {

			// Tile Data Viewer
			if (_tileDataViewer != null) {
				_tileDataViewer.Controls.Clear();
				_tileDataViewer.Close();
				tileDataViewerActive = false;
			}

			// Memory Values Listing
			if (_memValuesListing != null) {
				_memValuesListing.Controls.Clear();
				_memValuesListing.Close();
				memValuesListingActive = false;
			}

			// Solar bank interests simulator
			if (_solarBankInterestsSim != null) {
				_solarBankInterestsSim.Controls.Clear();
				_solarBankInterestsSim.Close();
				solarBankInterestsSimActive = false;
			}
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

		/// <summary>Adds the Misc. data section for the corresponding game</summary>
		private void AddMiscDataSection() {

			int positionY = 90;
			int groupHeight = 55;
			bool enableCoffinSection = false;
			switch (BokInterface.shorterGameName) {
				case "Boktai":
					positionY = 90;
					groupHeight = 164;
					enableCoffinSection = true;
					break;
				case "Zoktai":
					positionY = 198;
					break;
				case "Shinbok":
					positionY = 194;
					break;
				case "LunarKnights":
					positionY = 105;
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
			MenuStrip menuBar = WinFormHelpers.CreateMenuStrip("menuBar", "", control: this);

			if (shorterGameName == "LunarKnights") {
				return;
			}

			// Edit section
			if (shorterGameName == "Zoktai" || shorterGameName == "Shinbok") {

				ToolStripMenuItem editMenu = WinFormHelpers.CreateToolStripMenuItem("editMenu", "Edit", menuStrip: menuBar);

				ToolStripMenuItem editStatusMenu = WinFormHelpers.CreateToolStripMenuItem("editStatusMenu", "&Status", menuItem: editMenu);
				editStatusMenu.Click += new EventHandler(OpenStatusEditor);
				editMenu.DropDownItems.Add(editStatusMenu);

				ToolStripMenuItem editItemsMenu = WinFormHelpers.CreateToolStripMenuItem("edititemsMenu", "&Items", menuItem: editMenu);
				editItemsMenu.Click += new EventHandler(OpenInventoryEditor);
				editMenu.DropDownItems.Add(editItemsMenu);

				ToolStripMenuItem editKeyItemsMenu = WinFormHelpers.CreateToolStripMenuItem("editKeyitemsMenu", "&Key items", menuItem: editMenu);
				editKeyItemsMenu.Click += new EventHandler(OpenKeyItemsEditor);
				editMenu.DropDownItems.Add(editKeyItemsMenu);

				ToolStripMenuItem editWeaponsMenu = WinFormHelpers.CreateToolStripMenuItem("editWeaponsMenu", "&Weapons", menuItem: editMenu);
				editWeaponsMenu.Click += new EventHandler(OpenWeaponsEditor);
				editMenu.DropDownItems.Add(editWeaponsMenu);

				if (shorterGameName == "Shinbok") {
					ToolStripMenuItem editGunMenu = WinFormHelpers.CreateToolStripMenuItem("editGunMenu", "&Solar gun", menuItem: editMenu);
					editGunMenu.Click += new EventHandler(OpenSolarGunEditor);
					editMenu.DropDownItems.Add(editGunMenu);
				}

				ToolStripMenuItem editAccessoriesMenu = WinFormHelpers.CreateToolStripMenuItem("editAccessoriesMenu", shorterGameName == "Zoktai" ? "&Protectors" : "&Accessories", menuItem: editMenu);
				editAccessoriesMenu.Click += new EventHandler(OpenEquipsEditor);
				editMenu.DropDownItems.Add(editAccessoriesMenu);

				if (shorterGameName == "Zoktai") {
					ToolStripMenuItem editMagicsMenu = WinFormHelpers.CreateToolStripMenuItem("editMagicsMenu", "&Magics", menuItem: editMenu);
					editMagicsMenu.Click += new EventHandler(OpenMagicsEditor);
					editMenu.DropDownItems.Add(editMagicsMenu);
				}
			}

			// Misc tools section
			ToolStripMenuItem toolsMenu = WinFormHelpers.CreateToolStripMenuItem("toolsMenu", "Tools", menuStrip: menuBar);

			ToolStripMenuItem tileDataViewerMenu = WinFormHelpers.CreateToolStripMenuItem("tileDataViewerMenu", "&Tile data viewer", menuItem: toolsMenu);
			tileDataViewerMenu.Click += new EventHandler(OpenTileDataViewer);
			toolsMenu.DropDownItems.Add(tileDataViewerMenu);

			ToolStripMenuItem memoryValuesListMenu = WinFormHelpers.CreateToolStripMenuItem("memoryValuesListMenu", "&Memory values list", menuItem: toolsMenu);
			memoryValuesListMenu.Click += new EventHandler(OpenMemoryValuesList);
			toolsMenu.DropDownItems.Add(memoryValuesListMenu);

			if (shorterGameName != "Boktai") {
				ToolStripMenuItem solarBankInterestsSimMenu = WinFormHelpers.CreateToolStripMenuItem("solarBankInterestsSimMenu", "&Solar bank interests simulator", menuItem: toolsMenu);
				solarBankInterestsSimMenu.Click += new EventHandler(OpenSolarBankInterestsSim);
				toolsMenu.DropDownItems.Add(solarBankInterestsSimMenu);
			}
		}

		#endregion

		#region Openers - Editors
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

		#region Openers - Tools

		private void OpenTileDataViewer(object sender, EventArgs e) {

            // If tool is already active, stop
            if (tileDataViewerActive == true) {
                return;
            }

            tileDataViewerActive = true;
            switch (shorterGameName) {
                case "Boktai":
                    _tileDataViewer = new BoktaiTileDataViewer(this, _boktaiAddresses);
                    break;
                case "Zoktai":
                    _tileDataViewer = new ZoktaiTileDataViewer(this, _zoktaiAddresses);
                    break;
                case "Shinbok":
                    _tileDataViewer = new ShinbokTileDataViewer(this, _shinbokAddresses);
                    break;
                case "LunarKnights":
                    _tileDataViewer = new LunarKnightsTileDataViewer(this, _lunarKnightsAddresses);
                    break;
                default:
                    // If game not handled, indicate that the tool isn't active & stop here
                    tileDataViewerActive = false;
                    return;
            }

            // Initialize the loop to update / redraw automatically & add the on-close event handler
            _tileDataViewer.InitializeFrameLoop();
            _tileDataViewer.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                /**
                 * Remove the function from the list of functions to call each frame
                 * Also set instance with null to prevent it from doing anything else
                 */
                tileDataViewerActive = false;
                functionsList.RemoveAt(_tileDataViewer.index);
                _tileDataViewer = null;
            });
        }

        private void OpenMemoryValuesList(object sender, EventArgs e) {
            if (memValuesListingActive == true) {
                return;
            }

            memValuesListingActive = true;
            switch (shorterGameName) {
                case "Boktai":
                    _memValuesListing = new MemoryValuesListing(this, _boktaiAddresses);
                    break;
                case "Zoktai":
                    _memValuesListing = new MemoryValuesListing(this, _zoktaiAddresses);
                    break;
                case "Shinbok":
                    _memValuesListing = new MemoryValuesListing(this, _shinbokAddresses);
                    break;
                case "LunarKnights":
                    _memValuesListing = new MemoryValuesListing(this, _lunarKnightsAddresses);
                    break;
                default:
                    // If game not handled, indicate that the tool isn't active & stop here
                    memValuesListingActive = false;
                    return;
            }

            // Add the on-close event handler
            _memValuesListing.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                // Indicate that the tool isn't active anymore & set instance to null to prevent it from doing anything else
                memValuesListingActive = false;
                _memValuesListing = null;
            });
        }

        private void OpenSolarBankInterestsSim(object sender, EventArgs e) {
            if (solarBankInterestsSimActive == true) {
                return;
            }

            solarBankInterestsSimActive = true;
            switch (shorterGameName) {
                case "Zoktai":
                case "Shinbok":
                    _solarBankInterestsSim = new SolarBankInterestsSimulator(this);
                    break;
                default:
                    // If game not handled, indicate that the tool isn't active & stop here
                    solarBankInterestsSimActive = false;
                    return;
            }

            // Add the on-close event handler
            _solarBankInterestsSim.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                // Indicate that the tool isn't active anymore & set instance to null just in case, to prevent it from doing anything else
                solarBankInterestsSimActive = false;
                _solarBankInterestsSim = null;
            });
        }
		
		#endregion
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using BokInterface.All;

/**
 * File for the external window part of the Bok interface
 */

namespace BokInterface {

	partial class BokInterface {

		#region Main properties

		/// <summary>Required designer variable</summary>
		private IContainer _components = null;

		#endregion

		#region Common elements

		private System.Windows.Forms.GroupBox _currentStatusGroupBox = new(),
			_currentStatsGroupBox = new(),
			_inventoryGroupBox = new(),
			_editGroupBox = new(),
			_extrasGroupBox = new(),
			_miscDataGroupBox = new();
		// Misc data labels
		private System.Windows.Forms.Label _averageSpeedLabel = new(),
			_currentSpeedLabel = new(),
			_coffinDamageLabel = new(),
			_coffinWindupTimerLabel = new(),
			_coffinShakeTimerLabel = new(),
			_coffinShakeDurationLabel = new(),
			_coffinEscapeTimerLabel = new(),
			_coffinDistanceLabel = new();

		#endregion

		#region Subwindows

		private System.Windows.Forms.Form _miscToolsSelectionWindow = new();
		private List<System.Windows.Forms.Form> _subwindows = new();

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
	}
}
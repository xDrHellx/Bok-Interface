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

		#region Main interface properties

		/// <summary>Required designer variable</summary>
		private IContainer components = null;

		#endregion

		#region Common interface elements properties

		private System.Windows.Forms.GroupBox currentStatusGroupBox = new(),
			currentStatsGroupBox = new(),
			inventoryGroupBox = new(),
			editGroupBox = new(),
			extrasGroupBox = new(),
			miscDataGroupBox = new();
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

		#region Subwindows properties

		private System.Windows.Forms.Form miscToolsSelectionWindow = new();
		private List<System.Windows.Forms.Form> subwindows = new();

		#endregion

		/// <summary>Clean up any resources being used</summary>
		/// <param name="disposing">True if managed resources should be disposed; otherwise, false</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

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
					interfaceActivated = true;
					ShowBoktaiInterface();
					break;
				case "Zoktai":
					interfaceActivated = true;
					ShowZoktaiInterface();
					break;
				case "Shinbok":
					interfaceActivated = true;
					ShowShinbokInterface();
					break;
				case "LunarKnights":
					interfaceActivated = true;
					ShowLunarKnightsInterface();
					break;
				default:
					// If not a Boktai game, show the "Game not recognized" window & stop here
					interfaceActivated = false;
					ShowGameNotRecognizedWindow();
					break;
			}
		}

		/// <summary>Clears the interface window and all other sections within it</summary>
		private void ClearInterface() {

			// Close all subwindows
			foreach (Form subwindow in subwindows) {
				if (subwindow != null && subwindow.IsDisposed == false) {
					subwindow.Close();
				}
			}

			// Main window elements
			Controls.Clear();
			subwindows.Clear();
			currentStatusGroupBox.Controls.Clear();
			currentStatsGroupBox.Controls.Clear();
			inventoryGroupBox.Controls.Clear();
			editGroupBox.Controls.Clear();
			extrasGroupBox.Controls.Clear();

			// Tools selection subwindow elements
			miscToolsSelectionWindow.Controls.Clear();
			miscToolsSelectionWindow.Close();
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

		/// <summary>Adds the Tools section for the corresponding game</summary>
		private void AddToolsSection() {
			int btnWidthOffset = 0;
			switch (BokInterface.shorterGameName) {
                case "Boktai":
                    extrasGroupBox = WinFormHelpers.CreateGroupBox("extraTools", "Tools", 237, 25, 87, 49, this);
                    break;
                case "Zoktai":
                    extrasGroupBox = WinFormHelpers.CreateGroupBox("extraTools", "Tools", 237, 214, 87, 49, this);
                    break;
                case "Shinbok":
                    extrasGroupBox = WinFormHelpers.CreateGroupBox("extraTools", "Tools", 237, 241, 97, 49, this);
					btnWidthOffset += 10;
                    break;
                case "LunarKnights":
                    extrasGroupBox = WinFormHelpers.CreateGroupBox("extraTools", "Tools", 237, 25, 87, 49, this);
					break;
				default:
					// If game is not handled, don't add anything & stop here
					return;
			}

			// Add Misc Tools button
			Button miscToolsBtn = WinFormHelpers.CreateButton("showExtraTools", "Misc tools", 6, 21, 75 + btnWidthOffset, 23, extrasGroupBox); // 17
			miscToolsBtn.Click += new System.EventHandler(OpenMiscToolsSelector);
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

			miscDataGroupBox = WinFormHelpers.CreateGroupBox("miscData", "Misc. data", 5, positionY, 226, groupHeight, this);

			// Average speed
			_currentSpeedLabel = WinFormHelpers.CreateLabel("currentMovementSpeed", "Current movement speed : ", 7, 19, 200, 15, miscDataGroupBox, textAlignment: "MiddleLeft");
			_averageSpeedLabel = WinFormHelpers.CreateLabel("averageMovementSpeed", "Average over 60 frames : ", 7, 34, 200, 15, miscDataGroupBox, textAlignment: "MiddleLeft");

			// Add the coffin section if enabled
			if (enableCoffinSection == true) {

				WinFormHelpers.CreateSeparator("miscDataSeparator", 5, 51, 216, miscDataGroupBox);
				WinFormHelpers.CreateLabel("coffinSection", "Coffin data", 5, 53, 80, 15, miscDataGroupBox, textAlignment: "MiddleLeft").Font = new("Segoe UI", 9, FontStyle.Underline, GraphicsUnit.Point);

				// Coffin data
				_coffinDamageLabel = WinFormHelpers.CreateLabel("coffinDamage", "Damage : ", 5, 68, 200, 15, miscDataGroupBox, textAlignment: "MiddleLeft");
				_coffinWindupTimerLabel = WinFormHelpers.CreateLabel("coffinWindupTimer", "Windup begins in : ", 5, 83, 200, 15, miscDataGroupBox, textAlignment: "MiddleLeft");
				_coffinShakeTimerLabel = WinFormHelpers.CreateLabel("coffnShakeTimer", "Windup : ", 5, 98, 200, 15, miscDataGroupBox, textAlignment: "MiddleLeft");
				_coffinShakeDurationLabel = WinFormHelpers.CreateLabel("coffinShakeDuration", "Duration : ", 5, 113, 200, 15, miscDataGroupBox, textAlignment: "MiddleLeft");
				_coffinEscapeTimerLabel = WinFormHelpers.CreateLabel("coffinEscapeTimer", "Begins escaping in : ", 5, 128, 200, 15, miscDataGroupBox, textAlignment: "MiddleLeft");
				_coffinDistanceLabel = WinFormHelpers.CreateLabel("coffinDistance", "Distance : ", 5, 143, 200, 15, miscDataGroupBox, textAlignment: "MiddleLeft");
			}
		}

		#endregion
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
			extrasGroupBox = new();

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
			 * Clear the external tool window
			 * The Bok Interface supports all 4 games, so we need to do that
			 */
			this.ClearInterface();

			// Set corresponding game icon (or default icon if not available)
			this.Icon = WinFormHelpers.GetIcon(WinFormHelpers.GetGameIconName());

			// Try initializing list of memory values instances
			this._memoryValues = new(shorterGameName);

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
					// If not a Boktai game, show the "Game not recognized" window
					interfaceActivated = false;
					GameNotRecognizedWindow();
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
			this.Controls.Clear();
			this.subwindows.Clear();
			this.currentStatusGroupBox.Controls.Clear();
			this.currentStatsGroupBox.Controls.Clear();
			this.inventoryGroupBox.Controls.Clear();
			this.editGroupBox.Controls.Clear();
			this.extrasGroupBox.Controls.Clear();

			// Tools selection subwindow elements
			this.miscToolsSelectionWindow.Controls.Clear();
			this.miscToolsSelectionWindow.Close();
			this.miscToolsSelectorOpened = false;

			// Extra tools
			this.ClearExtraTools();
		}

		/// <summary>Clears subwindows related to extra tools</summary>
		private void ClearExtraTools() {

			// Tile Data Viewer
			if (this._tileDataViewer != null) {
				this._tileDataViewer.Controls.Clear();
				this._tileDataViewer.Close();
				this.tileDataViewerActive = false;
			}

			// Memory Values Listing
			if (this._memValuesListing != null) {
				this._memValuesListing.Controls.Clear();
				this._memValuesListing.Close();
				this.memValuesListingActive = false;
			}
		}

		/// <summary>Simplified method for setting the main window of the interface</summary>
		/// <param name="name">Window name</param>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		private void SetMainWindow(string name, Int32 width, Int32 height) {
			this.Name = name;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Font = WinFormHelpers.defaultFont;
			this.ClientSize = new System.Drawing.Size(width, height);
		}

		/// <summary>Adds Tools section for the corresponding game</summary>
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
			Button miscToolsBtn = WinFormHelpers.CreateButton("showExtraTools", "Misc tools", 6, 19, 75 + btnWidthOffset, 23, extrasGroupBox);
            miscToolsBtn.Click += new System.EventHandler(OpenMiscToolsSelector);
        }

		#endregion
	}
}
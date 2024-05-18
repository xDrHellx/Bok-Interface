using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

/**
 * File for the external window part of the Bok interface
 */

namespace BokInterface {

	partial class BokInterface {

		#region Main interface properties

		/// <summary>Required designer variable</summary>
		private IContainer components = null;

		/// <summary>Color for pure / base stat points (Boktai 2, 3, LK)</summary>
		public static string baseStatColor = "#FFE600";

		/// <summary>
		/// Color for stat points from equipments (Boktai 3) <br/>
		/// These points does not affect as many things as pure stat points <br/><br/>
		/// For example STR points from equipments does not affect coffin carrying speed
		/// </summary>
		public static string equipsStatColor = "#FFA529";

		/// <summary>Color for the total amount of points for a specific stat (Boktai 2, 3, LK)</summary>
		public static string totalStatColor = "#D3D3D3";

		public static System.Drawing.Font defaultFont = new("Segoe UI", 9, System.Drawing.FontStyle.Regular, GraphicsUnit.Point);
		public static System.Windows.Forms.Padding defaultMargin = new(3, 0, 3, 0);
		public static System.Windows.Forms.AnchorStyles defaultAnchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;

		#endregion

		#region Common interface elements properties
		
		private System.Windows.Forms.GroupBox currentStatusGroupBox = new();
		private System.Windows.Forms.GroupBox currentStatsGroupBox = new();
		private System.Windows.Forms.GroupBox inventoryGroupBox = new();
		private System.Windows.Forms.GroupBox editGroupBox = new();
		private System.Windows.Forms.GroupBox extrasGroupBox = new();
		private List<System.Windows.Forms.Label> currentStatusLabels = new();
		private List<System.Windows.Forms.Label> currentStatsLabels = new();
		private List<System.Windows.Forms.Button> editButtons = new();
		
		/// <summary>Tooltip for values that only updates after switching rooms</summary>
		public static System.Windows.Forms.ToolTip toolTip = BokInterface.CreateToolTip();

		#endregion

		#region Subwindows properties

		private System.Windows.Forms.Form statusEditWindow = new();
		private System.Windows.Forms.Form inventoryEditWindow = new();
		private System.Windows.Forms.Form equipsEditWindow = new();
		private System.Windows.Forms.Form solarGunEditWindow = new();
		private System.Windows.Forms.Form weaponsEditWindow = new();
		private System.Windows.Forms.Form magicsEditWindow = new();
		private System.Windows.Forms.Form miscToolsSelectionWindow = new();
		
		#endregion

		#region Common subwindows elements properties

		private List<System.Windows.Forms.Label> statusEditLabels = new();
		private List<System.Windows.Forms.Button> statusEditButtons = new();

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

			// Sets default icon if available
			this.Icon = this.GetIcon("nero");

			// Try initializing list of memory values instances
			this.memoryValues = new(shorterGameName);

			/**
			 * If not a Boktai game, shows the "Game not recognized" window
			 * Otherwise, shows the window for the corresponding game
			 */
			if(supportedGame == false) {
				GameNotRecognizedWindow();
			} else {

				// Set corresponding game icon
				this.Icon = this.GetIcon(this.GetGameIconName());

				// Show corresponding interface
				switch(shorterGameName) {
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
						// Just in case, show the "Game not recognized" window if the game is not handled via the switch
						interfaceActivated = false;
						GameNotRecognizedWindow();
						break;
				}
			}
		}

		/// <summary>Clears the interface window and all other sections within it</summary>
		private void ClearInterface() {

			// Main window-related
			this.Controls.Clear();

			this.currentStatusGroupBox.Controls.Clear();
			this.currentStatsGroupBox.Controls.Clear();
			this.inventoryGroupBox.Controls.Clear();
			this.editGroupBox.Controls.Clear();
			this.extrasGroupBox.Controls.Clear();

			this.currentStatusLabels.Clear();
			this.currentStatsLabels.Clear();
			this.editButtons.Clear();

			// Status edit subwindow-related
			this.statusEditWindow.Controls.Clear();
			this.statusEditLabels.Clear();
			this.statusEditButtons.Clear();
			this.statusEditWindow.Close();
			this.statusEditorOpened = false;

			// Tools selection subwindow-related
			this.miscToolsSelectionWindow.Controls.Clear();
			this.miscToolsSelectionWindow.Close();
			this.miscToolsSelectorOpened = false;

			// Extra tools-related
			this.ClearExtraTools();
		}

		/// <summary>Clears subwindows related to extra tools</summary>
		private void ClearExtraTools() {

			// Tile Data Viewer-related
			if(this.TileDataViewer != null) {
				this.TileDataViewer.Controls.Clear();
				this.TileDataViewer.Close();
				this.tileDataViewerActive = false;
			}

			if(this.MemoryValuesListing != null) {
				this.MemoryValuesListing.Controls.Clear();
				this.MemoryValuesListing.Close();
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
			this.Font = BokInterface.defaultFont;
			this.ClientSize = new System.Drawing.Size(width, height);
		}

		/// <summary>Get the specified icon if it exist</summary>
		/// <param name="fileName">File name (without .ico extension)</param>
		/// <returns><c>System.Drawing.Icon</c>Specified Icon instance (or default if the specified icon could not be found)</returns>
		private System.Drawing.Icon GetIcon(string fileName) {
			return fileName == "" ? this.Icon : (Icon)Properties.Resources.ResourceManager.GetObject(fileName);
		}

		#endregion
	}
}
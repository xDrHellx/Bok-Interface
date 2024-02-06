using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using  System.Resources.Extensions;

/**
 * File for the external window part of the Bok interface
 */

namespace BokInterface {

	partial class BokInterfaceMainForm {

		/// <summary>Required designer variable</summary>
		private IContainer components = null;
		
		#region Common interface variables

		/// <summary>Color for pure / base stat points (Boktai 2, 3, LK)</summary>
		public static string baseStatColor = "#FFE600";

		/// <summary>
		/// Color for stat points from equipments (Boktai 3)<br/>
		/// These points does not affect as many things as pure stat points <br/><br/>
		/// For example STR points from equipments does not affect coffin carrying speed
		/// </summary>
		public static string equipsStatColor = "#FFA529";

		/// <summary>Color for the total amount of points for a specific stat (Boktai 2, 3, LK)</summary>
		public static string totalStatColor = "#FFD3D3D3";

		public static System.Drawing.Font defaultFont = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		protected static System.Windows.Forms.Padding defaultMargin = new System.Windows.Forms.Padding(3, 0, 3, 0);
		protected static System.Windows.Forms.AnchorStyles defaultAnchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;

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
				switch(shorterGameName) {
					case "Boktai":
						interfaceActivated = true;
						this.Icon = this.GetIcon(this.GetGameIconName());
						ShowBoktaiInterface();
						break;
					case "Zoktai":
						interfaceActivated = true;
						this.Icon = this.GetIcon(this.GetGameIconName());
						ShowZoktaiInterface();
						break;
					case "Shinbok":
						interfaceActivated = true;
						this.Icon = this.GetIcon(this.GetGameIconName());
						ShowShinbokInterface();
						break;
					case "LunarKnights":
						interfaceActivated = true;
						this.Icon = this.GetIcon(this.GetGameIconName());
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
			this.statusEditing = false;

			// Tools selection subwindow-related
			this.miscToolsSelectionWindow.Controls.Clear();
			this.miscToolsSelectionWindow.Close();
			this.miscToolsSelecting = false;

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
			this.Font = BokInterfaceMainForm.defaultFont;
			this.ClientSize = new System.Drawing.Size(width, height);
		}

		/// <summary>Adds Tools section for the corresponding game</summary>
		private void AddToolsSection() {

			switch(shorterGameName) {
				case "Boktai":
					this.extrasGroupBox = this.CreateGroupBox("extraTools", "Tools", 237, 25, 87, 52, true);
					break;
				case "Zoktai":
					this.extrasGroupBox = this.CreateGroupBox("extraTools", "Tools", 237, 25, 87, 52, true);
					break;
				case "Shinbok":
					this.extrasGroupBox = this.CreateGroupBox("extraTools", "Tools", 237, 187, 87, 52, true);
					break;
				case "LunarKnights":
					this.extrasGroupBox = this.CreateGroupBox("extraTools", "Tools", 237, 25, 87, 52, true);
					break;
				default:
					// If game is not handled, don't add anything & stop here
					return;
			}

			// Add Misc Tools button
			System.Windows.Forms.Button miscToolsBtn = CreateButton("showExtraTools", "Misc tools", 6, 21, 75, 23);
			miscToolsBtn.Click += new System.EventHandler(this.OpenMiscToolsSelection);
			this.extrasGroupBox.Controls.Add(miscToolsBtn);
		}

		/// <summary>Get the specified icon if it exist</summary>
		/// <param name="fileName">File name (without .ico extension)</param>
		/// <returns><c>System.Drawing.Icon</c>Specified Icon instance (or default if the specified icon could not be found)</returns>
		private System.Drawing.Icon GetIcon(string fileName) {
			if(fileName == "") {
				return this.Icon;
			} else {
				return (Icon)Properties.Resources.ResourceManager.GetObject(fileName);
			}
		}

		/// <summary>Get the name of the icon corresponding to the current game</summary>
		/// <returns><c>string</c>Icon name (empty if the current game is not a Boktai game)</returns>
		/// 
		private string GetGameIconName() {
			switch(shorterGameName) {
				case "Boktai":
					return "lita";
				case "Zoktai":
					return "ringo";
				case "Shinbok":
					return "trinity";
				case "LunarKnights":
					return "lucian";
				default:
					return "";
			}
		}

		#endregion

		#region Form elements generating methods

		/// <summary>Simplified method for creating a group box</summary>
		/// <param name="name">Group name</param>
		/// <param name="text">Group text</param>
		/// <param name="positionX">X position</param>
		/// <param name="positionY">Y position</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
		/// <param name="addToWindow">Set to true to add the element directly to the main interface window</param>
		/// <returns><c>System.Windows.Forms.GroupBox</c>Group box instance</returns>
		private System.Windows.Forms.GroupBox CreateGroupBox(string name, string text, Int32 positionX, Int32 positionY, Int32 width, Int32 height, bool addToWindow = false) {

			System.Windows.Forms.GroupBox groupBox = new();
			groupBox.Name = name;
			groupBox.Text = text;
			groupBox.Location = new System.Drawing.Point(positionX, positionY);
			groupBox.Size = new System.Drawing.Size(width, height);
            groupBox.AutoSize = false;
			groupBox.TabIndex = 1;
			groupBox.Anchor = BokInterfaceMainForm.defaultAnchor;
			groupBox.Font = BokInterfaceMainForm.defaultFont;

			if(addToWindow == true) {
				this.Controls.Add(groupBox);
			}

			return groupBox;
		}

		/// <summary>Simplified method for creating a label</summary>
		/// <param name="name">Label name</param>
		/// <param name="text">Label text</param>
		/// <param name="positionX">X position</param>
		/// <param name="positionY">Y position</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
		/// <param name="addToWindow">Set to true to add the element directly to the main interface window</param>
		/// <param name="colorHex">Set the background color for the label</param>
		/// <param name="margin">Margin (by default System.Windows.Forms.Padding(0, 3, 0, 3), the default value in Visual Studio)</param>
		/// <param name="textAlignment">Text alignment, by default "MiddleCenter" (see System.Drawing.ContentAlignment for possible values)</param>
		/// <returns><c>System.Windows.Forms.Label</c>Label instance</returns>
		private System.Windows.Forms.Label CreateLabel(string name, string text, Int32 positionX, Int32 positionY, Int32 width, Int32 height, bool addToWindow = false, string colorHex = "", System.Windows.Forms.Padding margin = new System.Windows.Forms.Padding(), string textAlignment = "MiddleCenter") {

			System.Windows.Forms.Label label = new();
			label.Name = name;
			label.Text = text;
			label.Location = new System.Drawing.Point(positionX, positionY);
			label.Size = new System.Drawing.Size(width, height);
            label.AutoSize = false;
			label.TabIndex = 2;
			label.Anchor = BokInterfaceMainForm.defaultAnchor;
			label.Margin = BokInterfaceMainForm.defaultMargin;
			label.Font = BokInterfaceMainForm.defaultFont;
			
			if(colorHex != "") {
				label.BackColor = System.Drawing.ColorTranslator.FromHtml(colorHex);
			}

			// If no specific margin is passed, set defaults from Visual Studio
			if(margin.All == 0) {
				margin.Top = 3;
				margin.Left = 3;
			}

			// Text alignment
			switch(textAlignment){
				case "BottomCenter" :
					label.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
					break;
				case "BottomLeft" :
					label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
					break;
				case "BottomRight" :
					label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
					break;
				case "MiddleLeft" :
					label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
					break;
				case "MiddleRight" :
					label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
					break;
				case "TopCenter" :
					label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
					break;
				case "TopLeft" :
					label.TextAlign = System.Drawing.ContentAlignment.TopLeft;
					break;
				case "TopRight" :
					label.TextAlign = System.Drawing.ContentAlignment.TopRight;
					break;
				default:
					label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
					break;
			}

			if(addToWindow == true) {
				this.Controls.Add(label);
			}

			return label;
		}

		/// <summary>Simplified method for creating a button</summary>
		/// <param name="name">Label name</param>
		/// <param name="text">Label text</param>
		/// <param name="positionX">X position</param>
		/// <param name="positionY">Y position</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
		/// <param name="addToWindow">Set to true to add the element directly to the main interface window</param>
		/// <param name="colorHex">Set the background color for the label</param>
		/// <param name="margin">Margin (by default System.Windows.Forms.Padding(0, 3, 0, 3), the default value in Visual Studio)</param>
		/// <param name="textAlignment">Text alignment, by default "MiddleCenter" (see System.Drawing.ContentAlignment for possible values)</param>
		/// <returns><c>System.Windows.Forms.Button</c>Button instance</returns>
		private System.Windows.Forms.Button CreateButton(string name, string text, Int32 positionX, Int32 positionY, Int32 width, Int32 height, bool addToWindow = false, string colorHex = "", System.Windows.Forms.Padding margin = new System.Windows.Forms.Padding(), string textAlignment = "MiddleCenter") {

			System.Windows.Forms.Button btn = new();
			btn.Name = name;
			btn.Text = text;
			btn.Location = new System.Drawing.Point(positionX, positionY);
			btn.Size = new System.Drawing.Size(width, height);
			btn.AutoSize = false;
			btn.TabIndex = 2;
			btn.Anchor = BokInterfaceMainForm.defaultAnchor;
			btn.Margin = BokInterfaceMainForm.defaultMargin;
			btn.Font = BokInterfaceMainForm.defaultFont;
			
			if(colorHex != "") {
				btn.BackColor = System.Drawing.ColorTranslator.FromHtml(colorHex);
			}

			// If no specific margin is passed, set defaults from Visual Studio
			if(margin.All == 0) {
				margin.Top = 3;
				margin.Left = 3;
			}

			// Text alignment
			switch(textAlignment){
				case "BottomCenter" :
					btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
					break;
				case "BottomLeft" :
					btn.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
					break;
				case "BottomRight" :
					btn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
					break;
				case "MiddleLeft" :
					btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
					break;
				case "MiddleRight" :
					btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
					break;
				case "TopCenter" :
					btn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
					break;
				case "TopLeft" :
					btn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
					break;
				case "TopRight" :
					btn.TextAlign = System.Drawing.ContentAlignment.TopRight;
					break;
				default:
					btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
					break;
			}

			if(addToWindow == true) {
				this.Controls.Add(btn);
			}

			return btn;
		}

		/// <summary>Simplified method for creating a new sub window (AKA windows form)</summary>
		/// <param name="name">Subwindow name</param>
		/// <param name="title">Subwindow title</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
		/// <param name="icon">Subwindow icon (by default retrieves the one from the main interface window)</param>
		/// <param name="parentForm">Form the subwindow is attached to (this will make the subwindow always show in front of its parent, by default it shows in front of the main window)</param>
		/// <returns><c>System.Windows.Forms.Form</c>Subwindow instance</returns>
		private System.Windows.Forms.Form CreateSubWindow(string name, string title, Int32 width, Int32 height, string icon = "", System.Windows.Forms.Form parentForm = null) {
			
			System.Windows.Forms.Form form = new();
			form.Name = name;
			form.Text = title;
			form.Icon = this.GetIcon(icon);
			form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
			form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			form.BackColor = System.Drawing.SystemColors.Control;
			form.Font = BokInterfaceMainForm.defaultFont;
			form.ClientSize = new System.Drawing.Size(width, height);

			if(parentForm == null) {
				form.Owner = this;
			} else {
				form.Owner = parentForm;
			}

			return form;
		}

		/// <summary>Simplified method for creating a numeric input field</summary>
		/// <param name="name">Field name</param>
		/// <param name="defaultValue">Default value</param>
		/// <param name="positionX">X position</param>
		/// <param name="positionY">Y position</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
		/// <param name="minValue">Minimum settable value</param>
		/// <param name="maxValue">Maximum settable value</param>
		/// <param name="addToWindow">Set to true to add the element directly to the main interface window</param>
		/// <param name="colorHex">Set the background color for the label</param>
		/// <param name="margin">Margin (by default System.Windows.Forms.Padding(0, 3, 0, 3), the default value in Visual Studio)</param>
		/// <param name="valueAlignment">Value alignment, by default "Left" (see System.Windows.Forms.HorizontalAlignment for possible values)</param>
		/// <returns><c>System.Windows.Forms.NumericUpDown</c>NumericUpDown instance</returns>
		private System.Windows.Forms.NumericUpDown CreateNumericUpDown(string name, decimal defaultValue, Int32 positionX, Int32 positionY, Int32 width, Int32 height, decimal minValue = 1, decimal maxValue = 100, bool addToWindow = false, string colorHex = "", System.Windows.Forms.Padding margin = new System.Windows.Forms.Padding(), string valueAlignment = "Left") {
			
			System.Windows.Forms.NumericUpDown field = new();
			field.Name = name;
			field.Minimum = minValue;
			field.Maximum = maxValue;
			field.Value = defaultValue;
			field.Location = new System.Drawing.Point(positionX, positionY);
			field.Size = new System.Drawing.Size(width, height);
			field.AutoSize = false;
			field.TabIndex = 2;
			field.Anchor = BokInterfaceMainForm.defaultAnchor;
			field.Margin = BokInterfaceMainForm.defaultMargin;
			field.Font = BokInterfaceMainForm.defaultFont;
			field.Increment = (maxValue > 500 ? 10 : 1);
			field.DecimalPlaces = 0;
			
			if(colorHex != "") {
				field.BackColor = System.Drawing.ColorTranslator.FromHtml(colorHex);
			}

			// If no specific margin is passed, set defaults from Visual Studio
			if(margin.All == 0) {
				margin.Top = 3;
				margin.Left = 3;
			}

			// Value alignment
			switch(valueAlignment){
				case "Right" :
					field.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
					break;
				case "Center" :
					field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
					break;
				default:
					field.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
					break;
			}

			if(addToWindow == true) {
				this.Controls.Add(field);
			}

			return field;
		}

		#endregion

		// Interface elements that exists for all Boktai games
		#region Common interface elements
		
		private System.Windows.Forms.GroupBox currentStatusGroupBox = new();
		private System.Windows.Forms.GroupBox currentStatsGroupBox = new();
		private System.Windows.Forms.GroupBox inventoryGroupBox = new();
		private System.Windows.Forms.GroupBox editGroupBox = new();
		private System.Windows.Forms.GroupBox extrasGroupBox = new();
		private List<System.Windows.Forms.Label> currentStatusLabels = new();
		private List<System.Windows.Forms.Label> currentStatsLabels = new();
		private List<System.Windows.Forms.Button> editButtons = new();

		#endregion

		#region Subwindows

		private System.Windows.Forms.Form statusEditWindow = new();
		private System.Windows.Forms.Form inventoryEditWindow = new();
		private System.Windows.Forms.Form equipsEditWindow = new();
		private System.Windows.Forms.Form solarGunEditWindow = new();
		private System.Windows.Forms.Form weaponsEditWindow = new();
		private System.Windows.Forms.Form magicsEditWindow = new();
		private System.Windows.Forms.Form miscToolsSelectionWindow = new();
		
		#endregion

		#region Common subwindows elements

		private List<System.Windows.Forms.Label> statusEditLabels = new();
		private List<System.Windows.Forms.Button> statusEditButtons = new();

		#endregion
	}
}
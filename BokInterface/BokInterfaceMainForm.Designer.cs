using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

/**
 * File for the external window part of the Bok interface
 */

namespace BokInterface {

	partial class BokInterfaceMainForm {

		/// <summary>Required designer variable</summary>
		private IContainer components = null;

		/// <summary>Color for pure stat points (Boktai 2, 3, LK)</summary>
		private static string baseStatColor = "#FFE600";

		/// <summary>
		/// Color for stat points from equipments (Boktai 3)<br/>
		/// These points does not affect as many things as pure stat points <br/><br/>
		/// For example STR points from equipments does not affect coffin carrying speed
		/// </summary>
		private static string equipsStatColor = "#FFA529";

		/// <summary>Color for the total stat points for a specific stat (Boktai 2, 3, LK)</summary>
		private static string totalStatColor = "#FFD3D3D3";

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

			// Set the icon if it exists
			string iconPath = "../BokInterface/icon/ringo.ico";
        	if(File.Exists(iconPath) == true) {
				this.Icon = new System.Drawing.Icon(iconPath);
			}

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
			this.Controls.Clear();
			this.currentStatusGroupBox.Controls.Clear();
			this.currentStatsGroupBox.Controls.Clear();
			this.inventoryGroupBox.Controls.Clear();
			this.currentStatusLabels.Clear();
			this.currentStatsLabels.Clear();
		}

		/// <summary>Shows the "Game not recognized" window</summary>
		private void GameNotRecognizedWindow() {
			
			// Current game name
			this.CreateLabel("currentGameName", "Game not recognized!", 5, 5, 123, 20, true);

			// Window
			this.SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 350, 100);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BokInterfaceMainForm_FormClosing);
			this.Load += new System.EventHandler(this.BokInterfaceMainForm_Load);

			this.ResumeLayout(false);
		}

		/// <summary>Simplified method for setting the main window of the interface</summary>
		/// <param name="name">Window name</param>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		private void SetMainWindow(string name, Int32 width, Int32 height) {
			this.Name = name;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(width, height);
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
		/// <param name="addToWindow">Set to true to add the element directly to the window</param>
		private System.Windows.Forms.GroupBox CreateGroupBox(string name, string text, Int32 positionX, Int32 positionY, Int32 width, Int32 height, bool addToWindow = false) {

			System.Windows.Forms.GroupBox groupBox = new System.Windows.Forms.GroupBox();
			groupBox.Name = name;
            groupBox.AutoSize = false;
			groupBox.Location = new System.Drawing.Point(positionX, positionY);
			groupBox.Size = new System.Drawing.Size(width, height);
			groupBox.TabIndex = 1;
			groupBox.Text = text;
			groupBox.Font = new System.Drawing.Font("Segoe Ui", 9f);

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
		/// <param name="addToWindow">Set to true to add the element directly to the window</param>
		/// <param name="colorHex">Set the background color for the label</param>
		/// <param name="margin">Margin (by default System.Windows.Forms.Padding(0, 3, 0, 3), the default value in Visual Studio)</param>
		/// <param name="textAlignment">Text alignment, by default "MiddleCenter" (see System.Drawing.ContentAlignment for possible values)</param
		private System.Windows.Forms.Label CreateLabel(string name, string text, Int32 positionX, Int32 positionY, Int32 width, Int32 height, bool addToWindow = false, string colorHex = "", System.Windows.Forms.Padding margin = new System.Windows.Forms.Padding(), string textAlignment = "MiddleCenter") {

			System.Windows.Forms.Label label = new System.Windows.Forms.Label();
			label.Name = name;
            label.AutoSize = false;
			label.Location = new System.Drawing.Point(positionX, positionY);
			label.Size = new System.Drawing.Size(width, height);
			label.TabIndex = 2;
			label.Text = text;
			label.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			label.Font = new System.Drawing.Font("Segoe Ui", 9f);
			
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

		#endregion

		// Interface elements that exists for all Boktai games
		#region Common interface elements
		
		private System.Windows.Forms.GroupBox currentStatusGroupBox = new System.Windows.Forms.GroupBox();
		private System.Windows.Forms.GroupBox currentStatsGroupBox = new System.Windows.Forms.GroupBox();
		private System.Windows.Forms.GroupBox inventoryGroupBox = new System.Windows.Forms.GroupBox();
		private List<System.Windows.Forms.Label> currentStatusLabels = new List<System.Windows.Forms.Label>();
		private List<System.Windows.Forms.Label> currentStatsLabels = new List<System.Windows.Forms.Label>();

		#endregion
	}
}
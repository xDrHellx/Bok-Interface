using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BokInterface.All;
using System.Drawing;

/**
 * File for the main / initialization part of the Bok interface
 */

namespace BokInterface {

	/// <summary>Main class for the Bok Interface</summary>
	[ExternalTool("BokInterface")]
	public partial class BokInterfaceMainForm : ToolFormBase, IExternalToolForm {

		protected override string WindowTitleStatic => "Bok Interface";
		public override bool BlocksInputWhenFocused => false;
		protected Icon icon;
		protected Utilities utils;
		public uint currentGameId;
		public string currentGameName = "";
		public string shorterGameName = "";
		protected int count = 0;
		protected bool interfaceActivated = false;

		public ApiContainer ApiContainer {
			get => APIs.ApiContainer;
			set => APIs.Update(value);
		}

		#region Main methods
		
		public BokInterfaceMainForm() {

			// Get / instanciate utilities
			utils = new Utilities();
			
			/**
			 * We use a try - catch to prevent the tool from returning an error when no ROM is loaded
			 * When no ROM is loaded, memory domains aren't accessible
			 */
			try {
	            // Get & set the infos about the game currently running on BizHawk
				DetectCurrentGame();
				ShowInterfaceIndicator();
			} catch {}

			InitializeComponent();
		}

		/// <summary>Executed once after the constructor, and again every time a rom is loaded or reloaded</summary>
		public override void Restart() {

			// Update the APIs, as some of them might not be available if a game is not loaded
			APIs.Update(MainForm);

			/**
			 * We use a try - catch to prevent the tool from returning an error when no ROM is loaded
			 * When no ROM is loaded, memory domains aren't accessible
			 */
			try {
	            // Get & set the infos about the game currently running on BizHawk
				DetectCurrentGame();
				if(interfaceActivated == true) {
					ShowInterfaceIndicator();
				}
			} catch {}

			InitializeComponent();
		}

		/// <summary>Executed after every frame (except while turboing, use FastUpdateAfter for that)</summary>
		protected override void UpdateAfter() {

			try {
				if(interfaceActivated == true) {
					ShowInterfaceIndicator();
					
					// Run the corresponding method to update values in the interface
					switch(shorterGameName) {
						case "Boktai":
							UpdateBoktaiInterface();
							break;
						case "Zoktai":
							UpdateZoktaiInterface();
							break;
						case "Shinbok":
							UpdateShinbokInterface();
							break;
						default:
							// Nothing to do here
							break;
					}
				}
			} catch {}
		}
		
		/// <summary>
		/// Detects the current game <br/>
		/// This stores the game's ID in currentGameId and its name in currentGameName
		/// </summary>
		protected void DetectCurrentGame() {
			
			currentGameId = utils.GetGameCode();
			
			switch(currentGameId) {
				case 1346974549:	// EU
				case 1162425173:	// US
				case 1246311253: 	// JP
					currentGameName = "Boktai: The Sun is in Your Hand";
					shorterGameName = "Boktai";
					interfaceActivated = true;
					break;
				case 1345467221:	// EU
				case 1160917845:	// US
				case 1244803925:	// JP 1.0 & 1.1
					currentGameName = "Boktai 2: Solar Boy Django";
					shorterGameName = "Zoktai";
					interfaceActivated = true;
					break;
				case 1244869461:
					currentGameName = "Boktai 3: Sabata's Counterattack";
					shorterGameName = "Shinbok";
					interfaceActivated = true;
					break;
				case 1481329729:	// EU 1.1
				case 1347112001:	// EU 1.0
				case 1162562625:	// US
				case 1246448705:	// JP
					currentGameName = "Boktai DS | Lunar Knights";
					shorterGameName = "LunarKnights";
					interfaceActivated = true;
					break;
				default:
					currentGameName = "";
					shorterGameName = "";
					interfaceActivated = false;
					break;
			}
		}

		#endregion

		#region Events

		protected void BokInterfaceMainForm_Load(object sender, EventArgs e) { }

		protected void BokInterfaceMainForm_FormClosing(object sender, FormClosingEventArgs e) { }
		
		#endregion
	}
}
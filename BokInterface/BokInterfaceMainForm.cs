using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BokInterface.All;

/**
 * File for the main / initialization part of the Bok interface
 */

namespace BokInterface {

	/// <summary>Main class for the Bok Interface</summary>
	[ExternalTool("BokInterface")]
	public partial class BokInterfaceMainForm : ToolFormBase, IExternalToolForm {

		protected override string WindowTitleStatic => "Bok Interface";
		public override bool BlocksInputWhenFocused => false;
		protected Icon? icon;
		public uint currentGameId;
		public string currentGameName = "";
		public string shorterGameName = "";
		protected bool supportedGame = false;
		protected bool interfaceActivated = false;
		protected bool isDS = false;
		protected int retryCount = 0;

		private MemoryValues memoryValues;

		/// <summary>List of functions to call each frame</summary>
		public static List<Action> functionsList = new();

		public ApiContainer ApiContainer {
			get => APIs.ApiContainer;
			set => APIs.Update(value);
		}

		#region Main methods
		
		public BokInterfaceMainForm() {
			
			// Try initializing the interface
			InitializeInterface();
		}

		/// <summary>Executed once after the constructor, and again every time a rom is loaded or reloaded</summary>
		public override void Restart() {

			// Update the APIs, as some of them might not be available if a game is not loaded
			APIs.Update(MainForm);

			// Reset the retry count & isDS indicator
			retryCount = 0;
			isDS = false;

			// Try initializing the interface
			InitializeInterface();
		}

		/// <summary>Method used for initializing the interface</summary>
		protected void InitializeInterface() {

			// Reset variables used for initializing
			supportedGame = false;
			interfaceActivated = false;

			/**
			 * We use a try - catch to prevent the tool from returning an error when no ROM is loaded
			 * When no ROM is loaded, memory domains aren't accessible
			 */
			try {
	            // Get & set the infos about the game currently running on BizHawk
				DetectCurrentGame();
				if(interfaceActivated == true) {
					ShowInterfaceIndicator();
				} else {
					/**
					 * Retry getting the game code
					 * For DS games, because of the DS bootup screen, the game code is not always accessible after switching games
					 * 
					 * 10 frames should be enough for this
					 */
					if(retryCount < 10){
						retryCount++;
						DetectCurrentGame();
					}
				}
			} catch {}

			InitializeComponent();
		}

		/// <summary>Executed after every frame (except while turboing, use FastUpdateAfter for that)</summary>
		protected override void UpdateAfter() {
			
			try {
				if(supportedGame == true) {
					ShowInterfaceIndicator();

					// If the interface is not activated, reinitialize it
					if(interfaceActivated == false){
						InitializeComponent();
					}

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
						case "LunarKnights":
							UpdateLunarKnightsInterface();
							break;
						default:
							// Nothing to do here
							break;
					}

					// Loop on the list of functions to call each frame
					foreach(Action function in functionsList) {
						function();
					}
				} else {
					
					/**
					 * Retry getting the game code
					 * For DS games, because of the DS bootup screen, the game code is not always accessible after switching games
					 * 
					 * 10 frames should be enough for this
					 */
					if(retryCount < 10){
						retryCount++;
						DetectCurrentGame();
					}
				}
			} catch {}
		}
		
		/// <summary>
		/// Detects the current game <br/>
		/// This stores the game's ID in currentGameId and its name in currentGameName
		/// </summary>
		protected void DetectCurrentGame() {
			
			/**
			 * Try getting the game code
			 * If the game code is 0 or 4267703902, it's most likely not a GBA game & we need to try different memory addresses
			 */
			currentGameId = Utilities.GetGbaGameCode();
			if(new string[] {"4267703902", "0"}.Contains(currentGameId.ToString()) == true){
				currentGameId = Utilities.GetDsGameCode();
			}

			switch(currentGameId) {
				case 1346974549:	// EU
				case 1162425173:	// US
				case 1246311253: 	// JP
					currentGameName = "Boktai: The Sun is in Your Hand";
					shorterGameName = "Boktai";
					supportedGame = true;
					isDS = false;
					break;
				case 1345467221:	// EU
				case 1160917845:	// US
				case 1244803925:	// JP 1.0 & 1.1
					currentGameName = "Boktai 2: Solar Boy Django";
					shorterGameName = "Zoktai";
					supportedGame = true;
					isDS = false;
					break;
				case 1244869461:
					currentGameName = "Boktai 3: Sabata's Counterattack";
					shorterGameName = "Shinbok";
					supportedGame = true;
					isDS = false;
					break;
				case 1481329729:	// EU 1.1
				case 1347112001:	// EU 1.0
				case 1162562625:	// US
				case 1246448705:	// JP
					currentGameName = "Boktai DS - Lunar Knights";
					shorterGameName = "LunarKnights";
					supportedGame = true;
					isDS = true;
					break;
				default:
					currentGameName = "";
					shorterGameName = "";
					supportedGame = false;
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
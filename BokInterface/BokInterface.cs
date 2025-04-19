using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using BokInterface.All;

/**
 * File for the main / initialization part of the Bok interface
 */

namespace BokInterface {

    /// <summary>Main class for the Bok Interface</summary>
    [ExternalTool(
        "Bok Interface",
        Description = "External tool to aid with routing and the research of Boktai games"
    )]
    [ExternalToolEmbeddedIcon("BokInterface.img.django_head_16.png")]
    public partial class BokInterface : ToolFormBase, IExternalToolForm {

        #region Properties

        protected override string WindowTitleStatic => "Bok Interface v0.1.9a";
        public override bool BlocksInputWhenFocused => false;
        protected Icon? icon;
        public uint currentGameId;
        public static string currentGameName = "",
            shorterGameName = "";
        protected bool supportedGame = false,
            interfaceActivated = false,
            isDS = false;
        protected int retryCount = 0;
        private bool _previousDisplayMessagesSetting = true;
        public bool _previousIsPauseSetting = false;

        /// <summary>
        /// List of MemoryValues instances <br/>
        /// These are used for simplyfing getting and setting values from memory addresses, especially the ones that are "dynamic"
        /// </summary>
        private MemoryValues _memoryValues = new("");

        /// <summary>List of functions to call each frame</summary>
        public static List<Action> functionsList = [];

        public ApiContainer ApiContainer {
            get => APIs.ApiContainer;
            set => APIs.Update(value);
        }

        #endregion

        #region Main methods

        public BokInterface() {

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

            // Clear subwindows related to calculators & extra tools to prevent errors caused by switching between games
            ClearCalculators();
            ClearExtraTools();

            // Try initializing the interface
            InitializeInterface();
        }

        /// <summary>Method used for initializing the interface</summary>
        protected void InitializeInterface() {

            // Reset variables used for initializing
            supportedGame = interfaceActivated = false;

            /**
             * We use a try - catch to prevent the tool from returning an error when no ROM is loaded
             * When no ROM is loaded, memory domains aren't accessible
             */
            try {

                // Get the current setting for displaying messages
                _previousDisplayMessagesSetting = APIs.Config.DisplayMessages;

                // Get & set the infos about the game currently running on BizHawk
                DetectCurrentGame();
                if (interfaceActivated == true) {
                    ShowInterfaceIndicator();
                } else {
                    /**
                     * Retry getting the game code
                     * For DS games, because of the DS bootup screen, the game code is not always accessible after switching games
                     *
                     * 10 frames should be enough for this
                     */
                    if (retryCount < 10) {
                        retryCount++;
                        DetectCurrentGame();
                    }
                }
            } catch { }

            InitializeComponent();
        }

        /// <summary>Executed after every frame (except while turboing, use FastUpdateAfter for that)</summary>
        protected override void UpdateAfter() {

            try {
                if (supportedGame == true) {
                    ShowInterfaceIndicator();

                    // If the interface is not activated, reinitialize it
                    if (interfaceActivated == false) {
                        InitializeComponent();
                    }

                    // Force messages to be displayed
                    APIs.Client.DisplayMessages(true);

                    // Run the corresponding method to update values in the interface
                    switch (shorterGameName) {
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
                            // If game is not handled, put back the old setting for displaying messages
                            APIs.Client.DisplayMessages(_previousDisplayMessagesSetting);
                            break;
                    }

                    /**
                     * Check if we're past the GBA boot up screen
                     *
                     * Otherwise the emulator can crash if we try reading values from memory addresses,
                     * most likely because it reads "garbage" data
                     */
                    if (APIs.Emulation.FrameCount() >= 400) {

                        // Loop on the list of functions to call each frame
                        foreach (Action function in functionsList) {
                            function();
                        }
                    }
                } else {

                    /**
                     * Retry getting the game code
                     * For DS games, because of the DS bootup screen, the game code is not always accessible after switching games
                     *
                     * 10 frames should be enough for this
                     */
                    if (retryCount < 10) {
                        retryCount++;
                        DetectCurrentGame();
                    }
                }
            } catch { }
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
            if (new string[] { "4267703902", "0" }.Contains(currentGameId.ToString()) == true) {
                currentGameId = Utilities.GetDsGameCode();
            }

            switch (currentGameId) {
                case 1346974549:    // EU
                case 1162425173:    // US
                case 1246311253:    // JP
                    currentGameName = "Boktai: The Sun is in Your Hand";
                    shorterGameName = "Boktai";
                    supportedGame = true;
                    isDS = false;
                    break;
                case 1345467221:    // EU
                case 1160917845:    // US
                case 1244803925:    // JP 1.0 & 1.1
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
                case 1481329729:    // EU 1.1
                case 1347112001:    // EU 1.0
                case 1162562625:    // US
                case 1246448705:    // JP
                    currentGameName = "Boktai DS - Lunar Knights";
                    shorterGameName = "LunarKnights";
                    supportedGame = true;
                    isDS = true;
                    break;
                default:
                    currentGameName = shorterGameName = "";
                    supportedGame = false;
                    break;
            }
        }

        #endregion

        #region Events

        protected void BokInterface_Load(object sender, EventArgs e) { }

        protected void BokInterface_FormClosing(object sender, FormClosingEventArgs e) {

            // Put back the old setting for displaying messages
            APIs.Client.DisplayMessages(_previousDisplayMessagesSetting);
        }

        #endregion
    }
}

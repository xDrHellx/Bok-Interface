using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

using BokInterface.Utils;
using BokInterface.Calculators;
using BokInterface.Addresses;

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

        #region Tool properties

        protected override string WindowTitleStatic => "Bok Interface v0.1.9a";
        public override bool BlocksInputWhenFocused => false;
        protected Icon? icon;
        public uint currentGameId;
        public static string currentGameName = "",
            shorterGameName = "";
        private bool _supportedGame = false,
            _interfaceActivated = false,
            _isDS = false,
            _previousDisplayMessagesSetting = true;
        private int _retryCount = 0;
        public bool _previousIsPauseSetting = false;
        /// <summary>List of functions to call each frame</summary>
        public static List<Action> functionsList = [];

        public ApiContainer ApiContainer {
            get => APIs.ApiContainer;
            set => APIs.Update(value);
        }

        #endregion

        #region Instances

        /// <summary>
        ///     List of MemoryValues instances.<br/>
        ///     These are used for simplyfing getting and setting values from memory addresses that are "dynamic."
        /// </summary>
        private MemoryValues _memoryValues = new("");
        /// <summary>Movement calculator instance</summary>
        private MovementCalculator _movementCalculator = new();

        #endregion

        #region Subwindows

        private readonly List<Form> _subwindows = [];
        public bool statusEditorOpened = false,
            inventoryEditorOpened = false,
            keyItemsEditorOpened = false,
            equipsEditorOpened = false,
            solarGunEditorOpened = false,
            weaponsEditorOpened = false,
            magicsEditorOpened = false,
            tileDataViewerActive = false,
            memValuesListingActive = false,
            solarBankInterestsSimActive = false;

        #endregion

        #region Common elements

        private MenuStrip _menuBar = new();
        private GroupBox _currentStatusGroupBox = new(),
            _currentStatsGroupBox = new(),
            _miscDataGroupBox = new();

        // Misc data labels
        private Label _averageSpeedLabel = new(),
            _currentSpeedLabel = new(),
            _coffinDamageLabel = new(),
            _coffinWindupTimerLabel = new(),
            _coffinShakeTimerLabel = new(),
            _coffinShakeDurationLabel = new(),
            _coffinEscapeTimerLabel = new(),
            _coffinDistanceLabel = new();

        #endregion

        #region Init

        public BokInterface() {
            InitializeInterface();
        }

        /// <summary>Executed once after the constructor, and again every time a rom is loaded or reloaded</summary>
        public override void Restart() {

            // Update the APIs, as some of them might not be available if a game is not loaded
            APIs.Update(MainForm);

            // Reset the variables for initializing the corresponding game's interface
            ResetInitializationVariables();

            // Try initializing the interface again
            InitializeInterface();
        }

        /// <summary>Initialize the interface</summary>
        protected void InitializeInterface() {

            /**
             * We use a try - catch to prevent the tool from returning an error when no ROM is loaded
             * When no ROM is loaded, memory domains aren't accessible
             */
            try {

                // Get the current setting for displaying messages
                _previousDisplayMessagesSetting = APIs.Config.DisplayMessages;

                // Get & set the infos about the game currently running on BizHawk
                DetectCurrentGame();
                if (_interfaceActivated == true) {
                    ShowInterfaceIndicator();
                } else {
                    /**
                     * Retry getting the game code
                     * For DS games, because of the DS bootup screen, the game code is not always accessible after switching games
                     *
                     * 10 frames should be enough for this
                     */
                    if (_retryCount < 10) {
                        _retryCount++;
                        DetectCurrentGame();
                    }
                }
            } catch { }

            InitializeComponent();
        }

        /// <summary>Executed after every frame (except while turboing, use FastUpdateAfter for that)</summary>
        protected override void UpdateAfter() {
            try {
                if (_supportedGame == true) {
                    ShowInterfaceIndicator();

                    // If the interface is not activated, reinitialize it
                    if (_interfaceActivated == false) {
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
                    if (_retryCount < 10) {
                        _retryCount++;
                        DetectCurrentGame();
                    }
                }
            } catch { }
        }

        #endregion

        #region Game detection

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
                case 1246311233:    // E3 demo / beta
                case 1246311253:    // JP
                    currentGameName = "Boktai: The Sun is in Your Hand";
                    shorterGameName = "Boktai";
                    _supportedGame = true;
                    _isDS = false;
                    break;
                case 1345467221:    // EU
                case 1160917845:    // US
                case 1244803925:    // JP 1.0 & 1.1
                    currentGameName = "Boktai 2: Solar Boy Django";
                    shorterGameName = "Zoktai";
                    _supportedGame = true;
                    _isDS = false;
                    break;
                case 1244869461:
                    currentGameName = "Boktai 3: Sabata's Counterattack";
                    shorterGameName = "Shinbok";
                    _supportedGame = true;
                    _isDS = false;
                    break;
                case 1481329729:    // EU 1.1
                case 1347112001:    // EU 1.0
                case 1162562625:    // US
                case 1246448705:    // JP
                    currentGameName = "Boktai DS - Lunar Knights";
                    shorterGameName = "LunarKnights";
                    _supportedGame = true;
                    _isDS = true;
                    break;
                default:
                    ResetInitializationVariables();
                    break;
            }
        }

        /// <summary>Resets the variables used for initializing the interface</summary>
        protected void ResetInitializationVariables() {
            _retryCount = 0;
            _isDS = _supportedGame = _interfaceActivated = false;
            currentGameName = shorterGameName = "";
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

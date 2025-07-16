using System;
using System.Windows.Forms;

using BokInterface.All;
using BokInterface.Tools.TileDataViewer;
using BokInterface.Tools.MemoryValuesListing;
using BokInterface.Tools.SolarBankInterestsSimulator;

/**
 * Main file for tools selection subwindows
 */

namespace BokInterface {
    partial class BokInterface {

        #region Properties

        protected bool tileDataViewerActive = false,
            memValuesListingActive = false,
            solarBankInterestsSimActive = false;
        private TileDataViewer? _tileDataViewer;
        private MemoryValuesListing? _memValuesListing;
        private SolarBankInterestsSimulator? _solarBankInterestsSim;

        #endregion

        #region Subwindows methods

        private void BoktaiToolsSubwindow() {
            AddToolsLabel();
            AddTileDataViewerBtn();
            AddMemoryValuesListBtn();
        }

        private void ZoktaiToolsSubwindow() {
            AddToolsLabel();
            AddTileDataViewerBtn();
            AddMemoryValuesListBtn();
            AddSolarBankInterestsSimBtn();
        }

        private void ShinbokToolsSubwindow() {
            AddToolsLabel();
            AddTileDataViewerBtn();
            AddMemoryValuesListBtn();
            AddSolarBankInterestsSimBtn();
        }

        private void LunarKnightsToolsSubwindow() {
            // this.AddToolsLabel();
        }

        #endregion

        #region Elements generation

        /// <summary>Simplified method for adding the label in the tools selection subwindow</summary>
		/// <param name="posX">X position</param>
		/// <param name="posY">Y position</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
        private void AddToolsLabel(int posX = 5, int posY = 5, int width = 176, int height = 15) {
            WinFormHelpers.CreateLabel("availableToolsLabel", "-- Tools available --", posX, posY, width, height, _miscToolsSelectionWindow);
        }

        /// <summary>Simplified method for adding the Tile Data Viewer tool button to the tools selection subwindow</summary>
		/// <param name="posX">X position</param>
		/// <param name="posY">Y position</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
        private void AddTileDataViewerBtn(int posX = 5, int posY = 23, int width = 176, int height = 23) {

            Button tdvBtn = WinFormHelpers.CreateButton("tdvBtn", "Tile data viewer", posX, posY, width, height);
            tdvBtn.Click += new EventHandler(delegate (object sender, EventArgs e) {

                // If tool is already active, stop
                if (tileDataViewerActive == true) {
                    return;
                }

                tileDataViewerActive = true;
                switch (shorterGameName) {
                    case "Boktai":
                        _tileDataViewer = new BoktaiTileDataViewer(this, _boktaiAddresses);
                        break;
                    case "Zoktai":
                        _tileDataViewer = new ZoktaiTileDataViewer(this, _zoktaiAddresses);
                        break;
                    case "Shinbok":
                        _tileDataViewer = new ShinbokTileDataViewer(this, _shinbokAddresses);
                        break;
                    case "LunarKnights":
                        _tileDataViewer = new LunarKnightsTileDataViewer(this, _lunarKnightsAddresses);
                        break;
                    default:
                        // If game not handled, indicate that the tool isn't active & stop here
                        tileDataViewerActive = false;
                        return;
                }

                // Initialize the loop to update / redraw automatically & add the on-close event handler
                _tileDataViewer.InitializeFrameLoop();
                _tileDataViewer.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                    /**
                     * Remove the function from the list of functions to call each frame
                     * Also set instance with null to prevent it from doing anything else
                     */
                    tileDataViewerActive = false;
                    functionsList.RemoveAt(_tileDataViewer.index);
                    _tileDataViewer = null;
                });
            });

            _miscToolsSelectionWindow.Controls.Add(tdvBtn);
        }

        /// <summary>Simplified method for adding the Memory Values List tool button to the tools selection subwindow</summary>
		/// <param name="posX">X position</param>
		/// <param name="posY">Y position</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
        private void AddMemoryValuesListBtn(int posX = 5, int posY = 50, int width = 176, int height = 23) {

            Button mvlBtn = WinFormHelpers.CreateButton("mvlBtn", "Memory Values List", posX, posY, width, height);
            mvlBtn.Click += new EventHandler(delegate (object sender, EventArgs e) {

                // If tool is already active, stop
                if (memValuesListingActive == true) {
                    return;
                }

                memValuesListingActive = true;
                switch (shorterGameName) {
                    case "Boktai":
                        _memValuesListing = new MemoryValuesListing(this, _boktaiAddresses);
                        break;
                    case "Zoktai":
                        _memValuesListing = new MemoryValuesListing(this, _zoktaiAddresses);
                        break;
                    case "Shinbok":
                        _memValuesListing = new MemoryValuesListing(this, _shinbokAddresses);
                        break;
                    case "LunarKnights":
                        _memValuesListing = new MemoryValuesListing(this, _lunarKnightsAddresses);
                        break;
                    default:
                        // If game not handled, indicate that the tool isn't active & stop here
                        memValuesListingActive = false;
                        return;
                }

                // Add the on-close event handler
                _memValuesListing.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                    // Indicate that the tool isn't active anymore & set instance to null to prevent it from doing anything else
                    memValuesListingActive = false;
                    _memValuesListing = null;
                });
            });

            _miscToolsSelectionWindow.Controls.Add(mvlBtn);
        }

        /// <summary>Simplified method for adding the Solar bank interests simulator button to the tools selection subwindow</summary>
		/// <param name="posX">X position</param>
		/// <param name="posY">Y position</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
        private void AddSolarBankInterestsSimBtn(int posX = 5, int posY = 78, int width = 176, int height = 23) {

            Button solarBankSimBtn = WinFormHelpers.CreateButton("solarBankInterestsSimBtn", "Solar bank interests simulator", posX, posY, width, height);
            solarBankSimBtn.Click += new EventHandler(delegate (object sender, EventArgs e) {

                // If tool is already active, stop
                if (solarBankInterestsSimActive == true) {
                    return;
                }

                solarBankInterestsSimActive = true;
                switch (shorterGameName) {
                    case "Zoktai":
                    case "Shinbok":
                        _solarBankInterestsSim = new SolarBankInterestsSimulator(this);
                        break;
                    default:
                        // If game not handled, indicate that the tool isn't active & stop here
                        solarBankInterestsSimActive = false;
                        return;
                }

                // Add the on-close event handler
                _solarBankInterestsSim.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                    // Indicate that the tool isn't active anymore & set instance to null just in case, to prevent it from doing anything else
                    solarBankInterestsSimActive = false;
                    _solarBankInterestsSim = null;
                });
            });

            _miscToolsSelectionWindow.Controls.Add(solarBankSimBtn);
        }

        #endregion
    }
}

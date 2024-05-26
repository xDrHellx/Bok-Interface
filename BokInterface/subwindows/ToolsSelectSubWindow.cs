using System;
using System.Windows.Forms;

using BokInterface.All;
using BokInterface.Tools.TileDataViewer;
using BokInterface.Tools.MemoryValuesListing;

/**
 * Main file for tools selection subwindows
 */

namespace BokInterface {
    partial class BokInterface {

        #region Properties

        protected bool tileDataViewerActive = false;
        private TileDataViewer? _tileDataViewer;
        protected bool memValuesListingActive = false;
        private MemoryValuesListing? _memValuesListing;

        #endregion

        #region Subwindows methods for each game

        private void BoktaiToolsSubwindow() {
            AddToolsLabel();
            AddTileDataViewerBtn();
            AddMemoryValuesListBtn();
        }

        private void ZoktaiToolsSubwindow() {
            AddToolsLabel();
            AddTileDataViewerBtn();
            AddMemoryValuesListBtn();
        }

        private void ShinbokToolsSubwindow() {
            AddToolsLabel();
            AddTileDataViewerBtn();
            AddMemoryValuesListBtn();
        }

        private void LunarKnightsToolsSubwindow() {
            // this.AddToolsLabel();
        }

        #endregion

        #region Subwindow elements generating methods

        /// <summary>Simplified method for adding the label in the tools selection subwindow</summary>
		/// <param name="posX">X position</param>
		/// <param name="posY">Y position</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
        private void AddToolsLabel(int posX = 5, int posY = 5, int width = 176, int height = 15) {
            WinFormHelpers.CreateLabel("availableToolsLabel", "-- Tools available --", posX, posY, width, height, miscToolsSelectionWindow);
        }

        /// <summary>Simplified method for adding the Tile Data Viewer tool button to the tools selection subwindow</summary>
        /// <param name="posX">X position</param>
        /// <param name="posY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        private void AddTileDataViewerBtn(int posX = 5, int posY = 23, int width = 188, int height = 23) {

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

                // Initialize the loop to update / redraw automatically
                _tileDataViewer.InitializeFrameLoop();
                _tileDataViewer.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {

                    tileDataViewerActive = false;

                    // Remove the function from the list of functions to call each frame
                    // Also just in case, replace instance with null to prevent it from doing anything else
                    functionsList.RemoveAt(_tileDataViewer.index);
                    _tileDataViewer = null;
                });
            });

            miscToolsSelectionWindow.Controls.Add(tdvBtn);
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

                _memValuesListing = new("mvl", "Memory Values List", 650, 500, shorterGameName, this);
                _memValuesListing.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {

                    memValuesListingActive = false;

                    // Just in case, replace instance with null to prevent it from doing anything else
                    _memValuesListing = null;
                });

                _memValuesListing.Show();
            });

            miscToolsSelectionWindow.Controls.Add(mvlBtn);
        }

        #endregion
    }
}

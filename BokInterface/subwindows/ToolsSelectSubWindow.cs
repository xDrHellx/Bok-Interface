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
        private void AddTileDataViewerBtn(int posX = 5, int posY = 23, int width = 176, int height = 23) {

            Button tdvBtn = WinFormHelpers.CreateButton("tdvBtn", "Tile data viewer", posX, posY, width, height);
            tdvBtn.Click += new EventHandler(delegate (object sender, EventArgs e) {

                // If tool is already active, stop
                if (tileDataViewerActive == true) {
                    return;
                }

                tileDataViewerActive = true;

                _tileDataViewer = new("tileDateViewer", "Tile Data Viewer", 500, 500, shorterGameName, this);
                _tileDataViewer.InitializeFrameLoop();

                _tileDataViewer.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {

                    tileDataViewerActive = false;

                    // Remove the function from the list of functions to call each frame
                    int functionIndex = _tileDataViewer.index;
                    functionsList.RemoveAt(functionIndex);

                    // Just in case, replace instance with null to prevent it from doing anything else
                    _tileDataViewer = null;
                });

                _tileDataViewer.Show();
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

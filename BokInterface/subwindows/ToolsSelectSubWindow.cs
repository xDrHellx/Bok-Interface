using System;
using System.Windows.Forms;

using BokInterface.Tools.TileDataViewer;

/**
 * Main file for tools selection subwindows
 */

namespace BokInterface {
    partial class BokInterface {

        #region Properties

        protected bool tileDataViewerActive = false;
        private TileDataViewer? TileDataViewer;

        #endregion

        #region Subwindows methods for each game

        private void BoktaiToolsSubwindow() {
            AddToolsLabel();
            AddTileDataViewerBtn();
        }

        private void ZoktaiToolsSubwindow() {
            AddToolsLabel();
            AddTileDataViewerBtn();
        }

        private void ShinbokToolsSubwindow() {
            AddToolsLabel();
            AddTileDataViewerBtn();
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
        private void AddToolsLabel(int posX = 5, int posY = 5, int width = 188, int height = 15) {
            Label availableToolsLabel = CreateLabel("availableToolsLabel", "-- Tools available --", posX, posY, width, height);
            miscToolsSelectionWindow.Controls.Add(availableToolsLabel);
        }

        /// <summary>Simplified method for adding the Tile Data Viewer tool button to the tools selection subwindow</summary>
        /// <param name="posX">X position</param>
        /// <param name="posY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        private void AddTileDataViewerBtn(int posX = 5, int posY = 23, int width = 188, int height = 23) {

            Button tileDataBtn = CreateButton("tileDataBtn", "Tile data", posX, posY, width, height);
            tileDataBtn.Click += new EventHandler(delegate (object sender, EventArgs e) {

                // If tool is already active, stop
                if (tileDataViewerActive == true) {
                    return;
                }

                tileDataViewerActive = true;

                TileDataViewer = new("tileDateViewer", "Tile data viewer", 500, 500, shorterGameName, GetGameIconName(), this);
                TileDataViewer.InitializeFrameLoop();

                TileDataViewer.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {

                    tileDataViewerActive = false;

                    // Remove the function from the list of functions to call each frame
                    int functionIndex = TileDataViewer.index;
                    functionsList.RemoveAt(functionIndex);

                    // Just in case, replace instance with null to prevent it from doing anything else
                    TileDataViewer = null;
                });

                TileDataViewer.Show();
            });

            miscToolsSelectionWindow.Controls.Add(tileDataBtn);
        }

        #endregion
    }
}

using System;
using System.Windows.Forms;

using BokInterface.Tools.TileDataViewer;

/**
 * Main file for tools selection subwindows
 */

namespace BokInterface
{
    partial class BokInterfaceMainForm
    {

        #region Properties

        protected bool tileDataViewerActive = false;
        private TileDataViewer? TileDataViewer;

        #endregion

        #region Subwindows methods for each game

        private void BoktaiToolsSubwindow()
        {
            this.AddToolsLabel();
            this.AddTileDataViewerBtn();
        }

        private void ZoktaiToolsSubwindow()
        {
            this.AddToolsLabel();
            this.AddTileDataViewerBtn();
        }

        private void ShinbokToolsSubwindow()
        {
            this.AddToolsLabel();
            this.AddTileDataViewerBtn();
        }

        private void LunarKnightsToolsSubwindow()
        {
            // this.AddToolsLabel();
        }

        #endregion

        #region Subwindow elements generating methods

        /// <summary>Simplified method for adding the label in the tools selection subwindow</summary>
		/// <param name="posX">X position</param>
		/// <param name="posY">Y position</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
        private void AddToolsLabel(int posX = 5, int posY = 5, int width = 176, int height = 15)
        {
            System.Windows.Forms.Label availableToolsLabel = this.CreateLabel("availableToolsLabel", "-- Tools available --", 5, 5, 176, 15);
            this.miscToolsSelectionWindow.Controls.Add(availableToolsLabel);
        }

        /// <summary>Simplified method for adding the Tile Data Viewer tool button to the tools selection subwindow</summary>
		/// <param name="posX">X position</param>
		/// <param name="posY">Y position</param>
		/// <param name="width">Width (in pixels)</param>
		/// <param name="height">Height (in pixels)</param>
        private void AddTileDataViewerBtn(int posX = 5, int posY = 23, int width = 176, int height = 23)
        {

            System.Windows.Forms.Button tileDataBtn = this.CreateButton("tileDataBtn", "Tile data", posX, posY, width, height);
            tileDataBtn.Click += new System.EventHandler(delegate (object sender, EventArgs e)
            {

                // If tool is already active, stop
                if (this.tileDataViewerActive == true)
                {
                    return;
                }

                this.tileDataViewerActive = true;

                this.TileDataViewer = new("tileDateViewer", "Tile data viewer", 500, 500, BokInterfaceMainForm.shorterGameName, this.GetGameIconName(), this);
                this.TileDataViewer.InitializeFrameLoop();

                this.TileDataViewer.FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e)
                {

                    this.tileDataViewerActive = false;

                    // Remove the function from the list of functions to call each frame
                    int functionIndex = TileDataViewer.index;
                    BokInterfaceMainForm.functionsList.RemoveAt(functionIndex);

                    // Just in case, replace instance with null to prevent it from doing anything else
                    this.TileDataViewer = null;
                });

                this.TileDataViewer.Show();
            });

            this.miscToolsSelectionWindow.Controls.Add(tileDataBtn);
        }

        #endregion
    }
}
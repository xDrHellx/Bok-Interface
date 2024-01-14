using System;
using System.Windows.Forms;
using BokInterface.Tools;
using BokInterface.Tools.TileDataViewer;

/**
 * Main file for tools selection subwindows
 */

namespace BokInterface {
    partial class BokInterfaceMainForm {

        protected bool tileDataViewerActive = false;
        private TileDataViewer? TileDataViewer;

        private void BoktaiToolsSubwindow() {

            System.Windows.Forms.Label availableToolsLabel = this.CreateLabel("availableToolsLabel", "-- Tools available --", 5, 5, 176, 15);
            System.Windows.Forms.Button tileDataBtn = this.CreateButton("tileDataBtn", "Tile data", 5, 23, 176, 23);
            tileDataBtn.Click += new System.EventHandler(delegate(object sender, EventArgs e){
                
                // If tool is already active, stop
                if(this.tileDataViewerActive == true) {
                    return;
                }

                this.tileDataViewerActive = true;

                this.TileDataViewer = new("tileDateViewer", "Tile data viewer", 500, 500, "lita");
                this.TileDataViewer.InitializeFrameLoop();

                this.TileDataViewer.FormClosing += new FormClosingEventHandler(delegate(object sender, FormClosingEventArgs e) {

                    this.tileDataViewerActive = false;
                    
                    // Remove the function from the list of functions to call each frame
                    int functionIndex = TileDataViewer.index;
                    BokInterfaceMainForm.functionsList.RemoveAt(functionIndex);
                    
                    // Just in case, replace instance with null to prevent it from doing anything else
                    this.TileDataViewer = null;
                });

                this.TileDataViewer.Show();
            });

            this.miscToolsSelectionWindow.Controls.Add(availableToolsLabel);
            this.miscToolsSelectionWindow.Controls.Add(tileDataBtn);
        }

        private void ZoktaiToolsSubwindow() {

        }

        private void ShinbokToolsSubwindow() {

        }

        private void LunarKnightsToolsSubwindow() {
            
        }
    }
}
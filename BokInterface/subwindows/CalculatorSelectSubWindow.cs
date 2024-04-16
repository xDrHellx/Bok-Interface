using System;
using System.Windows.Forms;

using BokInterface.CalcSubwindows;

/**
 * Main file for tools selection subwindows
 */

namespace BokInterface {
    partial class BokInterface {

        #region Properties

        #endregion

        #region Subwindows methods for each game

        private void BoktaiCalculatorsSubwindow() {
            // AddCalculatorsLabel();
        }

        private void ZoktaiCalculatorsSubwindow() {
            AddCalculatorsLabel();
        }

        private void ShinbokCalculatorsSubwindow() {
            // AddCalculatorsLabel();
        }

        private void LunarKnightsCalculatorsSubwindow() {
            // AddCalculatorsLabel();
        }

        #endregion

        #region Subwindow elements generating methods

        /// <summary>Simplified method for adding the label in the calculators selection subwindow</summary>
        /// <param name="posX">X position</param>
        /// <param name="posY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        private void AddCalculatorsLabel(int posX = 5, int posY = 5, int width = 188, int height = 15) {
            Label availableCalculatorsLabel = CreateLabel("availableCalculatorsLabel", "-- Calculators available --", posX, posY, width, height);
            calculatorsSelectionWindow.Controls.Add(availableCalculatorsLabel);
        }

        #endregion
    }
}

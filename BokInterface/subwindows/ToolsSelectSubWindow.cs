using System;
using System.Windows.Forms;

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

        #region Tools init

        private void OpenTileDataViewer(object sender, EventArgs e) {

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
        }

        private void OpenMemoryValuesList(object sender, EventArgs e) {
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
        }

        private void OpenSolarBankInterestsSim(object sender, EventArgs e) {
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
        }

        #endregion
    }
}

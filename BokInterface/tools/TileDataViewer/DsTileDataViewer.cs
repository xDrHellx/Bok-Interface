using System.Windows.Forms;

using BokInterface.Addresses;

namespace BokInterface.Tools.TileDataViewer {
    /// <summary>TDViewer tool for Lunar Knights / Boktai DS</summary>
    class DsTileDataViewer : TileDataViewer {

        #region Properties

        private readonly BokInterface _bokInterface;
        private readonly DsAddresses _memAddresses;

        #endregion

        #region Constructor | Init

        public DsTileDataViewer(BokInterface bokInterface, DsAddresses lunarKnightsAddresses) {
            Owner = _bokInterface = bokInterface;
            _memAddresses = lunarKnightsAddresses;
            Icon = _bokInterface.Icon;
            InitializeSubwindowProperties();
        }

        protected override void SetGameAddresses() {
            // Currently not handled, not enough addresses available
            mapDataAddress = djangoXposAddress = djangoYposAddress = 0;
        }

        #endregion

        #region Drawing

        /// <summary>Draw tile effect icons</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="tileEffect">Value for the tile's effect</param>
        /// <param name="posX">X position of the tile</param>
        /// <param name="posY">Y position of the tile</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected override void DrawTileEffects(PaintEventArgs e, uint tileEffect, int posX, int posY, int scale) { }

        protected override string GetTileEffectName(int bitNb) {
            return "";
        }

        #endregion
    }
}

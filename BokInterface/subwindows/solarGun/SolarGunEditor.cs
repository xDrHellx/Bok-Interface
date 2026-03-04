using System.Windows.Forms;

namespace BokInterface.solarGun {
    /// <summary>Basis class for solar gun editor subclasses</summary>
    abstract class SolarGunEditor : Editor {

        #region Properties

        protected new readonly string name = "solarGunEditWindow",
            text = "Solar gun editor";

        #endregion

        #region Form elements

        protected TabControl inventoryTabControl = new();
        protected TabPage lensTab = new(),
            framesTab = new();

        #endregion

        #region Methods

        #endregion
    }
}

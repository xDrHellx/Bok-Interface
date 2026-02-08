using System.Collections.Generic;
using System.Windows.Forms;

namespace BokInterface.Magics {
    /// <summary>Basis class for magics editor subclasses</summary>
    abstract class MagicsEditor : Editor {

        #region Properties

        protected new readonly string name = "magicsEditWindow",
            text = "Magics editor";

        #endregion

        #region Form elements

        protected readonly List<CheckBox> checkBoxes = [];

        #endregion

        #region Methods

        #endregion
    }
}

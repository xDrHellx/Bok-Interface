using System.Collections.Generic;
using System.Windows.Forms;

namespace BokInterface.Magics;

/// <summary>Basis class for magics editor subclasses</summary>
abstract class MagicsEditor : Editor {

    protected new readonly string name = "magicsEditWindow",
        text = "Magics editor";
    protected readonly List<CheckBox> checkBoxes = [];
}

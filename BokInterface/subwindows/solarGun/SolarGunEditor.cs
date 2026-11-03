using System.Windows.Forms;

namespace BokInterface.solarGun;

/// <summary>Basis class for solar gun editor subclasses</summary>
abstract class SolarGunEditor : Editor {

    protected new readonly string name = "solarGunEditWindow",
        text = "Solar gun editor";
    protected TabControl inventoryTabControl = new();
    protected TabPage lensTab = new(),
        framesTab = new();
}

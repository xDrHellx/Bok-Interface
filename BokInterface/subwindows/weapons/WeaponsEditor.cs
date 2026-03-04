using System.Collections.Generic;
using System.Windows.Forms;

namespace BokInterface.Weapons {
    /// <summary>Basis class for weapons editor subclasses</summary>
    abstract class WeaponsEditor : Editor {

        #region Properties

        protected new readonly string name = "weaponsEditWindow",
            text = "Weapons editor";

        #endregion

        #region Form elements

        protected Panel slotsPanel = new();

        #endregion

        #region Methods

        /// <summary>Get a weapon from a dictionnary by using the weapon's value</summary>
        /// <param name="value"><c>decimal</c>Value</param>
        /// <param name="dictionnary">Dictionnary of weapons</param>
        /// <returns><c>Weapon</c>Weapon</returns>
        protected Weapon? GetWeaponByValue(decimal value, Dictionary<string, Weapon> dictionnary) {
            foreach (KeyValuePair<string, Weapon> index in dictionnary) {
                Weapon weapon = index.Value;
                if (weapon.value == value) {
                    return weapon;
                }
            }

            return null;
        }

        #endregion
    }
}

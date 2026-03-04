using System.Collections.Generic;

namespace BokInterface.Accessories {
    /// <summary>Basis class for accessories (armors) editor subclasses</summary>
    abstract class AccessoriesEditor : Editor {

        #region Properties

        protected new readonly string name = "accessoriesEditWindow",
            text = "Accessories editor";

        #endregion

        #region Methods

        /// <summary>Get an accessory from a dictionnary by using the accessory's value</summary>
        /// <param name="value"><c>decimal</c>Value</param>
        /// <param name="dictionnary">Dictionnary of accessories</param>
        /// <returns><c>Accessory</c>Accessory</returns>
        protected Accessory? GetAccessoryByValue(decimal value, Dictionary<string, Accessory> dictionnary) {
            foreach (KeyValuePair<string, Accessory> index in dictionnary) {
                Accessory accessory = index.Value;
                if (accessory.value == value) {
                    return accessory;
                }
            }

            return null;
        }

        #endregion
    }
}

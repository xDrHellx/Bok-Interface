using System.Collections.Generic;

using BokInterface.Items;

namespace BokInterface.KeyItems {
    /// <summary>Basis class for key items editor subclasses</summary>
    abstract class KeyItemsEditor : Editor {

        #region Properties

        protected new readonly string name = "keyItemsEditWindow",
            text = "Key items editor";

        #endregion

        #region Form elements

        #endregion

        #region Methods

        /// <summary>Get an item from a dictionnary by using the item's value</summary>
        /// <param name="value"><c>decimal</c>Value</param>
        /// <param name="dictionnary">Dictionnary of items</param>
        /// <returns><c>Item</c>Item</returns>
        protected Item? GetItemByValue(decimal value, Dictionary<string, Item> dictionnary) {
            foreach (KeyValuePair<string, Item> index in dictionnary) {
                Item item = index.Value;
                if (item.value == value) {
                    return item;
                }
            }

            return null;
        }

        #endregion
    }
}

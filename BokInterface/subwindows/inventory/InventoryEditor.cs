using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Items;

namespace BokInterface.Inventory {
    /// <summary>Basis class for inventory editor subclasses</summary>
    abstract class InventoryEditor : Editor {

        #region Properties

        protected new readonly string name = "inventoryEditWindow",
            text = "Inventory editor";

        #endregion

        #region Form elements

        protected readonly List<CheckBox> checkBoxes = [];

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

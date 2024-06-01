using System.Collections.Generic;
using System.Windows.Forms;

namespace BokInterface.Inventory {
    /// <summary>Basis class for inventory editor subclasses</summary>
    abstract class InventoryEditor : Form {

        #region Properties

        protected readonly string name = "inventoryEditWindow";
        protected readonly string text = "Inventory editor";

        #endregion

        #region Form elements
        #endregion

        #region Methods

        ///<summary>Get the current inventory</summary>
        /// <returns><c>IDictionary<string, decimal></c>Current inventory</returns>
        protected virtual IDictionary<string, decimal> GetCurrentInventory() {
            IDictionary<string, decimal> inventory = new Dictionary<string, decimal>();
            return inventory;
        }

        /// <summary>Add elements to the subwindow</summary>
        protected abstract void AddElements();

        /// <summary>Set values to memory addresses</summary>
        protected abstract void SetValues();

        #endregion
    }
}
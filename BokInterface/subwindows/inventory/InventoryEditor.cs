using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.All;

namespace BokInterface.Inventory {
    /// <summary>Basis class for inventory editor subclasses</summary>
    abstract class InventoryEditor : Form {

        #region Properties

        protected readonly string name = "inventoryEditWindow";
        protected readonly string text = "Inventory editor";

        #endregion

        #region Form elements

        protected GroupBox inventoryGroupbox = new();
        protected readonly List<ComboBox> dropDownLists = [];
        protected readonly List<NumericUpDown> numericUpDowns = [];

        #endregion

        #region Methods

        ///<summary>Sets common parameters for the form / subwindow</summary>
        ///<param name="width">Form width</param>
        ///<param name="height">Form height</param>
        protected void SetFormParameters(int width, int height) {
            Name = name;
            Text = text;
            AutoScaleDimensions = new SizeF(6F, 15F);
            AutoScaleMode = AutoScaleMode.Inherit;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = SystemColors.Control;
            Font = WinFormHelpers.defaultFont;
            AutoScroll = true;
            ClientSize = new Size(width, height);
        }

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
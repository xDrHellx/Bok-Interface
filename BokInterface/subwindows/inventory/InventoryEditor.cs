using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Utils;

namespace BokInterface.Inventory {
    /// <summary>Basis class for inventory editor subclasses</summary>
    abstract class InventoryEditor : Form {

        #region Properties

        protected readonly string name = "inventoryEditWindow";
        protected readonly string text = "Inventory editor";

        #endregion

        #region Form elements

        protected readonly List<ImageComboBox> dropDownLists = [];
        protected readonly List<NumericUpDown> numericUpDowns = [];
        protected CheckGroupBox slot1group = new(),
            slot2group = new(),
            slot3group = new(),
            slot4group = new(),
            slot5group = new(),
            slot6group = new(),
            slot7group = new(),
            slot8group = new(),
            slot9group = new(),
            slot10group = new(),
            slot11group = new(),
            slot12group = new(),
            slot13group = new(),
            slot14group = new(),
            slot15group = new(),
            slot16group = new();

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
            MaximizeBox = false;
        }

        /// <summary>Add elements to the subwindow</summary>
        protected abstract void AddElements();

        /// <summary>Set values to memory addresses</summary>
        protected abstract void SetValues();

        ///<summary>Sets default values for each field</summary>
        protected virtual void SetDefaultValues() { }

        #endregion
    }
}
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Utils;

namespace BokInterface.Accessories {
    /// <summary>Basis class for accessories (armors) editor subclasses</summary>
    abstract class AccessoriesEditor : Form {

        #region Properties

        protected readonly string name = "accessoriesEditWindow",
            text = "Accessories editor";

        #endregion

        #region Form elements

        protected readonly List<ImageComboBox> dropDownLists = [];
        protected CheckGroupBox? slot1group { get; set; }
        protected CheckGroupBox? slot2group { get; set; }
        protected CheckGroupBox? slot3group { get; set; }
        protected CheckGroupBox? slot4group { get; set; }
        protected CheckGroupBox? slot5group { get; set; }
        protected CheckGroupBox? slot6group { get; set; }
        protected CheckGroupBox? slot7group { get; set; }
        protected CheckGroupBox? slot8group { get; set; }
        protected CheckGroupBox? slot9group { get; set; }
        protected CheckGroupBox? slot10group { get; set; }
        protected CheckGroupBox? slot11group { get; set; }
        protected CheckGroupBox? slot12group { get; set; }
        protected CheckGroupBox? slot13group { get; set; }
        protected CheckGroupBox? slot14group { get; set; }
        protected CheckGroupBox? slot15group { get; set; }
        protected CheckGroupBox? slot16group { get; set; }

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

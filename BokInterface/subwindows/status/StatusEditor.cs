using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.All;

namespace BokInterface.Status {
    /// <summary>Basis class for status editor subclasses</summary>
    abstract class StatusEditor : Form {

        #region Properties

        protected readonly string name = "statusEditWindow";
        protected readonly string text = "Status editor";

        #endregion

        #region Form elements

        protected CheckGroupBox skillGroupBox = new(),
            expGroupBox = new(),
            statPointsGroupBox = new(),
            statusGroupBox = new(),
            statsGroupBox = new(),
            sollsGroupBox = new();
        protected readonly List<NumericUpDown> statusNumericUpDowns = [];

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

        /// <summary>Get default values</summary>
        /// <returns><c>IDictionary<string, decimal></c>Default values</returns>
        protected virtual IDictionary<string, decimal> GetDefaultValues() {
            IDictionary<string, decimal> defaultValues = new Dictionary<string, decimal>();
            return defaultValues;
        }

        /// <summary>Add elements to the subwindow</summary>
        protected abstract void AddElements();

        /// <summary>Set values to memory addresses</summary>
        protected abstract void SetValues();

        #endregion
    }
}
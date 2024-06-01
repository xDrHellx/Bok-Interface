using System.Collections.Generic;
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

        protected CheckGroupBox _skillGroupBox = new();
        protected CheckGroupBox _expGroupBox = new();
        protected CheckGroupBox _statPointsGroupBox = new();
        protected CheckGroupBox _statusGroupBox = new();
        protected CheckGroupBox _statsGroupBox = new();
        protected readonly List<NumericUpDown> _statusNumericUpDowns = [];

        #endregion

        #region Methods

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
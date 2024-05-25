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

        protected virtual IDictionary<string, decimal> GetDefaultValues() {
            IDictionary<string, decimal> defaultValues = new Dictionary<string, decimal>();
            return defaultValues;
        }

        protected abstract void AddElements();
        protected abstract void SetValues();
    }
}
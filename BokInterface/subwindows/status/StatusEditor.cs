using System.Collections.Generic;

using BokInterface.Utils;

namespace BokInterface.Status {
    /// <summary>Basis class for status editor subclasses</summary>
    abstract class StatusEditor : Editor {

        #region Properties

        protected new readonly string name = "statusEditWindow",
            text = "Status editor";

        #endregion

        #region Form elements

        protected CheckGroupBox skillGroupBox = new(),
            expGroupBox = new(),
            statPointsGroupBox = new(),
            statusGroupBox = new(),
            statsGroupBox = new(),
            sollsGroupBox = new();

        #endregion

        #region Methods

        /// <summary>Get default values</summary>
        /// <returns><c>IDictionary<string, decimal></c>Default values</returns>
        protected virtual IDictionary<string, decimal> GetDefaultValues() {
            IDictionary<string, decimal> defaultValues = new Dictionary<string, decimal>();
            return defaultValues;
        }

        #endregion
    }
}

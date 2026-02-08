using System.Collections.Generic;

using BokInterface.Addresses;
using BokInterface.Utils;

namespace BokInterface.Status {
    /// <summary>Status editor for Boktai</summary>
    class BoktaiStatusEditor : StatusEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly BoktaiAddresses _boktaiAddresses;

        #endregion

        #region Constructor

        public BoktaiStatusEditor(BokInterface bokInterface, MemoryValues memoryValues, BoktaiAddresses boktaiAddresses) {

            _memoryValues = memoryValues;
            _boktaiAddresses = boktaiAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(203, 144);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Get default values, depending on availability, these can be the current in-game values
            IDictionary<string, decimal> defaultValues = GetDefaultValues();

            // AddSetValuesButton(147, 121, this);
        }

        #endregion

        #region Values setting

        protected override void SetValues() {

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            // Sets values based on fields
            for (int i = 0; i < numericUpDowns.Count; i++) {

                // If the field is disabled, skip it
                if (numericUpDowns[i].Enabled == false) {
                    continue;
                }

                decimal value = numericUpDowns[i].Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the input field's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = numericUpDowns[i].Name.Split(['_'], 2);
                string subList = fieldParts[0],
                    memoryValueKey = fieldParts[1];
                switch (subList) {
                    default:
                        if (_memoryValues.U16.ContainsKey(memoryValueKey) == true) {
                            _memoryValues.U16[memoryValueKey].Value = (uint)value;
                        } else if (_memoryValues.U32.ContainsKey(memoryValueKey) == true) {
                            _memoryValues.U32[memoryValueKey].Value = (uint)value;
                        }
                        break;
                }
            }

            /**
             * If BizHawk was not paused before setting values, unpause it
             * Otherwise keep it paused
             */
            if (_bokInterface._previousIsPauseSetting == true) {
                APIs.Client.Unpause();
            }
        }

        protected override IDictionary<string, decimal> GetDefaultValues() {
            IDictionary<string, decimal> defaultValues = new Dictionary<string, decimal>();
            return defaultValues;
        }

        #endregion
    }
}

using System.Collections.Generic;
using System.Windows.Forms;

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

            // Button for setting values & its events
            // Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 147, 121, 75, 23, this);
            // setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
            //     // Write the values for 10 frames
            //     for (int i = 0; i < 10; i++) {
            //         SetValues();
            //     }
            // });
        }

        #endregion

        #region Values setting

        protected override void SetValues() {

            // Retrieve all input fields
            List<NumericUpDown> fields = statusNumericUpDowns;

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            // Sets values based on fields
            for (int i = 0; i < fields.Count; i++) {

                // If the field is disabled, skip it
                if (fields[i].Enabled == false) {
                    continue;
                }

                decimal value = fields[i].Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the input field's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = fields[i].Name.Split(['_'], 2);
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

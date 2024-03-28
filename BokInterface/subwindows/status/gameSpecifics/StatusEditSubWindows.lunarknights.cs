using System.Collections.Generic;
using System.Windows.Forms;

/**
 * File for Lunar Knights / Boktai DS' status edit subwindow
 */

namespace BokInterface {
    partial class BokInterface {

        private void LunarKnightsStatusEditSubwindow() {

        }

        /// <summary>Get default values for Lunar Knights / Boktai DS</summary>
        /// <returns><c>IDictionary<string, uint></c>Default values</returns>
        private IDictionary<string, decimal> GetLunarKnightsDefaultValues() {
            IDictionary<string, decimal> defaultValues = new Dictionary<string, decimal>();
            return defaultValues;
        }

        /// <summary>Specific method for setting status values</summary>
        /// <param name="fields">List of fields to parse through</param>
        private void SetLunarKnightsStatusValues(List<NumericUpDown> fields) {

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
                string subList = fieldParts[0];
                string memoryValueKey = fieldParts[1];
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
        }
    }
}

using System.Collections.Generic;

/**
 * Main file for status editing subwindows
 */

namespace BokInterface {
	partial class BokInterfaceMainForm {

		#region Properties for subwindow elements

		private System.Windows.Forms.GroupBox edit_statusGroupBox = new();
		private System.Windows.Forms.GroupBox edit_statsGroupBox = new();
		private List<System.Windows.Forms.Label> edit_statusLabels = new();
		private List<System.Windows.Forms.NumericUpDown> edit_statusNumericUpDowns = new();

		#endregion

		#region General status edit subwindow methods

		/// <summary>Clears the Status editing subwindow and all other sections within it</summary>
		private void ClearStatusEditControls() {
			this.edit_statusGroupBox.Controls.Clear();
			this.edit_statsGroupBox.Controls.Clear();
			this.edit_statusLabels.Clear();
			this.edit_statusNumericUpDowns.Clear();
		}

		/// <summary>
		/// <para>Get default values for the "Edit Status" subwindow</para>
		/// <para>Default values are the current in-games values unless these are either invalid or unavailable</para>
		/// <para>For example Django's HP is an unvalid value on the title screen</para>
		/// </summary>
		/// <returns><c>IDictionary</c>Dictionnary of key => values pairs</returns>
		private IDictionary<string, uint> GetDefaultStatusValues() {

			// Add default values according to current game
			switch(shorterGameName) {
				case "Boktai":
					return this.GetBoktaiDefaultValues();
				case "Zoktai":
					return this.GetZoktaiDefaultValues();
				case "Shinbok":
					return this.GetShinbokDefaultValues();
				case "LunarKnights":
					return this.GetLunarKnightsDefaultValues();
				default:
					return new Dictionary<string, uint>();
			}
		}

		/// <summary>Sets values related to Django's status</summary>
		private void SetStatusValues() {

			// Retrieve all input fields
			var fields = this.edit_statusNumericUpDowns;

			// Sets values based on fields for the current game
			for(int i = 0; i < fields.Count; i++) {
				var value = (uint)fields[i].Value;

				/**
				 * Indicate which sublist to use for setting the value, based on the input field's name
				 * We only split on the first "_"
				 */
				var fieldParts = fields[i].Name.Split(new char[]{'_'}, 2);
				string subList = fieldParts[0];
				string memoryValueKey = fieldParts[1];
				switch(subList) {
					case "django":
						if(memoryValues.Django.ContainsKey(memoryValueKey) == true) {
							memoryValues.Django[memoryValueKey].Value = value;
						}
						break;
					case "solls":
						if(memoryValues.Solls.ContainsKey(memoryValueKey) == true) {
							memoryValues.Solls[memoryValueKey].Value = value;
						}
						break;
					case "bike":
						if(memoryValues.Bike.ContainsKey(memoryValueKey) == true) {
							memoryValues.Bike[memoryValueKey].Value = value;
						}
						break;
					case "misc":
						if(memoryValues.Misc.ContainsKey(memoryValueKey) == true) {
							memoryValues.Misc[memoryValueKey].Value = value;
						}
						break;
					default:
						break;
				}
			}
		}

		#endregion
	}
}
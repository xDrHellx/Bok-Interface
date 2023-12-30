using System;
using System.Collections.Generic;

/**
 * Main file for status edit subwindows
 */

namespace BokInterface {
    partial class BokInterfaceMainForm {

		private System.Windows.Forms.GroupBox edit_statusGroupBox = new();
		private System.Windows.Forms.GroupBox edit_statsGroupBox = new();
		private List<System.Windows.Forms.Label> edit_statusLabels = new();
		private List<System.Windows.Forms.NumericUpDown> edit_statusNumericUpDowns = new();

		/// <summary>Clears the Status editing subwindow and all other sections within it</summary>
		private void ClearStatusEditControls() {
			this.edit_statusGroupBox.Controls.Clear();
			this.edit_statsGroupBox.Controls.Clear();
			this.edit_statusLabels.Clear();
			this.edit_statusNumericUpDowns.Clear();
		}

		#region Subwindow generation code

		private void BoktaiStatusEditSubwindow() {
			
		}

		private void ZoktaiStatusEditSubwindow() {
			
		}

		private void ShinbokStatusEditSubwindow() {

			int l = 0;
			int n = 0;

			// Get default values, depending on availability, these can be the current in-game values
			var defaultValues = this.GetDefaultStatusValues();

			// Sections
			this.edit_statusGroupBox = this.CreateGroupBox("editStatusGroup", "Status", 5, 5, 103, 105, true);
			this.edit_statsGroupBox = this.CreateGroupBox("editStatsGroup", "Stats", 114, 5, 83, 105, true);

			// Status
			this.edit_statusLabels.Add(this.CreateLabel("djangoEditHpLabel", "LIFE :", 7, 19, 34, 15));
			// this.edit_statusLabels.Add(this.CreateLabel("djangoEditEneLabel", "ENE :", 7, 47, 34, 15));
			// this.edit_statusLabels.Add(this.CreateLabel("djangoEditTrcLabel", "TRC :", 7, 76, 34, 15));

			this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_currentHp", defaultValues["django_currentHp"], 47, 16, 50, 23, 1, 1000));
			// this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_currentEne", defaultValues["django_currentEne"], 47, 45, 50, 23, 1, 1000));
			// this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_currentTrc", defaultValues["django_currentTrc"], 47, 74, 50, 23, 1, 1000));

			// Add elements to group
			for(int i = 0; i < this.edit_statusLabels.Count; i++) {
				l++;
				this.edit_statusGroupBox.Controls.Add(this.edit_statusLabels[i]);
			}

			for(int i = 0; i < this.edit_statusNumericUpDowns.Count; i++) {
				n++;
				this.edit_statusGroupBox.Controls.Add(this.edit_statusNumericUpDowns[i]);
			}

			// Stats
			this.edit_statusLabels.Add(this.CreateLabel("djangoEditHpLabel", "VIT", 8, 19, 27, 15));
			this.edit_statusLabels.Add(this.CreateLabel("djangoEditEneLabel", "SPR", 8, 47, 27, 15));
			this.edit_statusLabels.Add(this.CreateLabel("djangoEditTrcLabel", "STR", 8, 76, 27, 15));

			this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_baseVit", defaultValues["django_baseVit"], 36, 16, 41, 23));
			this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_baseSpr", defaultValues["django_baseSpr"], 36, 45, 41, 23));
			this.edit_statusNumericUpDowns.Add(this.CreateNumericUpDown("django_baseStr", defaultValues["django_baseStr"], 36, 74, 41, 23));

			// Add elements to group
			for(int i = l; i < this.edit_statusLabels.Count; i++) {
				l++;
				this.edit_statsGroupBox.Controls.Add(this.edit_statusLabels[i]);
			}

			for(int i = n; i < this.edit_statusNumericUpDowns.Count; i++) {
				n++;
				this.edit_statsGroupBox.Controls.Add(this.edit_statusNumericUpDowns[i]);
			}

			// Add groups to subwindow
			this.statusEditWindow.Controls.Add(this.edit_statusGroupBox);
			this.statusEditWindow.Controls.Add(this.edit_statsGroupBox);

			// Button for setting values
			System.Windows.Forms.Button setValuesButton = this.CreateButton("setStatusButton", "Set values", 123, 116, 75, 23);
			setValuesButton.Click += new System.EventHandler(this.SetValuesButton_Click);
			
			this.statusEditWindow.Controls.Add(setValuesButton);
		}

		private void LunarKnightsStatusEditSubwindow() {
			
		}

		#endregion

		#region Setting, getting, default values-related code

		/// <summary>
		/// <para>Get default values for the "Edit Status" subwindow</para>
		/// <para>Default values are the current in-games values unless these are either invalid or unavailable</para>
		/// <para>For example Django's HP is an unvalid value on the title screen</para>
		/// </summary>
		/// <returns><c>IDictionary</c>Dictionnary of key => values pairs</returns>
		private IDictionary<string, uint> GetDefaultStatusValues() {

			IDictionary<string, uint> defaultValues = new Dictionary<string, uint>();
			uint djangoCurrentHp;
			
			// Add default values according to current game
			switch(shorterGameName) {
				case "Boktai":
					//
					break;
				case "Zoktai":
					//
					break;
				case "Shinbok":

					djangoCurrentHp = this.memoryValues.Django["currentHp"].Value;

					// If HP value is valid, get the other in-game values
					if(djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {
						defaultValues.Add("django_currentHp", djangoCurrentHp);
						defaultValues.Add("django_baseVit", this.memoryValues.Django["baseVit"].Value);
						defaultValues.Add("django_baseSpr", this.memoryValues.Django["baseSpr"].Value);
						defaultValues.Add("django_baseStr", this.memoryValues.Django["baseStr"].Value);
					} else {
						// If HP is unvalid (for example if we are on the title screen or in bike races), use specific values
						defaultValues.Add("django_currentHp", 100);
						// defaultValues.Add("django_currentEne", 100);
						// defaultValues.Add("django_currentTrc", 1000);
						defaultValues.Add("django_baseVit", 10);
						defaultValues.Add("django_baseSpr", 10);
						defaultValues.Add("django_baseStr", 10);
					}

					break;
				case "LunarKnights":
					//
					break;
				default:
					break;
			}

			return defaultValues;
		}

		/// <summary>Sets values related to Django's status</summary>
		private void SetStatusValues() {

			// Retrieve all input fields
			var fields = this.edit_statusNumericUpDowns;

			// Sets values based on fields for the current game
			for(int i = 0; i < fields.Count; i++) {
				var value = (uint)fields[i].Value;

				// Indicate which sublist to use for setting the value, based on the input field's name
				string subList = fields[i].Name.Split('-')[0];
				switch(subList) {
					case "django":
						if(memoryValues.Django.ContainsKey(fields[i].Name) == true) {
							memoryValues.Django[fields[i].Name].Value = value;
						}
						break;
					case "solls":
						if(memoryValues.Solls.ContainsKey(fields[i].Name) == true) {
							memoryValues.Solls[fields[i].Name].Value = value;
						}
						break;
					case "bike":
						if(memoryValues.Bike.ContainsKey(fields[i].Name) == true) {
							memoryValues.Bike[fields[i].Name].Value = value;
						}
						break;
					case "misc":
						if(memoryValues.Misc.ContainsKey(fields[i].Name) == true) {
							memoryValues.Misc[fields[i].Name].Value = value;
						}
						break;
					default:
						break;
				}
			}
		}

		#endregion

		#region Buttons events

		void SetValuesButton_Click(object sender, EventArgs e) {
			for(int i = 0; i < 10; i++) {
				this.SetStatusValues();
			}
		}

		#endregion
	}
}
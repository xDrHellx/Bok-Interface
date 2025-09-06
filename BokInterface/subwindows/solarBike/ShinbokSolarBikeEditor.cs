using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Bike;
using BokInterface.Utils;

namespace BokInterface.solarBike {
    /// <summary>Solar bike editor for Boktai 3</summary>
    class ShinbokSolarBikeEditor : Form {

        #region Properties

        protected readonly string name = "solarBikeEditWindow";
        protected readonly string text = "Solar bike editor";

        #endregion

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _shinbokAddresses;
        private readonly ShinbokBikeParts _shinbokBikeParts;

        #endregion

        #region Form elements

        private GroupBox _mainGroup = new(),
            _optionsGroup = new();
        private readonly List<ComboBox> _dropDownList = [];
        private ComboBox _frontPart = new(),
            _tiresPart = new(),
            _bodyPart = new(),
            _specialPart = new(),
            _bikeColor = new(),
            _option1 = new(),
            _option2 = new(),
            _option3 = new(),
            _option4 = new();

        #endregion

        public ShinbokSolarBikeEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses ShinbokAddresses) {

            _memoryValues = memoryValues;
            _shinbokAddresses = ShinbokAddresses;
            _shinbokBikeParts = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(373, 175);
            AddElements();
            Show();
        }

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

        protected void AddElements() {

            // Init groups & add them to the editor subwindow
            _mainGroup = WinFormHelpers.CreateCheckGroupBox("main_group", "Main parts", 5, 5, 203, 166, control: this);
            _optionsGroup = WinFormHelpers.CreateCheckGroupBox("options_group", "Options", 214, 5, 154, 137, control: this);

            // Init dropdowns, generate & add options to them, then add the labels
            InitDropDowns();
            GenerateDropDownOptions();
            InitLabels();

            // Set default values for each field
            SetDefaultValues();

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setBikePartsButton", "Set values", 294, 148, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        /// <summary>Add the labels</summary>
        protected void InitLabels() {
            WinFormHelpers.CreateLabel("bike_front_label", "Front", 4, 23, 44, 15, _mainGroup, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("bike_tires_label", "Tires", 4, 52, 44, 15, _mainGroup, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("bike_body_label", "Body", 4, 81, 44, 15, _mainGroup, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("bike_special_label", "Special", 4, 111, 44, 15, _mainGroup, textAlignment: "MiddleLeft");
            WinFormHelpers.CreateLabel("bike_color_label", "Color", 4, 138, 44, 15, _mainGroup, textAlignment: "MiddleLeft");
        }

        /// <summary>Initialize the dropdowns</summary>
        protected void InitDropDowns() {

            // Main parts
            _frontPart = WinFormHelpers.CreateDropDownList("bike_front", 57, 19, 140, 23, _mainGroup, visibleOptions: 6);
            _tiresPart = WinFormHelpers.CreateDropDownList("bike_tires", 57, 48, 140, 23, _mainGroup, visibleOptions: 6);
            _bodyPart = WinFormHelpers.CreateDropDownList("bike_body", 57, 77, 140, 23, _mainGroup, visibleOptions: 6);
            _specialPart = WinFormHelpers.CreateDropDownList("bike_special", 57, 107, 140, 23, _mainGroup, visibleOptions: 6);
            _bikeColor = WinFormHelpers.CreateDropDownList("bike_color", 57, 136, 140, 23, _mainGroup, visibleOptions: 6);

            // Options
            _option1 = WinFormHelpers.CreateDropDownList("bike_option_1", 8, 19, 140, 23, _optionsGroup, visibleOptions: 6);
            _option2 = WinFormHelpers.CreateDropDownList("bike_option_2", 8, 48, 140, 23, _optionsGroup, visibleOptions: 6);
            _option3 = WinFormHelpers.CreateDropDownList("bike_option_3", 8, 77, 140, 23, _optionsGroup, visibleOptions: 6);
            _option4 = WinFormHelpers.CreateDropDownList("bike_option_4", 8, 107, 140, 23, _optionsGroup, visibleOptions: 6);

            // Add to the dropdown list to loop over it later
            _dropDownList.Add(_frontPart);
            _dropDownList.Add(_tiresPart);
            _dropDownList.Add(_bodyPart);
            _dropDownList.Add(_specialPart);
            _dropDownList.Add(_bikeColor);
            _dropDownList.Add(_option1);
            _dropDownList.Add(_option2);
            _dropDownList.Add(_option3);
            _dropDownList.Add(_option4);
        }

        ///<summary>Generate the options for the dropdowns</summary>
        private void GenerateDropDownOptions() {
            foreach (ComboBox dropdown in _dropDownList) {
                dropdown.DataSource = dropdown.Name switch {
                    "bike_front" => new BindingSource(_shinbokBikeParts.Front, null),
                    "bike_tires" => new BindingSource(_shinbokBikeParts.Tires, null),
                    "bike_body" => new BindingSource(_shinbokBikeParts.Body, null),
                    "bike_special" => new BindingSource(_shinbokBikeParts.Special, null),
                    "bike_color" => new BindingSource(_shinbokBikeParts.Colors, null),
                    _ => new BindingSource(_shinbokBikeParts.Options, null)
                };
                dropdown.DisplayMember = "Key";
                dropdown.ValueMember = "Value";
            }
        }

        protected void SetValues() {

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            // Sets values for each dropdown
            for (int i = 0; i < _dropDownList.Count; i++) {

                // If the dropdown is disabled, skip it
                if (_dropDownList[i].Enabled == false) {
                    continue;
                }

                KeyValuePair<string, ShinbokBikePart> selectedOption = (KeyValuePair<string, ShinbokBikePart>)_dropDownList[i].SelectedItem;
                ShinbokBikePart selectedBikePart = selectedOption.Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the slot's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = _dropDownList[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], selectedBikePart.value);
            }

            /**
             * If BizHawk was not paused before setting values, unpause it
             * Otherwise keep it paused
             */
            if (_bokInterface._previousIsPauseSetting == true) {
                APIs.Client.Unpause();
            }
        }

        ///<summary>
        ///<para>Method for setting memory values</para>
        ///<para>This is separated because we use the switch inside on different types</para>
        ///</summary>
        ///<param name="subList"><c>Sublit / dictionnary the key belongs to</c></param>
        ///<param name="valueKey"><c>strng</c>Key withint the dictionnary</param>
        ///<param name="value"><c>decimal</c>Value to set</param>
        private void SetMemoryValue(string subList, string valueKey, decimal value) {
            switch (subList) {
                case "bike":
                    if (_memoryValues.Bike.ContainsKey(valueKey) == true) {
                        _memoryValues.Bike[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
                default:
                    if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
            }
        }

        protected void SetDefaultValues() {
            foreach (ComboBox dropdown in _dropDownList) {
                /**
                 * Get the name of the field to retrieve the value from based on the dropdown's name (for example inventory_slotX_item => slotX_item)
                 * Then try getting the corresponding item & preselect it
                 */
                string[] fieldParts = dropdown.Name.Split(['_'], 2);
                ShinbokBikePart? selectedBikePart = GetBikePartByValue(_memoryValues.Bike[fieldParts[1]].Value, fieldParts[1]);
                if (selectedBikePart != null) {
                    dropdown.SelectedIndex = dropdown.FindStringExact(selectedBikePart.name);
                }
            }
        }

        ///<summary>Get a bike part from the bike parts list by using its value</summary>
        ///<param name="value"><c>decimal</c>Value</param>
        ///<param name="dictionnaryName">Related dictionnary to search into</param>
        ///<returns><c>ShinbokBikePart</c>Bike part</returns>
        private ShinbokBikePart? GetBikePartByValue(decimal value, string dictionnaryName) {

            Dictionary<string, ShinbokBikePart> dictionnary;
            switch (dictionnaryName) {
                case "front":
                    dictionnary = _shinbokBikeParts.Front;
                    break;
                case "tires":
                    dictionnary = _shinbokBikeParts.Tires;
                    break;
                case "body":
                    dictionnary = _shinbokBikeParts.Body;
                    break;
                case "special":
                    dictionnary = _shinbokBikeParts.Special;
                    break;
                case "color":
                    dictionnary = _shinbokBikeParts.Colors;
                    break;
                case "options":
                    dictionnary = _shinbokBikeParts.Options;
                    break;
                default:
                    return null;
            }

            foreach (KeyValuePair<string, ShinbokBikePart> index in dictionnary) {
                ShinbokBikePart bikePart = index.Value;
                if (bikePart.value == value) {
                    return bikePart;
                }
            }

            return null;
        }
    }
}

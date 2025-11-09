using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Utils;

namespace BokInterface {
    /// <summary>Basis class for editors</summary>
    abstract class Editor : Form {

        #region Properties

        protected readonly string name = "editWindow",
            text = "Editor";

        #endregion

        #region Form elements

        protected readonly List<ImageComboBox> dropDownLists = [];
        protected readonly List<NumericUpDown> numericUpDowns = [];
        protected CheckGroupBox? slot1group { get; set; }
        protected CheckGroupBox? slot2group { get; set; }
        protected CheckGroupBox? slot3group { get; set; }
        protected CheckGroupBox? slot4group { get; set; }
        protected CheckGroupBox? slot5group { get; set; }
        protected CheckGroupBox? slot6group { get; set; }
        protected CheckGroupBox? slot7group { get; set; }
        protected CheckGroupBox? slot8group { get; set; }
        protected CheckGroupBox? slot9group { get; set; }
        protected CheckGroupBox? slot10group { get; set; }
        protected CheckGroupBox? slot11group { get; set; }
        protected CheckGroupBox? slot12group { get; set; }
        protected CheckGroupBox? slot13group { get; set; }
        protected CheckGroupBox? slot14group { get; set; }
        protected CheckGroupBox? slot15group { get; set; }
        protected CheckGroupBox? slot16group { get; set; }

        #endregion

        #region Methods

        /// <summary>Sets common parameters for the form / subwindow</summary>
        /// <param name="width">Form width</param>
        /// <param name="height">Form height</param>
        /// <param name="name">Form name</param>
        /// <param name="title">Form title</param>
        protected void SetFormParameters(int width, int height, string name = "", string title = "") {
            Name = name != "" ? name : this.name;
            Text = title != "" ? title : text;
            AutoScaleDimensions = new SizeF(6F, 15F);
            AutoScaleMode = AutoScaleMode.Inherit;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = SystemColors.Control;
            Font = WinFormHelpers.defaultFont;
            AutoScroll = true;
            ClientSize = new Size(width, height);
            MaximizeBox = false;
        }

        /// <summary>Add elements to the subwindow</summary>
        protected abstract void AddElements();

        /// <summary>Set values to memory addresses</summary>
        protected abstract void SetValues();

        /// <summary>Sets default values for each field</summary>
        protected virtual void SetDefaultValues() { }

        /// <summary>Add the options for a list of dropdowns</summary>
        /// <param name="list">List of dropdowns</param>
        /// <param name="dictionnary">Dictionnary containing the data to use for the dropdown options</param>
        protected void AddDropDownOptions(List<ImageComboBox> list, object dictionnary) {
            foreach (ImageComboBox dropdown in list) {
                dropdown.DataSource = new BindingSource(dictionnary, null);
                dropdown.DisplayMember = "Key";
                dropdown.ValueMember = "Value";
            }
        }

        /// <summary>Select the first index (0) for each dropdown in a list</summary>
        /// <param name="list">List of dropdowns</param>
        protected void SelectFirstDropdownsIndex(List<ImageComboBox> list) {
            foreach (ImageComboBox dropdown in list) {
                dropdown.SelectedIndex = 0;
            }
        }

        /// <summary>Set the value to the minimum for each NumericUpDown in a list</summary>
        /// <param name="list">List of NumericUpDowns</param>
        protected void SetNumericUpDownsToMin(List<NumericUpDown> list) {
            foreach (NumericUpDown field in list) {
                field.Value = field.Minimum;
            }
        }

        /// <summary>
        ///     Add the "Set Values" button to a subwindow.<br/>
        ///     <i>Used for setting values to memory addresses after editing.</i>
        /// </summary>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="form">Parent subwindow</param>
        protected void AddSetValuesButton(int positionX, int positionY, Control subwindow) {
            Button setValuesBtn = WinFormHelpers.CreateButton("setValuesBtn", "Set values", positionX, positionY, 75, 23, subwindow);
            setValuesBtn.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;
using BokInterface.Bike;

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

        public ShinbokSolarBikeEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses ShinbokAddresses) {

            _memoryValues = memoryValues;
            _shinbokAddresses = ShinbokAddresses;
            _shinbokBikeParts = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(411, 279);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.solarBikeEditorOpened = false;
            });

            // Add elements & show the subwindow
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

            // Generate & add options to dropdowns
            GenerateDropDownOptions();

            // Set default values for each field
            SetDefaultValues();

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setBikePartsButton", "Set values", 349, 252, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        protected void SetValues() { }

        ///<summary>
        ///<para>Method for setting memory values</para>
        ///<para>This is separated because we use the switch inside on different types</para>
        ///</summary>
        ///<param name="subList"><c>Sublit / dictionnary the key belongs to</c></param>
        ///<param name="valueKey"><c>strng</c>Key withint the dictionnary</param>
        ///<param name="value"><c>decimal</c>Value to set</param>
        private void SetMemoryValue(string subList, string valueKey, decimal value) { }

        ///<summary>Generates the options for the dropdowns</summary>
        private void GenerateDropDownOptions() { }

        ///<summary>Get a bike part from the bike parts list by using its value</summary>
        ///<param name="value"><c>decimal</c>Value</param>
        ///<returns><c>ShinbokBikePart</c>Bike part</returns>
        private ShinbokBikePart? GetBikePartByValue(decimal value) {
            foreach (KeyValuePair<string, ShinbokBikePart> index in _shinbokBikeParts.All) {
                ShinbokBikePart bikePart = index.Value;
                if (bikePart.value == value) {
                    return bikePart;
                }
            }

            return null;
        }

        protected void SetDefaultValues() { }
    }
}
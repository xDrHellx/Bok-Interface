using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Utils;

namespace BokInterface.Tools.SolarBankInterestsSimulator {
    /// <summary>Class for the Solar bank interests simulator for Bok 2 and 3</summary>
    class SolarBankInterestsSimulator : Form {

        #region Properties

        protected string name = "solarBankInterestsSimulator",
            title = "Solar bank interests simulator";

        private readonly DataTable _dataTable = new();
        private DataGridView? _dataGridView;
        private NumericUpDown _baseSollsNumDown = new();
        private Button _calculateInterestsBtn = new();
        private ComboBox _interestsRateDropDown = new();
        private readonly Dictionary<int, double> _interestsRates = new() {
            {1, 1.562500},
            {3, 3.125000},
            {4, 4.687500},
            {6, 6.250000},
            {7, 7.812500},
            {9, 9.375000},
            {10, 10.937500},
            {12, 12.500000},
            {14, 14.062500},
            {15, 15.625000},
            {17, 17.187500},
            {18, 18.750000},
            {20, 20.312500}
        };

        #endregion

        #region Constructor

        public SolarBankInterestsSimulator(BokInterface bokInterface) {
            Owner = bokInterface;
            Icon = bokInterface.Icon;
            Name = name;
            Text = title;
            AutoScaleDimensions = new SizeF(6F, 15F);
            AutoScaleMode = AutoScaleMode.Inherit;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = SystemColors.Control;
            Font = WinFormHelpers.defaultFont;
            AutoScroll = true;
            ClientSize = new Size(500, 208);

            // Add elements & show the subwindow
            AddControls();
            Show();
            ActiveControl = null;
        }

        /// <summary>Add the form control</summary>
        private void AddControls() {
            WinFormHelpers.CreateLabel("interestsRateLbl", "Interests rate", 5, 105, 80, 15, this, textAlignment: "MiddleRight");
            WinFormHelpers.CreateLabel("baseSollsLbl", "Solls", 5, 132, 80, 15, this, textAlignment: "MiddleRight");
            WinFormHelpers.CreateTextBox("info",
                "Regarding interest and rate:"
                + "\r\n- Interest accumulates when the day rolls over at midnight in-game."
                + "\r\n- Up to 10 days when the date at game start is greater than the date on the save file."
                + "\r\n- The game also gives 1 Soll for free each time."
                + "\r\n- Rate is constant over multiple days of accumulation."
                + "\r\n- Truncated when displayed at the bank (i.e. it may display 15%, but may actually be 15.7%).",
                0, 0, ClientSize.Width, 98, this
            );

            _interestsRateDropDown = WinFormHelpers.CreateDropDownList("interestsRate", 86, 103, 50, 23, this, visibleOptions: 10);
            _interestsRateDropDown.DataSource = new BindingSource(_interestsRates, null);
            _interestsRateDropDown.DisplayMember = "Key";
            _interestsRateDropDown.ValueMember = "Value";
            _baseSollsNumDown = WinFormHelpers.CreateNumericUpDown("baseSolls", 1000, 86, 130, 50, 23, 1, 9999, control: this);

            _calculateInterestsBtn = WinFormHelpers.CreateButton("calculateInterestsBtn", "Calculate solar bank interests", 145, 103, 350, 50, this);
            _calculateInterestsBtn.Click += new EventHandler(delegate (object sender, EventArgs e) {
                GenerateDataTable();
            });

            // Generate empty data table at creation
            GenerateDataTable();
        }

        #endregion

        #region DataTable generation

        /// <summary>Generate the Data Table containing the memory addresses, values and infos</summary>
        private void GenerateDataTable() {

            // Clear the table & subwindow
            _dataTable.Columns.Clear();
            _dataTable.Rows.Clear();
            _dataTable.Clear();

            // Generate table data for the current game
            _dataGridView = WinFormHelpers.CreateDataGridView("interestsGrid", _dataTable, 0, 162, ClientSize.Width, 46, this);
            _dataGridView.TopLeftHeaderCell.Value = "Day";
            GenerateTableData();
            SetColumnsStyle();
        }

        /// <summary>Set columns styles (width, text-alignment, ...)</summary>
        private void SetColumnsStyle() {
            if (_dataGridView != null) {
                for (int i = 0; i < 10; i++) {
                    if (_dataGridView.Columns.Contains((i + 1).ToString())) {
                        _dataGridView.Columns[i].Width = 10;
                        _dataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
        }

        /// <summary>Generate the table data</summary>
        private void GenerateTableData() {

            // Get values from fields
            KeyValuePair<int, double> selectedRate = (KeyValuePair<int, double>)_interestsRateDropDown.SelectedItem;
            double interestsRate = selectedRate.Value;
            double currentSolls = (double)_baseSollsNumDown.Value;

            // Generate data
            DataRow row = _dataTable.NewRow();
            for (int i = 0; i < 10; i++) {

                // Generate column
                _dataTable.Columns.Add((i + 1).ToString());

                /**
                 * Calculate & add solls to column
                 * Note: the game gives 1 extra soll each day (up to 10 days)
                 */
                double interests = currentSolls * interestsRate / 100;
                currentSolls = Math.Floor(currentSolls + interests) + 1;
                row[i] = currentSolls > 9999 ? 9999 : currentSolls;
            }

            _dataTable.Rows.Add(row);
        }

        #endregion
    }
}
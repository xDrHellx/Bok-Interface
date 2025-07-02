using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.All;

namespace BokInterface.Tools.SolarBankInterestsSimulator {
    /// <summary>Class for the Solar bank interests simulator for Bok 2 and 3</summary>
    class SolarBankInterestsSimulator : Form {

        #region Properties

        protected string name = "solarBankInterestsSimulator",
            title = "Solar bank interests simulator";

        private readonly DataTable _dataTable = new();
        private DataGridView? _dataGridView;
        private NumericUpDown _interestsRateNumDown = new(),
            _baseSollsNumDown = new();
        private Button _calculateInterestsBtn = new();

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
                + "\r\n- Rate is constant over multiple days of accumulation."
                + "\r\n- Truncated when displayed at the bank (i.e. it may display 15%, but may actually be 15.7%)."
                + "\r\n- Can be any value between 1% and ~21% (will only display as 20%).",
                0, 0, ClientSize.Width, 98, this
            );

            _interestsRateNumDown = WinFormHelpers.CreateNumericUpDown("interestsRate", (decimal)1.70, 86, 103, 50, 23, maxValue: 21, nbDecimals: 2, control: this);
            _baseSollsNumDown = WinFormHelpers.CreateNumericUpDown("baseSolls", 1000, 86, 130, 50, 23, maxValue: 9999, control: this);

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
            double currentSolls = (double)_baseSollsNumDown.Value;
            double interestsRate = (double)_interestsRateNumDown.Value;

            // Generate data
            DataRow row = _dataTable.NewRow();
            for (int i = 0; i < 10; i++) {

                // Generate column
                _dataTable.Columns.Add((i + 1).ToString());

                // Calculate & add solls to column
                double interests = currentSolls * interestsRate / 100;
                currentSolls = Math.Floor(currentSolls + interests);
                row[i] = currentSolls > 9999 ? 9999 : currentSolls;
            }

            _dataTable.Rows.Add(row);
        }

        #endregion
    }
}
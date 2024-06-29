using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Tools.MemoryValuesListing {
    /// <summary>Shows the list of all memory addresses listed for a game with their values, based on the ones added in the Bok Interface itself</summary>
    class MemoryValuesListing : Form {

        #region Tool properties & instances

        public int index = 0;
        private readonly DataTable _dataTable = new();
        private DataGridView? _dataGridView;
        private readonly BokInterface _bokInterface;
        private readonly dynamic? _memAddresses;

        #endregion

        #region Subwindow properties

        protected string name = "memoryValuesListing",
            title = "Memory Values List";
        protected int width = 650,
            height = 500;

        #endregion

        #region Constructors

        public MemoryValuesListing(BokInterface bokInterface, BoktaiAddresses boktaiAddresses) {
            Owner = _bokInterface = bokInterface;
            _memAddresses = boktaiAddresses;
            Icon = _bokInterface.Icon;
            InitializeSubwindowProperties();
        }

        public MemoryValuesListing(BokInterface bokInterface, ZoktaiAddresses zoktaiAddresses) {
            Owner = _bokInterface = bokInterface;
            _memAddresses = zoktaiAddresses;
            Icon = _bokInterface.Icon;
            InitializeSubwindowProperties();
        }

        public MemoryValuesListing(BokInterface bokInterface, ShinbokAddresses shinbokAddresses) {
            Owner = _bokInterface = bokInterface;
            _memAddresses = shinbokAddresses;
            Icon = _bokInterface.Icon;
            InitializeSubwindowProperties();
        }

        public MemoryValuesListing(BokInterface bokInterface, LunarKnightsAddresses lunarKnightsAddresses) {
            Owner = _bokInterface = bokInterface;
            _memAddresses = lunarKnightsAddresses;
            Icon = _bokInterface.Icon;
            InitializeSubwindowProperties();
        }

        /// <summary>Initialize subwindow properties</summary>
        protected void InitializeSubwindowProperties() {
            Name = name;
            Text = title;
            AutoScaleDimensions = new SizeF(6F, 15F);
            AutoScaleMode = AutoScaleMode.Inherit;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = SystemColors.Control;
            Font = WinFormHelpers.defaultFont;
            AutoScroll = true;
            ClientSize = new Size(width, height);

            // Show the subwindow
            Show();
        }

        #endregion

        #region Drawing methods

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            _dataGridView = null;
            GenerateDataTable();
        }

        #endregion

        #region DataTable generation methods

        /// <summary>Generate the Data Table containing the memory addresses, values and infos</summary>
        private void GenerateDataTable() {

            // Clear the table & subwindow
            _dataTable.Columns.Clear();
            _dataTable.Rows.Clear();
            _dataTable.Clear();

            // Generate table data for the current game
            GenerateColumns();
            bool rowsGenerated = GenerateTableData();
            if (rowsGenerated == false) {
                // If no rows were generated, stop
                return;
            }

            // Show table in subwindow & set columns styles
            _dataGridView = WinFormHelpers.CreateDataGridView("mvlGrid", _dataTable, 5, 5, ClientSize.Width - 10, ClientSize.Height - 10, this);
            SetColumnsStyle();
        }

        /// <summary>Simplified method for generating columns for the data table</summary>
        private void GenerateColumns() {
            _dataTable.Columns.Add("Name");
            _dataTable.Columns.Add("Address");
            _dataTable.Columns.Add("Type");
            _dataTable.Columns.Add("Domain");
            _dataTable.Columns.Add("Notes");
        }

        /// <summary>Set columns styles (width, text-alignment, ...)</summary>
        private void SetColumnsStyle() {
            if (_dataGridView != null) {

                // Width
                _dataGridView.Columns[1].Width = 70; // Address
                _dataGridView.Columns[2].Width = 40; // Type
                _dataGridView.Columns[3].Width = 60; // Domain

                // Text alignment
                _dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                _dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        /// <summary>Simplified method for generating rows for the data table from a dictionnary</summary>
        private void GenerateRows(IDictionary<string, MemoryAddress> dictionnary) {
            foreach (KeyValuePair<string, MemoryAddress> row in dictionnary) {
                try {
                    // Try getting the MemoryAddress instance & adding the row
                    MemoryAddress memAddress = row.Value;
                    _dataTable.Rows.Add(
                        Utilities.FormatMemoryAddressName(row.Key),
                        "0x" + memAddress.Address.ToString("X"),
                        memAddress.Type,
                        memAddress.Domain,
                        memAddress.Note
                    );
                } catch {
                    // If anything fails just skip to the next dictionnary entry
                    continue;
                }
            }
        }

        /// <summary>Generate the table data for the corresponding game</summary>
        /// <returns><c>bool</c>True if rows were generated, false otherwise</returns>
        private bool GenerateTableData() {
            if (_memAddresses == null) {
                return false;
            }

            switch (BokInterface.shorterGameName) {
                case "Boktai":
                    GenerateRows(_memAddresses.Django);
                    GenerateRows(_memAddresses.Inventory);
                    GenerateRows(_memAddresses.Gardening);
                    GenerateRows(_memAddresses.Misc);
                    return true;
                case "Zoktai":
                    GenerateRows(_memAddresses.Django);
                    GenerateRows(_memAddresses.Sabata);
                    GenerateRows(_memAddresses.Inventory);
                    GenerateRows(_memAddresses.Magics);
                    GenerateRows(_memAddresses.Misc);
                    return true;
                case "Shinbok":
                    GenerateRows(_memAddresses.Django);
                    GenerateRows(_memAddresses.Sabata);
                    GenerateRows(_memAddresses.Solls);
                    GenerateRows(_memAddresses.Bike);
                    GenerateRows(_memAddresses.Misc);
                    return true;
                case "LunarKnights":
                    GenerateRows(_memAddresses.Django);
                    GenerateRows(_memAddresses.Sabata);
                    GenerateRows(_memAddresses.Inventory);
                    GenerateRows(_memAddresses.Misc);
                    return true;
                default:
                    // If game is not handled, do nothing
                    return false;
            }
        }

        #endregion
    }
}
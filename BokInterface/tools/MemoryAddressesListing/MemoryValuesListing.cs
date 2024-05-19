using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Tools.MemoryValuesListing {
    /// <summary>Shows the list of all memory addresses listed for a game with their values, based on the ones added in the Bok Interface itself</summary>
    class MemoryValuesListing : Form {

        #region Main properties

        public int index = 0;
        private readonly DataTable _dataTable = new();
        private DataGridView? _dataGridView;
        private readonly string _currentGame = "";

        #endregion

        #region Memory addresses properties

        private readonly BoktaiAddresses _boktaiAddresses = new();
        private readonly ZoktaiAddresses _zoktaiAddresses = new();
        private readonly ShinbokAddresses _shinbokAddresses = new();
        private readonly LunarKnightsAddresses _lunarKnightsAddresses = new();

        #endregion

        #region Subwindow-related methods

        public MemoryValuesListing(string name, string title, int width, int height, string currentGame, string icon = "", Form? parentForm = null) {
            Name = name;
            Text = title;
            Icon = GetIcon(icon);
            AutoScaleDimensions = new SizeF(6F, 15F);
            AutoScaleMode = AutoScaleMode.Inherit;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = SystemColors.Control;
            Font = WinFormHelpers.defaultFont;
            AutoScroll = true;
            ClientSize = new Size(width, height);
            _currentGame = currentGame;

            if (parentForm != null) {
                Owner = parentForm;
            }
        }

        /// <summary>Get the specified icon if it exist</summary>
        /// <param name="fileName">File name (without .ico extension)</param>
        /// <returns><c>System.Drawing.Icon</c>Specified Icon instance (or default if the specified icon could not be found)</returns>
        protected Icon GetIcon(string fileName) {
            return fileName == "" ? Icon : (Icon)Properties.Resources.ResourceManager.GetObject(fileName);
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            _dataGridView = null;

            // If current game is not handled, stop
            if (_currentGame == "") {
                return;
            }

            GenerateDataTable();
        }

        #endregion

        #region Data Table generation methods

        /// <summary>Generate the Data Table containing the memory addresses, values and infos</summary>
        private void GenerateDataTable() {

            // Clear the table & subwindow
            _dataTable.Columns.Clear();
            _dataTable.Rows.Clear();
            _dataTable.Clear();

            // Generate table data for the current game
            GenerateColumns();
            GenerateTableData(_currentGame);

            // Show table in subwindow & set columns styles
            _dataGridView = WinFormHelpers.CreateDataGridView("memValuesGrid", _dataTable, 5, 5, ClientSize.Width - 10, ClientSize.Height - 10, this);
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
        /// <param name="gameName">Current game name</param>
        private void GenerateTableData(string gameName) {
            switch (gameName) {
                case "Boktai":
                    GenerateRows(_boktaiAddresses.Django);
                    GenerateRows(_boktaiAddresses.Inventory);
                    GenerateRows(_boktaiAddresses.Gardening);
                    GenerateRows(_boktaiAddresses.Misc);
                    break;
                case "Zoktai":
                    GenerateRows(_zoktaiAddresses.Django);
                    GenerateRows(_zoktaiAddresses.Inventory);
                    GenerateRows(_zoktaiAddresses.Magics);
                    GenerateRows(_zoktaiAddresses.Misc);
                    break;
                case "Shinbok":
                    GenerateRows(_shinbokAddresses.Django);
                    GenerateRows(_shinbokAddresses.Solls);
                    GenerateRows(_shinbokAddresses.Bike);
                    GenerateRows(_shinbokAddresses.Misc);
                    break;
                case "LunarKnights":
                    GenerateRows(_lunarKnightsAddresses.Aaron);
                    GenerateRows(_lunarKnightsAddresses.Lucian);
                    GenerateRows(_lunarKnightsAddresses.Inventory);
                    GenerateRows(_lunarKnightsAddresses.Misc);
                    break;
                default:
                    // If game is not handled, do nothing
                    break;
            }
        }

        #endregion
    }
}
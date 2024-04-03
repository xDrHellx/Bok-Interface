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
        private readonly DataTable dataTable = new();
        private readonly string currentGame = "";

        #endregion

        #region Memory addresses properties

        private readonly BoktaiAddresses boktaiAddresses = new();
        private readonly ZoktaiAddresses zoktaiAddresses = new();
        private readonly ShinbokAddresses shinbokAddresses = new();
        private readonly LunarKnightsAddresses lunarKnightsAddresses = new();

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
            Font = BokInterface.defaultFont;
            AutoScroll = true;
            ClientSize = new Size(width, height);
            this.currentGame = currentGame;

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

            // If current game is not handled, stop
            if (currentGame == "") {
                return;
            }

            GenerateDataTable();
        }

        #endregion

        #region Data Table generation methods

        /// <summary>Generate the Data Table containing the memory addresses, values and infos</summary>
        private void GenerateDataTable() {

            // Clear the table & subwindow
            dataTable.Columns.Clear();
            dataTable.Rows.Clear();
            dataTable.Clear();

            // Generate table data for the current game
            GenerateColumns();
            GenerateTableData(currentGame);

            // Show table in subwindow
            CreateDataGridView("memValuesGrid", dataTable, 5, 5, ClientSize.Width - 10, ClientSize.Height - 10, true);
        }

        /// <summary>Simplified method for generating columns for the data table</summary>
        private void GenerateColumns() {
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("Value");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("Domain");
            dataTable.Columns.Add("Notes");
        }

        /// <summary>Simplified method for generating rows for the data table from a dictionnary</summary>
        private void GenerateRows(IDictionary<string, MemoryAddress> dictionnary) {
            foreach (KeyValuePair<string, MemoryAddress> row in dictionnary) {
                try {
                    // Try getting the MemoryAddress instance & adding the row
                    MemoryAddress memAddress = row.Value;
                    dataTable.Rows.Add(
                        Utilities.FormatMemoryAddressName(row.Key),
                        "0x" + memAddress.address.ToString("X"),
                        Utilities.ReadMemoryAddress(memAddress.address, memAddress.type, memAddress.domain),
                        memAddress.type,
                        memAddress.domain,
                        memAddress.note
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
                    GenerateRows(boktaiAddresses.Django);
                    GenerateRows(boktaiAddresses.Inventory);
                    GenerateRows(boktaiAddresses.Gardening);
                    GenerateRows(boktaiAddresses.Misc);
                    break;
                case "Zoktai":
                    GenerateRows(zoktaiAddresses.Django);
                    GenerateRows(zoktaiAddresses.Inventory);
                    GenerateRows(zoktaiAddresses.Magics);
                    GenerateRows(zoktaiAddresses.Misc);
                    break;
                case "Shinbok":
                    GenerateRows(shinbokAddresses.Django);
                    GenerateRows(shinbokAddresses.Solls);
                    GenerateRows(shinbokAddresses.Bike);
                    GenerateRows(shinbokAddresses.Misc);
                    break;
                case "LunarKnights":
                    GenerateRows(lunarKnightsAddresses.Aaron);
                    GenerateRows(lunarKnightsAddresses.Lucian);
                    GenerateRows(lunarKnightsAddresses.Inventory);
                    GenerateRows(lunarKnightsAddresses.Misc);
                    break;
                default:
                    // If game is not handled, do nothing
                    break;
            }
        }

        #endregion

        #region Subwindow elements creation methods

        /// <summary>Simplified method for creating a data grid view</summary>
        /// <param name="name">Group name</param>
        /// <param name="data">Data to show</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="addToWindow">Set to true to add the element directly to the subwindow</param>
        /// <returns><c>System.Windows.Forms.GroupBox</c>Data grid view instance</returns>
        private DataGridView CreateDataGridView(string name, DataTable data, int positionX, int positionY, int width, int height, bool addToWindow = false) {

            DataGridView grid = new() {
                Name = name,
                DataSource = data,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                AutoSize = false,
                TabIndex = 1,
                Anchor = BokInterface.defaultAnchor,
                Font = BokInterface.defaultFont,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Dock = DockStyle.Fill,
                EnableHeadersVisualStyles = false,
            };

            // Set a specific color for the header
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            // Add to subwindow
            if (addToWindow == true) {
                Controls.Add(grid);
            }

            return grid;
        }

        #endregion
    }
}
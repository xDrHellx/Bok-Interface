using System.Drawing;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Inventory {
    /// <summary>Inventory editor for Boktai</summary>
    class BoktaiInventoryEditor : InventoryEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly BoktaiAddresses _boktaiAddresses;

        #endregion

        public BoktaiInventoryEditor(BokInterface bokInterface, MemoryValues memoryValues, BoktaiAddresses BoktaiAddresses) {

            _memoryValues = memoryValues;
            _bokInterface = bokInterface;
            _boktaiAddresses = BoktaiAddresses;

            Name = name;
            Text = text;
            Icon = _bokInterface.Icon;
            AutoScaleDimensions = new SizeF(6F, 15F);
            AutoScaleMode = AutoScaleMode.Inherit;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = SystemColors.Control;
            Font = WinFormHelpers.defaultFont;
            AutoScroll = true;
            Owner = _bokInterface;
            ClientSize = new Size(430, 231);

            // Generate the subwindow & add the onClose event to it
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.inventoryEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() { }

        protected override void SetValues() { }
    }
}
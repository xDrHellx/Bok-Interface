using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Inventory {
    /// <summary>Inventory editor for Boktai 3</summary>
    class ShinbokInventoryEditor : InventoryEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _shinbokAddresses;

        #endregion

        public ShinbokInventoryEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses ShinbokAddresses) {

            _memoryValues = memoryValues;
            _bokInterface = bokInterface;
            _shinbokAddresses = ShinbokAddresses;

            Icon = _bokInterface.Icon;
            Owner = _bokInterface;

            SetFormParameters(400, 400);

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
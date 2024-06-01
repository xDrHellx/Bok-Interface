using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Inventory {
    /// <summary>Inventory editor for Lunar Knights / Boktai DS</summary>
    class LunarKnightsInventoryEditor : InventoryEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly LunarKnightsAddresses _lunarKnightsAddresses;

        #endregion

        public LunarKnightsInventoryEditor(BokInterface bokInterface, MemoryValues memoryValues, LunarKnightsAddresses LunarKnightsAddresses) {

            _memoryValues = memoryValues;
            _lunarKnightsAddresses = LunarKnightsAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

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
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.KeyItems {
    /// <summary>Key items editor for Lunar Knights / Boktai DS</summary>
    class LunarKnightsKeyItemsEditor : KeyItemsEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly LunarKnightsAddresses _lunarKnightsAddresses;

        #endregion

        public LunarKnightsKeyItemsEditor(BokInterface bokInterface, MemoryValues memoryValues, LunarKnightsAddresses LunarKnightsAddresses) {

            _memoryValues = memoryValues;
            _lunarKnightsAddresses = LunarKnightsAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(400, 400);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.keyItemsEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() { }

        protected override void SetValues() { }
    }
}
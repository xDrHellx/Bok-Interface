using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Weapons {
    /// <summary>Weapons editor for Boktai</summary>
    class BoktaiWeaponsEditor : WeaponsEditor {

        #region Instances

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly BoktaiAddresses _boktaiAddresses;

        #endregion

        public BoktaiWeaponsEditor(BokInterface bokInterface, MemoryValues memoryValues, BoktaiAddresses BoktaiAddresses) {

            _memoryValues = memoryValues;
            _boktaiAddresses = BoktaiAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(400, 400);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.weaponsEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() { }

        protected override void SetValues() { }
    }
}
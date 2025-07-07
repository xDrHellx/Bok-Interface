using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.KeyItems {
    ///<summary>Key items editor for Boktai</summary>
    ///<remarks>
    ///Key items in Boktai are in the same "inventoey" as normal items.<br/>
    ///This class might not be necessary.
    ///</remarks>
    class BoktaiKeyItemsEditor : KeyItemsEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly BoktaiAddresses _boktaiAddresses;

        #endregion

        #region Constructor

        public BoktaiKeyItemsEditor(BokInterface bokInterface, MemoryValues memoryValues, BoktaiAddresses BoktaiAddresses) {

            _memoryValues = memoryValues;
            _boktaiAddresses = BoktaiAddresses;
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

        #endregion

        #region Elements

        protected override void AddElements() { }

        #endregion

        #region Values setting

        protected override void SetValues() { }

        #endregion
    }
}
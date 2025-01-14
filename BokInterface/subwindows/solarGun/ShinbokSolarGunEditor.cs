using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;
using BokInterface.Weapons;

namespace BokInterface.solarGun {
    /// <summary>Solar gun editor for Boktai 3</summary>
    class ShinbokSolarGunEditor : SolarGunEditor {

        #region Instances

        protected readonly List<RadioButton> radioButtons = [];

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _shinbokAddresses;
        private readonly ShinbokGuns _shinbokGuns;

        #endregion

        #region Form elements

        protected readonly List<ImageComboBox> dropDownLists = [];
        protected readonly List<NumericUpDown> numericUpDowns = [];
        protected TabControl inventoryTabControl = new();
        protected TabPage lensTab = new(),
            framesTab = new();
        protected CheckGroupBox frameSlot1group = new(),
            frameSlot2group = new(),
            frameSlot3group = new(),
            frameSlot4group = new(),
            frameSlot5group = new(),
            frameSlot6group = new(),
            frameSlot7group = new(),
            frameSlot8group = new(),
            frameSlot9group = new(),
            frameSlot10group = new(),
            frameSlot11group = new(),
            frameSlot12group = new(),
            lensSlot1group = new(),
            lensSlot2group = new(),
            lensSlot3group = new(),
            lensSlot4group = new(),
            lensSlot5group = new(),
            lensSlot6group = new(),
            lensSlot7group = new(),
            lensSlot8group = new();

        #endregion

        public ShinbokSolarGunEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses ShinbokAddresses) {

            _memoryValues = memoryValues;
            _shinbokAddresses = ShinbokAddresses;
            _shinbokGuns = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(699, 312);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.solarGunEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() {

            inventoryTabControl = WinFormHelpers.CreateTabControl("inventories_tab", 5, 5, 200, 300, this);
            lensTab = WinFormHelpers.CreateTabPage("lens_tab", "Lenses");
            framesTab = WinFormHelpers.CreateTabPage("lens_tab", "Frames");

            inventoryTabControl.Controls.Add(lensTab);
            inventoryTabControl.Controls.Add(framesTab);
        }

        protected override void SetValues() {

        }

    }
}
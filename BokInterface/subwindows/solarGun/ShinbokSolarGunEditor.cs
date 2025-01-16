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

            SetFormParameters(428, 282);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.solarGunEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() {

            inventoryTabControl = WinFormHelpers.CreateTabControl("inventory_ctrl", 5, 5, 420, 244, this);
            AddLensTab();
            AddFramesTab();
        }

        /// <summary>Adds generated tab for lenses</summary>
        protected void AddLensTab() {
            lensTab = WinFormHelpers.CreateTabPage("lens_tab", "Lenses", tabControl: inventoryTabControl);

            // Slot 1
            CheckGroupBox slot1Group = WinFormHelpers.CreateCheckGroupBox("lens_slot1_group", "Slot 1", 6, 6, 130, 48, control: lensTab);
            WinFormHelpers.CreateDropDownList("slot1_gun_lens", 5, 19, 120, 23, slot1Group);

            // Slot 2
            CheckGroupBox slot2Group = WinFormHelpers.CreateCheckGroupBox("lens_slot2_group", "Slot 2", 140, 6, 130, 48, control: lensTab);
            WinFormHelpers.CreateDropDownList("slot2_gun_lens", 5, 19, 120, 23, slot2Group);

            // Slot 3
            CheckGroupBox slot3Group = WinFormHelpers.CreateCheckGroupBox("lens_slot3_group", "Slot 3", 6, 58, 130, 48, control: lensTab);
            WinFormHelpers.CreateDropDownList("slot3_gun_lens", 5, 19, 120, 23, slot3Group);

            // Slot 4
            CheckGroupBox slot4Group = WinFormHelpers.CreateCheckGroupBox("lens_slot4_group", "Slot 4", 140, 58, 130, 48, control: lensTab);
            WinFormHelpers.CreateDropDownList("slot4_gun_lens", 5, 19, 120, 23, slot4Group);

            // Slot 5
            CheckGroupBox slot5Group = WinFormHelpers.CreateCheckGroupBox("lens_slot5_group", "Slot 5", 6, 110, 130, 48, control: lensTab);
            WinFormHelpers.CreateDropDownList("slot5_gun_lens", 5, 19, 120, 23, slot5Group);

            // Slot 6
            CheckGroupBox slot6Group = WinFormHelpers.CreateCheckGroupBox("lens_slot6_group", "Slot 6", 140, 110, 130, 48, control: lensTab);
            WinFormHelpers.CreateDropDownList("slot6_gun_lens", 5, 19, 120, 23, slot6Group);

            // Slot 7
            CheckGroupBox slot7Group = WinFormHelpers.CreateCheckGroupBox("lens_slot7_group", "Slot 7", 6, 162, 130, 48, control: lensTab);
            WinFormHelpers.CreateDropDownList("slot7_gun_lens", 5, 19, 120, 23, slot7Group);

            // Slot 8
            CheckGroupBox slot8Group = WinFormHelpers.CreateCheckGroupBox("lens_slot8_group", "Slot 8", 140, 162, 130, 48, control: lensTab);
            WinFormHelpers.CreateDropDownList("slot8_gun_lens", 5, 19, 120, 23, slot8Group);
        }

        /// <summary>Adds generated tab for frames</summary>
        protected void AddFramesTab() {
            framesTab = WinFormHelpers.CreateTabPage("frames_tab", "Frames", tabControl: inventoryTabControl);

            // Slot 1
            CheckGroupBox slot1Group = WinFormHelpers.CreateCheckGroupBox("frame_slot1_group", "Slot 1", 6, 6, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot1_gun_frame", 5, 19, 120, 23, slot1Group);

            // Slot 2
            CheckGroupBox slot2Group = WinFormHelpers.CreateCheckGroupBox("frame_slot2_group", "Slot 2", 140, 6, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot2_gun_frame", 5, 19, 120, 23, slot2Group);

            // Slot 3
            CheckGroupBox slot3Group = WinFormHelpers.CreateCheckGroupBox("frame_slot3_group", "Slot 3", 274, 6, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot3_gun_frame", 5, 19, 120, 23, slot3Group);

            // Slot 4
            CheckGroupBox slot4Group = WinFormHelpers.CreateCheckGroupBox("frame_slot4_group", "Slot 4", 6, 58, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot4_gun_frame", 5, 19, 120, 23, slot4Group);

            // Slot 5
            CheckGroupBox slot5Group = WinFormHelpers.CreateCheckGroupBox("frame_slot5_group", "Slot 5", 140, 58, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot5_gun_frame", 5, 19, 120, 23, slot5Group);

            // Slot 6
            CheckGroupBox slot6Group = WinFormHelpers.CreateCheckGroupBox("frame_slot6_group", "Slot 6", 274, 58, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot6_gun_frame", 5, 19, 120, 23, slot6Group);

            // Slot 7
            CheckGroupBox slot7Group = WinFormHelpers.CreateCheckGroupBox("frame_slot7_group", "Slot 7", 6, 110, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot7_gun_frame", 5, 19, 120, 23, slot7Group);

            // Slot 8
            CheckGroupBox slot8Group = WinFormHelpers.CreateCheckGroupBox("frame_slot8_group", "Slot 8", 140, 110, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot8_gun_frame", 5, 19, 120, 23, slot8Group);

            // Slot 9
            CheckGroupBox slot9Group = WinFormHelpers.CreateCheckGroupBox("frame_slot9_group", "Slot 9", 274, 110, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot9_gun_frame", 5, 19, 120, 23, slot9Group);

            // Slot 10
            CheckGroupBox slot10Group = WinFormHelpers.CreateCheckGroupBox("frame_slot10_group", "Slot 10", 6, 162, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot10_gun_frame", 5, 19, 120, 23, slot10Group);

            // Slot 11
            CheckGroupBox slot11Group = WinFormHelpers.CreateCheckGroupBox("frame_slot11_group", "Slot 11", 140, 162, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot11_gun_frame", 5, 19, 120, 23, slot11Group);

            // Slot 12
            CheckGroupBox slot12Group = WinFormHelpers.CreateCheckGroupBox("frame_slot12_group", "Slot 12", 274, 162, 130, 48, control: framesTab);
            WinFormHelpers.CreateDropDownList("slot12_gun_frame", 5, 19, 120, 23, slot12Group);
        }

        protected override void SetValues() {

        }

    }
}
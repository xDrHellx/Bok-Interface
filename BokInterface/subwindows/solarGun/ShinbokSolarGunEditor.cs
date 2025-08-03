using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;
using BokInterface.Weapons;

namespace BokInterface.solarGun {
    /// <summary>Solar gun editor for Boktai 3</summary>
    class ShinbokSolarGunEditor : SolarGunEditor {

        #region Properties

        protected readonly List<RadioButton> radioButtons = [];

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _shinbokAddresses;
        private readonly ShinbokGuns _shinbokGuns;
        protected readonly List<ImageComboBox> lensesDropDownLists = [],
            framesDropDownLists = [];
        protected TabControl inventoryTabControl = new();
        protected TabPage lensTab = new(),
            framesTab = new();

        #endregion

        #region Constructor

        public ShinbokSolarGunEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses ShinbokAddresses) {

            _memoryValues = memoryValues;
            _shinbokAddresses = ShinbokAddresses;
            _shinbokGuns = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(411, 279);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.solarGunEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            inventoryTabControl = WinFormHelpers.CreateTabControl("inventory_ctrl", 5, 5, 420, 244, this);
            AddLensTab();
            AddFramesTab();

            // Generate & add options to dropdowns
            GenerateDropDownOptions();

            // Set default values for each field
            SetDefaultValues();

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 349, 252, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        /// <summary>Adds generated tab for lenses</summary>
        protected void AddLensTab() {
            lensTab = WinFormHelpers.CreateTabPage("lens_tab", "Lenses", tabControl: inventoryTabControl);

            // 1st row
            CheckGroupBox slot1Group = WinFormHelpers.CreateCheckGroupBox("lens_slot1_group", "Slot 1", 6, 6, 130, 48, control: lensTab);
            lensesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_gun_lens", 5, 19, 120, 23, slot1Group, visibleOptions: 5));

            CheckGroupBox slot2Group = WinFormHelpers.CreateCheckGroupBox("lens_slot2_group", "Slot 2", 140, 6, 130, 48, control: lensTab);
            lensesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_gun_lens", 5, 19, 120, 23, slot2Group, visibleOptions: 5));

            // 2nd row
            CheckGroupBox slot3Group = WinFormHelpers.CreateCheckGroupBox("lens_slot3_group", "Slot 3", 6, 58, 130, 48, control: lensTab);
            lensesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_gun_lens", 5, 19, 120, 23, slot3Group, visibleOptions: 5));

            CheckGroupBox slot4Group = WinFormHelpers.CreateCheckGroupBox("lens_slot4_group", "Slot 4", 140, 58, 130, 48, control: lensTab);
            lensesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_gun_lens", 5, 19, 120, 23, slot4Group, visibleOptions: 5));

            // 3rd row
            CheckGroupBox slot5Group = WinFormHelpers.CreateCheckGroupBox("lens_slot5_group", "Slot 5", 6, 110, 130, 48, control: lensTab);
            lensesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_gun_lens", 5, 19, 120, 23, slot5Group, visibleOptions: 5));

            CheckGroupBox slot6Group = WinFormHelpers.CreateCheckGroupBox("lens_slot6_group", "Slot 6", 140, 110, 130, 48, control: lensTab);
            lensesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_gun_lens", 5, 19, 120, 23, slot6Group, visibleOptions: 5));

            // 4th row
            CheckGroupBox slot7Group = WinFormHelpers.CreateCheckGroupBox("lens_slot7_group", "Slot 7", 6, 162, 130, 48, control: lensTab);
            lensesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_gun_lens", 5, 19, 120, 23, slot7Group, visibleOptions: 5));

            CheckGroupBox slot8Group = WinFormHelpers.CreateCheckGroupBox("lens_slot8_group", "Slot 8", 140, 162, 130, 48, control: lensTab);
            lensesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_gun_lens", 5, 19, 120, 23, slot8Group, visibleOptions: 5));
        }

        /// <summary>Adds generated tab for frames</summary>
        protected void AddFramesTab() {
            framesTab = WinFormHelpers.CreateTabPage("frames_tab", "Frames", tabControl: inventoryTabControl);

            // 1st row
            CheckGroupBox slot1Group = WinFormHelpers.CreateCheckGroupBox("frame_slot1_group", "Slot 1", 6, 6, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot1_gun_frame", 5, 19, 120, 23, slot1Group, visibleOptions: 5));

            CheckGroupBox slot2Group = WinFormHelpers.CreateCheckGroupBox("frame_slot2_group", "Slot 2", 140, 6, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot2_gun_frame", 5, 19, 120, 23, slot2Group, visibleOptions: 5));

            CheckGroupBox slot3Group = WinFormHelpers.CreateCheckGroupBox("frame_slot3_group", "Slot 3", 274, 6, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot3_gun_frame", 5, 19, 120, 23, slot3Group, visibleOptions: 5));

            // 2nd row
            CheckGroupBox slot4Group = WinFormHelpers.CreateCheckGroupBox("frame_slot4_group", "Slot 4", 6, 58, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot4_gun_frame", 5, 19, 120, 23, slot4Group, visibleOptions: 5));

            CheckGroupBox slot5Group = WinFormHelpers.CreateCheckGroupBox("frame_slot5_group", "Slot 5", 140, 58, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot5_gun_frame", 5, 19, 120, 23, slot5Group, visibleOptions: 5));

            CheckGroupBox slot6Group = WinFormHelpers.CreateCheckGroupBox("frame_slot6_group", "Slot 6", 274, 58, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot6_gun_frame", 5, 19, 120, 23, slot6Group, visibleOptions: 5));

            // 3rd row
            CheckGroupBox slot7Group = WinFormHelpers.CreateCheckGroupBox("frame_slot7_group", "Slot 7", 6, 110, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot7_gun_frame", 5, 19, 120, 23, slot7Group, visibleOptions: 5));

            CheckGroupBox slot8Group = WinFormHelpers.CreateCheckGroupBox("frame_slot8_group", "Slot 8", 140, 110, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot8_gun_frame", 5, 19, 120, 23, slot8Group, visibleOptions: 5));

            CheckGroupBox slot9Group = WinFormHelpers.CreateCheckGroupBox("frame_slot9_group", "Slot 9", 274, 110, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot9_gun_frame", 5, 19, 120, 23, slot9Group, visibleOptions: 5));

            // 4th row
            CheckGroupBox slot10Group = WinFormHelpers.CreateCheckGroupBox("frame_slot10_group", "Slot 10", 6, 162, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot10_gun_frame", 5, 19, 120, 23, slot10Group, visibleOptions: 5));

            CheckGroupBox slot11Group = WinFormHelpers.CreateCheckGroupBox("frame_slot11_group", "Slot 11", 140, 162, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot11_gun_frame", 5, 19, 120, 23, slot11Group, visibleOptions: 5));

            CheckGroupBox slot12Group = WinFormHelpers.CreateCheckGroupBox("frame_slot12_group", "Slot 12", 274, 162, 130, 48, control: framesTab);
            framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList("inventory_slot12_gun_frame", 5, 19, 120, 23, slot12Group, visibleOptions: 5));
        }

        ///<summary>Generates the options for the dropdowns</summary>
        private void GenerateDropDownOptions() {

            // Lenses
            foreach (ImageComboBox dropdown in lensesDropDownLists) {
                dropdown.DataSource = new BindingSource(_shinbokGuns.Lenses, null);
                dropdown.DisplayMember = "Key";
                dropdown.ValueMember = "Value";
            }

            // Frames
            foreach (ImageComboBox dropdown in framesDropDownLists) {
                dropdown.DataSource = new BindingSource(_shinbokGuns.Frames, null);
                dropdown.DisplayMember = "Key";
                dropdown.ValueMember = "Value";
            }
        }

        #endregion

        #region Values setting

        protected override void SetValues() {

            // Store the previous setting for BizHawk being paused
            _bokInterface._previousIsPauseSetting = APIs.Client.IsPaused();

            // Pause BizHawk
            APIs.Client.Pause();

            // Sets values for each lens slot
            for (int i = 0; i < lensesDropDownLists.Count; i++) {

                // If the slot is disabled, skip it
                if (lensesDropDownLists[i].Enabled == false) {
                    continue;
                }

                KeyValuePair<string, ShinbokLens> selectedOption = (KeyValuePair<string, ShinbokLens>)lensesDropDownLists[i].SelectedItem;
                ShinbokLens selectedItem = selectedOption.Value;

                /**
                 * Indicate which sublist to use for setting the value, based on the slot's name
                 * We only split on the first "_"
                 */
                string[] fieldParts = lensesDropDownLists[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], selectedItem.value);
            }

            // Do the same thing as above for each frame slot
            for (int i = 0; i < framesDropDownLists.Count; i++) {
                if (framesDropDownLists[i].Enabled == false) {
                    continue;
                }

                KeyValuePair<string, ShinbokFrame> selectedOption = (KeyValuePair<string, ShinbokFrame>)framesDropDownLists[i].SelectedItem;
                ShinbokFrame selectedItem = selectedOption.Value;

                string[] fieldParts = framesDropDownLists[i].Name.Split(['_'], 2);
                SetMemoryValue(fieldParts[0], fieldParts[1], selectedItem.value);
            }

            /**
             * If BizHawk was not paused before setting values, unpause it
             * Otherwise keep it paused
             */
            if (_bokInterface._previousIsPauseSetting == true) {
                APIs.Client.Unpause();
            }
        }

        ///<summary>
        ///<para>Method for setting memory values</para>
        ///<para>This is separated because we use the switch inside on different types</para>
        ///</summary>
        ///<param name="subList"><c>Sublit / dictionnary the key belongs to</c></param>
        ///<param name="valueKey"><c>strng</c>Key withint the dictionnary</param>
        ///<param name="value"><c>decimal</c>Value to set</param>
        private void SetMemoryValue(string subList, string valueKey, decimal value) {
            switch (subList) {
                case "inventory":
                    if (_memoryValues.Inventory.ContainsKey(valueKey) == true) {
                        _memoryValues.Inventory[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
                default:
                    if (_memoryValues.U16.ContainsKey(valueKey) == true) {
                        _memoryValues.U16[valueKey].Value = (uint)value;
                    } else if (_memoryValues.U32.ContainsKey(valueKey) == true) {
                        _memoryValues.U32[valueKey].Value = (uint)value;
                    }
                    break;
            }
        }

        protected override void SetDefaultValues() {

            /**
             * If Django's HP value is valid
             * (Invalid when below 0 or above 1000, ie when switching rooms, during bike races or on world map)
             */
            uint djangoCurrentHp = _memoryValues.Django["current_hp"].Value;
            if (djangoCurrentHp >= 0 && djangoCurrentHp <= 1000) {

                // Lenses
                foreach (ImageComboBox dropdown in lensesDropDownLists) {
                    /**
                     * Get the name of the field to retrieve the value from based on the dropdown's name (for example inventory_slotX_item => slotX_item)
                     * Then try getting the corresponding item & preselect it
                     */
                    string[] fieldParts = dropdown.Name.Split(['_'], 2);
                    ShinbokLens? selectedLens = GetLensByValue(_memoryValues.Inventory[fieldParts[1]].Value);
                    if (selectedLens != null) {
                        dropdown.SelectedIndex = dropdown.FindStringExact(selectedLens.name);
                    }
                }

                // Frames
                foreach (ImageComboBox dropdown in framesDropDownLists) {
                    string[] fieldParts = dropdown.Name.Split(['_'], 2);
                    ShinbokFrame? selectedFrame = GetFrameByValue(_memoryValues.Inventory[fieldParts[1]].Value);
                    if (selectedFrame != null) {
                        dropdown.SelectedIndex = dropdown.FindStringExact(selectedFrame.name);
                    }
                }
            } else {
                // If current stat is unvalid (for example because we are on the title screen or in a room transition), use specific values
                foreach (ImageComboBox dropdown in lensesDropDownLists) {
                    dropdown.SelectedIndex = 0;
                }

                foreach (ImageComboBox dropdown in framesDropDownLists) {
                    dropdown.SelectedIndex = 0;
                }
            }
        }

        ///<summary>Get a lens from the lenses list by using its value</summary>
        ///<param name="value"><c>decimal</c>Value</param>
        ///<returns><c>ShinbokLens</c>Lens</returns>
        private ShinbokLens? GetLensByValue(decimal value) {
            foreach (KeyValuePair<string, ShinbokLens> index in _shinbokGuns.Lenses) {
                ShinbokLens lens = index.Value;
                if (lens.value == value) {
                    return lens;
                }
            }

            return null;
        }

        ///<summary>Get a frame from the frames list by using its value</summary>
        ///<param name="value"><c>decimal</c>Value</param>
        ///<returns><c>ShinbokFrame</c>Frame</returns>
        private ShinbokFrame? GetFrameByValue(decimal value) {
            foreach (KeyValuePair<string, ShinbokFrame> index in _shinbokGuns.Frames) {
                ShinbokFrame frame = index.Value;
                if (frame.value == value) {
                    return frame;
                }
            }

            return null;
        }

        #endregion
    }
}

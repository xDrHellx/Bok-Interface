using System.Collections.Generic;

using BokInterface.Addresses;
using BokInterface.Utils;
using BokInterface.Weapons;

namespace BokInterface.solarGun {
    /// <summary>Solar gun editor for Boktai 3</summary>
    class ShinbokSolarGunEditor : SolarGunEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ShinbokAddresses _shinbokAddresses;
        private readonly ShinbokGuns _shinbokGuns;
        protected readonly List<ImageComboBox> lensesDropDownLists = [],
            framesDropDownLists = [];

        #endregion

        #region Constructor

        public ShinbokSolarGunEditor(BokInterface bokInterface, MemoryValues memoryValues, ShinbokAddresses ShinbokAddresses) {

            _memoryValues = memoryValues;
            _shinbokAddresses = ShinbokAddresses;
            _shinbokGuns = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(411, 279, name, text);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            inventoryTabControl = WinFormHelpers.CreateTabControl("inventory_ctrl", 5, 5, 420, 244, this);
            AddLensTab();
            AddFramesTab();

            // Add options to dropdowns
            AddDropDownOptions(lensesDropDownLists, _shinbokGuns.Lenses);
            AddDropDownOptions(framesDropDownLists, _shinbokGuns.Frames);

            // Set default values for each field
            SetDefaultValues();

            AddSetValuesButton(349, 252, this);
        }

        /// <summary>Adds generated tab for lenses</summary>
        protected void AddLensTab() {
            lensTab = WinFormHelpers.CreateTabPage("lens_tab", "Lenses", tabControl: inventoryTabControl);

            int xPos = 6,
                yPos = 6;
            for (int i = 1; i < 9; i++) {

                // Generate the group & the dropdown to it
                CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"lens_slot{i}_group", $"Slot {i}", xPos, yPos, 130, 48, control: lensTab);
                lensesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_gun_lens", 5, 19, 120, 23, group, visibleOptions: 5));

                // Offsets for position
                xPos += 134;
                if ((i % 2) == 0) {
                    xPos = 6;
                    yPos += 52;
                }
            }
        }

        /// <summary>Adds generated tab for frames</summary>
        protected void AddFramesTab() {
            framesTab = WinFormHelpers.CreateTabPage("frames_tab", "Frames", tabControl: inventoryTabControl);

            int xPos = 6,
                yPos = 6;
            for (int i = 1; i < 13; i++) {
                CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox($"frame_slot{i}_group", $"Slot {i}", xPos, yPos, 130, 48, control: framesTab);
                framesDropDownLists.Add(WinFormHelpers.CreateImageDropdownList($"inventory_slot{i}_gun_frame", 5, 19, 120, 23, group, visibleOptions: 5));

                xPos += 134;
                if ((i % 3) == 0) {
                    xPos = 6;
                    yPos += 52;
                }
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
        ///     Method for setting memory values.<br/>
        ///     This is separated because we use the switch inside on different types.
        ///</summary>
        ///<param name="subList"><c>Sublit / dictionnary the key belongs to</c></param>
        ///<param name="valueKey"><c>strng</c>Key withint the dictionnary</param>
        ///<param name="value"><c>decimal</c>Value to set</param>
        private void SetMemoryValue(string subList, string valueKey, decimal value) {
            if (subList == "inventory" && _memoryValues.Inventory.ContainsKey(valueKey) == true) {
                _memoryValues.Inventory[valueKey].Value = (uint)value;
            }
        }

        protected override void SetDefaultValues() {

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Utils;
using BokInterface.Weapons;

namespace BokInterface.solarGun {
    /// <summary>Solar gun editor for Boktai</summary>
    class BoktaiSolarGunEditor : SolarGunEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly BoktaiAddresses _boktaiAddresses;
        private readonly BoktaiGuns _boktaiGuns;
        protected TabControl inventoryTabControl = new();
        protected TabPage lensTab = new(),
            framesTab = new(),
            batteriesTab = new(),
            grenadesTab = new();
        protected readonly List<ImageCheckBox> framesCheckboxes = [],
            batteriesCheckboxes = [];
        protected readonly List<RadioButton> lensesButtons = [];
        protected readonly List<NumericUpDown> grenadesNumericUpDowns = [];
        protected NumericUpDown pineappleCharges = new();

        #endregion

        #region Constructor

        public BoktaiSolarGunEditor(BokInterface bokInterface, MemoryValues memoryValues, BoktaiAddresses BoktaiAddresses) {

            _memoryValues = memoryValues;
            _boktaiAddresses = BoktaiAddresses;
            _boktaiGuns = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(408, 262);

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

            inventoryTabControl = WinFormHelpers.CreateTabControl("inventory_ctrl", 5, 5, 417, 220, this);
            AddLensTab();
            AddFramesTab();
            AddBatteriesTab();
            AddGrenadesTab();

            // Set default values for each field
            SetDefaultValues();

            // Add warning
            Label expWarning = WinFormHelpers.CreateImageLabel("tooltip", "warning", 5, 232, this);
            WinFormHelpers.CreateLabel("warning", "Menu must be re-opened && gun parts must be equipped again for all changes to fully take effect.", 23, 222, 319, 40, this, textAlignment: "MiddleLeft");

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 346, 235, 75, 23, this);
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
            lensTab.AutoScroll = true;

            // Adds the fields in the tab
            int posX = 5,
                posY = 5,
                n = 0;
            foreach (KeyValuePair<string, BoktaiLens> entry in _boktaiGuns.Lenses) {

                // Check that the lens can be used for a checkbox
                BoktaiLens lens = entry.Value;
                if (lens.icon == null) {
                    continue;
                }

                // Add the group containing the lens icon & radio buttons
                GroupBox lensGroup = WinFormHelpers.CreateGroupBox(entry.Key + "_group", lens.name, posX, posY, 97, 92, lensTab);
                WinFormHelpers.CreatePictureBox(entry.Key + "_img", lens.icon, 5, 15, lensGroup);
                lensesButtons.Add(WinFormHelpers.CreateRadioButton(entry.Key + "_none", "N/A", 48, 10, 48, 20, lensGroup, 0));
                lensesButtons.Add(WinFormHelpers.CreateRadioButton(entry.Key + "_lvl_1", "Lvl 1", 48, 30, 48, 20, lensGroup, 1));
                lensesButtons.Add(WinFormHelpers.CreateRadioButton(entry.Key + "_lvl_2", "Lvl 2", 48, 50, 48, 20, lensGroup, 2));
                lensesButtons.Add(WinFormHelpers.CreateRadioButton(entry.Key + "_lvl_3", "Lvl 3", 48, 70, 48, 20, lensGroup, 3));

                // Increment position values for the next lens
                posX += 101;

                // If 4th lens on the current row, updates values for positions
                n++;
                if ((n % 4) == 0) {
                    posX = 5;
                    posY += 93;
                }
            }
        }

        /// <summary>Adds generated tab for frames</summary>
        protected void AddFramesTab() {
            framesTab = WinFormHelpers.CreateTabPage("frames_tab", "Frames", tabControl: inventoryTabControl);
            framesTab.AutoScroll = true;

            // Adds the fields in the tab
            int posX = 5,
                posY = 5,
                n = 0;
            foreach (KeyValuePair<string, BoktaiFrame> entry in _boktaiGuns.Frames) {

                // Check that the frame can be used for a checkbox
                BoktaiFrame frame = entry.Value;
                if (frame.icon == null) {
                    continue;
                }

                // Add the checkbox & label for that frame
                ImageCheckBox frameCheckBox = WinFormHelpers.CreateImageCheckBox(entry.Key + "_check", frame.icon, posX, posY + (frame.name == "Guardian" ? 5 : 0), control: framesTab);
                Label frameLabel = WinFormHelpers.CreateLabel(entry.Key + "_label", frame.name, posX, posY + 44, 80, 18, framesTab, textAlignment: "MiddleLeft");
                framesCheckboxes.Add(frameCheckBox);

                // Increment position values for the next frame
                posX += 76;

                // If 4th frame on the current row, updates values for positions
                n++;
                if ((n % 4) == 0) {
                    posX = 5;
                    posY += 66;
                }
            }
        }

        /// <summary>Adds generated tab for batteries</summary>
        protected void AddBatteriesTab() {
            batteriesTab = WinFormHelpers.CreateTabPage("batteries_tab", "Batteries", tabControl: inventoryTabControl);
            batteriesTab.AutoScroll = true;

            // Adds the fields in the tab
            int posX = 5,
                n = 0,
                cboxHeight;
            foreach (KeyValuePair<string, BoktaiBattery> entry in _boktaiGuns.Batteries) {

                // Check that the battery can be used for a checkbox
                BoktaiBattery battery = entry.Value;
                if (battery.icon == null) {
                    continue;
                }

                // Ugly way to handle height adjustment for different icons
                if (n < 2) {
                    cboxHeight = 22;
                } else if (n < 4) {
                    cboxHeight = 19;
                } else if (n < 7) {
                    cboxHeight = 21;
                } else {
                    cboxHeight = 5;
                }

                // Add the checkbox & label
                ImageCheckBox batteryCheckbox = WinFormHelpers.CreateImageCheckBox(entry.Key + "_checkbox", battery.icon, posX, cboxHeight, control: batteriesTab);
                WinFormHelpers.CreateLabel(entry.Key + "_label", battery.name, posX, 53, 50, 18, batteriesTab, textAlignment: "MiddleLeft");
                batteriesCheckboxes.Add(batteryCheckbox);

                // Increment n & position for the next battery
                posX += 50;
                n++;
            }
        }

        /// <summary>Adds generated tab for grenades</summary>
        protected void AddGrenadesTab() {
            grenadesTab = WinFormHelpers.CreateTabPage("grenades_tab", "Grenades", tabControl: inventoryTabControl);
            grenadesTab.AutoScroll = true;

            int posX = 5,
                posY = 5,
                n = 0;
            foreach (KeyValuePair<string, BoktaiGrenade> entry in _boktaiGuns.Grenades) {

                BoktaiGrenade grenade = entry.Value;
                if (grenade.icon == null) {
                    continue;
                }

                // Add the group containg the icon & amount
                GroupBox group = WinFormHelpers.CreateGroupBox(entry.Key + "_grp", grenade.name, posX, posY, 90, n == 1 ? 105 : 68, grenadesTab);
                WinFormHelpers.CreatePictureBox(entry.Key + "_img", grenade.icon, 5, 20, group);
                WinFormHelpers.CreateLabel(entry.Key + "_label", "Amount", 34, 15, 55, 20, group);
                grenadesNumericUpDowns.Add(WinFormHelpers.CreateNumericUpDown(entry.Key + "_amount", 0, 44, 35, 40, 18, control: group));

                // Add charges for pineapple grenades
                if (n == 1) {
                    WinFormHelpers.CreateLabel(entry.Key + "_charges_label", "Charges", 34, 56, 55, 20, group);
                    pineappleCharges = WinFormHelpers.CreateNumericUpDown(entry.Key + "_charges", 0, 45, 76, 40, 18, maxValue: 5, control: group);
                }

                // Increment position values for the next grenade
                posX += 95;

                // If 4th grenade on the current row, move to 2nd row
                n++;
                if ((n % 4) == 0) {
                    posX = 5;
                    posY += 69;
                }

                // If 6th grenade adjust position again
                if (n == 5) {
                    posX += 95;
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

            // Set values for gun parts
            SetLensesValues();
            SetFramesValues();
            SetBatteriesValues();
            SetGrenadesValues();

            /**
             * If BizHawk was not paused before setting values, unpause it
             * Otherwise keep it paused
             */
            if (_bokInterface._previousIsPauseSetting == true) {
                APIs.Client.Unpause();
            }
        }

        /// <summary>Set values related to lenses</summary>
        private void SetLensesValues() {

            int bitPosition = 0,
                lensIndex = 0;
            uint newLensesValue = _boktaiAddresses.Inventory["lenses"].Value;
            foreach (RadioButton btn in lensesButtons) {
                if (btn.Enabled == false || btn.Checked == false) {
                    continue;
                }

                // Set all bits for the lens to 0
                newLensesValue = (uint)Utilities.SetBitToZero((int)newLensesValue, bitPosition + 2);
                newLensesValue = (uint)Utilities.SetBitToZero((int)newLensesValue, bitPosition + 1);
                newLensesValue = (uint)Utilities.SetBitToZero((int)newLensesValue, bitPosition);

                /**
                 * Set the checked lens lvl to 1 & adjust EXP (otherwise the lens will level up if it's too high)
                 * If the current EXP is between the range for that level, keep it
                 *
                 * Note: Dark Lens requires more EXP (lensIndex 7)
                 */
                uint currentExp = APIs.Memory.ReadU16(_boktaiAddresses.Inventory["lenses_exp"].Address + (lensIndex * 2));
                string[] nameParts = btn.Name.Split(['_'], 2);
                switch (nameParts[1]) {
                    case "lvl_3":
                        newLensesValue = (uint)Utilities.SetBitToOne((int)newLensesValue, bitPosition + 2);
                        if (
                            (lensIndex != 7 && currentExp < 2000)
                            ||
                            (lensIndex == 7 && currentExp < 4000)
                        ) {
                            APIs.Memory.WriteU16(_boktaiAddresses.Inventory["lenses_exp"].Address + (lensIndex * 2), (uint)(lensIndex == 7 ? 4000 : 2000));
                        }
                        break;
                    case "lvl_2":
                        newLensesValue = (uint)Utilities.SetBitToOne((int)newLensesValue, bitPosition + 1);
                        if (
                            (lensIndex != 7 && (currentExp >= 2000 || currentExp <= 500))
                            ||
                            (lensIndex == 7 && (currentExp >= 4000 || currentExp <= 1000))
                        ) {
                            APIs.Memory.WriteU16(_boktaiAddresses.Inventory["lenses_exp"].Address + (lensIndex * 2), (uint)(lensIndex == 7 ? 1000 : 500));
                        }
                        break;
                    case "lvl_1":
                        newLensesValue = (uint)Utilities.SetBitToOne((int)newLensesValue, bitPosition);
                        if (
                            (lensIndex != 7 && currentExp >= 500)
                            ||
                            (lensIndex == 7 && currentExp <= 1000)
                        ) {
                            APIs.Memory.WriteU16(_boktaiAddresses.Inventory["lenses_exp"].Address + (lensIndex * 2), 0);
                        }
                        break;
                    default:
                        break;
                }

                bitPosition += 3;
                lensIndex++;
            }

            // Update the bitmask for the lenses
            _boktaiAddresses.Inventory["lenses"].Value = newLensesValue;
        }

        /// <summary>Set values related to frames</summary>
        private void SetFramesValues() {

            int bitPosition = 0;
            uint newFramesValue = _boktaiAddresses.Inventory["frames"].Value;
            foreach (ImageCheckBox checkbox in framesCheckboxes) {
                if (checkbox.Enabled == false) {
                    continue;
                }

                /**
                 * Set the value for the bit corresponding to the frame
                 * (checked : 1 | unchecked : 0)
                 */
                if (checkbox.Checked == true) {
                    newFramesValue = (uint)Utilities.SetBitToOne((int)newFramesValue, bitPosition);
                } else {
                    newFramesValue = (uint)Utilities.SetBitToZero((int)newFramesValue, bitPosition);
                }

                bitPosition++;
            }

            // Update the bitmask
            _boktaiAddresses.Inventory["frames"].Value = newFramesValue;
        }

        /// <summary>Set values related to batteries</summary>
        private void SetBatteriesValues() {

            int bitPosition = 0;
            uint newBatteriesValue = _boktaiAddresses.Inventory["batteries"].Value;
            foreach (ImageCheckBox checkbox in batteriesCheckboxes) {
                if (checkbox.Enabled == false) {
                    continue;
                }

                newBatteriesValue = checkbox.Checked == true ? (uint)Utilities.SetBitToOne((int)newBatteriesValue, bitPosition) : (uint)Utilities.SetBitToZero((int)newBatteriesValue, bitPosition);
                bitPosition++;
            }

            // Updated the bitmask
            _boktaiAddresses.Inventory["batteries"].Value = newBatteriesValue;
        }

        /// <summary>Set values related to grenades</summary>
        private void SetGrenadesValues() {

            int bitPosition = 0;
            foreach (NumericUpDown field in grenadesNumericUpDowns) {
                if (field.Enabled == false) {
                    continue;
                }

                uint amount = (uint)field.Value;
                APIs.Memory.WriteU16(_boktaiAddresses.Inventory["grenade_amounts"].Address + bitPosition, amount);

                // Pineapple grenade charges
                if (field.Name == "Pineapple_amount") {
                    APIs.Memory.WriteU16(_boktaiAddresses.Inventory["pineapple_grenade_charges"].Address + bitPosition, (uint)pineappleCharges.Value);
                }

                bitPosition += 2;
            }
        }

        #endregion

        #region Default values

        protected override void SetDefaultValues() {

            uint statStructurePointer = _boktaiAddresses.Misc["stat"].Value;
            if (statStructurePointer > 0) {

                // For each lens check the option for its level (or N/A if unavailable)
                uint lenses = _boktaiAddresses.Inventory["lenses"].Value;
                int bitPosition = 0;
                for (int i = 0; i < 8; i++) {

                    // Determine which lens level is in the inventory
                    KeyValuePair<string, BoktaiLens> lens = _boktaiGuns.Lenses.ElementAt(i);
                    string btnName = lens.Key;
                    if (Utilities.IsBitOne(lenses, bitPosition + 2) == true) {
                        // Lvl 3
                        btnName += "_lvl_3";
                    } else if (Utilities.IsBitOne(lenses, bitPosition + 1) == true) {
                        // Lvl 2
                        btnName += "_lvl_2";
                    } else if (Utilities.IsBitOne(lenses, bitPosition) == true) {
                        // Lvl 1
                        btnName += "_lvl_1";
                    } else {
                        // None: the lens isn't in the inventory
                        btnName += "_none";
                    }

                    // Check the corresponding button
                    RadioButton btn = lensesButtons.Where(X => X.Name == btnName).FirstOrDefault();
                    if (btn != null) {
                        btn.Checked = true;
                    }

                    bitPosition += 3;
                }

                CheckBasedOnBitmaskValue(framesCheckboxes, (int)_boktaiAddresses.Inventory["frames"].Value);
                CheckBasedOnBitmaskValue(batteriesCheckboxes, (int)_boktaiAddresses.Inventory["batteries"].Value);

                pineappleCharges.Value = _boktaiAddresses.Inventory["pineapple_grenade_charges"].Value <= 5 ? _boktaiAddresses.Inventory["pineapple_grenade_charges"].Value : 0;

                bitPosition = 0;
                foreach (NumericUpDown field in grenadesNumericUpDowns) {
                    if (field.Enabled == true) {
                        field.Value = APIs.Memory.ReadU16(_boktaiAddresses.Inventory["grenade_amounts"].Address + bitPosition);
                    }

                    bitPosition += 2;
                }
            } else {
                /**
                 * If current stat is unvalid (for example because we are on the title screen or in a room transition), set all lenses to N/A,
                 * uncheck all checkboxes & set grenades to 0
                 */
                foreach (KeyValuePair<string, BoktaiLens> lens in _boktaiGuns.Lenses) {
                    RadioButton btn = lensesButtons.Where(X => X.Name == lens.Key + "_none").FirstOrDefault();
                    if (btn != null) {
                        btn.Checked = true;
                    }
                }

                UncheckAll(framesCheckboxes);
                UncheckAll(batteriesCheckboxes);

                pineappleCharges.Value = 0;
                foreach (NumericUpDown field in grenadesNumericUpDowns) {
                    if (field.Enabled == true) {
                        field.Value = 0;
                    }
                }
            }
        }

        /// <summary>Check ImageCheckBoxes in a list based on the corresponding bitmask's value</summary>
        /// <param name="list">List of ImageCheckBox instances</param>
        /// <param name="bitmaskValue">Bitmask value</param>
        private void CheckBasedOnBitmaskValue(List<ImageCheckBox> list, int bitmaskValue) {
            int bitPosition = 0;
            foreach (ImageCheckBox checkBox in list) {
                if (checkBox.Enabled == false) {
                    continue;
                }

                checkBox.Checked = Utilities.IsBitOne(bitmaskValue, bitPosition);
                bitPosition++;
            }
        }

        /// <summary>Uncheck all ImageCheckBoxes in a list</summary>
        /// <param name="list">List of ImageCheckBox instances</param>
        private void UncheckAll(List<ImageCheckBox> list) {
            foreach (ImageCheckBox checkBox in list) {
                if (checkBox.Enabled == false) {
                    continue;
                }

                checkBox.Checked = false;
            }
        }

        #endregion
    }
}

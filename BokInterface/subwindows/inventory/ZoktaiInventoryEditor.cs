using System;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Inventory {
    /// <summary>Inventory editor for Boktai 2</summary>
    class ZoktaiInventoryEditor : InventoryEditor {

        #region Instances
        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly ZoktaiAddresses _zoktaiAddresses;

        #endregion

        public ZoktaiInventoryEditor(BokInterface bokInterface, MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {

            _memoryValues = memoryValues;
            _zoktaiAddresses = zoktaiAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(559, 279);

            // Add the onClose event to the subwindow
            FormClosing += new FormClosingEventHandler(delegate (object sender, FormClosingEventArgs e) {
                _bokInterface.inventoryEditorOpened = false;
            });

            // Add elements & show the subwindow
            AddElements();
            Show();
        }

        protected override void AddElements() {

            inventoryGroupbox = WinFormHelpers.CreateGroupBox("inventoryGroup", "Items", 5, 5, 549, 244, this);

            // 1st row
            WinFormHelpers.CreateDowndownList("slot1_item", 5, 16, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot1", "Rotten state", 5, 47, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot1_rotten_state", 0, 85, 44, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot2_item", 141, 16, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot2", "Rotten state", 141, 47, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot2_rotten_state", 0, 221, 44, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot3_item", 277, 16, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot3", "Rotten state", 277, 47, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot3_rotten_state", 0, 357, 44, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot4_item", 413, 16, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot4", "Rotten state", 413, 47, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot4_rotten_state", 0, 493, 44, 50, 23, 0, 3839, control: inventoryGroupbox);

            // 2nd row
            WinFormHelpers.CreateDowndownList("slot5_item", 5, 73, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot5", "Rotten state", 5, 104, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot5_rotten_state", 0, 85, 101, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot6_item", 141, 73, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot6", "Rotten state", 141, 104, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot6_rotten_state", 0, 221, 101, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot7_item", 277, 73, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot7", "Rotten state", 277, 104, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot7_rotten_state", 0, 357, 101, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot8_item", 413, 73, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot8", "Rotten state", 413, 104, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot8_rotten_state", 0, 493, 101, 50, 23, 0, 3839, control: inventoryGroupbox);

            // 3rd row
            WinFormHelpers.CreateDowndownList("slot9_item", 5, 130, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot9", "Rotten state", 5, 161, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot9_rotten_state", 0, 85, 158, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot10_item", 141, 130, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot10", "Rotten state", 141, 161, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot10_rotten_state", 0, 221, 158, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot11_item", 277, 130, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot11", "Rotten state", 277, 161, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot11_rotten_state", 0, 357, 158, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot12_item", 413, 130, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot12", "Rotten state", 413, 161, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot12_rotten_state", 0, 493, 158, 50, 23, 0, 3839, control: inventoryGroupbox);

            // 4th row
            WinFormHelpers.CreateDowndownList("slot13_item", 5, 187, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot13", "Rotten state", 5, 218, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot13_rotten_state", 0, 85, 215, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot14_item", 141, 187, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot14", "Rotten state", 141, 218, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot14_rotten_state", 0, 221, 215, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot15_item", 277, 187, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot15", "Rotten state", 277, 218, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot15_rotten_state", 0, 357, 215, 50, 23, 0, 3839, control: inventoryGroupbox);

            WinFormHelpers.CreateDowndownList("slot16_item", 413, 187, 130, 23, inventoryGroupbox);
            WinFormHelpers.CreateLabel("slot16", "Rotten state", 413, 218, 74, 15, inventoryGroupbox);
            WinFormHelpers.CreateNumericUpDown("slot16_rotten_state", 0, 493, 215, 50, 23, 0, 3839, control: inventoryGroupbox);

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 479, 252, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        protected override void SetValues() { }
    }
}
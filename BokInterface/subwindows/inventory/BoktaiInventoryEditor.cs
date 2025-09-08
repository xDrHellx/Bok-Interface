using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.Items;
using BokInterface.Utils;

namespace BokInterface.Inventory {
    /// <summary>Inventory editor for Boktai</summary>
    class BoktaiInventoryEditor : InventoryEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly BoktaiAddresses _boktaiAddresses;
        private readonly BoktaiItems _boktaiItems;
        protected TabControl inventoryTabControl = new();

        #endregion

        #region Constructor

        public BoktaiInventoryEditor(BokInterface bokInterface, MemoryValues memoryValues, BoktaiAddresses BoktaiAddresses) {

            _memoryValues = memoryValues;
            _boktaiAddresses = BoktaiAddresses;
            _boktaiItems = new();

            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(394, 408);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() {

            // Add tabs & fields
            inventoryTabControl = WinFormHelpers.CreateTabControl("inventory_ctrl", 5, 5, 387, 375, this);
            AddItems();
            AddKeyItems();

            // Set default values for each field
            SetDefaultValues();

            // Button for setting values & its events
            Button setValuesButton = WinFormHelpers.CreateButton("setStatusButton", "Set values", 315, 382, 75, 23, this);
            setValuesButton.Click += new EventHandler(delegate (object sender, EventArgs e) {
                // Write the values for 10 frames
                for (int i = 0; i < 10; i++) {
                    SetValues();
                }
            });
        }

        private void AddItems() {

            TabPage itemsTab = WinFormHelpers.CreateTabPage("items_tab", "Items", tabControl: inventoryTabControl);
            itemsTab.AutoScroll = true;

            // Add fields for all items
            int posX = 5,
                posY = 5,
                n = 0;
            foreach (KeyValuePair<string, BoktaiItem> entry in _boktaiItems.Items) {

                BoktaiItem item = entry.Value;
                if (item.icon == null) {
                    continue;
                }

                // Add fields for that item
                CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox(entry.Key + "_grp", item.name, posX, posY, 110, 64, control: itemsTab);
                WinFormHelpers.CreatePictureBox(entry.Key + "_img", item.icon, 8, 24, group);
                WinFormHelpers.CreateLabel(entry.Key + "_label", "Amount", 54, 17, 55, 18, group);

                NumericUpDown amountField = WinFormHelpers.CreateNumericUpDown(entry.Key + "_amount", 0, 61, 35, 44, 18, control: group);
                amountField.Tag = item.value;
                numericUpDowns.Add(amountField);

                n++;
                posX += 115;

                if ((n % 3) == 0) {
                    posX = 5;
                    posY += 69;
                }
            }
        }

        private void AddKeyItems() {

            TabPage keyItemsTab = WinFormHelpers.CreateTabPage("key_items_tab", "Key items", tabControl: inventoryTabControl);
            keyItemsTab.AutoScroll = true;

            int posX = 5,
                posY = 5,
                n = 0;
            foreach (KeyValuePair<string, BoktaiItem> entry in _boktaiItems.KeyItems) {

                BoktaiItem keyItem = entry.Value;
                if (keyItem.icon == null) {
                    continue;
                }

                CheckGroupBox group = WinFormHelpers.CreateCheckGroupBox(entry.Key + "_grp", keyItem.name, posX, posY, 120, 64, control: keyItemsTab);
                WinFormHelpers.CreatePictureBox(entry.Key + "_img", keyItem.icon, 8, 24, group);
                WinFormHelpers.CreateLabel(entry.Key + "_label", "Amount", 64, 17, 55, 18, group);

                NumericUpDown amountField = WinFormHelpers.CreateNumericUpDown(entry.Key + "_amount", 0, 71, 35, 44, 18, control: group);
                amountField.Tag = keyItem.value;

                // Restrict the maximum amount that can be set for everything except Keys & Silver Coins
                if (keyItem.value < 30 || keyItem.value > 34) {
                    amountField.Maximum = 1;
                }

                numericUpDowns.Add(amountField);

                n++;
                posX += 125;

                if ((n % 3) == 0) {
                    posX = 5;
                    posY += 69;
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

            // Loop over each field
            foreach (NumericUpDown field in numericUpDowns) {

                // Retrieve the current inventory
                Dictionary<string, BoktaiItem> inventory = GetCurrentInventory();

                /**
                 * Check if the field is disabled or if the group is unchecked
                 * Checking for the group is necessary, as elements that haven't been "seen" yet will be considered Enabled for some reason (thanks WinForm ?)
                 */
                CheckGroupBox group = (CheckGroupBox)field.Parent;
                if (group.Checked == false || field.Enabled == false) {
                    continue;
                }

                // If we're removing the item from the inventory
                uint newAmount = (uint)field.Value;
                if (newAmount == 0) {

                    // Get a reference to the item from the current inventory
                    BoktaiItem item = inventory.FirstOrDefault(x => x.Value.value.ToString() == field.Tag.ToString()).Value;
                    if (item == null) {
                        continue;
                    }

                    // Loop over the inventory, starting from the item to remove
                    uint inventorySize = _boktaiAddresses.Inventory["items_order"].Value;
                    for (int i = item.slot + 1; i < inventorySize - 1; i++) {

                        // Set pointers
                        int bitPosition = i * 2,
                            nextBitPosition = (i + 1) * 2;

                        // Shift the next item into the current slot
                        APIs.Memory.WriteU16(_boktaiAddresses.Inventory["items_order"].Address + bitPosition, APIs.Memory.ReadU16(_boktaiAddresses.Inventory["items_order"].Address + nextBitPosition));
                        APIs.Memory.WriteU16(_boktaiAddresses.Inventory["items_ids"].Address + bitPosition, APIs.Memory.ReadU16(_boktaiAddresses.Inventory["items_ids"].Address + nextBitPosition));
                        APIs.Memory.WriteU16(_boktaiAddresses.Inventory["items_amounts"].Address + bitPosition, APIs.Memory.ReadU16(_boktaiAddresses.Inventory["items_amounts"].Address + nextBitPosition));
                    }

                    // Resize the inventory
                    _boktaiAddresses.Inventory["items_order"].Value = inventorySize - 1;
                } else {

                    /**
                     * If not removing, check if the item is already present in the inventory
                     * If yes, only update the amount
                     */
                    BoktaiItem item = inventory.FirstOrDefault(x => x.Value.value.ToString() == field.Tag.ToString()).Value;
                    if (item != null) {
                        APIs.Memory.WriteU16(_boktaiAddresses.Inventory["items_amounts"].Address + (item.slot * 2), newAmount);
                    } else {

                        // If the item isn't in the inventory yet, get the item corresponding to the field & add it at the end of the inventory
                        BoktaiItem correspondingItem = _boktaiItems.All.FirstOrDefault(x => x.Value.value.ToString() == field.Tag.ToString()).Value;
                        if (correspondingItem == null) {
                            continue;
                        }

                        // Get inventory size & prepare the pointer
                        uint inventorySize = _boktaiAddresses.Inventory["items_order"].Value;
                        uint bitPosition = (inventorySize + 1) * 2;

                        // Add an extra slot to the current inventory & set values for the item on that slot
                        _boktaiAddresses.Inventory["items_order"].Value = inventorySize + 1;
                        APIs.Memory.WriteU16(_boktaiAddresses.Inventory["items_order"].Address + bitPosition, correspondingItem.value);
                        APIs.Memory.WriteU16(_boktaiAddresses.Inventory["items_ids"].Address + bitPosition, correspondingItem.value);
                        APIs.Memory.WriteU16(_boktaiAddresses.Inventory["items_amounts"].Address + bitPosition, (uint)field.Value);
                    }
                }
            }

            /**
             * If BizHawk was paused before setting values, unpause it
             * Otherwise keep it paused
             */
            if (_bokInterface._previousIsPauseSetting == false) {
                APIs.Client.Unpause();
            }
        }

        protected override void SetDefaultValues() {

            uint statStructurePointer = _boktaiAddresses.Misc["stat"].Value;
            if (statStructurePointer > 0) {

                // Get the current inventory, stop if it's empty
                Dictionary<string, BoktaiItem> inventory = GetCurrentInventory();
                if (inventory.Count == 0) {
                    return;
                }

                // Otherwise loop over each item
                foreach (KeyValuePair<string, BoktaiItem> entry in inventory) {
                    BoktaiItem item = entry.Value;
                    if (item == null) {
                        continue;
                    }

                    // Get the field corresponding to the item
                    NumericUpDown amountField = numericUpDowns.FirstOrDefault(x => (uint)x.Tag == item.value);
                    if (amountField == null || amountField.Enabled == false) {
                        continue;
                    }

                    // Update the field's value
                    amountField.Value = item.amount;
                }
            } else {
                // If current stat is unvalid (for example because we are on the title screen or in a room transition), set all item amounts to 0
                foreach (NumericUpDown field in numericUpDowns) {
                    if (field.Enabled == true) {
                        field.Value = 0;
                    }
                }
            }
        }

        /// <summary>Get the current inventory and store its data in a list</summary>
        /// <returns><c>Dictionary</c>Dictionnary of BoktaiItem instances</returns>
        private Dictionary<string, BoktaiItem> GetCurrentInventory() {

            Dictionary<string, BoktaiItem> inventory = [];

            // Get the size of the current inventory
            uint size = _boktaiAddresses.Inventory["items_order"].Value;
            if (size == 0) {
                return inventory;
            }

            // Loop over the inventory to retrieve items within
            for (int i = 0; i < size; i++) {
                int bitPosition = i * 2;

                // Check if the stored item ID for that slot is valid & if the item isn't in the list yet
                uint itemId = APIs.Memory.ReadU16(_boktaiAddresses.Inventory["items_ids"].Address + bitPosition);
                BoktaiItem? item = GetItemByValue(itemId);
                if (item == null || inventory.ContainsKey(item.name) == true) {
                    continue;
                }

                // Set the amount & position (order) of the item by retrieving them from the inventory
                item.amount = (int)APIs.Memory.ReadU16(_boktaiAddresses.Inventory["items_amounts"].Address + bitPosition);
                item.slot = i;

                // Store the instance containing all the item's info based on the inventory
                inventory.Add(item.name, item);
            }

            return inventory;
        }

        ///<summary>Get an item from the items list by using its value</summary>
        ///<param name="value"><c>decimal</c>Value</param>
        ///<returns><c>Item</c>Item</returns>
        private BoktaiItem? GetItemByValue(decimal value) {
            foreach (KeyValuePair<string, BoktaiItem> index in _boktaiItems.All) {
                BoktaiItem item = index.Value;
                if (item.value == value) {
                    return item;
                }
            }

            return null;
        }

        #endregion
    }
}

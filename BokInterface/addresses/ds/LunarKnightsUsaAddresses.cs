namespace BokInterface.Addresses {
    /// <summary>Main class for Lunar Knights (US version) memory addresses</summary>
    public class LunarKnightsUsaAddresses : DsAddresses {

        public LunarKnightsUsaAddresses() {
            InitPlayerAddresses();
            InitInventoryAddresses();
        }

        private void InitPlayerAddresses() {

            // Current stats
            note = "Used for damage calculations, will be copied to its Persistent equivalent on screen transition. Must be combined with the \"stat\" memory address' value";
            Player.Add("lucian_current_hp", new MemoryAddress(0x313C94, note, domain: "Main RAM"));
        }

        private void InitInventoryAddresses() {
            for (int i = 0; i < 20; i++) {
                int slotNumber = 1 + i;

                // Items (2 bytes each)
                uint addressOffset = 0x2 * (uint)i;
                Inventory.Add("item_slot_" + slotNumber, new MemoryAddress(0x1EC058 + addressOffset, note: "Item slot", domain: "Main RAM"));
                Inventory.Add("key_item_slot_" + slotNumber, new MemoryAddress(0x1EC328 + addressOffset, note: "Key item slot", domain: "Main RAM"));

                // Only 16 slots for accessories & 4 slots for shields
                if (i < 16) {
                    Inventory.Add("accessory_slot_" + slotNumber, new MemoryAddress(0x1EC650 + addressOffset, note: "Accessory slot", domain: "Main RAM"));
                    if (i < 4) {
                        Inventory.Add("shield_slot_" + slotNumber, new MemoryAddress(0x1EC670 + addressOffset, note: "Shield slot", domain: "Main RAM"));
                    }
                }
            }
        }
    }
}

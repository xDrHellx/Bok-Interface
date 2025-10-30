namespace BokInterface.Addresses {
    /// <summary>Main class for Lunar Knights (EU version) memory addresses</summary>
    public class LunarKnightsEuropeanAddresses : DsAddresses {

        public LunarKnightsEuropeanAddresses() {
            InitPlayerAddresses();
            InitInventoryAddresses();
        }

        private void InitPlayerAddresses() {

            // Current stats
            note = "Used for damage calculations, will be copied to its Persistent equivalent on screen transition. Must be combined with the \"stat\" memory address' value";
            Player.Add("lucian_current_hp", new MemoryAddress(0x313E74, note, domain: "Main RAM"));
        }

        private void InitInventoryAddresses() {
            for (int i = 0; i < 20; i++) {
                int slotNumber = 1 + i;

                // Items (2 bytes each)
                uint addressOffset = 0x2 * (uint)i;
                Inventory.Add("item_slot_" + slotNumber, new MemoryAddress(0x1EC238 + addressOffset, note: "Item slot", domain: "Main RAM"));
                Inventory.Add("item_slot_durability_" + slotNumber, new MemoryAddress(0x1EC3A0 + addressOffset, note: "Durability for item in slot", domain: "Main RAM"));
                Inventory.Add("key_item_slot_" + slotNumber, new MemoryAddress(0x1EC508 + addressOffset, note: "Key item slot", domain: "Main RAM"));

                // Only 16 slots for accessories & 4 slots for shields
                if (i < 16) {
                    Inventory.Add("accessory_slot_" + slotNumber, new MemoryAddress(0x1EC830 + addressOffset, note: "Accessory slot", domain: "Main RAM"));
                    if (i < 4) {
                        Inventory.Add("shield_slot_" + slotNumber, new MemoryAddress(0x1EC850 + addressOffset, note: "Shield slot", domain: "Main RAM"));
                    }
                }
            }
        }
    }
}

using System.Collections.Generic;

using BokInterface.JunkParts;

namespace BokInterface.Addresses {
    /// <summary>Main class for Lunar Knights (US version) memory addresses</summary>
    public class LunarKnightsUsaAddresses : DsAddresses {

        public LunarKnightsUsaAddresses() {
            InitPlayerAddresses();
            InitInventoryAddresses();
            OrderDictionnaries();
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
                Inventory.Add("item_slot_" + slotNumber, new MemoryAddress(0x1EC058 + addressOffset, "Item slot", domain: "Main RAM"));
                Inventory.Add("item_slot_durability_" + slotNumber, new MemoryAddress(0x1EC1C0 + addressOffset, "Durability for item in slot", domain: "Main RAM"));
                Inventory.Add("key_item_slot_" + slotNumber, new MemoryAddress(0x1EC328 + addressOffset, "Key item slot", domain: "Main RAM"));

                // Only 16 slots for accessories & 4 slots for shields
                if (i < 16) {
                    Inventory.Add("accessory_slot_" + slotNumber, new MemoryAddress(0x1EC650 + addressOffset, "Accessory slot", domain: "Main RAM"));
                    if (i < 4) {
                        Inventory.Add("shield_slot_" + slotNumber, new MemoryAddress(0x1EC670 + addressOffset, "Shield slot", domain: "Main RAM"));
                    }
                }
            }

            // Junk parts
            uint n = 0;
            foreach (KeyValuePair<string, DsJunkPart> part in junkParts.All) {
                uint addressOffset = 0x2 * n;
                Inventory.Add($"amount_{part.Key}", new MemoryAddress(0x1EC790 + addressOffset, "Junk part amount", domain: "Main RAM"));
                n++;
            }

            Inventory.Add("unlocked_junk_parts", new MemoryAddress(0x1EC7D4, "bitmask", domain: "Main RAM"));
        }
    }
}

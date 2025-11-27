using System.Collections.Generic;

using BokInterface.JunkParts;

namespace BokInterface.Addresses {
    /// <summary>Main class for Boktai DS memory addresses</summary>
    public class BoktaiDsAddresses : DsAddresses {

        public BoktaiDsAddresses() {
            InitPlayerAddresses();
            InitInventoryAddresses();
            OrderDictionnaries();
        }

        private void InitPlayerAddresses() {

            // TODO Need find proper addresses, with pointer to data (currently player values cannot be updated)
            // Current stats
            note = "Used for damage calculations, will be copied to its Persistent equivalent on screen transition. Must be combined with the \"stat\" memory address' value";
            Player.Add("lucian_current_hp", new MemoryAddress(0x2211F6, note, domain: "Main RAM"));
            Player.Add("aaron_current_hp", new MemoryAddress(0x2211F4, note, domain: "Main RAM"));

            // Persistent stats (used on screen transitions & save data)
            // note = "Also corresponds to values from Save Data";
        }

        private void InitInventoryAddresses() {
            for (int i = 0; i < 20; i++) {
                int slotNumber = 1 + i;

                // Items, Key items, accessories & shield (2 bytes each)
                uint addressOffset = 0x2 * (uint)i;
                Inventory.Add("item_slot_" + slotNumber, new MemoryAddress(0x221278 + addressOffset, "Item slot", domain: "Main RAM"));
                Inventory.Add("item_slot_durability_" + slotNumber, new MemoryAddress(0x2213E0 + addressOffset, "Durability for item in slot", domain: "Main RAM"));
                Inventory.Add("key_item_slot_" + slotNumber, new MemoryAddress(0x221548 + addressOffset, "Key item slot", domain: "Main RAM"));

                // Only 16 slots for accessories & 4 slots for shields
                if (i < 16) {
                    Inventory.Add("accessory_slot_" + slotNumber, new MemoryAddress(0x221870 + addressOffset, "Accessory slot", domain: "Main RAM"));
                    if (i < 4) {
                        Inventory.Add("shield_slot_" + slotNumber, new MemoryAddress(0x221890 + addressOffset, "Shield slot", domain: "Main RAM"));
                    }
                }
            }

            // Junk parts
            uint n = 0;
            foreach (KeyValuePair<string, DsJunkPart> part in junkParts.All) {
                uint addressOffset = 0x2 * n;
                Inventory.Add($"amount_{part.Key.ToLower()}", new MemoryAddress(0x2219B0 + addressOffset, "Junk part amount", domain: "Main RAM"));
                n++;
            }

            Inventory.Add("unlocked_junk_parts", new MemoryAddress(0x2219F4, "bitmask", domain: "Main RAM"));
        }
    }
}

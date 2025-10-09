namespace BokInterface.Addresses {
    /// <summary>Main class for Lunar Knights (EU version) memory addresses</summary>
    public class LunarKnightsEuropeanAddresses : DsAddresses {

        public LunarKnightsEuropeanAddresses() {
            InitInventoryAddresses();
        }

        private void InitInventoryAddresses() {
            for (int i = 0; i < 20; i++) {
                int slotNumber = 1 + i;

                // Items (2 bytes each)
                uint addressOffset = 0x2 * (uint)i;
                Inventory.Add("item_slot_" + slotNumber, new MemoryAddress(0x1EC238 + addressOffset, note: "Item slot", domain: "Main RAM"));
            }
        }
    }
}

namespace BokInterface.All {

    /// <summary>Main class for utilities</summary>
    public class Utilities {

        public Utilities() {

        }

        /// <summary>Retrieve the code for the current game running on BizHawk</summary>
        /// <returns><c>uint</c>Game code</returns>
        public uint GetGameCode() {
            return APIs.Memory.ReadU32(0x080000AC) & 0xFFFFFF;
        }
    }
}
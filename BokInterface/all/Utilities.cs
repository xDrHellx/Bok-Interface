using System;

namespace BokInterface.All {

    /// <summary>Main class for utilities</summary>
    public class Utilities {

        public Utilities() {

        }

        #region Game code & region methods

        /// <summary>Retrieve the code for the current GBA game running on BizHawk</summary>
        /// <returns><c>uint</c>Game code</returns>
        public static uint GetGbaGameCode() {
            return APIs.Memory.ReadU32(0x080000AC);
        }

        /// <summary>Retrieve the code for the current DS game running on BizHawk</summary>
        /// <returns><c>uint</c>Game code</returns>
        public static uint GetDsGameCode() {
            return APIs.Memory.ReadU32(0x3FFE0C, "Main RAM");
        }

        /// <summary>Get the value for the game's version</summary>
        /// <returns><c>uint</c>Indicator (for example 0 for v1.0, 1 for v1.1, ...)</returns>
        public static uint GetGameVersion() {
            return APIs.Memory.ReadU8(0x080000bc, "Main RAM");
        }

        #endregion

        #region Simplified memory addresses methods

        /// <summary>Shortcut method for retrieving the value of a dynamic memory address</summary>
        /// <param name="firstAddress">First address to read (U32)</param>
        /// <param name="secondAddress">Second address to read (U16)</param>
        /// <param name="type">Type of method to use for reading the result of both addresses (by default "U16" because it is the most common one)</param>
        /// <returns><c>uint</c>Value</returns>
        public static uint ReadDynamicAddress(uint firstAddress, uint secondAddress, string type = "U16") {

            // We convert the type to lowercase to make the switch simpler
            string lowerType = type.ToLower();
            return lowerType switch {
                "u8" => APIs.Memory.ReadU8(APIs.Memory.ReadU32(firstAddress) + secondAddress),
                "u24" => APIs.Memory.ReadU24(APIs.Memory.ReadU32(firstAddress) + secondAddress),
                "u32" => APIs.Memory.ReadU32(APIs.Memory.ReadU32(firstAddress) + secondAddress),
                _ => APIs.Memory.ReadU16(APIs.Memory.ReadU32(firstAddress) + secondAddress),
            };
        }

        /// <summary>Shortcut method for retrieving the value of a dynamic memory address</summary>
        /// <param name="value">Value to set</param>
        /// <param name="firstAddress">First address (U32)</param>
        /// <param name="secondAddress">Second address (U16)</param>
        /// <param name="type">Type of method to use for writing to the result of both addresses (by default "U16" because it is the most common one)</param>
        public static void WriteDynamicAddress(uint value, uint firstAddress, uint secondAddress, string type = "U16") {

            // We convert the type to lowercase to make the switch simpler
            string lowerType = type.ToLower();
            switch (lowerType) {
                case "u8":
                    APIs.Memory.WriteU8(APIs.Memory.ReadU32(firstAddress) + secondAddress, value);
                    break;
                case "u24":
                    APIs.Memory.WriteU24(APIs.Memory.ReadU32(firstAddress) + secondAddress, value);
                    break;
                case "u32":
                    APIs.Memory.WriteU32(APIs.Memory.ReadU32(firstAddress) + secondAddress, value);
                    break;
                default:
                    APIs.Memory.WriteU16(APIs.Memory.ReadU32(firstAddress) + secondAddress, value);
                    break;
            };
        }

        /// <summary>Convert an hexadecimal value to an integer</summary>
        /// <param name="value">Hexadecimal value</param>
        /// <returns><c>int</c>Decimal</returns>
        public static int HexToInt(string value) {
            return int.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>Convert an integer to a hexadecimal value</summary>
        /// <param name="value">Integer</param>
        /// <returns><c>string</c>Hexadecimal</returns>
        public static string IntToHex(uint value) {
            return value.ToString("X");
        }

        #endregion

        #region EXP & level conversions methods

        /// <summary>Convert EXP to Level</summary>
        /// <param name="exp">EXP amount</param>
        /// <returns><c>decimal</c>Level</returns>
        public static decimal ExpToLevel(uint exp) {
            return exp > 0 ? Convert.ToDecimal(exp) / 100 : 0;
        }

        /// <summary>Convert Level to EXP</summary>
        /// <param name="level">Level</param>
        /// <returns><c>uint</c>EXP</returns>
        public static uint LevelToExp(decimal level) {
            return level > 0 ? (uint)(level * 100) : 0;
        }

        #endregion
    }
}

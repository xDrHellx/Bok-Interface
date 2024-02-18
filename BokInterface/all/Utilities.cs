using System;

namespace BokInterface.All
{

    /// <summary>Main class for utilities</summary>
    public class Utilities
    {

        public Utilities()
        {

        }

        /// <summary>Retrieve the code for the current GBA game running on BizHawk</summary>
        /// <returns><c>uint</c>Game code</returns>
        public static uint GetGbaGameCode()
        {
            return APIs.Memory.ReadU32(0x080000AC);
        }

        /// <summary>Retrieve the code for the current DS game running on BizHawk</summary>
        /// <returns><c>uint</c>Game code</returns>
        public static uint GetDsGameCode()
        {
            return APIs.Memory.ReadU32(0x3FFE0C, "Main RAM");
        }

        /// <summary>Shortcut method for retrieving the value of a dynamic memory address</summary>
        /// <param name="firstAddress">First address to read (U32)</param>
        /// <param name="secondAddress">Second address to read (U16)</param>
        /// <returns><c>uint</c>Value</returns>
        public static uint ReadDynamicAddress(uint firstAddress, uint secondAddress)
        {
            return APIs.Memory.ReadU16(APIs.Memory.ReadU32(firstAddress) + secondAddress);
        }

        /// <summary>Shortcut method for retrieving the value of a dynamic memory address</summary>
        /// <param name="value">Value to set</param>
        /// <param name="firstAddress">First address (U32)</param>
        /// <param name="secondAddress">Second address (U16)</param>
        public static void WriteDynamicAddress(uint value, uint firstAddress, uint secondAddress)
        {
            APIs.Memory.WriteU16(APIs.Memory.ReadU32(firstAddress) + secondAddress, value);
        }

        /// <summary>Convert an hexadecimal value to an integer</summary>
        /// <param name="value">Hexadecimal value</param>
        /// <returns><c>int</c>Decimal</returns>
        public static int HexToInt(string value)
        {
            return Int32.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>Convert an integer to a hexadecimal value</summary>
        /// <param name="value">Integer</param>
        /// <returns><c>string</c>Hexadecimal</returns>
        public static string IntToHex(uint value)
        {
            return value.ToString("X");
        }
    }
}
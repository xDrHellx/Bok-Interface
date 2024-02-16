using System.Drawing;
using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Tools.TextToWorldSpace {
    /// <summary>
    /// <para>Allows printing text and data to world space</para>
    /// <para>Made by Raphi, converted from Lua to C# by Doc</para>
    /// </summary>
    class TextToWorldSpace {

        /**
         * TODO
         * 
         * Issue with the coordinates, they don't follow Django, all the way when the camera stops moving
         */

        #region Main properties

        protected uint planeScale = 0x30/0x100;
        protected uint heightScale = 0x18/0x100;
        protected Color textColor = new();

        #endregion

        #region Memory addresses properties

        private readonly BoktaiAddresses boktaiAddresses = new();
        private readonly ZoktaiAddresses zoktaiAddresses = new();
        private readonly ShinbokAddresses shinbokAddresses = new();
        private readonly LunarKnightsAddresses lunarKnightsAddresses = new();
        private uint cameraXposAddress = 0;
        private uint cameraYposAddress = 0;
        private uint cameraZposAddress = 0;

        #endregion
        
        public TextToWorldSpace(string gameName, string text, uint x, uint y, uint z = 0, Color? textColor = null) {
            if(text == "") {
                return;
            }

            // Set memory addresses used for getting the camera coordinates
            this.SetCameraAddresses(gameName);

            // Set text color & write to coordinates
            this.textColor = textColor == null ? Color.LimeGreen : (Color)textColor;
            this.WriteTextToCoordinates(text, x, y, z);
        }

        /// <summary>Set camera memory addresses used for writing position</summary>
        /// <param name="gameName">Current game name</param>
        /// <returns><c>uint, uint, uint</c>Camera memory addresses (X, Y, Z)</returns>
        private void SetCameraAddresses(string gameName) {
            switch(gameName) {
                case "Boktai":
                    this.cameraXposAddress = boktaiAddresses.Misc["x_camera"];
                    this.cameraYposAddress = boktaiAddresses.Misc["y_camera"];
                    this.cameraZposAddress = boktaiAddresses.Misc["z_camera"];
                    break;
                case "Zoktai":
                    this.cameraXposAddress = zoktaiAddresses.Misc["x_camera"];
                    this.cameraYposAddress = zoktaiAddresses.Misc["y_camera"];
                    this.cameraZposAddress = zoktaiAddresses.Misc["z_camera"];
                    break;
                case "Shinbok":
                    this.cameraXposAddress = shinbokAddresses.Misc["x_camera"];
                    this.cameraYposAddress = shinbokAddresses.Misc["y_camera"];
                    this.cameraZposAddress = shinbokAddresses.Misc["z_camera"];
                    break;
                case "LunarKnights":
                    // Current not handled, not enough data available
                    this.cameraXposAddress = this.cameraYposAddress = this.cameraZposAddress = 0;
                    break;
                default:
                    this.cameraXposAddress = this.cameraYposAddress = this.cameraZposAddress = 0;
                    break;
            }
        }

        /// <summary>Write text to coordinates</summary>
        /// <param name="text">Text to write</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        protected void WriteTextToCoordinates(string text, uint x, uint y, uint z) {

            // Convert world to screen coordinates
            var screenCoordinates = this.WorldToScreen(x, y, z);
            int posX = screenCoordinates.Item1;
            int posY = screenCoordinates.Item2;
            
            // Print text on screen at screen coordinates
            APIs.Gui.Text(posX, posY, text, this.textColor);
        }

        /// <summary>Convert world coordinates to view coordinates</summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <returns><c>uint, uint, uint</c>View coordinates(X, Y, Z)</returns>
        protected (uint, uint, uint) WorldToView(uint x, uint y, uint z) {

            uint scaledWorldY = y * heightScale;
            uint worldX = x/2;
            uint worldZ = z/2;

            uint worldZ2 = (worldX + worldZ) * planeScale;

            uint viewX = (worldX - worldZ) * planeScale;
            uint viewY = worldZ2 - scaledWorldY;
            uint viewZ = worldZ2 + scaledWorldY;

            return (viewX, viewY, viewZ);
        }

        /// <summary>Converts view coordinates to screen point</summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <returns><c>int, int</c>Emulator screen points</returns>
        protected (int, int) ViewToScreen(uint x, uint y, uint z = 0) {

            // Get camera coordinates
            uint camX = (uint)APIs.Memory.ReadS16(this.cameraXposAddress);
            uint camY = (uint)APIs.Memory.ReadS16(this.cameraYposAddress);
            // uint camZ = (uint)APIs.Memory.ReadS16(this.cameraZposAddress); // Useless for now
            
            // Adjusts position
            x = x - camX + 0x78;
            y = y - camY + 0x50;
            // z = z - camZ; // Useless for now

            // Transforms GBA screen point to Emulator screen point
            APIs.Gui.AddMessage(((int)x).ToString() + ((int)y).ToString());
            Point screenPoint = APIs.Client.TransformPoint(new Point((int)x, (int)y));
            return (screenPoint.X, screenPoint.Y);
        }

        /// <summary>Returns world coordinates converted to screen coordinates</summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <returns><c>int, int</c>Screen coordinates (X, Y)</returns>
        protected (int, int) WorldToScreen(uint x, uint y, uint z) {
            var viewCoordinates = this.WorldToView(x, y, z);
            return this.ViewToScreen(viewCoordinates.Item1, viewCoordinates.Item2, viewCoordinates.Item3);
        }
    }
}
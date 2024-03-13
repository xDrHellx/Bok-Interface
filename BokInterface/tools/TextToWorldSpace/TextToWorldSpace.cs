using System.Drawing;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Tools.TextToWorldSpace {
    /// <summary>
    /// <para>Allows printing text and data to world space</para>
    /// <para>Made by Raphi, Doc & Shenef</para>
    /// </summary>
    class TextToWorldSpace {

        #region Main properties

        protected double planeScale = (double)0x30 / 0x100;
        protected double heightScale = (double)0x18 / 0x100;
        protected Color textColor = new();

        #endregion

        #region Memory addresses properties

        private readonly BoktaiAddresses _boktaiAddresses = new();
        private readonly ZoktaiAddresses _zoktaiAddresses = new();
        private readonly ShinbokAddresses _shinbokAddresses = new();
        private uint _cameraXposAddress = 0;
        private uint _cameraYposAddress = 0;

        #endregion

        public TextToWorldSpace(string text, double x, double y, double z, Color? textColor = null) {
            if (text == "") {
                return;
            }

            // Set memory addresses used for getting the camera coordinates
            SetCameraAddresses(BokInterface.shorterGameName);

            // Set text color & write to coordinates
            this.textColor = textColor == null ? Color.LimeGreen : (Color)textColor;
            WriteTextToCoordinates(text, x, y, z);
        }

        /// <summary>Set camera memory addresses used for writing position</summary>
        /// <param name="gameName">Current game name</param>
        /// <returns><c>uint, uint, uint</c>Camera memory addresses (X, Y, Z)</returns>
        private void SetCameraAddresses(string gameName) {
            switch (gameName) {
                case "Boktai":
                    _cameraXposAddress = _boktaiAddresses.Misc["x_camera"];
                    _cameraYposAddress = _boktaiAddresses.Misc["y_camera"];
                    break;
                case "Zoktai":
                    _cameraXposAddress = _zoktaiAddresses.Misc["x_camera"];
                    _cameraYposAddress = _zoktaiAddresses.Misc["y_camera"];
                    break;
                case "Shinbok":
                    _cameraXposAddress = _shinbokAddresses.Misc["x_camera"];
                    _cameraYposAddress = _shinbokAddresses.Misc["y_camera"];
                    break;
                case "LunarKnights":
                    // Current not handled, not enough data available
                    _cameraXposAddress = _cameraYposAddress = 0;
                    break;
                default:
                    _cameraXposAddress = _cameraYposAddress = 0;
                    break;
            }
        }

        /// <summary>Write text to coordinates</summary>
        /// <param name="text">Text to write</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        protected void WriteTextToCoordinates(string text, double x, double y, double z) {

            // Convert world to screen coordinates
            (double, double) screenCoordinates = WorldToScreen(x, y, z);
            double posX = screenCoordinates.Item1;
            double posY = screenCoordinates.Item2;

            // Print text on screen at screen coordinates
            APIs.Gui.Text((int)posX, (int)posY, text, textColor);
        }

        /// <summary>Convert world coordinates to view coordinates</summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <returns><c>double, double, double</c>View coordinates(X, Y, Z)</returns>
        protected (double, double, double) WorldToView(double x, double y, double z) {

            double scaledWorldZ = z * heightScale;
            double worldX = x / 2;
            double worldY = y / 2;

            double worldY2 = (worldX + worldY) * planeScale;

            double viewX = (worldX - worldY) * planeScale;
            double viewY = worldY2 - scaledWorldZ;
            double viewZ = worldY2 + scaledWorldZ;

            return (viewX, viewY, viewZ);
        }

        /// <summary>Converts view coordinates to screen point</summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <returns><c>int, int</c>Emulator screen points</returns>
        protected (int, int) ViewToScreen(double x, double y) {

            // Get camera coordinates
            double camX = APIs.Memory.ReadS16(_cameraXposAddress);
            double camY = APIs.Memory.ReadS16(_cameraYposAddress);

            // Adjusts position
            x = x - camX + (BokInterface.gbaScreenWidth / 2);
            y = y - camY + (BokInterface.gbaScreenHeight / 2);

            // Transforms GBA screen point to Emulator screen point
            Point screenPoint = APIs.Client.TransformPoint(new Point((int)x, (int)y));
            return (screenPoint.X, screenPoint.Y);
        }

        /// <summary>Returns world coordinates converted to screen coordinates</summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <returns><c>double, double</c>Screen coordinates (X, Y)</returns>
        protected (double, double) WorldToScreen(double x, double y, double z) {
            (double, double, double) viewCoordinates = WorldToView(x, y, z);
            return ViewToScreen(viewCoordinates.Item1, viewCoordinates.Item2);
        }
    }
}

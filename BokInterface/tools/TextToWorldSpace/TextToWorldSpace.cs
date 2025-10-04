using System.Drawing;

using BokInterface.Addresses;
using BokInterface.Utils;

namespace BokInterface.Tools.TextToWorldSpace {
    /// <summary>
    /// <para>Allows printing text and data to world space</para>
    /// <para>Made by Raphi, Doc & Shenef</para>
    /// </summary>
    class TextToWorldSpace {

        #region Properties

        protected double planeScale = (double)0x30 / 0x100,
            heightScale = (double)0x18 / 0x100;
        protected Color textColor = new();
        private readonly dynamic? _memAddresses;
        private uint _cameraXposAddress = 0,
            _cameraYposAddress = 0;

        #endregion

        #region Constructors

        public TextToWorldSpace(BoktaiAddresses boktaiAddresses, string text, double x, double y, double z, Color? textColor = null) {
            if (text == "") {
                return;
            }

            // Set memory addresses used for getting the camera coordinates
            _memAddresses = boktaiAddresses;
            SetCameraAddresses();

            // Set text color & write to coordinates
            this.textColor = textColor == null ? Color.LimeGreen : (Color)textColor;
            WriteTextToCoordinates(text, x, y, z);
        }

        public TextToWorldSpace(ZoktaiAddresses zoktaiAddresses, string text, double x, double y, double z, Color? textColor = null) {
            if (text == "") {
                return;
            }

            // Set memory addresses used for getting the camera coordinates
            _memAddresses = zoktaiAddresses;
            SetCameraAddresses();

            // Set text color & write to coordinates
            this.textColor = textColor == null ? Color.LimeGreen : (Color)textColor;
            WriteTextToCoordinates(text, x, y, z);
        }

        public TextToWorldSpace(ShinbokAddresses shinbokAddresses, string text, double x, double y, double z, Color? textColor = null) {
            if (text == "") {
                return;
            }

            // Set memory addresses used for getting the camera coordinates
            _memAddresses = shinbokAddresses;
            SetCameraAddresses();

            // Set text color & write to coordinates
            this.textColor = textColor == null ? Color.LimeGreen : (Color)textColor;
            WriteTextToCoordinates(text, x, y, z);
        }

        public TextToWorldSpace(LunarKnightsAddresses lunarKnightsAddresses, string text, double x, double y, double z, Color? textColor = null) {
            if (text == "") {
                return;
            }

            // Set memory addresses used for getting the camera coordinates
            _memAddresses = lunarKnightsAddresses;
            SetCameraAddresses();

            // Set text color & write to coordinates
            this.textColor = textColor == null ? Color.LimeGreen : (Color)textColor;
            WriteTextToCoordinates(text, x, y, z);
        }

        #endregion

        #region Methods

        /// <summary>Set camera memory addresses used for writing position</summary>
        /// <returns><c>bool</c>True if addresses were set, false otherwise</return>
        private bool SetCameraAddresses() {
            if (_memAddresses == null) {
                return false;
            }

            switch (BokInterface.shorterGameName) {
                case "Boktai":
                case "Zoktai":
                case "Shinbok":
                    _cameraXposAddress = _memAddresses.Misc["x_camera"].Address;
                    _cameraYposAddress = _memAddresses.Misc["y_camera"].Address;
                    return true;
                case "LunarKnights":
                default:
                    // Current not handled, not enough data available
                    _cameraXposAddress = _cameraYposAddress = 0;
                    return false;
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
            double posX = screenCoordinates.Item1,
                posY = screenCoordinates.Item2;

            // Print text on screen at screen coordinates
            APIs.Gui.Text((int)posX, (int)posY, text, textColor);
        }

        /// <summary>Convert world coordinates to view coordinates</summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <returns><c>double, double, double</c>View coordinates(X, Y, Z)</returns>
        protected (double, double, double) WorldToView(double x, double y, double z) {

            // Prepare world coordinates
            double scaledWorldZ = z * heightScale,
                worldX = x / 2,
                worldY = y / 2,
                worldY2 = (worldX + worldY) * planeScale;

            // Get wiew coordinates
            double viewX = (worldX - worldY) * planeScale,
                viewY = worldY2 - scaledWorldZ,
                viewZ = worldY2 + scaledWorldZ;

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

        #endregion
    }
}

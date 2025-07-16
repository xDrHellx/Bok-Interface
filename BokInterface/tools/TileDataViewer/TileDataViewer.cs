using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.All;

namespace BokInterface.Tools.TileDataViewer {
    /// <summary>
    ///     <para>Tile Data Viewer tool.</para>
    ///     <para><i>
    ///         Print and show Tile Data of the current map. <br/>
    ///         Made by Raphi, converted from Lua to C# by Doc.
    ///     </i></para>
    /// </summary>
    abstract class TileDataViewer : Form {

        #region Properties

        protected string name = "tileDataViewer",
            title = "Tile Data Viewer";
        protected int width = 500,
            height = 500;
        public int index = 0;
        protected uint scale = 16,
            alpha = 0xA0,
            mapDataAddress = 0,
            djangoXposAddress = 0,
            djangoYposAddress = 0;
        protected static int imgNb = 1,
            frameCount = 0;
        protected List<Color>? colorPalette;
        protected static readonly Pen s_zonePen = new(Color.LimeGreen);
        protected static readonly Color s_blackColor = ColorTranslator.FromHtml("#0F0F0F");
        protected readonly bool _debugMode = false;

        #endregion

        #region Subwindow & loop
        #region Settings & frame loop

        /// <summary>Initialize subwindow properties</summary>
        protected void InitializeSubwindowProperties() {
            Name = name;
            Text = title;
            AutoScaleDimensions = new SizeF(6F, 15F);
            AutoScaleMode = AutoScaleMode.Inherit;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = SystemColors.Control;
            Font = WinFormHelpers.defaultFont;
            AutoScroll = true;
            SetSubwindowSize(width, height);

            // Prevent flickering
            SetStyle(
                ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer,
                true
            );

            // Generate color palette
            colorPalette = GenerateRandomColorPalette();

            // Set memory addresses to use & show subwindow
            SetGameAddresses();
            Show();
        }

        /// <summary>Sets the subwindow's size</summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        protected void SetSubwindowSize(int width, int height) {
            ClientSize = new Size(width, height);
        }

        /// <summary>
        ///     <para>Initialize frame loop.</para>
        ///     <para><i>
        ///         Add the corresponding methods to BokInterface.functionsList to have them be executed every frame. <br/>
        ///         Also get the index from that list for removing the methods when closing the tool.
        ///     </i></para>
        /// </summary>
        public void InitializeFrameLoop() {
            BokInterface.functionsList.Add(Refresh);
            /**
             * Get the index of the added function,
             * used for removing the method from BokInterface.functionsList when the tool is closed
             */
            index = BokInterface.functionsList.Count - 1;
        }

        #endregion

        #region Game specific

        /// <summary>Set the game addresses for reading data</summary>
        protected abstract void SetGameAddresses();

        /// <summary>Get the name of a tile effect</summary>
        /// <param name="bitNb">Number corresponding to the bit effect</param>
        /// <returns><c>string</c>Effect name (empty if unknown / undocumented)</returns>
        protected abstract string GetTileEffectName(int bitNb);

        #endregion

        #region Drawing

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            // Increment the frame counter (used for alternating between icons & tile effects)
            frameCount++;
            if (frameCount >= 60) {
                frameCount = 0;
            }

            // Get map & tile data pointers
            uint mapDataPointers = APIs.Memory.ReadU32(mapDataAddress);
            if (mapDataPointers == 0) {
                return;
            }

            uint tileData = APIs.Memory.ReadU32(mapDataPointers + 4);
            if (tileData == 0) {
                return;
            }

            // Get tile width & height
            uint tileWidth = APIs.Memory.ReadU16(tileData + 4);
            uint tileHeight = APIs.Memory.ReadU16(tileData + 6);

            // Draw map tiles data (effects, stairs, ...)
            DrawTileData(e, tileData, tileWidth, tileHeight);

            // Draw zones
            DrawZones(e, APIs.Memory.ReadU32(mapDataPointers + 12));

            /**
             * Draw Django
             * If both values are at 0, it might be due to a soft reset, so we get the addresses again
             */
            uint djangoX = APIs.Memory.ReadU16(djangoXposAddress);
            uint djangoY = APIs.Memory.ReadU16(djangoYposAddress);
            if (djangoX == 0 && djangoY == 0) {
                SetGameAddresses();
                djangoX = APIs.Memory.ReadU16(djangoXposAddress);
                djangoY = APIs.Memory.ReadU16(djangoYposAddress);
            }

            DrawDjangoIcon(e, djangoX, djangoY);
        }

        /// <summary>Draw a tile and its data</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="tileData">Tile data</param>
        /// <param name="tileWidth">Tile width</param>
        /// <param name="tileHeight">Tile height</param>
        protected void DrawTileData(PaintEventArgs e, uint tileData, uint tileWidth, uint tileHeight) {
            if (colorPalette == null) {
                return;
            }

            for (int tileY = 0; tileY < tileHeight - 1; tileY++) {
                for (int tileX = 0; tileX < tileWidth - 1; tileX++) {

                    uint tile = APIs.Memory.ReadU32(tileData + 0xC + (tileY * tileWidth + tileX) * 4);
                    uint value = tile & 0xFF;

                    Color tileColor = colorPalette[(int)value + 1];
                    DrawFilledRectangle(e, tileColor, (int)(tileX * scale), (int)(tileY * scale), (int)scale);

                    // Draw the stairs icon if present on the tile
                    DrawStairsIcon(e, (tile & 0xF0) >> 4, tileX, tileY, (int)scale);

                    // Draw the effects on top if it has any
                    DrawTileEffects(e, (tile & 0xFFFF0000) >> 16, tileX, tileY, (int)scale);
                }
            }

            // Adjust subwindow size based on the number of tiles to show
            SetSubwindowSize((int)(tileWidth * scale) - 16, (int)(tileHeight * scale) - 16);
        }

        /// <summary>Draw stairs icon based on value</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="stairsValue">Value for the stairs on the tile</param>
        /// <param name="posX">X position of the tile</param>
        /// <param name="posY">Y position of the tile</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected void DrawStairsIcon(PaintEventArgs e, uint stairsValue, int posX, int posY, int scale) {
            if (stairsValue > 0) {
                DrawTileImage(e, "stairs" + stairsValue, posX * scale, 1 + posY * scale);
            }
        }

        /// <summary>Draw tile effect icons</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="tileEffect">Value for the tile's effect</param>
        /// <param name="posX">X position of the tile</param>
        /// <param name="posY">Y position of the tile</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected abstract void DrawTileEffects(PaintEventArgs e, uint tileEffect, int posX, int posY, int scale);

        /// <summary>
        ///     <para>Draw zones.</para>
        ///     <para><i>
        ///         For example loading zones, but not necessarily. <br/>
        ///         The map data only defines the position/size of the zone, but not what it does.
        ///     </i></para>
        /// </summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="zonesData">Zones-related data</param>
        protected void DrawZones(PaintEventArgs e, uint zonesData) {

            float zoneScale = scale / 256.0f;
            uint zoneCount = APIs.Memory.ReadU8(zonesData);
            uint zonePtr = zonesData + 4;

            for (int i = 0; i < zoneCount; i++) {

                // Get the top left & bottom right corners
                int startX = APIs.Memory.ReadS16(zonePtr);
                int startY = APIs.Memory.ReadS16(zonePtr + 2);
                int endX = APIs.Memory.ReadS16(zonePtr + 4);
                int endY = APIs.Memory.ReadS16(zonePtr + 6);

                // Draw the zone
                e.Graphics.DrawRectangle(
                    s_zonePen,
                    startX * zoneScale,
                    startY * zoneScale,
                    (endX - startX) * zoneScale,
                    (endY - startY) * zoneScale
                );

                // Increment the pointer to the next zone
                zonePtr += 12;
            }
        }

        /// <summary>Draw Django icon on tilemap</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="posX">X position</param>
        /// <param name="posY">Y position</param>
        protected void DrawDjangoIcon(PaintEventArgs e, uint posX, uint posY) {
            // Alternate between icons over time
            imgNb = frameCount >= 0 && frameCount < 30 ? 1 : 2;
            DrawTileImage(
                e,
                "django" + imgNb,
                (int)(5 + posX / 256 * scale - scale / 4),
                (int)(4 + posY / 256 * scale - scale / 4)
            );
        }

        #endregion

        #region Simplified generation

        /// <summary>Simplified method for drawing a filled rectangle</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="color">Rectangle color</param>
        /// <param name="posX">X position</param>
        /// <param name="posY">Y position</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected void DrawFilledRectangle(PaintEventArgs e, Color color, int posX, int posY, int scale) {
            using Pen pen = new(color, 1);

            Rectangle rectangle = new(posX, posY, scale, scale);
            e.Graphics.DrawRectangle(pen, rectangle);

            using SolidBrush brush = new(color);
            e.Graphics.FillRectangle(brush, rectangle);
        }

        /// <summary>Simplified method for drawing an image on a tile</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="imgName">Image name</param>
        /// <param name="posX">X position of the image</param>
        /// <param name="posY">Y position of the image</param>
        protected void DrawTileImage(PaintEventArgs e, string imgName, int posX, int posY) {
            Image tileImg = (Image)Properties.Resources.ResourceManager.GetObject(imgName);
            Point imgCorner = new(posX, posY);
            e.Graphics.DrawImage(tileImg, imgCorner);
        }

        #endregion

        #region Misc

        /// <summary>Generate a random color palette</summary>
        /// <returns><c>List (System.Drawing.Color)</c>Palette</returns>
        protected List<Color> GenerateRandomColorPalette() {

            List<Color> result = [];
            uint seed = 0x803049D;

            for (int i = 0; i < 255; i++) {
                seed = (seed * 0x41C64E6D + 12345) & 0xFFFFFFFF;

                uint r = (seed >> 8) & 0x1F;
                uint g = (seed >> 13) & 0x1F;
                uint b = (seed >> 18) & 0x1F;

                // Generate color & convert to System.Drawing.Color
                uint color = (alpha << 24) | (r << 19) | (g << 11) | (b << 3);
                Color generatedColor = Color.FromArgb((int)color);

                result.Add(generatedColor);
            }

            return result;
        }

        #endregion
    }
}
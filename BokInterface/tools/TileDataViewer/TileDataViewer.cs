using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.All;

namespace BokInterface.Tools.TileDataViewer {
    /// <summary>
    /// <summary>Basis class for the Tile Data Viewer tool</summary>
    /// <para>Prints and show Tile Data (AKA Map)</para>
    /// <para>Made by Raphi, converted from Lua to C# by Doc</para>
    /// </summary>
    abstract class TileDataViewer : Form {

        #region Subwindow properties

        protected string name = "tileDataViewer",
            title = "Tile Data Viewer";
        protected int width = 500,
            height = 500;

        #endregion

        #region Tool properties

        public int index = 0;
        protected uint scale = 16,
            alpha = 0xA0;
        protected int textY = 0;
        protected static int imgNb = 1,
            n = 0;
        protected List<Color>? colorPalette;
        protected static readonly Pen s_zonePen = new(Color.LimeGreen);
        protected static readonly Color s_blackColor = ColorTranslator.FromHtml("#0f0f0f");

        #endregion

        #region Memory addresses properties
        protected uint mapDataAddress = 0,
            djangoXposAddress = 0,
            djangoYposAddress = 0;

        #endregion

        #region Subwindow & loop-related methods

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

        /// <summary>Sets memory addresses used for the current game</summary>
        protected abstract void SetGameAddresses();

        /// <summary>
        /// <para>Initialize frame loop</para>
        /// <para>Adds the corresponding methods to BokInterface.functionsList to have them be executed every frame</para>
        /// <para>Also get the index from that list for removing the methods when closing the tool</para>
        /// </summary>
        public void InitializeFrameLoop() {
            BokInterface.functionsList.Add(Refresh);

            /**
             * Get the index of the added function,
             * used for removing the method from BokInterface.functionsList when the subwindow is closed
             */
            index = BokInterface.functionsList.Count - 1;
        }

        #endregion

        #region Drawing methods

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            // 1. Get map data & pointers
            uint mapDataPointers = APIs.Memory.ReadU32(mapDataAddress);
            if (mapDataPointers == 0) {
                return;
            }

            uint mapData = APIs.Memory.ReadU32(mapDataPointers + 4);
            if (mapData == 0) {
                return;
            }

            // Get tile width & height
            uint tileWidth = APIs.Memory.ReadU16(mapData + 4);
            uint tileHeight = APIs.Memory.ReadU16(mapData + 6);

            // 2. Draw map tiles data (effects, stairs, ...)
            DrawTileData(e, mapData, tileWidth, tileHeight);

            // 3. Draw zones
            DrawZones(e, APIs.Memory.ReadU32(mapDataPointers + 12));

            // 4. Draw Django on map
            uint djangoX = APIs.Memory.ReadU16(djangoXposAddress);
            uint djangoY = APIs.Memory.ReadU16(djangoYposAddress);

            // If both values are at 0, it might be due to a soft reset, so we get the addresses again
            if (djangoX == 0 && djangoY == 0) {
                SetGameAddresses();
                djangoX = APIs.Memory.ReadU16(djangoXposAddress);
                djangoY = APIs.Memory.ReadU16(djangoYposAddress);
            }

            DrawDjangoIcon(e, djangoX, djangoY);

            // Write tile address on screen
            // uint tileAddress = mapData + 0xc + ((djangoY >> 8) * tileWidth + (djangoX >> 8)) * 4;
        }

        /// <summary>Draws a tile and its data</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="mapData">Map data</param>
        /// <param name="tileWidth">Tile width</param>
        /// <param name="tileHeight">Tile height</param>
        protected void DrawTileData(PaintEventArgs e, uint mapData, uint tileWidth, uint tileHeight) {

            if (colorPalette == null) {
                return;
            }

            for (int tileY = 0; tileY < tileHeight - 1; tileY++) {
                for (int tileX = 0; tileX < tileWidth - 1; tileX++) {

                    uint tile = APIs.Memory.ReadU32(mapData + 0xC + (tileY * tileWidth + tileX) * 4);
                    uint value = tile & 0xFF;

                    Color tileColor = colorPalette[(int)value + 1];
                    DrawFilledRectangle(
                        e,
                        tileColor,
                        (int)(5 + tileX * scale),
                        (int)(5 + tileY * scale),
                        (int)scale
                    );

                    // Draw the tile's effect if it has any
                    DrawTileEffect(e, (tile & 0xFFFF0000) >> 16, tileX, tileY, (int)scale);

                    // Draw the stairs icon if stairs are present on the tile
                    DrawStairsIcon(e, (tile & 0xF0) >> 4, tileX, tileY, (int)scale);
                }
            }

            // Adjust subwindow size based on the number of tiles to show
            SetSubwindowSize(
                (int)(tileWidth * scale) - 5,
                (int)(tileHeight * scale) - 5
            );
        }

        /// <summary>Draws tile effect icons</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="tileEffect">Value for the tile's effect</param>
        /// <param name="posX">X position of the tile</param>
        /// <param name="posY">Y position of the tile</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected abstract void DrawTileEffect(PaintEventArgs e, uint tileEffect, int posX, int posY, int scale);

        /// <summary>Draws stairs icon on the tile if there are any</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="stairsValue">Value for the stairs on the tile</param>
        /// <param name="posX">X position of the tile</param>
        /// <param name="posY">Y position of the tile</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected void DrawStairsIcon(PaintEventArgs e, uint stairsValue, int posX, int posY, int scale) {
            if (stairsValue == 0) {
                return;
            }

            // Draw the stairs img
            DrawTileImage(e, "stairs" + stairsValue, 5 + posX * scale, 6 + posY * scale);
        }

        /// <summary>
        /// Draw zones. <br/>
        /// For example loading zones, but not necessarily. <br/>
        /// The map data only defines the position/size of the zone, but not what it does.
        /// </summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="zonesData">Zones-related data</param>
        protected void DrawZones(PaintEventArgs e, uint zonesData) {

            float zoneScale = scale / 256.0f;
            uint zoneCount = APIs.Memory.ReadU8(zonesData);
            uint zonePtr = zonesData + 4;

            for (int i = 0; i < zoneCount; i++) {
                int startX = APIs.Memory.ReadS16(zonePtr);
                int startY = APIs.Memory.ReadS16(zonePtr + 2);
                int endX = APIs.Memory.ReadS16(zonePtr + 4);
                int endY = APIs.Memory.ReadS16(zonePtr + 6);
                // start height (u8), end height (u8), and zone id (u16) follows, but these don't matter for drawing.

                e.Graphics.DrawRectangle(
                    s_zonePen,
                    5 + startX * zoneScale,
                    5 + startY * zoneScale,
                    (endX - startX) * zoneScale,
                    (endY - startY) * zoneScale
                );

                zonePtr += 12;
            }
        }

        /// <summary>Draws Django icon on tilemap</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="posX">X position</param>
        /// <param name="posY">Y position</param>
        protected void DrawDjangoIcon(PaintEventArgs e, uint posX, uint posY) {

            // Update imgNb to switch between images for Django & draw it
            UpdateImgNb();
            DrawTileImage(
                e,
                "django" + imgNb,
                (int)(10 + posX / 256 * scale - scale / 4),
                (int)(9 + posY / 256 * scale - scale / 4)
            );
        }

        #endregion

        #region Utilities & misc methods

        /// <summary>Simplified method for drawing a filled rectangle</summary>
        /// <param name="e">Painting event used for drawing</param>
        /// <param name="color">Rectangle color</param>
        /// <param name="posX">X position</param>
        /// <param name="posY">Y position</param>
        /// <param name="scale">Scale (used for drawing)</param>
        protected void DrawFilledRectangle(PaintEventArgs e, Color color, int posX, int posY, int scale) {
            using Pen pen = new(color, 1);
            Rectangle rectangle = new(
                posX,
                posY,
                scale,
                scale
            );

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

        /// <summary>Updates imgNb variable, used for Django icons (see DrawDjangoIcon)</summary>
        protected void UpdateImgNb() {
            n++;
            if (n >= 0 && n < 30) {
                imgNb = 1;
            } else {
                imgNb = 2;
            }

            if (n >= 60) {
                n = 0;
            }
        }

        /// <summary>Generate a random color palette</summary>
        /// <returns><c>List (System.Drawing.Color)</c>Palette</returns>
        protected List<Color> GenerateRandomColorPalette() {

            List<Color> result = [];
            uint seed = 0x803049d;

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

        /// <summary>Write text on GUI</summary>
        /// <param name="text">Text</param>
        protected void WriteText(string text) {
            APIs.Gui.Text(0, textY, text);
            textY += 20;
        }

        /// <summary>Dump actors infos on screen</summary>
        protected void DumpActors() {

            uint globalEnable = APIs.Memory.ReadU32(0x03004438);

            for (int groupIndex = 0; groupIndex < 0xc; groupIndex++) {

                uint actor = APIs.Memory.ReadU32(0x03004480 + groupIndex * 8);
                uint enableFlags = APIs.Memory.ReadU32(0x03004480 + groupIndex * 8 + 4);

                while (actor != 0) {
                    // this.WriteText(String.Format(
                    //     "  {0} (flags: {1}, {2}, {3}) @ ({4}, {5}, {6})",
                    //     actor,
                    //     APIs.Memory.ReadU32(actor + 8),
                    //     APIs.Memory.ReadU32(actor + 0xc),
                    //     APIs.Memory.ReadU16(actor + 0x12),
                    //     APIs.Memory.ReadS16(actor + 0x24) >> 8,
                    //     APIs.Memory.ReadS16(actor + 0x26) >> 8,
                    //     APIs.Memory.ReadS16(actor + 0x28) >> 8
                    // ));

                    actor = APIs.Memory.ReadU32(actor + 4);
                }
            }
        }

        #endregion
    }
}

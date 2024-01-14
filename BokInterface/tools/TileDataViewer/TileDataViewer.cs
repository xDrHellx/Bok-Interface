using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BokInterface.All;
using BokInterface.Boktai;

namespace BokInterface.Tools.TileDataViewer {
    /// <summary>
    /// <para>Subwindow for the Tile Data Viewer</para>
    /// <para>Prints and show Tile Data (AKA Map)</para>
    /// <para>Made by Raphi, converted from Lua to C# by Doc</para>
    /// </summary>
    class TileDataViewer : Form {

        public int index = 0;
        private readonly BoktaiAddresses boktaiAddresses = new();
        protected uint scale = 16; // 14
        protected uint alpha = 0xa0;
        /// <summary>Byte of the 32 bit tile data that should be rendered</summary>
        protected int tileByte = 0;
        protected int textY = 0;
        protected static int imgNb = 1;
        protected static int n = 0;
        protected List<System.Drawing.Color> colorPalette;

        public TileDataViewer(string name, string title, Int32 width, Int32 height, string icon = "") {
            this.Name = name;
            this.Text = title;
            this.Icon = this.GetIcon(icon);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Font = BokInterfaceMainForm.defaultFont;
            this.AutoScroll = true;
            this.SetSubwindowSize(width, height);

            // Prevent flickering
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint | 
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | 
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, 
                true
            );

            // Generate color palette
            this.colorPalette = this.GenerateRandomColorPalette();
        }

        /// <summary>Get the specified icon if it exist</summary>
        /// <param name="fileName">File name (without .ico extension)</param>
        /// <returns><c>System.Drawing.Icon</c>Specified Icon instance (or default if the specified icon could not be found)</returns>
        protected System.Drawing.Icon GetIcon(string fileName) {
            if(fileName == "") {
				return this.Icon;
			} else {
				return (Icon)Properties.Resources.ResourceManager.GetObject(fileName);
			}
        }

        /// <summary>Sets the subwindow's size</summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        protected void SetSubwindowSize(int width, int height) {
            this.ClientSize = new System.Drawing.Size(width, height);
        }

        /// <summary>
        /// <para>Initialize frame loop</para>
        /// <para>Adds the corresponding methods to BokInterfaceMainForm.functionsList to have them be executed every frame</para>
        /// <para>Also get the index from that list for removing the methods when closing the Tile Data Viewer</para>
        /// </summary>
        public void InitializeFrameLoop() {
            BokInterfaceMainForm.functionsList.Add(this.Refresh);

            /**
             * Get the index of the added function,
             * used for removing the method from BokInterfaceMainForm.functionsList when the subwindow is closed
             */ 
            this.index = BokInterfaceMainForm.functionsList.Count -1;
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            // 1. Get map data
            uint mapDataPointers = APIs.Memory.ReadU32(boktaiAddresses.Misc["map_data"]);
            if(mapDataPointers == 0) {
                return;
            }

            uint mapData = APIs.Memory.ReadU32(mapDataPointers + 4);
            if(mapData == 0) {
                return;
            }

            // Get tile width & height
            uint tileWidth = APIs.Memory.ReadU16(mapData + 4);
            uint tileHeight = APIs.Memory.ReadU16(mapData + 6);

            // Set tile shift
            int tileShift = this.tileByte * 8;
            // this.WriteText(String.Format("Visualizing tile data with mask {0}", 0xff << tileShift));

            // 2. Draw map tiles data
            this.DrawTileData(e, mapData, tileWidth, tileHeight, tileShift);

            // 3. Draw zones
            this.DrawZones(e, APIs.Memory.ReadU32(mapDataPointers + 12));

            // 4. Draw Django on map
            uint djangoX = APIs.Memory.ReadU16(boktaiAddresses.Django["x_position"]);
            uint djangoY = APIs.Memory.ReadU16(boktaiAddresses.Django["y_position"]);
            this.DrawDjangoIcon(e, djangoX, djangoY);

            // Write tile address on screen
            uint tileAddress = mapData + 0xc + ((djangoY >> 8) * tileWidth + (djangoX >> 8)) * 4;
            // this.WriteText(String.Format("Tile address: {0} (={0})", tileAddress, APIs.Memory.ReadU32(tileAddress)));
        }

        /// <summary>Draws a tile and its data</summary>
        /// <param name="e">Painting event using for drawing</param>
        /// <param name="mapData">Map data</param>
        /// <param name="tileWidth">Tile width</param>
        /// <param name="tileHeight">Tile height</param>
        /// <param name="tileShift">Tile shift</param>
        protected void DrawTileData(PaintEventArgs e, uint mapData, uint tileWidth, uint tileHeight, int tileShift) {

            for(int tileY = 0; tileY < tileHeight -1; tileY++) {
                for(int tileX = 0; tileX < tileWidth -1; tileX++) {

                    uint tile = APIs.Memory.ReadU32(mapData + 0xc + (tileY * tileWidth + tileX) * 4);
                    uint value = (tile >> tileShift) & 0xff;

                    System.Drawing.Color tileColor = this.colorPalette[(int)value + 1];
                    using (Pen pen = new(tileColor, 1)) {
                        System.Drawing.Rectangle rectangle = new(
                            (int)(5 + tileX * scale),
                            (int)(5 + tileY * scale),
                            (int)scale,
                            (int)scale
                        );
                        e.Graphics.DrawRectangle(pen, rectangle);

                        using (System.Drawing.SolidBrush brush = new(tileColor)) {
                            e.Graphics.FillRectangle(brush, rectangle);
                        }
                    }
                }
            }

            // Adjusts subwindow size based on the number of tiles to show
            this.SetSubwindowSize(
                (int)(tileWidth * scale) -5,
                (int)(tileHeight * scale) -5
            );
        }

        private static Pen zonePen = new Pen(Color.LimeGreen);
        /// <summary>
        /// Draw zones. For example loading zones, but not necessarily. The map data only defines the position/size of
        /// the zone, but not what it does.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="zonesData"></param>
        protected void DrawZones(PaintEventArgs e, uint zonesData)
        {
            var zoneScale = scale/256.0f;
            var zoneCount = APIs.Memory.ReadU8(zonesData);
            var zonePtr = zonesData + 4;
            for (var i = 0; i < zoneCount; i++)
            {
                var startX = APIs.Memory.ReadS16(zonePtr);
                var startY = APIs.Memory.ReadS16(zonePtr+2);
                var endX = APIs.Memory.ReadS16(zonePtr+4);
                var endY = APIs.Memory.ReadS16(zonePtr+6);
                // start height (u8), end height (u8), and zone id (u16) follows, but these don't matter for drawing.

                e.Graphics.DrawRectangle(zonePen, 5 + startX*zoneScale, 5 + startY*zoneScale,
                    (endX-startX)*zoneScale, (endY-startY)*zoneScale);
                zonePtr += 12;
            }
        }

        /// <summary>Draws Django icon on tilemap</summary>
        /// <param name="e">Painting event using for drawing</param>
        /// <param name="posX">X position</param>
        /// <param name="posY">Y position</param>
        protected void DrawDjangoIcon(PaintEventArgs e, uint posX, uint posY) {

            // Update imgNb to switch between images for Django
            this.UpdateImgNb();
            
            // Set the upper-left corner of the img & draw it
            Image djangoImg = (Image)Properties.Resources.ResourceManager.GetObject("django" + imgNb);
            Point imgCorner = new(
                (int)(10 + posX / 256 * scale - scale / 4),
                (int)(9 + posY / 256 * scale - scale / 4)
            );
            e.Graphics.DrawImage(djangoImg, imgCorner);
        }

        /// <summary>Updates imgNb variable, used for Django icons (see DrawDjangoIcon)</summary>
        protected void UpdateImgNb() {
            n++;
            if(n >= 0 && n < 30) {
                imgNb = 1;
            } else {
                imgNb = 2;
            }

            if(n >= 60) {
                n = 0;
            }
        }

        /// <summary>Generate a random color palette</summary>
        /// <returns><c>List (System.Drawing.Color)</c>Palette</returns>
        protected List<System.Drawing.Color> GenerateRandomColorPalette() {

            // Initializing an array with a single value shifted left by 24 bits
            // uint[] result = {alpha << 24};
            List<System.Drawing.Color> result = new();
            uint seed = 0x803049d;

            for(int i = 0; i < 255; i++) {
                seed = (seed * 0x41c64e6d + 12345) & 0xffffffff;
                
                uint r = (seed >> 8) & 0x1f;
                uint g = (seed >> 13) & 0x1f;
                uint b = (seed >> 18) & 0x1f;

                // Generate color & convert to System.Drawing.Color
                uint color = (alpha << 24) | (r << 19) | (g << 11) | (b << 3);
                System.Drawing.Color generatedColor = System.Drawing.Color.FromArgb((int)color);

                result.Add(generatedColor);
            }

            return result;
        }

        /// <summary>Write text on GUI</summary>
        /// <param name="text">Text</param>
        protected void WriteText(string text) {
            APIs.Gui.Text(0, this.textY, text);
            this.textY += 20;
        }

        /// <summary>Dump actors infos on screen</summary>
        protected void DumpActors() {

            uint globalEnable = APIs.Memory.ReadU32(0x03004438);

            for(int groupIndex = 0; groupIndex < 0xc; groupIndex++) {

                uint actor = APIs.Memory.ReadU32(0x03004480 + groupIndex * 8);
                uint enableFlags = APIs.Memory.ReadU32(0x03004480 + groupIndex * 8 + 4);

                // this.WriteText(String.Format("Group {0} ({1} & {2} = {3})", groupIndex, enableFlags, globalEnable, enableFlags & globalEnable));

                while(actor != 0){
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
    }
}
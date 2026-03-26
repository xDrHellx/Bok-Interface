using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Items;
using BokInterface.Weapons;
using BokInterface.Abilities;
using BokInterface.Accessories;
using System.Reflection;
using System;

namespace BokInterface.Utils {
    ///<summary>Class for ComboBox with images next to selectable options</summary>
    public class ImageComboBox : ComboBox {

        private readonly ToolTip _toolTip = new();

        // Draws the items into the object
        protected override void OnDrawItem(DrawItemEventArgs e) {
            e.DrawBackground();
            e.DrawFocusRectangle();

            /**
             * Draw the value for the item in the list
             * Depending on the item's type we may show different things
             */
            object item = Items[e.Index];
            switch (item) {
                case KeyValuePair<string, Item> kvp:             // Boktai (series) items
                    DrawItemWithIcon(kvp.Value, e);
                    break;
                case KeyValuePair<string, Weapon> kvp:           // Bok 2 & 3 weapons
                    DrawItemWithIcon(kvp.Value, e);
                    break;
                case KeyValuePair<string, ShinbokLens> kvp:      // Bok 3 gun lenses
                    DrawItemWithIcon(kvp.Value, e);
                    break;
                case KeyValuePair<string, ShinbokFrame> kvp:     // Bok 3 gun frames
                    DrawItemWithIcon(kvp.Value, e);
                    break;
                case KeyValuePair<string, Accessory> kvp:        // Bok 2, 3, DS accessories
                    DrawItemWithIcon(kvp.Value, e);
                    GenerateToolTip(kvp.Value, e, BokInterface.shorterGameName == "Zoktai" ? ["defense", "weight", "effect"] : ["effect"]);
                    break;
                case KeyValuePair<string, Ability> kvp:          // Bok 2 & 3 weapon SP abilities
                    using (SolidBrush brush = new(e.ForeColor)) {
                        e.Graphics.DrawString(kvp.Value.name, e.Font, brush, e.Bounds.Left, e.Bounds.Top + 1);
                    }
                    break;
                default:                                        // Default ImageComboBox items
                    using (SolidBrush brush = new(e.ForeColor)) {
                        e.Graphics.DrawString(item?.ToString(), e.Font, brush, e.Bounds.Left, e.Bounds.Top + 2);
                    }
                    break;
            }

            base.OnDrawItem(e);
        }

        /// <summary>Draw the option for an ImageComboBoxItem</summary>
        /// <param name="item">ImageComboBoxItem</param>
        /// <param name="e">DrawItemEventArgs reference for drawing</param>
        /// <param name="leftBoundOffset">Left offset for drawing (by default 16px)/param>
        /// <param name="topBoundOffset">Top offset for drawing (by default 1px)</param>
        protected void DrawItemWithIcon<T>(T item, DrawItemEventArgs e, int leftBoundOffset = 16, int topBoundOffset = 1) {
            if (item == null) {
                return;
            }

            // Check if the item has the required fields
            FieldInfo nameField = item.GetType().GetField("name", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public);
            FieldInfo iconField = item.GetType().GetField("icon", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public);
            if (nameField == null || iconField == null) {
                return;
            }

            // Retrieve & draw the item's name
            string name = (string)nameField.GetValue(item);
            if (string.IsNullOrEmpty(name) == true) {
                return;
            }

            using SolidBrush brush = new(e.ForeColor);
            e.Graphics.DrawString(name, e.Font, brush, e.Bounds.Left + leftBoundOffset, e.Bounds.Top + topBoundOffset);

            // Same as above for the item's icon
            Image icon = (Image)iconField.GetValue(item);
            if (icon != null) {
                e.Graphics.DrawImage(icon, e.Bounds.Left, e.Bounds.Top + topBoundOffset);
            }
        }

        /// <summary>Generate a tooltip for an ImageComboBoxItem</summary>
        /// <param name="item">ImageComboBoxItem</param>
        /// <param name="e">DrawItemEventArgs reference for drawing</param>
        /// <param name="fields">Fields to show in the tooltip (each will be separated by "|")</param>
        protected void GenerateToolTip<T>(T item, DrawItemEventArgs e, params string[] fields) {
            if (item == null || fields.Length == 0) {
                return;
            }

            // Check if the item has each field & construct the text for the tooltip
            string text = "";
            Type type = item.GetType();
            foreach (string field in fields) {
                FieldInfo info = type.GetField(field, BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public);
                if (info == null) {
                    return;
                }

                string value = info.GetValue(item).ToString();
                if (string.IsNullOrEmpty(value) == false) {
                    text += (text != "" ? " | " : "") + field switch {
                        "defense" => $"DEF: {value}",
                        "weight" => $"WEIGHT: {value}",
                        _ => value
                    };
                }
            }

            // Add the tooltip if there is text to show
            if (text != "" && (e.State & DrawItemState.Selected) == DrawItemState.Selected) {
                _toolTip.Show(text, this, e.Bounds.Right, e.Bounds.Bottom, 2000);
            }
        }
    }
}

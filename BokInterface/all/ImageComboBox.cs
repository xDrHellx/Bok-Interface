using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Items;

namespace BokInterface.All {
    ///<summary>Class for ComboBox with images next to selectable options</summary>
    class ImageComboBox : ComboBox {
        public ImageComboBox() { }

        // Draws the items into the object
        protected override void OnDrawItem(DrawItemEventArgs e) {
            e.DrawBackground();
            e.DrawFocusRectangle();

            /**
             * Draw the value for the item in the list
             * Depending on the type of the item in the list we show different things
             */
            if (Items[e.Index].GetType() == typeof(KeyValuePair<string, Item>)) {

                // For Boktai items
                KeyValuePair<string, Item> option = (KeyValuePair<string, Item>)Items[e.Index];
                Item optionItem = option.Value;

                // Draw the item's name & icon (if it has one)
                e.Graphics.DrawString(optionItem.name, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + (optionItem.icon != null ? optionItem.icon.Width : 0), e.Bounds.Top + 1);
                if (optionItem.icon != null) {
                    e.Graphics.DrawImage(optionItem.icon, e.Bounds.Left, e.Bounds.Top + 1);
                }
            } else {
                // Default item
                ImageComboBoxItem item = new(Items[e.Index].ToString());
                e.Graphics.DrawString(item.Value, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left, e.Bounds.Top + 2);
            }

            base.OnDrawItem(e);
        }
    }

    ///<summary>Class for default options shown in ImageComboBox's dropdown list</summary>
    public class ImageComboBoxItem(string val) {

        private string _value = val;

        public string Value {
            get { return _value; }
            set { _value = value; }
        }

        public ImageComboBoxItem() : this("") { }

        public override string ToString() {
            return _value;
        }
    }
}
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.Entities;
using BokInterface.Items;
using BokInterface.Weapons;
using BokInterface.Abilities;
using BokInterface.Accessories;

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

                /**
                 * Draw the item's name & icon (if it has one)
                 * We always add the space an icon would take so that elements are aligned properly
                 */
                e.Graphics.DrawString(optionItem.name, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 16, e.Bounds.Top + 1);
                if (optionItem.icon != null) {
                    e.Graphics.DrawImage(optionItem.icon, e.Bounds.Left, e.Bounds.Top + 1);
                }
            } else if (Items[e.Index].GetType() == typeof(KeyValuePair<string, Weapon>)) {

                // For Boktai weapons
                KeyValuePair<string, Weapon> option = (KeyValuePair<string, Weapon>)Items[e.Index];
                Weapon optionItem = option.Value;
                e.Graphics.DrawString(optionItem.name, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 16, e.Bounds.Top + 1);
                if (optionItem.icon != null) {
                    e.Graphics.DrawImage(optionItem.icon, e.Bounds.Left, e.Bounds.Top + 1);
                }
            } else if (Items[e.Index].GetType() == typeof(KeyValuePair<string, Ability>)) {

                // For Weapon SP abilities (Bok 2 & 3)
                KeyValuePair<string, Ability> option = (KeyValuePair<string, Ability>)Items[e.Index];
                Ability optionItem = option.Value;
                e.Graphics.DrawString(optionItem.name, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left, e.Bounds.Top + 1);
            } else if (Items[e.Index].GetType() == typeof(KeyValuePair<string, Accessory>)) {

                // For Boktai accessories
                KeyValuePair<string, Accessory> option = (KeyValuePair<string, Accessory>)Items[e.Index];
                Accessory optionItem = option.Value;
                e.Graphics.DrawString(optionItem.name, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 16, e.Bounds.Top + 1);
                if (optionItem.icon != null) {
                    e.Graphics.DrawImage(optionItem.icon, e.Bounds.Left, e.Bounds.Top + 1);
                }
            } else if (Items[e.Index].GetType() == typeof(KeyValuePair<string, ShinbokLens>)) {

                // For Shinbok gun lenses
                KeyValuePair<string, ShinbokLens> option = (KeyValuePair<string, ShinbokLens>)Items[e.Index];
                ShinbokLens optionItem = option.Value;
                e.Graphics.DrawString(optionItem.name, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 16, e.Bounds.Top + 1);
                if (optionItem.icon != null) {
                    e.Graphics.DrawImage(optionItem.icon, e.Bounds.Left, e.Bounds.Top + 1);
                }
            } else if (Items[e.Index].GetType() == typeof(KeyValuePair<string, ShinbokFrame>)) {

                // For Shinbok gun frames
                KeyValuePair<string, ShinbokFrame> option = (KeyValuePair<string, ShinbokFrame>)Items[e.Index];
                ShinbokFrame optionItem = option.Value;
                e.Graphics.DrawString(optionItem.name, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 16, e.Bounds.Top + 1);
                if (optionItem.icon != null) {
                    e.Graphics.DrawImage(optionItem.icon, e.Bounds.Left, e.Bounds.Top + 1);
                }
            } else if (Items[e.Index].GetType() == typeof(KeyValuePair<string, Character>)) {

                // For Boktai characters
                KeyValuePair<string, Character> option = (KeyValuePair<string, Character>)Items[e.Index];
                Character optionItem = option.Value;

                /**
                 * Draw the item's name & icon (if it has one)
                 * We always add the space an icon would take so that elements are aligned properly
                 */
                e.Graphics.DrawString(optionItem.name, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 16, e.Bounds.Top + 1);
                if (optionItem.icon != null) {
                    e.Graphics.DrawImage(optionItem.icon, e.Bounds.Left, e.Bounds.Top + 1);
                }
            } else if (Items[e.Index].GetType() == typeof(KeyValuePair<string, Character>)) {

                // For Boktai enemies
                KeyValuePair<string, Enemy> option = (KeyValuePair<string, Enemy>)Items[e.Index];
                Enemy optionItem = option.Value;

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
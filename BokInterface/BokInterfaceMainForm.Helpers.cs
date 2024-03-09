using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BokInterface.All;

/**
 * File for the winforms elements generation methods of the Bok interface & checks related to them
 */

namespace BokInterface {

    partial class BokInterfaceMainForm {

        #region Form elements generating methods

        /// <summary>Simplified method for creating a group box</summary>
        /// <param name="name">Group name</param>
        /// <param name="text">Group text</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="addToWindow">Set to true to add the element directly to the main interface window</param>
        /// <returns><c>System.Windows.Forms.GroupBox</c>Group box instance</returns>
        private GroupBox CreateGroupBox(string name, string text, int positionX, int positionY, int width, int height, bool addToWindow = false) {

            GroupBox groupBox = new() {
                Name = name,
                Text = text,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                AutoSize = false,
                TabIndex = 1,
                Anchor = defaultAnchor,
                Font = defaultFont
            };

            // Add to main window
            if (addToWindow == true) {
                Controls.Add(groupBox);
            }

            return groupBox;
        }

        /// <summary>Simplified method for creating a label</summary>
        /// <param name="name">Label name</param>
        /// <param name="text">Label text</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="addToWindow">Set to true to add the element directly to the main interface window</param>
        /// <param name="colorHex">Set the background color for the label</param>
        /// <param name="margin">Margin (by default System.Windows.Forms.Padding(0, 3, 0, 3), the default value in Visual Studio)</param>
        /// <param name="textAlignment">Text alignment, by default "MiddleCenter" (see System.Drawing.ContentAlignment for possible values)</param>
        /// <returns><c>System.Windows.Forms.Label</c>Label instance</returns>
        private Label CreateLabel(string name, string text, int positionX, int positionY, int width, int height, bool addToWindow = false, string colorHex = "", Padding margin = new Padding(), string textAlignment = "MiddleCenter") {

            Label label = new() {
                Name = name,
                Text = text,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                AutoSize = false,
                TabIndex = 2,
                Anchor = defaultAnchor,
                Margin = defaultMargin,
                Font = defaultFont
            };

            if (colorHex != "") {
                label.BackColor = ColorTranslator.FromHtml(colorHex);
            }

            // If no specific margin is passed, set defaults from Visual Studio
            if (margin.All == 0) {
                margin.Top = 3;
                margin.Left = 3;
            }

            // Text alignment
            label.TextAlign = GetTextAlignment(textAlignment);

            // Add to main window
            if (addToWindow == true) {
                Controls.Add(label);
            }

            return label;
        }

        /// <summary>Simplified method for creating a button</summary>
        /// <param name="name">Label name</param>
        /// <param name="text">Label text</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="addToWindow">Set to true to add the element directly to the main interface window</param>
        /// <param name="colorHex">Set the background color for the label</param>
        /// <param name="margin">Margin (by default System.Windows.Forms.Padding(0, 3, 0, 3), the default value in Visual Studio)</param>
        /// <param name="textAlignment">Text alignment, by default "MiddleCenter" (see System.Drawing.ContentAlignment for possible values)</param>
        /// <returns><c>System.Windows.Forms.Button</c>Button instance</returns>
        private Button CreateButton(string name, string text, int positionX, int positionY, int width, int height, bool addToWindow = false, string colorHex = "", Padding margin = new Padding(), string textAlignment = "MiddleCenter") {

            Button btn = new() {
                Name = name,
                Text = text,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                AutoSize = false,
                TabIndex = 2,
                Anchor = defaultAnchor,
                Margin = defaultMargin,
                Font = defaultFont
            };

            if (colorHex != "") {
                btn.BackColor = ColorTranslator.FromHtml(colorHex);
            }

            // If no specific margin is passed, set defaults from Visual Studio
            if (margin.All == 0) {
                margin.Top = 3;
                margin.Left = 3;
            }

            // Text alignment
            btn.TextAlign = GetTextAlignment(textAlignment);

            // Add to main window
            if (addToWindow == true) {
                Controls.Add(btn);
            }

            return btn;
        }

        /// <summary>Simplified method for creating a new sub window (AKA windows form)</summary>
        /// <param name="name">Subwindow name</param>
        /// <param name="title">Subwindow title</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="icon">Subwindow icon (by default retrieves the one from the main interface window)</param>
        /// <param name="parentForm">Form the subwindow is attached to (this will make the subwindow always show in front of its parent, by default it shows in front of the main window)</param>
        /// <returns><c>System.Windows.Forms.Form</c>Subwindow instance</returns>
        private Form CreateSubWindow(string name, string title, int width, int height, string icon = "", Form? parentForm = null) {

            Form form = new() {
                Name = name,
                Text = title,
                Icon = GetIcon(icon),
                AutoScaleDimensions = new SizeF(6F, 15F),
                AutoScaleMode = AutoScaleMode.Inherit,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                BackColor = SystemColors.Control,
                Font = defaultFont,
                ClientSize = new Size(width, height)
            };

            // Form parent / owner
            if (parentForm == null) {
                form.Owner = this;
            } else {
                form.Owner = parentForm;
            }

            return form;
        }

        /// <summary>Simplified method for creating a numeric input field</summary>
        /// <param name="name">Field name</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="minValue">Minimum settable value</param>
        /// <param name="maxValue">Maximum settable value</param>
        /// <param name="nbDecimals">Number of decimals (0 by default)</param>
        /// <param name="addToWindow">Set to true to add the element directly to the main interface window</param>
        /// <param name="colorHex">Set the background color for the label</param>
        /// <param name="margin">Margin (by default System.Windows.Forms.Padding(0, 3, 0, 3), the default value in Visual Studio)</param>
        /// <param name="valueAlignment">Value alignment, by default "Left" (see System.Windows.Forms.HorizontalAlignment for possible values)</param>
        /// <returns><c>System.Windows.Forms.NumericUpDown</c>NumericUpDown instance</returns>
        private NumericUpDown CreateNumericUpDown(string name, decimal defaultValue, int positionX, int positionY, int width, int height, decimal minValue = 0, decimal maxValue = 99, int nbDecimals = 0, bool addToWindow = false, string colorHex = "", Padding margin = new Padding(), string valueAlignment = "Left") {

            NumericUpDown field = new() {
                Name = name,
                Minimum = minValue,
                Maximum = maxValue,
                Value = defaultValue,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                AutoSize = false,
                TabIndex = 2,
                Anchor = defaultAnchor,
                Margin = defaultMargin,
                Font = defaultFont,
                Increment = (decimal)(maxValue > 500 ? 10 : (nbDecimals == 2 ? 0.05 : (nbDecimals == 1 ? 0.5 : 1))),
                DecimalPlaces = nbDecimals
            };

            if (colorHex != "") {
                field.BackColor = ColorTranslator.FromHtml(colorHex);
            }

            // If no specific margin is passed, set defaults from Visual Studio
            if (margin.All == 0) {
                margin.Top = 3;
                margin.Left = 3;
            }

            // Value alignment
            field.TextAlign = valueAlignment switch {
                "Right" => HorizontalAlignment.Right,
                "Center" => HorizontalAlignment.Center,
                _ => HorizontalAlignment.Left,
            };

            if (addToWindow == true) {
                Controls.Add(field);
            }

            return field;
        }

        /// <summary>Simplified method for creating a CheckGroupBox</summary>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="isCheckedByDefault">Set to true if the CheckGroupBox has to be checked when initiated</param>
        /// <param name="addToWindow">Set to true to add the element directly to the main interface window</param>
        /// <returns><c>CheckGroupBox</c>CheckGroupBox instance</returns>
        private CheckGroupBox CreateCheckGroupBox(string name, string text, int positionX, int positionY, int width, int height, bool isCheckedByDefault = false, bool addToWindow = false) {

            CheckGroupBox checkGroupBox = new() {
                Name = name,
                Text = text,
                Checked = isCheckedByDefault,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                TabIndex = 1,
                AutoSize = false,
                Anchor = defaultAnchor,
                Margin = defaultMargin,
                Font = defaultFont
            };

            // Add to main window
            if (addToWindow == true) {
                Controls.Add(checkGroupBox);
            }

            return checkGroupBox;
        }

        /// <summary>Simplified method for creating a label containing an image</summary>
        /// <param name="name">Label name</param>
        /// <param name="imgName">Image name</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="addToWindow">Set to true to add the element directly to the main interface window</param>
        /// <returns><c>System.Windows.Forms.Label</c>Label instance</returns>
        private Label CreateImageLabel(string name, string imgName, int positionX, int positionY, bool addToWindow = false) {

            // Get image
            Image img = (Image)Properties.Resources.ResourceManager.GetObject(imgName);

            // Create label
            Label label = new() {
                Name = name,
                Text = "",
                Location = new Point(positionX, positionY),
                Size = new Size(img.Width, img.Height),
                Image = img,
                AutoSize = false,
                TabIndex = 2,
                ImageAlign = ContentAlignment.MiddleCenter,
                Anchor = defaultAnchor,
                Margin = defaultMargin,
                Font = defaultFont
            };

            // Add to main window
            if (addToWindow == true) {
                Controls.Add(label);
            }

            return label;
        }

        /// <summary>Simplified method for creating a tooltip</summary>
        /// <returns><c>System.Windows.Forms.ToolTip<c/>Tooltip instance</returns>
        private static ToolTip CreateToolTip() {

            ToolTip toolTip = new() {
                // Always shows tooltip even if window isn't focused
                ShowAlways = true,

                // Set tooltip delays
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500
            };

            return toolTip;
        }

        #endregion

        #region Simplified adding methods

        /// <summary>Simplified method for adding the values warning tooltip to a list of labels</summary>
		/// <param name="labels">List of labels</param>
		public static void AddValuesWarningToolTip(List<Label> labels) {
            for (int i = 0; i < labels.Count; i++) {
                AddToolTip(labels[i], "Requires switching room to be fully updated");
            }
        }

        /// <summary>Simplified method for adding a tooltip to a label</summary>
        /// <param name="label">Label instance</param>
        /// <param name="text">Tooltip text</param>
        public static void AddToolTip(Label label, string text, Cursor? mouseCursor = null) {
            toolTip.SetToolTip(label, text);
            label.Cursor = mouseCursor ?? Cursors.Help;
        }

        /// <summary>Adds Tools section for the corresponding game</summary>
        private void AddToolsSection() {

            switch (shorterGameName) {
                case "Boktai":
                    extrasGroupBox = CreateGroupBox("extraTools", "Tools", 237, 25, 87, 52, true);
                    break;
                case "Zoktai":
                    extrasGroupBox = CreateGroupBox("extraTools", "Tools", 237, 187, 87, 52, true);
                    break;
                case "Shinbok":
                    extrasGroupBox = CreateGroupBox("extraTools", "Tools", 237, 187, 87, 52, true);
                    break;
                case "LunarKnights":
                    extrasGroupBox = CreateGroupBox("extraTools", "Tools", 237, 25, 87, 52, true);
                    break;
                default:
                    // If game is not handled, don't add anything & stop here
                    return;
            }

            // Add Misc Tools button
            Button miscToolsBtn = CreateButton("showExtraTools", "Misc tools", 6, 21, 75, 23);
            miscToolsBtn.Click += new System.EventHandler(OpenMiscToolsSelector);
            extrasGroupBox.Controls.Add(miscToolsBtn);
        }

        #endregion

        #region Simplified checks methods

        /// <summary>Get the name of the icon corresponding to the current game</summary>
        /// <returns><c>string</c>Icon name</returns>
        private string GetGameIconName() {
            return shorterGameName switch {
                "Boktai" => "lita",
                "Zoktai" => "ringo",
                "Shinbok" => "trinity",
                "LunarKnights" => "lucian",
                _ => "nero",
            };
        }

        /// <summary>Get the corresponding text alignment based on a string</summary>
        /// <param name="value">Text alignment string</param>
        /// <returns><c>System.Drawing.ContentAlignment</c>Text alignment object</returns>
        private ContentAlignment GetTextAlignment(string value) {
            return value switch {
                "BottomCenter" => ContentAlignment.BottomCenter,
                "BottomLeft" => ContentAlignment.BottomLeft,
                "BottomRight" => ContentAlignment.BottomRight,
                "MiddleLeft" => ContentAlignment.MiddleLeft,
                "MiddleRight" => ContentAlignment.MiddleRight,
                "TopCenter" => ContentAlignment.TopCenter,
                "TopLeft" => ContentAlignment.TopLeft,
                "TopRight" => ContentAlignment.TopRight,
                _ => ContentAlignment.MiddleCenter,
            };
        }

        #endregion
    }
}
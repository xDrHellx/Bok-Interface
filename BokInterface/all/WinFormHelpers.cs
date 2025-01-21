using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BokInterface.All {

    /// <summary>
    /// Class for WinForm Helpers
    /// <para><example>WinForm elements generation methods</example></para>
    /// </summary>
    static class WinFormHelpers {

        /// <summary>Tooltip for values that only updates after switching rooms</summary>
        public static ToolTip toolTip = CreateToolTip();

        /// <summary>Color for base stat points (Boktai 2, 3, LK)</summary>
        public static string baseStatColor = "#FFE600";

        /// <summary>Color for base stat points obtained from cards (Boktai 3)</summary>
        public static string cardsStatColor = "#f7df02";

        /// <summary>
        /// <para>
        /// Color for stat points from accessories (Boktai 3) <br/>
        /// These points does not affect as many things as base stat points
        /// </para>
        /// <example>For example STR points from accessories does not affect coffin carrying speed</example>
        /// </summary>
        public static string equipsStatColor = "#FFA529";

        /// <summary>Color for the total amount of points for a specific stat (Boktai 2, 3, LK)</summary>
        public static string totalStatColor = "#D3D3D3";

        public static Font defaultFont = new("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point);
        public static Padding defaultMargin = new(3, 0, 3, 0);
        public static AnchorStyles defaultAnchor = AnchorStyles.Top | AnchorStyles.Left;

        /// <summary>Get the specified icon if it exist</summary>
		/// <param name="fileName">File name (without .ico extension)</param>
		/// <returns><c>System.Drawing.Icon</c>Specified Icon instance (or default if the specified icon could not be found)</returns>
		public static Icon? GetIcon(string fileName) {
            return fileName == "" ? null : (Icon)Properties.Resources.ResourceManager.GetObject(fileName);
        }

        #region Form elements

        /// <summary>Simplified method for creating a group box</summary>
        /// <param name="name">Group name</param>
        /// <param name="text">Group text</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <returns><c>GroupBox</c>Instance</returns>
        public static GroupBox CreateGroupBox(string name, string text, int positionX, int positionY, int width, int height, Control? control = null) {

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

            // If a Control instance is passed, add the generated element to it
            control?.Controls.Add(groupBox);

            return groupBox;
        }

        /// <summary>Simplified method for creating a label</summary>
        /// <param name="name">Label name</param>
        /// <param name="text">Label text</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <param name="colorHex">Set the background color for the label</param>
        /// <param name="margin">Margin (by default Padding(0, 3, 0, 3), the default value in Visual Studio)</param>
        /// <param name="textAlignment">Text alignment, by default "MiddleCenter" (see System.Drawing.ContentAlignment for possible values)</param>
        /// <returns><c>Label</c>Instance</returns>
        public static Label CreateLabel(string name, string text, int positionX, int positionY, int width, int height, Control? control = null, string colorHex = "", Padding margin = new Padding(), string textAlignment = "MiddleCenter") {

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

            // If a Control instance is passed, add the generated element to it
            control?.Controls.Add(label);

            return label;
        }

        /// <summary>Simplified method for creating a button</summary>
        /// <param name="name">Label name</param>
        /// <param name="text">Label text</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <param name="colorHex">Set the background color for the label</param>
        /// <param name="margin">Margin (by default Padding(0, 3, 0, 3), the default value in Visual Studio)</param>
        /// <param name="textAlignment">Text alignment, by default "MiddleCenter" (see System.Drawing.ContentAlignment for possible values)</param>
        /// <param name="enabled">True if it should be enabled (True by default)</param>
        /// <returns><c>Button</c>Instance</returns>
        public static Button CreateButton(string name, string text, int positionX, int positionY, int width, int height, Control? control = null, string colorHex = "", Padding margin = new Padding(), string textAlignment = "MiddleCenter", bool enabled = true) {

            Button btn = new() {
                Name = name,
                Text = text,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                AutoSize = false,
                TabIndex = 2,
                Anchor = defaultAnchor,
                Margin = defaultMargin,
                Font = defaultFont,
                Enabled = enabled
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

            // If a Control instance is passed, add the generated element to it
            control?.Controls.Add(btn);

            return btn;
        }

        /// <summary>Simplified method for creating a new sub window (AKA windows form)</summary>
        /// <param name="name">Subwindow name</param>
        /// <param name="title">Subwindow title</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="parentForm">Form the subwindow is attached to (this will make the subwindow always show in front of its parent, by default it shows in front of the main window)</param>
        /// <param name="icon">Subwindow icon (by default retrieves the one from the main interface window)</param>
        /// <returns><c>Form</c>Instance</returns>
        public static Form CreateSubWindow(string name, string title, int width, int height, Form? parentForm = null, string icon = "") {

            Form form = new() {
                Name = name,
                Text = title,
                Icon = icon == "" && parentForm != null && parentForm.Icon != null ? parentForm.Icon : GetIcon(icon),
                AutoScaleDimensions = new SizeF(6F, 15F),
                AutoScaleMode = AutoScaleMode.Inherit,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                BackColor = SystemColors.Control,
                Font = defaultFont,
                ClientSize = new Size(width, height),
                Owner = parentForm
            };

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
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <param name="colorHex">Set the background color for the label</param>
        /// <param name="margin">Margin (by default Padding(0, 3, 0, 3), the default value in Visual Studio)</param>
        /// <param name="valueAlignment">Value alignment, by default "Left" (see HorizontalAlignment for possible values)</param>
        /// <param name="enabled">True if it should be enabled (True by default)</param>
        /// <returns><c>NumericUpDown</c>Instance</returns>
        public static NumericUpDown CreateNumericUpDown(string name, decimal defaultValue, int positionX, int positionY, int width, int height, decimal minValue = 0, decimal maxValue = 99, int nbDecimals = 0, Control? control = null, string colorHex = "", Padding margin = new Padding(), string valueAlignment = "Left", bool enabled = true) {

            NumericUpDown numUpDown = new() {
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
                DecimalPlaces = nbDecimals,
                Enabled = enabled
            };

            if (colorHex != "") {
                numUpDown.BackColor = ColorTranslator.FromHtml(colorHex);
            }

            // If no specific margin is passed, set defaults from Visual Studio
            if (margin.All == 0) {
                margin.Top = 3;
                margin.Left = 3;
            }

            // Value alignment
            numUpDown.TextAlign = valueAlignment switch {
                "Right" => HorizontalAlignment.Right,
                "Center" => HorizontalAlignment.Center,
                _ => HorizontalAlignment.Left,
            };

            // If a Control instance is passed, add the generated element to it
            control?.Controls.Add(numUpDown);

            return numUpDown;
        }

        /// <summary>Simplified method for creating a CheckBox</summary>
        /// <param name="name">Field name</param>
        /// <param name="text">Field text</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="isCheckedByDefault">Set to true if the CheckBox has to be checked when initiated</param>
        /// <param name="checkboxOnRight">Set to true if the Checkbox has to be on the right of the text</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <returns><c>CheckBox</c>Instance</returns>
        public static CheckBox CreateCheckBox(string name, string text, int positionX, int positionY, int width, int height, bool isCheckedByDefault = false, bool checkboxOnRight = false, Control? control = null) {

            CheckBox checkBox = new() {
                Name = name,
                Text = text,
                Checked = isCheckedByDefault,
                RightToLeft = checkboxOnRight == true ? RightToLeft.Yes : RightToLeft.No,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                TabIndex = 1,
                AutoSize = false,
                Anchor = defaultAnchor,
                Margin = defaultMargin,
                Font = defaultFont
            };

            // If a Control instance is passed, add the generated element to it
            control?.Controls.Add(checkBox);

            return checkBox;
        }

        /// <summary>Simplified method for creating a tooltip</summary>
        /// <returns><c>ToolTip<c/>Instance</returns>
        public static ToolTip CreateToolTip() {

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

        /// <summary>Simplified method for creating a data grid view</summary>
        /// <param name="name">Group name</param>
        /// <param name="data">Data to show</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <returns><c>DataGridView</c>Instance</returns>
        public static DataGridView CreateDataGridView(string name, DataTable data, int positionX, int positionY, int width, int height, Control? control = null) {

            DataGridView gridView = new() {
                Name = name,
                DataSource = data,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                AutoSize = false,
                TabIndex = 1,
                Anchor = defaultAnchor,
                Font = defaultFont,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Dock = DockStyle.Fill,
                EnableHeadersVisualStyles = false,
                ReadOnly = true
            };

            // Set a specific color for the header
            gridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            // If a Control instance is passed, add the generated element to it
            control?.Controls.Add(gridView);

            return gridView;
        }

        /// <summary>Simplified method for creating a dropdown list</summary>
        /// <param name="name">Dropdown name</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <param name="dropDownWidth">Dropdown Width (in pixels, if not specified will take use the element's width as reference)</param>
        /// <param name="dropDownHeight">Dropdown Height (in pixels, if not specified will take use the element's height as reference)</param>
        /// <param name="visibleOptions">Amount of options visible without needing to scroll (will take priority over dropDownHeight parameters if specified)</param>
        /// <param name="enabled">True if it should be enabled (True by default)</param>
        /// <returns><c>ComboBox</c>Instance</returns>
        public static ComboBox CreateDropDownList(string name, int positionX, int positionY, int width, int height, Control? control = null, int dropDownWidth = 0, int dropDownHeight = 0, int visibleOptions = 0, bool enabled = true) {

            ComboBox dropDownList = new() {
                Name = name,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                DropDownStyle = ComboBoxStyle.DropDownList,
                TabIndex = 1,
                Anchor = defaultAnchor,
                Font = defaultFont,
                // If DropDownWidth was specified, use it, otherwise use the element's width
                DropDownWidth = dropDownWidth > 0 ? dropDownWidth : width,
                Enabled = enabled
            };

            /**
             * If the number of options to show without needed to scroll is specified, use it
             * Otherwise handle the DropDownHeight the same way as the DropDownWidth
             */
            if (visibleOptions > 0) {
                dropDownList.DropDownHeight = (height - 4) * visibleOptions;
            } else {
                dropDownList.DropDownHeight = dropDownHeight > 0 ? dropDownHeight : height;
            }

            // If a Control instance is passed, add the generated element to it
            control?.Controls.Add(dropDownList);

            return dropDownList;
        }

        /// <summary>Simplified method for creating a Panel</summary>
        /// <param name="name">Panel name</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="autoScroll">True if AutoScroll should be active (True by default)</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <param name="enabled">True if it should be enabled (True by default)</param>
        /// <returns><c>Panel</c>Instance</returns>
        public static Panel CreatePanel(string name, int positionX, int positionY, int width, int height, Control? control = null, bool autoScroll = true, bool enabled = true) {

            Panel panel = new() {
                Name = name,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                TabIndex = 1,
                Anchor = defaultAnchor,
                Font = defaultFont,
                AutoScroll = autoScroll,
                Enabled = enabled
            };

            control?.Controls.Add(panel);

            return panel;
        }

        /// <summary>Simplified method for creating a TextBox</summary>
        /// <param name="name">TextBox name</param>
        /// <param name="text">TextBox text</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <param name="multiLine">True if text added should take multiple lines (True by default)</param>
        /// <param name="readOnly">True if text should be readonly (True by default)</param>
        /// <returns><c>TextBox</c>Instance</returns>
        public static TextBox CreateTextBox(string name, string text, int positionX, int positionY, int width, int height, Control? control = null, bool multiLine = true, bool readOnly = true) {

            TextBox textBox = new() {
                Name = name,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                TabIndex = 1,
                Anchor = defaultAnchor,
                Font = defaultFont,
                Text = text,
                Multiline = multiLine,
                ReadOnly = readOnly
            };

            control?.Controls.Add(textBox);

            return textBox;
        }

        /// <summary>Simplified method for creating a RadioButton</summary>
        /// <param name="name">RadioButton name</param>
        /// <param name="text">RadioButton text</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <param name="tag">Tag (used to store a value if necessary)</param>
        /// <param name="isCheckedByDefault">Set to true if the CheckBox has to be checked when initiated</param>
        /// <returns><c>RadioButton</c>Instance</returns>
        public static RadioButton CreateRadioButton(string name, string text, int positionX, int positionY, int width, int height, Control? control = null, object? tag = null, bool isCheckedByDefault = false) {

            RadioButton radioBtn = new() {
                Name = name,
                Text = text,
                Checked = isCheckedByDefault,
                Tag = tag,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                TabIndex = 1,
                AutoSize = false,
                Anchor = defaultAnchor,
                Margin = defaultMargin,
                Font = defaultFont
            };

            control?.Controls.Add(radioBtn);

            return radioBtn;
        }

        /// <summary>Simplified method for creating a TabControl</summary>
        /// <param name="name">TabControl name</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <returns><c>TabControl</c>Instance</returns>
        public static TabControl CreateTabControl(string name, int positionX, int positionY, int width, int height, Control? control = null) {

            TabControl tabCtrl = new() {
                Name = name,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                TabIndex = 1,
                AutoSize = false,
                Anchor = defaultAnchor,
                Margin = defaultMargin,
                Font = defaultFont,
                // SelectedIndex = 1
            };

            control?.Controls.Add(tabCtrl);

            return tabCtrl;
        }

        /// <summary>Simplified method for creating a TabPage</summary>
        /// <param name="name">TabPage name</param>
        /// <param name="text">TabPage text</param>
        /// <param name="bgColorHex">Background color (hex code) (if none, defaults to white for visibility)</param>
        /// <param name="tabControl">TabControl instance if the element is to be attached to it directly</param>
        public static TabPage CreateTabPage(string name, string text, string bgColorHex = "", TabControl? tabControl = null) {

            TabPage tabPage = new() {
                Name = name,
                Text = text,
                TabIndex = 2,
                AutoSize = false,
                Anchor = defaultAnchor,
                Margin = defaultMargin,
                Font = defaultFont,
                BackColor = bgColorHex != "" ? ColorTranslator.FromHtml(bgColorHex) : Color.White
            };

            tabControl?.Controls.Add(tabPage);

            return tabPage;
        }

        #endregion

        #region Custom elements

        /// <summary>Simplified method for creating a label containing an image</summary>
        /// <param name="name">Label name</param>
        /// <param name="imgName">Image name</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <returns><c>Label</c>Instance</returns>
        public static Label CreateImageLabel(string name, string imgName, int positionX, int positionY, Control? control = null) {

            // Get image
            Image img = (Image)Properties.Resources.ResourceManager.GetObject(imgName);

            // Create label
            Label label = new() {
                Name = name,
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

            // If a Control instance is passed, add the generated element to it
            control?.Controls.Add(label);

            return label;
        }

        /// <summary>Simplified method for creating a label containing an image</summary>
        /// <param name="name">Label name</param>
        /// <param name="img">Image</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <returns><c>Label</c>Instance</returns>
        public static Label CreateImageLabel(string name, Image? img, int positionX, int positionY, Control? control = null) {
            Label label = new() {
                Name = name,
                Location = new Point(positionX, positionY),
                AutoSize = false,
                TabIndex = 2,
                ImageAlign = ContentAlignment.MiddleCenter,
                Anchor = defaultAnchor,
                Margin = defaultMargin,
                Font = defaultFont
            };

            if (img != null) {
                label.Size = new Size(img.Width, img.Height);
                label.Image = img;
            }

            // If a Control instance is passed, add the generated element to it
            control?.Controls.Add(label);

            return label;
        }

        /// <summary>Simplified method for creating a dropdown list that can have images next to items (options)</summary>
        /// <param name="name">Dropdown name</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <param name="dropDownWidth">Dropdown Width (in pixels, if not specified will take use the element's width as reference)</param>
        /// <param name="dropDownHeight">Dropdown Height (in pixels, if not specified will take use the element's height as reference)</param>
        /// <param name="visibleOptions">Amount of options visible without needing to scroll (will take priority over dropDownHeight parameters if specified)</param>
        /// <param name="enabled">True if it should be enabled (True by default)</param>
        /// <returns><c>ComboBox</c>Instance</returns>
        public static ImageComboBox CreateImageDropdownList(string name, int positionX, int positionY, int width, int height, Control? control = null, int dropDownWidth = 0, int dropDownHeight = 0, int visibleOptions = 0, bool enabled = true) {

            ImageComboBox dropDownList = new() {
                Name = name,
                Location = new Point(positionX, positionY),
                Size = new Size(width, height),
                DropDownStyle = ComboBoxStyle.DropDownList,
                DrawMode = DrawMode.OwnerDrawVariable,
                TabIndex = 1,
                Anchor = defaultAnchor,
                Font = defaultFont,
                // If DropDownWidth was specified, use it, otherwise use the element's width
                DropDownWidth = dropDownWidth > 0 ? dropDownWidth : width,
                Enabled = enabled
            };

            /**
             * If the number of options to show without needed to scroll is specified, use it
             * Otherwise handle the DropDownHeight the same way as the DropDownWidth
             */
            if (visibleOptions > 0) {
                dropDownList.DropDownHeight = (height - 4) * visibleOptions;
            } else {
                dropDownList.DropDownHeight = dropDownHeight > 0 ? dropDownHeight : height;
            }

            // If a Control instance is passed, add the generated element to it
            control?.Controls.Add(dropDownList);

            return dropDownList;
        }

        /// <summary>Simplified method for creating a CheckGroupBox</summary>
        /// <param name="name">Field name</param>
        /// <param name="text">Field text</param>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <param name="width">Width (in pixels)</param>
        /// <param name="height">Height (in pixels)</param>
        /// <param name="isCheckedByDefault">Set to true if the CheckGroupBox has to be checked when initiated</param>
        /// <param name="control">Control instance if the element is to be attached to it directly</param>
        /// <returns><c>CheckGroupBox</c>Instance</returns>
        public static CheckGroupBox CreateCheckGroupBox(string name, string text, int positionX, int positionY, int width, int height, bool isCheckedByDefault = false, Control? control = null) {

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

            // If a Control instance is passed, add the generated element to it
            control?.Controls.Add(checkGroupBox);

            return checkGroupBox;
        }

        #endregion

        #region Simplified adding

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

        #endregion

        #region Simplified checks

        /// <summary>Get the name of the icon corresponding to the current game</summary>
        /// <returns><c>string</c>Icon name</returns>
        public static string GetGameIconName() {
            return BokInterface.shorterGameName switch {
                "Boktai" => "lita",
                "Zoktai" => "ringo",
                "Shinbok" => "trinity",
                "LunarKnights" => "lucian",
                _ => "nero"
            };
        }

        /// <summary>Get the corresponding text alignment based on a string</summary>
        /// <param name="value">Text alignment string</param>
        /// <returns><c>System.Drawing.ContentAlignment</c>Text alignment object</returns>
        private static ContentAlignment GetTextAlignment(string value) {
            return value switch {
                "BottomCenter" => ContentAlignment.BottomCenter,
                "BottomLeft" => ContentAlignment.BottomLeft,
                "BottomRight" => ContentAlignment.BottomRight,
                "MiddleLeft" => ContentAlignment.MiddleLeft,
                "MiddleRight" => ContentAlignment.MiddleRight,
                "TopCenter" => ContentAlignment.TopCenter,
                "TopLeft" => ContentAlignment.TopLeft,
                "TopRight" => ContentAlignment.TopRight,
                _ => ContentAlignment.MiddleCenter
            };
        }

        #endregion
    }
}
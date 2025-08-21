using System;
using System.Drawing;
using System.Windows.Forms;

namespace BokInterface.Utils {
    /// <summary>Class for ImageCheckBox (PictureBox with a CheckBox)</summary>
    public class ImageCheckBox : PictureBox {

        /// <summary>CheckBox instance</summary>
        private readonly CheckBox _checkBoxInstance;
        /// <summary>Indicate if the checkbox is triggered when clicking on the image instead (True by default)</summary>
        public bool CheckWithImage = true;

        public ImageCheckBox() {
            _checkBoxInstance = new() {
                Location = new Point(8, 0),
                AutoSize = true
            };

            Controls.Add(_checkBoxInstance);
        }

        public new Image Image {
            get => base.Image;
            set {
                // Also put the CheckBox in the bottom-right corner
                base.Image = value;
                if (value != null) {
                    Size = value.Size;
                    _checkBoxInstance.Location = new Point(Width - _checkBoxInstance.Width + 2, Height - _checkBoxInstance.Height + 1);
                }
            }
        }

        /// <summary>Delegate to CheckBox.Checked</summary>
        public bool Checked {
            get { return _checkBoxInstance.Checked; }
            set { _checkBoxInstance.Checked = value; }
        }

        protected override void OnClick(EventArgs e) {
            base.OnClick(e);
            if (CheckWithImage == true) {
                _checkBoxInstance.Checked = !_checkBoxInstance.Checked;
            }
        }
    }
}

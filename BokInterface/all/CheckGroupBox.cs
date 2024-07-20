using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BokInterface.All {
    /// <summary>Class for CheckGroupBox (GroupBox with a Checkbox)</summary>
    public class CheckGroupBox : GroupBox {

        /// <summary>CheckBox instance</summary>
        private readonly CheckBox _checkBoxInstance;
        /// <summary>Event handler for checking or unchecking</summary>
        public event EventHandler? CheckedChanged;

        /// <summary>Add the CheckBox to the control</summary>
        public CheckGroupBox() {
            _checkBoxInstance = new CheckBox {
                Location = new Point(8, 0)
            };

            _checkBoxInstance.CheckedChanged += CheckBoxInstance_CheckedChanged;
            _checkBoxInstance.Layout += CheckBoxInstance_Layout;

            Controls.Add(_checkBoxInstance);
        }

        /// <summary>Keep the CheckBox text synced with our text</summary>
        public override string Text {
            get { return base.Text; }
            set {
                base.Text = _checkBoxInstance.Text = value;
                _checkBoxInstance.TabIndex = TabIndex + 1;
                _checkBoxInstance.AutoSize = true;
            }
        }

        /// <summary>Delegate to CheckBox.Checked</summary>
        public bool Checked {
            get { return _checkBoxInstance.Checked; }
            set { _checkBoxInstance.Checked = value; }
        }

        /// <summary>Enable/disable contained controls</summary>
        private void EnableDisableControls() {
            foreach (Control control in Controls) {
                if (control != _checkBoxInstance) {
                    try {
                        control.Enabled = _checkBoxInstance.Checked;
                    } catch (Exception) { }
                }
            }
        }

        /// <summary>Enable/disable contained controls</summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Events args</param>
        private void CheckBoxInstance_CheckedChanged(object sender, EventArgs e) {
            EnableDisableControls();
        }

        /// <summary>
        /// Enable/disable contained controls <br/>
        /// We do this here to set editability when the control is first loaded
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Layout events args</param>
        private void CheckBoxInstance_Layout(object sender, LayoutEventArgs e) {
            EnableDisableControls();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            // If unchecked, disable all elements within the CheckGroupBox
            if (_checkBoxInstance.Checked == false) {
                foreach (Label subElement in Controls.OfType<Label>()) {
                    subElement.Enabled = false;
                }

                foreach (NumericUpDown subElement in Controls.OfType<NumericUpDown>()) {
                    subElement.Enabled = false;
                }

                foreach (ComboBox subElement in Controls.OfType<ComboBox>()) {
                    subElement.Enabled = false;
                }

                foreach (CheckBox subElement in Controls.OfType<CheckBox>()) {
                    if (subElement == _checkBoxInstance) {
                        continue;
                    }

                    subElement.Enabled = false;
                }

                foreach (CheckGroupBox subElement in Controls.OfType<CheckGroupBox>()) {
                    subElement.Enabled = false;
                }
            }
        }
    }
}
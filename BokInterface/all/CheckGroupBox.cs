using System;
using System.Drawing;
using System.Windows.Forms;

namespace BokInterface.All {
    /// <summary>Class for CheckGroupBox (GroupBox with a Checkbox)</summary>
    public class CheckGroupBox : GroupBox {

        /// <summary>CheckBox instance</summary>
        private readonly CheckBox CheckBoxInstance;
        /// <summary>Event handler for checking or unchecking</summary>
        public event EventHandler? CheckedChanged;

        /// <summary>Add the CheckBox to the control</summary>
        public CheckGroupBox() {
            CheckBoxInstance = new CheckBox {
                Location = new Point(8, 0)
            };

            CheckBoxInstance.CheckedChanged += CheckBoxInstance_CheckedChanged;
            CheckBoxInstance.Layout += CheckBoxInstance_Layout;

            Controls.Add(CheckBoxInstance);
        }

        /// <summary>Keep the CheckBox text synced with our text</summary>
        public override string Text {
            get { return base.Text; }
            set {
                base.Text = CheckBoxInstance.Text = value;
                CheckBoxInstance.TabIndex = TabIndex + 1;
                CheckBoxInstance.AutoSize = true;
            }
        }

        /// <summary>Delegate to CheckBox.Checked</summary>
        public bool Checked {
            get { return CheckBoxInstance.Checked; }
            set { CheckBoxInstance.Checked = value; }
        }

        /// <summary>Enable/disable contained controls</summary>
        private void EnableDisableControls() {
            foreach (Control control in Controls) {
                if (control != CheckBoxInstance) {
                    try {
                        control.Enabled = CheckBoxInstance.Checked;
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
    }
}
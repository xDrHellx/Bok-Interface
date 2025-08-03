using System.Drawing;
using System.Windows.Forms;

using BokInterface.Utils;

namespace BokInterface.solarGun {
    /// <summary>Basis class for solar gun editor subclasses</summary>
    abstract class SolarGunEditor : Form {

        #region Properties

        protected readonly string name = "solarGunEditWindow";
        protected readonly string text = "Solar gun editor";

        #endregion

        #region Methods

        ///<summary>Sets common parameters for the form / subwindow</summary>
        ///<param name="width">Form width</param>
        ///<param name="height">Form height</param>
        protected void SetFormParameters(int width, int height) {
            Name = name;
            Text = text;
            AutoScaleDimensions = new SizeF(6F, 15F);
            AutoScaleMode = AutoScaleMode.Inherit;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = SystemColors.Control;
            Font = WinFormHelpers.defaultFont;
            AutoScroll = true;
            ClientSize = new Size(width, height);
            MaximizeBox = false;
        }

        /// <summary>Add elements to the subwindow</summary>
        protected abstract void AddElements();

        /// <summary>Set values to memory addresses</summary>
        protected abstract void SetValues();

        ///<summary>Sets default values for each field</summary>
        protected virtual void SetDefaultValues() { }

        #endregion
    }
}

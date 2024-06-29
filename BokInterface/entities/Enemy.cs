using System.Drawing;

namespace BokInterface.Entities {
    /// <summary>Class representing an enemy</summary>
    class Enemy {

        /// <summary>Character name</summary>
        public readonly string name;
        /// <summary>Value (decimal)</summary>
        public readonly uint value;

        /// <summary>Icon for dropdown lists</summary>
        public readonly Image? icon = null;
        /// <summary>Indicates if it's an immortal (boss)</summary>
        public readonly bool immortal;
        /// <summary>Indicates if it's a crimson enemy</summary>
        public readonly bool crimson;

        public Enemy(string name, uint value, string icon = "", bool immortal = false, bool crimson = false) {
            this.name = name;
            this.value = value;
            this.immortal = immortal;
            this.crimson = crimson;

            // If an icon was specified try getting & setting it to the property
            if (icon != "") {
                try {
                    this.icon = (Image)Properties.Resources.ResourceManager.GetObject(icon);
                } catch { }
            }
        }
    }
}
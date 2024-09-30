using System.Drawing;

namespace BokInterface.Entities {
    /// <summary>Class representing a character</summary>
    class Character {

        /// <summary>Character name</summary>
        public readonly string name;
        /// <summary>Value (decimal)</summary>
        public readonly uint value;
        /// <summary>Indicates if the character is playable or not</summary>
        public readonly bool playable;

        /// <summary>Icon for dropdown lists</summary>
        public readonly Image? icon = null;
        /// <summary>In-game mugshot</summary>
        public readonly Image? mugshot = null;

        public Character(string name, uint value, string icon = "", string mugshot = "", bool playable = false) {
            this.name = name;
            this.value = value;
            this.playable = playable;

            // If an icon was specified try getting & setting it to the property
            if (icon != "") {
                try {
                    this.icon = (Image)Properties.Resources.ResourceManager.GetObject(icon);
                } catch { }
            }

            // Do the same thing for the mugshot
            if (mugshot != "") {
                try {
                    this.mugshot = (Image)Properties.Resources.ResourceManager.GetObject(mugshot);
                } catch { }
            }
        }
    }
}
using System.Drawing;

namespace BokInterface.Weapons.Abilities {
    ///<summary>Class representing a weapon or armor ability (effect)</summary>
    class Ability {

        public readonly string name;
        public readonly decimal value;
        /// <summary>Description (usually corresponds to the in-game text</summary>
        public readonly string description;
        public readonly Image? icon;

        public Ability(string name, decimal value, string description = "", string icon = "") {

            this.name = name;
            this.value = value;
            this.description = description;

            // If an icon was specified try getting & setting it to the property
            if (icon != "") {
                try {
                    this.icon = (Image)Properties.Resources.ResourceManager.GetObject(icon);
                } catch { }
            }
        }
    }
}
using System.Drawing;

namespace BokInterface.Magics {
    ///<summary>Base class for representing a magic</summary>
    abstract class Magic {

        public string name;
        /// <summary>Magic type (Luna, Sol, Dark, ...)</summary>
        public string type;
        public string iconName;
        public Image? icon = null;
        /// <summary>Description (usually corresponds to the in-game text</summary>
        public string description;

        public Magic(string name, string type, string icon = "", string description = "") {
            this.name = name;
            this.type = type;
            this.description = description;

            // If an icon was specified try getting & setting it to the property
            iconName = icon;
            if (icon != "") {
                try {
                    this.icon = (Image)Properties.Resources.ResourceManager.GetObject(icon);
                } catch { }
            }
        }
    }
}
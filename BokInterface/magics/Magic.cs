using System.Drawing;

namespace BokInterface.Magics {
    ///<summary>Base class for representing a magic</summary>
    abstract class Magic {

        /// <summary>Magic name</summary>
        public string name;
        /// <summary>Magic type (Luna, Sol, Dark, ...)</summary>
        public string type;
        /// <summary>Magic Icon</summary>
        public Image? icon = null;
        /// <summary>Description (usually corresponds to the in-game text</summary>
        public string description;
        /// <summary>Resource library for retrieving the icon</summary>
        protected abstract string library { get; }

        public Magic(string name, string type, string icon = "", string description = "") {
            this.name = name;
            this.type = type;
            this.description = description;
            SetIconResource(icon);
        }

        /// <summary>Simplified method for setting the instance's icon via resources</summary>
        /// <param name="icon">Icon string</param>
        /// <returns><c>Image</c>Resource</returns>
        protected void SetIconResource(string iconString) {
            icon = null;
            if (iconString != "") {
                try {
                    icon = library != "" ? (Image)ResourceLoader.LoadResource(library, iconString) : (Image)Properties.Resources.ResourceManager.GetObject(iconString);
                } catch { }
            }
        }
    }
}

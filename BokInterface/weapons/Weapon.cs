using System.Drawing;

namespace BokInterface.Weapons {
    ///<summary>Base class for representing a weapon</summary>
    abstract class Weapon {

        ///<summary>Weapon name</summary>
        public string name;
        ///<summary>Value (decimal)<summary>
        public uint value;
        /// <summary>Weapon type (Sword, Gun, ...)</summary>
        public string type;
        /// <summary>Weapon icon</summary>
        public Image? icon = null;
        ///<summary>Weapon durability (if it has a +X bonus</summary>
        public int durability;
        /// <summary>SP effect the weapon can obtain (SP ability in-game)</summary>
        public string spEffect;
        /// <summary>Base weapon damage (Power in-game)</summary>
        public int baseDamage;
        /// <summary>Level required to equip</summary>
        public int level;
        /// <summary>Row when viewed in the library</summary>
        public int row;
        /// <summary>Indicates if event weapon (ex: Astro Sword)</summary>
        public bool eventWeapon;
        /// <summary>Indicates if the weapon adjusts to Django's level (Level and Power set to "??" in-game)</summary>
        public bool adjustToLevel;
        /// <summary>Price when buying</summary>
        public int buyPrice;
        /// <summary>Price when selling</summary>
        public int sellPrice;
        /// <summary>Resource library for retrieving the icon</summary>
        protected abstract string library { get; }

        public Weapon(string name, uint value, string type, string icon = "", int durability = 0, string spEffect = "", int baseDamage = 1, int level = 1, int row = 1, int buyPrice = 0, bool eventWeapon = false, bool adjustToLevel = false) {
            this.name = name;
            this.value = value;
            this.type = type;
            this.durability = durability;
            this.spEffect = spEffect;
            this.baseDamage = baseDamage;
            this.level = level;
            this.row = row;
            this.eventWeapon = eventWeapon;
            this.adjustToLevel = adjustToLevel;
            this.buyPrice = buyPrice;

            // Price for selling is always the buying price divided by 2 (or 0 if it cannot be sold)
            sellPrice = buyPrice > 0 ? buyPrice / 2 : 0;

            // If an icon was specified try getting & setting it to the property
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

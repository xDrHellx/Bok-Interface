using System.Drawing;

namespace BokInterface.Accessories {
    ///<summary>Base class for representing an accessory</summary>
    abstract class Accessory {

        ///<summary>Weapon name</summary>
        public string name;
        ///<summary>Value (decimal)<summary>
        public uint value;
        /// <summary>Accessory type (Head, Body, Hand, Foot)</summary>
        public string type;
        public Image? icon = null;
        public int row;
        /// <summary>Indicates if obtained via CrossOver points</summary>
        public bool crossOver;
        /// <summary>Price when buying</summary>
        public int buyPrice;
        /// <summary>Price when selling</summary>
        public int sellPrice;
        ///<summary>Accessory defense (refered as Durability in-game, and only used in Zoktai)</summary>
        public int defense;
        /// <summary>Accessory weight (only used in Zoktai)</summary>
        public int weight;

        public Accessory(string name, uint value, string type, string icon = "", int row = 1, int buyPrice = 0, bool crossOver = false, int defense = 0, int weight = 0) {
            this.name = name;
            this.value = value;
            this.type = type;
            this.row = row;
            this.crossOver = crossOver;
            this.defense = defense;
            this.weight = weight;

            this.buyPrice = buyPrice;

            // Price for selling is always the buying price divided by 2 (or 0 if it cannot be sold)
            sellPrice = buyPrice > 0 ? buyPrice / 2 : 0;

            // If an icon was specified try getting & setting it to the property
            if (icon != "") {
                try {
                    this.icon = (Image)Properties.Resources.ResourceManager.GetObject(icon);
                } catch { }
            }
        }
    }
}
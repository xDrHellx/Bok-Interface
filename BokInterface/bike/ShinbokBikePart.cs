namespace BokInterface.Bike {
    ///<summary>Class for representing a bike part in Shinbok</summary>
    public class ShinbokBikePart(string name, uint value, string type = "") {

        ///<summary>Weapon name</summary>
        public string name = name;
        ///<summary>Value (decimal)<summary>
        public uint value = value;
        /// <summary>Part type (Front, Tires, Body, Special, Color, Options)</summary>
        public string type = type;
    }
}
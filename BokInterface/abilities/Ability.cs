namespace BokInterface.Abilities {
    ///<summary>Class representing a weapon ability (effect)</summary>
    class Ability(string name, decimal value, string description = "") {
        ///<summary>Ability name</summary>
        public readonly string name = name;
        ///<summary>Value (decimal)<summary>
        public readonly decimal value = value;
        /// <summary>Description (usually corresponds to the in-game text</summary>
        public readonly string description = description;
    }
}
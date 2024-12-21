namespace BokInterface.Abilities {
    ///<summary>Class representing a weapon or armor ability (effect)</summary>
    class Ability(string name, decimal value, string description = "") {

        public readonly string name = name;
        public readonly decimal value = value;
        /// <summary>Description (usually corresponds to the in-game text</summary>
        public readonly string description = description;
    }
}
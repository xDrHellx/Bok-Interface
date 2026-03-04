namespace BokInterface.JunkParts {
    ///<summary>Class for representing a junk part for Bok DS / LK</summary>
    public class DsJunkPart(string name, string description = "") {
        /// <summary>Part name</summary>
        public string name = name;
        /// <summary>Description (usually corresponds to the in-game text</summary>
        public string description = description;
    }
}

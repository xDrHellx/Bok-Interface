namespace BokInterface.Weapons {
    /// <summary>Class for reprensent an attack pattern for swords in Shinbok</summary>
    class ShinbokSwordAttackPattern(string pattern, uint value) {
        /// <summary>Attack pattern</summary>
        public readonly string pattern = pattern;
        ///<summary>Value (decimal)<summary>
        public readonly uint value = value;
    }
}
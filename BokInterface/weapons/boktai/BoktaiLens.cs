namespace BokInterface.Weapons {
    /// <summary>Class for representing a Gun Lens in Boktai</summary>
    class BoktaiLens(string name, uint value, string element, string icon = "", int level = 1) : Lens(name, value, element, icon) {

        /// <summary>Lens level (1 ~ 3)</summary>
        public int level = level < 1 ? 1 : (level > 3 ? 3 : level);
        protected override string library { get => "BoktaiResources"; }
    }
}

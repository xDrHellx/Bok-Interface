namespace BokInterface.Weapons {
    /// <summary>Class for representing a Gun Lens in Shinbok</summary>
    class ShinbokLens(string name, uint value, string element, string icon = "", bool eventLens = false) : Lens(name, value, element, icon) {

        /// <summary>Indicate if obtained from event</summary>
        public bool eventLens = eventLens;
        protected override string library { get => "ShinbokResources"; }
    }
}

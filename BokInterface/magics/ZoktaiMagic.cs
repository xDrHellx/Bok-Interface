namespace BokInterface.Magics {
    ///<summary>Class representing a magic for Zoktai</summary>
    class ZoktaiMagic(string name, string type, string icon = "", string description = "") : Magic(name, type, icon, description) {
        protected override string library { get => "ZoktaiResources"; }
    }
}

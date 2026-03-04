using System.Collections.Generic;

namespace BokInterface.JunkParts {
    /// <summary>Class for Bok DS / LK junk parts instances and lists</summary>
    public class DsJunkParts {

        public Dictionary<string, DsJunkPart> All = [];

        public DsJunkParts() {
            InitFullList();
        }

        ///<summary>Init the list containing all instances for Junk Parts</summary>
        private void InitFullList() {
            All.Add("Iron", new DsJunkPart("Iron"));
            All.Add("Steel", new DsJunkPart("Steel"));
            All.Add("Solvent", new DsJunkPart("Solvent"));
            All.Add("Throttle", new DsJunkPart("Throttle"));
            All.Add("Leather", new DsJunkPart("Leather"));
            All.Add("Lens", new DsJunkPart("Lens"));
            All.Add("Adamant", new DsJunkPart("Adamant"));
            All.Add("Mythril", new DsJunkPart("Mythril"));
            All.Add("Crystal", new DsJunkPart("Crystal"));
            All.Add("Gunpowder", new DsJunkPart("Gunpowder"));
            All.Add("Convertor", new DsJunkPart("Convertor"));
            All.Add("Meteorite", new DsJunkPart("Meteorite"));
            All.Add("Lunasteel", new DsJunkPart("Lunasteel"));
            All.Add("Fang", new DsJunkPart("Fang"));
            All.Add("Generator", new DsJunkPart("Generator"));
        }

    }
}

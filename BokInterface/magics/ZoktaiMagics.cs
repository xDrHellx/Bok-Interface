using System.Collections.Generic;
using System.Linq;

namespace BokInterface.Magics {
    /// <summary>Class for Zoktai magics instances and lists</summary>
    class ZoktaiMagics {

        public Dictionary<string, Magic> Luna = [],
            Sol = [],
            Dark = [],
            Sabata = [],
            All = [];

        public ZoktaiMagics() {
            InitLunaMagics();
            InitSolMagics();
            InitDarkMagics();
            InitSabataMagics();
            InitFullList();
        }

        ///<summary>Init magic instances for Luna magics</summary>
        private void InitLunaMagics() {
            Luna.Add("Enchant Sol", new ZoktaiMagic("Enchant Sol", "Luna", "enchant_sol"));
            Luna.Add("Enchant Dark", new ZoktaiMagic("Enchant Dark", "Luna", "enchant_dark"));
            Luna.Add("Enchant Flame", new ZoktaiMagic("Enchant Flame", "Luna", "enchant_flame"));
            Luna.Add("Enchant Frost", new ZoktaiMagic("Enchant Frost", "Luna", "enchant_frost"));
            Luna.Add("Enchant Cloud", new ZoktaiMagic("Enchant Cloud", "Luna", "enchant_cloud"));
            Luna.Add("Enchant Earth", new ZoktaiMagic("Enchant Earth", "Luna", "enchant_earth"));
            Luna.Add("Transform", new ZoktaiMagic("Transform", "Luna", "transform"));
            Luna.Add("Rising Sun", new ZoktaiMagic("Rising Sun", "Luna", "rising_sun"));
        }

        ///<summary>Init magic instances for Sol magics</summary>
        private void InitSolMagics() {
            Sol.Add("Freeze", new ZoktaiMagic("Freeze", "Sol", "freeze"));
            Sol.Add("Dash", new ZoktaiMagic("Dash", "Sol", "dash"));
            Sol.Add("Healing", new ZoktaiMagic("Healing", "Sol", "healing"));
            Sol.Add("Dynamite", new ZoktaiMagic("Dynamite", "Sol", "dynamite"));
        }

        ///<summary>Init magic instances for Dark magics</summary>
        private void InitDarkMagics() {
            Dark.Add("Sleeping", new ZoktaiMagic("Sleeping", "Dark", "sleeping"));
            Dark.Add("Change Bat", new ZoktaiMagic("Change Bat", "Dark", "change_bat"));
            Dark.Add("Change Mouse", new ZoktaiMagic("Change Mouse", "Dark", "change_mouse"));
            Dark.Add("Change Wolf", new ZoktaiMagic("Change Wolf", "Dark", "change_wolf"));
        }

        ///<summary>Init magic instances for Sabata</summary>
        private void InitSabataMagics() {
            Sabata.Add("Zero Shift", new ZoktaiMagic("Zero Shift", "Sabata", "zero_shift"));
            Sabata.Add("Dark Sun", new ZoktaiMagic("Dark Sun", "Sabata", "dark_sun"));
        }

        ///<summary>Init the full list containing all magics (mostly used for editors)</summary>
        private void InitFullList() {
            All = All
                .Concat(Luna)
                .Concat(Sabata)
                .Concat(Sol)
                .Concat(Dark)
                .ToDictionary(e => e.Key, e => e.Value);
        }
    }
}
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
            Luna.Add("Enchant Sol", new Magic("Enchant Sol", "Luna", "enchant_sol"));
            Luna.Add("Enchant Dark", new Magic("Enchant Dark", "Luna", "enchant_dark"));
            Luna.Add("Enchant Flame", new Magic("Enchant Flame", "Luna", "enchant_flame"));
            Luna.Add("Enchant Frost", new Magic("Enchant Frost", "Luna", "enchant_frost"));
            Luna.Add("Enchant Cloud", new Magic("Enchant Cloud", "Luna", "enchant_cloud"));
            Luna.Add("Enchant Earth", new Magic("Enchant Earth", "Luna", "enchant_earth"));
            Luna.Add("Transform", new Magic("Transform", "Luna", "transform"));
            Luna.Add("Rising Sun", new Magic("Rising Sun", "Luna", "rising_sun"));
        }

        ///<summary>Init magic instances for Sol magics</summary>
        private void InitSolMagics() {
            Sol.Add("Freeze", new Magic("Freeze", "Sol", "freeze"));
            Sol.Add("Dash", new Magic("Dash", "Sol", "dash"));
            Sol.Add("Healing", new Magic("Healing", "Sol", "healing"));
            Sol.Add("Dynamite", new Magic("Dynamite", "Sol", "dynamite"));
        }

        ///<summary>Init magic instances for Dark magics</summary>
        private void InitDarkMagics() {
            Dark.Add("Sleeping", new Magic("Sleeping", "Dark", "sleeping"));
            Dark.Add("Change Bat", new Magic("Change Bat", "Dark", "change_bat"));
            Dark.Add("Change Mouse", new Magic("Change Mouse", "Dark", "change_mouse"));
            Dark.Add("Change Wolf", new Magic("Change Wolf", "Dark", "change_wolf"));
        }

        ///<summary>Init magic instances for Sabata</summary>
        private void InitSabataMagics() {
            Sabata.Add("Zero Shift", new Magic("Zero Shift", "Dark", "zero_shift"));
            Sabata.Add("Dark Sun", new Magic("Dark Sun", "Dark", "dark_sun"));
        }

        ///<summary>Init the full list containing all magics (mostly used for editors)</summary>
        private void InitFullList() {
            All.Add("Empty slot", new Magic("Empty slot", ""));
            All = All
                .Concat(Luna)
                .Concat(Sol)
                .Concat(Dark)
                .Concat(Sabata)
                .ToDictionary(e => e.Key, e => e.Value);
        }
    }
}
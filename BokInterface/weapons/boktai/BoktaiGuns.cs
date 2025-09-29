using System.Collections.Generic;

namespace BokInterface.Weapons {
    /// <summary>Class for Boktai gun frames & lenses instances lists</summary>
    class BoktaiGuns {

        public Dictionary<string, BoktaiLens> Lenses = [];
        public Dictionary<string, BoktaiFrame> Frames = [];
        public Dictionary<string, BoktaiBattery> Batteries = [];
        public Dictionary<string, BoktaiGrenade> Grenades = [];

        public BoktaiGuns() {
            InitLenses();
            InitFrames();
            InitBatteries();
            InitGrenades();
        }

        /// <summary>Init instances for Lenses</summary>
        private void InitLenses() {
            Lenses.Add("Sol", new BoktaiLens("Sol", 0, "sol", "sol_lens"));
            Lenses.Add("Luna", new BoktaiLens("Luna", 1, "luna", "luna_lens"));
            Lenses.Add("Flame", new BoktaiLens("Flame", 2, "flame", "flame_lens"));
            Lenses.Add("Frost", new BoktaiLens("Frost", 3, "frost", "frost_lens"));
            Lenses.Add("Cloud", new BoktaiLens("Cloud", 4, "cloud", "cloud_lens"));
            Lenses.Add("Earth", new BoktaiLens("Earth", 5, "earth", "earth_lens"));
            Lenses.Add("Dark", new BoktaiLens("Dark", 6, "dark", "dark_lens"));
            Lenses.Add("Star", new BoktaiLens("Star", 7, "", "star_lens"));
        }

        /// <summary>Init instances for Frames</summary>
        private void InitFrames() {
            string type = "Spread";
            Frames.Add("Fighter", new BoktaiFrame("Fighter", "", "E", type, "fighter"));
            Frames.Add("Knight", new BoktaiFrame("Knight", "E", "E", type, "knight"));
            Frames.Add("Crusader", new BoktaiFrame("Crusader", "C", "D", type, "crusader"));
            Frames.Add("Dragoon", new BoktaiFrame("Dragoon", "A", "C", type, "dragoon", true));

            type = "Sword";
            Frames.Add("Fencer", new BoktaiFrame("Fencer", "C", "", type, "fencer"));
            Frames.Add("Swordsman", new BoktaiFrame("Swordsman", "B", "", type, "swordsman"));
            Frames.Add("Swordmaster", new BoktaiFrame("Swordmaster", "A", "", type, "swordmaster"));
            Frames.Add("Samurai", new BoktaiFrame("Samurai", "S", "", type, "samurai", true));

            type = "Rotating";
            Frames.Add("Axel", new BoktaiFrame("Axel", "D", "", type, "axel"));
            Frames.Add("Vortex", new BoktaiFrame("Vortex", "C", "", type, "vortex"));
            Frames.Add("Tornado", new BoktaiFrame("Tornado", "B", "", type, "tornado"));
            Frames.Add("Tempest", new BoktaiFrame("Tempest", "A", "", type, "tempest", true));

            type = "Heavy";
            Frames.Add("Spear", new BoktaiFrame("Spear", "B", "C", type, "spear"));
            Frames.Add("Lance", new BoktaiFrame("Lance", "A", "B", type, "lance"));
            Frames.Add("Javelin", new BoktaiFrame("Javelin", "S", "A", type, "javelin"));
            Frames.Add("Phalanx", new BoktaiFrame("Phalanx", "S", "S", type, "phalanx", true));

            type = "Automatic";
            Frames.Add("Knife", new BoktaiFrame("Knife", "D", "E", type, "knife"));
            Frames.Add("Dagger", new BoktaiFrame("Dagger", "C", "E", type, "dagger"));
            Frames.Add("Gradius", new BoktaiFrame("Gradius", "B", "D", type, "gradius"));
            Frames.Add("Calamity", new BoktaiFrame("Calamity", "A", "D", type, "calamity", true));

            type = "Special";
            Frames.Add("Juggler", new BoktaiFrame("Juggler", "C", "C", type, "juggler"));
            Frames.Add("Wizard", new BoktaiFrame("Wizard", "B", "D", type, "wizard"));
            Frames.Add("Stalker", new BoktaiFrame("Stalker", "C", "C", type, "stalker"));
            Frames.Add("Beatmania", new BoktaiFrame("Beatmania", "E", "E", type, "beatmania"));
            Frames.Add("Guardian", new BoktaiFrame("Guardian", "B", "", type, "guardian", true));
            Frames.Add("Phantom", new BoktaiFrame("Phantom", "S", "S", type, "phantom"));
        }

        /// <summary>Init instances for Batteries</summary>
        private void InitBatteries() {
            Batteries.Add("Single", new BoktaiBattery("Single", 1, "single_battery"));
            Batteries.Add("Double", new BoktaiBattery("Double", 2, "double_battery"));
            Batteries.Add("Triple", new BoktaiBattery("Triple", 3, "triple_battery"));
            Batteries.Add("Quad", new BoktaiBattery("Quad", 4, "quad_battery"));
            Batteries.Add("Quint", new BoktaiBattery("Quint", 5, "quint_battery"));
            Batteries.Add("Infinite", new BoktaiBattery("Infinite", 999, "infinite_battery"));
            Batteries.Add("Chaos", new BoktaiBattery("Chaos", 5, "chaos_battery"));
            Batteries.Add("Astro", new BoktaiBattery("Astro", 999, "astro_battery")); // Requires unlocking in 0x0203D8B0
        }

        /// <summary>Init instances for Grenades</summary>
        private void InitGrenades() {
            // Grenades.Add("No grenade", new BoktaiGrenade("No grenade"));
            Grenades.Add("Bomb", new BoktaiGrenade("Bomb", "bomb"));
            Grenades.Add("Pineapple", new BoktaiGrenade("Pineapple", "pineapple"));
            Grenades.Add("Rising Sun", new BoktaiGrenade("Rising Sun", "rising_sun"));
            Grenades.Add("Flash", new BoktaiGrenade("Flash", "flash"));
            Grenades.Add("Scan", new BoktaiGrenade("Scan", "scan"));
            Grenades.Add("Change", new BoktaiGrenade("Change", "change"));
            Grenades.Add("Nightmare", new BoktaiGrenade("Nightmare", "nightmare"));
        }
    }
}

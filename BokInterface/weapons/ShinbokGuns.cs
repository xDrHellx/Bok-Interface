using System.Collections.Generic;

namespace BokInterface.Weapons {
    /// <summary>Class for Shinbok gun frames & lenses instances lists</summary>
    class ShinbokGuns {

        public Dictionary<string, ShinbokLens> Lenses = [];
        public Dictionary<string, ShinbokFrame> Frames = [];

        public ShinbokGuns() {
            InitLenses();
            InitFrames();
        }

        /// <summary>Init instances for Lenses</summary>
        private void InitLenses() {
            Lenses.Add("Empty slot", new ShinbokLens("Empty slot", 65535, ""));
            Lenses.Add("Sol", new ShinbokLens("Sol", 0, "sol", "sol_lens"));
            Lenses.Add("Luna", new ShinbokLens("Luna", 1, "luna", "luna_lens"));
            Lenses.Add("Flame", new ShinbokLens("Flame", 2, "flame", "flame_lens"));
            Lenses.Add("Frost", new ShinbokLens("Frost", 3, "frost", "frost_lens"));
            Lenses.Add("Cloud", new ShinbokLens("Cloud", 4, "cloud", "cloud_lens"));
            Lenses.Add("Earth", new ShinbokLens("Earth", 5, "earth", "earth_lens"));
            Lenses.Add("Star", new ShinbokLens("Star", 6, "", "star_lens"));
            Lenses.Add("Astro", new ShinbokLens("Astro", 7, "sol", "astro_lens", true));
            Lenses.Add("Dark", new ShinbokLens("Dark", 8, "dark", "dark_lens"));
        }

        /// <summary>Init instances for Frames</summary>
        private void InitFrames() {
            Frames.Add("Empty slot", new ShinbokFrame("Empty slot", 0, "", ""));
            Frames.Add("Fighter", new ShinbokFrame("Fighter", 1, "C", "S", "fighter"));
            Frames.Add("Calamity", new ShinbokFrame("Calamity", 2, "C", "A", "calamity"));
            Frames.Add("Wizard", new ShinbokFrame("Wizard", 3, "B", "B", "wizard"));
            Frames.Add("Bubbles", new ShinbokFrame("Bubbles", 4, "B", "A", "bubbles"));
            Frames.Add("Hoop", new ShinbokFrame("Hoop", 5, "B", "A", "hoop"));
            Frames.Add("Juggler", new ShinbokFrame("Juggler", 6, "B", "A", "juggler"));
            Frames.Add("Stalker", new ShinbokFrame("Stalker", 7, "A", "B", "stalker"));
            Frames.Add("Dragoon", new ShinbokFrame("Dragoon", 8, "S", "A", "dragoon"));
            Frames.Add("Samurai", new ShinbokFrame("Samurai", 9, "B", "B", "samurai"));
            Frames.Add("Bomber", new ShinbokFrame("Bomber", 10, "S", "C", "bomber"));
            Frames.Add("Tempest", new ShinbokFrame("Tempest", 11, "C", "B", "tempest"));
            Frames.Add("Beatmania", new ShinbokFrame("Beatmania", 12, "D", "S", "beatmania"));
            // Frames.Add("Phantom", new ShinbokFrame("Phantom", 13, "", "", "phantom")); // Disabled as it causes the game to crash as soon as in Django's inventory
        }
    }
}
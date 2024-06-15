using System;

using BizHawk.Client.Common;
using BizHawk.Client.GBAHawk;

namespace BokInterface.All {

    /// <summary>Main class for BizHawk APIs</summary>
    public static class APIs {

        // Hack-ish way to make these APIs available outside of the main tool form
        private static IMainFormForTools? s_mainFormForTools;
        private static ApiContainer? s_apiContainer;
        private static IEmuClientApi? s_clientApi;
        private static IEmulationApi? s_emulationApi;
        // private static IGameInfoApi? gameInfoApi; // deprecated
        private static ISaveStateApi? s_saveStateApi;
        private static IMemoryApi? s_memoryApi;

        private static IGuiApi? s_guiApi;

        public static IMainFormForTools MainFormForTools => Require(s_mainFormForTools);
        public static MainForm MainForm => (MainForm)MainFormForTools;

        public static ApiContainer ApiContainer => Require(s_apiContainer);

        // These seem to always be available even when no ROM is loaded
        public static IEmuClientApi Client => Require(s_clientApi);
        public static IEmulationApi Emulation => Require(s_emulationApi);
        // public static IGameInfoApi GameInfo => Require(gameInfoApi); // deprecated
        public static ISaveStateApi SaveState => Require(s_saveStateApi);
        public static IMemoryApi Memory => Require(s_memoryApi);
        public static IGuiApi Gui => Require(s_guiApi);

        public static Config Config => ((EmulationApi)Emulation).ForbiddenConfigReference;

        internal static void Update(ApiContainer container) {

            s_apiContainer = container;
            Fill(out s_clientApi);
            Fill(out s_emulationApi);
            // Fill(out gameInfoApi); // deprecated
            Fill(out s_saveStateApi);
            Fill(out s_memoryApi);
            Fill(out s_guiApi);

            static void Fill<T>(out T? field) where T : class, IExternalApi {
                if (s_apiContainer.Libraries.TryGetValue(typeof(T), out var api)) {
                    field = api as T;
                } else {
                    field = null;
                }
            }
        }

        internal static void Update(IMainFormForTools mainForm) {
            s_mainFormForTools = mainForm;
        }

        private static T Require<T>(T? value) where T : class {
            return value ?? throw new InvalidOperationException($"{typeof(T).Name} is not available. Accessed before tool has been initialized?");
        }

        public static bool LoadRom(string path) {

            // Copy what the OpenRom API does because `IEmuClientApi.OpenRom` does not return the success bool
            // https://github.com/TASVideos/BizHawk/blob/b8f5050d6c426ba81ec1b1e1265b9b6cb9a40d3a/src/BizHawk.Client.Common/Api/Classes/EmuClientApi.cs#L141
            return MainFormForTools.LoadRom(
                path,
                new LoadRomArgs { OpenAdvanced = OpenAdvancedSerializer.ParseWithLegacy(path) }
            );
        }
    }
}
using BokInterface.Addresses;

namespace BokInterface.KeyItems {
    /// <summary>Key items editor for Lunar Knights / Boktai DS</summary>
    class LunarKnightsKeyItemsEditor : KeyItemsEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly LunarKnightsAddresses _lunarKnightsAddresses;

        #endregion

        #region Constructor

        public LunarKnightsKeyItemsEditor(BokInterface bokInterface, MemoryValues memoryValues, LunarKnightsAddresses LunarKnightsAddresses) {

            _memoryValues = memoryValues;
            _lunarKnightsAddresses = LunarKnightsAddresses;
            Owner = _bokInterface = bokInterface;
            Icon = _bokInterface.Icon;

            SetFormParameters(400, 400);
            AddElements();
            Show();
        }

        #endregion

        #region Elements

        protected override void AddElements() { }

        #endregion

        #region Values setting

        protected override void SetValues() { }

        #endregion
    }
}

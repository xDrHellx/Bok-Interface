using BokInterface.Addresses;

namespace BokInterface.KeyItems {
    /// <summary>Key items editor for Lunar Knights / Boktai DS</summary>
    class DsKeyItemsEditor : KeyItemsEditor {

        #region Properties

        private readonly MemoryValues _memoryValues;
        private readonly BokInterface _bokInterface;
        private readonly DsAddresses _lunarKnightsAddresses;

        #endregion

        #region Constructor

        public DsKeyItemsEditor(BokInterface bokInterface, MemoryValues memoryValues, DsAddresses LunarKnightsAddresses) {

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

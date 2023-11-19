using System.ComponentModel;
using BizHawk.Client.Common;
using BokInterface.All;

/**
 * File for the GUI part of the Bok interface
 */

namespace BokInterface {

	partial class BokInterfaceMainForm {
        
		#region GUI related code

        protected int screenWidth;
        protected int screenHeight;

        /// <summary>Shows the indicator, to tell that if interface is ON or OFF</summary>
        private void ShowInterfaceIndicator() {
            
            // screenWidth = APIs.Client.ScreenWidth();
            // screenHeight = APIs.Client.ScreenHeight();

            APIs.Gui.Text(3, 1, interfaceActivated == true ? "Bok ON" : "Bok Off", System.Drawing.Color.Orange, "bottomright");
        }

		#endregion
	}
}
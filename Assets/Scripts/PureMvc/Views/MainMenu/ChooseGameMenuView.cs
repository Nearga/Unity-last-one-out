using System;
using UnityEngine.UI;

namespace LastOneOut
{
	[ResourceObject("Prefabs/Views/MainMenu/ChooseGameMenuView")]
	public class ChooseGameMenuView : BaseView<ChooseGameMenuView>
	{
		public Button HotseatButton;

		public Button VsBotButton;

		public Button WatchBotButton;

		public Button BackButton;


		override protected Type GetMediatorType() { return typeof(ChooseGameMenuMediator); }
	}
}
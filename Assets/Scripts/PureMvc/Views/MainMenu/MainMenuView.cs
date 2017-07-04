using System;
using UnityEngine.UI;

namespace LastOneOut
{
	[ResourceObject("Prefabs/Views/MainMenu/MainMenuView")]
	public class MainMenuView : BaseView<MainMenuView>
	{
		public Button StartButton;
				
		public Button ExitButton;


		override protected Type GetMediatorType() { return typeof(MeinMenuMediator); }
	}
}
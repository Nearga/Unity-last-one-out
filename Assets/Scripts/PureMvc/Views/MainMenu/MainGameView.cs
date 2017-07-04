using System;
using UnityEngine.UI;

namespace LastOneOut
{
	[ResourceObject("Prefabs/Views/MainMenu/MainGameView")]
	public class MainGameView : BaseView<MainGameView>
	{
		public Button StartButton;
				
		public Button ExitButton;


		override protected Type GetMediatorType() { return typeof(StartGameMediator); }
	}
}
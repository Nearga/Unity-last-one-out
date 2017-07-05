using PureMVC.Core;
using System;
using UnityEngine.UI;

namespace LastOneOut
{
	[UnitySingleton("Prefabs/Views/MainMenu/MainMenuView")]
	public class MainMenuView : BaseView<MainMenuView>
	{
		public Button StartButton;
				
		public Button ExitButton;


		public override void OnEnable()
		{
			view = View.GetInstance(() => new View());
			base.OnEnable();
		}

		override protected Type GetMediatorType() { return typeof(MeinMenuMediator); }
	}
}
using PureMVC.Core;
using System;
using UnityEngine.UI;

namespace LastOneOut
{
	[ResourceObject("Prefabs/Views/MainMenu/ChooseGameMenuView")]
	public class ChooseGameMenuView : BaseView<ChooseGameMenuView>
	{
		public Button PlayerVsPlayerButton;

		public Button PlayerVsBotButton;

		public Button BotVsPlayerButton;

		public Button ButVsBotButton;

		public Button BackButton;


		public override void OnEnable()
		{
			view = View.GetInstance(() => new View());
			base.OnEnable();
		}


		override protected Type GetMediatorType() { return typeof(ChooseGameMenuMediator); }
	}
}
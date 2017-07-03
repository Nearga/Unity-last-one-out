using System;
using UnityEngine;
using UnityEngine.UI;

namespace LastOneOut
{
	[ResourceObject("Prefabs/Views/ChooseGameView")]
	public class ChooseGameView : BaseView<ChooseGameView>
	{
		public Button HotseatButton;

		public Button VsBotButton;

		public Button WatchBotButton;

		public Button BackButton;

		override protected Type GetMediatorType()
		{
			return typeof(ChooseGameMediator);
		}
	}
}
using System;
using UnityEngine;
using UnityEngine.UI;

namespace LastOneOut
{
	[ResourceObject("Prefabs/Views/StartGameView")]
	public class StartGameView : BaseView<StartGameView>
	{
		public Button StartButton;
				
		public Button ExitButton;

		override protected Type GetMediatorType()
		{
			return typeof(StartGameMediator);
		}
	}
}
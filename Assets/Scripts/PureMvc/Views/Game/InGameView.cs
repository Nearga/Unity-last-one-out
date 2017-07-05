using PureMVC.Core;
using System;
using UnityEngine.UI;

namespace LastOneOut
{
	[ResourceObject("Prefabs/Views/Game/InGameView")]
	public class InGameView : BaseView<InGameView>
	{
		public Button MainMenuButton;


		public override void OnEnable()
		{
			view = View.GetInstance(() => new View());
			base.OnEnable();
		}

		override protected Type GetMediatorType() { return typeof(InGameMediator); }
	}
}
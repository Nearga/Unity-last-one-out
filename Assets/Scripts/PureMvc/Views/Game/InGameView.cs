using System;
using UnityEngine.UI;

namespace LastOneOut
{
	[ResourceObject("Prefabs/Views/Game/InGameView")]
	public class InGameView : BaseView<InGameView>
	{
		public Button MainMenuButton;


		override protected Type GetMediatorType() { return typeof(InGameMediator); }
	}
}
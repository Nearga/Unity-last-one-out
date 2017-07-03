using UnityEngine;

namespace LastOneOut
{
	[ResourceObject("Prefabs/Views/ChooseGameView")]
	public class ChooseGameView : BaseView<ChooseGameView>
	{
		[SerializeField]
		MvcButton hotseatButton;

		[SerializeField]
		MvcButton vsBotButton;

		[SerializeField]
		MvcButton watchBotButton;

		[SerializeField]
		MvcButton backButton;
	}
}
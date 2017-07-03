using UnityEngine;

namespace LastOneOut
{
	[ResourceObject("Prefabs/Views/StartGameView")]
	public class StartGameView : BaseView<StartGameView>
	{
		[SerializeField]
		MvcButton startButton;

		[SerializeField]
		MvcButton exitButton;		
	}
}
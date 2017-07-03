using UnityEngine;

namespace LastOneOut
{
	public class ExitGameMediator : BaseMediator
	{
		public ExitGameMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent) { }
			

		public override void OnButtonClickedAction()
		{
			Debug.Log("Application.Quit");
			Application.Quit();
		}
	}
}

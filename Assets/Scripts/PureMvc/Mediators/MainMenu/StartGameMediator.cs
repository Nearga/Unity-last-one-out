using UnityEngine;

namespace LastOneOut
{
	public class StartGameMediator : BaseMediator
	{
		public StartGameMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent) { }


		public override void OnButtonClickedAction()
		{
			Debug.Log("Clicked");

			SendNotification(Notifications.Navigate, typeof(ChooseGameView));
		}
	}
}
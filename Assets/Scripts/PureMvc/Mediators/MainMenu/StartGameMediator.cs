using System;
using UnityEngine;

namespace LastOneOut
{
	public class StartGameMediator : BaseMediator
	{
		public StartGameMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent) { }

		private MainGameView startGameView;

		public override void OnRegister()
		{
			// View should be GameObject...
			var asGameObject = ViewComponent as GameObject; // StartGameView;
			if (asGameObject == null)
				Debug.LogException(new Exception("ViewComponent is not a GameObject"));

			// ... and StartGameView
			startGameView = asGameObject.GetComponent<MainGameView>();
			if (startGameView == null)
				Debug.LogException(new Exception("ViewComponent is not a StartGameView"));

			// Add buttons listeners
			startGameView.StartButton.RemoveListenersAndSubscribe(OnStartClicked);
			startGameView.ExitButton.RemoveListenersAndSubscribe(OnExitClicked);
		}

		void OnStartClicked()
		{
			Debug.Log("OnStartClicked");
			SendNotification(Notifications.Navigate, typeof(ChooseGameView));
		}

		void OnExitClicked()
		{
			Debug.Log("OnExitClicked");
			Application.Quit();
		}
	}
}
using System;
using UnityEngine;

namespace LastOneOut
{
	public class ChooseGameMenuMediator : BaseMediator
	{
		public ChooseGameMenuMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent) { }

		private ChooseGameMenuView chooseGameView;

		public override void OnRegister()
		{
			// View should be GameObject...
			var asGameObject = ViewComponent as GameObject; // StartGameView;
			if (asGameObject == null)
				Debug.LogException(new Exception("ViewComponent is not a GameObject"));

			// ... and StartGameView
			chooseGameView = asGameObject.GetComponent<ChooseGameMenuView>();
			if (chooseGameView == null)
				Debug.LogException(new Exception("ViewComponent is not a StartGameView"));

			// Add buttons listeners
			chooseGameView.HotseatButton.RemoveListenersAndSubscribe(OnHotseatClicked);
			chooseGameView.VsBotButton.RemoveListenersAndSubscribe(OnVsBotClicked);
			chooseGameView.WatchBotButton.RemoveListenersAndSubscribe(OnWatchBotClicked);
			chooseGameView.BackButton.RemoveListenersAndSubscribe(OnBackClicked);
		}

		void OnHotseatClicked()
		{
			Debug.Log("OnHotseatClicked");
			SendNotification(Notifications.Navigate, typeof(InGameView));
		}

		void OnVsBotClicked()
		{

		}

		void OnWatchBotClicked()
		{

		}

		void OnBackClicked()
		{
			Debug.Log("OnBackClicked");
			SendNotification(Notifications.Navigate, typeof(MainMenuView));
		}
	}
}
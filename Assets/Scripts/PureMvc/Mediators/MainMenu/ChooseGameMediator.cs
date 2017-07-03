using UnityEngine;
using System.Collections;
using System;

namespace LastOneOut
{
	public class ChooseGameMediator : BaseMediator
	{
		public ChooseGameMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent) { }

		private ChooseGameView chooseGameView;

		public override void OnRegister()
		{
			// View should be GameObject...
			var asGameObject = ViewComponent as GameObject; // StartGameView;
			if (asGameObject == null)
				Debug.LogException(new NotSupportedException("ViewComponent is not a GameObject"));

			// ... and StartGameView
			chooseGameView = asGameObject.GetComponent<ChooseGameView>();
			if (chooseGameView == null)
				Debug.LogException(new NotSupportedException("ViewComponent is not a StartGameView"));

			// Add buttons listeners
			chooseGameView.HotseatButton.RemoveListenersAndSubscribe(OnHotseatClicked);
			chooseGameView.VsBotButton.RemoveListenersAndSubscribe(OnVsBotClicked);
			chooseGameView.WatchBotButton.RemoveListenersAndSubscribe(OnWatchBotClicked);
			chooseGameView.BackButton.RemoveListenersAndSubscribe(OnBackClicked);
		}

		void OnHotseatClicked()
		{

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
			SendNotification(Notifications.Navigate, typeof(StartGameView));
		}
	}
}
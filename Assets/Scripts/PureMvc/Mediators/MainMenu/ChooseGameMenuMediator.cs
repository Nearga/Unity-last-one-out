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
			chooseGameView.PlayerVsPlayerButton.RemoveListenersAndSubscribe(PvPClicked);
			chooseGameView.PlayerVsBotButton.RemoveListenersAndSubscribe(PvEClicked);
			chooseGameView.BotVsPlayerButton.RemoveListenersAndSubscribe(EvPClicked);
			chooseGameView.ButVsBotButton.RemoveListenersAndSubscribe(EvEClicked);
			chooseGameView.BackButton.RemoveListenersAndSubscribe(BackClicked);

			// WIP. Not sure i'd have time to implement it, but it can be implemented fast enough.
			chooseGameView.PlayerVsBotButton.interactable = false;
			chooseGameView.BotVsPlayerButton.interactable = false;
			chooseGameView.ButVsBotButton.interactable = false;
		}

		#region Events

		void PvPClicked()
		{
			Debug.Log("PvPClicked");			
			SendStartGameNotification(GameType.PvP);
		}

		void PvEClicked()
		{
			Debug.Log("PvEClicked");
			SendStartGameNotification(GameType.PvE);
		}

		void EvPClicked()
		{
			Debug.Log("EvPClicked");
			SendStartGameNotification(GameType.EvP);
		}

		void EvEClicked()
		{
			Debug.Log("EvEClicked");
			SendStartGameNotification(GameType.EvE);
		}

		void BackClicked()
		{
			Debug.Log("BackClicked");
			SendLoadViewNotification(typeof(MainMenuView));
		}

		#endregion
	}
}
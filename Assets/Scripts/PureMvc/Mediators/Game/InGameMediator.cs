using System;
using System.Linq;
using PureMVC.Interfaces;
using UnityEngine;
using System.Collections.Generic;

namespace LastOneOut
{
	public class InGameMediator : BaseMediator
	{
		public InGameMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent) { }

		private InGameView inGameView;

		public override string[] ListNotificationInterests()
		{
			var list = base.ListNotificationInterests().ToList();
			list.AddRange(new List<string> {
				Notifications.StartGame,
				Notifications.StartNewRound,
				Notifications.SyncItemsState,
				Notifications.PointerClicked,
				Notifications.PointerEnter,
				Notifications.PointerExit  });
			return list.ToArray();
		}

		public override void OnRegister()
		{
			// View should be GameObject ...
			var asGameObject = ViewComponent as GameObject; // StartGameView;
			if (asGameObject == null)
				Debug.LogException(new Exception("ViewComponent is not a GameObject"));

			// ... and InGameView
			inGameView = asGameObject.GetComponent<InGameView>();
			if (inGameView == null)
				Debug.LogException(new Exception("ViewComponent is not a StartGameView"));

			// Add buttons listeners
			inGameView.MainMenuButton.RemoveListenersAndSubscribe(OnMainMenuClicked);			
		}

		#region Events

		void OnMainMenuClicked()
		{
			Debug.Log("OnMainMenuClicked");
			//SendNotification(Notifications.NavigateTo, typeof(MainMenuView), null);
			SendLoadViewNotification(typeof(MainMenuView));
		}

		#endregion

		#region Notifications

		public override void HandleNotification(INotification notification)
		{
			base.HandleNotification(notification);
			//Debug.Log("Handling " + notification.Name);

			switch (notification.Name)
			{
				case Notifications.StartGame:
					HandleStartGameNotification();
					break;
				case Notifications.SyncItemsState:
					HandleSyncItemsState();
					break;
				case Notifications.StartNewRound:
					HandleStartNewRound();
					break;
				default:
					break;
			}

		}

		private void HandleStartGameNotification()
		{
			inGameView.GenerateItems();
			SendNotification(Notifications.StartNewRound);
		}
		
		private void HandleSyncItemsState()
		{
			inGameView.SyncItems();
		}

		private void HandleStartNewRound()
		{
			var gameState = UnityFacade.GetInstance().RetrieveProxy<GameStateProxy>();
			inGameView.SetItemsLeftText(gameState.ItemsLeft);
			inGameView.SetRoundNumber(gameState.CurrentRoundNumber, gameState.GameState);
			inGameView.SetPlayerTurnText(gameState.IsFirstPlayerRound());
		}

		#endregion
	}
}
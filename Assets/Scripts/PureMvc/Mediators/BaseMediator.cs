using PureMVC.Core;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LastOneOut
{
	// Handles Notifications.Navigate
	public abstract class BaseMediator : Mediator
	{
		public BaseMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent) { }

		public BaseMediator(string mediatorName, GameObject viewComponent) : base(mediatorName, viewComponent) { }


		private Dictionary<GameType, Action> GameTypeToInitAction = new Dictionary<GameType, Action>
		{
			{
				GameType.PvP, () => 
				{
					var humanInputController = ((HumanInputController)(Controller.GetInstance(() => new HumanInputController())));
					humanInputController.SetControllerPlayerType(new List<PlayerTurn> { PlayerTurn.FirstPlayer, PlayerTurn.SecondPlayer });
					var aiInputController = ((AiInputController)(Controller.GetInstance(() => new AiInputController())));
					aiInputController.SetControllerPlayerType(null);
				}
			}

		};


		public override string[] ListNotificationInterests()
		{
			var list = base.ListNotificationInterests().ToList();
			list.Add(Notifications.NavigateTo);
			return list.ToArray();
		}

		public override void HandleNotification(INotification notification)
		{
			var type = ((Type)notification.Body);
			

			// Hide current View
			var viewGO = ViewComponent as GameObject;
			//var view = viewGO.GetComponent<ViewComponent>(); 
			viewGO.SetActive(false);


			// Load new View
			if (type == typeof(MainMenuView))
			{
				if (SceneManager.GetActiveScene().name != Constants.MainMenuScene)
					SceneManager.LoadScene(Constants.MainMenuScene, LoadSceneMode.Single);

				var newView = MainMenuView.Instance;
				newView.gameObject.SetActive(true);
			}
			if (type == typeof(ChooseGameMenuView))
			{
				if (SceneManager.GetActiveScene().name != Constants.MainMenuScene)
					SceneManager.LoadScene(Constants.MainMenuScene, LoadSceneMode.Single);

				var newView = ChooseGameMenuView.Instance;
				newView.gameObject.SetActive(true);
			}
			if (type == typeof(InGameView))
			{
				SceneManager.LoadScene(Constants.GameScene, LoadSceneMode.Single);

				var newView = InGameView.Instance;
				newView.gameObject.SetActive(true);

				var gameType = (GameType)Enum.Parse(typeof(GameType), notification.Type);
				InitGameControllers(gameType);
			}
		}

		protected void SendStartGameNotification(GameType gameType)
		{
			SendNotification(Notifications.NavigateFrom);
			SendNotification(Notifications.NavigateTo, typeof(InGameView), gameType.ToString());
		}

		void InitGameControllers(GameType gameType)
		{
			GameTypeToInitAction[gameType].Invoke();
		}


	}
}
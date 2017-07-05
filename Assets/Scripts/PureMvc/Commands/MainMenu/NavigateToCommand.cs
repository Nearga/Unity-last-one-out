using PureMVC.Core;
using PureMVC.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace LastOneOut
{
	public class NavigateToCommand : BaseCommand
	{
		private Dictionary<GameType, Action> GameTypeToInitAction = new Dictionary<GameType, Action>
		{
			{
				GameType.PvP, () =>
				{
					//HumanInputController.GetInstance(() => new HumanInputController());

					var humanInputController = ((HumanInputController)(HumanInputController.GetInstance(() => new HumanInputController())));
					humanInputController.SetControllerPlayerType(new List<PlayerTurn> { PlayerTurn.FirstPlayer, PlayerTurn.SecondPlayer });
					var aiInputController = ((AiInputController)(AiInputController.GetInstance(() => new AiInputController())));
					aiInputController.SetControllerPlayerType(null);
				}
			}
		};

		public override void Execute(INotification notification)
		{
			//Debug.Log("NavigateToCommand.Execute");

			var type = ((Type)notification.Body);

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

		void InitGameControllers(GameType gameType)
		{
			GameTypeToInitAction[gameType].Invoke();
		}
	}
}

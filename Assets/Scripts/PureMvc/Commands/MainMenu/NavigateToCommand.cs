using PureMVC.Interfaces;
using System;
using UnityEngine.SceneManagement;

namespace LastOneOut
{
	public class NavigateToCommand : BaseCommand
	{
		public override void Execute(INotification notification)
		{
			//Debug.Log("NavigateToCommand.Execute");
			var type = ((Type)notification.Body);

			// Load new View. If necessary, load next scene as well. For the Game view, send notification about game start
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
				var stateProxy = UnityFacade.GetInstance().RetrieveProxy<GameStateProxy>();
				stateProxy.StartNewGame(gameType);

				Timer.Instance.Once(0.1f, () => SendNotification(Notifications.StartGame));	// A bit weird solution. There should be loading screen, SceneLoadedEvent, etc.
			}
		}
	}
}

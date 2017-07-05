using PureMVC.Patterns.Proxy;
using System.Collections.Generic;
using System.Linq;

namespace LastOneOut
{
	public enum GameType
	{
		PvP,
		PvE,
		EvP,
		EvE
	}


	public class GameStateProxy : Proxy
	{
		public GameStateProxy() : base("GameStateProxy") { }
		public GameStateProxy(string name) : base(name) { }

		public GameStateModel GameStateModel { get; private set; }

		public override void OnRegister()
		{
			GameStateModel = new GameStateModel();
		}

		public void StartNewGame(GameType gameType = GameType.PvE)
		{
			var gameSettingsProxy = UnityFacade.GetInstance().RetrieveProxy<GameSettingsProxy>();

			GameStateModel.CurrentTurnNumber = 0;
			GameStateModel.GameType = gameType; // Default is PvE

			GameStateModel.ItemStatuses = new List<ItemStatus>(gameSettingsProxy.GameSettings.TotalItems);
			for (int i = 0; i < gameSettingsProxy.GameSettings.TotalItems; i++)
			{
				ItemStatus status = new ItemStatus()
				{
					Id = i,
					ItemState = ItemState.OnBoard
				};
				GameStateModel.ItemStatuses.Add(status);
			}			
		}
	}
}

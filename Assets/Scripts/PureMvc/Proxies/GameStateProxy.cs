using PureMVC.Patterns.Proxy;

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
			GameStateModel.CurrentTurnNumber = 0;
			GameStateModel.GameType = gameType; // Default if PvE
		}
	}
}

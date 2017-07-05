using System.Collections.Generic;

namespace LastOneOut
{
	public enum GameType
	{
		PvP,
		PvE,
		EvP,
		EvE
	}

	public enum PlayerTurn
	{
		FirstPlayer,
		SecondPlayer
	}

	public class BaseInputController : BaseController
	{
		public List<PlayerTurn> PlayerTurns { get; private set;}

		protected override void InitializeController()
		{
			base.InitializeController();

			RegisterCommand(Notifications.StartGame, () => new StartGameCommand());
		}

		public void SetControllerPlayerType(IEnumerable<PlayerTurn> playerTurns)
		{
			PlayerTurns = new List<PlayerTurn>(playerTurns);
		}
	}
}
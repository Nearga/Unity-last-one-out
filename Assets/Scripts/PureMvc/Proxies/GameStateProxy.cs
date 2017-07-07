using PureMVC.Patterns.Proxy;
using System.Collections.Generic;
using System.Linq;

namespace LastOneOut
{
	public enum GameState
	{
		Started, 
		Player1Won,
		Player2Won,
		Stopped
	}

	public enum GameType
	{
		PvP,
		PvE,
		EvP,
		EvE
	}

	public enum RoundModulo
	{
		Even,
		Odd
	}

	// No logic here. Only init and accessors.
	public class GameStateProxy : Proxy
	{
		public GameStateProxy() : base("GameStateProxy") { }
		public GameStateProxy(string name) : base(name) { }

		protected GameStateModel GameStateModel { get; private set; }

		private GameSettingsProxy gameSettingsProxy;
		private GameState gameState;

		public override void OnRegister()
		{
			GameStateModel = new GameStateModel();
		}
		

		#region Is AI round

		// Key: Tuple that contains game type and is round even. Value: True if AI input is expected, False if user input is expected.
		private Dictionary<Tuple<GameType, RoundModulo>, bool> roundIsAiInput = new Dictionary<Tuple<GameType, RoundModulo>, bool>
		{
			{ Tuple.New(GameType.PvP, RoundModulo.Even), false },
			{ Tuple.New(GameType.PvP, RoundModulo.Odd), false },

			{ Tuple.New(GameType.PvE, RoundModulo.Even), true },
			{ Tuple.New(GameType.PvE, RoundModulo.Odd), false },

			{ Tuple.New(GameType.EvP, RoundModulo.Even), false },
			{ Tuple.New(GameType.EvP, RoundModulo.Odd), true },

			{ Tuple.New(GameType.EvE, RoundModulo.Even), true },
			{ Tuple.New(GameType.EvE, RoundModulo.Odd), true },
		};

		#endregion

		public IEnumerable<ItemModel> ItemModelsList
		{
			get
			{
				return GameStateModel.ItemModels;
			}
			set
			{
				foreach (var item in GameStateModel.ItemModels)
				{
					var updatedItem = value.FirstOrDefault(i => i.Id == item.Id);
					if (updatedItem != null)
						item.ItemState = updatedItem.ItemState;
				}

				SendNotification(Notifications.SyncItemsState);
			}
		}

		public int CurrentRoundNumber
		{
			get { return GameStateModel.CurrentRoundNumber; }
			private set { GameStateModel.CurrentRoundNumber = value; }
		}

		public int ItemsLeft
		{
			get { return ItemModelsList.Where(i => i.ItemState != ItemState.OffBoard).Count(); }
		}

		public GameState GameState
		{
			private set { gameState = value; }
			get { return gameState; }
		}


		public void StartNewGame(GameType gameType = GameType.PvE)
		{
			gameSettingsProxy = UnityFacade.GetInstance().RetrieveProxy<GameSettingsProxy>();

			GameState = GameState.Started;

			GameStateModel.CurrentRoundNumber = 0;
			GameStateModel.GameType = gameType; // Default is PvE

			// Init the items states and ids
			GameStateModel.ItemModels = new List<ItemModel>(gameSettingsProxy.GameSettings.TotalItems);
			for (int i = 0; i < gameSettingsProxy.GameSettings.TotalItems; i++)
			{
				ItemModel status = new ItemModel()
				{
					Id = i,
					ItemState = ItemState.OnBoard
				};
				GameStateModel.ItemModels.Add(status);
			}
		}
		
		public void StartNewRound()
		{
			if (ItemsLeft > 0)
			{
				GameStateModel.CurrentRoundNumber++;
				return;
			}

			gameState = gameSettingsProxy.GameSettings.IsLastWinner
				? (IsFirstPlayerRound() ? GameState.Player2Won : GameState.Player1Won) // Fastfix. Fix properly later.
				: (IsFirstPlayerRound() ? GameState.Player1Won : GameState.Player2Won);
		}

		/// <summary>
		/// Defines, which player's round is it now.
		/// </summary>
		/// <returns>True if its first player turn. False if its second player turn</returns>
		public bool IsFirstPlayerRound()
		{
			return RoundOddness() == RoundModulo.Even ? true : false;
		}
		
		/// <summary>
		/// Defines, is game is listening to player or AI in the current round.
		/// </summary>
		/// <returns>True, if AI should make a move. False, if player should move.</returns>
		public bool IsAiRound()
		{
			var isAiRound = roundIsAiInput[new Tuple<GameType, RoundModulo>(GameStateModel.GameType, RoundOddness())];

			//Debug.LogFormat("This is {0} round. It is {1} (even/odd). Due to {2} game type, IsAiRound is {3}", GameStateModel.CurrentRoundNumber, roundModulo, GameStateModel.GameType, isAiRound);
			return isAiRound;
		}

		private RoundModulo RoundOddness()
		{
			var roundModulo = (GameStateModel.CurrentRoundNumber % 2 == 0) ? RoundModulo.Even : RoundModulo.Odd;
			return roundModulo;
		}
	}
}

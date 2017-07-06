using PureMVC.Patterns.Proxy;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LastOneOut
{
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

	public class GameStateProxy : Proxy
	{
		public GameStateProxy() : base("GameStateProxy") { }
		public GameStateProxy(string name) : base(name) { }

		public GameStateModel GameStateModel { get; private set; }

		public override void OnRegister()
		{
			GameStateModel = new GameStateModel();
		}

		#region Is AI round config

		// Key: Tuple that contains game type and is round even. Value: True if AI input is expected, False if user input is expected.
		private Dictionary<Tuple<GameType, RoundModulo>, bool> turnIsAiInput = new Dictionary<Tuple<GameType, RoundModulo>, bool>
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

		public void StartNewGame(GameType gameType = GameType.PvE)
		{
			var gameSettingsProxy = UnityFacade.GetInstance().RetrieveProxy<GameSettingsProxy>();

			GameStateModel.CurrentRoundNumber = 0;
			GameStateModel.GameType = gameType; // Default is PvE

			// Init the items states and ids
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

		public void StartNewRound()
		{
			GameStateModel.CurrentRoundNumber++;
		}

		// Returns true, if AI should make a move. False, if its players move.
		public bool IsAiRound()
		{
			var roundModulo = (GameStateModel.CurrentRoundNumber % 2 == 0) ? RoundModulo.Even : RoundModulo.Odd;
			var isAiRound = turnIsAiInput[new Tuple<GameType, RoundModulo>(GameStateModel.GameType, roundModulo)];

			Debug.LogFormat("This is {0} round. It is {1} (even/odd). Due to {2} game type, IsAiRound is {3}", GameStateModel.CurrentRoundNumber, roundModulo, GameStateModel.GameType, isAiRound);
			return isAiRound;
		}
	}
}

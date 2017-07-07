using UnityEngine;

namespace LastOneOut
{
	public class Solver
	{
		private GameStateProxy gameStateProxy;
		private GameSettingsProxy gameSettingsProxy;

		public int AmountOfItemsToLeaveOnBoard()
		{
			gameStateProxy = UnityFacade.GetInstance().RetrieveProxy<GameStateProxy>();
			gameSettingsProxy = UnityFacade.GetInstance().RetrieveProxy<GameSettingsProxy>();
			
			var itemsAmount = ClosestWinningNumber();
			return itemsAmount;
		}

		int ClosestWinningNumber()
		{
			var itemsPerTurn = gameSettingsProxy.GameSettings.TakePerTurn;
			var itemsOnTheTable = gameStateProxy.ItemsLeft;
			
			int newCandiadate = 0;
			int iteration = 0;
						
			while (newCandiadate <= itemsOnTheTable) // Iterate through the "winning sequence"
			{
				newCandiadate = IterationNumber(iteration);
				iteration++;
			}

			iteration--; // We're overpassing the amount of items at this stage
			iteration--; // Get previous iteration, its the biggest number in "winning sequence", but less than items left

			var amountOnIteration = IterationNumber(iteration);
			if (amountOnIteration == itemsOnTheTable) // We're losing, lets hope opponent will do a mistake in the next turn
				return amountOnIteration - Random.Range(1, gameSettingsProxy.GameSettings.TakePerTurn);
			
			var res = IterationNumber(iteration);
			return (res >= 1) ? res : 1; // Small hack, dont return less than 1
		}

		int IterationNumber(int iter)
		{
			var res = (1 + gameSettingsProxy.GameSettings.TakePerTurn) * iter + 1; // Top secret formula. Amount of items that should left in order AI to win.
			return res;
		}
	}
}
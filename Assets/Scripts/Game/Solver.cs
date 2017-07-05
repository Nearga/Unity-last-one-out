namespace LastOneOut
{
	public class Solver
	{
		public int AmountOfItemsToTake()
		{
			var gameStateProxy = UnityFacade.GetInstance().RetrieveProxy(typeof(GameStateProxy));
			var gameSettingsProxy = UnityFacade.GetInstance().RetrieveProxy(typeof(GameSettingsProxy));


			return 1;
		}
	}
}
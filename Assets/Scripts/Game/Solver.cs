namespace LastOneOut
{
	// For more complicated cases, DI should be used. But for current case, static is enough, as Solver is stateless and quite simple.
	public static class Solver
	{
		public static int AmountOfItemsToTake()
		{
			var gameStateProxy = UnityFacade.GetInstance().RetrieveProxy(typeof(GameStateProxy));
			var gameSettingsProxy = UnityFacade.GetInstance().RetrieveProxy(typeof(GameSettingsProxy));


			return 1;
		}
	}
}
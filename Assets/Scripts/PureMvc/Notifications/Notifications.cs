namespace LastOneOut
{
	// Notification names
	public class Notifications
	{
		// Init
		public const string Initialize = "Initialize";

		// Navigation
		public const string NavigateTo = "NavigateTo";
		public const string NavigateFrom = "NavigateFrom";

		// Game
		public const string StartGame = "StartGame";
		public const string StartRound = "StartRound";

		public const string PointerEnter = "PointerEnter"; // User input reactions
		public const string PointerExit = "PointerExit";
		public const string PointerClicked = "PointerClicked";

		public const string SyncItemsState = "SyncItemsState"; // When GameStateProxy contains solved state and InGameView should be updated 
	}
}

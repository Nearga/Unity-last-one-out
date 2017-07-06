using System.Collections.Generic;

namespace LastOneOut
{
	public enum ItemState
	{
		OnBoard, 
		SelectedPickable,
		SelectedUnpickable,
		OffBoard
	}

	public class ItemModel
	{
		public int Id { get; set; }
		public ItemState ItemState { get; set; }
	}

	public class GameStateModel
	{
		public GameType GameType { get; set; }
		public int CurrentRoundNumber { get; set; }
		public List<ItemModel> ItemModels { get; set; }
	}
}

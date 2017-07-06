using PureMVC.Interfaces;
using UnityEngine;

namespace LastOneOut
{
	public class ProcessPointerExitCommand : BaseCommand
	{
		public override void Execute(INotification notification)
		{
			base.Execute(notification);

			if (gameStateProxy.IsAiRound())
			{
				Debug.Log("ProcessPointerExitCommand: Waiting for AI input");
				return;
			}

			//Debug.Log("ProcessPointerExitCommand");

			var currentItems = gameStateProxy.ItemModelsList;

			foreach (var item in currentItems)
			{
				// Skip OffBoard items
				if (item.ItemState == ItemState.OffBoard)
					continue;

				// Set all items as OnBoard
				item.ItemState = ItemState.OnBoard;
			}

			// TODO: add some fading - they disappears too fast

			gameStateProxy.ItemModelsList = currentItems; // Save state
		}
	}
}
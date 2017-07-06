using PureMVC.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
	public class ProcessPointerEnterCommand : BaseCommand
	{
		public override void Execute(INotification notification)
		{
			base.Execute(notification);

			if (gameStateProxy.IsAiRound())
			{
				Debug.Log("ProcessPointerEnterCommand: Waiting for AI input");
				return;
			}

			Debug.Log("ProcessPointerEnterCommand");

			gameStateProxy.ItemStatuses = new List<ItemStatus>
			{
				new ItemStatus { Id = 0, ItemState = ItemState.OffBoard },
				new ItemStatus { Id = 1, ItemState = ItemState.OnBoard },
				new ItemStatus { Id = 2, ItemState = ItemState.SelectedPickable },
				new ItemStatus { Id = 3, ItemState = ItemState.SelectedUnpickable },
			};
		}
	}
}
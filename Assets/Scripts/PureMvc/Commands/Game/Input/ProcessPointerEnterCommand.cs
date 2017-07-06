using PureMVC.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

			//Debug.Log("ProcessPointerEnterCommand");


			// It its first player, order by default. If its a second player, order by descending.
			var currentItems = gameStateProxy.IsFirstPlayerRound() 
				? gameStateProxy.ItemStatuses.OrderBy(i => i.Id) 
				: gameStateProxy.ItemStatuses.OrderByDescending(i => i.Id);

			var selectedItemId = (int)notification.Body;
			ItemStatus firstSelectableItem = null; // Start of the selection zone

			foreach (var item in currentItems)
			{
				// Skip OffBoard items
				if (item.ItemState == ItemState.OffBoard)
					continue;

				// Mark start of the selection zone
				if (firstSelectableItem == null)
					firstSelectableItem = item;

				// If outside of selection zone, skip it (state should be OnBoard)
				bool isItemInSelectionZone = gameStateProxy.IsFirstPlayerRound()
					? item.Id <= selectedItemId
					: item.Id >= selectedItemId;

				if (!isItemInSelectionZone)
				{
					item.ItemState = ItemState.OnBoard;
					continue;
				}

				bool isItemSelectable = item.Id < firstSelectableItem.Id + gameSettingsProxy.GameSettings.TakePerTurn; // Selectable, if id is less then start of selection zone + takePerTurn
				if (isItemSelectable)
					item.ItemState = ItemState.SelectedPickable;
				else
					item.ItemState = ItemState.SelectedUnpickable;
			}

			gameStateProxy.ItemStatuses = currentItems; // Save the state			
		}
	}
}
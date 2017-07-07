using PureMVC.Interfaces;
using System;
using System.Linq;
using UnityEngine;

namespace LastOneOut
{
	public class ProcessPointerClickedCommand : BaseCommand
	{
		public override void Execute(INotification notification)
		{
			base.Execute(notification);

			var inputType = (InputType)Enum.Parse(typeof(InputType), notification.Type);

			if (gameStateProxy.IsAiRound() && inputType == InputType.UserInput)
			{
				//Debug.Log("ProcessPointerEnterCommand: Waiting for AI input");
				return;
			}

			//Debug.Log("ProcessPointerClickedCommand");

			var currentItems = gameStateProxy.ItemModelsList;

			// If any item is marked as unpickable, dont do the selection. 
			// TODO: add some hint or blinking of unpickable objcts for user
			if (currentItems.Any(i => i.ItemState == ItemState.SelectedUnpickable))
				return;

			var selectedItemId = (int)notification.Body;
			ItemModel firstSelectableItem = null; // Start of the selection zone

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
					? item.Id >= selectedItemId
					: item.Id <= selectedItemId;

				if (isItemInSelectionZone)
				{
					item.ItemState = ItemState.OffBoard;
				}
			}

			gameStateProxy.ItemModelsList = currentItems; // Save the state
			SendNotification(Notifications.StartNewRound);
		}
	}
}
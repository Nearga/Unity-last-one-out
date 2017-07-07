using PureMVC.Interfaces;
using System.Linq;
using UnityEngine;

namespace LastOneOut
{
	public class AiDecisionCommand : BaseCommand
	{
		[Inject]
		Solver solver;

		private int pickedItemId;

		public AiDecisionCommand()
		{
			this.Inject();
		}

		public override void Execute(INotification notification)
		{
			base.Execute(notification);

			if (!gameStateProxy.IsAiRound())
			{
				Debug.Log("AiInputCommand: Waiting for user input");
				return;
			}

			Debug.Log("AiInputCommand executing");

			// Emulate "thinking" time. 
			Timer.Instance.Once(Random.Range(2f, 5f), MakeDecisionAndMarkItem);			
			//Timer.Instance.Once(0f, MakeDecisionAndMarkItem);
		}

		void MakeDecisionAndMarkItem()
		{
			var amountOfItemsToLeave = solver.AmountOfItemsToLeaveOnBoard();

			Debug.Log(amountOfItemsToLeave);

			var currentItems = gameStateProxy.IsFirstPlayerRound()
				? gameStateProxy.ItemModelsList.OrderBy(i => i.Id)
				: gameStateProxy.ItemModelsList.OrderByDescending(i => i.Id);

			ItemModel firstSelectableItem = null; // Start of the selection zone

			foreach (var item in currentItems) // Iterating in reversed order
			{
				// Skip OffBoard items
				if (item.ItemState == ItemState.OffBoard)
					continue;

				// Mark start of the selection zone
				if (firstSelectableItem == null)
					firstSelectableItem = item;

				if (gameStateProxy.IsFirstPlayerRound())
				{
					// We're looping forward
					if (item.Id == firstSelectableItem.Id + amountOfItemsToLeave) // This is the item we have to pick
					{
						pickedItemId = item.Id;
						Debug.Log("Picking " + pickedItemId);
						SendNotification(Notifications.PointerEnter, pickedItemId, InputType.AiInput.ToString()); // Send "selection" command

						Timer.Instance.Once(1f, DoTakeItems);
						return;
					}
				}
				else
				{
					// We're looping backward
					if (item.Id == firstSelectableItem.Id - amountOfItemsToLeave) // This is the item we have to pick
					{
						pickedItemId = item.Id;
						Debug.Log("Picking " + pickedItemId);
						SendNotification(Notifications.PointerEnter, pickedItemId, InputType.AiInput.ToString()); // Send "selection" command

						Timer.Instance.Once(1f, DoTakeItems);
						return;
					}
				}
			}

			int a = 1;
		}

		void DoTakeItems()
		{
			SendNotification(Notifications.PointerClicked, pickedItemId, InputType.AiInput.ToString()); // Send "clicking" command
		}
	}
}
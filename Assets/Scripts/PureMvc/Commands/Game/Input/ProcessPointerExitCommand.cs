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
		}
	}
}
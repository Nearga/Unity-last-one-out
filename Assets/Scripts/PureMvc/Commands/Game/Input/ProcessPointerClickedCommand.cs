using PureMVC.Interfaces;
using UnityEngine;

namespace LastOneOut
{
	public class ProcessPointerClickedCommand : BaseCommand
	{
		public override void Execute(INotification notification)
		{
			base.Execute(notification);

			if (gameStateProxy.IsAiRound())
			{
				Debug.Log("ProcessPointerClickedCommand: Waiting for AI input");
				return;
			}

			//Debug.Log("ProcessPointerClickedCommand");
		}
	}
}
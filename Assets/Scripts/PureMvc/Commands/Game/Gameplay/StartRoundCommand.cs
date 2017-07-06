using PureMVC.Interfaces;
using UnityEngine;

namespace LastOneOut
{
	public class StartRoundCommand : BaseCommand
	{
		public override void Execute(INotification notification)
		{
			base.Execute(notification);
			Debug.Log("StartRoundCommand");
			
			if (gameStateProxy.IsAiRound())
				SendNotification(Notifications.AiInput);
		}
	}
}
using PureMVC.Interfaces;
using UnityEngine;

namespace LastOneOut
{
	public class StartRoundCommand : BaseCommand
	{
		public override void Execute(INotification notification)
		{
			base.Execute(notification);

			gameStateProxy.StartNewRound();

			Debug.Log("StartRoundCommand " + gameStateProxy.CurrentRoundNumber);
			
			if (gameStateProxy.IsAiRound())
				SendNotification(Notifications.AiInput);
		}
	}
}
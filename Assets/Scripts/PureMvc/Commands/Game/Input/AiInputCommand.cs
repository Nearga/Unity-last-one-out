using PureMVC.Interfaces;
using UnityEngine;

namespace LastOneOut
{
	public class AiInputCommand : BaseCommand
	{
		public override void Execute(INotification notification)
		{
			base.Execute(notification);
			
			if (!gameStateProxy.IsAiRound())
			{
				Debug.Log("AiInputCommand: Waiting for user input");
				return; 
			}

			Debug.Log("AiInputCommand executing");
		}
	}
}
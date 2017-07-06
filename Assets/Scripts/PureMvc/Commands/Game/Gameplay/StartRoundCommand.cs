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

			var gameStateModel = UnityFacade.GetInstance().RetrieveProxy<GameStateProxy>().GameStateModel;
		}
	}
}
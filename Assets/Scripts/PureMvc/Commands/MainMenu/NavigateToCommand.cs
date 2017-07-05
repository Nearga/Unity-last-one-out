using PureMVC.Interfaces;
using UnityEngine;

namespace LastOneOut
{
	public class NavigateToCommand : BaseCommand
	{
		public override void Execute(INotification notification)
		{
			Debug.Log("NavigateToCommand.Execute");
		}
	}
}

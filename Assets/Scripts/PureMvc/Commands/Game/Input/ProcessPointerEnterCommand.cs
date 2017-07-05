using PureMVC.Interfaces;
using UnityEngine;

namespace LastOneOut
{
	public class ProcessPointerEnterCommand : BaseCommand
	{
		public override void Execute(INotification notification)
		{
			base.Execute(notification);
			Debug.Log("ProcessPointerEnterCommand");
		}
	}
}
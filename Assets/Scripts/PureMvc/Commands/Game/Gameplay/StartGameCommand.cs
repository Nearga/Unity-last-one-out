using PureMVC.Interfaces;
using UnityEngine;

public class StartGameCommand : BaseCommand
{
	public override void Execute(INotification notification)
	{
		base.Execute(notification);
		Debug.Log("StartGameCommand");
	}
}

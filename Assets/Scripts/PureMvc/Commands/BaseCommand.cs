using LastOneOut;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class BaseCommand : SimpleCommand
{
	protected GameStateProxy gameStateProxy;

	public override void Execute(INotification notification)
	{
		gameStateProxy = UnityFacade.GetInstance().RetrieveProxy<GameStateProxy>();
	}
}

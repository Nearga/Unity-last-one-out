using LastOneOut;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public enum InputType
{
	AiInput, 
	UserInput
}

public class BaseCommand : SimpleCommand
{
	protected GameStateProxy gameStateProxy;
	protected GameSettingsProxy gameSettingsProxy;

	public override void Execute(INotification notification)
	{
		// Ctor should be a better place for it, check that later
		gameStateProxy = UnityFacade.GetInstance().RetrieveProxy<GameStateProxy>();
		gameSettingsProxy = UnityFacade.GetInstance().RetrieveProxy<GameSettingsProxy>();
	}
}

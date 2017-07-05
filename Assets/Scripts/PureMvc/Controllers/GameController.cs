using PureMVC.Core;

namespace LastOneOut
{ 
	public class GameController : Controller {

		protected override void InitializeController()
		{
			base.InitializeController();

			RegisterCommand(Notifications.StartGame, () => new StartGameCommand());

			RegisterCommand(Notifications.PointerEnter, () => new ProcessPointerEnterCommand());
			RegisterCommand(Notifications.PointerExit, () => new ProcessPointerExitCommand());
			RegisterCommand(Notifications.PointerClicked, () => new ProcessPointerClickedCommand());
		}
	}		
}
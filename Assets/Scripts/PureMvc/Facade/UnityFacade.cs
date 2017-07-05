using PureMVC.Core;
using PureMVC.Patterns.Facade;

namespace LastOneOut
{
    public class UnityFacade : Facade
    {
        static UnityFacade()
        {
            instance = new UnityFacade();
        }

        public static UnityFacade GetInstance()
        {
            return instance as UnityFacade;
        }

        protected override void InitializeController()
		{
			// Init Controller
			controller = Controller.GetInstance(() => new GameController());
			
			// Proxies
			RegisterProxy(new GameSettingsProxy());
			RegisterProxy(new GameStateProxy());
			

			// Commands
			RegisterCommand(Notifications.Initialize, () => new InitializeCommand());

			RegisterCommand(Notifications.NavigateTo, () => new NavigateToCommand());
		}
		
		

		public void Initialize()
        {
            SendNotification(Notifications.Initialize);
		}
    }
}
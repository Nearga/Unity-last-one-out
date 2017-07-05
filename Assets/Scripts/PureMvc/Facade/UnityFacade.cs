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
            base.InitializeController();
			
			// Commands
			RegisterCommand(Notifications.Initialize, () => new InitializeCommand());

			// Proxies
			RegisterProxy(new SettingsProxy("SettingsProxy"));	
		}
		

		public void Initialize()
        {
            SendNotification(Notifications.Initialize);
		}
    }
}
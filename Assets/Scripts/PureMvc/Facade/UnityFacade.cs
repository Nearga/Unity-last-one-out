using PureMVC.Patterns;

namespace LastOneOut
{
    public class UnityFacade : Facade
    {
        static UnityFacade()
        {
            m_instance = new UnityFacade();
        }

        public static UnityFacade GetInstance()
        {
            return m_instance as UnityFacade;
        }

        protected override void InitializeController()
        {
            base.InitializeController();
			
			// Commands
			RegisterCommand(Notifications.Initialize, typeof(InitializeCommand));

			// Proxies
			RegisterProxy(new SettingsProxy("SettingsProxy"));			
		}
		

		public void Initialize()
        {
            SendNotification(Notifications.Initialize);
		}
    }
}
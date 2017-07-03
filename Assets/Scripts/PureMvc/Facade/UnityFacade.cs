using System;
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

			//var c = MainMenuController.Instance;

			// Commands
			RegisterCommand(Notifications.Initialize, typeof(InitializeCommand));


			//RegisterCommand(Notifications.Navigate, typeof(NavigateCommand)); // TODO: move to MainMenuController
		}
		

		public void Initialize()
        {
            SendNotification(Notifications.Initialize);
		}
    }
}
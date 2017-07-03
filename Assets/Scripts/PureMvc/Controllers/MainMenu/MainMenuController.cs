using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
	public class MainMenuController : BaseController
	{
		protected override void InitializeController()
		{
			base.InitializeController();

			//RegisterCommand(Notifications.Navigate, typeof(NavigateCommand));
		}
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
	public class InGameMediator : BaseMediator
	{
		public InGameMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent) { }

		private InGameView inGameView;

		public override void OnRegister()
		{
			// View should be GameObject...
			var asGameObject = ViewComponent as GameObject; // StartGameView;
			if (asGameObject == null)
				Debug.LogException(new Exception("ViewComponent is not a GameObject"));

			// ... and InGameView
			inGameView = asGameObject.GetComponent<InGameView>();
			if (inGameView == null)
				Debug.LogException(new Exception("ViewComponent is not a StartGameView"));

			// Add buttons listeners
			inGameView.MainMenuButton.RemoveListenersAndSubscribe(OnMainMenuClicked);
		}
		
		void OnMainMenuClicked()
		{
			Debug.Log("OnMainMenuClicked");
			SendNotification(Notifications.Navigate, typeof(MainMenuView));
		}
	}
}
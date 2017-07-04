using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LastOneOut
{
	// Handles Notifications.Navigate
	public abstract class BaseMediator : Mediator
	{
		public BaseMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent) { }

		public BaseMediator(string mediatorName, GameObject viewComponent) : base(mediatorName, viewComponent) { }


		public override IList<string> ListNotificationInterests()
		{
			var list = base.ListNotificationInterests();
			list.Add(Notifications.Navigate);
			return list;
		}

		public override void HandleNotification(INotification notification)
		{
			var type = ((Type)notification.Body);

			/*
			// TODO: finish dynamic instatiation, based on type

			//var mss = typeof(SingletonObject<>).GetMethods();

			var m = typeof(SingletonObject<>).GetMethod("InstantiateSingleton");
			MethodInfo i = m.MakeGenericMethod(type);
			var ii = i.Invoke(null, null);
			*/


			// Hide current View
			var viewGO = ViewComponent as GameObject;
			//var view = viewGO.GetComponent<ViewComponent>(); 
			viewGO.SetActive(false);


			// Load new View
			if (type == typeof(MainMenuView))
			{
				if (SceneManager.GetActiveScene().name != Constants.MainMenuScene)
					SceneManager.LoadScene(Constants.MainMenuScene, LoadSceneMode.Single);

				var newView = MainMenuView.Instance;
				newView.gameObject.SetActive(true);
			}
			if (type == typeof(ChooseGameMenuView))
			{
				if (SceneManager.GetActiveScene().name != Constants.MainMenuScene)
					SceneManager.LoadScene(Constants.MainMenuScene, LoadSceneMode.Single);

				var newView = ChooseGameMenuView.Instance;
				newView.gameObject.SetActive(true);
			}			
			if (type == typeof(InGameView))
			{
				SceneManager.LoadScene(Constants.GameScene, LoadSceneMode.Single);

				var newView = InGameView.Instance;
				newView.gameObject.SetActive(true);
			}
		}
	}
}
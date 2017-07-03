using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
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

			// NOTE: temporary workaround
			if (type == typeof(ChooseGameView))
			{
				var newView = ChooseGameView.Instance;
				newView.gameObject.SetActive(true);
			}
			if (type == typeof(StartGameView))
			{
				var newView = StartGameView.Instance;
				newView.gameObject.SetActive(true);
			}

			// Hide current view
			var viewGO = ViewComponent as GameObject;
			//var view = viewGO.GetComponent<ViewComponent>(); 
			viewGO.SetActive(false);
		}
	}
}
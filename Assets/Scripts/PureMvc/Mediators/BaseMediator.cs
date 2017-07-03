using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
	public abstract class BaseMediator : Mediator
	{
		protected GameObject viewComponent;

		public BaseMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent) { }

		public override void OnRegister()
		{
			SubscribeOnSource();
		}

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

		void SubscribeOnSource()
		{
			// Check that ViewComponent is a GameObject
			var asGameObject = ViewComponent as GameObject;
			if (asGameObject == null)
				Debug.LogException(new NotSupportedException("ViewComponent is not an GameObject"));
			
			// If it contains MvcButton, just call DoAction, once clicked
			var asButton = asGameObject.GetComponentInChildren<MvcButton>();
			if (asButton != null)
				asButton.SubscribeOnClick(OnButtonClickedAction);
		}



		public virtual void OnButtonClickedAction() { }
	}
}
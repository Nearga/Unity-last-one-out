using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System;
using System.Linq;
using UnityEngine;

namespace LastOneOut
{
	// Handles Notifications.Navigate
	public abstract class BaseMediator : Mediator
	{
		public BaseMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent) { }

		public BaseMediator(string mediatorName, GameObject viewComponent) : base(mediatorName, viewComponent) { }
		

		public override string[] ListNotificationInterests()
		{
			var list = base.ListNotificationInterests().ToList();
			list.Add(Notifications.NavigateFrom);
			return list.ToArray();
		}

		public override void HandleNotification(INotification notification)
		{
			switch (notification.Name)
			{
				case Notifications.NavigateFrom:
					// Hide current View
					var viewGO = ViewComponent as GameObject;
					//var view = viewGO.GetComponent<ViewComponent>(); 
					viewGO.SetActive(false);
					break;
				default:
					break;
			}
		}

		protected void SendStartGameNotification(GameType gameType)
		{
			SendNotification(Notifications.NavigateFrom);
			SendNotification(Notifications.NavigateTo, typeof(InGameView), gameType.ToString());
		}

		protected void SendLoadViewNotification(Type navigateToView)
		{
			SendNotification(Notifications.NavigateFrom);
			SendNotification(Notifications.NavigateTo, navigateToView);
		}
	}
}
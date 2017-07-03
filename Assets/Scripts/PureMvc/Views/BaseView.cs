using PureMVC.Core;
using PureMVC.Interfaces;
using System;
using UnityEngine;

namespace LastOneOut
{
	public abstract class BaseView<T> : SingletonObject<T> where T : SingletonObject
	{
		protected abstract Type GetMediatorType();
		protected IMediator mediator;
		protected IView view;

		private void OnEnable()
		{
			view = View.Instance;

			var type = GetMediatorType();
			mediator = (IMediator)Activator.CreateInstance(type, type.ToString(), gameObject);
			view.RegisterMediator(mediator);
		}

		private void OnDisable()
		{
			view.RemoveMediator(mediator.GetType().ToString());
		}

		/*
		protected IView view;

		void Start()
		{
			view = View.Instance;
			
			// Run through all IMediatorPluggers and create + register them if necessary
			var mediatorPluggerControls = GetComponentsInChildren<IMediatorPlugger>();
			foreach (var item in mediatorPluggerControls)
			{
				var mediatorName = item.GetMediatorName();
				if (!view.HasMediator(mediatorName))
				{
					try
					{
						//var t = typeof(StartGameMediator).FullName;
						var mediatorType = Type.GetType(string.Format("{0}.{1}", Constants.Namespace, mediatorName));
						var mediatorPlug = (IMediator)Activator.CreateInstance(mediatorType, mediatorName, item.GetGameObject());
						view.RegisterMediator(mediatorPlug);
					}
					catch (ArgumentNullException)
					{
						var itemGO = item.GetGameObject();
						var viewGO = itemGO.GetComponentInParent<BaseView<T>>();

						string viewName = string.Empty;
						if (viewGO != null)
							viewName = viewGO.name;

						Debug.LogError(String.Format("MediatorName is either not set or not valid. Please, check the control {0} at the view {1}.", itemGO.name, viewName));
					}
					catch (Exception ex)
					{
						throw ex;
					}					
				}				
			}
		}
		*/
	}
}
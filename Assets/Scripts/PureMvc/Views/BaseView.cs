using PureMVC.Interfaces;
using System;

namespace LastOneOut
{
	public abstract class BaseView<T> : UnitySingleton<T> where T : UnitySingleton
	{
		protected abstract Type GetMediatorType();
		protected IMediator mediator;
		protected IView view; // "Composition over inheritance" - yessir!

		virtual public void OnEnable()
		{
			var type = GetMediatorType();
			mediator = (IMediator)Activator.CreateInstance(type, type.ToString(), gameObject);
			view.RegisterMediator(mediator);
		}

		private void OnDisable()
		{
			view.RemoveMediator(mediator.GetType().ToString());
		}
	}
}
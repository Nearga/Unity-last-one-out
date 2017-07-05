using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System;
using UnityEngine;

public static class FacadeEx  {

	public static T RetrieveProxy<T>(this Facade facade) where T : IProxy
	{
		try
		{
			var name = typeof(T).Name;
			var proxy = facade.RetrieveProxy(name);
			return (T)proxy;
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			return default(T);
		}
	}

	public static IProxy RetrieveProxy(this Facade facade, Type proxyType)
	{
		try
		{
			var proxy = facade.RetrieveProxy(proxyType.Name);
			return proxy;
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			return null;
		}
	}
}

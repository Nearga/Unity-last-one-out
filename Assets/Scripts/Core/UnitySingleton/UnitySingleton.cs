using UnityEngine;

public abstract class UnitySingleton : MonoBehaviour {

	public virtual void OnSngletonDestroyed() { }
}

public abstract class UnitySingleton<T> : UnitySingleton where T : UnitySingleton
{
	static T instance;

	protected void Awake()
	{
		DestroySingleton();		
		instance = (T)(object)this;
	}

	public static T Instance
	{
		get
		{
			InstantiateSingleton();
			return instance;
		}
	}


	public static void DestroySingleton()
	{
		if (instance)
		{
			instance.OnSngletonDestroyed();
			Destroy(instance.gameObject);
		}
		instance = null;
	}


	public static void InstantiateSingleton()
	{
		if (!instance)
		{
			var obj = FindObjectOfType(typeof(T));

			if (obj)
			{
				instance = (T)obj;
				return;
			}

			var resourcePath = default(string);
			var attributes = typeof(T).GetCustomAttributes(typeof(UnitySingletonAttribute), false);

			// Try to load path from the ResourceObjectAttribute
			if (attributes.Length > 0)
				resourcePath = ((UnitySingletonAttribute)attributes[0]).Path;

			// If empty, try to load by Name
			if (string.IsNullOrEmpty(resourcePath))
				resourcePath = typeof(T).Name;

			var resourceObj = Resources.Load(resourcePath);
			if (resourceObj)
			{
				obj = Instantiate(resourceObj);

				if (obj)
				{
					instance = ((GameObject)obj).GetComponent<T>();
				}
				else
				{
					Debug.LogError(string.Format("Could not instantiate instance of {0}", typeof(T)));
				}
			}
			else
			{
				//Debug.LogError(string.Format("Could not load instance of {0}", typeof(T)));
				instance = new GameObject(typeof(T).Name, typeof(T)).GetComponentInChildren<T>();
			}

			DontDestroyOnLoad(instance);
		};
	}
}

using UnityEngine;
using System.Linq;

namespace LastOneOut
{
	public static class Factory
	{
		public static T Instantiate<T>(GameObject prefab, Transform parent = null) where T : MonoBehaviour
		{
			try
			{
				var newInstance = GameObject.Instantiate(prefab);
				newInstance.name = prefab.name;

				if (parent != null)
					newInstance.transform.parent = parent;

				return newInstance.GetComponent(typeof(T)) as T;
			}
			catch (System.Exception)
			{
				Debug.LogError("Something went wrong at instancing. Source gameobject: " + prefab.name + ". Requested type: " + typeof(T));
				return null;
			}
		}

		public static T Instantiate<T>(string resourcePath, Transform parent = null) where T : MonoBehaviour
		{
			try
			{
				var newInstance = (GameObject)GameObject.Instantiate(Resources.Load(resourcePath));
				newInstance.name = resourcePath.Split('/').Last();

				if (parent != null)
					newInstance.transform.parent = parent;

				return newInstance.GetComponent(typeof(T)) as T;
			}
			catch (System.Exception)
			{
				Debug.LogError("Something went wrong at instancing. Resource path: " + resourcePath + ". Requested type: " + typeof(T));
				return null;
			}
		}

		public static GameObject Instantiate(string resourcePath, Transform parent = null)
		{
			try
			{
				var newInstance = (GameObject)GameObject.Instantiate(Resources.Load(resourcePath));
				newInstance.name = resourcePath.Split('/').Last();

				if (parent != null)
					newInstance.transform.parent = parent;

				return newInstance;
			}
			catch (System.Exception)
			{
				Debug.LogError("Something went wrong at instancing. Resource path: " + resourcePath);
				return null;
			}
		}
	}
}
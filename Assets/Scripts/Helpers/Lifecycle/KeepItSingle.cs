using UnityEngine;

public class KeepItSingle : MonoBehaviour {

	static GameObject instance;

	protected void Awake()
	{
		DestroySingleton();
		instance = gameObject;
	}

	public static void DestroySingleton()
	{
		if (instance)
		{
			Destroy(instance.gameObject);
		}
		instance = null;
	}
}

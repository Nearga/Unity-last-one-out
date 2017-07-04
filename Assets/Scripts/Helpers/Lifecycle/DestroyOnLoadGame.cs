using UnityEngine;

namespace LastOneOut
{
	public class DestroyOnLoadGame : MonoBehaviour
	{
		void Start()
		{
			if (Bootstrap.Instance)
				Destroy(gameObject);
		}
	}
}
using UnityEngine;

namespace LastOneOut
{
	public class Bootstrap : MonoBehaviour
	{
		public static Bootstrap Instance;

		void Awake()
        {
			if (Instance)
			{
				gameObject.SetActive(false);
				Destroy(gameObject);

				return;
			}
			
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		private void Start()
		{
			UnityFacade.GetInstance().Initialize();
		}
	}
}
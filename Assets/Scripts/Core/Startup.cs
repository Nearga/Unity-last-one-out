using UnityEngine;

namespace LastOneOut
{
	public class Startup : MonoBehaviour
	{
		DependencyContainer container;
			 
		void Awake()
		{
			container = DependencyContainer.Instance;
			container.ClearBindings();
			
			container.Bind<AssetMapRoot>(GetComponentInChildren<AssetMapRoot>());

			container.BindCompleted();
		}
	}
}
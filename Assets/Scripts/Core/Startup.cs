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
			
			// In a real world, a key should be an interface
			container.Bind<AssetMapRoot>(GetComponentInChildren<AssetMapRoot>());

			container.Bind<Solver>(new Solver());

			container.BindCompleted();
		}
	}
}
using UnityEngine.Events;
using UnityEngine.UI;

namespace LastOneOut
{
	public static class ButtonEx
	{
		public static void RemoveListenersAndSubscribe(this Button button, UnityAction action)
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(action);
		}		
	}
}
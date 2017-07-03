using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LastOneOut
{
	public class MvcButton : MvcBaseControl
	{
		public void SubscribeOnClick(UnityAction action)
		{
			var button = GetComponentInChildren<Button>();
			button.onClick.AddListener(action);
		}
	}
}
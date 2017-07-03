using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LastOneOut
{
	public class MvcBaseControl : MonoBehaviour, IMediatorPlugger
	{
		[SerializeField]
		string mediatorName;

		#region IMediatorPlugger implementation

		public string GetMediatorName()
		{
			return mediatorName;
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}

		#endregion
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
	public class AssetMapRoot : MonoBehaviour
	{
		[SerializeField]
		List<GameObject> Maps;

		Dictionary<System.Type, AssetMapBase> assetMaps = new Dictionary<System.Type, AssetMapBase>();

		void Awake()
		{
			foreach (GameObject prefab in Maps)
			{
				GameObject go = Instantiate(prefab);
				go.transform.parent = transform;
			}

			UpdateAssetMaps();
		}

		void UpdateAssetMaps()
		{
			AssetMapBase[] maps = GetComponentsInChildren<AssetMapBase>();

			foreach (AssetMapBase map in maps)
			{
				assetMaps[map.GetType()] = map;
			}
		}

		public void AddMap<T>(T map) where T : AssetMapBase
		{
			assetMaps[typeof(T)] = map;
		}

		public T GetMap<T>() where T : AssetMapBase
		{
			AssetMapBase value;
			if (!assetMaps.TryGetValue(typeof(T), out value))
			{
				Debug.LogError("Type not found: " + typeof(T));
			}
			return (T)value;
		}
	}
}
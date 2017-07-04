using System;
using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
	[Serializable]
	public class AssetMapItem
	{
		public string EnumName;
		public int EnumValue;
		public UnityEngine.Object Object;
		public UnityEngine.Color Color;
	}

	public abstract class AssetMapBase : MonoBehaviour
	{
		public UnityEngine.Object Default;
		public List<AssetMapItem> Items;

		public abstract Type EnumType { get; }

		public abstract Type ObjectType { get; }

		protected virtual void Awake()
		{

		}
	}

	public abstract class AssetMap<TEnum, TObject> : AssetMapBase
		where TEnum : struct
		where TObject : UnityEngine.Object
	{

		protected Dictionary<Enum, AssetMapItem> assetMap = new Dictionary<Enum, AssetMapItem>();


		public TObject Get(int key)
		{
			AssetMapItem obj;

			Enum enumObj = (Enum)Enum.ToObject(EnumType, key);
			assetMap.TryGetValue(enumObj, out obj);
			if (obj == null)
			{
				return null;
			}
			return (TObject)obj.Object;
		}

		public TObject Get(TEnum key)
		{
			AssetMapItem obj;

			Enum enumObj = (Enum)Enum.ToObject(EnumType, key);
			assetMap.TryGetValue(enumObj, out obj);
			if (obj == null || obj.Object == null)
			{
				Debug.LogError("There's no " + key + " in the asset maps ");
				return null;
			}
			return (TObject)obj.Object;
		}


		protected override void Awake()
		{
			base.Awake();

			//Basically convert List to Dictionary
			foreach (AssetMapItem item in Items)
			{
				UnityEngine.Object obj = item.Object;
				if (obj == null)
					obj = Default;

				Enum enumObj = (Enum)Enum.ToObject(EnumType, item.EnumValue);

				if (obj != null || item.Color != Color.clear)
					assetMap.Add(enumObj, item);
			}

		}

		public override Type EnumType
		{
			get
			{
				return typeof(TEnum);
			}
		}

		public override Type ObjectType
		{
			get
			{
				return typeof(TObject);
			}
		}
	}
}
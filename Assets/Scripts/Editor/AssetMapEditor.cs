using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LastOneOut
{
	[CustomEditor(typeof(AssetMapBase), true)]
	public class AssetMapEditor : UnityEditor.Editor
	{
		enum MapValueType
		{
			Color,
			NotValueType
		}

		MapValueType mapValueType = MapValueType.NotValueType;

		public override void OnInspectorGUI()
		{
			AssetMapBase script = (AssetMapBase)target;


			Undo.RecordObject(script, "Asset Map Editor");
			EditorGUI.BeginChangeCheck();
			bool forcedSave = false;

			if (script.Items == null)
				script.Items = new List<AssetMapItem>();
			Type enumType = script.EnumType;

			mapValueType = GetMapValueType(script.ObjectType);

			switch (mapValueType)
			{
				//				case MapValueType.Color:
				//					script.Default = EditorGUILayout.ColorField(script.c, item.Color);
				//					break;
				case MapValueType.NotValueType:
					script.Default = EditorGUILayout.ObjectField("Default", script.Default, script.ObjectType, false);
					break;
			}

			EditorGUILayout.Space();

			List<string> enumNames = Enum.GetNames(enumType).ToList();
			var enumValues = Enum.GetValues(enumType);

			for (int i = 0; i < enumNames.Count; i++)
			{
				var tempItem = script.Items.Where(x => x.EnumName == enumNames[i]).FirstOrDefault();
				if (tempItem != null)
				{
					if (script.Items[i] != tempItem)
					{
						script.Items.Remove(tempItem);
						script.Items.Insert(i, tempItem);
						forcedSave = true;
					}
				}
				else
				{
					script.Items.Insert(i, new AssetMapItem()
					{
						EnumName = enumNames[i],
						EnumValue = (int)enumValues.GetValue(i),
						Object = null,
						Color = Color.black
					});
					forcedSave = true;
				}
				script.Items[i].EnumValue = (int)enumValues.GetValue(i);
			}


			for (int i = 0; i < script.Items.Count; i++)
			{
				if (enumNames.Contains(script.Items[i].EnumName))
				{
					enumNames.Remove(script.Items[i].EnumName);
					switch (mapValueType)
					{
						case MapValueType.Color:
							script.Items[i].Color = EditorGUILayout.ColorField(script.Items[i].EnumName.EnumSplit(), script.Items[i].Color);
							break;
						case MapValueType.NotValueType:
							script.Items[i].Object = EditorGUILayout.ObjectField(script.Items[i].EnumName.EnumSplit(), script.Items[i].Object, script.ObjectType, false);
							break;
					}
				}
				else
				{
					script.Items.RemoveAt(i);
					i--;
					forcedSave = true;
				}
			}

			// Enum values
			/*
						foreach (Enum enumKey in Enum.GetValues(enumType))
						{
							AssetMapItem item = FindItem(script, enumKey);
							if (item == null)
							{
								item = new AssetMapItem();
								item.EnumName = enumKey.ToString();
								item.EnumValue = Convert.ToInt32(enumKey);
								item.Object = null;
								script.Items.Add(item);
							}
							item.EnumName = enumKey.ToString();
							switch (mapValueType)
							{
								case MapValueType.Color:						
									item.Color = EditorGUILayout.ColorField(enumKey.ToString(), item.Color);
									break;
								case MapValueType.NotValueType:
									item.Object = EditorGUILayout.ObjectField(enumKey.ToString(), item.Object, script.ObjectType, false);
									break;
							}
						}*/

			if (EditorGUI.EndChangeCheck() || forcedSave)
			{
				Undo.RecordObject(script, "Asset Map");
				EditorUtility.SetDirty(script);
				AssetDatabase.SaveAssets();
			}
		}

		private AssetMapItem FindItem(AssetMapBase behaviour, Enum enumKey)
		{
			int enumValue = Convert.ToInt32(enumKey);
			foreach (var item in behaviour.Items)
			{
				if (item.EnumValue == enumValue)
					return item;
			}
			return null;
		}

		private AssetMapItem FindItem(AssetMapBase behaviour, string enumName)
		{
			foreach (var item in behaviour.Items)
			{
				if (item.EnumName == enumName)
					return item;
			}
			return null;
		}

		MapValueType GetMapValueType(Type t)
		{
			if (t.IsGenericType)
			{
				if (t.GetGenericArguments()[0] == typeof(Color))
				{
					return MapValueType.Color;
				}
			}
			return MapValueType.NotValueType;
		}
	}
}

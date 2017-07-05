using UnityEngine;

namespace LastOneOut
{
	public enum MaterialType
	{
		Normal,
		Selected,
		Forbidden
	}

	public class MaterialsAssetMap : AssetMap<MaterialType, Material>
	{
	}
}
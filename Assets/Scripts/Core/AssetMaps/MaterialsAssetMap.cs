using UnityEngine;

namespace LastOneOut
{
	public enum MaterialType
	{
		Normal,
		Selected
	}

	public class MaterialsAssetMap : AssetMap<MaterialType, Material>
	{
	}
}
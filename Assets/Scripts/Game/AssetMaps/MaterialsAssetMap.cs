using UnityEngine;

namespace LastOneOut
{
	public enum MaterialType
	{
		Normal,
		Selected,
		Forbidden,
		Hidden
	}

	public class MaterialsAssetMap : AssetMap<MaterialType, Material>
	{
	}
}
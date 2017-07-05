using LastOneOut;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public int Id;

	public Action<int> PointerEnterItem, PointerExitItem, PointerClickItem;

	private InGameView inGameView;


	[Inject]
	readonly AssetMapRoot assetMapRoot;


	public void Initialize(InGameView view, int id)
	{
		this.Inject();

		inGameView = view;
		Id = id;
	}

	public void SetMaterial(MaterialType type)
	{
		var materialsAssetMap = assetMapRoot.GetMap<MaterialsAssetMap>();
		gameObject.GetComponent<Renderer>().material = materialsAssetMap.Get(type);
	}
	
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Pointer Enter " + Id);

		PointerEnterItem.Invoke(Id);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Pointer Exit " + Id);

		PointerExitItem.Invoke(Id);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Pointer Click " + Id);

		PointerClickItem.Invoke(Id);
	}

	public override string ToString()
	{
		return String.Format("Item " + Id);
	}
}

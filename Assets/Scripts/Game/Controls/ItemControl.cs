using LastOneOut;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Action<int> PointerEnterItem, PointerExitItem;

	private InGameView inGameView;
	private int id;

	public void Initialize(InGameView view, int id)
	{
		inGameView = view;
		this.id = id;
	}
	
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Pointer Enter " + id);

		PointerEnterItem.Invoke(id);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Pointer Exit " + id);

		PointerExitItem.Invoke(id);
	}
}

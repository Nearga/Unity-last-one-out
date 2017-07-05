using PureMVC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LastOneOut
{
	[UnitySingleton("Prefabs/Views/Game/InGameView")]
	public class InGameView : BaseView<InGameView>
	{
		public Button MainMenuButton;

		public GameObject TableGameObject;

		public GameObject ItemGameObject;


		private List<ItemControl> items = new List<ItemControl>();
		private GameSettingsProxy gameSettingsProxy;

		public override void OnEnable()
		{
			view = View.GetInstance(() => new View());
			base.OnEnable();

			gameSettingsProxy = UnityFacade.GetInstance().RetrieveProxy<GameSettingsProxy>();
		}

		override protected Type GetMediatorType() { return typeof(InGameMediator); }


		public void GenerateItems()
		{
			if (TableGameObject == null)
				TableGameObject = GameObject.FindGameObjectWithTag("Table");

			items.Clear();

			for (int i = 0; i < gameSettingsProxy.GameSettings.TotalItems; i++)
			{
				AddItem(i);
			}
		}

		#region Item generation

		private void AddItem(int itemId)
		{
			var item = Factory.Instantiate<ItemControl>(ItemGameObject, TableGameObject.transform);
			item.Initialize(this, itemId);
			item.transform.position = CalculateItemPosition(itemId);
			item.PointerEnterItem += PointerEnterItem;
			item.PointerExitItem += PointerExitItem;
			item.PointerClickItem += PointerClickItem;

			items.Add(item);
		}

		private void RemoveItem(int itemId)
		{

		}

		private Vector3 CalculateItemPosition(int itemId)
		{
			var totalItems = gameSettingsProxy.GameSettings.TotalItems;

			var tablePos = TableGameObject.transform.position;
			var leftmostItemPos = tablePos + Vector3.up * 0.55f + Vector3.left * 0.40f + Vector3.back * 0.1f; // Some magic numbers. Bad, but fast.
			var rightmostItemPos = tablePos + Vector3.up * 0.55f + Vector3.right * 0.40f + Vector3.back * 0.1f;
			
			float itemsLerp = (float)itemId / (float)(totalItems - 1);
			var currentPos = Vector3.Lerp(leftmostItemPos, rightmostItemPos, itemsLerp);

			return currentPos;
		}

		#endregion

		private void PointerEnterItem(int id)
		{
			var item = items.Where(i => i.Id == id).FirstOrDefault();
			item.SetMaterial(MaterialType.Selected);
		}

		private void PointerExitItem(int id)
		{

		}

		private void PointerClickItem(int id)
		{

		}
	}
}
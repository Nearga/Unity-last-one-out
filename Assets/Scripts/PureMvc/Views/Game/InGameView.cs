using PureMVC.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LastOneOut
{
	[ResourceObject("Prefabs/Views/Game/InGameView")]
	public class InGameView : BaseView<InGameView>
	{
		public Button MainMenuButton;

		public GameObject TableGameObject;

		public GameObject ItemGameObject;


		private List<ItemControl> items;
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

			for (int i = 0; i < gameSettingsProxy.GameSettings.TotalItems; i++)
			{
				var item = Factory.Instantiate<ItemControl>(ItemGameObject, TableGameObject.transform);
				item.transform.position = CalculateItemPosition(i);
			}
		}

		private Vector3 CalculateItemPosition(int itemId)
		{
			var totalItems = gameSettingsProxy.GameSettings.TotalItems;

			var tablePos = TableGameObject.transform.position;
			var leftmostItemPos = tablePos + Vector3.up * 0.55f + Vector3.left * 0.45f;
			var rightmostItemPos = tablePos + Vector3.up * 0.55f + Vector3.right * 0.45f;

			// Lerp goes from 0 to 1, depending on item id (first id = 0, last id = 1) 
			//float itemsLerp = 
			//	(itemId == 0) ? 0 :
			//	(itemId == totalItems) ? 1 :
			//	itemId / totalItems;

			float itemsLerp = (float)itemId / (float)(totalItems - 1);

			var currentPos = Vector3.Lerp(leftmostItemPos, rightmostItemPos, itemsLerp);

			return currentPos;
		}
	}
}
using DG.Tweening;
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

		[SerializeField]
		GameObject tableGameObject;

		[SerializeField]
		GameObject itemGameObject;

		[SerializeField]
		RectTransform ItemsLeftRt;

		[SerializeField]
		RectTransform PlayersTurnRt;

		[SerializeField]
		RectTransform RoundRt;


		private List<ItemControl> items = new List<ItemControl>();
		private GameSettingsProxy gameSettingsProxy;
		private GameStateProxy gameStateProxy;
		private Sequence roundSequence;

		public override void OnEnable()
		{
			view = View.GetInstance(() => new View());
			base.OnEnable();

			gameSettingsProxy = UnityFacade.GetInstance().RetrieveProxy<GameSettingsProxy>();
			gameStateProxy = UnityFacade.GetInstance().RetrieveProxy<GameStateProxy>();

			RoundRt.SetActive(false);

			roundSequence = DOTween.Sequence();
			roundSequence.Append(RoundRt.DOScale(1f, 0.0f));
			roundSequence.Append(RoundRt.DOShakeScale(0.2f, 0.1f));
			roundSequence.Insert(1f, RoundRt.DOScale(0.4f, 0.3f));
			roundSequence.OnComplete(OnRoundSeqCompleted);
		}

		override protected Type GetMediatorType() { return typeof(InGameMediator); }

		#region Controls

		public void SetItemsLeftText(int itemsLeft)
		{
			ItemsLeftRt.SetText(itemsLeft > 0
				? string.Format("Items left: {0}/{1}", itemsLeft, gameSettingsProxy.GameSettings.TotalItems)
				: "No items left");			
		}

		public void SetRoundNumber(int roundNum, GameState state)
		{
			RoundRt.SetActive(true);

			if (state == GameState.Started)
				RoundRt.SetText("Round " + roundNum);
			else
				RoundRt.SetText(state == GameState.Player1Won ? "Player One won!" : "Player Two won!");
			
			roundSequence.Restart(false);
		}

		private void OnRoundSeqCompleted()
		{
			if (gameStateProxy.GameState == GameState.Started)
				RoundRt.SetActive(false);
			if (gameStateProxy.GameState == GameState.Player1Won || gameStateProxy.GameState == GameState.Player2Won ) 
				roundSequence.Restart(true); // Poor man's celebration, just loop the sequance
		}

		public void SetPlayerTurnText(bool isFirstPlayerTurn)
		{
			PlayersTurnRt.SetText( isFirstPlayerTurn // a bit confusing, but minor issue, no time to fix
				? "Second player"
				: "First player");
		}

		#endregion

		#region Item management 

		public void GenerateItems() // TODO: merge this with SyncItems
		{
			if (tableGameObject == null)
				tableGameObject = GameObject.FindGameObjectWithTag("Table");

			items.Clear();

			for (int i = 0; i < gameSettingsProxy.GameSettings.TotalItems; i++)
			{
				AddItem(i);
			}
		}

		public void SyncItems()
		{
			foreach (var item in gameStateProxy.ItemModelsList)
			{
				var existingItem = items.FirstOrDefault(i => i.Id == item.Id);
				if (existingItem != null)
					SetState(existingItem, item.ItemState);
			}
		}

		#endregion

		#region Item generation

		private void AddItem(int itemId)
		{
			var item = Factory.Instantiate<ItemControl>(itemGameObject, tableGameObject.transform);
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

			var tablePos = tableGameObject.transform.position;
			var leftmostItemPos = tablePos + Vector3.left * 0.2f + Vector3.up * 0.96f +  Vector3.forward * 0.7f; // Some magic numbers. Bad, but fast. In proper world, spawnpoints should be used.
			var rightmostItemPos = tablePos + Vector3.left * 0.2f + Vector3.up * 0.96f - Vector3.forward * 0.7f;
			
			float itemsLerp = (float)itemId / (float)(totalItems - 1);
			var currentPos = Vector3.Lerp(leftmostItemPos, rightmostItemPos, itemsLerp);

			return currentPos;
		}

		private void SetState(ItemControl item, ItemState state)
		{
			switch (state)
			{
				case ItemState.OnBoard:
					item.SetMaterial(MaterialType.Normal);
					break;
				case ItemState.SelectedPickable:
					item.SetMaterial(MaterialType.Selected);
					break;
				case ItemState.SelectedUnpickable:
					item.SetMaterial(MaterialType.Forbidden);
					break;
				case ItemState.OffBoard:
					item.SetMaterial(MaterialType.Hidden);
					break;
				default:
					break;
			}
		}

		#endregion

		private void PointerEnterItem(int id)
		{
			view.NotifyObservers(new Notification(Notifications.PointerEnter, id, InputType.UserInput.ToString()));
		}

		private void PointerExitItem(int id)
		{
			view.NotifyObservers(new Notification(Notifications.PointerExit, id, InputType.UserInput.ToString()));
		}

		private void PointerClickItem(int id)
		{
			view.NotifyObservers(new Notification(Notifications.PointerClicked, id, InputType.UserInput.ToString()));
		}
	}
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public static class TransformExtention
{
	public static Transform TransformScale(this Transform transform, float scaleValue)
	{
		if (transform != null)
			transform.localScale = new Vector3(scaleValue, scaleValue,scaleValue);

		return transform;
	}

	#region ExistCheck

	public static Transform TransformExist(this Transform transform, Action onExist, Action onNonexisting = null)
	{
		if (transform == null)
		{
			if (onNonexisting != null)
				onNonexisting.Invoke();
		}
		else
		{
			if (onExist != null)
				onExist.Invoke();
		}
		return transform;
	}

	public static Transform TransformExist<T>(this Transform transform, Action<T> onExist, T value, Action onNonexisting = null)
	{
		if (transform == null)
		{
			if (onNonexisting != null)
				onNonexisting.Invoke();
		}
		else
		{
			if (onExist != null)
				onExist.Invoke(value);
		}
		return transform;
	}

	public static Transform TransformExist<T, T2>(this Transform transform, Action<T, T2> onExist, T value, T2 secondValue, Action onNonexisting = null)
	{
		if (transform == null)
		{
			if (onNonexisting != null)
				onNonexisting.Invoke();
		}
		else
		{
			onExist.Invoke(value, secondValue);
		}
		return transform;
	}

	#endregion


	#region Try/SetText

	public static void SetText(this Transform transform, string txt)
	{
		try
		{
			var textComponent = transform.GetComponentInChildren<Text>();

			textComponent.text = txt;
		}
		catch (Exception e)
		{
			throw new MissingComponentException("Text component is not found in " + (transform != null ? transform.name : e.ToString()), e);
		}
	}

	public static void SetText(this Transform transform, int number)
	{
		try
		{
			string txt = number.ToString();

			var textComponent = transform.GetComponentInChildren<Text>();

			textComponent.text = txt;
		}
		catch (Exception e)
		{
			throw new MissingComponentException("Text component is not found in " + (transform != null ? transform.name : e.ToString()), e);
		}
	}

	public static void SetText(this Transform transform, string txt, int skip)
	{
		try
		{
			skip = Mathf.Max(0, skip);
			var textComponent = transform.GetComponentsInChildren<Text>();

			if (textComponent.Length <= skip)
				throw new MissingComponentException("Text component is not found in " + transform.name);
			if (textComponent == null)
				throw new MissingComponentException("Text component is not found in " + transform.name);

			textComponent[skip].text = txt;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public static bool TrySetText(this Transform transform, string txt)
	{
		try
		{
			var textComponent = transform.GetComponentInChildren<Text>();

			if (textComponent == null)
				return false;

			textComponent.text = txt;
			return true;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return false;
		}
	}

	#endregion

	#region Try/SetTextFormat

	public static void SetTextFormat(this Transform transform, string text, params object[] args)
	{
		transform.SetText(string.Format(text, args));
	}

	public static void TrySetTextFormat(this Transform transform, string text, params object[] args)
	{
		transform.TrySetText(string.Format(text, args));
	}

	#endregion

	#region Try/SetTextColor

	public static void SetTextColor(this Transform transform, Color color)
	{
		try
		{
			var textComponent = transform.GetComponentInChildren<Text>();

			if (textComponent == null)
				throw new MissingComponentException("Text component is not found in " + transform.name);

			textComponent.color = color;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public static bool TrySetTextColor(this Transform transform, Color color)
	{
		try
		{
			var textComponent = transform.GetComponentInChildren<Text>();

			if (textComponent == null)
				return false;

			textComponent.color = color;
			return true;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return false;
		}
	}

	#endregion

	#region Try/SetColor

	public static void SetColor(this Transform transform, Color color)
	{
		try
		{
			var colorComponent = transform.GetComponentInChildren<Graphic>();

			if (colorComponent == null)
				throw new MissingComponentException("Color component is not found in " + transform.name);

			colorComponent.color = color;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public static bool TrySetColor(this Transform transform, Color color)
	{
		try
		{
			var colorComponent = transform.GetComponentInChildren<Graphic>();

			if (colorComponent == null)
				return false;

			colorComponent.color = color;
			return true;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return false;
		}
	}

	#endregion

	#region Try/SetAlpha

	public static void SetAlpha(this Transform transform, float alpha)
	{
		try
		{
			var colorComponent = transform.GetComponentInChildren<Graphic>();

			if (colorComponent == null)
				throw new MissingComponentException("Color component is not found in " + transform.name);

			Color col = colorComponent.color;
			col.a = alpha;
			colorComponent.color = col;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public static bool TrySetAlpha(this Transform transform, float alpha)
	{
		try
		{
			var colorComponent = transform.GetComponentInChildren<Graphic>();

			if (colorComponent == null)
				return false;

			Color col = colorComponent.color;
			col.a = alpha;
			colorComponent.color = col;
			return true;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return false;
		}
	}

	#endregion

	#region Try/SetButtonEvent

	public static void SetButtonEvent(this Transform transform, UnityAction action)
	{
		try
		{
			var buttonComponent = transform.GetComponentInChildren<Button>();

			if (buttonComponent == null)
			{
				buttonComponent = transform.GetComponentInChildren<Button>();
				if (buttonComponent == null)
					throw new MissingComponentException("Button Component is missing in " + transform.name);
			}
			buttonComponent.onClick.AddListener(action);
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public static bool TrySetButtonEvent(this Transform transform, UnityAction action)
	{
		try
		{
			if (transform == null)
				return false;

			var buttonComponent = transform.GetComponentInChildren<Button>();

			if (buttonComponent == null)
			{
				buttonComponent = transform.GetComponentInChildren<Button>();
				if (buttonComponent == null)
					return false;
			}
			buttonComponent.onClick.AddListener(action);
			return true;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return false;
		}
	}

	#endregion

	#region Try/SetAndDeleteAllButtonEvent

	public static void SetAndDeleteAllButtonEvent(this Transform transform, UnityAction action)
	{
		try
		{
			var buttonComponent = transform.GetComponentInChildren<Button>();

			if (buttonComponent == null)
			{
				buttonComponent = transform.GetComponentInChildren<Button>();
				if (buttonComponent == null)
					throw new MissingComponentException("Button Component is missing in " + transform.name);
			}
			buttonComponent.onClick.RemoveAllListeners();
			buttonComponent.onClick.AddListener(action);
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public static bool TrySetAndDeleteAllButtonEvent(this Transform transform, UnityAction action)
	{
		try
		{
			if (transform == null)
				return false;

			var buttonComponent = transform.GetComponentInChildren<Button>();

			if (buttonComponent == null)
			{
				buttonComponent = transform.GetComponentInChildren<Button>();
				if (buttonComponent == null)
					return false;
			}
			buttonComponent.onClick.RemoveAllListeners();
			buttonComponent.onClick.AddListener(action);
			return true;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return false;
		}
	}

	#endregion

	#region Try/SetButtonInteractive

	public static void SetButtonInteractive(this Transform transform, bool interactive)
	{
		try
		{
			var buttonComponent = transform.GetComponentInChildren<Button>();

			if (buttonComponent == null)
			{
				buttonComponent = transform.GetComponentInChildren<Button>();
				if (buttonComponent == null)
					throw new MissingComponentException("Button Component is missing in " + transform.name);
			}

			buttonComponent.interactable = interactive;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public static bool TrySetButtonInteractive(this Transform transform, bool interactive)
	{
		try
		{
			if (transform == null)
				return false;

			var buttonComponent = transform.GetComponentInChildren<Button>();

			if (buttonComponent == null)
			{
				buttonComponent = transform.GetComponentInChildren<Button>();
				if (buttonComponent == null)
					return false;
			}
			buttonComponent.interactable = interactive;
			return true;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return false;
		}
	}

	#endregion

	#region Try/SetImage

	public static void SetImage(this Transform transform, Sprite sprite)
	{
		try
		{
			var imageComponent = transform.GetComponentInChildren<Image>();

			if (imageComponent == null)
				throw new MissingComponentException("Image component is missing in " + transform.name);

			imageComponent.sprite = sprite;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public static void SetImage(this Transform transform, Sprite sprite, int skip)
	{
		try
		{
			var imageComponent = transform.GetComponentsInChildren<Image>()[skip];

			if (imageComponent == null)
				throw new MissingComponentException("Image component is missing in " + transform.name);

			imageComponent.sprite = sprite;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public static bool TrySetImage(this Transform transform, Sprite sprite)
	{
		try
		{
			var imageComponent = transform.GetComponentInChildren<Image>();

			if (imageComponent == null)
				return false;

			imageComponent.sprite = sprite;
			return true;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return false;
		}
	}

	public static void SetImage(this Transform transform, Texture2D texture)
	{
		try
		{
			var imageComponent = transform.GetComponentInChildren<RawImage>();

			if (imageComponent == null)
				throw new MissingComponentException("Image component is missing in " + transform.name);

			imageComponent.texture = texture;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public static bool TrySetImage(this Transform transform, Texture2D texture)
	{
		try
		{
			var imageComponent = transform.GetComponentInChildren<RawImage>();

			if (imageComponent == null)
				return false;

			imageComponent.texture = texture;
			return true;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return false;
		}
	}

	#endregion

	#region Try/SetActive

	public static void SetActive(this Transform transform, bool isActive)
	{
		try
		{
			if (transform != null && transform.gameObject != null)
				transform.gameObject.SetActive(isActive);
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public static bool TrySetActive(this Transform transform, bool isActive)
	{
		try
		{
			if (transform == null || transform.gameObject == null)
				return false;

			transform.gameObject.SetActive(isActive);
			return true;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return false;
		}
	}

	#endregion



	public static Text GetTextComponent(this Transform transform)
	{
		try
		{
			var text = transform.GetComponentInChildren<Text>();

			if (text == null)
				throw new MissingComponentException("Text component is not found in " + transform.name);

			return text;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return null;
		}
	}

	public static void DestroyAllChildren(this Transform transform)
	{
		for (int i = transform.childCount - 1; i >= 0; --i)
		{
			UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
		}
		transform.DetachChildren();
	}
}
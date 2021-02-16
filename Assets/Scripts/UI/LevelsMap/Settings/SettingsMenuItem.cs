using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenuItem : MonoBehaviour
{
	[SerializeField] private Sprite spriteOn;
	[SerializeField] private Sprite spriteOff;

	private Image image;
	public Image Image
	{
		get
		{
			if(image == null)
			{
				image = GetComponent<Image>();
			}
			return image;
		}
	}

	private RectTransform trans;
	public RectTransform Trans
	{
		get
		{
			if(trans == null)
			{
				trans = transform as RectTransform;
			}
			return trans;
		}
	}

	private Button button;
	public Button Button
	{
		get
		{
			if(button == null)
			{
				button = GetComponent<Button>();
			}
			return button;
		}
	}

	private bool isEnable = true;
	public bool IsEnable
	{
		set
		{
			isEnable = value;

			if(isEnable)
				Image.sprite = spriteOn;
			else
				Image.sprite = spriteOff;
		}
		get
		{
			return isEnable;
		}
	}

	public void Click()
	{
		IsEnable = !IsEnable;
	}
}
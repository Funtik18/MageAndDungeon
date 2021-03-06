﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FixedJoystick : Joystick
{

	public UnityAction onTap;


	private Fader fader;
	public Fader Fader
	{
		get
		{
			if(fader == null)
			{
				fader = GetComponent<Fader>();
			}
			return fader;
		}
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);

		onTap?.Invoke();
	}


	public void StartOpenJoystick()
	{
		Fader.FadeIn();
		Fader.CanvasGroup.interactable = true;
		Fader.CanvasGroup.blocksRaycasts = true;
	}


	[ContextMenu("OpenJoystick")]
	private void OpenJoystick()
	{
		Fader.CanvasGroup.alpha = 1;
		Fader.CanvasGroup.interactable = true;
		Fader.CanvasGroup.blocksRaycasts = true;
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
	[ContextMenu("CloseJoystick")]
	private void CloseJoystick()
	{
		Fader.CanvasGroup.alpha = 0;
		Fader.CanvasGroup.interactable = false;
		Fader.CanvasGroup.blocksRaycasts = false;
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
}
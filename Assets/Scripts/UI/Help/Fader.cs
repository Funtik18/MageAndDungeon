using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
	private float fadeDuration;
	private float startFade = 0f;
	private float endFade = 1f;

	private CanvasGroup canvasGroup;
	public CanvasGroup CanvasGroup 
	{
		get
		{
			if(canvasGroup == null)
			{
				canvasGroup = GetComponent<CanvasGroup>();
			}
			return canvasGroup;
		}
	}

	private Coroutine fadeCoroutine = null;
	public bool IsFadeProcess => fadeCoroutine != null;

	public void FadeIn()
	{
		StartFade(CanvasGroup.alpha, 1f, 0.5f);
	}
	public void FadeOut()
	{
		StartFade(CanvasGroup.alpha, 0f, 0.5f);
	}

	public void StartFade(float startFade, float endFade, float fadeDuration)
	{
		if(!IsFadeProcess)
		{
			this.fadeDuration = fadeDuration;
			this.startFade = startFade;
			this.endFade = endFade;

			fadeCoroutine = StartCoroutine(Fade(CanvasGroup));
		}
	}
	private IEnumerator Fade(CanvasGroup cg)
	{
		float counter = 0;

		while(counter < fadeDuration)
		{
			counter += Time.deltaTime;
			cg.alpha = Mathf.Lerp(startFade, endFade, counter / fadeDuration);

			yield return null;
		}

		StopFade();
	}
	public void StopFade()
	{
		if(IsFadeProcess)
		{
			StopCoroutine(fadeCoroutine);
			fadeCoroutine = null;
		}
	}
}

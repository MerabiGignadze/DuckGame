using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UIFadeOut : MonoBehaviour
{
	float fadeSpeed = 1;
	CanvasGroup canvasGroup;
	private void Start()
	{
		if (canvasGroup == null) { InitilializeCanvas(); }
	}
	public void InitilializeCanvas()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}
	public void Fade(bool tougle, float speedMultiplier)
	{
		if (canvasGroup == null) { InitilializeCanvas(); }
		if (canvasGroup != null)
		{
			fadeSpeed = speedMultiplier;
			StopAllCoroutines();
			if (tougle)
			{
				StartCoroutine(FadeIn());
			}
			else
			{
				StartCoroutine(FadeOut());
			}
		}

	}
	IEnumerator FadeOut()
	{
		while (canvasGroup.alpha > 0)
		{
			canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		canvasGroup.blocksRaycasts = false;
	}
	IEnumerator FadeIn()
	{
		while (canvasGroup.alpha < 1)
		{
			canvasGroup.alpha += Time.deltaTime * fadeSpeed;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		canvasGroup.blocksRaycasts = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashHighlighter : MonoBehaviour
{
	[SerializeField] SpriteRenderer spriteRenderer;

	public void Flash()
	{
		StartCoroutine(FlashRoutine());
	}

	IEnumerator FlashRoutine()
	{
		float alpha = 0;
		Color color = spriteRenderer.color;
		while (alpha < 1)
		{
			alpha += Time.deltaTime * 20;
			yield return new WaitForSeconds(Time.deltaTime);
			color.a = alpha; 
			spriteRenderer.color = color;
		}
		while (alpha > 0)
		{
			alpha -= Time.deltaTime * 15;
			yield return new WaitForSeconds(Time.deltaTime);
			color.a = alpha;
			spriteRenderer.color = color;
		}
	}
}

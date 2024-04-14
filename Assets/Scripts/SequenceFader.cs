using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceFader : MonoBehaviour
{
	[SerializeField] Image fadeCover;
	[SerializeField] float fadeSpeed;
	[SerializeField] float stopTime;
	IEnumerator fadeRoutine;


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			FadeIn();
		}
	}
	public void FadeIn()
	{
		if (fadeRoutine != null) { StopCoroutine(fadeRoutine); }
		fadeRoutine = Fade();
		StartCoroutine(fadeRoutine);
	}
	IEnumerator Fade()
	{
		fadeCover.fillClockwise = false;
		yield return new WaitForSeconds(1);
		while (fadeCover.fillAmount < 1)
		{
			fadeCover.fillAmount += Time.deltaTime * fadeSpeed;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		fadeCover.fillAmount = 1;
		fadeCover.fillClockwise = true;
		yield return new WaitForSeconds(stopTime);
		while (fadeCover.fillAmount > 0)
		{
			fadeCover.fillAmount -= Time.deltaTime * fadeSpeed;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		fadeCover.fillAmount = 0;
	}
}

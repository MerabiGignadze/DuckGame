using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
	float fps;
	public TextMeshProUGUI fpsText;
	public Color highFpsColor;
	public Color lowFpsColor;
	public Color poorFpsColor;

    void Start()
    {
		StartCoroutine(ShowFps());
    }
    IEnumerator ShowFps()
	{
		while (true)
		{
			fps = 1 / Time.deltaTime;
			int frames = (int)fps;
			ChangeFpsColor(frames);
			fpsText.text = frames.ToString() + " FPS";
			yield return new WaitForSeconds(0.3f);
		}
	}
	void ChangeFpsColor(int fps)
	{
		if (fps < 10)
		{
			fpsText.color = poorFpsColor;
		}
		else if (fps < 20)
		{
			fpsText.color = lowFpsColor;
		}
		else
		{
			fpsText.color = highFpsColor;
		}
	}
}

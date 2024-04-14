using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckAnimEvents : MonoBehaviour
{
	[SerializeField] DuckObject duckObject;
	public void AppearEnd()
	{
		duckObject.PlaySplashSound();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
	[SerializeField] Transform visualObject;
	IEnumerator activeRoutine;
	[SerializeField] float scaleSpeed;
	[SerializeField] DuckObject duckObject;

	public void Pick()
	{
		ChangeObjectScale(1.1f);
		duckObject.PickDuck();
	}
	public void Release()
	{
		duckObject.ReleaseDuck();
	}
	public void ChangeObjectScale(float _targetScale)
	{
		if (activeRoutine != null) { StopCoroutine(activeRoutine); }
		activeRoutine = SmoothScaleRoutine(_targetScale);
		StartCoroutine(activeRoutine);
	}
	IEnumerator SmoothScaleRoutine(float _scale)
	{
		Vector3 targetScale = new Vector3((int)duckObject.transform.localScale.x,1,1) * _scale;
		Vector3 velocity = Vector3.zero;

		while (Vector3.Distance(visualObject.localScale, targetScale) > 0.01f)
		{
			visualObject.localScale = Vector3.SmoothDamp(visualObject.localScale, targetScale, ref velocity, scaleSpeed);
			yield return new WaitForSeconds(Time.deltaTime);
		}
		visualObject.localScale = targetScale;
	}
}

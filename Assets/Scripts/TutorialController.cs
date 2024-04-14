using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
	[SerializeField] float timer;
	[SerializeField] float timeInterval;
	[SerializeField] bool activeTutorial;
	IEnumerator swipeRoutine;
	[SerializeField] UIFadeOut handHint;
	[SerializeField] float swipeSpeed;
	SequenceManager sequenceManager;
	[SerializeField] Camera gameCamera;

	private void Update()
	{
		if (activeTutorial)
		{
			TimeCycle();
		}
	}
	public void Initialize(SequenceManager _sequenceManager)
	{
		sequenceManager = _sequenceManager;
		handHint.Fade(false, 20);
	}
	void TimeCycle()
	{
		if (timer < timeInterval)
		{
			timer += Time.deltaTime;
		}
		if (timer >= timeInterval)
		{
			Vector3[] duckPos = sequenceManager.GetSwimingDuckPosition();
			StopSwipeRoutine();
			swipeRoutine = SwipeHint(duckPos[0], duckPos[1]);
			StartCoroutine(swipeRoutine);
			ResetTimer();
		}
	}
	IEnumerator SwipeHint(Vector3 _origin, Vector3 _target)
	{
		_origin = gameCamera.WorldToScreenPoint(_origin);
		_target = gameCamera.WorldToScreenPoint(_target);
		handHint.transform.position = _origin;
		handHint.Fade(true, 20);
		Vector3 velocity = Vector3.zero;
		while (Vector3.Distance(handHint.transform.position, _target) > 0.1)
		{
			handHint.transform.position = Vector3.SmoothDamp(handHint.transform.position, _target, ref velocity, swipeSpeed);
			yield return new WaitForSeconds(Time.deltaTime);
		}
		yield return new WaitForSeconds(0.5f);
		handHint.Fade(false, 20);
	}
	public void ActivateTutorial(bool _enable)
	{
		activeTutorial = _enable;
		if (!_enable) { StopSwipeRoutine(); }
	}
	public void ResetTutorial()
	{
		timer = 0;
		StopSwipeRoutine();
	}
	void ResetTimer()
	{
		timer = 0;
	}
	void StopSwipeRoutine()
	{
		handHint.Fade(false,20);
		if (swipeRoutine != null) { StopCoroutine(swipeRoutine); }
	}
}

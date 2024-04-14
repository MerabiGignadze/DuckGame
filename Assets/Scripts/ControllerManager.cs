using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour
{
	public RectTransform controllerCircle;
	public RectTransform controllerFingerStick;
	public RectTransform inputPointer;
	//public PlayerController playerController;
	public Vector3 inputDirection;
	public float HorrizontalAxis;
	public float VerticalAxis;
	public UIFadeOut touchControllors;
	public bool showFingerPosition;
	public float normilizedController;
	public Vector2 startPosition;
	public float positionFromStart;
	[Header("MouseSwipe")]
	[SerializeField] float currentMousePosition;
	[SerializeField] float swipeStartPosition;
	public bool activeMouseSwipe;

	private void Start()
	{
		touchControllors.Fade(false, 30);
		if (showFingerPosition) { inputPointer.GetComponent<Image>().enabled = true; }
		else { inputPointer.GetComponent<Image>().enabled = false; }
	}
	private void Update()
	{
		JoyStickController();
		normilizedController = Vector2.Distance(controllerCircle.position, controllerFingerStick.position) / controllerCircle.sizeDelta.x / 2 * 10;
		positionFromStart = startPosition.x - Input.mousePosition.x;
		if (activeMouseSwipe && Input.GetMouseButton(0)) { SwipeController(); }
		if (Input.GetMouseButtonDown(0)) { swipeStartPosition = Input.mousePosition.x; currentMousePosition = swipeStartPosition; }
	}
	void JoyStickController()
	{
		if (Input.GetMouseButtonDown(0))
		{
			controllerCircle.position = Input.mousePosition;
			startPosition = Input.mousePosition;
			touchControllors.Fade(true, 15);
			//playerController.moving = true;
		}
		if (Input.GetMouseButton(0))
		{
			float conrollerRadius = (controllerCircle.sizeDelta.x - controllerFingerStick.sizeDelta.x) / 2;
			inputPointer.position = Input.mousePosition;
			inputDirection = new Vector2(inputPointer.position.x - controllerCircle.position.x, inputPointer.position.y - controllerCircle.position.y).normalized;
			if (Vector2.Distance(controllerCircle.position, inputPointer.position) < conrollerRadius)
			{
				controllerFingerStick.position = inputPointer.position;
			}
			else
			{
				controllerFingerStick.position = controllerCircle.position + inputDirection * conrollerRadius;
			}
			HorrizontalAxis = inputDirection.x;
			VerticalAxis = inputDirection.y;
			//Debug.DrawLine(controllerCircle.position, controllerCircle.position + inputDirection * controllerCircle.sizeDelta.x/2, Color.green, 2f);
		}
		else
		{
			controllerFingerStick.position = controllerCircle.position;
		}
		if (Input.GetMouseButtonUp(0))
		{
			touchControllors.Fade(false, 15);
			//playerController.moving = false;
			startPosition = Vector2.zero;
			HorrizontalAxis = 0;
			VerticalAxis = 0;
		}
	}
	public void ResetStartPosition()
	{
		positionFromStart = 0;
		startPosition = Input.mousePosition;
	}
	void SwipeController()
	{
		currentMousePosition = Input.mousePosition.x;
		if (swipeStartPosition != currentMousePosition)
		{
			if (currentMousePosition < swipeStartPosition - 15)
			{
				CountSwipe(-1);
			}
			if (currentMousePosition > swipeStartPosition + 15)
			{
				CountSwipe(1);
			}
			swipeStartPosition = currentMousePosition;
		}
	}
	void CountSwipe(float _direction)
	{
	}
	public void ActivateMouseSwipe(bool _tougle)
	{
		activeMouseSwipe = _tougle;
	}
	public float PadDistance()
	{
		return Vector2.Distance(controllerCircle.position, controllerFingerStick.position);
	}
}

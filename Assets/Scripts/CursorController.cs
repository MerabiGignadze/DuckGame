using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
	[SerializeField] Camera gameCamera;
	DraggableObject selectedDraggable;

    void Update()
    {
		ObjectInteraction();
		DragObject();
    }

	void ObjectInteraction()
	{
		RaycastHit2D hit;
		Ray camRay = gameCamera.ScreenPointToRay(Input.mousePosition);
		hit = Physics2D.Raycast(camRay.origin, camRay.direction);
		if (hit.transform)
		{
			if (Input.GetMouseButtonDown(0) && hit.transform.GetComponent<DraggableObject>())
			{
				selectedDraggable = hit.transform.GetComponent<DraggableObject>();
				selectedDraggable.Pick();
			}
		}
		if (selectedDraggable && Input.GetMouseButtonUp(0))
		{
			selectedDraggable.Release();
			selectedDraggable = null;
		}
	}
	void DragObject()
	{
		if (!selectedDraggable) { return; }
		Vector3 screenProjection = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
		selectedDraggable.transform.position = gameCamera.ScreenToWorldPoint(screenProjection);
	}
}

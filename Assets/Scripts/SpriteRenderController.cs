using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderController : MonoBehaviour
{
	[SerializeField]
	SpriteRenderer[] spriteRenderers;
	[SerializeField]
	bool visibleObject;
	[HideInInspector]
	public bool objectIsVisible; 

	public void EnableRenderers(bool _visible)
	{
		visibleObject = _visible;
		objectIsVisible = visibleObject;
		foreach (SpriteRenderer sprite in spriteRenderers)
		{
			sprite.enabled = _visible;
		}
	}
	private void OnDrawGizmos()
	{
		if (visibleObject != objectIsVisible)
		{
			EnableRenderers(objectIsVisible);
		}
	}
}

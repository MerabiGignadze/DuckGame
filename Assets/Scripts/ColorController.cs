using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
	[SerializeField]
	SpriteRenderer[] spriteElements;
	[SerializeField]
	Color spriteCololor;
	[HideInInspector]
	public Color activeSpriteColor;

	public void ChangeColor(Color _color, bool inEditor)
	{
		if (!inEditor) { spriteCololor = _color; }
		activeSpriteColor = spriteCololor;
		foreach (SpriteRenderer sprite in spriteElements)
		{
			sprite.color = activeSpriteColor;
		}
	}
	private void OnDrawGizmos()
	{
		if (spriteCololor != activeSpriteColor)
		{
			ChangeColor(activeSpriteColor, true);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceSlot : MonoBehaviour
{
	[SerializeField] Image slotImage;
	[SerializeField] Color checkedColor;
	[SerializeField] Color uncheckedColor;
	[SerializeField] Animator slotAnimator;

	public void CheckSlot(bool _check, bool _withAnim)
	{
		if (_check)
		{
			slotImage.color = checkedColor;
			if (_withAnim) { PlayCheckAnim(); }
			return;
		}
		slotImage.color = uncheckedColor;
	}
	void PlayCheckAnim()
	{
		slotAnimator.SetTrigger("Check");
	}
}

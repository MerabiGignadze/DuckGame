using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class HapticsController : MonoBehaviour
{
	public void Haptic(HapticTypes _haptic)
	{
		MMVibrationManager.Haptic(_haptic);
	}
}

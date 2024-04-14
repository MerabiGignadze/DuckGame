using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreenController : MonoBehaviour
{
	public GameManagers gameManagers;

	public void PushRestartButton()
	{
		gameManagers.StartLevel();
	}
}

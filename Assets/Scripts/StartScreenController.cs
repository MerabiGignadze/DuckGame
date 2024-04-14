using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenController : MonoBehaviour
{
	public GameManagers gameManagers;

	public void PushStartGameButton()
	{
		gameManagers.StartLevel();
	}
}

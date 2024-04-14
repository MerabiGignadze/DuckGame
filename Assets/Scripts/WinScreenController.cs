using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenController : MonoBehaviour
{
	public GameManagers gameManagers;
	public TextMeshProUGUI levelText;

	public void PushNextButton()
	{
		gameManagers.StartLevel();
	}
	public void DrawWinScreen()
	{

	}
}

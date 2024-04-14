using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SequenceManager : MonoBehaviour
{
	[SerializeField] GameManagers gameManagers;
	[SerializeField] int maxSequence;
	[SerializeField] int currentSequence;
	[SerializeField] List<BasketObject> basketObjects;
	[SerializeField] List<DuckObject> duckObjects;
	[SerializeField] Color[] ColorPalet;
	[SerializeField] List<Color> activeColors;
	IEnumerator sequenceRoutine;
	[SerializeField] List<Vector3> entryPositions;
	float colorThreshold = 0.01f;
	TutorialController tutorialController;

	public void StartGame()
	{
		SubscribeToBasketEvents(true);
		CollectEntryPoints();
		StartSequense();
		gameManagers.uiManager.InitializeSequenceUI(maxSequence);
		tutorialController = gameManagers.uiManager.tutorialController;
		tutorialController.Initialize(this);
		foreach (DuckObject duck in duckObjects)
		{
			duck.Initilize(gameManagers.soundPlayer);
			duck.ResetDuck(entryPositions[duckObjects.IndexOf(duck)]);
			duck.ShowDuck(false);
		}
	}
	void CollectEntryPoints()
	{
		entryPositions = new List<Vector3>();
		foreach (DuckObject duck in duckObjects)
		{
			entryPositions.Add(duck.transform.position);
		}
	}
	void StartSequense()
	{
		if (sequenceRoutine != null) { StopCoroutine(sequenceRoutine); }
		sequenceRoutine = StartSequenseRoutine();
		StartCoroutine(sequenceRoutine);
	}
	IEnumerator StartSequenseRoutine()
	{
		foreach (BasketObject basket in basketObjects)
		{
			basket.Initialize(gameManagers.soundPlayer);
			basket.ResetBasket(); basket.ShowBasket(false);
		}
		PickColors();
		yield return new WaitForSeconds(1);
		foreach (BasketObject basket in basketObjects)
		{
			yield return new WaitForSeconds(0.2f);
			basket.ShowBasket(true);
		}
		foreach (DuckObject duck in duckObjects)
		{	
			yield return new WaitForSeconds(0.2f);
			duck.ShowDuck(true);
		}
		tutorialController.ActivateTutorial(true);
	}
	IEnumerator EndSequence()
	{
		bool lastSequence = currentSequence >= maxSequence;
		gameManagers.soundPlayer.PlaySound(gameManagers.soundPlayer.coreSounds.fx_correct, false);
		tutorialController.ActivateTutorial(false);
		tutorialController.ResetTutorial();
		gameManagers.uiManager.UpdateSequenceUI(currentSequence);
		
		float waitTime = 0.5f;
		foreach (BasketObject basket in basketObjects)
		{
			basket.EmitStarParticles();
		}
		if (!lastSequence)
		{
			waitTime = 1.5f;
			gameManagers.uiManager.FadeOut();
		}
		yield return new WaitForSeconds(waitTime); 
		NextSqeuence();

		if (lastSequence) { yield return new WaitForSeconds(1f); }
		foreach (DuckObject duck in duckObjects)
		{
			duck.ResetDuck(entryPositions[duckObjects.IndexOf(duck)]);
			duck.ShowDuck(false);
		}
	}
	void NextSqeuence()
	{
		if (currentSequence < maxSequence)
		{
			currentSequence++;
			StartSequense();
		}
		else
		{
			EndGame();
		}
	}
	void EndGame()
	{
		SubscribeToBasketEvents(false);
		gameManagers.EndLevelel(GameManagers.EndLelelCondition.Win);
		ResetSequence();
	}
	void ResetSequence()
	{
		currentSequence = 0;
	}
	void CheckBaskets()
	{
		tutorialController.ResetTutorial();
		foreach (BasketObject basket in basketObjects)
		{
			if (!basket.occupiedBasket)
			{
				return;
			}
		}
		StartCoroutine(EndSequence());
	}
	void SubscribeToBasketEvents(bool _subscribe)
	{
		foreach (BasketObject basket in basketObjects)
		{
			if (_subscribe) { basket.OccupieBasketEvent += CheckBaskets; }
			else { basket.OccupieBasketEvent -= CheckBaskets; }
		}
	}
	void PickColors()
	{
		activeColors = new List<Color>();
		int duckCount = duckObjects.Count;
		List<Color> colorsHolder = ColorPalet.ToList();
		for (int i = 0; i < duckCount; i++)
		{
			Color randomColor = colorsHolder[Random.Range(0, colorsHolder.Count)];
			activeColors.Add(randomColor);
			colorsHolder.Remove(randomColor);
			basketObjects[i].SetColor(randomColor);
		}
		SetRandomDuckColors();
	}
	void SetRandomDuckColors()
	{
		List<Color> colorsHolder = activeColors;
		foreach (DuckObject duck in duckObjects)
		{
			Color randomColor = colorsHolder[Random.Range(0, colorsHolder.Count)];
			duck.SetColor(randomColor);
			colorsHolder.Remove(randomColor);
		}
	}
	public Vector3[] GetSwimingDuckPosition()
	{
		List<Vector3> positions = new List<Vector3>();
		foreach (DuckObject duck in duckObjects)
		{
			if (!duck.inBasket)
			{
				positions.Add(duck.transform.position);
				positions.Add(GetBasketByColor(duck.GetColor()));
				break;
			}
		}
		return positions.ToArray();
	}
	Vector3 GetBasketByColor(Color _color)
	{
		Vector3 basketPosition = basketObjects[0].transform.position;
		foreach (BasketObject basket in basketObjects)
		{
			if (SameColor(_color, basket.GetBasketColor()))
			{
				basketPosition = basket.transform.position;
				break;
			}
		}
		return basketPosition;
	}
	bool SameColor(Color _color1, Color _color2)
	{
		return (Mathf.Abs(_color1.r - _color2.r) <= colorThreshold &&
				Mathf.Abs(_color1.g - _color2.g) <= colorThreshold &&
				Mathf.Abs(_color1.b - _color2.b) <= colorThreshold &&
				Mathf.Abs(_color1.a - _color2.a) <= colorThreshold);
	}
}

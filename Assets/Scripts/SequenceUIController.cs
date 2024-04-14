using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceUIController : MonoBehaviour
{
	[SerializeField] SequenceSlot slotToSpawn;
	[SerializeField] List<SequenceSlot> sequenceSlots;
	int maxSequences;

	public void InitializeSlots(int _maxSequence)
	{
		maxSequences = _maxSequence;
		PopulateWithSlots();
	}
	void PopulateWithSlots()
	{
		sequenceSlots = new List<SequenceSlot>();
		foreach (Transform slot in transform)
		{
			Destroy(slot.gameObject);
		}
		for (int i = 0; i <= maxSequences; i++)
		{			
			SequenceSlot newSlot = Instantiate(slotToSpawn, transform);
			sequenceSlots.Add(newSlot);
			newSlot.CheckSlot(false, false);
		}
	}
	public void UpdateSlots(int _currentSequence)
	{
		foreach (SequenceSlot slot in sequenceSlots)
		{
			int slotIndex = sequenceSlots.IndexOf(slot);
			slot.CheckSlot(slotIndex <= _currentSequence, slotIndex == _currentSequence);
		}
	}
}

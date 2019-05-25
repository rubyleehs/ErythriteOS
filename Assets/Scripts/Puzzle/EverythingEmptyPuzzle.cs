using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EEPuzzle")]
public class EverythingEmptyPuzzle : Puzzle
{
    public override bool CheckWinCondition()
    {
        return (HistoryManager.currentPointOfTime == InventoryOS.elements.Length - 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinPuzzle")]
public class FinalWinPuzzle : Puzzle
{
    public override bool CheckWinCondition()
    {
        for (int i = 0; i < PuzzleManager.isSolved.Length -1; i++)
        {
            if (PuzzleManager.isSolved[i] == 0) return false;
        }
        return true;
    }
}

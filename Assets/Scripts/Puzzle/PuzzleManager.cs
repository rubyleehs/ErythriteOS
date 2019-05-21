using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Puzzle[] puzzles;
    protected bool[] isSolved;

    [HideInInspector]
    public int currentPuzzleIndex = 0;

    [HideInInspector]
    public new Transform transform;
    protected PuzzleShower puzzleShower;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        isSolved = new bool[puzzles.Length];
        puzzleShower = GetComponent<PuzzleShower>();
        puzzleShower.Initialize();
        if (puzzles.Length > 0) SetPuzzle(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) ChangePuzzle(1);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangePuzzle(-1);
    }

    public void SetPuzzle(int index)
    {
        currentPuzzleIndex = Mathf.Clamp(index, 0, puzzles.Length - 1);
        puzzleShower.SetPuzzle(puzzles[currentPuzzleIndex]);
    }

    public void ChangePuzzle(int delta)
    {
        currentPuzzleIndex = Mathf.Clamp(currentPuzzleIndex + delta, 0, puzzles.Length - 1);
        puzzleShower.SetPuzzle(puzzles[currentPuzzleIndex]);
    }
}

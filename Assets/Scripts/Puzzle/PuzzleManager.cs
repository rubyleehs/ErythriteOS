using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Puzzle[] puzzles;
    public PuzzleShower puzzleShower;
    public GameObject levelIndicatorGO;


    public int levelsPerSide;
    public float radius;
    public Color selectedColor;
    public Color[] indicatorColor;
    [HideInInspector]
    public SpriteRenderer[] levelIndicators;

    public static int[] isSolved;

    [HideInInspector]
    public int currentPuzzleIndex = 0;

    [HideInInspector]
    public new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        isSolved = new int[puzzles.Length];
        levelIndicators = new SpriteRenderer[puzzles.Length];
        puzzleShower.Initialize();
        SpawnLevelIndicators();
        if (puzzles.Length > 0) SetPuzzle(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) ChangePuzzle(1);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangePuzzle(-1);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (BoardOS.CheckElementPattern(puzzleShower.puzzle)) SolvePuzzle();
        }
    }

    public void SolvePuzzle()
    {
        if (puzzles[currentPuzzleIndex].CheckWinCondition())
        {
            isSolved[currentPuzzleIndex]++;
            levelIndicators[currentPuzzleIndex].color = Color.Lerp(selectedColor, indicatorColor[isSolved[currentPuzzleIndex]], 0.4f);
            Debug.Log("sucess!");
        }
    }

    public void SetPuzzle(int index)
    {
        levelIndicators[currentPuzzleIndex].color = indicatorColor[isSolved[currentPuzzleIndex]];
        currentPuzzleIndex = index;
        while (currentPuzzleIndex < 0) currentPuzzleIndex += puzzles.Length;
        while (currentPuzzleIndex >= puzzles.Length) currentPuzzleIndex -= puzzles.Length;

        puzzleShower.SetPuzzle(puzzles[currentPuzzleIndex]);
        levelIndicators[currentPuzzleIndex].color = Color.Lerp(selectedColor, indicatorColor[isSolved[currentPuzzleIndex]], 0.4f);
    }

    public void ChangePuzzle(int delta)
    {
        levelIndicators[currentPuzzleIndex].color = indicatorColor[isSolved[currentPuzzleIndex]];
        currentPuzzleIndex += delta;
        while (currentPuzzleIndex < 0) currentPuzzleIndex += puzzles.Length;
        while (currentPuzzleIndex >= puzzles.Length) currentPuzzleIndex -= puzzles.Length;
        puzzleShower.SetPuzzle(puzzles[currentPuzzleIndex]);
        levelIndicators[currentPuzzleIndex].color = Color.Lerp(selectedColor, indicatorColor[isSolved[currentPuzzleIndex]], 0.4f);//
    }

    public void SpawnLevelIndicators()
    {
        for (int i = 0; i < puzzles.Length; i++)
        {
            int side = Mathf.FloorToInt(i / levelsPerSide);
            int column = i % levelsPerSide;

            Vector2 start = Quaternion.Euler(Vector3.forward * (side * 60 - 30)) * Vector2.down * radius;
            Vector2 end = Quaternion.Euler(Vector3.forward * ((side + 1) * 60 - 30)) * Vector2.down * radius;
            float rot = side * 60;
            if (column == 0) rot -= 30;
            levelIndicators[i] = Instantiate(levelIndicatorGO, Vector2.Lerp(start, end, (float)column / (float)levelsPerSide), Quaternion.Euler(Vector3.forward * rot), transform).GetComponent<SpriteRenderer>();
            levelIndicators[i].color = indicatorColor[isSolved[i]];
        }
    }
}

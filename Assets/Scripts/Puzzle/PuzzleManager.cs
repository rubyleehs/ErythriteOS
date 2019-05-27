using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Puzzle[] I_puzzles;
    public static Puzzle[] puzzles;
    public PuzzleShower puzzleShower;
    public Transform indicatorParent;
    public GameObject levelIndicatorGO;

    public int levelsPerSide;
    public float radius;
    public Color selectedColor;
    public Color[] indicatorColor;
    [HideInInspector]
    public SpriteRenderer[] levelIndicators;

    public static int[] isSolved;

    [HideInInspector]
    public static int currentPuzzleIndex = 0;

    [HideInInspector]
    public new Transform transform;

    private void Awake()
    {
        puzzles = I_puzzles;
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
            SubmitPuzzle();
        }
    }

    public void SubmitPuzzle()
    {
        if (CheckIfCurrentCanBeSubmitted())
        {
            isSolved[currentPuzzleIndex]++;
            levelIndicators[currentPuzzleIndex].color = Color.Lerp(selectedColor, indicatorColor[isSolved[currentPuzzleIndex]], 0.4f);
            HistoryManager.bSubmit.interactable = false;
            //Debug.Log("sucess!");

            for (int i = 0; i < isSolved.Length; i++)
            {
                if (isSolved[i] <= 0)
                {
                    Debug.Log(i);
                    return;
                }
            }

            GameManager.EndOfGame(true);
        }
        //else Debug.Log("fail");
    }

    public void SetPuzzle(int index)
    {
        levelIndicators[currentPuzzleIndex].color = indicatorColor[isSolved[currentPuzzleIndex]];
        currentPuzzleIndex = index;
        while (currentPuzzleIndex < 0) currentPuzzleIndex += puzzles.Length;
        while (currentPuzzleIndex >= puzzles.Length) currentPuzzleIndex -= puzzles.Length;

        puzzleShower.SetPuzzle(puzzles[currentPuzzleIndex]);
        levelIndicators[currentPuzzleIndex].color = Color.Lerp(selectedColor, indicatorColor[isSolved[currentPuzzleIndex]], 0.4f);

        HistoryManager.bSubmit.interactable = CheckIfCurrentCanBeSubmitted();
        //HistoryManager.bClear.onClick.Invoke();
    }

    public void ChangePuzzle(int delta)
    {
        levelIndicators[currentPuzzleIndex].color = indicatorColor[isSolved[currentPuzzleIndex]];
        currentPuzzleIndex += delta;
        while (currentPuzzleIndex < 0) currentPuzzleIndex += puzzles.Length;
        while (currentPuzzleIndex >= puzzles.Length) currentPuzzleIndex -= puzzles.Length;
        puzzleShower.SetPuzzle(puzzles[currentPuzzleIndex]);
        levelIndicators[currentPuzzleIndex].color = Color.Lerp(selectedColor, indicatorColor[isSolved[currentPuzzleIndex]], 0.4f);

        HistoryManager.bSubmit.interactable = CheckIfCurrentCanBeSubmitted();
        //HistoryManager.bClear.onClick.Invoke();
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
            levelIndicators[i] = Instantiate(levelIndicatorGO, Vector2.Lerp(start, end, (float)column / (float)levelsPerSide) + (Vector2)indicatorParent.position, Quaternion.Euler(Vector3.forward * rot), indicatorParent).GetComponent<SpriteRenderer>();
            levelIndicators[i].color = indicatorColor[isSolved[i]];
        }
    }

    public static bool CheckIfCurrentCanBeSubmitted()
    {
        //Debug.Log("PCheck Start");
        if (isSolved[currentPuzzleIndex] > 0) return false;
        else
        {
            //Debug.Log("PCheck Not Solved");
            if (!puzzles[currentPuzzleIndex].CheckWinCondition()) return false;
            else
            {
                //Debug.Log(puzzles[currentPuzzleIndex].name);

                return BoardOS.FindElementPattern(PuzzleShower.puzzle);
            }
        }
    }
}

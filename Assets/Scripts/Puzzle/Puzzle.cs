using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle")]
public class Puzzle : ScriptableObject
{
    [TextArea]
    public string code;

    public Sprite sprite;

    public virtual bool CheckWinCondition()
    {
        return true;
    }
}

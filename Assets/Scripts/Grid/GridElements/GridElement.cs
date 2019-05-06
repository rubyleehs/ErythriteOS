using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridElement : ScriptableObject
{
    public int id;
    public Sprite sprite;

    public abstract void Run(HexBoardTile tile);
}

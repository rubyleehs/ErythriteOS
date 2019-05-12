using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public Material material;

    public virtual void Run(HexBoardTile tile) //function held in tile
    {
        Debug.Log("You forgot to override Run()");
    }
    public virtual void BattleCry(HexBoardTile tile) { } //Stuff that happen when a tile becomes this.

    public virtual void Deathrattle(HexBoardTile tile) { } //Stuff that happen when said tile become something else

    public virtual void OnTurnEnd(HexBoardTile tile) { } //Stuff that happen on every turn;
}

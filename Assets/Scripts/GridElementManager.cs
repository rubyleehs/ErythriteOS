using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElementManager : MonoBehaviour
{
    public static GridElement[] elements;
    public GridElement[] I_elements;

    private void Awake()
    {
        elements = I_elements;
        for (int i = 0; i < elements.Length; i++)
        {

            elements[i].id = i;
        }
    }

    public IEnumerator Run(HexTile tile)
    {
        GameManager.allowBoardUpdateAnim = false;
        yield return new WaitForEndOfFrame();
        GameManager.allowBoardUpdateAnim = true;
        if (tile.elementID >= 0) StartCoroutine(elements[tile.elementID].Run(tile));
    }
}

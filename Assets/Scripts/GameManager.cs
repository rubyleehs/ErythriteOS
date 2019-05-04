using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static HexGrid hexGrid;
    public HexGrid I_hexGrid;

    public static bool allowBoardUpdateAnim = true;
    public static float boardUpdateAnimDur = 0.65f;

    private void Awake()
    {
        hexGrid = I_hexGrid;
    }

    private void Update()
    {
        HexTile ht = hexGrid.WorldPosToGrid(MainCamera.GetMouseWorld2DPoint());
        Debug.Log(ht);
        if (ht != null)
        {
            for (int i = 0; i < 6; i++)
            {
                if (ht.adjTiles[i] != null) Debug.DrawLine(ht.worldPos, ht.adjTiles[i].worldPos, Color.red, 1);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                ht.UpdateElement(0);
                StartCoroutine(hexGrid.gridElementManager.Run(ht));
            }
        }
    }

}

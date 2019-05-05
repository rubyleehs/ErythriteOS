using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AnimationInfo
{
    public float staggerAnimDuration;

    public float tileLightUpDuration;

    public Color tileLightUpColor;
    public Color occupiedTileColor;

}

public class GameManager : MonoBehaviour
{
    public static AnimationInfo animationInfo;
    public AnimationInfo I_animationInfo;

    public static TileVisualUpdateSequencer tvus;
    public TileVisualUpdateSequencer I_tvus;

    public static ShotgunSurgery shotgunSurgery;
    public ShotgunSurgery I_shotgunSurgery;

    private void Awake()
    {
        animationInfo = I_animationInfo;
        tvus = I_tvus;
        shotgunSurgery = I_shotgunSurgery;
    }

    private void Update()
    {
        HexTile ht = GridOS.hexGrid.WorldPosToGrid(MainCamera.GetMouseWorld2DPoint());
        if (ht != null)
        {
            for (int i = 0; i < 6; i++)
            {
                if (ht.adjTiles[i] != null) Debug.DrawLine(ht.worldPos, ht.adjTiles[i].worldPos, Color.red, 1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                ht.UpdateElement(0);
                GridOS.Run(ht);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ht.UpdateElement(1);
                GridOS.Run(ht);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ht.UpdateElement(2);
                GridOS.Run(ht);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ht.UpdateElement(3);
                GridOS.Run(ht);
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log(ht.ReadElementID());
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)) GridHistory.Undo();
        else if (Input.GetKeyDown(KeyCode.Y)) GridHistory.Redo();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AnimationInfo
{
    public float staggerAnimDuration;

    public float tileLightUpDuration;
    public float tileElementFadeDuration;

    public Color tileLightUpColor;
    public Color occupiedTileColor;

}

public class GameManager : MonoBehaviour
{
    public static AnimationInfo animationInfo;
    public AnimationInfo I_animationInfo;

    public static ShotgunSurgery shotgunSurgery;
    public ShotgunSurgery I_shotgunSurgery;

    private void Awake()
    {
        animationInfo = I_animationInfo;
        shotgunSurgery = I_shotgunSurgery;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) BoardOS.TryUpdateElement(MainCamera.mousePos, 0, true);
        else if (Input.GetKeyDown(KeyCode.Alpha1)) BoardOS.TryUpdateElement(MainCamera.mousePos, 1, true);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) BoardOS.TryUpdateElement(MainCamera.mousePos, 2, true);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) BoardOS.TryUpdateElement(MainCamera.mousePos, 3, true);
    }  
}

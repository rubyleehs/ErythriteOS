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

    public static Transform endGameScreen;
    public Transform I_endGameScreen;//

    private void Awake()
    {
        animationInfo = I_animationInfo;
        shotgunSurgery = I_shotgunSurgery;
        endGameScreen = I_endGameScreen;
    }
    
    public static void EndOfGame(bool isEnd)
    {
        PlayerInteractions.canInteractWithElements = !isEnd;
        endGameScreen.gameObject.SetActive(isEnd);
    }

    public void ContinueGame() //Because UNITY UI BUTTON FOR SOME REASON DONT LIKE STATIC FUNCTIONS
    {
        EndOfGame(false);
    }
}

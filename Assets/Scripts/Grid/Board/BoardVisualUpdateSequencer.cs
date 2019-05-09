using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardVisualUpdateSequencer : MonoBehaviour
{
    private static List<List<HexBoardTile>> queue;

    private float dt = 0;

    private void Awake()
    {
        queue = new List<List<HexBoardTile>>();
    }

    private void Update()
    {
        if (queue.Count > 0)
        {
            if (dt < GameManager.animationInfo.staggerAnimDuration) dt += Time.deltaTime;
            else
            {
                dt = 0;

                for (int i = 0; i < queue[0].Count; i++)
                {
                    if(queue[0][i] != null) queue[0][i].UpdateVisuals();
                }
                queue.RemoveAt(0);
            }
        }
        else dt = 0;
    }

    public void ForceComplete()
    {
        while(queue.Count >0)
        {
            for (int i = 0; i < queue[0].Count; i++)
            {
                if (queue[0][i] != null) queue[0][i].UpdateVisuals();
            }
            queue.RemoveAt(0);
        }
    }


    public static void AddToQueue(List<List<HexBoardTile>> list)
    {
        queue.AddRange(list);
    }

    public static void AddToQueue(HexBoardTile tile)
    {
        queue.Add(new List<HexBoardTile> { tile });
    }

    public static void AddToQueue(List<HexBoardTile> l)
    {
        queue.Add(new List<HexBoardTile>(l));
    }
}

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
        if (tile == null) return;
        queue.Add(new List<HexBoardTile> { tile });
    }

    public static void AddToQueue(List<HexBoardTile> l)
    {
        if(l.Count == 0) return;
        queue.Add(new List<HexBoardTile>(l));
    }

    public static void JoinLastQueueMember(HexBoardTile tile)
    {
        if (queue.Count == 0) queue.Add(null);
        queue[queue.Count - 1].Add(tile);
    }

    public static void JoinLastQueueMember(List<HexBoardTile> l)
    {
        if (queue.Count == 0) queue.Add(null);
        queue[queue.Count - 1].AddRange(l);
    }
}

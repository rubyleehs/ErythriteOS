using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisualUpdateSequencer : MonoBehaviour
{
    private static List<List<HexTile>> queue;

    private float dt = 0;

    private void Awake()
    {
        queue = new List<List<HexTile>>();
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


    public static void AddToQueue(HexTile tile)
    {
        queue.Add(new List<HexTile> { tile });
    }

    public static void AddToQueue(List<HexTile> l)
    {
        queue.Add(l);
    }
}

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
        for (int i = 0; i < elements.Length; i++) elements[i].id = i;
    }
}

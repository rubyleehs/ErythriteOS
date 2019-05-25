using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Image i;

    private void Awake() {
        i = GetComponent<Image>();
        i.alphaHitTestMinimumThreshold = 0.01f;
    }
}

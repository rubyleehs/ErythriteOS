using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class Button : MonoBehaviour
{
    public KeyCode key;
    public float deadPeriod = 1f;
    public float checkPeriod = 0.15f;

    private Image i;
    private UnityEngine.UI.Button b;
    private float t;

    private void Awake() {
        i = GetComponent<Image>();
        i.alphaHitTestMinimumThreshold = 0.01f;
        b = GetComponent<UnityEngine.UI.Button>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            EmulateButtonClick();
            t = 0;
        }
        else if (Input.GetKeyUp(key))
        {
            FadeColor(b.colors.normalColor);
        }
        else if (Input.GetKey(key))
        {
            t += Time.deltaTime;
            if(t > deadPeriod)
            {
                EmulateButtonClick();
                t -= checkPeriod;
            }
        }
    }

    private void EmulateButtonClick()
    {
        FadeColor(b.colors.pressedColor);
        b.onClick.Invoke();
    }

    private void FadeColor(Color color)
    {
        Graphic g = GetComponent<Graphic>();
        g.CrossFadeColor(color, b.colors.fadeDuration, true, true);
    }
}

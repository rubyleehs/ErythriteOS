using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public float period;
    public Transform topPanel;
    public Transform bottomPanel;
    public float distance;

    private Vector2 a;
    private Vector2 b;

    private void Awake()
    {
        a = topPanel.position;
        b = bottomPanel.position;
        PlayerInteractions.canInteractWithElements = false;
    }

    public void Open()
    {
        PlayerInteractions.canInteractWithElements = true;
        StartCoroutine(OpenAnim());
    }

    public IEnumerator OpenAnim()
    {
        float t = 0;
        float smoothProgress = 0;
        while(smoothProgress < 1)
        {
            t += Time.deltaTime;
            smoothProgress = Mathf.SmoothStep(0, 1, t / period);
            topPanel.position = Vector2.Lerp(a, a + Vector2.up * distance, smoothProgress);
            bottomPanel.position = Vector2.Lerp(b, b + Vector2.down * distance, smoothProgress);
            yield return null;
        }
    }
}

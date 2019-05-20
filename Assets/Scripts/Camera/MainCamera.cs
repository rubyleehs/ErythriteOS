using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public static Vector2 mousePos;
    public new static Camera camera;
    public new static Transform transform;
    public static float width;//world space
    public static float height;
    public static Vector2 TR;
    public static Vector2 BL;

    [Header("Positioning")]
    public float z;
    public Transform camRig;

    [Header("Audio")]
    public float masterVolume;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        transform = GetComponent<Transform>();

        if (height > 0) camera.orthographicSize = height;
        else height = camera.orthographicSize;

        width = height * camera.aspect;
        TR = camera.ScreenToWorldPoint(Vector3.one);
        BL = camera.ScreenToWorldPoint(Vector3.zero);
    }

    private void Start()
    {
        AudioListener.volume = masterVolume;
    }

    private void Update()
    {
        mousePos = GetMouseWorld2DPoint();
    }

    public void SetHeight(float value)
    {
        height = value;
        width = height * camera.aspect;
        camera.orthographicSize = height;
    }

    public void SetPosition(Vector3 position)
    {
       camRig.position = new Vector3(position.x, position.y, position.z);
        TR = camera.ScreenToWorldPoint(Vector3.one);
        BL = camera.ScreenToWorldPoint(Vector3.zero);
    }
    public void SetPosition(Vector2 position)
    {
       SetPosition(new Vector3(position.x, position.y,camRig.position.z));
    }

    protected static Vector2 GetMouseWorld2DPoint()
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public static bool IsOnCamera(Vector3 pos, float leeway)
    {
        Vector2 screenPos = camera.WorldToViewportPoint(pos);
        if (screenPos.x < -leeway || screenPos.x > 1 + leeway || screenPos.y < -leeway || screenPos.y > 1 + leeway) return false;
        else return true;
    }
}

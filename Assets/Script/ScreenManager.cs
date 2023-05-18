using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;
    public Vector2 ScreenSize = Vector2.zero;
    List<Action<Vector2, Vector2>> ScreeenChangeEvents = new List<Action<Vector2, Vector2>>();
    bool firstRun = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void addEventListener(Action<Vector2, Vector2> action)
    {
        Debug.Log("Event added");
        ScreeenChangeEvents.Add(action);
    }

    void Start()
    {
        ScreenSize = new Vector2(Screen.width, Screen.height);
        StartCoroutine("ScreenChangeControl");
    }

    public void UpdateScreenSizeAndCallEvent(bool forceRun = false)
    {
        if (ScreenSize.x != Screen.width || ScreenSize.y != Screen.height)
        {
            Vector2 nScreenSize = new Vector2(Screen.width, Screen.height);
            foreach (Action<Vector2, Vector2> action in ScreeenChangeEvents)
                action(nScreenSize, ScreenSize);
            ScreenSize = nScreenSize;
        }
        if(forceRun)
            foreach (Action<Vector2, Vector2> action in ScreeenChangeEvents)
                action(ScreenSize, ScreenSize);
    }

    IEnumerator ScreenChangeControl()
    {
        while (true)
        {
            ScreenManager.Instance.UpdateScreenSizeAndCallEvent();
            yield return new WaitForSeconds(2);
        }
    }


    public static ScreenPoses GetScreenPoses()
    {
        ScreenPoses pos = new ScreenPoses();
        pos.BottomLeft = Camera.main.ViewportToWorldPoint(Vector3.zero);
        pos.TopRight = Camera.main.ViewportToWorldPoint(Vector3.one);
        pos.TopLeft = new Vector3(pos.BottomLeft.x, pos.TopRight.y, 0);
        pos.BottomRight = new Vector3(pos.TopRight.x, pos.BottomLeft.y, 0);
        return pos;
    }

}

public class ScreenPoses
{
    public Vector2 TopLeft;
    public Vector2 BottomLeft;
    public Vector2 TopRight;
    public Vector2 BottomRight;
}
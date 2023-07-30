using System;
using UnityEngine;

public class ApplicationQuitOrPause : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private static Action _quitActions;

    public static void Add(Action action)
    {
        _quitActions += action;
    }

    public static void Remove(Action action)
    {
        _quitActions -= action;
    }

    public static void RemoveAll()
    {
        foreach (Action quitAction in _quitActions.GetInvocationList())
        {
            _quitActions -= quitAction;
        }
    }

    private static void InvokeQuitActions()
    {
        _quitActions?.Invoke();
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            InvokeQuitActions();
        }
    }
#endif

#if UNITY_EDITOR || UNITY_IOS
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            InvokeQuitActions();
        }
    }

    private void OnApplicationQuit()
    {
        InvokeQuitActions();
    }
#endif
}
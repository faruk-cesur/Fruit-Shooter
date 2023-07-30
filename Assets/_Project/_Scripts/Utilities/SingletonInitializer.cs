using System;
using UnityEngine;
using System.Collections.Generic;

public class SingletonInitializer : MonoBehaviour
{
    [SerializeField] private List<SingletonBase> _singletons = new List<SingletonBase>();

    private void Awake() => _singletons.ForEach(s => s?.Init());
}
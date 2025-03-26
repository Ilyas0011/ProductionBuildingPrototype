using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;

public interface IInitializable
{
    public bool IsReady { get; set; }
    Task Init();

    void FinishInit()
    {
        IsReady = true;
        Debug.Log($"Service initialized successfully: {GetType().Name}");
    }
}

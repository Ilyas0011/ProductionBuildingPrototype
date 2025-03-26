using System;
using System.Threading.Tasks;
using UnityEngine;

public class InputManager: IInitializable
{
    private UnityCallbackService _unityCallbackService;

    private bool isInputEnabled;

    public Action Move;

    public bool IsReady { get; set; }
    public bool DontAutoInit { get; }

    public Task Init()
    {
        _unityCallbackService = ServiceLocator.Get<UnityCallbackService>();

        _unityCallbackService.FrameUpdated += InputUpdate;

        SetInputEnabled(true);

        return Task.CompletedTask;
    }

    public void SetInputEnabled(bool isEnabled) => isInputEnabled = isEnabled;

    public void InputUpdate()
    {
        if (isInputEnabled)
        {
            if (Input.GetMouseButtonDown(0)) Move?.Invoke();
        }
    }
}

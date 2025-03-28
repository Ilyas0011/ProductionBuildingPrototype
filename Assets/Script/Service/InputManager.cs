using System;
using System.Threading.Tasks;
using UnityEngine;

public class InputManager: IInitializable
{
    private UnityCallbackService _unityCallbackService;

    private bool isInputEnabled;

    public Action Move;
    public Action<float, float> MoveCamera;
    
    public bool IsReady { get; set; }
    public bool DontAutoInit { get; }

    private bool isMobile;

    public Task Init()
    {
        _unityCallbackService = ServiceLocator.Get<UnityCallbackService>();

        _unityCallbackService.FrameUpdated += InputUpdate;

        SetInputEnabled(true);

        return Task.CompletedTask;
    }

    public void CheckInputType()
    {
        #if UNITY_ANDROID || UNITY_IOS
                     isMobile = true;
        #else
                isMobile = false;
        #endif
    }

    public void SetInputEnabled(bool isEnabled)
    {
        isInputEnabled = isEnabled;
    }

    public void InputUpdate()
    {
        if(isMobile)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
                Move?.Invoke();

            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                float deltaX = (touch0.deltaPosition.x + touch1.deltaPosition.x) * 0.5f;
                float deltaY = (touch0.deltaPosition.y + touch1.deltaPosition.y) * 0.5f;

               MoveCamera?.Invoke(deltaX, deltaY);
            }
        }
        else
        {
            if (isInputEnabled)
            {
                if (Input.GetMouseButtonDown(0)) Move?.Invoke();

                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

               MoveCamera?.Invoke(horizontal, vertical);
            }
        }
    }
}

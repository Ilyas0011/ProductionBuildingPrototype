using System;
using System.Threading.Tasks;
using UnityEngine;

public class InputService
{
    private UnityCallbackService _unityCallbackService;

    private bool isInputEnabled;

    public Action Move;
    public Action<float, float> MoveCamera;
    
    public bool IsReady { get; set; }
    public bool DontAutoInit { get; }

    public InputService()
    {
        _unityCallbackService = ServiceLocator.Get<UnityCallbackService>();

        if(IsMobileType() == true)
            _unityCallbackService.FrameUpdated += MobileInputUpdate;
        else
            _unityCallbackService.FrameUpdated += DesktopUpdate;

        SetInputEnabled(true);
    }

    public bool IsMobileType() =>  Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    public void SetInputEnabled(bool isEnabled) => isInputEnabled = isEnabled;
    private void MobileInputUpdate()
    {
        if (!isInputEnabled)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            float deltaX = -touch.deltaPosition.x * 0.1f;
            float deltaY = -touch.deltaPosition.y * 0.1f;

            MoveCamera?.Invoke(deltaX, deltaY);
        }
        else if (touch.phase == TouchPhase.Ended && touch.deltaPosition.magnitude == 0)
        {
            Move?.Invoke();
        }
    }

    private void DesktopUpdate()
    {
        if (!isInputEnabled)
            return;

        if (Input.GetMouseButtonDown(0)) Move?.Invoke();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        MoveCamera?.Invoke(horizontal, vertical);
    }
}

using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private InputService _inputManager;

    [SerializeField] private float _moveSpeed = 5f;

    public void Awake()
    {
        _inputManager = ServiceLocator.Get<InputService>();
        _inputManager.MoveCamera += MoveCamera;

        if (_inputManager.IsMobileType())
            _moveSpeed = 20f;
        else
            _moveSpeed = 40f;
    }

    private void MoveCamera(float horizontal, float vertical)
    {
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        transform.Translate(moveDirection * Time.deltaTime * _moveSpeed, Space.World);
    }

    private void OnDestroy() => _inputManager.MoveCamera -= MoveCamera;
}

using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private InputManager _inputManager;

    [SerializeField] private float _moveSpeed = 5f;

    private void Awake()
    {
        _inputManager = ServiceLocator.Get<InputManager>();
        _inputManager.MoveCamera += MoveCamera;
    }

    private void MoveCamera(float horizontal, float vertical)
    {
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        transform.Translate(moveDirection * Time.deltaTime * _moveSpeed, Space.World);
    }
}

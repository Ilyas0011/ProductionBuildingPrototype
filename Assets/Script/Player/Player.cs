using UnityEngine;

public class Player : MonoBehaviour
{
    private MovementController _movementController;
    private InputManager _inputManager;

    private void Awake() => _movementController = GetComponent<MovementController>();
    private void Start()
    {
        _inputManager = ServiceLocator.Get<InputManager>();

        _inputManager.Move += _movementController.MoveTo;
    }

    private void OnDestoy() => _inputManager.Move -= _movementController.MoveTo;
}

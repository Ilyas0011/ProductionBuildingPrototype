using UnityEngine;

public class Player : MonoBehaviour
{
    private MovementController _movementController;
    private AnimationController _animationController;
    private InputManager _inputManager;

    public void Init()
    {
        _movementController = GetComponent<MovementController>();
        _animationController = GetComponent<AnimationController>();
        _inputManager = ServiceLocator.Get<InputManager>();

        _inputManager.Move += _movementController.MoveTo;
    }

    private void OnDestroy() => _inputManager.Move -= _movementController.MoveTo;
}

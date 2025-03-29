using UnityEngine;

public class Player : MonoBehaviour
{
    private MovementController _movementController;
    private AnimationController _animationController;
    private InputService _inputManager;

    public void Init()
    {
        _movementController = GetComponent<MovementController>();
        _animationController = GetComponent<AnimationController>();
        _inputManager = ServiceLocator.Get<InputService>();

        _inputManager.Move += _movementController.MoveTo;
    }

    private void OnDestroy() => _inputManager.Move -= _movementController.MoveTo;
}

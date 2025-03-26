using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _playerAnimator;

    private void Awake() => _playerAnimator = GetComponent<Animator>();


}

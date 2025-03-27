using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _playerAnimator;

    public void Awake() => _playerAnimator = GetComponent<Animator>();
    public void PlayWalking(bool isWalking) => _playerAnimator.SetBool("isWalking", isWalking);
}

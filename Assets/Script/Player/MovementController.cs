using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private AnimationController _animationContoller;
    private Camera _camera;

    private void Awake()
    {
       agent = GetComponent<NavMeshAgent>();
        _animationContoller = GetComponent<AnimationController>();
       _camera = Camera.main;
    }

    public void MoveTo()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }
    }

    private void Update()
    {
        if (agent.velocity.sqrMagnitude == 0f)
            _animationContoller.PlayWalking(false);
        else
            _animationContoller.PlayWalking(true);
    }
}

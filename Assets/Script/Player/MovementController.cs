using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera _camera;

    private void Awake()
    {
       agent = GetComponent<NavMeshAgent>();
       _camera = Camera.main;
    }

    public void MoveTo()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            agent.SetDestination(hit.point);
    }
}

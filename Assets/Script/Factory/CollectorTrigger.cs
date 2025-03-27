using System;
using UnityEngine;

public class CollectorTrigger : MonoBehaviour
{
    public Action TriggerEntered;
    public Action TriggerExit;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
            TriggerEntered?.Invoke();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
            TriggerExit?.Invoke();
    }
}

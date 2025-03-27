using System;
using UnityEngine;

public class CollectorTrigger : MonoBehaviour
{
    public Action TriggerEntered;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerEntered?.Invoke();
        }
    }
}

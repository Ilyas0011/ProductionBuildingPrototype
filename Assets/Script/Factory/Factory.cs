using System;
using System.Threading;
using TMPro;
using UnityEngine;

public abstract class Factory : MonoBehaviour, IFactory
{
    [SerializeField] protected float _productionTime = 2f;

    public event Action<ResourceType, int> ResourceCollected;
    public event Action<int> ResourceProduced;
    public event Action ResourcesReset;

    protected abstract ResourceType _resourceType { get; }

    private int _resourceAmount = 0;
    private float _timeSinceLastProduction;

    private CollectorTrigger _trigger;

    private void Awake()
    {
        _trigger = GetComponentInChildren<CollectorTrigger>();
        _trigger.TriggerEntered += CollectingResource;

        ResetProductionValues();
    }
    private void ResetProductionValues()
    {
        _resourceAmount = 0;
        _timeSinceLastProduction = _productionTime;
    }

    private void Update()
    {
        _timeSinceLastProduction += Time.deltaTime;

        if (_timeSinceLastProduction > _productionTime)
            ProduceResource();
    }

    private void ProduceResource()
    {
        _resourceAmount++;
        Debug.Log(_resourceAmount);
        ResourceProduced?.Invoke(_resourceAmount);
        _timeSinceLastProduction = 0f;
    }

    public void CollectingResource()
    {
        if (_resourceAmount != 0)
        {
            ResourceCollected?.Invoke(_resourceType, _resourceAmount);
            _resourceAmount = 0;
            ResourcesReset?.Invoke();
            Debug.Log(_resourceAmount);
        }
    }
}

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

    [SerializeField] protected ResourceType _resourceType;
    public ResourceType ResourceType => _resourceType;

    private int _resourceAmount = 0;
    private float _timeSinceLastProduction;

    private CollectorTrigger _trigger;

    private SavesService _savesService;

    public void Init()
    {
        _savesService = ServiceLocator.Get<SavesService>();
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
        ResourceProduced?.Invoke(_resourceAmount);
        _timeSinceLastProduction = 0f;
    }

    public void CollectingResource()
    {
        if (_resourceAmount != 0)
        {
            _savesService.AddResource(ResourceType, _resourceAmount);
            _resourceAmount = 0;
            ResourcesReset?.Invoke();
        }
    }
}

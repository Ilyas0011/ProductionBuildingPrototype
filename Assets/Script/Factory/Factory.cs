using System;
using System.Threading;
using TMPro;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    [SerializeField] protected float _productionTime = 2f;

    public event Action<int> ResourceProduced;
    public event Action OpenResourceScreen;
    public event Action CloseResourceScreen;
    public event Action ResourcesReset;

    [SerializeField] protected ResourceType _resourceType;
    public ResourceType ResourceType => _resourceType;

    private int _resourceAmount = 0;
    private float _timeSinceLastProduction;

    private CollectorTrigger _trigger;
    private AudioService _audioService;
    private SavesService _savesService;

    private bool isCollectorTrigger = false;

    public void Init()
    {
        _audioService = ServiceLocator.Get<AudioService>();
        _savesService = ServiceLocator.Get<SavesService>();
        _trigger = GetComponentInChildren<CollectorTrigger>();

        ResetProductionValues();
    }

    private void OnEnable()
    {
        _trigger.TriggerEntered += TriggerEntry;
        _trigger.TriggerExit += TriggerExit;
    }

    private void OnDisable()
    {
        _trigger.TriggerEntered -= TriggerEntry;
        _trigger.TriggerExit -= TriggerExit;
    }

    private void TriggerEntry() => isCollectorTrigger = true;
    private void TriggerExit()
    {
        isCollectorTrigger = false;
        CloseResourceScreen?.Invoke();
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

        if (isCollectorTrigger)
            CollectingResource();
    }

    private void ProduceResource()
    {
        _resourceAmount++;
        ResourceProduced?.Invoke(_resourceAmount);
        _timeSinceLastProduction = 0f;
    }

    public void CollectingResource()
    {
        if (_resourceAmount > 1)
        {
            _savesService.AddResource(ResourceType, _resourceAmount);
            OpenResourceScreen?.Invoke();
            _audioService.PlaySounds(AudioIdentifier.ResourceCollect);
            _resourceAmount = 0;
            ResourcesReset?.Invoke();
        }
    }
}

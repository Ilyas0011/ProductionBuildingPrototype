using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameEntry : MonoBehaviour
{
    [SerializeField] private Config _config;
    private static bool EntryInit = false;

    private void Awake()
    {
        if (EntryInit)
            return;

        Application.targetFrameRate = 60;

        ServiceLocator.Register(this);
        ServiceLocator.Register(_config);
        _config.Init();

        RegisterServices();
        InitServices();

        EntryInit = true;
    }

    private List<ServiceExecutionStage> _serviceRegistrationOrder = new()
    {
         new(typeof(UnityCallbackService)),
         new(typeof(SavesService)),
         new(typeof(InputService)),
         new(typeof(ViewService)),
         new(typeof(AudioService)),
         new(typeof(UIService))

    };
    class ServiceExecutionStage
    {
        public readonly Type ServiceType;
        public object ServiceInstance { get; private set; }
        public ServiceExecutionStage(Type serviceType) => ServiceType = serviceType;

        public object CreateInstance()
        {
            if (ServiceInstance != null)
                throw new Exception($"Service of {ServiceType} type has already instanced.");

            if (ServiceType.IsSubclassOf(typeof(MonoBehaviour)))
            {
                var container = new GameObject();
                container.name = $"[{ServiceType.Name}]";
                ServiceInstance = container.AddComponent(ServiceType);
                Object.DontDestroyOnLoad(container);
            }
            else
            {
                ServiceInstance = Activator.CreateInstance(ServiceType);
            }
            return ServiceInstance;
        }
    }

    private void RegisterServices()
    {
        foreach (var order in _serviceRegistrationOrder)
        {
         
            try
            {
                var instance = order.CreateInstance();
                ServiceLocator.Register(instance);
            }
            catch (Exception e)
            {
                Debug.LogError($"[Service Registration] Error: {e}");
            }
        }

    }

    private void InitServices()
    {
        foreach (var order in _serviceRegistrationOrder)
        {
            try
            {
                if (order.ServiceInstance is not IInitializable initableService) continue;
            }
            catch (Exception e)
            {
                Debug.LogError($"[Service Initialization] Error: {e}");
            }
        }
    }
}
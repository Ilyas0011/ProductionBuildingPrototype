using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using Object = UnityEngine.Object;

public class GameEntry : MonoBehaviour
{
    [SerializeField] private Config _config;

    void Awake()
    {
        ServiceLocator.Register(this);
        ServiceLocator.Register(_config);
        _config.Init();

        RegisterServices();
        InitServices();
    }

    private List<ServiceExecutionStage> _serviceRegistrationOrder = new()
    {
         new(typeof(UnityCallbackService)),
         new(typeof(InputManager)),
         new(typeof(UIService)),
         new(typeof(SavesService)),
         new(typeof(ScreenManager))
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

    void RegisterServices()
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

    async void InitServices()
    {
        foreach (var order in _serviceRegistrationOrder)
        {
            try
            {
                if (order.ServiceInstance is not IInitializable initableService) continue;

                await initableService.Init();
                initableService.FinishInit();
            }
            catch (Exception e)
            {
                Debug.LogError($"[Service Initialization] Error: {e}");
            }
        }
    }
}
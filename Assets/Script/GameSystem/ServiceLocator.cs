using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register(object service)
    {
        Type serviceType = service.GetType();
        if (_services.ContainsKey(serviceType))
            throw new InvalidOperationException($"The {serviceType} service is already registered.");

        _services[serviceType] = service;
    }

    public static T Get<T>()
    {
        Type serviceType = typeof(T);
        if (_services.TryGetValue(serviceType, out var instance))
            return (T)instance;

        throw new Exception($"Service {serviceType} not found.");
    }
}

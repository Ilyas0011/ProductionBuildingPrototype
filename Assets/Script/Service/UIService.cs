using System;
using System.Collections.Generic;
using UnityEngine;

public class UIService : MonoBehaviour 
{
    public Action UpdateResource;

    private ViewService _screenManager;
    private List<Factory> _factoryList;

    public bool IsReady { get; set; }
    public bool DontAutoInit { get; }

    public void SubscribeToFactoryEvents(List<Factory> factoryList)
    {
        _factoryList = factoryList;

        _screenManager = ServiceLocator.Get<ViewService>();

        foreach (var factory in factoryList)
        {
            factory.OpenResourceScreen += OpenResourceScreen;
            factory.CloseResourceScreen += CloseResourceScrenn;
        }
    }

    private void OnDisable()
    {
        foreach (var factory in _factoryList)
        {
            factory.OpenResourceScreen -= OpenResourceScreen;
            factory.CloseResourceScreen -= CloseResourceScrenn;
        }
    }

    private void OpenResourceScreen()
    {
        if (_screenManager.GetCurrenWindowType() == typeof(ResourceWindow))
            UpdateResource?.Invoke();
        else
            _screenManager.OpenWindow(WindowIdentifier.Resources);
    } 

    private void CloseResourceScrenn() => _screenManager.CloseWindow();

}

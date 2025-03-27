using System;
using System.Collections.Generic;
using UnityEngine;
using static BaseScreen;

public class UIService
{
    public Action UpdateResource;

    private ScreenManager screenManager;
    private List<Factory> _factoryList;

    public void SubscribeToFactoryEvents(List<Factory> factoryList)
    {
        _factoryList = factoryList;

        screenManager = ServiceLocator.Get<ScreenManager>();

        foreach (var factory in factoryList)
        {
            factory.OpenResourceScreen += OpenResourceScreen;
            factory.CloseResourceScreen += CloseResourceScrenn;
        }
    }

    private void OnDestroy()
    {
        foreach (var factory in _factoryList)
        {
            factory.OpenResourceScreen -= OpenResourceScreen;
            factory.CloseResourceScreen -= CloseResourceScrenn;
        }
    }

    private void OpenResourceScreen()
    {
        if (screenManager.GetCurrenScreenType() == typeof(ResourceScreen))
            UpdateResource?.Invoke();
        else
            screenManager.OpenScreen(ScreenIdentifier.Resources);
    } 

    private void CloseResourceScrenn() => screenManager.OpenScreen(ScreenIdentifier.CoreGame);

}

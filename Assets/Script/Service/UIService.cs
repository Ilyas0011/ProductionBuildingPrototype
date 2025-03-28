using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static BaseScreen;

public class UIService: IInitializable
{
    public Action UpdateResource;

    private ScreenManager _screenManager;
    private List<Factory> _factoryList;

    private AudioService _audioService;
    private AudioClip _openScreenClip;
    private Config _config;

    public bool IsReady { get; set; }
    public bool DontAutoInit { get; }

    public Task Init()
    {
        _audioService = ServiceLocator.Get<AudioService>();
        _config = ServiceLocator.Get<Config>();

        _openScreenClip = _config.GetAudioClip(AudioIdentifier.OpenScreen);

        return Task.CompletedTask;
    }

    public void SubscribeToFactoryEvents(List<Factory> factoryList)
    {
        _factoryList = factoryList;

        _screenManager = ServiceLocator.Get<ScreenManager>();

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
        if (_screenManager.GetCurrenScreenType() == typeof(ResourceScreen))
        {
            UpdateResource?.Invoke();
        }
        else
        {
            _audioService.PlaySounds(AudioIdentifier.OpenScreen);
            _screenManager.OpenScreen(ScreenIdentifier.Resources);
        }
    } 

    private void CloseResourceScrenn() => _screenManager.OpenScreen(ScreenIdentifier.CoreGame);

}

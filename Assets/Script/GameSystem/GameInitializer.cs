using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Factory> _factories = new List<Factory>();
    private UIService _uiService;

    private void Awake()
    {
        _player.Init();

        foreach (var factory in _factories)
            factory.Init();

        _uiService = ServiceLocator.Get<UIService>();
        _uiService.SubscribeToFactoryEvents(_factories);
    }
}

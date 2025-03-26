using System;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Device;
using static BaseScreen;

public class ScreenManager : MonoBehaviour, IInitializable
{
    public BaseScreen _currentScreenObject;
    private Transform _canvasTransform;
    private Config _config;

    public bool IsReady { get; set; }
    public bool DontAutoInit { get; }

    public ScreenManager() => _config = ServiceLocator.Get<Config>();

    public Task Init()
    {
        SpawnCanvas();

        OpenScreen(ScreenIdentifier.Menu);
        return Task.CompletedTask;
    }

    private void SpawnCanvas()
    {
        _canvasTransform = Instantiate(_config.CanvasPrefab, transform.position, Quaternion.identity).transform;
        _canvasTransform.SetParent(transform);
    }

    public void OpenScreen(ScreenIdentifier screenIdentifier)
    {
        BaseScreen screen = _config.GetScreenPrefab(screenIdentifier);

        if (_currentScreenObject != null)
            Destroy(_currentScreenObject.gameObject);

        _currentScreenObject = Instantiate(screen, _canvasTransform.position, Quaternion.identity);
        _currentScreenObject.transform.SetParent(_canvasTransform);
    }
}

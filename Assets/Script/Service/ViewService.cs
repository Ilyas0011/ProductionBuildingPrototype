using System;
using System.Threading.Tasks;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Device;

public class ViewService : MonoBehaviour, IInitializable
{
    public BaseScreen _currentScreenObject;
    public BaseWindow _currentWindowsObject;
    private Transform _canvasTransform;
    private Config _config;

    public bool IsReady { get; set; }
    public bool DontAutoInit { get; }

    public Task Init()
    {
        _config = ServiceLocator.Get<Config>();
        SpawnCanvas();

        OpenScreen(ScreenIdentifier.Menu);
        return Task.CompletedTask;
    }

    private void SpawnCanvas()
    {
        _canvasTransform = Instantiate(_config.CanvasPrefab, Vector3.zero, Quaternion.identity).transform;
        _canvasTransform.SetParent(transform);
    }

    public Type GetCurrenWindowType()
    {
        if(_currentWindowsObject != null )
            return _currentWindowsObject.GetType();

        return null;
    }

    public void CloseWindow()
    {
        if (_currentWindowsObject != null)
            Destroy(_currentWindowsObject.gameObject);

        _currentWindowsObject = null;
    }

    public void OpenWindow(WindowIdentifier windowIdentifier)
    {
        BaseWindow window = _config.GetWindowPrefab(windowIdentifier);

        if (_currentWindowsObject != null)
            Destroy(_currentWindowsObject.gameObject);

        _currentWindowsObject = Instantiate(window, Vector3.zero, Quaternion.identity);
        _currentWindowsObject.transform.SetParent(_canvasTransform, false);
    }

    public void OpenScreen(ScreenIdentifier screenIdentifier)
    {
        BaseScreen screen = _config.GetScreenPrefab(screenIdentifier);

        if (_currentScreenObject != null)
            Destroy(_currentScreenObject.gameObject);

        if (_currentWindowsObject != null)
            Destroy(_currentWindowsObject.gameObject);

        _currentScreenObject = Instantiate(screen, Vector3.zero, Quaternion.identity);
        _currentScreenObject.transform.SetParent(_canvasTransform, false);
 
    }
}

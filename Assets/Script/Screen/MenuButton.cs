using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BaseScreen;
using static ScreenManager;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private ScreenIdentifier _screenIdentifier;

    private ScreenManager _screenManager;

    private Button _button;

    private void Awake ()
    {
        _screenManager = ServiceLocator.Get<ScreenManager>();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked() => _screenManager.OpenScreen(_screenIdentifier);
}

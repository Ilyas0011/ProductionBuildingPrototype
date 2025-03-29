using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private ScreenIdentifier _screenIdentifier;

    private ViewService _screenManager;

    private Button _button;

    private void Awake ()
    {
        _screenManager = ServiceLocator.Get<ViewService>();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked() => _screenManager.OpenScreen(_screenIdentifier);
}

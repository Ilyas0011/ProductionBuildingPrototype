using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreen : BaseScreen
{
    public override ScreenIdentifier ID => ScreenIdentifier.Menu;
    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = ServiceLocator.Get<InputManager>();
        _inputManager.SetInputEnabled(false);

        if (SceneManager.GetActiveScene().name != "Menu")
            SceneManager.LoadScene("Menu");
    }
}

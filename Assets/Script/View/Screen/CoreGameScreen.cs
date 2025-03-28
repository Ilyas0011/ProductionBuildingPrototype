using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreGameScreen : BaseScreen
{
    public override ScreenIdentifier ID => ScreenIdentifier.CoreGame;
    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = ServiceLocator.Get<InputManager>();
        _inputManager.SetInputEnabled(true);

        if (SceneManager.GetActiveScene().name != "Game")
            SceneManager.LoadScene("Game");
    }
}

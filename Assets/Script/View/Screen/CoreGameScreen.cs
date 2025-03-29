using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreGameScreen : BaseScreen
{
    public override ScreenIdentifier ID => ScreenIdentifier.CoreGame;
    private InputService _inputManager;

    private void Awake()
    {
        _inputManager = ServiceLocator.Get<InputService>();
        _inputManager.SetInputEnabled(true);

        if (SceneManager.GetActiveScene().name != "Game")
            SceneManager.LoadScene("Game");
    }
}

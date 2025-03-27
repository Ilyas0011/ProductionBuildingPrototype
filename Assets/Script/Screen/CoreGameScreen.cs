using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreGameScreen : BaseScreen
{
    public override ScreenIdentifier ID => ScreenIdentifier.CoreGame;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "Game")
            SceneManager.LoadScene("Game");
    }
}

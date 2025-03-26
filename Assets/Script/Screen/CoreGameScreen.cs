using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreGameScreen : BaseScreen
{
    public override ScreenIdentifier ID => ScreenIdentifier.CoreGame;
    void Awake() => SceneManager.LoadScene("Game");
}

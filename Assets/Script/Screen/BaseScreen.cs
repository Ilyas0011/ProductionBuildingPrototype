using UnityEngine;

public abstract class BaseScreen : MonoBehaviour
{
    public abstract ScreenIdentifier ID { get; }
    public enum ScreenIdentifier
    {
        Menu = 1,
        CoreGame = 2
    }
}

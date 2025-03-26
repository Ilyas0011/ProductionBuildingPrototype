using System;
using UnityEngine;

public class UnityCallbackService : MonoBehaviour
{
    public Action PhysicsUpdated;
    public Action FrameUpdated;
    private void FixedUpdate() => PhysicsUpdated?.Invoke();
    private void Update() => FrameUpdated?.Invoke();
}

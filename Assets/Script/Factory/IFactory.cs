using System;
using UnityEngine;

public interface IFactory
{
    event Action<ResourceType, int> ResourceCollected;
    event Action<int> ResourceProduced;
    event Action ResourcesReset;
}

using UnityEngine;

public class StandartFactory : Factory
{
    protected override ResourceType _resourceType => ResourceType.Battery;

    //  protected override ResourceType _resourceType => throw new System.NotImplementedException();
}

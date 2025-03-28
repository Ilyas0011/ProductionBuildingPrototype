using System;
using System.Collections.Generic;

[Serializable]
public class SavesData
{
    public bool IsMuteAudio;

    public List<ResourceAmount> resourceAmount;

    public SavesData()
    {
        resourceAmount = new List<ResourceAmount>
        {
            new ResourceAmount(ResourceType.Battery, 0),
            new ResourceAmount(ResourceType.Stone, 0),
            new ResourceAmount(ResourceType.Planks, 0),
            new ResourceAmount(ResourceType.Glue, 0),
            new ResourceAmount(ResourceType.Iron, 0)
        };
    }
}

[Serializable]
public class ResourceAmount
{
    public ResourceType resourceType;
    public int amount;

    public ResourceAmount(ResourceType resourceType, int amount)
    {
        this.resourceType = resourceType;
        this.amount = amount;
    }
}
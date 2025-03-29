using System.Threading.Tasks;
using UnityEngine;

public class SavesService
{
    public SavesData Data { get; private set; }

    private const string SaveKey = "SavesData";

    public SavesService() => Load();

    public void Load()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            Data = JsonUtility.FromJson<SavesData>(json);
        }
        else
        {
            Data = new SavesData();
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public bool GetIsMuteMusic() => Data.IsMuteAudio;

    public void SetIsMuteMusc(bool isMute)
    {
        Data.IsMuteAudio = isMute;
        Save();
    }

    public int GetResource(ResourceType resourceType)
    {
        foreach (var resource in Data.resourceAmount)
        {
            if (resource.resourceType == resourceType)
                return resource.amount;
        }
        return 0;
    }


    public void AddResource(ResourceType resourceType, int value)
    {
        foreach (var resource in Data.resourceAmount)
        {
            if (resource.resourceType == resourceType)
            {
                resource.amount += value;
                Save();
                return;
            }
        }

        Data.resourceAmount.Add(new ResourceAmount(resourceType, value));
        Save();
    }
}

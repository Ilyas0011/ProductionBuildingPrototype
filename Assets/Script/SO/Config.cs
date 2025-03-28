using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static BaseScreen;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config")]
public class Config : ScriptableObject
{
    public BaseScreen[] ScreenPrefabs;
    public CanvasPrefab CanvasPrefab;

    public AudioClip[] AudioClip;

    private Dictionary<ScreenIdentifier, BaseScreen> _screenById;
    private Dictionary<AudioIdentifier, AudioClip> _audioClipDictionary;

    public void Init()
    {
        #if UNITY_EDITOR
        ValidationScreenById();
        #endif

        FillScreenDictionary();
        FillAudioDictionary();
    }

    private void FillScreenDictionary()
    {
        _screenById = new Dictionary<ScreenIdentifier, BaseScreen>();

        foreach (var screenPrefab in ScreenPrefabs)
            _screenById[screenPrefab.ID] = screenPrefab;
    }

    private void FillAudioDictionary()
    {
        _audioClipDictionary = new Dictionary<AudioIdentifier, AudioClip>();

        foreach (var clip in AudioClip)
        {
            string name = clip.name;
            if (Enum.TryParse(name, out AudioIdentifier audioIdentifier))
                _audioClipDictionary[audioIdentifier] = clip;
            else
                throw new Exception($"The string {name} does not match.");
        }
    }

    public BaseScreen GetScreenPrefab(ScreenIdentifier screenIdentifier) => _screenById[screenIdentifier];
    public AudioClip GetAudioClip(AudioIdentifier audioIdentifier) => _audioClipDictionary[audioIdentifier];

    private void ValidationScreenById()
    {
        foreach (var screenType in GetAllDerivedTypes())
        {
            bool found = false;
            foreach (var prefab in ScreenPrefabs)
            {

                if (prefab.GetType() == screenType)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                throw new Exception($"The screen {screenType.Name} is not in ScreenPrefabs.");
        }

        var screenID = new HashSet<ScreenIdentifier>();

        foreach (var screenPrefab in ScreenPrefabs)
        {
            if (!screenID.Add(screenPrefab.ID))
                throw new Exception($"Duplicate Screen ID found: {screenPrefab.ID}");
        }
    }

    public List<Type> GetAllDerivedTypes()
    {
        List<Type> allScreenTypes = new();

        Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();

        foreach (var type in allTypes)
        {
            if (type.IsSubclassOf(typeof(BaseScreen)) && !type.IsAbstract)
                allScreenTypes.Add(type);
        }

        return allScreenTypes;
    }

}

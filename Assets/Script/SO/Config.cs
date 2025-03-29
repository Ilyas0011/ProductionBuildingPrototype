using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config")]
public class Config : ScriptableObject
{
    public BaseScreen[] ScreenPrefabs;
    public BaseWindow[] WindowPrefabs;

    public CanvasPrefab CanvasPrefab;

    public AudioClip[] AudioClip;

    private Dictionary<ScreenIdentifier, BaseScreen> _screenById;
    private Dictionary<WindowIdentifier, BaseWindow> _windowById;
    private Dictionary<AudioIdentifier, AudioClip> _audioClipDictionary;

    public void Init()
    {
        #if UNITY_EDITOR
        ValidationScreen();
        ValidationWindow();
        #endif

        FillScreenDictionary();
        FillWindowDictionary();
        FillAudioDictionary();
    }

    private void FillScreenDictionary()
    {
        _screenById = new Dictionary<ScreenIdentifier, BaseScreen>();

        foreach (var screenPrefab in ScreenPrefabs)
            _screenById[screenPrefab.ID] = screenPrefab;
    }
    private void FillWindowDictionary()
    {
        _windowById = new Dictionary<WindowIdentifier, BaseWindow>();

        foreach (var windowPrefab in WindowPrefabs)
            _windowById[windowPrefab.ID] = windowPrefab;
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
    public BaseWindow GetWindowPrefab(WindowIdentifier windowIdentifier) => _windowById[windowIdentifier];
    public AudioClip GetAudioClip(AudioIdentifier audioIdentifier) => _audioClipDictionary[audioIdentifier];

    private void ValidationScreen()
    {
        foreach (var screenType in GetDerivedViewTypes(true))
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

    private void ValidationWindow()
    {
        foreach (var windowType in GetDerivedViewTypes(false))
        {
            bool found = false;
            foreach (var prefab in WindowPrefabs)
            {

                if (prefab.GetType() == windowType)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                throw new Exception($"The screen {windowType.Name} is not in ScreenPrefabs.");
        }

        var windowID = new HashSet<WindowIdentifier>();

        foreach (var windowPrefab in WindowPrefabs)
        {
            if (!windowID.Add(windowPrefab.ID))
                throw new Exception($"Duplicate Screen ID found: {windowPrefab.ID}");
        }
    }

    public List<Type> GetDerivedViewTypes(bool screen)
    {
        List<Type> allViewTypes = new();

        Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();

        if (screen)
        {
            foreach (var type in allTypes)
            {
                if (type.IsSubclassOf(typeof(BaseScreen)) && !type.IsAbstract)
                    allViewTypes.Add(type);
            }
        }else
        {

            foreach (var type in allTypes)
            {
                if (type.IsSubclassOf(typeof(BaseWindow)) && !type.IsAbstract)
                    allViewTypes.Add(type);
            }
        }

        return allViewTypes;
    }

}

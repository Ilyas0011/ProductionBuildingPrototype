using TMPro;
using UnityEngine;

public class ResourceWindow : BaseWindow
{
    [SerializeField] private TextMeshProUGUI[] _resourceText;
    private SavesService _savesService;
    private UIService _uiService;

    public override WindowIdentifier ID => WindowIdentifier.Resources;

    private void Awake()
    {
        _savesService = ServiceLocator.Get<SavesService>();
        _uiService = ServiceLocator.Get<UIService>();
        UpdateResourceText();
    }

    private void UpdateResourceText()
    {
        for (int i = 0; i < _resourceText.Length; i++)
        {
            int resourceID = i + 1;
            string resourceName = ((ResourceType)resourceID).ToString();
            _resourceText[i].text = $"{resourceName}: {_savesService.GetResource((ResourceType)resourceID)}";
        }
    }

    private void OnEnable() => _uiService.UpdateResource += UpdateResourceText;
    private void OnDisable() => _uiService.UpdateResource -= UpdateResourceText;
}
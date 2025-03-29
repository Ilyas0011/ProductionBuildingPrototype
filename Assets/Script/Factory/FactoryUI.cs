using TMPro;
using UnityEngine;

public class FactoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resourceText;

    private Factory _factory;
    private string _resourceName;

    void Awake()
    {
        ResetResourceText();

        _factory = GetComponentInParent<Factory>();
        _resourceName = _factory.ResourceType.ToString();
    }

    private void OnEnable()
    {
        _factory.ResourceProduced += UpdateResourceText;
        _factory.ResourcesReset += ResetResourceText;
    }

    private void OnDisable()
    {
        _factory.ResourceProduced -= UpdateResourceText;
        _factory.ResourcesReset -= ResetResourceText;
    }

    private void UpdateResourceText(int amount) => _resourceText.text = $"{_resourceName}: {amount.ToString()}";

    private void ResetResourceText() => _resourceText.text = $"{_resourceName}: 0";
}

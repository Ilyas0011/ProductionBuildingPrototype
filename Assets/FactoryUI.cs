using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class FactoryUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _resourceAmountText;
    [SerializeField] protected Image _factoryIcon;
    private Factory _factory;

    void Awake()
    {
        ResetResourceText();

        _factory = GetComponentInParent<Factory>();
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

    private void UpdateResourceText(int amount) => _resourceAmountText.text = amount.ToString();

    private void ResetResourceText() => _resourceAmountText.text = "0";
}

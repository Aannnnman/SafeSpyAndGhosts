using UnityEngine;
using UnityEngine.UI;

public class SafeSlotNumberChanger : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _numberText;

    private int _number = 0;
    private const int _one = 1;

    public int Number => _number;

    private void Start()
    {
        _numberText.text = _number.ToString();
        _button.onClick.AddListener(() => IncreaseNumber(_one));
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(() => IncreaseNumber(_one));
    }

    private void IncreaseNumber(int amount)
    {
        _number += amount;

        if (_number > 9)
            _number = 0;

        _numberText.text = _number.ToString();
    }
}

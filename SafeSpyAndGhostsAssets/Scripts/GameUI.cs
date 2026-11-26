using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Game _game;

    [SerializeField] private Text _currencyText;
    [SerializeField] private Text _incomeText;
    [SerializeField] private Button _clickButton;

    private void Start()
    {
        _clickButton.onClick.AddListener(_game.ManualClick);
        _game.OnStateChanged += RefreshUI;

        RefreshUI();
    }

    private void RefreshUI()
    {
        _currencyText.text = $"Crystals: {_game.Currency:F1}";

        _incomeText.text = $"Income: {_game.TotalIncomePerSecond:F1}/sec";
    }

    private void OnDestroy()
    {
        if (_game != null)
            _game.OnStateChanged -= RefreshUI;

        if (_clickButton != null)
            _clickButton.onClick.RemoveListener(_game.ManualClick);
    }
}
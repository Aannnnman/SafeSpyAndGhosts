using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private Game _game;

    [SerializeField] private int _sourceIndex;

    [SerializeField] private Text _levelText;
    [SerializeField] private Text _incomeText;
    [SerializeField] private Text _costText;
    [SerializeField] private Text _statusText;
    [SerializeField] private Button _upgradeButton;

    private void Start()
    {
        _upgradeButton.onClick.AddListener(BuyUpgrade);

        _game.OnStateChanged += RefreshUI;

        RefreshUI();
    }

    private void BuyUpgrade()
    {
        _game.TryUpgrade(_sourceIndex);
    }

    private void RefreshUI()
    {
        IncomeSource source = _game.GetSource(_sourceIndex);

        if (source == null)
            return;

        _levelText.text = $"Level: {source.Level}";
        _incomeText.text = $"Income: {source.IncomePerSecond:F1}/sec";

        _costText.text = $"Cost: {source.NextUpgradeCost:F1}";

        if (!source.IsUnlocked)
        {
            _statusText.text =
                $"Blocked. Requires level " +
                $"{source.Data.RequiredFirstSourceLevel} " +
                "of the first source.";

            _upgradeButton.interactable = false;
        }
        else if (_game.Currency < source.NextUpgradeCost)
        {
            _statusText.text = "Not enough crystals.";
            _upgradeButton.interactable = false;
        }
        else
        {
            _statusText.text = "Upgrade available.";
            _upgradeButton.interactable = true;
        }
    }

    private void OnDestroy()
    {
        if (_game != null)
            _game.OnStateChanged -= RefreshUI;

        if (_upgradeButton != null)
            _upgradeButton.onClick.RemoveListener(BuyUpgrade);
    }
}

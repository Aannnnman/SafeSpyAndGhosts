using UnityEngine;
using UnityEngine.UI;

public class Memory : MonoBehaviour
{
    [SerializeField] private float _requiredCurrency;

    [SerializeField] private Game _game;
    [SerializeField] private Text _requirementText;
    [SerializeField] private Image _unlockedImage;
    [SerializeField] private Button _memoryButton;

    private bool _isUnlocked;

    private void OnEnable()
    {
        _game.OnStateChanged += CheckUnlock;
        CheckUnlock();
    }

    private void OnDisable()
    {
        _game.OnStateChanged -= CheckUnlock;
    }

    private void CheckUnlock()
    {
        if (_isUnlocked)
            return;

        if (_game.Currency < _requiredCurrency)
        {
            _requirementText.text = $"Requires crystals: {_requiredCurrency:F0}";

            return;
        }

        _isUnlocked = true;

        _unlockedImage.gameObject.SetActive(true);
        _memoryButton.interactable = true;
    }
}

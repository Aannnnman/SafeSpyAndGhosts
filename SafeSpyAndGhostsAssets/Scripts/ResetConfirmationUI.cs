using UnityEngine;
using UnityEngine.UI;

public class ResetConfirmationUI : MonoBehaviour
{
    [SerializeField] private Game _game;

    [SerializeField] private GameObject _confirmationPanel;

    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    private void Start()
    {
        _confirmationPanel.SetActive(false);

        _resetButton.onClick.AddListener(ShowConfirmation);
        _yesButton.onClick.AddListener(ConfirmReset);
        _noButton.onClick.AddListener(CancelReset);
    }

    private void ShowConfirmation()
    {
        _confirmationPanel.SetActive(true);
    }

    private void ConfirmReset()
    {
        _game.ResetProgress();
        _confirmationPanel.SetActive(false);
    }

    private void CancelReset()
    {
        _confirmationPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_resetButton != null)
            _resetButton.onClick.RemoveListener(ShowConfirmation);

        if (_yesButton != null)
            _yesButton.onClick.RemoveListener(ConfirmReset);

        if (_noButton != null)
            _noButton.onClick.RemoveListener(CancelReset);
    }
}

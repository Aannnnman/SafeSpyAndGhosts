using System;
using UnityEngine;
using UnityEngine.UI;

public class SafePasswordChecker : MonoBehaviour
{
    public event Action OnPasswordPassed;

    [SerializeField] private SafeSlotNumberChanger[] _slots;
    [SerializeField] private Button _checkButton;
    [SerializeField] private string _correctPassword;

    private void Start()
    {
        _checkButton.onClick.AddListener(CheckPassword);
    }

    private void OnDestroy()
    {
        _checkButton.onClick.RemoveListener(CheckPassword);
    }

    private void CheckPassword()
    {
        string enteredPassword = "";

        foreach (SafeSlotNumberChanger slot in _slots)
        {
            enteredPassword += slot.Number.ToString();
        }

        if (enteredPassword == _correctPassword)
        {
            OnPasswordPassed?.Invoke();
        }
    }
}

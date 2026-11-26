using UnityEngine;

public class WinMessage : MonoBehaviour
{
    [SerializeField] private SafePasswordChecker _checker;
    [SerializeField] private GameObject _winPanel;

    private void OnEnable()
    {
        _checker.OnPasswordPassed += MessageActivate;
    }

    private void OnDisable()
    {
        _checker.OnPasswordPassed -= MessageActivate;
    }

    private void MessageActivate()
    {
        _winPanel.SetActive(true);
    }
}

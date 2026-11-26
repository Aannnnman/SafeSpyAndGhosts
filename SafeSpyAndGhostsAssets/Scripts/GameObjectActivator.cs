using UnityEngine;
using UnityEngine.UI;

public class GameObjectActivator : MonoBehaviour
{
    [SerializeField] private Button _activateButton;
    [SerializeField] private Button _disactivateButton;
    [SerializeField] private GameObject _gameObject;

    private void OnEnable()
    {
        _activateButton.onClick.AddListener(Activate);
        _disactivateButton.onClick.AddListener(Disactivate);
    }

    private void OnDisable()
    {
        _activateButton.onClick.RemoveListener(Activate);
        _disactivateButton.onClick.RemoveListener(Disactivate);
    }

    private void Activate()
    {
        _gameObject.SetActive(true);
    }

    private void Disactivate()
    {
        _gameObject.SetActive(false);
    }
}

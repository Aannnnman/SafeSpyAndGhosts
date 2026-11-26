using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button _sceneLoadButton;
    [SerializeField] private string _sceneName;

    private void OnEnable()
    {
        _sceneLoadButton.onClick.AddListener(LoadGameplay);
    }

    private void OnDisable()
    {
        _sceneLoadButton.onClick.RemoveListener(LoadGameplay);
    }

    private void LoadGameplay()
    {
        SceneManager.LoadScene(_sceneName);
    }
}

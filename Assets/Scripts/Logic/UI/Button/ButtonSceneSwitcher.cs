using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ButtonSceneSwitcher : MonoBehaviour
{
    [SerializeField] private AssetProvider.Scenes _sceneName;
    [SerializeField] private Button _button;

    [Inject]
    private ISceneLoader _sceneLoader;

    private void OnValidate()
    {
        _button ??= GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(LoadScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LoadScene);
    }

    private void LoadScene()
    {
        _sceneLoader.SwitchTo(_sceneName.ToString());
    }
}
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class SoundToggle : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _muteImage;

    private IAmbientSoundContainer _ambientSound;
    private IGameplaySoundContainer _gameplaySound;
    private IUISoundContainer _uiSound;

    private void OnValidate()
    {
        _button ??= GetComponent<Button>();
        _muteImage ??= GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Toggle);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Toggle);
    }

    [Inject]
    private void Constructor(IAmbientSoundContainer ambientSound, 
        IGameplaySoundContainer gameplaySound, IUISoundContainer uISound)
    {
        _ambientSound = ambientSound;
        _gameplaySound = gameplaySound;
        _uiSound = uISound;
    }

    private void Toggle()
    {
        _ambientSound.TryMute();
        _gameplaySound.TryMute();
        _uiSound.TryMute();
        _muteImage.enabled = !_muteImage.enabled;
    }
}
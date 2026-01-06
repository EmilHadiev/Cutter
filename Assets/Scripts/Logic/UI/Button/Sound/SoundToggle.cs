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
    private IEnemySoundContainer _enemySound;

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

    private void Start()
    {
        TryShowMuteImage();
    }

    [Inject]
    private void Constructor(IAmbientSoundContainer ambientSound, IEnemySoundContainer enemySound,
        IGameplaySoundContainer gameplaySound, IUISoundContainer uISound)
    {
        _ambientSound = ambientSound;
        _gameplaySound = gameplaySound;
        _uiSound = uISound;
        _enemySound = enemySound;
    }

    private void Toggle()
    {
        _ambientSound.TryMute();
        _gameplaySound.TryMute();
        _uiSound.TryMute();
        _enemySound.TryMute();

        TryShowMuteImage();
    }

    private void TryShowMuteImage()
    {
        if (_enemySound.IsMuted())
            _muteImage.enabled = true;
        else
            _muteImage.enabled = false;
    }
}
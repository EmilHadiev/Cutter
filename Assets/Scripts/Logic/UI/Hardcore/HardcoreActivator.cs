using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class HardcoreActivator : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _hardcoreImage;
    [SerializeField] private TMP_Text _hardcoreDescriptionText;

    [SerializeField] private Color _hardcoreColor;
    [SerializeField] private Color _normalColor;

    [Inject] private readonly PlayerProgress _progress;

    private void OnValidate()
    {
        _button ??= GetComponent<Button>();
        _backgroundImage ??= GetComponent<Image>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(TryActivateHardcore);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TryActivateHardcore);
    }

    private void Start()
    {
        if (_progress.IsHardcoreOpen == false)
        {
            DeactivateView();
            gameObject.SetActive(false);
            return;
        }

        if (_progress.IsHardcoreActivate)
            ActivateView();
        else
            DeactivateView();
    }

    private void TryActivateHardcore()
    {
        if (_progress.IsHardcoreActivate)
        {
            _progress.IsHardcoreActivate = false;
            DeactivateView();
        }
        else
        {
            _progress.IsHardcoreActivate = true;
            ActivateView();
        }
    }

    private void ActivateView()
    {
        _hardcoreImage.gameObject.SetActive(true);
        _hardcoreDescriptionText.gameObject.SetActive(true);
        _backgroundImage.color = _hardcoreColor;
    }

    private void DeactivateView()
    {
        _hardcoreImage.gameObject.SetActive(false);
        _hardcoreDescriptionText.gameObject.SetActive(false);
        _backgroundImage.color = _normalColor;
    }
}
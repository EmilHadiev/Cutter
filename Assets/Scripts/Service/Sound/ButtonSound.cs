using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private Button _button;

    [Inject]
    private readonly IUISoundContainer _soundContainer;

    private void OnValidate() => _button ??= GetComponent<Button>();

    private void OnEnable() => _button.onClick.AddListener(PlaySound);

    private void OnDisable() => _button.onClick.RemoveListener(PlaySound);

    private void PlaySound() => _soundContainer.Play(SoundsName.ButtonClick);
}
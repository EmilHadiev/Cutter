using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SwordsContainer : MonoBehaviour
{
    [SerializeField] private SwordTemplateView _view;
    [SerializeField] private Transform _container;
    [SerializeField] private Image _currentSword;

    private PlayerData _playerData;

    private List<SwordTemplateView> _views;
    private IEnumerable<SwordData> _data;

    private void Awake()
    {
        CreateTemplates();
        Show();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _views.Count; i++)
            _views[i].Clicked += SetSwordToPlayer;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _views.Count; i++)
            _views[i].Clicked -= SetSwordToPlayer;
    }

    [Inject]
    private void Constructor(IEnumerable<SwordData> data, PlayerData playerData)
    {
        _data = data;
        _playerData = playerData;
    }

    private void CreateTemplates()
    {
        _views = new List<SwordTemplateView>();

        foreach (var data in _data)
        {
            var view = Instantiate(_view, _container);
            view.Init(data, null);
            _views.Add(view);
        }
    }

    private void Show()
    {
        foreach (var view in _views)
            view.Render();
    }

    private void SetSwordToPlayer(SwordData sword)
    {
        _playerData.Sword = sword.Sword;
    }
}
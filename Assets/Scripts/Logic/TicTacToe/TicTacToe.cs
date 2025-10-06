using DynamicMeshCutter;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
public class TicTacToe : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private Sprite _blind;
    [SerializeField] private Sprite _correct;
    [SerializeField] private bool3x3 _matrix;
    [SerializeField] private TicTacToeField[] _fields;

    private const int MaxMatrixValue = 3;

    private readonly List<bool> _results = new List<bool>(9);

    private int _countCorrect;

    private IPlayer _player;
    private IRewardService _reward;
    private IGameplaySoundContainer _soundContainer;
    private ICutMouseBehaviour _mouseBehavior;

    private void OnValidate()
    {
        if (_fields.Length == 0)
            _fields = GetComponentsInChildren<TicTacToeField>();

        _observer ??= GetComponent<TriggerObserver>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnPlayerEntered;

        for (int i = 0; i < _fields.Length; i++)
            _fields[i].Cut += OnCut;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnPlayerEntered;

        for (int i = 0; i < _fields.Length; i++)
            _fields[i].Cut -= OnCut;
    }

    private void Start()
    {
        FillIn();
    }

    [Inject]
    private void Constructor(IPlayer player, IRewardService rewardService, IGameplaySoundContainer container)
    {
        _player = player;
        _reward = rewardService;
        _soundContainer = container;
    }

    [ContextMenu(nameof(FillIn))]
    private void FillIn()
    {
        // Очищаем матрицу перед заполнением
        _matrix = new bool3x3(false, false, false, false, false, false, false, false, false);
        _results.Clear();

        // Выбираем случайный тип выигрышной комбинации
        int combinationType = UnityEngine.Random.Range(0, MaxMatrixValue); // 0-горизонталь, 1-вертикаль, 2-диагональ

        switch (combinationType)
        {
            case 0: // Горизонтальная линия
                int randomRow = UnityEngine.Random.Range(0, MaxMatrixValue);
                _matrix[randomRow] = new bool3(true, true, true);
                Debug.Log($"Горизонтальная линия в строке {randomRow}");
                break;

            case 1: // Вертикальная линия
                int randomCol = UnityEngine.Random.Range(0, MaxMatrixValue);
                for (int row = 0; row < MaxMatrixValue; row++)
                {
                    _matrix[row][randomCol] = true;
                }
                Debug.Log($"Вертикальная линия в столбце {randomCol}");
                break;

            case 2: // Диагональ
                bool mainDiagonal = UnityEngine.Random.Range(0, MaxMatrixValue - 1) == 0;
                if (mainDiagonal)
                {
                    // Главная диагональ (слева-направо)
                    for (int i = 0; i < MaxMatrixValue; i++)
                    {
                        _matrix[i][i] = true;
                    }
                    Debug.Log("Главная диагональ");
                }
                else
                {
                    // Побочная диагональ (справа-налево)
                    for (int i = 0; i < MaxMatrixValue; i++)
                    {
                        _matrix[i][(MaxMatrixValue - 1) - i] = true;
                    }
                    Debug.Log("Побочная диагональ");
                }
                break;
        }

        // Заполняем список результатов для отображения
        for (int row = 0; row < MaxMatrixValue; row++)
        {
            for (int col = 0; col < MaxMatrixValue; col++)
            {
                bool element = _matrix[row][col];
                _results.Add(element);
                Debug.Log($"Элемент [{row},{col}] = {element}");
            }
            Debug.Log("-----");
        }

        SetElements();
    }

    private void SetElements()
    {
        for (int i = 0; i < _results.Count; i++)
        {
            if (_results[i])
                _fields[i].SetValue(_correct, _results[i]);
            else
                _fields[i].SetValue(_blind, _results[i]);
        }
    }

    private void OnPlayerEntered(Collider collider)
    {
        BlindPlayer();
    }

    private void BlindPlayer()
    {
        _player.Blinder.Blind();
        _soundContainer.Play(SoundsName.TakeDamage);
        Hide();
    }

    private void Hide() => gameObject.SetActive(false);

    private void OnCut(bool isCorrect)
    {
        if (isCorrect == false)
        {
            BlindPlayer();
            return;
        }

        _countCorrect += 1;
        Debug.Log(_countCorrect + " правильных!");

        if (_countCorrect == MaxMatrixValue)
        {
            _reward.AddAdditionalReward(GetReward());
            _soundContainer.Play(SoundsName.AttackObstacleImpact);
            Hide();
        }

    }

    private int GetReward() => _reward.StandartReward / 2;
}

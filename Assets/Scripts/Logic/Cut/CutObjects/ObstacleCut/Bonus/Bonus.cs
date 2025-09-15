using UnityEngine;
using Zenject;

[RequireComponent(typeof(BonusCut))]
public abstract class Bonus : MonoBehaviour
{
    [SerializeField] private BonusCut _bonusCut;

    [Inject]
    private readonly IPlayer _player;

    private void OnValidate() => _bonusCut ??= GetComponent<BonusCut>();

    private void OnEnable() => _bonusCut.Cut += OnCut;
    private void OnDisable() => _bonusCut.Cut -= OnCut;

    private void Update() => transform.LookAt(_player.Movable.Transform);

    protected abstract void OnCut();
}
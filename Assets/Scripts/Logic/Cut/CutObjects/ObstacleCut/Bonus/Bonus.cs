using UnityEngine;

[RequireComponent(typeof(BonusCut))]
public abstract class Bonus : MonoBehaviour
{
    [SerializeField] private BonusCut _bonusCut;

    private void OnValidate() => _bonusCut ??= GetComponent<BonusCut>();

    private void OnEnable() => _bonusCut.Cut += OnCut;
    private void OnDisable() => _bonusCut.Cut -= OnCut;

    protected abstract void OnCut();
}
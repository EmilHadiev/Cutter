using UnityEngine;

[RequireComponent(typeof(CharacterCut))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private CharacterCut _characterCut;
    [SerializeField] private Health _health;

    private void OnValidate()
    {
        _characterCut ??= GetComponent<CharacterCut>();
        _health ??= GetComponent<Health>();
    }

    public ICharacterCut CharacterCut => _characterCut;

    public IHealth Health => _health;
}
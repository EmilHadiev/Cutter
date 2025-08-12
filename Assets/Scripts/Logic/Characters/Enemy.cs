using UnityEngine;

[RequireComponent(typeof(CharacterCut))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private CharacterCut _characterCut;

    private void OnValidate()
    {
        _characterCut ??= GetComponent<CharacterCut>();
    }

    public ICharacterCut CharacterCut => _characterCut;
}

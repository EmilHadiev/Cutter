using System;
using UnityEngine;

public class SpawnOptions : MonoBehaviour
{
    [SerializeField] private bool _isCanDefending;
    [SerializeField] private bool _isArmorPresent;

    private Action<IEnemy>[] _options;

    private void Awake()
    {
        _options = new Action<IEnemy>[] { TryDeactivateDefending, TryDeactivateArmor };
    }

    public void SetupEnemy(IEnemy enemy)
    {
        foreach (var option in _options)
            option?.Invoke(enemy);
    }

    private void TryDeactivateDefending(IEnemy enemy)
    {
        if (_isCanDefending == false)
            enemy.Defender.Deactivate();
        else
            enemy.Defender.Activate();
    }

    private void TryDeactivateArmor(IEnemy enemy)
    {
        if (_isArmorPresent)
            enemy.Armor.Activate();
        else
            enemy.Armor.Deactivate();
    }
}
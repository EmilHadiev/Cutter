using UnityEngine;

public interface IBonusSpawner
{
    void Spawn(BonusType type, Vector3 position);
}
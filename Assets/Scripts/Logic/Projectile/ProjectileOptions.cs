using UnityEngine;

public class ProjectileOptions : MonoBehaviour
{
    [SerializeField] private PlayerDetectorZone _playerDetector;

    private void Awake() => SetOptions();

    private void SetOptions() => _playerDetector.transform.parent = null;

    public void SetPlaceToSpawn(Vector3 position) => SetPlace(transform, position);
    public void SetPlaceToPlayerDetector(Vector3 position) => SetPlace(_playerDetector.transform, position);

    private void SetPlace(Transform transform, Vector3 position) => transform.position = position;
}
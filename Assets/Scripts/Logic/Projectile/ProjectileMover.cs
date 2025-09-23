using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _target;

    public void SetTarget(Transform transform) => _target = transform;

    private void Update()
    {
        if (_target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }
}
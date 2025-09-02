using UnityEngine;

public class CameraFollower : MonoBehaviour, ICameraFollower
{
    [SerializeField] private float _rotationX;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _distance;

    private Transform _player;

    private void LateUpdate()
    {
        if (_player == null)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(_rotationX, 0, 0);
        var position = rotation * new Vector3(0, 0, -_distance) + GetFollowingPosition();
        transform.rotation = rotation;
        transform.position = position;
    }

    public void Follow(Transform target)
    {
        _player = target;
    }

    private Vector3 GetFollowingPosition()
    {
        //сдвигаем нашу камеру по y, чтобы мо могли перемещать ее по Y
        Vector3 followingPosition = _player.position;
        followingPosition.y = _offsetY;
        return followingPosition;
    }

}

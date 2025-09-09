using UnityEngine;

public class CameraFollower : MonoBehaviour, ICameraFollower
{
    [SerializeField] private float _distance;

    private Transform _player;

    private void LateUpdate()
    {
        if (_player == null)
        {
            return;
        }

        var position = new Vector3(0, 0, -_distance) + GetFollowingPosition();
        transform.LookAt(transform.forward);
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
        followingPosition.y = _player.position.y;
        return followingPosition;
    }

}

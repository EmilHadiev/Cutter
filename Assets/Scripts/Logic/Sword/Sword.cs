using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class Sword : MonoBehaviour
{
    [SerializeField] private ParticlePosition _particlePosition;

    private IMousePosition _cutView;
    private SwordView _swordView;
    private ICutMouseBehaviour _cutMouseBehaviour;

    private void OnValidate()
    {
        _particlePosition ??= GetComponentInChildren<ParticlePosition>();
    }

    private void OnEnable()
    {
        _cutMouseBehaviour.CutStarted += _swordView.Show;
        _cutMouseBehaviour.CutEnded += _swordView.Deactivate;
    }

    private void OnDisable()
    {
        _cutMouseBehaviour.CutStarted -= _swordView.Show;
        _cutMouseBehaviour.CutEnded -= _swordView.Deactivate;
    }

    private void Update()
    {
        Vector3 mousePosition = _cutView.GetMousePosition();

        // Следуем за курсором
        transform.LookAt(mousePosition);

        // Вращаем меч в сторону курсора
        //RotateTowardsMouse(mousePosition);
    }

    private void RotateTowardsMouse(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        // Ограничиваем вращение только осью Z (для 2D)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Плавное вращение
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
            15 * Time.deltaTime);
    }

    [Inject]
    private void Constructor(IMousePosition cutView, IFactory factory, ICutMouseBehaviour cutMouseBehaviour)
    {
        _cutView = cutView;
        _cutMouseBehaviour = cutMouseBehaviour;
        _swordView = new SwordView(factory, _particlePosition.transform);
    }
}
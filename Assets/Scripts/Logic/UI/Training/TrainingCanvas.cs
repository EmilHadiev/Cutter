using UnityEngine;

public class TrainingCanvas : MonoBehaviour
{
    [SerializeField] private HintInfo[] _hits;

    private void OnValidate()
    {
        if (_hits.Length == 0)
            _hits ??= GetComponentsInChildren<HintInfo>();
    }

    private int _currentIndex = 0;

    public void TryShowHint()
    {
        if (_currentIndex <= _hits.Length - 1)
        {
            _hits[_currentIndex].gameObject.SetActive(true);
            _hits[_currentIndex].Show();
        }

        IncreaseIndex();
    }

    private void IncreaseIndex() => _currentIndex++;
}
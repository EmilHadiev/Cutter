using UnityEngine;

public abstract class UiState : MonoBehaviour
{
    public virtual void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);
}

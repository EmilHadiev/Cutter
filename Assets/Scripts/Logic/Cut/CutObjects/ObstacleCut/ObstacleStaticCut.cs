using UnityEngine;

[RequireComponent(typeof(MaterialColorChanger))]
public class ObstacleStaticCut : ObstacleCut
{
    [SerializeField] private GameObject _nonStaticPrefab;

    private void Start()
    {
        CreateClone(_nonStaticPrefab);
        SetScale(gameObject.transform.localScale);
    }

    [ContextMenu(nameof(SetRandomRotation))]
    private void SetRandomRotation()
    {
        float y = Random.Range(0, 180);
        transform.Rotate(new Vector3(0, y, 0));
    }
}